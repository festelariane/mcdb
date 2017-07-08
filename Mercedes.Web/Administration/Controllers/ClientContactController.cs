using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    public class ClientContactController : Controller
    {
        private readonly IContactService _contactService;
        public ClientContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        // GET: Customer
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public JsonResult List()
        {
            var list = _contactService.GetAllContacts();
            if (list == null)
                list = new List<Contact>();
            
            return Json(new { data = list });
        }
    }
}