using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mercedes.Web.Framework.Website.Controllers
{
    public class HomeController: Controller
    {
        private readonly ICarService _carService;
        private readonly IContactService _contactService;

        public HomeController(ICarService carService, IContactService contactService)
        {
            _carService = carService;
            _contactService = contactService;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
