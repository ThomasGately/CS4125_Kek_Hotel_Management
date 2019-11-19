using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CS4125_Kek_Hotel_Management.Models
{
    [Table("Invoices")]
    public class Invoice
    {
        [Key]


        public int CardId { get; set; }
        public int BookingId { get; set; }

        public virtual Card Card { get; set; }

        public virtual Booking Booking { get; set; }
    }
}