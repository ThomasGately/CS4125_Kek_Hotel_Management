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

        public ActionResult BookingRoomDateView()
        {
            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookingRoomDateView(BookingRoomDateViewModles model)
        {
            using (var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>())
            {


                Debug.WriteLine("kek");
                Debug.WriteLine("");
                //var Room1 = context.Rooms.Include(r => r.Booking).Single(r => r.RoomId == 1);
                //var user1 = UserManager.FindById(User.Identity.GetUserId());

                var Room1 = new Room();

                Room1.Price = 1;
                Room1.Description = "3";


                if (Room1 != null)
                {

                    //User1.Bookings.Add(NewBooking);

                    var newB = new List<Booking>
                    {
                        new Booking
                        {
                            Price = Room1.Price,
                            Payed = false,
                            ArrivalDate = model.DateArrival,
                            DepartureDate = model.DateArrival.AddDays(model.NoOfDaysStayed),
                            AadOns = "",
                            ChechIn = 0,
                            ChechOut = 0,

                            ApplicationUser = UserManager.FindById(User.Identity.GetUserId()),
                            Room = Room1
                        }
                    };

                    Room1.Bookings = newB;

                    context.Rooms.Add(Room1);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
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
