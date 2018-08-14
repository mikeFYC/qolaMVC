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
            return RedirectToAction("ActivityCategory");
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
            var l_Category = new ActivityCategoryModel();
            l_Category.Id = Convert.ToInt32(Request.Form["Category"]);
            p_Model.Category = l_Category;

            MasterDAL.AddActivity(p_Model);
            return RedirectToAction("Activity");
        }

        public ActionResult AddActivity()
        {
            ViewBag.Categories = MasterDAL.GetAllActivityCategory();
            return View();
        }
    }
}