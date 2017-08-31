using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using DryIoc.WebApi;
using Owin;
using System.Web.Http;
using DryIoc;
using Mercedes.Core.Infrastructure;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Web.Http.Dispatcher;
using System.Web.Http.Dependencies;
using ImTools;
using DryIoc.Web;
using DryIoc.WebApi;

namespace Mercedes.Web.App_Start
{
    public class WebApiConfig
    {
        public static void Configure(IAppBuilder app, IContainer container)
        {
            var config = GlobalConfiguration.Configuration;
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
            var typeFinder = SiteEngine.Instance.Resolve<ITypeFinder>();
            var allAssemblies = typeFinder.GetAssemblies();
            container = container.With(scopeContext: new AsyncExecutionFlowScopeContext());
            container.WithWebApi(config, allAssemblies, throwIfUnresolved: type => type.Name.EndsWith("Controller"), scopeContext: null);
        }
    }
}