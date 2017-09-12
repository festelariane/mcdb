using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Models
{
    public class ProjectModel: BaseEntityModel
    {
        public int ProjectTypeId { get; set; }
        public ProjectTypeModel ProjectType { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
    }
}
