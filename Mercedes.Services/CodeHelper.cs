using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mercedes.Services
{
    public class CodeHelper
    {
        public static string UriCombine(string val, string append)
        {
            if (string.IsNullOrEmpty(val))
            {
                return append;
            }
            if (string.IsNullOrEmpty(append))
            {
                return val;
            }
            return val.TrimEnd('/') + "/" + append.TrimStart('/');
        }
        public static string GetBaseUrl()
        {
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
            {
                appUrl += "/";
            }
            var baseUrl = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, appUrl);

            return baseUrl;

        }
        public static string GetRootUrl()
        {
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
            {
                appUrl += "/";
            }
            var rootUrl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);

            return rootUrl;

        }
    }
}
