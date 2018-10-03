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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            List<ActivityCategoryModel> l_Model = MasterDAL.GetAllActivityCategory();
            return View(l_Model);
        }

        public ActionResult EditActivityCategory(int CategoryId)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            MasterDAL.AddActivityCategory(p_Model);
            return RedirectToAction("ActivityCategory");
        }

        public ActionResult AddActivityCategory()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View();
        }

        public ActionResult Activity()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.Message = TempData["Message"];
            List<ActivityModel> l_Model = MasterDAL.GetAllActivity();
            return View(l_Model);
        }

        [HttpPost]
        public ActionResult AddActivity(ActivityModel p_Model)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.Categories = MasterDAL.GetAllActivityCategory();
            return View();
        }

        public ActionResult Users()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<UserModel> l_Users = UserDAL.GetUsersCollections(user.Home, user.UserType);
            Collection<UserModel> l_UsersInactive = UserDAL.GetUsersCollections(user.Home, user.UserType, 'I');
            ViewBag.InactiveUsers = l_UsersInactive;
            return View(l_Users);
        }

        public ActionResult AddUser()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<HomeModel> l_Homes = HomeDAL.GetHomeCollections();
            ViewBag.Homes = l_Homes;
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserModel p_Model)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_Status = Convert.ToBoolean(Request.Form["Status"]);
            p_Model.Status = l_Status ? Constants.EnumerationTypes.AvailabilityStatus.A : Constants.EnumerationTypes.AvailabilityStatus.I;
            p_Model.Password = Helpers.QolaCulture.Sha1Hash(p_Model.Password);
            UserDAL.AddNewUsers(p_Model);
            return RedirectToAction("Users");
        }
    }
}