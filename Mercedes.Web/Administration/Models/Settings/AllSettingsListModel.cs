using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Admin.Models.Settings
{
    public class AllSettingsListModel : BaseModel
    {
        public string SearchSettingName { get; set; }
        public string SearchSettingValue { get; set; }
    }
}