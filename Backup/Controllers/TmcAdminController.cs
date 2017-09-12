using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Controllers
{
    public class TmcAdminController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Configure()
        {

            return View("~/Plugins/TrinhMinh.WebApi/Views/Configure.cshtml");
        }
    }
}
