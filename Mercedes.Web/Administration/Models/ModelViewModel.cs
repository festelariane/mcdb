using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Models
{
    public class ModelViewModel : BaseEntityModel
    {
        public ModelViewModel()
        {
            AllCategories = new List<SelectListItem>();
        }
        public IList<SelectListItem> AllCategories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        public string Gear { get; set; }
        public string FuelUsed { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}