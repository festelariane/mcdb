namespace Mercedes.Data.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MercedesDataContext : DbContext
    {
        public MercedesDataContext()
            : base("name=MercedesDataContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<EmailAccount> EmailAccounts { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Model_Image_Mapping> Model_Image_Mapping { get; set; }
        public virtual DbSet<PriceModel> PriceModels { get; set; }
        public virtual DbSet<RentType> RentTypes { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationStatu> ReservationStatus { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Models)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.DrivingLicenseNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.IdNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .Property(e => e.FuelUsed)
                .IsFixedLength();

            modelBuilder.Entity<Model>()
                .HasMany(e => e.PriceModels)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.VehicleModelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Model)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RentType>()
                .Property(e => e.RentTypeSystemName)
                .IsUnicode(false);

            modelBuilder.Entity<RentType>()
                .HasMany(e => e.PriceModels)
                .WithRequired(e => e.RentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.ReservationStatusCode)
                .IsUnicode(false);

            modelBuilder.Entity<Reservation>()
                .HasOptional(e => e.Reservation1)
                .WithRequired(e => e.Reservation2);

            modelBuilder.Entity<Reservation>()
                .HasOptional(e => e.Reservation11)
                .WithRequired(e => e.Reservation3);

            modelBuilder.Entity<ReservationStatu>()
                .Property(e => e.ReservationStatusCode)
                .IsUnicode(false);

            modelBuilder.Entity<ReservationStatu>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.ReservationStatu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithMany(e => e.Users)
                .Map(m => m.ToTable("UsersToRoles").MapLeftKey("UserId").MapRightKey("UserRoleId"));

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);
        }
    }
}
