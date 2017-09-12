using Mercedes.Core.Domain;
using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Domain
{
    public class ProjectType: BaseEntity
    {
        public string SystemName { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
