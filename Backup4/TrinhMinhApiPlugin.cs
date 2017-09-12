using Mercedes.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Mercedes.Plugins.TrinhMinh.WebApi
{
    public class TrinhMinhApiPlugin : BasePlugin, IMiscPlugin
    {
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "TmcAdmin";
            routeValues = new RouteValueDictionary { { "Namespaces", "Mercedes.Plugins.TrinhMinh.WebApi.Controllers" }, { "area", null } };
        }
    }
}
