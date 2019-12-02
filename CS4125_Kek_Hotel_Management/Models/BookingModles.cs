using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CS4125_Kek_Hotel_Management.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public double Price { get; set; }

        public bool Payed { get; set; }

        public DateTime DatePayed { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ChechIn { get; set; }

        public DateTime ChechOut { get; set; }

        public String AadOns { get; set; }

        [Required]
        public virtual UserInfo UserInfo { get; set; }

        [Required]
        public virtual Room Room { get; set; }
    }
}