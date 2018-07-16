using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActivityCategory()
        {
            List<ActivityCategoryModel> l_Model = MasterDAL.GetAllActivityCategory();
            return View(l_Model);
        }

        [HttpPost]
        public ActionResult AddActivityCategory(ActivityCategoryModel p_Model)
        {
            MasterDAL.AddActivityCategory(p_Model);
            return View();
        }

        public ActionResult AddActivityCategory()
        {
            return View();
        }

        public ActionResult Activity()
        {
            List<ActivityModel> l_Model = MasterDAL.GetAllActivity();
            return View(l_Model);
        }

        [HttpPost]
        public ActionResult AddActivity(ActivityModel p_Model)
        {
            MasterDAL.AddActivity(p_Model);
            return View();
        }

        public ActionResult AddActivity()
        {
            return View();
        }
    }
}