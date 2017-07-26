using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Models.Settings
{
    public class SettingModel : BaseEntityModel
    {
        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string Value { get; set; }
    }
}