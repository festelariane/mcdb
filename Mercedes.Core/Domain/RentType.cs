using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public class RentType: BaseEntity
    {
        public string RentTypeSystemName { get; set; }
        public string RentTypeName { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
