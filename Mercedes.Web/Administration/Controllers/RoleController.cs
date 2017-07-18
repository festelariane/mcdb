using FastMapper;
using Mercedes.Admin.Models;
using Mercedes.Admin.Mvc.Attributes;
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
    [UserAdminAuthorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        // GET: Role
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new RoleListModel();
            return View(model);
        }
        [HttpPost]
        public JsonResult List(DataSourceRequest command, RoleListModel model)
        {
            var dataModel = _roleService.GetAllRoles();
            return Json(new { data = dataModel });
        }
        [HttpGet]
        public ActionResult Add()
        {
            var model = new RoleViewModel();
            return View("Create", model);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var role = _roleService.GetRole(Id);
            var model = TypeAdapter.Adapt<UserRole, RoleViewModel>(role);
            return View("Edit", model);
        }
        [HttpPost]
        public JsonResult Update(RoleViewModel model)
        {
            UserRole userRole;
            if (model.Id > 0)
            {
                userRole = _roleService.GetRole(model.Id);
                userRole.UpdatedOn = DateTime.UtcNow;
            }
            else
            {
                userRole = new UserRole();
                userRole.CreatedOn = DateTime.UtcNow;
                userRole.UpdatedOn = userRole.CreatedOn;
            }
            var rs = _roleService.SaveRole(TypeAdapter.Adapt(model, userRole));
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var rs = _roleService.DeleteRole(new UserRole { Id = Id });
            return Json(rs);
        }
    }
}