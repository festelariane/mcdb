using Mercedes.Admin.Mvc.Attributes;
using Mercedes.Core.Domain;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Controllers
{
    [UserAdminAuthorize]
    public class RentTypeController : Controller
    {
        private readonly IRentService _rentService;
        public RentTypeController(IRentService rentService)
        {
            _rentService = rentService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult List()
        {
            var ls = _rentService.GetAllRentTypes();
            return Json(new { data = ls });
        }
        [HttpPost]
        public JsonResult Add(RentType rentType)
        {
            var rs = _rentService.AddRentType(rentType);
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var rs = _rentService.DeleteRentType(new RentType { Id = Id });
            return Json(rs);
        }
        [HttpPost]
        public JsonResult Update(RentType rentType)
        {
            var rs = _rentService.UpdateRentType(rentType);
            return Json(rs);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var model = _rentService.GetRentTypeById(Id);
            return View("_UpdateRentTypeFrom", model);
        }
    }
}