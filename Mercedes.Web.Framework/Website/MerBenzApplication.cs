using Mercedes.Core.Infrastructure;
using Mercedes.Web.Framework.Mvc.Infrastructure;
using Mercedes.Web.Framework.Website.Global_Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mercedes.Web.Framework.Website
{
    public class MerBenzApplication: System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SiteEngine.Instance.Init();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Set viewEngines
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MercedesViewEngine());
        }
    }
}
