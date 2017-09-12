using FastMapper;
using Mercedes.Plugins.TrinhMinh.WebApi.Domain;
using Mercedes.Plugins.TrinhMinh.WebApi.Extensions;
using Mercedes.Plugins.TrinhMinh.WebApi.Models;
using Mercedes.Plugins.TrinhMinh.WebApi.Services;
using Mercedes.Web.Framework.AjaxDataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Controllers
{
    public class TmcAdminController:Controller
    {
        private readonly ITmcMainService _tmcService;
        public TmcAdminController(ITmcMainService tmcService)
        {
            _tmcService = tmcService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Configure()
        {

            return View("~/Plugins/TrinhMinh.WebApi/Views/Configure.cshtml");
        }
        
        public ActionResult ProjectTypeList()
        {
            return View("~/Plugins/TrinhMinh.WebApi/Views/ProjectType/List.cshtml");
        }

        [HttpPost]
        public JsonResult ProjectTypeList(DataSourceRequest command)
        {
            var ls = _tmcService.GetAllProjectTypes();
            var dataModel = ls.Select(x => x.ToModel());
            return Json(new { data = dataModel });
        }
     
        public ActionResult EditProjectType(int Id)
        {
            var projectType = _tmcService.GetProjectType(Id);
            var model = projectType.ToModel();
            return View("~/Plugins/TrinhMinh.WebApi/Views/ProjectType/Edit.cshtml", model);
        }

        [HttpPost]
        public JsonResult SaveProjectType(ProjectTypeModel model)
        {
            ProjectType currentProjectType;
            if (model.Id > 0)
            {
                currentProjectType = _tmcService.GetProjectType(model.Id);
                TypeAdapter.Adapt<ProjectTypeModel, ProjectType>(model, currentProjectType);
                _tmcService.UpdateProjectType(currentProjectType);
            }
            else
            {
                currentProjectType = model.ToEntity();
                _tmcService.AddProjectType(currentProjectType);
            }
            
            return Json(true);
        }
        [HttpPost]
        public JsonResult DeleteProjectType(DataSourceRequest command)
        {
            var ls = _tmcService.GetAllProjectTypes();
            var dataModel = ls.Select(x => x.ToModel());
            return Json(new { data = dataModel });
        }
        public ActionResult AddProjectType()
        {
            var model = new ProjectTypeModel();
            return View("~/Plugins/TrinhMinh.WebApi/Views/ProjectType/Create.cshtml", model);
        }

        //Projects
        public ActionResult ProjectList()
        {
            return View("~/Plugins/TrinhMinh.WebApi/Views/Project/List.cshtml");
        }

        [HttpPost]
        public JsonResult ProjectList(DataSourceRequest command)
        {
            var ls = _tmcService.GetAllProjects();
            var dataModel = ls.Select(x => {
                var item = x.ToModel();
                item.ImageUrl = Url.Content(item.ImageUrl);
                return item;
            } );
            return Json(new { data = dataModel });
        }

        public ActionResult EditProject(int Id)
        {
            var projectType = _tmcService.GetProject(Id);
            var model = projectType.ToModel();
            var allProjectTypes = _tmcService.GetAllProjectTypes();
            foreach (var pt in allProjectTypes)
            {
                model.AllProjectTypes.Add(new SelectListItem() { Text = pt.Name, Value = pt.Id.ToString() });
            }
            model.ImageUrl = Url.Content(model.ImageUrl);
            return View("~/Plugins/TrinhMinh.WebApi/Views/Project/Edit.cshtml", model);
        }

        [HttpPost]
        public JsonResult SaveProject(ProjectModel model)
        {
            Project currentProject;
            if (model.Id > 0)
            {
                currentProject = _tmcService.GetProject(model.Id);
                TypeAdapter.Adapt<ProjectModel, Project>(model, currentProject);
                _tmcService.UpdateProject(currentProject);
            }
            else
            {
                currentProject = model.ToEntity();
                _tmcService.AddProject(currentProject);
            }

            return Json(true);
        }
        [HttpPost]
        public JsonResult DeleteProject(DataSourceRequest command)
        {
            var ls = _tmcService.GetAllProjects();
            var dataModel = ls.Select(x => x.ToModel());
            return Json(new { data = dataModel });
        }
        public ActionResult AddProject()
        {
            var model = new ProjectModel();
            var allProjectTypes = _tmcService.GetAllProjectTypes();
            foreach (var pt in allProjectTypes)
            {
                model.AllProjectTypes.Add(new SelectListItem() { Text = pt.Name, Value = pt.Id.ToString() });
            }
            return View("~/Plugins/TrinhMinh.WebApi/Views/Project/Create.cshtml", model);
        }
    }
}
