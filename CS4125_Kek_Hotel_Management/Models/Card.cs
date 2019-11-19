using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CS4125_Kek_Hotel_Management.Models
{
    [Table("Cards")]
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public string BillingAddress { get; set; }

        public string CardHolderName { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int CardsCustomerId { get; set; }

        public virtual Customer CardsCustomer { get; set; }
    }
}