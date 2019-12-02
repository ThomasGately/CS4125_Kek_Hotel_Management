using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CS4125_Kek_Hotel_Management.Models
{
    public class UserInfo
    {
        [Key]
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LoyalityDiscount { get; set; }

        [Required]
        public virtual ApplicationUser ApplicationUser_Id { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}