using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
                TempData["Message2"] = "Successfully Deleted Activity Category";
                TempData["MessageType2"] = "success";
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

                int id = MasterDAL.AddActivity(p_Model);

                
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/Content/assets/Images/Activity_mike/") + id.ToString() + ".png");
                }

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
            p_Model.FunPicture = p_Model.Id.ToString()+".png";
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

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/Content/assets/Images/Activity_mike/") + p_Model.Id.ToString() + ".png");
                }


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

        #region Users

        public ActionResult Users(string index,string str)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            List<UserModel> l_Users;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            if(str==null || str == "")
            {
                l_Users = UserDAL.GetUsersCollections(user.Home, user.UserType);
                List<UserModel> l_UsersInactive = UserDAL.GetUsersCollections(user.Home, user.UserType, 'I');
                ViewBag.InactiveUsers = l_UsersInactive;
            }
            else
            {
                l_Users = UserDAL.GetUsersCollections_Filter(user.Home, user.UserType, 'A', str);
                List<UserModel> l_UsersInactive = UserDAL.GetUsersCollections_Filter(user.Home, user.UserType, 'I', str);
                ViewBag.InactiveUsers = l_UsersInactive;
            }
            if(index==null || index == "")
            {
                TempData["start"] = "1";
            }
            else
            {
                TempData["start"] = index;
            }
            


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
            UserModel usersample = new UserModel();

            TempData["EDIT"] = "";
            TempData["sameuname"] = "";

            return View(usersample);
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
            if (p_Model.ID == 0)
            {
                p_Model.Password = Helpers.QolaCulture.Sha1Hash(p_Model.Password);
            }
            
            foreach (PropertyInfo prop in typeof(UserModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(p_Model) == null) { prop.SetValue(p_Model, ""); }
                }
            }
            //if (p_Model.Address == null) p_Model.Address = "";
            //if (p_Model.City == null) p_Model.City = "";
            //if (p_Model.PostalCode == null) p_Model.PostalCode = "";
            //if (p_Model.Province == null) p_Model.Province = "";
            //if (p_Model.Country == null) p_Model.Country = "";
            //if (p_Model.Email == null) p_Model.Email = "";
            //if (p_Model.WorkPhone == null) p_Model.WorkPhone = "";
            //if (p_Model.HomePhone == null) p_Model.HomePhone = "";
            //if (p_Model.Mobile == null) p_Model.Mobile = "";
            p_Model.ModifiedBy = user.ID;
            TempData["sameuname"] = "";
            if (p_Model.ID == 0)
            {
                if (UserDAL.AddNewUsers(p_Model) == -1)
                {
                    TempData["sameuname"] = "true";
                    TempData["EDIT"] = "";
                    return View("AddUser", p_Model);
                }
            }
            else if (p_Model.ID > 0)
            {
                if (UserDAL.UpdateUsers(p_Model)==false)
                {
                    TempData["sameuname"] = "true";
                    TempData["EDIT"] = "true";
                    return View("AddUser", p_Model);
                }
            }

            return RedirectToAction("Users");
        }

        public ActionResult EditUser(int userid)
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
            TempData["EDIT"] = "true";
            UserModel usersample = UserDAL.Get_User_By_Id(userid);
            return View("AddUser", usersample);
        }

        public int DeleteUser(int userid)
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
            return UserDAL.RemoveUsers(userid);

        }

        #endregion

        #region Buildings

        public ActionResult Buildings(string str)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            List<HomeModel> l_Homes;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            if (str == null || str == "")
            {
                l_Homes = HomeDAL.GetHomeCollections_mike();
            }
            else
            {
                l_Homes = HomeDAL.GetHomeCollections_mike_Filter(str);
            }



            return View(l_Homes);
        }

        public ActionResult AddBuildings()
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

            HomeModel homesample = new HomeModel();
            homesample.Province = new ProvinceModel();

            TempData["EDIT"] = "";

            return View(homesample);
        }

        [HttpPost]
        public ActionResult AddBuildings(HomeModel p_Model)
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

            foreach (PropertyInfo prop in typeof(HomeModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(p_Model) == null) { prop.SetValue(p_Model, ""); }
                }
            }



            if (p_Model.Id == 0)
            {
                p_Model.status_mike = "A";
                p_Model.ModifiedBy = user;
                int identity = HomeDAL.AddNewHome(p_Model);


                HttpPostedFileBase file = Request.Files[0];
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                file.SaveAs(Server.MapPath("~/Content/assets/Images/home_ico/") + identity.ToString() + ".png");
                p_Model.IconImage = "images/home_ico/" + identity.ToString() + ".PNG";
                p_Model.Id = identity;
                HomeDAL.UpdateHome(p_Model);

            }
            else if (p_Model.Id > 0)
            {
                if (Request.Files.Count >= 1)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength>0)
                    {
                        int fileSize = file.ContentLength;
                        string fileName = file.FileName;
                        string mimeType = file.ContentType;
                        System.IO.Stream fileContent = file.InputStream;
                        file.SaveAs(Server.MapPath("~/Content/assets/Images/home_ico/") + p_Model.Id.ToString() + ".png");
                    }            
                }
                p_Model.status_mike = "A";
                p_Model.ModifiedBy = user;
                if (HomeDAL.UpdateHome(p_Model) == false)
                {
                    TempData["EDIT"] = "true";
                    return View("AddBuildings", p_Model);
                }
            }

            return RedirectToAction("Buildings");
        }

        public ActionResult EditBuildings(int homeid)
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
            TempData["EDIT"] = "true";
            HomeModel usersample = HomeDAL.GetHomeById(homeid);
            return View("AddBuildings", usersample);
        }

        [HttpGet]
        public int DeleteBuildings(int homeid)
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
            if (HomeDAL.RemoveHome_InactiveforNow(homeid) == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        #endregion



        public ActionResult ChangePassword()
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
            //UserDAL.ResetPassword();
            return View();
        }

        public int ChangePassword_Save(string userid,string old,string new1,string new2)
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
            new1 = Helpers.QolaCulture.Sha1Hash(new1);
            old = Helpers.QolaCulture.Sha1Hash(old);
            int id = Int32.Parse(userid);
            int result = UserDAL.ResetPassword_mike(id, old,new1);

            return result;


            
        }

    }
}