
using Mercedes.Core.Caching;
using Mercedes.Core.Infrastructure;
using Mercedes.Services.Contract;
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
    }
}
