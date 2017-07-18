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
            var model = new ModelViewModel();
            var allCategories = _carService.GetAllCategory();
            foreach (var category in allCategories)
            {
                model.AllCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult List()
        {
            var ls = _carService.GetAllModel();
            return Json(new { data = ls });
        }
        [HttpPost]
        public JsonResult Add(ModelViewModel model)
        {
            Model currentModel = new Model();
            currentModel.CreatedOn = DateTime.UtcNow;
            currentModel.UpdatedOn = currentModel.CreatedOn;

            var m = TypeAdapter.Adapt<ModelViewModel, Model>(model, currentModel);
            m.CategoryId = model.SelectedCategoryId.GetValueOrDefault();
            var rs = _carService.AddModel(m);
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
            var viewModel = new ModelViewModel();
            var m = TypeAdapter.Adapt<Model, ModelViewModel>(model,viewModel);
            var allCategories = _carService.GetAllCategory();
            foreach (var category in allCategories)
            {
                viewModel.AllCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
            return View("_UpdateModelFrom", m);
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