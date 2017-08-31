using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Mercedes.Web.Framework.Mvc.Routes
{
    public interface IRouteRegistrator
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
