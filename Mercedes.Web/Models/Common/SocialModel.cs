using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Web.Models.Common
{
    public class SocialModel:BaseModel
    {
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
        public string GooglePlusLink { get; set; }
    }
}