using Mercedes.Admin.Extensions;
using Mercedes.Admin.Models.Configuration;
using Mercedes.Services.Contract;
using Mercedes.Web.Framework.AjaxDataSource;
using Mercedes.Web.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class EmailAccountController: Controller
    {
        private readonly IEmailAccountService _emailAccountService;
        private readonly IEmailService _emailService;
        public EmailAccountController(IEmailAccountService emailAccountService, IEmailService emailService)
        {
            _emailAccountService = emailAccountService;
            _emailService = emailService;
        }
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
     
        public ActionResult List()
        {
            var model = new EmailListModel();
            return View();
        }
        [HttpPost]
        public JsonResult List(DataSourceRequest command, EmailListModel model)
        {
            var ls = _emailAccountService.GetAllEmailAccounts();
            var dataModel = ls.Select(x => x.ToModel());
            return Json(new { data = dataModel });
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new EmailAccountModel();
            return View("Create", model);
        }
        [HttpPost]
        public JsonResult Add(EmailAccountModel model)
        {
            var email = model.ToEntity();
            _emailAccountService.InsertEmailAccount(email);
            return Json(true);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var currentEmailAccount = _emailAccountService.GetEmailAccountById(Id);
            if (currentEmailAccount == null)
            {
                return Json("Cannot retreive data");
            }
            var model = currentEmailAccount.ToModel();
            return View("Edit", model);
        }
        [HttpPost]
        [FormValueRequired("save")]
        public JsonResult Edit(EmailAccountModel model)
        {
            var currentEmailAccount = _emailAccountService.GetEmailAccountById(model.Id);
            model.ToEntity(currentEmailAccount);
            _emailAccountService.UpdateEmailAccount(currentEmailAccount);
            
            return Json(true);
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("changepassword")]
        public virtual JsonResult ChangePassword(EmailAccountModel model)
        {
            var emailAccount = _emailAccountService.GetEmailAccountById(model.Id);
            if (emailAccount == null)
                return Json(false);
            
            emailAccount.Password = model.Password;
            _emailAccountService.UpdateEmailAccount(emailAccount);
            return Json(true);
        }
        [HttpPost, ActionName("Edit")]
        [FormValueRequired("sendtestemail")]
        public virtual JsonResult SendTestEmail(EmailAccountModel model)
        {
            var emailAccount = _emailAccountService.GetEmailAccountById(model.Id);
            if (emailAccount == null)
                return Json(false);
            
            try
            {
                if (String.IsNullOrWhiteSpace(model.SendTestEmailTo))
                    throw new Exception("Enter test email address");

                string subject = "Testing email functionality.";
                string body = "Email works fine.";
                _emailService.SendEmail(emailAccount, subject, body, emailAccount.Email, emailAccount.DisplayName, model.SendTestEmailTo, null);
            }
            catch (Exception exc)
            {
                
            }
            return Json(true);
        }
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            var emailAccount = _emailAccountService.GetEmailAccountById(id);
            if (emailAccount == null)
                return Json(false);

            try
            {
                _emailAccountService.DeleteEmailAccount(emailAccount);
                return Json(true);
            }
            catch (Exception exc)
            {
                return Json(false);
            }
        }
    }
}