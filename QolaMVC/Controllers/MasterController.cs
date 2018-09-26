using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ActionResult EditActivityCategory(int CategoryId)
        {
            try
            {
                var l_Activity = MasterDAL.GetActivityCategoryById(CategoryId);
                return View(l_Activity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult EditActivityCategory(ActivityCategoryModel p_Model)
        {
            try
            {
                MasterDAL.UpdateActivityCategory(p_Model);
                return RedirectToAction("ActivityCategory");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult DeleteActivityCategory(int CategoryId)
        {
            try
            {
                MasterDAL.DeleteActivityCategory(CategoryId);
                TempData["Message"] = "Successfully Deleted Activity Category";
                TempData["MessageType"] = "success";
                return RedirectToAction("ActivityCategory"); ;
            }
            catch (Exception ex)
            {
                throw;
            }
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
            ViewBag.Message = TempData["Message"];
            List<ActivityModel> l_Model = MasterDAL.GetAllActivity();
            return View(l_Model);
        }

        [HttpPost]
        public ActionResult AddActivity(ActivityModel p_Model)
        {
            try
            {
                var l_Category = new ActivityCategoryModel();
                l_Category.Id = Convert.ToInt32(Request.Form["Category"]);
                p_Model.Category = l_Category;

                MasterDAL.AddActivity(p_Model);
                return RedirectToAction("Activity");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult EditActivity(int ActivityId)
        {
            try
            {
                ViewBag.Categories = MasterDAL.GetAllActivityCategory();
                var l_Activity = MasterDAL.GetActivityById(ActivityId);
                return View(l_Activity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult EditActivity(ActivityModel p_Model)
        {
            try
            {
                int l_CategoryId = Convert.ToInt32(Request.Form["Category"]);
                ActivityCategoryModel l_category = new ActivityCategoryModel();
                l_category.Id = l_CategoryId;

                p_Model.Category = l_category;
                MasterDAL.UpdateActivity(p_Model);

                TempData["Message"] = "Successfully updated Activity";
                TempData["MessageType"] = "success";

                return RedirectToAction("Activity");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult DeleteActivity(int ActivityId)
        {
            try
            {

                MasterDAL.DeleteActivity(ActivityId);

                TempData["Message"] = "Successfully Deleted Activity";
                TempData["MessageType"] = "success";
                return RedirectToAction("Activity"); ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult AddActivity()
        {
            ViewBag.Categories = MasterDAL.GetAllActivityCategory();
            return View();
        }

        public ActionResult Users()
        {
            Collection<UserModel> l_Users = UserDAL.ge();
            ViewBag.Homes = l_Homes;
            return View();
        }

        public ActionResult AddUser()
        {
            Collection<HomeModel> l_Homes = HomeDAL.GetHomeCollections();
            ViewBag.Homes = l_Homes;
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserModel p_Model)
        {
            UserDAL.AddNewUsers(p_Model);
            return RedirectToAction("Users");
        }
    }
}