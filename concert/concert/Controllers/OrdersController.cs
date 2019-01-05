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
    public class OrdersController : Controller
    {
        private Concert5904Entities db = new Concert5904Entities();

        // GET: Orders
        public ActionResult Index()
        {
            var IDUSER = Convert.ToInt32(Session["IDUSER"]);
            var order = db.Order.Where(a => a.O_IDUSer == IDUSER).ToList();
            //var Status = order.Where(a => a.O_SatatusID == 2).FirstOrDefault();
            //var detail = db.Booking.Where(a => a.B_OrderID == Status.OrderID).ToList();
            //TempData["gg"] = detail;
            return View(order);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.O_SatatusID = new SelectList(db.StatusOrder, "StatusID", "StatusName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,O_IDUSer,O_SatatusID,O_TotalPrice,O_Date")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.O_SatatusID = new SelectList(db.StatusOrder, "StatusID", "StatusName", order.O_SatatusID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);

            var Price = db.Booking.Where(s => s.B_OrderID == order.OrderID).ToList();
            TempData["gg"] = Price;
            order.O_TotalPrice = Price.Sum(a => a.Zone.Price);
            if (order == null)
            {
                return HttpNotFound();
            }
            //ViewBag.O_SatatusID = new SelectList(db.StatusOrder, "StatusID", "StatusName", order.O_SatatusID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.pic != null)
                {
                    byte[] Temp = new byte[order.pic.ContentLength];
                    order.pic.InputStream.Read(Temp, 0, order.pic.ContentLength);
                    order.O_IMG = Temp;

                }
                order.O_SatatusID = 1;
                db.Entry(order).State = EntityState.Modified;

                var IDUSER = Convert.ToInt32(Session["IDUSER"]);
                var HK = db.User.Where(a => a.IDUser == IDUSER).FirstOrDefault();
                var date = DateTime.Now.ToString("dd/MM/yyyy");
                Order _Order = new Order()
                {
                    O_Date = date,
                    O_SatatusID = 2,
                    O_IDUSer = HK.IDUser,
                    O_TotalPrice = 0

                };
                db.Order.Add(_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.O_SatatusID = new SelectList(db.StatusOrder, "StatusID", "StatusName", order.O_SatatusID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
