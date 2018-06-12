using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class HomeReportsController : Controller
    {
        // GET: HomeReports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Allergies()
        {
            return View();
        }

        public ActionResult SpecialDiets()
        {
            return View();
        }

        public ActionResult Likes()
        {
            return View();
        }

        public ActionResult Dislikes()
        {
            return View();
        }

    }
}