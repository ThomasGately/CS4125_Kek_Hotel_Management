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


    }
}