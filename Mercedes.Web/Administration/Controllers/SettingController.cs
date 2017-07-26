using Mercedes.Admin.Models.Settings;
using Mercedes.Services.Contract;
using Mercedes.Web.Framework.AjaxDataSource;
using System.Linq;
using System.Web.Mvc;
using Mercedes.Admin.Extensions;
using Mercedes.Core.Domain;

namespace Mercedes.Admin.Controllers
{
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new AllSettingsListModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult List(DataSourceRequest command, AllSettingsListModel model)
        {
            var settings = _settingService.FindSettings(model.SearchSettingName, model.SearchSettingValue);

            var dataModel = settings.Select(x => {
                var itemModel = x.ToModel();
                return itemModel;
            });
            return Json(new { data = dataModel });
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new SettingModel();
            return View("Create", model);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var currentSetting = _settingService.GetSettingById(Id);
            if(currentSetting == null)
            {
                return Json("This setting doesn't exist");
            }
            return View("Edit",currentSetting.ToModel());
        }
        [HttpPost]
        public JsonResult Update(SettingModel model)
        {
            Setting currentSetting;
            if (model.Id > 0)
            {
                currentSetting = _settingService.GetSettingById(model.Id);
            }
            else
            {
                currentSetting = new Setting();
            }
            model.ToEntity(currentSetting);
            var rs = _settingService.AddOrUpdate(currentSetting);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var rs = _settingService.Delete(new Setting { Id = Id });
            return Json(rs);
        }
    }
}