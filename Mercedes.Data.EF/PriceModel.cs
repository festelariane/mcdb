namespace Mercedes.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceModel")]
    public partial class PriceModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceModel()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        public int RentTypeId { get; set; }

        public int VehicleModelId { get; set; }

        public decimal Price { get; set; }

        public virtual Model Model { get; set; }

        public virtual RentType RentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
