using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CS4125_Kek_Hotel_Management.Models
{
    public enum _Room_type { Good, Bad };


    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public _Room_type Room_type { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}