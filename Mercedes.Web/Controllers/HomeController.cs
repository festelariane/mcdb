using Mercedes.Core.Domain;
using Mercedes.Core.Infrastructure;
using Mercedes.Services.Contract;
using Mercedes.Web.Models;
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
        private readonly IContactService _contactService;       

        public HomeController(ICarService carService, IContactService contactService)
        {
            _carService = carService;
            _contactService = contactService;
        }
        public ActionResult Index()
        {           
            var allCategory = _carService.GetAllCategory();          
            return View(allCategory);
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            var allCategory = _carService.GetAllCategory();
            return View(allCategory);
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }       

        [HttpPost]
        public JsonResult SendContact(Contact info)
        {
            string Mess = "error";
            
            if (info != null)
            {
                // add/update
                if (_contactService.AddContact(info))
                    Mess = "success";
            }

            return Json(Mess);
        }        
    }
}