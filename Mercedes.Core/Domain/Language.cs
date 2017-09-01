using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    [Serializable]
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public bool Active { get; set; }
        public bool IsDefault { get; set; }
    }
}
