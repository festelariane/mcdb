using Mercedes.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Mercedes.Web.Framework.Plugins
{
    /// <summary>
    /// Misc plugin interface. 
    /// It's used by the plugins that have a configuration page but don't fit any other category (such as payment or tax plugins)
    /// </summary>
    public partial interface IMiscPlugin : IPlugin
    {
        void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);
    }
}
