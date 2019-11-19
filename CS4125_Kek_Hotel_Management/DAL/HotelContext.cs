using CS4125_Kek_Hotel_Management.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CS4125_Kek_Hotel_Management.DAL
{
    public class HotelContext : DbContext
    {

        public HotelContext() : base("HotelContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<Booking>()
                .HasRequired<Customer>(s => s.BookedCustomer)
                .WithMany(g => g.Bookings)
                .HasForeignKey<int>(s => s.BookedCustomerId);

            modelBuilder.Entity<Card>()
                .HasRequired<Customer>(s => s.CardsCustomer)
                .WithMany(g => g.Cards)
                .HasForeignKey<int>(s => s.CardsCustomerId);

            modelBuilder.Entity<Room>()
                .HasOptional(s => s.Booking)
                .WithRequired(ad => ad.BookedRoom);
        }
    }
}