using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mercedes.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //     namespaces: new[] { "Mercedes.Web.Controllers" }
            //);
            
            routes.MapRoute(
                name: "Homepage",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index" },
                 namespaces: new[] { "Mercedes.Web.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "About" },
                 namespaces: new[] { "Mercedes.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Home", action = "Contact" },
                 namespaces: new[] { "Mercedes.Web.Controllers" }
            );
           
            routes.MapRoute(
                name: "Detail",
                 url: "chi-tiet/{code}",
                defaults: new { controller = "VehicleSeries", action = "VehicleModelDetail", code = UrlParameter.Optional },
                 namespaces: new[] { "Mercedes.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index"},
                 namespaces: new[] { "Mercedes.Web.Controllers" }
            );
            routes.MapRoute(
               name: "VehicleSeries",
               url: "{type}/{id}",
               defaults: new { controller = "VehicleSeries", action = "Index", id = UrlParameter.Optional, type = UrlParameter.Optional },
                namespaces: new[] { "Mercedes.Web.Controllers" }
           );
        }
    }
}
