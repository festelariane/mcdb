using Mercedes.Admin.Mvc.Attributes;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    [UserAdminAuthorize]
    public class ManufacturerController : Controller
    {
        private readonly ICarService _carService;
        public ManufacturerController(ICarService carService)
        {
            _carService = carService;
        }
        // GET: Manufacturer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult List()
        {
            var ls = _carService.GetAllManufacturers();
            //TempData["ListAcc"] = ls;
            return Json(new { data = ls });
        }
        [HttpPost]
        public JsonResult Add(Manufacturer manufacturer)
        {
            var rs = _carService.AddManufacturer(manufacturer);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var rs = _carService.DeleteManufacturer(new Manufacturer { Id = Id });
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Update(Manufacturer manufacturer)
        {
            var rs = _carService.UpdateManufacturer(manufacturer);
            return Json(rs);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var model = _carService.GetManufacturerById(Id);
            return View("_UpdateManuafacturerFrom", model);
        }
    }
}