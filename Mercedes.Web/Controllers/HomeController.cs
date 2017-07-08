using Mercedes.Core.Domain;
using Mercedes.Core.Infrastructure;
using Mercedes.Services.Contract;
using Mercedes.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.Homebanners = GetAllImageUrl("HomeBanner");
            var allCategory = _carService.GetAllCategory();          
            return View(allCategory);
        }

        List<string> GetAllImageUrl(string Folder)
        {
            List<string> ImagesUrl = new List<string>();
            try
            {
                List<string> FileNames = new List<string>();
                try
                {
                    string sImageFolder = Server.MapPath(@"~/images/"+ Folder);
                    if (!String.IsNullOrEmpty(sImageFolder))
                    {
                        string pattem = "*.png|*.gif|*.jpg|*.jpeg";
                        foreach (string FileFilter in pattem.Split('|'))
                        {
                            FileNames.AddRange(Directory.GetFiles(sImageFolder, FileFilter)
                                         .Select(path => Path.GetFileName(path))
                                         .ToArray());
                        }
                    }
                }
                catch { }

                if (FileNames.Count > 0)
                {
                    String src = "";
                    foreach (string item in FileNames)
                    {
                        src = String.Format("~/images/{0}/{1}", Folder, item);
                        ImagesUrl.Add(src);
                    }
                }
            }
            catch { }

            return ImagesUrl;
        }
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            
            ViewBag.AboutGallery = GetAllImageUrl("Gallery");
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