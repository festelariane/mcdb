using LP.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using Mercedes.Core.Plugins;

[assembly: PreApplicationStartMethod(typeof(PluginManager), "Initialize")]
namespace Mercedes.Core.Plugins
{
    public class PluginManager
    {
        #region Const

        private const string InstalledPluginsFilePath = "~/App_Data/InstalledPlugins.txt";
        private const string PluginsPath = "~/Plugins";
        private const string ShadowCopyPath = "~/Plugins/bin";

        #endregion
        #region Fields

        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        private static DirectoryInfo _shadowCopyFolder;
        private static bool _clearShadowDirectoryOnStartup;

        #endregion
        public static IEnumerable<PluginDescriptor> ReferencedPlugins { get; set; }
        
        public static IEnumerable<string> IncompatiblePlugins { get; set; }
        public static void MarkPluginAsInstalled(string systemName)
        {
            if (String.IsNullOrEmpty(systemName))
                throw new ArgumentNullException("systemName");
            var filePath = SiteHelper.MapPath(InstalledPluginsFilePath);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                }
            }
            var installedPluginSystemNames = PluginFileParser.ParseInstalledPluginsFile(filePath);
            bool alreadyInstalled = installedPluginSystemNames.FirstOrDefault(x => x.Equals(systemName, StringComparison.InvariantCultureIgnoreCase)) != null;
            if (!alreadyInstalled)
            {
                installedPluginSystemNames.Add(systemName);
            }
            PluginFileParser.SaveInstalledPluginsFile(installedPluginSystemNames, filePath);
        }
        public static void MarkPluginAsUninstalled(string systemName)
        {
            if (String.IsNullOrEmpty(systemName))
                throw new ArgumentNullException("systemName");

            var filePath = SiteHelper.MapPath(InstalledPluginsFilePath);
            if (!File.Exists(filePath))
            {
                return;
            }
            
            var installedPluginSystemNames = PluginFileParser.ParseInstalledPluginsFile(filePath);
            bool alreadyInstalled = installedPluginSystemNames.FirstOrDefault(x => x.Equals(systemName, StringComparison.InvariantCultureIgnoreCase)) != null;
            if (alreadyInstalled)
            {
                installedPluginSystemNames.Remove(systemName);
            }
            PluginFileParser.SaveInstalledPluginsFile(installedPluginSystemNames, filePath);
        }
        public static void MarkAllPluginsAsUninstalled()
        {
            var filePath = SiteHelper.MapPath(InstalledPluginsFilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static void Initialize()
        {
            var pluginFolder = new DirectoryInfo(SiteHelper.MapPath(PluginsPath));
            _shadowCopyFolder = new DirectoryInfo(SiteHelper.MapPath(ShadowCopyPath));
            var referencedPlugins = new List<PluginDescriptor>();
            var incompatiblePlugins = new List<string>();
            _clearShadowDirectoryOnStartup = false;
            try
            {
                var installedPluginSystemNames = PluginFileParser.ParseInstalledPluginsFile(SiteHelper.MapPath(InstalledPluginsFilePath));
                //ensure folders are created
                //will check later
                Directory.CreateDirectory(pluginFolder.FullName);
                Directory.CreateDirectory(_shadowCopyFolder.FullName);
                var binFiles = _shadowCopyFolder.GetFiles("*", SearchOption.AllDirectories);
                if (_clearShadowDirectoryOnStartup)
                {
                    foreach (var f in binFiles)
                    {
                        try
                        {
                            File.Delete(f.FullName);
                        }
                        catch (Exception exc)
                        {
                        }
                    }
                }
                //load description files
                foreach (var dfd in GetPluginDescriptors(pluginFolder))
                {
                    var descriptionFile = dfd.Key;
                    var pluginDescriptor = dfd.Value;
                    //ensure that version of plugin is valid
                    if (!pluginDescriptor.SupportedVersions.Contains(SiteVersion.CurrentVersion, StringComparer.InvariantCultureIgnoreCase))
                    {
                        incompatiblePlugins.Add(pluginDescriptor.SystemName);
                        continue;
                    }
                    //some validation
                    if (String.IsNullOrWhiteSpace(pluginDescriptor.SystemName))
                        throw new Exception(string.Format("A plugin '{0}' has no system name. Try assigning the plugin a unique name and recompiling.", descriptionFile.FullName));
                    if (referencedPlugins.Contains(pluginDescriptor))
                        throw new Exception(string.Format("A plugin with '{0}' system name is already defined", pluginDescriptor.SystemName));

                    pluginDescriptor.Installed = installedPluginSystemNames
                            .FirstOrDefault(x => x.Equals(pluginDescriptor.SystemName, StringComparison.InvariantCultureIgnoreCase)) != null;

                    try
                    {
                        if (descriptionFile.Directory == null)
                            throw new Exception(string.Format("Directory cannot be resolved for '{0}' description file", descriptionFile.Name));
                        //get list of all DLLs in plugins (not in bin!)
                        var pluginFiles = descriptionFile.Directory.GetFiles("*.dll", SearchOption.AllDirectories)
                            .Where(x => !binFiles.Select(q => q.FullName).Contains(x.FullName))
                            .Where(x => IsPluginPackageFolder(x.Directory))
                            .ToList();
                        var mainPluginFile = pluginFiles.FirstOrDefault(x => x.Name.Equals(pluginDescriptor.PluginFileName, StringComparison.InvariantCultureIgnoreCase));
                        pluginDescriptor.OriginalAssemblyFile = mainPluginFile;
                        //shadow copy main plugin file
                        pluginDescriptor.ReferencedAssembly = PerformFileDeploy(mainPluginFile);
                        //load all other referenced assemblies now
                        foreach (var plugin in pluginFiles
                            .Where(x => !x.Name.Equals(mainPluginFile.Name, StringComparison.InvariantCultureIgnoreCase))
                            .Where(x => !IsAlreadyLoaded(x)))
                            PerformFileDeploy(plugin);
                        //init plugin type (only one plugin per assembly is allowed)
                        foreach (var t in pluginDescriptor.ReferencedAssembly.GetTypes())
                            if (typeof(IPlugin).IsAssignableFrom(t))
                                if (!t.IsInterface)
                                    if (t.IsClass && !t.IsAbstract)
                                    {
                                        pluginDescriptor.PluginType = t;
                                        break;
                                    }

                        referencedPlugins.Add(pluginDescriptor);
                    }
                    catch(ReflectionTypeLoadException ex)
                    {
                        var msg = string.Format("Plugin '{0}'. ", pluginDescriptor.FriendlyName);
                        foreach (var e in ex.LoaderExceptions)
                            msg += e.Message + Environment.NewLine;

                        var fail = new Exception(msg, ex);
                        throw fail;
                    }
                    catch (Exception ex)
                    {
                        var msg = string.Format("Plugin '{0}'. {1}", pluginDescriptor.FriendlyName, ex.Message);

                        var fail = new Exception(msg, ex);
                        throw fail;
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = string.Empty;
                for (var e = ex; e != null; e = e.InnerException)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                throw fail;
            }
            ReferencedPlugins = referencedPlugins;
            IncompatiblePlugins = incompatiblePlugins;
        }
        private static IEnumerable<KeyValuePair<FileInfo, PluginDescriptor>> GetPluginDescriptors(DirectoryInfo pluginFolder)
        {
            if (pluginFolder == null)
                throw new ArgumentNullException("pluginFolder");
            var result = new List<KeyValuePair<FileInfo, PluginDescriptor>>();
            foreach(var descriptionFile in pluginFolder.GetFiles("Description.txt",SearchOption.AllDirectories))
            {
                if(!IsPluginPackageFolder(descriptionFile.Directory))
                {
                    continue;
                }
                var pluginDescriptor = PluginFileParser.ParsePluginDescriptionFile(descriptionFile.FullName);
                result.Add(new KeyValuePair<FileInfo, PluginDescriptor>(descriptionFile, pluginDescriptor));
            }
            result.Sort((firstPair, nextPair) => firstPair.Value.DisplayOrder.CompareTo(nextPair.Value.DisplayOrder));
            return result;
        }
        private static bool IsPluginPackageFolder(DirectoryInfo folder)
        {
            if (folder == null)
                return false;
            if (folder.Parent == null)
                return false;
            if (!folder.Parent.Name.Equals("Plugins", StringComparison.InvariantCultureIgnoreCase)) return false;
            return true;
        }
        private static Assembly PerformFileDeploy(FileInfo plug)
        {
            if (plug.Directory == null || plug.Directory.Parent == null)
                throw new InvalidOperationException("The plugin directory for the " + plug.Name + " file exists in a folder outside of the allowed folder hierarchy");

            FileInfo shadowCopiedPlug;
            if(SiteHelper.GetTrustLevel() != AspNetHostingPermissionLevel.Unrestricted)
            {
                //Medium trust. Copy to bin folder
                shadowCopiedPlug = InitializeMediumTrust(plug, new DirectoryInfo(plug.Directory.Name));
            }
            else
            {
                var directory = AppDomain.CurrentDomain.DynamicDirectory;
                Debug.WriteLine(plug.FullName + " to " + directory);
                //were running in full trust so copy to standard dynamic folder
                shadowCopiedPlug = InitializeFullTrust(plug, new DirectoryInfo(directory));
            }
            //we can now register the plugin definition
            var shadowCopiedAssembly = Assembly.Load(AssemblyName.GetAssemblyName(shadowCopiedPlug.FullName));
            //add the reference to the build manager
            Debug.WriteLine("Adding to BuildManager: '{0}'", shadowCopiedAssembly.FullName);
            BuildManager.AddReferencedAssembly(shadowCopiedAssembly);

            return shadowCopiedAssembly;
        }
        private static FileInfo InitializeMediumTrust(FileInfo plug, DirectoryInfo shadowCopyPlugFolder)
        {
            var shouldCopy = true;

            var shadowCopiedPlug = new FileInfo(Path.Combine(shadowCopyPlugFolder.FullName, plug.Name));
            // check if a shadow copied file already exists and if it does, check if it's updated, if not don't copy
            if (shadowCopiedPlug.Exists)
            {
                var areFilesIdentical = shadowCopiedPlug.CreationTimeUtc.Ticks >= plug.CreationTimeUtc.Ticks;
                if (areFilesIdentical)
                {
                    shouldCopy = false;
                }
                else
                {
                    Debug.WriteLine("New plugin found; Deleting the old file: '{0}'", shadowCopiedPlug.Name);
                    File.Delete(shadowCopiedPlug.FullName);
                }
            }
            if (shouldCopy)
            {
                try
                {
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
                }
                catch (IOException)
                {
                    Debug.WriteLine(shadowCopiedPlug.FullName + " is locked, attempting to rename");
                    //this occurs when the files are locked,
                    //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
                    //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
                    try
                    {
                        var oldFile = shadowCopiedPlug.FullName + Guid.NewGuid().ToString("N") + ".old";
                        File.Move(shadowCopiedPlug.FullName, oldFile);
                    }
                    catch (IOException exc)
                    {
                        throw new IOException(shadowCopiedPlug.FullName + " rename failed, cannot initialize plugin", exc);
                    }
                    //ok, we've made it this far, now retry the shadow copy
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
                }
            }

            return shadowCopiedPlug;
        }
        private static FileInfo InitializeFullTrust(FileInfo plug, DirectoryInfo shadowCopyPlugFolder)
        {
            var shadowCopiedPlug = new FileInfo(Path.Combine(shadowCopyPlugFolder.FullName, plug.Name));
            try
            {
                File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
            }
            catch (IOException)
            {
                Debug.WriteLine(shadowCopiedPlug.FullName + " is locked, attempting to rename");
                //this occurs when the files are locked,
                //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
                //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
                try
                {
                    var oldFile = shadowCopiedPlug.FullName + Guid.NewGuid().ToString("N") + ".old";
                    File.Move(shadowCopiedPlug.FullName, oldFile);
                }
                catch (IOException exc)
                {
                    throw new IOException(shadowCopiedPlug.FullName + " rename failed, cannot initialize plugin", exc);
                }
                //ok, we've made it this far, now retry the shadow copy
                File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
            }
            return shadowCopiedPlug;
        }
        private static bool IsAlreadyLoaded(FileInfo fileInfo)
        {
            try
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                if (fileNameWithoutExt == null)
                    throw new Exception(string.Format("Cannot get file extension for {0}", fileInfo.Name));
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    string assemblyName = a.FullName.Split(new[] { ',' }).FirstOrDefault();
                    if (fileNameWithoutExt.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Cannot validate whether an assembly is already loaded. " + exc);
            }
            return false;
        }
    }
}
