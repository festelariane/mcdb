using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public class PriceModel : BaseEntity
    {
        public RentType RentType{ get; set; }
        public int RentTypeId { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public decimal Price { get; set; }
    }
}
