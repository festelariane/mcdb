using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Web.Models
{    
    public class CarModel
    {
        public CarModel()
        {

        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string UrlImage { get; set; }
    }
}