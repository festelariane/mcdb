using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Admin.Models.Plugins
{
    public class PluginModel : BaseModel
    {
        public PluginModel()
        {
        }
        public string Group { get; set; }

        public string FriendlyName { get; set; }
        public string SystemName { get; set; }

        public string Version { get; set; }
        public string Author { get; set; }
        public int DisplayOrder { get; set; }

        public string ConfigurationUrl { get; set; }

        public bool Installed { get; set; }
        public string Description { get; set; }

        public bool CanChangeEnabled { get; set; }
        public bool IsEnabled { get; set; }

        public string LogoUrl { get; set; }

    }
}