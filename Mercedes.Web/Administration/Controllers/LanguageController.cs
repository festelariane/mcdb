using FastMapper;
using Mercedes.Admin.Models.Language;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mercedes.Admin.Extensions;
using System.Web.Mvc;
using Mercedes.Web.Framework.AjaxDataSource;

namespace Mercedes.Admin.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ISettingService _settingService;

        public LanguageController(ISettingService settingsService)
        {
            this._settingService = settingsService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public JsonResult List(DataSourceRequest command)
        {
            var ls = _settingService.GetAllLanguages().ToList();
            return Json(new { data = TypeAdapter.Adapt<List<Language>, List<LanguageModel>>(ls) });
        }
        [HttpGet]
        public ActionResult Add()
        {
            var model = new LanguageModel();
            return View("Create", model);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var currentSetting = _settingService.GetLanguageById(Id);
            if (currentSetting == null)
            {
                return Json("This setting doesn't exist");
            }
            return View("Edit", currentSetting.ToModel());
        }
        [HttpPost]
        public JsonResult Update(LanguageModel model)
        {
            Language currentLanguage;
            if (model.Id > 0)
            {
                currentLanguage = _settingService.GetLanguageById(model.Id);
            }
            else
            {
                currentLanguage = new Language();
            }
            model.ToEntity(currentLanguage);
            var rs = _settingService.AddOrUpdate(currentLanguage);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = _settingService.Delete(new Language { Id = id });
            return Json(result);
        }
    }
}