using FastMapper;
using Mercedes.Admin.Models;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mercedes.Admin.Extensions;
using Mercedes.Admin.Mvc.Attributes;
using Mercedes.Web.Framework.AjaxDataSource;

namespace Mercedes.Admin.Controllers
{
    [UserAdminAuthorize]
    public class ModelController : Controller
    {
        private readonly ICarService _carService;
        public ModelController(ICarService carService)
        {
            _carService = carService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new ModelViewModel();
            var allCategories = _carService.GetAllCategory();
            foreach (var category in allCategories)
            {
                model.AllCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new ModelViewModel();
            var allCategories = _carService.GetAllCategory();
            foreach (var category in allCategories)
            {
                model.AllCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
            return View("Create", model);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var currentModel = _carService.GetModelById(Id);
            var model = TypeAdapter.Adapt<Model, ModelViewModel>(currentModel);
            model.SelectedCategoryId = currentModel.CategoryId;
            var allCategories = _carService.GetAllCategory();
            foreach (var category in allCategories)
            {
                model.AllCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
            return View("Edit", model);
        }
        
        [HttpPost]
        public JsonResult List(DataSourceRequest command, ModelListModel model)
        {
            var ls = _carService.GetAllModel();
            return Json(new { data = ls });
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var rs = _carService.DeleteModel(new Model{ Id = Id });
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Update(ModelViewModel model)
        {
            Model carModel;
            if (model.Id > 0)
            {
                carModel = _carService.GetModelById((int)model.Id);
                carModel.UpdatedOn = DateTime.UtcNow;
            }
            else
            {
                carModel = new Model();
                carModel.CreatedOn = DateTime.UtcNow;
                carModel.UpdatedOn = carModel.CreatedOn;
            }
            TypeAdapter.Adapt(model, carModel);
            carModel.CategoryId = model.SelectedCategoryId.GetValueOrDefault();
            var rs = _carService.AddOrUpdateCarModel(carModel);
            return Json(rs);
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
            }).OrderBy(x=> x.DisplayOrder);
            return Json(dataModel);
        }
        [HttpPost]
        public JsonResult AddModelImage(VehiclePictureModel model)
        {
            var entity = model.ToEntity();
            var rs = _carService.AddModelImage(entity);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult RemoveModelPicture(int Id)
        {
            var rs = _carService.DeleteModelImage(Id);
            return Json(rs);
        }
    }
}