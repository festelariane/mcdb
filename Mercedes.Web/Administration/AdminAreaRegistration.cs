using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin
{
    public class AdminAreaRegistration: AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{param}",
                new { controller = "Home", action = "Index", area = "Admin", param = UrlParameter.Optional },
                new[] { "Mercedes.Admin.Controllers" }
            );
            context.MapRoute(
              name: "Admin_Login",
              url: "Admin/Account/Login",
               namespaces: new[] { "Mercedes.Admin.Controllers" }
          );
        }
    }
}