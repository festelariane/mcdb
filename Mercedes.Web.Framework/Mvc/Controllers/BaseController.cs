using log4net;
using Mercedes.Core.Infrastructure;
using Mercedes.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Mercedes.Framework.Mvc.Controllers
{
    public abstract partial class BaseController : Controller
    {
        protected readonly HttpContextBase _contextBase = IocHelper.Resolve<HttpContextBase>();
        protected string GetBaseUrl()
        {
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
            {
                appUrl += "/";
            }
            var baseUrl = string.Format("{0}://{1}{2}", _contextBase.Request.Url.Scheme,_contextBase.Request.Url.Authority, appUrl);

            return baseUrl;

        }

        protected RedirectResult RedirectToUrl(string url)
        {
            if(Url.IsLocalUrl(url) || url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return Redirect(url);
            }
            else
            {
                return Redirect(CodeHelper.UriCombine(CodeHelper.GetRootUrl(), url));
            }
        }

        protected void MapNameValueColletionToObject<T>(T model, NameValueCollection collection, string[] includedProperties = null) where T:new()
        {
            if (model == null)
                throw new Exception("Please assign a model");
            var properties = model.GetType().GetProperties();
            if(includedProperties != null && includedProperties.Length > 0)
            {
                properties = properties.Where(item => includedProperties.Contains(item.Name, StringComparer.InvariantCultureIgnoreCase)).ToArray();
            }
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    if (!collection.AllKeys.Any(x => x.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                        continue;
                    var items = collection.GetValues(property.Name);
                    if (items != null)
                    {
                        var item = items.First();
                        int i = 1;
                        while(string.IsNullOrEmpty(item) && items.Length > i)
                        {
                            item = items[i];
                            i++;
                        }
                        Type newT = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        // ...and change the type
                        object newA = Convert.ChangeType(item, newT);
                        property.SetValue(model, newA, null);
                    }
                }
            }
        }
        public virtual ActionResult InvokeModuleNotSupported()
        {
            return new EmptyResult();
        }
    }
}
