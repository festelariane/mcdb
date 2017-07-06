using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using Mercedes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Web.Controllers
{
    public class VehicleSeriesController : Controller
    {
        private readonly ICarService _carService;
        public VehicleSeriesController(ICarService carService)
        {
            _carService = carService;
        }
        // GET: VehicleSeries
        
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            ViewBag.Message = "";

            var ls = _carService.GetModelByCategoryId(id);
            return View(ls);            
        }

        [HttpGet]
        public ActionResult VehicleModelDetail(int id = 0)
        {
            var detail = _carService.GetModelDetail(id);
            ViewBag.PriceModels = _carService.GetPriceModelByModel(id);
            if (detail == null)
                detail = new Model();
            return View(detail);            
        }
    }
}