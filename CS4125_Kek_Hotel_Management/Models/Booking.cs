using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CS4125_Kek_Hotel_Management.Models
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public double Price { get; set; }

        public bool Payed { get; set; }

        public DateTime DatePayed { get; set; }

        public DateTime ArrivalSate { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ChechIn { get; set; }

        public DateTime ChechOut { get; set; }

        public int BookedCustomerId { get; set; }

        public int BookedRoomId { get; set; }

        public virtual Customer BookedCustomer { get; set; }

        public virtual Room BookedRoom { get; set; }

    }
}