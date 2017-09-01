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
            var ls = _settingService.GetAllLocaleResourceStringsByLang(lang.Id).ToList();
            return Json(new { data = ls });
        }
        [HttpGet]
        public ActionResult Add()
        {
            var model = new List<LocaleResourceStringModel>();
            ViewBag.Languages = _settingService.GetAllLanguages();
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
            ViewBag.Languages = _settingService.GetAllLanguages();
            return View("Edit", currentSetting.ToList().ToModel());
        }
        [HttpPost]
        public JsonResult Update(FormCollection form)
        {
            List<LocaleResourceString> resources = new List<LocaleResourceString>();
            var key = form["ResourceName"];
            var langs = _settingService.GetAllLanguages();
            foreach (var lang in langs)
            {
                var resource = new LocaleResourceString
                {
                    LanguageId = lang.Id,
                    ResourceName = key,
                    ResourceValue = form[lang.Id.ToString()]
                };
                resources.Add(resource);
            }
            var rs = _settingService.AddOrUpdate(resources);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(string resourceName)
        {
            var result = _settingService.Delete(resourceName);
            return Json(result);
        }
    }
}