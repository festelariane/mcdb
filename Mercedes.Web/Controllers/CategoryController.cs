using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICarService _carService;
        public CategoryController(ICarService carService)
        {
            _carService = carService;
        }
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        // GET: Category
        [HttpGet]
        public ActionResult Category()
        {
            var allCategory = _carService.GetAllCategory();
            return PartialView("Category", allCategory);
        }
    }
}