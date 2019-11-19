using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CS4125_Kek_Hotel_Management.Models;

namespace CS4125_Kek_Hotel_Management.DAL
{
    public class HotelInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<HotelContext>
    {
        protected override void Seed(HotelContext context)
        {
            var Users = new List<Customer>
            {
                new Customer()
            };

            Users.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
        }
    }
}