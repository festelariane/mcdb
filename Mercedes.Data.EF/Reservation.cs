namespace Mercedes.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int VehicleId { get; set; }

        public DateTime? PickupDate { get; set; }

        public DateTime? DropOffDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ReservationStatusCode { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public int? PriceModelId { get; set; }

        public decimal? Price { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual PriceModel PriceModel { get; set; }

        public virtual Reservation Reservation1 { get; set; }

        public virtual Reservation Reservation2 { get; set; }

        public virtual Reservation Reservation11 { get; set; }

        public virtual Reservation Reservation3 { get; set; }

        public virtual ReservationStatu ReservationStatu { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
