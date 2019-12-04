using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CS4125_Kek_Hotel_Management.Models;
using CS4125_Kek_Hotel_Management.Payment;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CS4125_Kek_Hotel_Management.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public BookingsController()
        {
        }

        public BookingsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Bookings
        public ActionResult Index()
        {
            var booking = db.Bookings.Include(b => b.Room);
            return View(booking.ToList());
        }

        // GET: BookingRoomDateView
        public ActionResult BookingRoomDateView()
        {
            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingRoomDateView(BookingRoomDateViewModles model)
        {
            using (var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>())
            {

                var DepartureDate = model.DateArrival.AddDays(model.NoOfDaysStayed);
                var query =
                //(
                    from Room in context.Rooms
                    join Booking in context.Bookings
                    on Room.RoomId equals Booking.Room.RoomId into RoomBooking
                    from Booking in RoomBooking.DefaultIfEmpty()
                    select new
                    {
                        FoundRoom = Room,
                        FoundBooking = Booking

                    };
                //).FirstOrDefault();

                Room foundRoom = null;

                foreach (var rom in query)
                {
                    if (rom.FoundRoom.Bookings == null)
                    {
                        foundRoom = rom.FoundRoom;
                        break;
                    }
                    if (rom.FoundBooking.ArrivalDate > model.DateArrival.AddDays(model.NoOfDaysStayed) ||
                        rom.FoundBooking.DepartureDate < model.DateArrival)
                    {
                        foundRoom = rom.FoundRoom;
                        break;
                    }
                }

                if (foundRoom == null)
                {
                    return RedirectToAction("NoRoomFound");
                }

                var newBooking = new Booking
                {
                    Price = foundRoom.Price,
                    Payed = false,
                    ArrivalDate = model.DateArrival,
                    DepartureDate = model.DateArrival.AddDays(model.NoOfDaysStayed),
                    AadOns = "",
                    ChechIn = 0,
                    ChechOut = 0,

                    ApplicationUser = UserManager.FindById(User.Identity.GetUserId()),
                    Room = foundRoom
                };

                var pay = new BasePayment(newBooking);
                LoyaltyDiscount decorator1 = new LoyaltyDiscount(pay);

                if (foundRoom.Bookings == null)
                {

                    foundRoom.Bookings = new List<Booking>
                    {
                        newBooking
                    };

                    context.SaveChanges();
                }
                else
                {
                    foundRoom.Bookings.Add(newBooking);
                }


                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: MyBookings
        public ActionResult MyBookings()
        {
            using (var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>())
            {
                string uID = User.Identity.GetUserId();
                List<MyBookings> query =(
                    from Booking in context.Bookings
                    where Booking.ApplicationUser.Id == uID
                    select new MyBookings
                    {
                        BookingId = Booking.BookingId,
                        RoomDescription = Booking.Room.Description,
                        Price = Booking.Price,
                        Payed = Booking.Payed,
                        ArrivalDate = Booking.ArrivalDate,
                        DepartureDate = Booking.DepartureDate,
                        ChechIn = Booking.ChechIn,
                        ChechOut = Booking.ChechOut,
                        AadOns = Booking.AadOns

                    }).ToList();

                foreach (var rom in query)
                {
                    var test = rom;
                }
                var reslut = query;
                return View(query);
            }
        }

        public ActionResult BookingConfirm()
        {
            return View();
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingConfirm()
        {

        }
        */

        public ActionResult NoRoomFound()
        {
            return View();
        }
        
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,Price,Payed,DatePayed,ArrivalSate,DepartureDate,ChechIn,ChechOut,AadOns")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description", booking.BookingId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description", booking.BookingId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,Price,Payed,DatePayed,ArrivalSate,DepartureDate,ChechIn,ChechOut,AadOns")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description", booking.BookingId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
