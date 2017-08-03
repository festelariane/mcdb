using FastMapper;
using Mercedes.Admin.Extensions;
using Mercedes.Admin.Models;
using Mercedes.Admin.Models.UserManagement;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using Mercedes.Services.Models;
using Mercedes.Web.Framework.AjaxDataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IRoleService _roleService;
        public AccountController(IUserManagementService userManagementService, IUserRegistrationService userRegistrationService, IRoleService roleService)
        {
            _userManagementService = userManagementService;
            _userRegistrationService = userRegistrationService;
            _roleService = roleService;
        }
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult Login()
        {
            return View(new UserLoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SecurityCode.Trim() != (Session["LOGINADMIN"].ToString()))
                {
                    ModelState.AddModelError("", "Security code not match!");
                }
                else if (model.Username.Trim().ToLower() == "admin" && model.Password.Trim().ToLower() == "admin")
                {
                    Session["AccAdmin"] = model;
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is not valid!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Data is not valid!");
            }



            return View(model);
        }
        public ActionResult Logout()
        {
            Session["AccAdmin"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult List()
        {
            var model = new UserListModel();
            return View(model);
        }
        [HttpPost]
        public JsonResult List(DataSourceRequest command, UserListModel model)
        {
            var ls = _userManagementService.GetAll(false,command.Page, command.PageSize);
            var dataModel = ls.Select(x => x.ToModel());
            return Json(new { data = dataModel });
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new UserModel();
            model.AllRoles = new SelectList(_roleService.GetAllRoles(), "Id", "Name");
            return View("Create", model);
        }
        [HttpPost]
        public JsonResult Add(UserModel model)
        {
            UserRegistrationRequest request = new UserRegistrationRequest();
            TypeAdapter.Adapt(model, request);
            foreach (var roleId in model.SelectedRoles)
            {
                request.Roles.Add(new UserRole() { Id = roleId });
            }
            var result = _userRegistrationService.RegisterUser(request);
            return Json(result);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var currentUser = _userManagementService.GetUser(Id);
            if(currentUser == null)
            {
                return Json("Cannot retreive data");
            }
            var model = currentUser.ToModel();
            model.AllRoles = new SelectList(_roleService.GetAllRoles(), "Id", "Name");
            return View("Edit", model);
        }
        [HttpPost]
        public JsonResult Edit(UserModel model)
        {
            var currentUser = _userManagementService.GetUser(model.Id);
            HashSet<int> currentRoles = new HashSet<int>(currentUser.Roles.Select(r => r.Id));
            HashSet<int> selectedRoles = new HashSet<int>(model.SelectedRoles);
            model.ToEntity(currentUser);
            var rs = _userManagementService.UpdateUser(currentUser);
            
            if(!currentRoles.SetEquals(selectedRoles))
            {
                var removedRoles = currentRoles.Except(selectedRoles).ToList();
                if (removedRoles.Count > 0)
                    _roleService.RemoveUserRoles(currentUser, removedRoles);
                foreach(var roleId in model.SelectedRoles)
                {
                    _roleService.AssignUserToRole(currentUser, new UserRole() { Id = roleId });
                }
            }
            return Json(rs);
        }
    }
}