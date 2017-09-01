
using Mercedes.Core.Caching;
using Mercedes.Core.Infrastructure;
using Mercedes.Plugins.TrinhMinh.WebApi.Models;
using Mercedes.Services.Contract;
using System.Collections.Generic;
using System.Web.Http;
namespace Mercedes.Plugins.TrinhMinh.WebApi.Controllers
{
    public class TmcApiController : ApiController
    {
        private readonly ICarService _carService; 
        public TmcApiController(ICarService carService)
        {
            _carService = carService;
        }
        [HttpGet]
        [HttpPost]
        public IHttpActionResult TestService()
        {
            var data = _carService.GetAllManufacturers();
            return Ok(data);
            //return Ok("Test completed successfully!!!");
        }
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetAllCategories()
        {
            var allCategories = _carService.GetAllCategory();
            return Ok(allCategories);
        }

        public IHttpActionResult GetAllProjects()
        {
            var allProjects = new List<ProjectModel>();
            allProjects.Add(new ProjectModel()
            {
                Id = 1,
                DisplayOrder = 0,
                ImageUrl = "",
                Name = "LP",
                ProjectType = new ProjectTypeModel() { Name = "Website", DisplayOrder = 0, Id = 1, SystemName = "Website" },
                ProjectTypeId = 1,
                Published = true
            });
            return Ok(allProjects);
        }
    }
}
