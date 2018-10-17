using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAvailableSuites()
        {
            List<dynamic> l_Json = new List<dynamic>();
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }
    }
}