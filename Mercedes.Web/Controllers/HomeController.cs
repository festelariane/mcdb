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
       
        public HomeController(ICarService carService)
        {
            _carService = carService;
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
        public JsonResult SendContact(ContactModel info)
        {
            string Mess = "success";
           
            if (info != null)
            {               
                // update
            }

            return Json(Mess);
        }        
    }
}