using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CS4125_Kek_Hotel_Management.Models;

namespace CS4125_Kek_Hotel_Management
{
    public class HotelDBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var Rooms = new List<Room>
            {
                new Room{ Price = 1, Description = "0", Room_type = _Room_type.Good},
                new Room{ Price = 2, Description = "1", Room_type = _Room_type.Good},
                new Room{ Price = 3, Description = "2", Room_type = _Room_type.Good},
                new Room{ Price = 3, Description = "3", Room_type = _Room_type.Bad},
                new Room{ Price = 4, Description = "4", Room_type = _Room_type.Bad},
            };
        }
    }
}