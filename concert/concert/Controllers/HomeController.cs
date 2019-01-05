using concert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace concert.Controllers
{
    public class HomeController : Controller
    {
        private Concert5904Entities db = new Concert5904Entities();



        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            Session["User"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string inputUsername, string inputPassword)
        {
            var data = db.User.Where(p => p.UserName == inputUsername && p.Password == inputPassword).FirstOrDefault();

            if (data != null)
            {
                if (data.IDType == 1)
                {
                    Session["user"] = 1;
                    return RedirectToAction(nameof(GennerailIndex));
                }
                else
                {
                    Session["user"] = 2;
                    Session["IDUSER"] = data.IDUser;
                    AddOrder(data.UserName);
                    return RedirectToAction(nameof(GennerailIndex));
                }

            }

            return View();
        }
        public void AddOrder(string UserName)
        {
            int i = 0;
            int r = 0;

            var HK = db.User.Where(a => a.UserName == UserName).FirstOrDefault();
            var HKoRder = db.Order.Where(a => a.O_IDUSer == HK.IDUser).ToList();

            if (HK.IDType == 2)
            {
                if (HKoRder.Count == 0)
                {
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

                }

                foreach (var item in HKoRder)
                {
                    if (item.O_SatatusID == 1)
                    {
                        i++;
                    }
                    if (item.O_SatatusID == 2)
                    {
                        r++;
                    }
                }
                if (i != 0)
                {
                    if (r == 0)
                    {
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
                    }

                }
                else
                {
                    if (HKoRder.Count > 0)
                    {
                        if (r == 0)
                        {
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
                        }
                    }
                }
            }

        }
        public ActionResult GennerailIndex()//ของลูกค้า
        {
            return View();
        }

        public ActionResult GennerailIndex1()//ของแอดมิน
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction(nameof(Index));
        }

    }
}