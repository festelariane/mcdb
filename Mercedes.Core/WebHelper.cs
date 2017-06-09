using LP.Core;
using Mercedes.Core.Caching;
using Mercedes.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Mercedes.Core
{
    public class WebHelper : IWebHelper
    {
        private readonly HttpContextBase _httpContext;
        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }
        public void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "")
        {
            if(SiteHelper.GetTrustLevel() > AspNetHostingPermissionLevel.Medium)
            {
                HttpRuntime.UnloadAppDomain();
            }
            else
            {
                bool success = TryWriteWebConfig();
                if (!success)
                {
                    throw new Exception("LandingPage needs to be restarted due to a configuration change, but was unable to do so." + Environment.NewLine +
                        "To prevent this issue in the future, a change to the web server configuration is required:" + Environment.NewLine +
                        "- run the application in a full trust environment, or" + Environment.NewLine +
                        "- give the application write access to the 'web.config' file.");
                }
            }
        }

        public void RestartCaches()
        {
            //Remove cached views
            var keys = (from System.Collections.DictionaryEntry dict in this._httpContext.Cache
                        let k = dict.Key.ToString()
                        //where k.StartsWith(":ViewCacheEntry:")
                        select k).ToList();
            foreach (var key in keys)
            {
                this._httpContext.Cache.Remove(key);
            }
            var allCachedManagers = IocHelper.ResolveAll<ICacheManager>();
            foreach(var cm in allCachedManagers)
            {
                cm.Clear();
            }
        }
        protected virtual bool TryWriteWebConfig()
        {
            try
            {
                // In medium trust, "UnloadAppDomain" is not supported. Touch web.config
                // to force an AppDomain restart.
                File.SetLastWriteTimeUtc(SiteHelper.MapPath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }

            //not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }
        public virtual bool IsStaticResource(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var path = request.Path;
            var extension = VirtualPathUtility.GetExtension(path);

            if (extension == null) return false;

            switch (extension.ToLower())
            {
                case ".axd":
                case ".ashx":
                case ".bmp":
                case ".css":
                case ".gif":
                case ".htm":
                case ".html":
                case ".ico":
                case ".jpeg":
                case ".jpg":
                case ".js":
                case ".png":
                case ".rar":
                case ".zip":
                    return true;
            }

            return false;
        }
    }
}
