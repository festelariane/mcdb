using FastMapper;
using Mercedes.Admin.Models.Language;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ISettingService _settingService;

        public LanguageController(ISettingService settingsService)
        {
            this._settingService = settingsService;
        }
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListJson()
        {
            var ls = _settingService.GetAllLanguages(false);
            return Json(new { data = TypeAdapter.Adapt<List<Language>, List<LanguageModel>>(ls) });
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LanguageModel model)
        {
            var result = _settingService.AddLanguage(TypeAdapter.Adapt<LanguageModel, Language>(model));
            if (result > 1)
            {
                return RedirectToAction("Create");
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult Update(int id, string name, string culture, bool isactive, bool isdefault)
        {
            var result = _settingService.UpdateLanguage(new Language()
            {
                Id = id,
                Name = name,
                Culture = culture,
                IsDefault = isdefault,
                Active = isactive
            });
            return Json(result);
        }
        public ActionResult Update(LanguageModel model)
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateAction(LanguageModel model)
        {
            var result = _settingService.UpdateLanguage(TypeAdapter.Adapt<LanguageModel, Language>(model));
            return RedirectToAction("List");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = _settingService.DeleteLanguageById(id);
            return Json(result);
        }
    }
}