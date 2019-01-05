using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using concert.Models;

namespace concert.Controllers
{
    public class BandsController : Controller
    {
        private Concert5904Entities db = new Concert5904Entities();

        // GET: Bands
        public ActionResult Index()
        {
            var band = db.Band.Include(b => b.Category);
            return View(band.ToList());
        }

        // GET: Bands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = db.Band.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        // GET: Bands/Create
        public ActionResult Create()
        {
            ViewBag.IDCategory = new SelectList(db.Category, "IDCategory", "NameCatgory");
            return View();
        }

       
        [HttpPost]
        public ActionResult Create([Bind(Include = "IDBand,NameBand,IDCategory")] Band band)
        {
            if (ModelState.IsValid)
            {
                db.Band.Add(band);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCategory = new SelectList(db.Category, "IDCategory", "NameCatgory", band.IDCategory);
            return View(band);
        }

        // GET: Bands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = db.Band.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCategory = new SelectList(db.Category, "IDCategory", "NameCatgory", band.IDCategory);
            return View(band);
        }

       
        [HttpPost]
         public ActionResult Edit([Bind(Include = "IDBand,NameBand,IDCategory")] Band band)
        {
            if (ModelState.IsValid)
            {
                db.Entry(band).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCategory = new SelectList(db.Category, "IDCategory", "NameCatgory", band.IDCategory);
            return View(band);
        }

        // GET: Bands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = db.Band.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Band band = db.Band.Find(id);
            db.Band.Remove(band);
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
