﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mercedes.Web.Framework.Mvc;
using System.Web.Mvc;

namespace Mercedes.Admin.Models
{
    public class CategoryModel: BaseEntityModel
    {
        public CategoryModel()
        {
            AllManufacturers = new List<SelectListItem>();
        }
        public IList<SelectListItem> AllManufacturers { get; set; }
        public int? SelectedManufactureId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ImageUrl { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public int DisplayOrder { get; set; }
    }
}