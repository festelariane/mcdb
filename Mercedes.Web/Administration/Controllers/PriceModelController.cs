using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class PriceModelController : Controller
    {
        private readonly IRentService _rentService;
        private readonly ICarService _carService;
        public PriceModelController(IRentService rentService, ICarService carService)
        {
            _rentService = rentService;
            _carService = carService;
        }
        // GET: PriceModel
        public ActionResult Index()
        {
            var models = _carService.GetAllModel();
            models.Insert(0, new Model { Id=0,Name="All Model"});
            ViewBag.Models = models;
            var rentTypes = _rentService.GetAllRentTypes();
            ViewBag.RentTypes = rentTypes;
            return View();
        }
        [HttpPost]
        public JsonResult List(int modelId=0)
        {
            IList<PriceModel> ls = null;
            if (modelId == 0)
            {
                ls = _rentService.GetAllPriceModels();
            }
            else
            {
                ls = _rentService.GetAllPriceModelsByModelId(modelId);
            }
            return Json(new { data = ls });
        }
        //[HttpPost]
        //public JsonResult Add(Model model)
        //{
        //    var rs = _rentService.AddModel(model);
        //    return Json(rs);
        //}
        //[HttpPost]
        //public JsonResult Delete(int Id)
        //{
        //    var rs = _rentService.DeleteModel(new Model { Id = Id });
        //    return Json(rs);
        //}
        //[HttpPost]
        //public JsonResult Update(Model model)
        //{
        //    var rs = _rentService.UpdateModel(model);
        //    return Json(rs);
        //}
        //[HttpGet]
        //public ActionResult Update(int Id)
        //{
        //    var model = _rentService.GetModelById(Id);
        //    ViewBag.Manufacturers = _rentService.GetAllManufacturers();
        //    return View("_UpdateModelFrom", model);
        //}
    }
}