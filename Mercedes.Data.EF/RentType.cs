namespace Mercedes.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RentType")]
    public partial class RentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RentType()
        {
            PriceModels = new HashSet<PriceModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RentTypeSystemName { get; set; }

        [StringLength(50)]
        public string RentTypeName { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceModel> PriceModels { get; set; }
    }
}
