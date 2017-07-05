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
        public ActionResult Index(string id)
        {
            ViewBag.Message = "";
           
            if (!String.IsNullOrEmpty(id))
            {
                try {
                    var ls = _carService.GetModelByCategoryId(int.Parse(id));
                    return View(ls);
                } catch { }
            }
             
            return View(new List<Model>()); 
        }

        [HttpGet]
        public ActionResult VehicleModelDetail(int id)
        {
            try {
                var detail = _carService.GetModelDetail(id);
                ViewBag.PriceModels = _carService.GetPriceModelByModel(id);
                return View(detail);
            } catch { }

            return View(new Model());
        }

    }
}