using concert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace concert.Controllers
{
    public class UsersController : Controller
    {
        private Concert5904Entities db = new Concert5904Entities();

        public ActionResult Index()
        {
            return View();
        }
      
     
    }
}