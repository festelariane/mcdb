using Mercedes.Core.Infrastructure;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        public HomeController(ICarService carService)
        {
            _carService = carService;
        }
        public ActionResult Index()
        {
            //Test
            //var manufacture1 = _carService.GetManufacturerById(1);
            //var allManufactures = _carService.GetAllManufacturers();
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }        
    }
}