using FastMapper;
using Mercedes.Admin.Models.Language;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using Mercedes.Web.Framework.AjaxDataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mercedes.Admin.Extensions;

namespace Mercedes.Admin.Controllers
{
    public class LocaleResourceStringController : Controller
    {
        private readonly ISettingService _settingService;

        public LocaleResourceStringController(ISettingService settingsService)
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
            var languages = _settingService.GetAllLanguages().Where(p=>p.Active).ToList();
            ViewBag.Languages = TypeAdapter.Adapt<List<Language>, List<LanguageModel>>(languages);
            return View(new List<LocaleResourceStringModel>());
        }
        [HttpPost]
        public JsonResult List(DataSourceRequest command)
        {
            var lang = _settingService.GetAllLanguages().FirstOrDefault(p => p.Active);
            if (null == lang)
            {
                return Json(null);
            }
            var ls = _settingService.GetAllLocaleResourceStringsByLang(lang.Id);
            return Json(new { data = ls });
        }
        [HttpGet]
        public ActionResult Add()
        {
            var model = new List<LocaleResourceStringModel>();
            return View("Create", model);
        }
        [HttpGet]
        public ActionResult Update(string resourceName)
        {
            var currentSetting = _settingService.GetAllLocaleResourceStringsByKey(resourceName);
            if (currentSetting == null)
            {
                return Json("This setting doesn't exist");
            }
            return View("Edit", currentSetting.ToList().ToModel());
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
    }
}