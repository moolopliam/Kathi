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
    public class ShowsController : Controller
    {

        private Concert5904Entities db = new Concert5904Entities();
        private readonly ConvertDate td = new ConvertDate();

        // GET: Shows
        public ActionResult Index1()
        {

            var data = db.Show.ToList();
            return View(data);
        }
        public ActionResult Index()
        {

            var data = db.Show.ToList();
            return View(data);
        }

        public ActionResult Create()
        {

            ViewBag.IDBand = new SelectList(db.Band, "IDBand", "NameBand");
            ViewBag.IDPlace = new SelectList(db.Place, "IDPlace", "NamePlace");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Show show)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    show.Time = Convert.ToString(show.ATime).Substring(10, 6);


                    var Tempdate = td.ThaiDate(show.Mydate);
                    show.Date = Tempdate;

                    if (show.pic != null)
                    {
                        byte[] Temp = new byte[show.pic.ContentLength];
                        show.pic.InputStream.Read(Temp, 0, show.pic.ContentLength);
                        show.Picture = Temp;

                    }


                    db.Show.Add(show);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            ViewBag.IDBand = new SelectList(db.Band, "IDBand", "NameBand");
            ViewBag.IDPlace = new SelectList(db.Place, "IDPlace", "NamePlace");



            return RedirectToAction(nameof(Index));
        }

        public ActionResult BagCad()
        {
            var IDUSER = Convert.ToInt32(Session["IDUSER"]);
            var order = db.Order.Where(a => a.O_IDUSer == IDUSER).ToList();
            var orderdetail = order.Where(x => x.O_SatatusID == 2).FirstOrDefault();
            var data = db.Booking.Where(a => a.B_OrderID == orderdetail.OrderID).ToList();
            TempData["total"] = data.Sum(s => s.Zone.Price);
            TempData["num"] = data.Sum(a => a.NumCard);
            return View(data);
        }
        public ActionResult ShowDetail(int id,int i=0)
        {

            var data = db.Show.Where(a => a.IDShow == id).FirstOrDefault();
            TempData["s"] = db.Zone.ToList();
            TempData["V"] = data.MatTicket;
            TempData["error"] = null;
            if (i != 0)
            {
                TempData["error"] = "error";
            }
            return View(data);
        }

        public ActionResult AddBooking(int id, int amout, int Zone)
        {
            var data = db.Show.Where(a => a.IDShow == id).FirstOrDefault();
            if (data != null)
            {
                var IDUSER = Convert.ToInt32(Session["IDUSER"]);
                var order = db.Order.Where(a => a.O_IDUSer == IDUSER).ToList();
                var orderdetail = order.Where(x => x.O_SatatusID == 2).FirstOrDefault();
                var MaX = db.Show.Where(a => a.IDShow == id).FirstOrDefault();
                var Value = db.Booking.Where(z => z.B_OrderID == orderdetail.OrderID).ToList();
                var CHK = Value.Where(a => a.IDShow == data.IDShow).ToList();
                var SumNum = CHK.Sum(a => a.NumCard);
                if ((SumNum+amout) <= Convert.ToInt32(MaX.MatTicket))
                {
                    if (orderdetail != null)
                    {
                        var date = DateTime.Now.ToString("dd/MM/yyyy");
                        Booking _booking = new Booking()
                        {
                            B_OrderID = orderdetail.OrderID,
                            IDShow = data.IDShow,
                            NumCard = amout,
                            DateBooking = date,
                            IDZone = Zone,
                            IDCus = orderdetail.O_IDUSer

                        };
                        db.Booking.Add(_booking);
                        db.SaveChanges();
                        return RedirectToAction(nameof(BagCad));
                    }
                }
                else
                {

                    return RedirectToAction("ShowDetail",new {id = data.IDShow,i =5});
                }


            }
            return RedirectToAction("ShowDetail", new { id = data.IDShow, i = 5 });
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Show.Where(x => x.IDShow == id).FirstOrDefault();

            if (show == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBand = new SelectList(db.Band, "IDBand", "NameBand");
            ViewBag.IDPlace = new SelectList(db.Place, "IDPlace", "NamePlace");


            return View(show);
        }

        [HttpPost]
        public ActionResult Edit(Show show)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Tempdate = td.ThaiDate(show.Mydate);
                    show.Date = Tempdate;

                    if (show.pic != null)
                    {
                        byte[] Temp = new byte[show.pic.ContentLength];
                        show.pic.InputStream.Read(Temp, 0, show.pic.ContentLength);
                        show.Picture = Temp;

                    }


                    db.Entry(show).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            ViewBag.IDBand = new SelectList(db.Band, "IDBand", "NameBand");
            ViewBag.IDPlace = new SelectList(db.Place, "IDPlace", "NamePlace");

            return RedirectToAction(nameof(Index));

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Show.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        public ActionResult Delete(int id)
        {
            var data = db.Show.Find(id);
            db.Show.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string txtName)
        {
            var data = db.Show.ToList();
            if (!String.IsNullOrEmpty(txtName))
            {
                data = db.Show.Where(p => p.Band.NameBand.Contains(txtName)).ToList();
            }
            return View(nameof(Index), data);
        }



    }
}