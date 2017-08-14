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
    }
}