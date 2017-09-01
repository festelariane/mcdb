using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Models
{
    public class ProjectTypeModel: BaseEntityModel
    {
        public string SystemName { get; set; }
        public string Name { get; set; }
        public string WebsiteUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}
