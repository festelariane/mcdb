using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public class Model : BaseEntity
    {
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
