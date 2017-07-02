using Mercedes.Services.Contract;
using Mercedes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Web.Controllers
{
    public class VehicleSeriesController : Controller
    {
        private readonly ICarService _carService;
        public VehicleSeriesController(ICarService carService)
        {
            _carService = carService;
        }
        // GET: VehicleSeries
        
        [HttpGet]
        public ActionResult Index(string id)
        {
            String message = "";
            var ls = new List<CarModel>();
            switch (id)
            {
                case "1":
                    ls.Add(new CarModel() { Code = "C200", Name = "Mercedes C200 2017", Price = "2,200 USD / THÁNG", UrlImage = "c200-img-01.jpg" });
                    ls.Add(new CarModel() { Code = "C250", Name = "Mercedes C250 2017", Price = "2,500 USD / THÁNG", UrlImage = "c250-img-01.jpg" });
                    message = "C-Class Series";
                    break;
                case "2":
                    ls.Add(new CarModel() { Code = "E200", Name = "Mercedes E200 2017", Price = "2,700 USD / THÁNG", UrlImage = "e200-img-01.jpg" });
                    ls.Add(new CarModel() { Code = "E250", Name = "Mercedes E250 2017", Price = "2,900 USD / THÁNG", UrlImage = "e250-img-01.jpg" });
                    ls.Add(new CarModel() { Code = "E300", Name = "Mercedes E300 2017", Price = "3,300 USD / THÁNG", UrlImage = "e300-img-01.jpg" });
                    message = "E-Class Series";
                    break;
                case "3":
                    ls.Add(new CarModel() { Code = "S400", Name = "Mercedes S400 2017", Price = "4,000 USD / THÁNG", UrlImage = "s400-img-01.jpg" });
                    ls.Add(new CarModel() { Code = "S500", Name = "Mercedes S500 2017", Price = "5,300 USD / THÁNG", UrlImage = "s500-img-01.jpg" });
                    message = "S-Class Series";
                    break;
            }

            ViewBag.Message = message;
            return View(ls);            
        }

        [HttpGet]
        public ActionResult VehicleModelDetail(string code)
        {
            var detail = new ModelDetail();
            switch (code)
            {
                case "C200":
                    detail = new ModelDetail()
                    {
                        Name = "Mercedes Benz C200",
                        Year = "2017",
                        Price = "2,200 USD / THÁNG",
                        Type = "Số tự động",
                        Color = "Bạc - Đen - Xám",
                        Fuels = "10 lít / 100km",
                        UrlImage = "c200-img-01.jpg"
                    };
                    break;
                case "C250":
                    detail = new ModelDetail()
                    {
                        Name = "Mercedes Benz C250",
                        Year = "2017",
                        Price = "2,500 USD / THÁNG",
                        Type = "Số tự động",
                        Color = "Bạc - Đen - Xám",
                        Fuels = "10 lít / 100km",
                        UrlImage = "c250-img-01.jpg"
                    };
                    break;
                case "E200":
                    detail = new ModelDetail()
                    {
                        Name = "Mercedes Benz E200",
                        Year = "2017",
                        Price = "2,700 USD / THÁNG",
                        Type = "Số tự động",
                        Color = "Bạc - Đen - Xám",
                        Fuels = "10 lít / 100km",
                        UrlImage = "c250-img-01.jpg"
                    };
                    break;
                case "E250":
                    detail = new ModelDetail()
                    {
                        Name = "Mercedes Benz E250",
                        Year = "2017",
                        Price = "2,900 USD / THÁNG",
                        Type = "Số tự động",
                        Color = "Bạc - Đen - Xám",
                        Fuels = "10 lít / 100km",
                        UrlImage = "c250-img-01.jpg"
                    };
                    break;
                case "E300":
                    detail = new ModelDetail()
                    {
                        Name = "Mercedes Benz E300",
                        Year = "2017",
                        Price = "3,300 USD / THÁNG",
                        Type = "Số tự động",
                        Color = "Bạc - Đen - Xám",
                        Fuels = "10 lít / 100km",
                        UrlImage = "c250-img-01.jpg"
                    };
                    break;
                case "S400":
                    detail = new ModelDetail()
                    {
                        Name = "Mercedes Benz S400",
                        Year = "2017",
                        Price = "4,000 USD / THÁNG",
                        Type = "Số tự động",
                        Color = "Bạc - Đen - Xám",
                        Fuels = "10 lít / 100km",
                        UrlImage = "c250-img-01.jpg"
                    };
                    break;
                case "S500":
                    detail = new ModelDetail()
                    {
                        Name = "Mercedes Benz S500",
                        Year = "2017",
                        Price = "5,300 USD / THÁNG",
                        Type = "Số tự động",
                        Color = "Bạc - Đen - Xám",
                        Fuels = "10 lít / 100km",
                        UrlImage = "c250-img-01.jpg"
                    };
                    break;
            }
            return View(detail);
        }
               
    }
}