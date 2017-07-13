using Mercedes.Admin.Models;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mercedes.Admin.Extensions;

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
            ViewBag.Categories = _carService.GetAllCategory();
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

        public ActionResult ModelPictures(int modelId)
        {
            var model = new ManageVehiclePictureModel();
            model.VehicleModelId = modelId;
            model.NewPictureModel.VehicleModelId = modelId;
            var allImages = _carService.GetVehicleModelImageUrl(modelId);
            foreach(var imgEntity in allImages)
            {
                var imgModel = imgEntity.ToModel();
                imgModel.FullImageUrl = Url.Content(imgModel.FullImageUrl);
                imgModel.ThumbImageUrl = Url.Content(imgModel.ThumbImageUrl);
                model.VehiclePictures.Add(imgModel);
            }
            return View(model);   
        }
        [HttpPost]
        public JsonResult LoadModelPictures(int modelId)
        {
            var model = new ManageVehiclePictureModel();
            model.VehicleModelId = modelId;
            model.NewPictureModel.VehicleModelId = modelId;
            var allImages = _carService.GetVehicleModelImageUrl(modelId);

            var dataModel = allImages.Select(x => {
                var imgModel = x.ToModel();
                imgModel.FullImageUrl = Url.Content(imgModel.FullImageUrl);
                imgModel.ThumbImageUrl = Url.Content(imgModel.ThumbImageUrl);
                return imgModel;
            });
            return Json(dataModel);
        }
        [HttpPost]
        public JsonResult AddModelImage(VehiclePictureModel model)
        {
            var entity = model.ToEntity();
            var rs = _carService.AddModelImage(entity);
            return Json(rs);
        }
    }
}