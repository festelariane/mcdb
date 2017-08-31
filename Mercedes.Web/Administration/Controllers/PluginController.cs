using Mercedes.Admin.Models.Plugins;
using Mercedes.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mercedes.Core.Extensions;
using Mercedes.Admin.Extensions;
using System.Web.Routing;
using Mercedes.Core.Plugins;
using Mercedes.Core;

namespace Mercedes.Admin.Controllers
{
    public class PluginController: Controller
    {
        private readonly IWebHelper _webHelper;
        private readonly IPluginFinder _pluginFinder;
        public PluginController(IPluginFinder pluginFinder, IWebHelper webHelper)
        {
            _pluginFinder = pluginFinder;
            _webHelper = webHelper;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new PluginListModel();
            model.AvailableLoadModes = LoadPluginsMode.All.ToSelectList(true).ToList();
            model.AvailableGroups.Add(new SelectListItem { Text = "All", Value = "" });
            foreach (var g in _pluginFinder.GetPluginGroups())
                model.AvailableGroups.Add(new SelectListItem { Text = g, Value = g });
            return View(model);
        }
        [HttpPost]
        public JsonResult List(PluginListModel model)
        {
            var loadMode = (LoadPluginsMode)model.SearchLoadModeId;
            var pluginDescriptors = _pluginFinder.GetPluginDescriptors(loadMode, group: model.SearchGroup).ToList();
            var data = pluginDescriptors.Select(x => 
            {
                return PreparePluginModel(x);
            });
            return Json(new { data = data });
        }
        [NonAction]
        protected virtual PluginModel PreparePluginModel(PluginDescriptor pluginDescriptor)
        {
            var pluginModel = pluginDescriptor.ToModel();
            if (pluginDescriptor.Installed)
            {
                pluginModel.ConfigurationUrl = Url.Action("ConfigureMiscPlugin", "Plugin", new { systemName = pluginDescriptor.SystemName });
            }
            return pluginModel;
        }
        [HttpPost]
        public virtual ActionResult Install(string pluginSystemName)
        {
            var pluginDescriptor = _pluginFinder.GetPluginDescriptorBySystemName(pluginSystemName, LoadPluginsMode.All);
            if (pluginDescriptor == null)
                return RedirectToAction("List");
            
            if (pluginDescriptor.Installed)
                return RedirectToAction("List");

            //install plugin
            pluginDescriptor.Instance().Install();
            _webHelper.RestartAppDomain();
            return Json(true);
        }
        [HttpPost]
        public virtual ActionResult Uninstall(string pluginSystemName)
        {
            throw new NotImplementedException("Uninstall");
        }

        public virtual ActionResult ConfigureMiscPlugin(string systemName)
        {
            var descriptor = _pluginFinder.GetPluginDescriptorBySystemName<IMiscPlugin>(systemName);
            if (descriptor == null || !descriptor.Installed)
                return Redirect("List");

            var plugin = descriptor.Instance<IMiscPlugin>();

            string actionName, controllerName;
            RouteValueDictionary routeValues;
            plugin.GetConfigurationRoute(out actionName, out controllerName, out routeValues);
            var model = new MiscPluginModel();
            model.FriendlyName = descriptor.FriendlyName;
            model.ConfigurationActionName = actionName;
            model.ConfigurationControllerName = controllerName;
            model.ConfigurationRouteValues = routeValues;
            return View(model);
        }
    }
}