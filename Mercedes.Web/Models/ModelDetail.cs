using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Web.Models
{
    public class ModelDetail
    {
        public ModelDetail()
        {

        }       
        public string Name { get; set; }
        public string Year { get; set; }
        public string Price { get; set; }
        public string UrlImage { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Fuels { get; set; }
    }
}