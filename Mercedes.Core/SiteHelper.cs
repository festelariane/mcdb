using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace LP.Core
{
    public static class SiteHelper
    {
        private static AspNetHostingPermissionLevel? _trustLevel;
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if(!_trustLevel.HasValue)
            {
                _trustLevel = AspNetHostingPermissionLevel.None;
                //determine maximum trust level
                foreach (AspNetHostingPermissionLevel trustLevel in new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal 
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break;
                    }
                    catch (SecurityException ex)
                    {
                        
                    }
                }
                
            }
            return _trustLevel.Value;
        }
        static SiteHelper()
        {
            
        }
        public static string MapPath(string path)
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
        public static object GetDefaultValue(Type type)
        {
            object retVal = null;
            if (type.IsValueType)
            {
                retVal = Activator.CreateInstance(type);
            }
            return retVal;
        }
    }
}
