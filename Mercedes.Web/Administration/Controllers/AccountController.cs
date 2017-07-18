using Mercedes.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
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
    }
}