using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Models
{
    public class ProjectModel: BaseEntityModel
    {
        public ProjectModel()
        {
            AllProjectTypes = new List<SelectListItem>();
        }
        public IList<SelectListItem> AllProjectTypes { get; set; }
        public int ProjectTypeId { get; set; }
        public ProjectTypeModel ProjectType { get; set; }
        [UIHint("Picture")]
        public string ImageUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
    }
}
