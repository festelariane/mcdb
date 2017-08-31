using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Mercedes.Core.Plugins
{
    public partial interface IMiscPlugin : IPlugin
    {
        void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);
    }
}
