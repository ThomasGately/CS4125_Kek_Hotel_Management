using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CS4125_Kek_Hotel_Management.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public string BillingAddress { get; set; }

        public string CardHolderName { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}