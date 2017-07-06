using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class ModelController : Controller
    {
        private readonly ICarService _carService;
        public ModelController(ICarService carService)
        {
            _carService = carService;
        }
        public ActionResult Index()
        {
            ViewBag.Manufacturers = _carService.GetAllManufacturers();
            return View(new Model());
        }
        [HttpPost]
        public JsonResult List()
        {
            var ls = _carService.GetAllModel();
            return Json(new { data = ls });
        }
        [HttpPost]
        public JsonResult Add(Model model)
        {
            var rs = _carService.AddModel(model);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var rs = _carService.DeleteModel(new Model{ Id = Id });
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Update(Model model)
        {
            var rs = _carService.UpdateModel(model);
            return Json(rs);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var model = _carService.GetModelById(Id);
            ViewBag.Categories = _carService.GetAllCategory();
            return View("_UpdateModelFrom", model);
        }
    }
}