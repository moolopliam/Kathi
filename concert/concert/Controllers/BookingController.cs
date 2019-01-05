using concert.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace concert.Controllers
{
    public class BookingController : Controller
    {
        private Concert5904Entities db = new Concert5904Entities();
        // GET: Booking

        public ActionResult Index1()
        {

            var data = db.Booking.ToList();
            return View(data);
        }


        public ActionResult Index()
        {

            var data = db.Booking.ToList();
            return View(data);
        }

        public ActionResult Create()
        {


            ViewBag.IDShow = new SelectList(db.Show, "IDShow", "NameShow");
            ViewBag.IDZone = new SelectList(db.Zone, "IDZone", "NameZone");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Booking booking)
        {

            if (ModelState.IsValid)
            {
                db.Booking.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IDCus = new SelectList(db.Customer, "IDCus", "NameCus");
            ViewBag.IDShow = new SelectList(db.Show, "IDShow", "NameShow");
            ViewBag.IDZone = new SelectList(db.Zone, "IDZone", "NameZone");
            return View(booking);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Where(x => x.IDBooking == id).FirstOrDefault();
            if (booking == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDCus = new SelectList(db.Customer, "IDCus", "NameCus");
            ViewBag.IDShow = new SelectList(db.Show, "IDShow", "NameShow");
            ViewBag.IDZone = new SelectList(db.Zone, "IDZone", "NameZone");

            return View(booking);
        }

        [HttpPost]
        public ActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IDCus = new SelectList(db.Customer, "IDCus", "NameCus");
            ViewBag.IDShow = new SelectList(db.Show, "IDShow", "NameShow");
            ViewBag.IDZone = new SelectList(db.Zone, "IDZone", "NameZone");

            return View(booking);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        public ActionResult Delete(int id)
        {
            var data = db.Booking.Find(id);
            db.Booking.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}