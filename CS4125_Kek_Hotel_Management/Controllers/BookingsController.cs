using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        public ActionResult BookingRoomDateView(BookingRoomDateViewModles model)
        {
            Debug.WriteLine("kek");
            Debug.WriteLine("");

            if (ModelState.IsValid)
            {
                DbSet<Room> Rooms = db.Rooms;
                DbSet<Booking> bookings = db.Bookings;
                DbSet<UserInfo> UserInfos = db.UesrInfos;

                var userid = User.Identity.GetUserId();

                var query1 =
                    (from Room in Rooms
                     join Booking in bookings
                     on Room.RoomId
                     equals Booking.Room.RoomId into RoomGroup
                     //where Booking.ArrivalDate > model.DateDeparture ||
                     //Booking.DepartureDate < model.DateArrival
                     select new
                     {
                         Room.RoomId,
                         Room.Price,
                         FoundRoom = Room
                    }).FirstOrDefault();

                var query2 =
                    (from UserInfo in UserInfos
                     where UserInfo.ApplicationUser_Id.Id == userid
                     select new
                     {
                         FounUser = UserInfo
                     }).FirstOrDefault();

                Debug.WriteLine(query2);


                if (query1 != null && query2 != null)
                {

                    Debug.WriteLine(User.Identity.GetUserId());
                    Debug.WriteLine("jjjjjjjjjj");
                    Booking NewBooking = new Booking{
                        Price = query1.Price,
                        Payed = false,
                        ArrivalDate = model.DateArrival,
                        DepartureDate = model.DateArrival.AddDays(model.NoOfDaysStayed),
                        AadOns = "",
                        Room = query1.FoundRoom,
                        UserInfo = query2.FounUser
                    };
                    db.Bookings.Add(NewBooking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Debug.WriteLine("jjjj");
                    Debug.WriteLine("");
                }
                /*
                foreach (var group in query)
                {
                    Debug.WriteLine("RoomId: {0}", group.RoomId);
                    Debug.WriteLine("");
                }
                */


            }
            else
            {
                Debug.WriteLine("gggg");
                Debug.WriteLine("");
            }

            /*
            var data = Rooms.GroupJoin(
                Room => Room.BookingId,
                Booking => Booking.BookingId,
                (Room, RoomGroup) => new
                {
                    BookindId = Booking.
                }
                ).To

            foreach (var _booking in bookings)
            {
                if (_booking.ArrivalDate.Ticks < model.DateDeparture.Ticks &&
                    _booking.DepartureDate.Ticks > model.DateArrival.Ticks &&
                    _booking.DepartureDate.Ticks > model.DateDeparture.Ticks)
                {

                }
            }

            db.Bookings.Add(booking);
            db.SaveChanges();
        }

        ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description", booking.BookingId);
        return View(booking);
        */
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
