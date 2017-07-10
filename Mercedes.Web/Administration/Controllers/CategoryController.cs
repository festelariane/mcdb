using FastMapper;
using Mercedes.Admin.Models;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using Mercedes.Web.Framework.AjaxDataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICarService _carService;
        public CategoryController(ICarService carService)
        {
            _carService = carService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new CategoryListModel();
            return View(model);
        }
        [HttpPost]
        public JsonResult List(DataSourceRequest command, CategoryListModel model)
        {
            var ls = _carService.GetAllCategory();
            return Json(new { data = ls });
        }
        [HttpGet]
        public ActionResult Add()
        {
            var model = new CategoryModel();
            var allManufacturers = _carService.GetAllManufacturers();
            foreach (var manufacture in allManufacturers)
            {
                model.AllManufacturers.Add(new SelectListItem() { Text = manufacture.Code, Value = manufacture.Id.ToString() });
            }
            return View("Edit", model);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var currentCategory = _carService.GetCategoryById(Id);
            var model = TypeAdapter.Adapt<Category, CategoryModel>(currentCategory);
            model.SelectedManufactureId = currentCategory.ManufacturerId;
            var allManufacturers = _carService.GetAllManufacturers();
            foreach (var manufacture in allManufacturers)
            {
                model.AllManufacturers.Add(new SelectListItem() { Text = manufacture.Code, Value = manufacture.Id.ToString() });
            }
            return View("Edit", model);
        }
        [HttpPost]
        public JsonResult ActionResult(Model model)
        {
            var rs = _carService.AddModel(model);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var rs = _carService.DeleteCategory(new Category{ Id = Id });
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Update(CategoryModel model)
        {
            Category currentCategory;
            if (model.Id > 0)
            {
                currentCategory = _carService.GetCategoryById(model.Id);
                currentCategory.UpdatedOn = DateTime.UtcNow;
            }
            else
            {
                currentCategory = new Category();
                currentCategory.CreatedOn = DateTime.UtcNow;
                currentCategory.UpdatedOn = currentCategory.CreatedOn;
            }
            
            var category = TypeAdapter.Adapt<CategoryModel, Category>(model,currentCategory);
            category.ManufacturerId = model.SelectedManufactureId.GetValueOrDefault();
            var rs = _carService.SaveCategory(category);
            return Json(rs);
        }
       
    }
}