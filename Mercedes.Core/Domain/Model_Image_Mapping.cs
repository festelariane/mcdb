using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public class Model_Image_Mapping : BaseEntity
    {
        public int VehicleModelId { get; set; }
        public string FullImageUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}
