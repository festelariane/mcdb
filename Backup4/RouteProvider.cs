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
            );
            
            //Project Types
            routes.MapRoute(
                name: "Tmc.ProjectTypeList",
                url: "admin-plugin/tmc/projecttype/list",
                defaults: new { controller = "TmcAdmin", action = "ProjectTypeList" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });
            routes.MapRoute(
                name: "Tmc.AddProjectType",
                url: "admin-plugin/tmc/projecttype/add",
                defaults: new { controller = "TmcAdmin", action = "AddProjectType" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });
            routes.MapRoute(
                name: "Tmc.EditProjectType",
                url: "admin-plugin/tmc/projecttype/edit",
                defaults: new { controller = "TmcAdmin", action = "EditProjectType" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });
            routes.MapRoute(
                name: "Tmc.SaveProjectType",
                url: "admin-plugin/tmc/projecttype/save",
                defaults: new { controller = "TmcAdmin", action = "SaveProjectType" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });

            //Projects
            routes.MapRoute(
                name: "Tmc.ProjectList",
                url: "admin-plugin/tmc/project/list",
                defaults: new { controller = "TmcAdmin", action = "ProjectList" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });
            routes.MapRoute(
                name: "Tmc.AddProject",
                url: "admin-plugin/tmc/project/add",
                defaults: new { controller = "TmcAdmin", action = "AddProject" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });
            routes.MapRoute(
                name: "Tmc.EditProject",
                url: "admin-plugin/tmc/project/edit",
                defaults: new { controller = "TmcAdmin", action = "EditProject" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });
            routes.MapRoute(
                name: "Tmc.SaveProject",
                url: "admin-plugin/tmc/project/save",
                defaults: new { controller = "TmcAdmin", action = "SaveProject" },
                namespaces: new[] { "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" });
        }
    }
}
