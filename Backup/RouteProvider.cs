using Mercedes.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace Mercedes.Plugins.TrinhMinh.WebApi
{
    public class RouteProvider : IRouteProvider
    {
        public int Priority
        {
            get
            {
                return 0;
            }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var r = routes.MapHttpRoute(
                name: "DefaultTmcApi",
                routeTemplate: "tmapi/{controller}/{action}/{id}",
                defaults: new { controller = "TmcApi", action = "Index", id = UrlParameter.Optional,@namespace = "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" }
                //namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" }
            );
            //.Constraints["Namespaces"] = new string[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" };
        }
    }
}
