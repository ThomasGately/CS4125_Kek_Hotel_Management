using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;

namespace CS4125_Kek_Hotel_Management.Models
{
    public class BookingRoomDateViewModles
    {
        [Required]
        [Display(Name = "Room Type")]
        public _Room_type Room_type { get; set; }

        [Required]
        [Display(Name = "Number of Gusets")]
        public int NoOfGuests { get; set; }

        [Required]
        [Display(Name = "Date of Arrival")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateArrival { get; set; }

        [Required]
        [Display(Name = "Number of days you wish to stay")]
        public int NoOfDaysStayed { get; set; }
    }

    public class MyBookings
    {
        public int BookingId { get; set; }

        public string RoomDescription { get; set; }

        public double Price { get; set; }

        public bool Payed { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public int ChechIn { get; set; }

        public int ChechOut { get; set; }

        public String AadOns { get; set; }
    }
}