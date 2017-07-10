using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public class Model : BaseEntity
    {
        public Category Category { get; set; }
        public RentType RentType { get; set; }
        public PriceModel PriceModel { get; set; }
        public IList<Model_Image_Mapping> ImageURLs { get; set; }

        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }       
    }
}
