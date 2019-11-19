using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CS4125_Kek_Hotel_Management.Models
{
    public enum _Room_type { Good, Bad };

    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public Boolean Occupied { get; set; }

        public _Room_type Room_type { get; set; }

        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }
    }
}