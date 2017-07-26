using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public class Setting : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
