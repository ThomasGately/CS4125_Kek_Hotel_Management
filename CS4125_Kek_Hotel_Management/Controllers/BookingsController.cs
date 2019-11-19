﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS4125_Kek_Hotel_Management.DAL;
using CS4125_Kek_Hotel_Management.Models;

namespace CS4125_Kek_Hotel_Management.Controllers
{
    public class BookingsController : Controller
    {
        private HotelContext db = new HotelContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.BookedCustomer).Include(b => b.BookedRoom);
            return View(bookings.ToList());
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
            ViewBag.BookedCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,Price,Payed,DatePayed,ArrivalSate,DepartureDate,ChechIn,ChechOut,BookedCustomerId,BookedRoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookedCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", booking.BookedCustomerId);
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
            ViewBag.BookedCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", booking.BookedCustomerId);
            ViewBag.BookingId = new SelectList(db.Rooms, "RoomId", "Description", booking.BookingId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,Price,Payed,DatePayed,ArrivalSate,DepartureDate,ChechIn,ChechOut,BookedCustomerId,BookedRoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookedCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", booking.BookedCustomerId);
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