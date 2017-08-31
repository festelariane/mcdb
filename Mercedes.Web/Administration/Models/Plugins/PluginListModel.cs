using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Models.Plugins
{
    public class PluginListModel : BaseModel
    {
        public PluginListModel()
        {
            AvailableLoadModes = new List<SelectListItem>();
            AvailableGroups = new List<SelectListItem>();
        }

        public int SearchLoadModeId { get; set; }
        public string SearchGroup { get; set; }

        public IList<SelectListItem> AvailableLoadModes { get; set; }
        public IList<SelectListItem> AvailableGroups { get; set; }
    }
}