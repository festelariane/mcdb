using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes.Admin.Models
{
    public class VehiclePictureModel: BaseModel
    {
        public int VehicleModelId { get; set; }
        [UIHint("Picture")]
        public string FullImageUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}