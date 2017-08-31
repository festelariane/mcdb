namespace Mercedes.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Model_Image_Mapping
    {
        public int Id { get; set; }

        public int VehicleModelId { get; set; }

        [StringLength(500)]
        public string FullImageUrl { get; set; }

        [StringLength(500)]
        public string ThumbImageUrl { get; set; }

        public int DisplayOrder { get; set; }
    }
}
