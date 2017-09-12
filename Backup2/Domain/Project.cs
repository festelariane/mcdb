using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Domain
{
    public class Project : BaseEntity
    {
        public int ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }
        public string ImageUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
    }
}
