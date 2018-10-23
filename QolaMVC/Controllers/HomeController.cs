using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using QolaMVC.Helpers;
using static QolaMVC.Helpers.ProgressNotesHelper;
using System.Data;
using System.Collections;
using System.Text;

namespace QolaMVC.Controllers
{
    public class HomeController : Controller
    {
        private string _colorCode = "W";
        private string _mobiltySelectedValue = string.Empty;
        private string _previousValue = string.Empty;

        public ActionResult Index()
        {
            //var context = Request.GetOwinContext();
            //var authManager = context.Authentication;

            //var identity = (ClaimsIdentity)authManager.User.Identity;
            //IEnumerable<Claim> claims = identity.Claims;

            //var l_Name = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
            //       .Select(c => c.Value).SingleOrDefault();
            //var l_UserId = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            //                   .Select(c => c.Value).SingleOrDefault();

            var user = (UserModel)TempData["User"];

            if (user != null)
            {
                Collection<HomeModel> l_Homes = HomeDAL.GetHomeFill(user.ID, 1);// Convert.ToInt32(l_UserId));
                UserModel l_User = UserDAL.GetUserById(user.ID);// Convert.ToInt32(l_UserId));
                TempData["User"] = l_User;

                ViewBag.User = l_User;
                TempData.Keep("User");
                return View(l_Homes.Where(m => m.ProvinceName == "Alberta"));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public ActionResult Menu(int p_HomeId)
        {
            var user = (UserModel)TempData["User"];
            TempData["Home"] = HomeDAL.GetHomeById(p_HomeId);
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            HomeModel l_Home = HomeDAL.GetHomeById(p_HomeId);
            TempData["occupy"] = HomeDAL.GetOccupybyID(p_HomeId);
            //dynamic l_Json=to_do_list_function.get_to_do_list_number( user.ID, l_Home.Id);
            //TempData["DU"] = l_Json.DU;
            //TempData["HO"] = l_Json.HO;
            //TempData["IDA"] = l_Json.IDA;
            //TempData["IAA"] = l_Json.IAA;
            //TempData["IFRA"] = l_Json.IFRA;
            //TempData["IRCA"] = l_Json.IRCA;
            //TempData["RDA"] = l_Json.RDA;
            //TempData["RAA"] = l_Json.RAA;
            //TempData["RFRA"] = l_Json.RFRA;
            //TempData["RRCA"] = l_Json.RRCA;
            //TempData["PN"] = l_Json.PN;
            //TempData["AN"] = l_Json.AN;
            //TempData["RB"] = l_Json.RB;
            //TempData["RI"] = l_Json.RI;
            //TempData["RP"] = l_Json.RP;
            //TempData["NR"] = l_Json.NR;
            //TempData["SAE"] = l_Json.SAE;

            return View(l_Home);
        }

        [HttpGet]
        public ActionResult Number()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            
            dynamic l_Json = to_do_list_function.get_to_do_list_number(user.ID, home.Id);

            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Number_nextmonth()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            dynamic l_Json = to_do_list_function.get_to_do_list_number_nextmonth(user.ID, home.Id);

            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ResidentMenu(int p_ResidentId)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = ResidentsDAL.GetResidentById(p_ResidentId);
            var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");

            ViewBag.Message = TempData["Message"];

            TempData["Resident"] = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            ViewBag.ProgressNotes = progressNotes;
            ProgressNotesHelper.RegisterSession(resident);
            TempData["NOTE"] = "NO";

            return View(resident);
        }

        public ActionResult ResidentMenu2(int p_ResidentId)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = ResidentsDAL.GetResidentById(p_ResidentId);
            var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");

            ViewBag.Message = TempData["Message"];

            TempData["Resident"] = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            ViewBag.ProgressNotes = progressNotes;
            ProgressNotesHelper.RegisterSession(resident);
            TempData["NOTE"] = "YES";

            return View("ResidentMenu", resident);

        }



        public ActionResult ManageResidents(int p_HomeId)
        {
            var l_Residents = ResidentsDAL.GetResidentCollections(p_HomeId);
            return View(l_Residents);
        }

        public ActionResult AddNewResident()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;

            return View();
        }

        [HttpPost]
        public ActionResult CreateNewResident(ResidentModel p_Model)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;

            p_Model.ModifiedBy = user;
            p_Model.ModifiedOn = DateTime.Now;
            p_Model.Home = home;
            int[] RR = new int[2];
            RR=ResidentsDAL.AddNewResidentGeneralInfo(p_Model);
            ResidentsDAL.update_checklist(user.ID,RR[0]);
            return RedirectToAction("AddNewResident");
        }

        public ActionResult ActivityCalendar()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            return View();
        }




        #region ajax requests
        [HttpPost]
        public JsonResult getEvents(FormCollection form)
        {
            ActivityEventModel l_Model = new ActivityEventModel();
            l_Model.ActivityId = Convert.ToInt32(Request.Form["activity"]);
            l_Model.ProgramName = Convert.ToString(Request.Form["title"]);
            var frequency = Convert.ToInt32(Request.Form["frequency"]);
            List<DateTime> l_Dates = new List<DateTime>();

            if (frequency == 1) //date & time
            {
                var startDate = Convert.ToDateTime(Request.Form["startDate"]);
                var time = Convert.ToString(Request.Form["time"]);

                l_Model.ProgramStartDate = startDate;
                l_Model.ProgramStartTime = startDate.ToLongTimeString();
                l_Model.ProgramEndDate = startDate;
                l_Model.ProgramEndTime = startDate.ToLongTimeString();

                HomeDAL.AddNewActivityEvent(l_Model);
            }
            else if(frequency == 2) //every week day
            {
                var time = Convert.ToString(Request.Form["time"]);
                var weekDay = Convert.ToString(Request.Form["weekDay"]);
                l_Dates = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                .Where(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d).ToString("dddd").Equals(weekDay))
               .Select(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d)).ToList();

                foreach (var l_D in l_Dates)
                {
                    l_Model.ProgramStartDate = l_D;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = l_D;
                    l_Model.ProgramEndTime = l_D.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent(l_Model);
                }
            }
            else if(frequency == 3) //date between
            {
                var dateFrom = Convert.ToDateTime(Request.Form["dateFrom"]);
                var dateTo = Convert.ToDateTime(Request.Form["dateTo"]);
                var time = Convert.ToString(Request.Form["time"]);

                for (var dt = dateFrom; dt <= dateTo; dt = dt.AddDays(1))
                {
                    l_Model.ProgramStartDate = dt;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = dt;
                    l_Model.ProgramEndTime = dt.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent(l_Model);
                }
            }
            

            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            //foreach (var l_Data in l_ActivityEvents)
            //{
            //    var columns = new Dictionary<string, string>
            //    {
            //        { "id", l_Data.ProgramId.ToString()},
            //        { "title", l_Data.ProgramName},
            //        { "startDate", l_Data.ProgramStartDate.ToShortDateString()},
            //        { "endDate", l_Data.ProgramEndDate.ToShortDateString()},
            //        { "startTime", l_Data.ProgramStartTime},
            //        { "endTime", l_Data.ProgramEndTime}
            //    };

            //    l_Events.Add(columns);
            //}

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(5);

            for( var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    try
                    {
                        var columns = new Dictionary<string, string>
                        {
                            { "id", l_Data.ProgramId.ToString()},
                            { "title", l_Data.ProgramName},
                            { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                            { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                            { "startTime", l_Data.ProgramStartTime},
                            { "endTime", l_Data.ProgramEndTime}
                        };

                        l_Events.Add(columns);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents()
        {
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(2);

            for (var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "id", l_Data.ProgramId.ToString()},
                        { "title", l_Data.ProgramName},
                        { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                        { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                        { "startTime", l_Data.ProgramStartTime},
                        { "endTime", l_Data.ProgramEndTime}
                    };

                    l_Events.Add(columns);
                }
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCategoriesForCalendar()
        {
            List<ActivityModel> l_Model = MasterDAL.GetAllActivity();
            List<Dictionary<string, string>> l_ActivityCategories = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Model)
            {
                var columns = new Dictionary<string, string>
                {
                    { "Id", l_Data.Id.ToString() },
                    {"Name", l_Data.EnglishName}
                };

                l_ActivityCategories.Add(columns);
            }

            return Json(l_ActivityCategories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBirthdayCalendar()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            
            ViewBag.Message = TempData["Message"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_ActivityEvents = new QolaMVC.WebAPI.BirthdayCalendarController().Get(home.Id);//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", l_Data.ProgramStartDate.ToShortDateString()},
                    { "endDate", l_Data.ProgramEndDate.ToShortDateString()},
                    { "startTime", l_Data.ProgramStartTime},
                    { "endTime", l_Data.ProgramEndTime}
                };

                l_Events.Add(columns);
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuggestedActivityCalendar()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetSuggestedActivityEvents(resident.ID);
            
            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", l_Data.ProgramStartDate.ToShortDateString()},
                    { "endDate", l_Data.ProgramEndDate.ToShortDateString()},
                    { "startTime", l_Data.ProgramStartTime},
                    { "endTime", l_Data.ProgramEndTime}
                };

                l_Events.Add(columns);
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        #region calendar 2
        [HttpPost]
        public JsonResult getEvents_C2(FormCollection form)
        {
            ActivityEventModel l_Model = new ActivityEventModel();
            l_Model.ActivityId = Convert.ToInt32(Request.Form["activity"]);
            l_Model.ProgramName = Convert.ToString(Request.Form["title"]);
            var frequency = Convert.ToInt32(Request.Form["frequency"]);
            List<DateTime> l_Dates = new List<DateTime>();

            if (frequency == 1) //date & time
            {
                var startDate = Convert.ToDateTime(Request.Form["startDate"]);
                var time = Convert.ToString(Request.Form["time"]);

                l_Model.ProgramStartDate = startDate;
                l_Model.ProgramStartTime = startDate.ToLongTimeString();
                l_Model.ProgramEndDate = startDate;
                l_Model.ProgramEndTime = startDate.ToLongTimeString();

                HomeDAL.AddNewActivityEvent_C2(l_Model);
            }
            else if (frequency == 2) //every week day
            {
                var time = Convert.ToString(Request.Form["time"]);
                var weekDay = Convert.ToString(Request.Form["weekDay"]);
                l_Dates = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                .Where(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d).ToString("dddd").Equals(weekDay))
               .Select(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d)).ToList();

                foreach (var l_D in l_Dates)
                {
                    l_Model.ProgramStartDate = l_D;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = l_D;
                    l_Model.ProgramEndTime = l_D.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent_C2(l_Model);
                }
            }
            else if (frequency == 3) //date between
            {
                var dateFrom = Convert.ToDateTime(Request.Form["dateFrom"]);
                var dateTo = Convert.ToDateTime(Request.Form["dateTo"]);
                var time = Convert.ToString(Request.Form["time"]);

                for (var dt = dateFrom; dt <= dateTo; dt = dt.AddDays(1))
                {
                    l_Model.ProgramStartDate = dt;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = dt;
                    l_Model.ProgramEndTime = dt.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent_C2(l_Model);
                }
            }


            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C2();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(5);

            for (var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    try
                    {
                        var columns = new Dictionary<string, string>
                        {
                            { "id", l_Data.ProgramId.ToString()},
                            { "title", l_Data.ProgramName},
                            { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                            { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                            { "startTime", l_Data.ProgramStartTime},
                            { "endTime", l_Data.ProgramEndTime}
                        };

                        l_Events.Add(columns);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C2()
        {
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C2();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(2);

            for (var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "id", l_Data.ProgramId.ToString()},
                        { "title", l_Data.ProgramName},
                        { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                        { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                        { "startTime", l_Data.ProgramStartTime},
                        { "endTime", l_Data.ProgramEndTime}
                    };

                    l_Events.Add(columns);
                }
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region calendar 3
        [HttpPost]
        public JsonResult getEvents_C3(FormCollection form)
        {
            ActivityEventModel l_Model = new ActivityEventModel();
            l_Model.ActivityId = Convert.ToInt32(Request.Form["activity"]);
            l_Model.ProgramName = Convert.ToString(Request.Form["title"]);
            var frequency = Convert.ToInt32(Request.Form["frequency"]);
            List<DateTime> l_Dates = new List<DateTime>();

            if (frequency == 1) //date & time
            {
                var startDate = Convert.ToDateTime(Request.Form["startDate"]);
                var time = Convert.ToString(Request.Form["time"]);

                l_Model.ProgramStartDate = startDate;
                l_Model.ProgramStartTime = startDate.ToLongTimeString();
                l_Model.ProgramEndDate = startDate;
                l_Model.ProgramEndTime = startDate.ToLongTimeString();

                HomeDAL.AddNewActivityEvent_C3(l_Model);
            }
            else if (frequency == 2) //every week day
            {
                var time = Convert.ToString(Request.Form["time"]);
                var weekDay = Convert.ToString(Request.Form["weekDay"]);
                l_Dates = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                .Where(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d).ToString("dddd").Equals(weekDay))
               .Select(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d)).ToList();

                foreach (var l_D in l_Dates)
                {
                    l_Model.ProgramStartDate = l_D;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = l_D;
                    l_Model.ProgramEndTime = l_D.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent_C3(l_Model);
                }
            }
            else if (frequency == 3) //date between
            {
                var dateFrom = Convert.ToDateTime(Request.Form["dateFrom"]);
                var dateTo = Convert.ToDateTime(Request.Form["dateTo"]);
                var time = Convert.ToString(Request.Form["time"]);

                for (var dt = dateFrom; dt <= dateTo; dt = dt.AddDays(1))
                {
                    l_Model.ProgramStartDate = dt;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = dt;
                    l_Model.ProgramEndTime = dt.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent_C3(l_Model);
                }
            }


            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C3();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(5);

            for (var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    try
                    {
                        var columns = new Dictionary<string, string>
                        {
                            { "id", l_Data.ProgramId.ToString()},
                            { "title", l_Data.ProgramName},
                            { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                            { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                            { "startTime", l_Data.ProgramStartTime},
                            { "endTime", l_Data.ProgramEndTime}
                        };

                        l_Events.Add(columns);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C3()
        {
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C3();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(2);

            for (var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "id", l_Data.ProgramId.ToString()},
                        { "title", l_Data.ProgramName},
                        { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                        { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                        { "startTime", l_Data.ProgramStartTime},
                        { "endTime", l_Data.ProgramEndTime}
                    };

                    l_Events.Add(columns);
                }
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region calendar 4
        [HttpPost]
        public JsonResult getEvents_C4(FormCollection form)
        {
            ActivityEventModel l_Model = new ActivityEventModel();
            l_Model.ActivityId = Convert.ToInt32(Request.Form["activity"]);
            l_Model.ProgramName = Convert.ToString(Request.Form["title"]);
            var frequency = Convert.ToInt32(Request.Form["frequency"]);
            List<DateTime> l_Dates = new List<DateTime>();

            if (frequency == 1) //date & time
            {
                var startDate = Convert.ToDateTime(Request.Form["startDate"]);
                var time = Convert.ToString(Request.Form["time"]);

                l_Model.ProgramStartDate = startDate;
                l_Model.ProgramStartTime = startDate.ToLongTimeString();
                l_Model.ProgramEndDate = startDate;
                l_Model.ProgramEndTime = startDate.ToLongTimeString();

                HomeDAL.AddNewActivityEvent_C4(l_Model);
            }
            else if (frequency == 2) //every week day
            {
                var time = Convert.ToString(Request.Form["time"]);
                var weekDay = Convert.ToString(Request.Form["weekDay"]);
                l_Dates = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                .Where(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d).ToString("dddd").Equals(weekDay))
               .Select(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d)).ToList();

                foreach (var l_D in l_Dates)
                {
                    l_Model.ProgramStartDate = l_D;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = l_D;
                    l_Model.ProgramEndTime = l_D.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent_C4(l_Model);
                }
            }
            else if (frequency == 3) //date between
            {
                var dateFrom = Convert.ToDateTime(Request.Form["dateFrom"]);
                var dateTo = Convert.ToDateTime(Request.Form["dateTo"]);
                var time = Convert.ToString(Request.Form["time"]);

                for (var dt = dateFrom; dt <= dateTo; dt = dt.AddDays(1))
                {
                    l_Model.ProgramStartDate = dt;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = dt;
                    l_Model.ProgramEndTime = dt.ToLongTimeString();

                    HomeDAL.AddNewActivityEvent_C4(l_Model);
                }
            }


            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C4();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(5);

            for (var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    try
                    {
                        var columns = new Dictionary<string, string>
                        {
                            { "id", l_Data.ProgramId.ToString()},
                            { "title", l_Data.ProgramName},
                            { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                            { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                            { "startTime", l_Data.ProgramStartTime},
                            { "endTime", l_Data.ProgramEndTime}
                        };

                        l_Events.Add(columns);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C4()
        {
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C4();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var dtF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = dtF.AddMonths(2);

            for (var d = dtF; d <= end; d = d.AddMonths(1))
            {
                foreach (var l_Data in l_ActivityEvents)
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "id", l_Data.ProgramId.ToString()},
                        { "title", l_Data.ProgramName},
                        { "startDate", new DateTime(d.Year, d.Month, l_Data.ProgramStartDate.Day).ToShortDateString()},
                        { "endDate", new DateTime(d.Year, d.Month, l_Data.ProgramEndDate.Day).ToShortDateString()},
                        { "startTime", l_Data.ProgramStartTime},
                        { "endTime", l_Data.ProgramEndTime}
                    };

                    l_Events.Add(columns);
                }
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        [HttpPost]
        public void btnPdf_Click()
        {
            Collection<ProgressNotesModel> progressNotes;
            int residentId = 0;
            string[] arrSuiteNo;
            string exception = string.Empty;
            int iSno = 1;
            int selectedUserType = 0; //Convert.ToInt32(hdnUserType.Value);
            int selectedCategory = 0;// Convert.ToInt32(hdnCategory.Value);
            try
            {
                //arrSuiteNo = Request.Form["lblResident"].Split(',');
                ResidentModel l_Resident = (ResidentModel)TempData["Resident"];
                //s_FromDate = txtFromDate.Text;
                //s_ToDate = txtToDate.Text;
                Document doc = new Document(PageSize.A4, 30f, 30f, 77f, 20f);
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, mStream);

                writer.PageEvent = new PDFFooter();
                doc.Open();
                iTextSharp.text.Font tableFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font fontBoldText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font fontBoldHeadText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font fontCellHeader = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font font6B = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 7f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                PdfPTable tblProgress = new PdfPTable(7);
                tblProgress.WidthPercentage = 100f;

                float[] wthProgress = new float[] { 0.51f, 1.2f, 0.8f, 1f, 3f, 1.5f, 1.2f };
                tblProgress.SetWidths(wthProgress);
                tblProgress.SpacingAfter = 5f;

                PdfPTable tblNote = new PdfPTable(1);
                tblNote.WidthPercentage = 100f;
                float[] wthNote = new float[] { 1f };
                tblNote.SetWidths(wthNote);
                try
                {
                    progressNotes = new Collection<ProgressNotesModel>();
                    if (l_Resident != null)
                    {
                        DateTime fromDate = QolaCulture.stringToDateFormat(Request.Form["From"].ToString());
                        DateTime toDate = QolaCulture.stringToDateFormat(Request.Form["To"].ToString());
                        residentId = l_Resident.ID;
                        progressNotes = ProgressNotesDAL.GetProgressNotesCollections(residentId, fromDate, toDate, "R", selectedUserType, selectedCategory);
                    }
                    if (progressNotes != null)
                    {
                        foreach (ProgressNotesModel progressNote in progressNotes)
                        {
                            PdfPCell snoCell = new PdfPCell(new Phrase(iSno.ToString(), tableFont));
                            snoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblProgress.AddCell(snoCell);
                            PdfPCell dateCell = new PdfPCell(new Phrase(QolaCulture.dateToUSDateStringFormat(progressNote.Date), tableFont));
                            dateCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblProgress.AddCell(dateCell);
                            string times = QolaCulture.dateToUSTimeStringFormat(progressNote.Date, "HH:mm");

                            PdfPCell timeCell = new PdfPCell(new Phrase(times, tableFont));
                            timeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblProgress.AddCell(timeCell);

                            var parsedHtmlElements = HTMLWorker.ParseToList(new System.IO.StringReader("<span style='font-size: 9pt;'>" + progressNote.Title + "</span><br/>" + "<span style='font-size: 9pt;'>" + progressNote.Note + "</span>"), null);
                            if (progressNote.Category == 6)
                            {
                                PdfPCell fallDateCell = new PdfPCell(new Phrase("Date" + ": " + QolaCulture.dateToUSDateStringFormat(progressNote.FallDate) + "\n", tableFont));
                                fallDateCell.Border = 0;
                                tblNote.AddCell(fallDateCell);
                                PdfPCell timeFallCell = new PdfPCell(new Phrase("Time" + ": " + QolaCulture.dateToUSDateStringFormat(progressNote.FallDate, "hh:mm tt") + "\n", tableFont));
                                timeFallCell.Border = 0;
                                tblNote.AddCell(timeFallCell);
                                PdfPCell locationCell = new PdfPCell(new Phrase("ProgLocation" + ": " + progressNote.Location + "\n", tableFont));
                                locationCell.Border = 0;
                                tblNote.AddCell(locationCell);
                                if (progressNote.WitnessFall != "")
                                {
                                    PdfPCell witnessCell = new PdfPCell(new Phrase("Witnessed Fall: " + progressNote.WitnessFall + "\n\n", tableFont));
                                    witnessCell.Border = 0;
                                    tblNote.AddCell(witnessCell);
                                }
                                if (progressNote.UnWitnessType != 'Y')
                                {
                                    PdfPCell witnessCell = new PdfPCell(new Phrase("Unwitnessed " + "\n\n", tableFont));
                                    witnessCell.Border = 0;
                                    tblNote.AddCell(witnessCell);
                                }
                            }
                            foreach (var htmlElement in parsedHtmlElements)
                            {
                                if (htmlElement.Chunks.Count > 0)
                                {
                                    PdfPCell Note = new PdfPCell(new Phrase((htmlElement as Phrase)));
                                    Note.Border = 0;
                                    tblNote.AddCell(Note);
                                }
                            }
                            PdfPCell categoryCell = new PdfPCell(new Phrase(ProgressNotesHelper.GetCategory(progressNote.Category), tableFont));
                            categoryCell.PaddingBottom = 5f;
                            categoryCell.PaddingLeft = 1f;
                            tblProgress.AddCell(categoryCell);

                            PdfPCell titleCell = new PdfPCell(new PdfPTable(tblNote));
                            titleCell.PaddingBottom = 5f;
                            titleCell.PaddingLeft = 1f;
                            tblProgress.AddCell(titleCell);
                            tblNote.Rows.Clear();


                            if (progressNote.ACkFlag == 'N')
                            {
                                Phrase userType = new Phrase(progressNote.ModifiedBy.FirstName + " " + progressNote.ModifiedBy.LastName + "\n(", tableFont);
                                userType.Add(new Chunk(progressNote.ModifiedBy.UserTypeName, font6B));
                                userType.Add(new Chunk(")", font6B));
                                PdfPCell userCell = new PdfPCell(userType);
                                tblProgress.AddCell(userCell);
                            }

                            else if (progressNote.ACkFlag == 'A')
                            {
                                Phrase ackUserType = new Phrase();
                                ackUserType.Add(new Chunk(progressNote.AcknowledgedBy.FirstName + " " + progressNote.AcknowledgedBy.LastName, tableFont));
                                ackUserType.Add(new Chunk("\n(" + progressNote.AcknowledgedBy.UserTypeName, font6B));
                                ackUserType.Add(new Chunk(")", font6B));

                                PdfPCell AckUserCell = new PdfPCell(ackUserType);
                                tblProgress.AddCell(AckUserCell);
                            }
                            else
                            {
                                tblProgress.AddCell(new PdfPCell());
                            }
                            PdfPCell signatureCell = new PdfPCell(new Phrase("", tableFont));
                            tblProgress.AddCell(signatureCell);

                            iSno += 1;
                        }
                    }
                    doc.Add(tblProgress);
                    string reportName = string.Empty;
                    if (Session["ResidentStatus"] != null && Session["ResidentStatus"].ToString() == "A")
                    {
                        reportName = l_Resident.FirstName + "_" + l_Resident.LastName + "_" + DateTime.Now.ToString("yyyyMMdd");
                    }
                    else
                    {
                        reportName = "ProgressNotes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    }
                    doc.Close();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + reportName + ".pdf");
                    Response.Clear();
                    Response.BinaryWrite(mStream.ToArray());
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                catch (System.Threading.ThreadAbortException lException)
                {
                    throw (lException);
                }
                catch (Exception Ex)
                {
                    exception = "frmProgressNotes btnPdf_Click" + Ex.Message.ToString();
                    //Log.Write(exception);

                }
            }
            catch (DocumentException dex)
            {
                throw (dex);
            }
            catch (IOException ioex)
            {
                throw (ioex);
            }
        }

        public ActionResult EmergencyList()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            
            TempData.Keep("User");
            TempData.Keep("Home");
            
            ViewBag.User = user;
            ViewBag.Home = home;
            
            var ds = new DataSet();
            ds = ResidentsDAL.GetEmergencyResidentDetails(home.Id, "0");
            return View(ds);
        }

        public void btnPdf_Click_EmergencyList()
        {
            string sException = string.Empty;
            int iHomeId = 0;
            DataSet ds = null;
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            //Int32.TryParse(Session["HomeId"].ToString(), out iHomeId
            try
            {
                if (home != null)
                {
                    iHomeId = home.Id;
                    ds = new DataSet();
                    ds = ResidentsDAL.GetEmergencyResidentDetails(iHomeId, "0");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Document doc = new Document(PageSize.A4, 30f, 30f, 40f, 20f);
                        doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(doc, mStream);


                        writer.PageEvent = new pdfHeaderFooter();

                        doc.Open();

                        iTextSharp.text.Font fontCellSize12 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize12B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                        Paragraph paragraph = new Paragraph("EmergencyResidentDetails", fontCellSize11B);
                        paragraph.Alignment = Element.TITLE;



                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string sReason = string.Empty;
                            string sReasonValue = string.Empty;

                            PdfPTable PdfTable1;

                            PdfTable1 = new PdfPTable(10);
                            float[] wthtbl1 = new float[] { .8f, 3f, 10f, 5f, 8f, 10f, 8f, .8f, 7.9f, 10f };
                            PdfTable1.SetWidths(wthtbl1);
                            PdfTable1.WidthPercentage = 100f;
                            PdfTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            PdfTable1.SpacingAfter = 5f;
                            PdfTable1.SpacingBefore = 5f;

                            PdfPCell cell = new PdfPCell(new Phrase("EmergencyResidentDetails", fontCellSize11B));
                            cell.Border = 0;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Colspan = 10;
                            cell.PaddingBottom = 10f;
                            PdfTable1.AddCell(cell);

                            PdfPCell PdfTable1HeaderSuitNo = new PdfPCell(new Phrase("SuiteNo", fontCellSize9B));
                            PdfTable1HeaderSuitNo.Colspan = 2;
                            PdfTable1HeaderSuitNo.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderSuitNo);

                            PdfPCell PdfTable1HeaderName = new PdfPCell(new Phrase("Name", fontCellSize9B));
                            PdfTable1HeaderName.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderName);

                            PdfPCell PdfTable1HeaderGender = new PdfPCell(new Phrase("Gender", fontCellSize9B));
                            PdfTable1HeaderGender.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderGender);

                            PdfPCell PdfTable1HeaderPhone = new PdfPCell(new Phrase("Phone", fontCellSize9B));
                            PdfTable1HeaderPhone.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderPhone);

                            PdfPCell PdfTable1HeaderContactPerson = new PdfPCell(new Phrase("ContactPerson", fontCellSize9B));
                            PdfTable1HeaderContactPerson.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderContactPerson);

                            PdfPCell PdfTable1HeaderEmergencyContact = new PdfPCell(new Phrase("EmergencyContact", fontCellSize9B));
                            PdfTable1HeaderEmergencyContact.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderEmergencyContact);

                            PdfPCell PdfTable1HeaderMobilty = new PdfPCell(new Phrase("Mobility", fontCellSize9B));
                            PdfTable1HeaderMobilty.Colspan = 2;
                            PdfTable1HeaderMobilty.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderMobilty);

                            PdfPCell PdfTable1HeaderComment = new PdfPCell(new Phrase("Comments", fontCellSize9B));
                            PdfTable1HeaderComment.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderComment);

                            if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))
                            {

                                int ActualOrder = 1; // ddlResidentCareAssessment.SelectedValue == "H" ? 1 : ddlResidentCareAssessment.SelectedValue == "M" ? 2 : ddlResidentCareAssessment.SelectedValue == "L" ? 3 : ddlResidentCareAssessment.SelectedValue == "N" ? 4 : 0;
                                for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                                {

                                    sReasonValue = string.Empty;
                                    sReason = string.Empty;
                                    sReason = sReason + ds.Tables[0].Rows[iRow]["fd_SA_vision"] + ds.Tables[0].Rows[iRow]["fd_mobility"] + ds.Tables[0].Rows[iRow]["fd_SA_hearing"] + ds.Tables[0].Rows[iRow]["fd_cognitive_function"] + ds.Tables[0].Rows[iRow]["fd_risk_level"].ToString() + ",";
                                    if (sReason != string.Empty && sReason != null)
                                    {
                                        sReason = sReason.Remove(sReason.Length - 1);
                                        string sResult;
                                        string[] sValue = sReason.Split(',');
                                        foreach (string word in sValue)
                                        {
                                            if (word == "0MA" || word == "0MD" || word == "0MI" || word == "0MS")
                                            {
                                                GetMobilityValue(word);
                                            }
                                            sResult = Get_bindResidentCareAssessment(word, "");
                                            if (sResult != string.Empty && sResult != null)
                                            {
                                                if (sReasonValue.Length > 0)
                                                {
                                                    sReasonValue = sReasonValue + ',' + ' ' + Get_bindResidentCareAssessment(word, "W");
                                                }
                                                else
                                                {
                                                    sReasonValue = Get_bindResidentCareAssessment(word, "W");
                                                }
                                            }

                                            sResult = string.Empty;
                                        }
                                    }
                                    int order = Convert.ToInt32(ds.Tables[0].Rows[iRow]["fd_risk_order"]);
                                    int AcceptedOrder = 0;
                                    if (ActualOrder > 0)
                                    {
                                        AcceptedOrder = _colorCode == "R" || order == 1 ? 1 : _colorCode == "Y" || order == 2 ? 2 : _colorCode == "G" ? 3 : 4;
                                        if (AcceptedOrder != ActualOrder)
                                        {
                                            _colorCode = "W";
                                            _mobiltySelectedValue = string.Empty;
                                            continue;
                                        }
                                    }
                                    PdfPCell suiteColorCell = new PdfPCell();
                                    string level = string.Empty;
                                    switch (order)
                                    {
                                        case 1:
                                            level = "HighFallRisk";
                                            break;
                                        case 2:
                                            level = "MediumFallingRisk";
                                            break;
                                        case 3:
                                            level = "LowFallingRisk";
                                            break;
                                        default:
                                            level = string.Empty;
                                            break;
                                    }
                                    if (_colorCode == "R" || order == 1)
                                    {
                                        suiteColorCell.BackgroundColor = BaseColor.RED;
                                    }
                                    else if (_colorCode == "Y" || order == 2)
                                    {
                                        suiteColorCell.BackgroundColor = BaseColor.YELLOW;
                                    }
                                    else if (_colorCode == "G")
                                    {
                                        suiteColorCell.BackgroundColor = BaseColor.GREEN;
                                    }
                                    PdfTable1.AddCell(suiteColorCell);
                                    _colorCode = "W";
                                    PdfPCell PdfTable1SuitNoValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_suite_no"].ToString(), fontCellSize9));
                                    PdfTable1SuitNoValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1SuitNoValue);

                                    PdfPCell PdfTable1NameValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_first_name"] + " " + ds.Tables[0].Rows[iRow]["fd_last_name"], fontCellSize9));
                                    PdfTable1NameValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1NameValue);

                                    PdfPCell PdfTable1GenderValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_gender"].ToString(), fontCellSize9));
                                    PdfTable1GenderValue.HorizontalAlignment = Element.ALIGN_CENTER;
                                    PdfTable1.AddCell(PdfTable1GenderValue);

                                    string phone = ds.Tables[0].Rows[iRow]["fd_phone"].ToString().Trim().Length > 0 ? ds.Tables[0].Rows[iRow]["fd_phone"].ToString() : "NoPhone";
                                    PdfPCell PdfTable1AttendeesValue = new PdfPCell(new Phrase(phone, fontCellSize9));
                                    PdfTable1AttendeesValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1AttendeesValue);

                                    PdfPCell PdfTable1PhoneValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_contact_1"].ToString(), fontCellSize9));
                                    PdfTable1PhoneValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1PhoneValue);

                                    PdfPCell PdfTable1EmergencyContact = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_home_phone_1"].ToString(), fontCellSize9));
                                    PdfTable1EmergencyContact.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1EmergencyContact);

                                    PdfTable1.AddCell(suiteColorCell);



                                    PdfPCell PdfTable1Mobility = new PdfPCell(new Phrase(_mobiltySelectedValue, fontCellSize9B));
                                    PdfTable1Mobility.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1Mobility);
                                    _mobiltySelectedValue = string.Empty;

                                    PdfPCell PdfTable1Comment = new PdfPCell(new Phrase(level + "," + sReasonValue.Replace("<b>", string.Empty).Replace("</b>", string.Empty), fontCellSize9));
                                    PdfTable1Comment.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1Comment);
                                };
                            }
                            else
                            {
                                PrintCarePlanEmergencyDetails(ds, fontCellSize9B, fontCellSize9, PdfTable1);
                            }
                            doc.Add(PdfTable1);
                        }

                        doc.Close();
                        string reportName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Emergency_Resident_Details_" + reportName + ".pdf");
                        Response.Clear();
                        Response.BinaryWrite(mStream.ToArray());
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                }
            }
            catch (Exception Ex)
            {
                sException = "frmEmergencyResidentDetails btnPdf_Click |" + Ex.Message.ToString();
                //Log.Write(sException);
               
            }
        }

        private string Get_bindResidentCareAssessment(string sCareAssessment, string colorCode)
        {
            string[] redColor = { "5HS", "6HA", "0MA", "0MD", "2WS", "8CS", "9CA", "2VI", "3VB", "3HD", "6CI" };
            string[] yellowColor = { "4HI", "22HM", "23HE", "3WA", "1WI", "0MS", "7CI", "7CG", "2HI" };
            string[] greenColor = { "8VIL", "9VIR", "6VL", "7VR", "9HM", "10HP", "11HE", "12HA" };

            string sValue = string.Empty;
            DataTable dt = new DataTable();
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            string colorValue = "W";
            dt.Columns.Add("CareAssessmentValue", typeof(string));
            dt.Columns.Add("CareAssessment", typeof(string));
            dt.Rows.Add("<b>" + "Mobility" + "</b>(Assistance)", "0MA");
            dt.Rows.Add("<b>Mobility </b>(Dependent)", "0MD");
            dt.Rows.Add("<b>Mobility</b>(Supervision)", "0MS");
            dt.Rows.Add("<b>Mobility</b>(Independent)", "0MI");
            dt.Rows.Add("<b>Walker</b>(Independent)", "1WI");
            dt.Rows.Add("<b>Walker</b>(Supervised)", "2WS");
            dt.Rows.Add("<b>Walker</b>(Assisted)", "3WA");
            dt.Rows.Add("<b>Wheelchair</b>(Independent)", "4HI");
            dt.Rows.Add("<b>Wheelchair</b>(Supervised)", "5HS");
            dt.Rows.Add("<b>Wheelchair</b>(Assisted)", "6HA");
            dt.Rows.Add("<b>Wheelchair</b>(Manual)", "22HM");
            dt.Rows.Add("<b>Wheelchair</b>(Electric)", "23HE");
            dt.Rows.Add("<b>Cane</b>(Independent)", "7CI");
            dt.Rows.Add("<b>Cane</b>(Supervised)", "8CS");
            dt.Rows.Add("<b>Cane</b>(Assisted)", "9CA");
            dt.Rows.Add("<b>SensoryAbilities</b>(Impaired)", "2VI");
            dt.Rows.Add("<b>SensoryAbilities</b>(ImpairedLeft)", "8VIL");
            dt.Rows.Add("<b>SensoryAbilities</b>(ImpairedRight)", "9VIR");
            dt.Rows.Add("<b>SensoryAbilities</b>(Blind)", "3VB");
            dt.Rows.Add("<b>SensoryAbilities</b>(BlindLeft)", "6VL");
            dt.Rows.Add("<b>SensoryAbilities</b>(BlindRight)", "7VR");
            dt.Rows.Add("<b>Hearing</b>(Impaired)", "2HI");
            dt.Rows.Add("<b>Hearing</b>(ImpairedLeft)", "9HM");
            dt.Rows.Add("<b>Hearing</b>(ImpairedRight)", "10HP");
            dt.Rows.Add("<b>Hearing</b>(Deaf)", "3HD");
            dt.Rows.Add("<b>Hearing</b>(DeafLeft)", "11HE");
            dt.Rows.Add("<b>Hearing</b>(DeafRight)", "12HA");
            dt.Rows.Add("<b>Cognitivefunction</b>(Confused)", "4CC");
            dt.Rows.Add("<b>Cognitivefunction</b>(Shorttermloss)", "5CS");
            dt.Rows.Add("<b>Cognitivefunction</b>(Significantimpairment)", "6CI");
            dt.Rows.Add("<b>Cognitivefunction</b>(Cuing)", "7CG");


            bool isAffected = false;
            foreach (DataRow Drow in dt.Rows)
            {
                if (Drow["CareAssessment"].ToString() == sCareAssessment)
                {
                    sValue = Drow["CareAssessmentValue"].ToString();
                    colorValue = Drow["CareAssessment"].ToString();
                    if (isAffected == false)
                    {
                        foreach (string red in redColor)
                        {
                            if (red == colorValue)
                            {

                                _colorCode = "R";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if (isAffected == false && _colorCode != "R")
                    {
                        foreach (string yellow in yellowColor)
                        {
                            if (yellow == colorValue)
                            {

                                _colorCode = "Y";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if (isAffected == false && _colorCode != "R" && _colorCode != "G")
                    {
                        foreach (string green in greenColor)
                        {
                            if (green == colorValue)
                            {
                                _colorCode = "G";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    break;
                }
                isAffected = false;
            }
            return sValue;
        }

        protected void ddlResidentCareAssessment_Change(object sender, EventArgs e)
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

            int homeId = home.Id;
            string exception = string.Empty;
            DataSet ds = null;
            string sTick = string.Empty;
            string sUntick = string.Empty;
            bool bDataFlag = false;
            string sReason = string.Empty;
            string sReasonValue = string.Empty;
            string color = "W";
            try
            {
                int ActualOrder = 1;// ddlResidentCareAssessment.SelectedValue == "H" ? 1 : ddlResidentCareAssessment.SelectedValue == "M" ? 2 : ddlResidentCareAssessment.SelectedValue == "L" ? 3 : ddlResidentCareAssessment.SelectedValue == "N" ? 4 : 0;
                System.Text.StringBuilder sbReport = new System.Text.StringBuilder();
                ds = new DataSet();
                ds = ResidentsDAL.GetEmergencyResidentDetails(homeId, "0");
               // divEmergencyResidentDetails.Visible = true;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sbReport.Append("<div> <script src='JS/jquery-ui.min.js' type='text/javascript'></script><script src='JS/tableSorting.js' type='text/javascript'></script><table width='100%' class='tableStatic' id='tblSort'><thead><th  style='width:.1px;' data-sort='string'>RiskLevel</th><th style='width:10px;' data-sort='int'>SuiteNo </th><th style='width:25px;' data-sort='string' class=''>Name </th><th style='text-align: center;width:10px;'data-sort='string'  class=''>Gender </th><th style='width:10px;'data-sort='string'  class=''>Phone </th> <th style='width:10px;' data-sort='string' class=''>ContactPerson </th> <th style='width:15px;' data-sort='string'  class=''>EmergencyContact </th><th style='width:.1px;' data-sort='string'>RiskLevel </th><th style='width:15px;'data-sort='string'  class=''>Mobility </th> <th style='width:25px;' data-sort='string'>Comments </th> </thead><tbody>");
                        if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))
                        {
                            bDataFlag = true;
                            sbReport.Append(BindCarePlanEmergencyDetails(ds));
                            sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script></div>");
                        }
                        else
                        {
                            bDataFlag = true;

                            for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                            {
                                sReasonValue = string.Empty;
                                sReason = string.Empty;
                                sReason = sReason + ds.Tables[0].Rows[iRow]["fd_SA_vision"] + ds.Tables[0].Rows[iRow]["fd_mobility"] + ds.Tables[0].Rows[iRow]["fd_SA_hearing"] + ds.Tables[0].Rows[iRow]["fd_cognitive_function"] + ds.Tables[0].Rows[iRow]["fd_risk_level"].ToString() + ",";
                                if (sReason != string.Empty && sReason != null && !String.IsNullOrEmpty(sReason) && !String.IsNullOrWhiteSpace(sReason))
                                {
                                    sReason = sReason.Remove(sReason.Length - 1);
                                    string sResult;
                                    string[] sValue = sReason.Split(',');
                                    foreach (string word in sValue)
                                    {
                                        if (!String.IsNullOrEmpty(sReason) && !String.IsNullOrWhiteSpace(sReason))
                                        {
                                            if (word == "0MA" || word == "0MD" || word == "0MI" || word == "0MS")
                                            {
                                                GetMobilityValue(word);
                                            }
                                            sResult = Get_bindResidentCareAssessment(word, color);
                                            if (sResult != string.Empty && sResult != null)
                                            {
                                                if (sReasonValue.Length > 0)
                                                {
                                                    sReasonValue = sReasonValue + ',' + ' ' + Get_bindResidentCareAssessment(word, color);
                                                }
                                                else
                                                {
                                                    sReasonValue = Get_bindResidentCareAssessment(word, color);
                                                }
                                            }
                                        }
                                        sResult = string.Empty;
                                    }
                                }

                                int order = Convert.ToInt32(ds.Tables[0].Rows[iRow]["fd_risk_order"]);
                                int AcceptedOrder = 0;
                                if (ActualOrder > 0)
                                {
                                    AcceptedOrder = _colorCode == "R" || order == 1 ? 1 : _colorCode == "Y" || order == 2 ? 2 : _colorCode == "G" ? 3 : 4;
                                    if (AcceptedOrder != ActualOrder)
                                    {
                                        _colorCode = "W";
                                        _mobiltySelectedValue = string.Empty;
                                        continue;
                                    }
                                }

                                string phone = ds.Tables[0].Rows[iRow]["fd_phone"].ToString().Trim().Length > 0 ? ds.Tables[0].Rows[iRow]["fd_phone"].ToString() : "NoPhone";
                                string level = string.Empty;
                                int sortOrder = 1;
                                switch (order)
                                {
                                    case 1:
                                        level = "HighFallRisk";
                                        break;
                                    case 2:
                                        level = "MediumFallingRisk";
                                        break;
                                    case 3:
                                        level = "LowFallingRisk";
                                        break;
                                    default:
                                        level = string.Empty;
                                        break;
                                }

                                string colorQuery = "<td style='background-color:white; width:2px;'></td>";

                                if (_colorCode == "R" || order == 1)
                                {
                                    sortOrder = 4;
                                    colorQuery = "<td style='background-color:red;width:3px;'><span style='color:red;'>" + sortOrder + "</span></td>";
                                }
                                else if (_colorCode == "Y" || order == 2)
                                {
                                    sortOrder = 3;
                                    colorQuery = "<td style='background-color:yellow'><span style='color:yellow;'>" + sortOrder + "</span></td>";
                                }
                                else if (_colorCode == "G")
                                {
                                    sortOrder = 2;
                                    colorQuery = "<td style='background-color:green'><span style='color:green;'>" + sortOrder + "</span></td>";
                                }
                                string designString = "<tr style='text-align:left;'>" + colorQuery + "<td style='width:5px;'>" + ds.Tables[0].Rows[iRow]["fd_suite_no"] + "</td>";
                                designString += "<td>" + ds.Tables[0].Rows[iRow]["fd_first_name"] + " " + ds.Tables[0].Rows[iRow]["fd_last_name"] + "</td><td>"
                                    + ds.Tables[0].Rows[iRow]["fd_gender"] + "</td><td>" + phone + "</td><td>" + ds.Tables[0].Rows[iRow]["fd_contact_1"] + "</td><td>"
                                    + ds.Tables[0].Rows[iRow]["fd_home_phone_1"] + "</td>"
                                    + colorQuery
                                    + "<td style='width:10px;'><b>" + _mobiltySelectedValue + "</b></td><td><b>" + level + "</b>";
                                if (sReasonValue.Length > 0 && level.Length > 0)
                                {
                                    designString += "<b>, </b>";
                                }
                                designString += sReasonValue + "</td></tr>";
                                sbReport.Append(designString);
                                _colorCode = "W";
                                _mobiltySelectedValue = string.Empty;

                            }
                            sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script></div>");
                        }
                    }
                    else
                    {
                        sbReport.Append("<div><table width='100%' class='tableStatic'><tr><th>No Record</th></tr></table></div>");
                    }
                    if (!bDataFlag)
                    {
                        //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                    //divEmergencyResidentDetails.InnerHtml = sbReport.ToString();
                }
                else
                {
                    //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                }
            }

            catch (Exception Ex)
            {
                exception = "frmEmergencyResidentDetails EmergencyResidentDetails |" + Ex.Message.ToString();
                //Log.Write(exception);
               
            }
        }

        protected void ddlResidentCareAssessment_Change_Old(object sender, EventArgs e)
        {
            int iHomeId = 0;
            string exception = string.Empty;
            DataSet ds = null;
            string sTick = string.Empty;
            string sUntick = string.Empty;
            bool bDataFlag = false;
            string sReason = string.Empty;
            string sReasonValue = string.Empty;

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
                if (home != null == true)
                {
                    iHomeId = home.Id;
                    int ActualOrder = 1; // ddlResidentCareAssessment.SelectedValue == "H" ? 1 : ddlResidentCareAssessment.SelectedValue == "M" ? 2 : ddlResidentCareAssessment.SelectedValue == "L" ? 3 : ddlResidentCareAssessment.SelectedValue == "N" ? 4 : 0;
                    System.Text.StringBuilder sbReport = new System.Text.StringBuilder();
                    ds = new DataSet();
                    ds = ResidentsDAL.GetEmergencyResidentDetails(iHomeId, "0");

                    //divEmergencyResidentDetails.Visible = true;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            sbReport.Append("<div> <script src='JS/jquery-ui.min.js' type='text/javascript'></script><script src='JS/tableSorting.js' type='text/javascript'></script><table width='100%' class='tableStatic' id='tblSort'><thead><th  style='width:.1px;' data-sort='string'>RiskLevel</th><th style='width:10px;' data-sort='int'>SuiteNo </th><th style='width:25px;' data-sort='string' class=''>Name </th><th style='text-align: center;width:10px;'data-sort='string'  class=''>Gender</th><th style='width:10px;'data-sort='string'  class=''>Phone </th> <th style='width:10px;' data-sort='string' class=''>ContactPerson </th> <th style='width:20px;' data-sort='string'  class=''>EmergencyContact </th><th style='width:.1px;' data-sort='string'>RiskLevel </th><th style='width:15px;'data-sort='string'  class=''>Mobility </th> <th style='width:25px;' data-sort='string'>Comments </th> </thead><tbody>");
                            if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))
                            {
                                bDataFlag = true;
                                sbReport.Append(BindCarePlanEmergencyDetails(ds));
                                sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script></div>");
                            }
                            else
                            {

                                bDataFlag = true;

                                for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                                {
                                    sReasonValue = string.Empty;
                                    sReason = string.Empty;

                                    sReason = sReason + ds.Tables[0].Rows[iRow]["fd_SA_vision"] + ds.Tables[0].Rows[iRow]["fd_mobility"] + ds.Tables[0].Rows[iRow]["fd_SA_hearing"] + ds.Tables[0].Rows[iRow]["fd_cognitive_function"] + ds.Tables[0].Rows[iRow]["fd_risk_level"].ToString() + ",";

                                    if (sReason != string.Empty && sReason != null)
                                    {
                                        sReason = sReason.Remove(sReason.Length - 1);
                                        string sResult = "";
                                        string[] sValue = sReason.Split(',');
                                        foreach (string word in sValue)
                                        {

                                            if (word == "0MA" || word == "0MD" || word == "0MI" || word == "0MS")
                                            {
                                                GetMobilityValue(word);
                                            }
                                            sResult = Get_bindResidentCareAssessment(word, "W");


                                            if (sResult != string.Empty && sResult != null)
                                            {

                                                if (sReasonValue.Length > 0)
                                                {
                                                    sReasonValue = sReasonValue + ',' + ' ' + Get_bindResidentCareAssessment(word, _colorCode);
                                                }
                                                else
                                                {
                                                    sReasonValue = Get_bindResidentCareAssessment(word, _colorCode);
                                                }

                                            }

                                            sResult = string.Empty;
                                        }
                                    }

                                    int order = Convert.ToInt32(ds.Tables[0].Rows[iRow]["fd_risk_order"]);
                                    int AcceptedOrder = 0;
                                    if (ActualOrder > 0)
                                    {
                                        AcceptedOrder = _colorCode == "R" || order == 1 ? 1 : _colorCode == "Y" || order == 2 ? 2 : _colorCode == "G" || order == 3 ? 3 : 4;
                                        if (AcceptedOrder != ActualOrder)
                                        {
                                            _colorCode = "W";
                                            _previousValue = _mobiltySelectedValue;
                                            _mobiltySelectedValue = string.Empty;
                                            continue;
                                        }
                                    }

                                    int sortOrder = 1;
                                    string phone = ds.Tables[0].Rows[iRow]["fd_phone"].ToString().Trim().Length > 0 ? ds.Tables[0].Rows[iRow]["fd_phone"].ToString() : "NoPhone";
                                    string colorQuery = "<td style='background-color:white; width:2px;' ></td>";
                                    string level = string.Empty;
                                    switch (order)
                                    {
                                        case 1:
                                            level = "HighFallRisk";
                                            break;
                                        case 2:
                                            level = "MediumFallingRisk";
                                            break;
                                        case 3:
                                            level = "LowFallingRisk";
                                            break;
                                        default:
                                            level = string.Empty;
                                            break;
                                    }
                                    if (_colorCode == "R" || order == 1)
                                    {
                                        sortOrder = 4;
                                        colorQuery = "<td style='background-color:red;width:3px;'><span style='color:red;'>" + sortOrder + "</span></td>";
                                    }
                                    else if (_colorCode == "Y" || order == 2)
                                    {
                                        sortOrder = 3;
                                        colorQuery = "<td style='background-color:yellow'><span style='color:yellow;'>" + sortOrder + "</span></td>";
                                    }
                                    else if (_colorCode == "G" || order == 3)
                                    {
                                        sortOrder = 2;
                                        colorQuery = "<td style='background-color:green'><span style='color:green;'>" + sortOrder + "</span></td>";
                                    }
                                    else if (order == 3 && sReasonValue == "")
                                    {
                                        sortOrder = 1;
                                        colorQuery = "<td style='background-color:white; width:2px;'></td>";
                                    }
                                    string designString = "<tr style='text-align:left;'>" + colorQuery + "<td style='width:5px;'>" + ds.Tables[0].Rows[iRow]["fd_suite_no"] + "</td>";
                                    designString += "<td>" + ds.Tables[0].Rows[iRow]["fd_first_name"] + " " + ds.Tables[0].Rows[iRow]["fd_last_name"] + "</td><td>"
                                        + ds.Tables[0].Rows[iRow]["fd_gender"] + "</td><td>" + phone + "</td><td>" + ds.Tables[0].Rows[iRow]["fd_contact_1"] + "</td><td>"
                                        + ds.Tables[0].Rows[iRow]["fd_home_phone_1"] + "</td>"
                                        + colorQuery
                                        + "<td style='width:10px;'><b>" + _mobiltySelectedValue + "</b></td><td>" + "<b>" + level + "</b>";
                                    if (sReasonValue.Length > 0 && level.Length > 0)
                                    {
                                        designString += "<b>, </b>";
                                    }
                                    designString += sReasonValue + "</td></tr>";
                                    sbReport.Append(designString);
                                    _colorCode = "W";
                                    _previousValue = _mobiltySelectedValue;
                                    _mobiltySelectedValue = string.Empty;
                                }
                                string style = "<style type='text/css'>th[data-sort]{cursor:pointer;}</style>";
                                sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script>" + style + "</div>");
                            }
                        }
                        else
                        {
                            sbReport.Append("<div><table width='100%' class='tableStatic'><tr><th>No Record</th></tr></table></div>");
                        }
                        if (!bDataFlag)
                        {
                           // AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                        }
                        //divEmergencyResidentDetails.InnerHtml = sbReport.ToString();
                    }
                    else
                    {
                        //divEmergencyResidentDetails.InnerHtml = sbReport.ToString();
                       // AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                }
            }

            catch (Exception Ex)
            {
                exception = "frmEmergencyResidentDetails EmergencyResidentDetails |" + Ex.Message.ToString();
               // Log.Write(exception);
               
            }

        }

        private string GetMobilityValue(string dataValue)
        {
            switch (dataValue)
            {
                case "0MI":
                    if (_mobiltySelectedValue == "Assisted" || _mobiltySelectedValue == "Dependent" ||
                          _mobiltySelectedValue == "Supervised")
                    {
                        _mobiltySelectedValue = _previousValue;
                    }
                    else
                    {
                        _mobiltySelectedValue = "Independent";
                        _previousValue = _mobiltySelectedValue;
                    }
                    break;
                case "0MS":
                    if (_mobiltySelectedValue == "Assisted" || _mobiltySelectedValue == "Dependent")
                    {
                        _previousValue = _mobiltySelectedValue;
                    }
                    else
                    {
                        _mobiltySelectedValue = "Supervised";
                        _previousValue = _mobiltySelectedValue;
                    }
                    break;
                case "0MA":
                    _mobiltySelectedValue = "Assisted";
                    _previousValue = _mobiltySelectedValue;
                    break;
                case "0MD":
                    _mobiltySelectedValue = "Dependent";
                    _previousValue = _mobiltySelectedValue;
                    break;
                default:
                    if (_mobiltySelectedValue != string.Empty)
                    {
                        _mobiltySelectedValue = string.Empty;
                    }
                    break;
            }

            return _mobiltySelectedValue;
        }

        private void GetCategoryValue()
        {
            string[] redColor = { "14.4", "11.7", "11.6", "8.2", "7.6", "6.6", "6.5" };
            string[] yellowColor = { "14.3", "11.5", "11.4", "7.3", "6.3" };
            string[] greenColor = { "14.2", "11.3", "11.2", "7.2", "6.2" };
            string exception = string.Empty;
            try
            {
                ArrayList list = new ArrayList();
                list.Add(new System.Web.UI.WebControls.ListItem("6.1" + "-Aucune intervention", "6.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.2" + "-Légère vérification avec ou sans adaptation", "6.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.3" + "-Encadrer, rappeler, stimuler, surveiller", "6.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.5" + "-Accompagner un usager présentant un risque ou une difficulté à  faire ses transferts", "6.5"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.6" + "-Procéder aux transferts d’un usager présentant un risque ou une difficulté", "6.6"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.1" + "-Aucune intervention", "7.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.2" + "-Légère vérification avec ou sans adaptation", "7.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.3" + "-Encadrer, rappeler, stimuler, surveiller", "7.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.5" + "-Accompagner un usager présentant un risque ou une difficulté dans  ses déplacements", "7.5"));
                list.Add(new System.Web.UI.WebControls.ListItem("8.1" + "-Aucune intervention", "8.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("8.2" + "-En cas d’urgence", "8.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.1" + "-Aucune intervention", "11.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.2" + "-Légère vérification avec ou sans adaptation", "11.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.3" + "-Aider, conseiller, encadrer, prévenir, rappeler, sensibiliser", "11.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.4" + "-Favoriser la sollicitation", "11.4"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.5" + "-Apprendre à l’usager à développer de meilleures habiletés sociales et de résolution de problème", "11.5"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.6" + "-Accompagner a un usager présentant un risque ou une difficulté à développer de meilleures habiletés qui et de résolution de problème", "11.6"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.7" + "-Contrôler les troubles relationnels de l’usager", "11.7"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.1" + "-Aucune intervention", "14.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.2" + "-Légère vérification avec ou sans adaptation de l’usager dans ses activités de la vie domestique", "14.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.3" + "-Aider, conseiller, encadrer, favoriser, rappeler, stimuler, surveiller, vérifier", "14.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.4" + "-Accompagner l’usager dans ses activités de la vie domestique", "14.4"));

                list = new ArrayList();
                list.Add(new System.Web.UI.WebControls.ListItem("HighRisk", "H"));
                list.Add(new System.Web.UI.WebControls.ListItem("ModerateRisk", "M"));
                list.Add(new System.Web.UI.WebControls.ListItem("LowRisk", "L"));
                list.Add(new System.Web.UI.WebControls.ListItem("NoRisk", "N"));

                //ddlResidentCareAssessment.DataSource = list;
                //ddlResidentCareAssessment.DataTextField = "Text";
                //ddlResidentCareAssessment.DataValueField = "Value";
                //ddlResidentCareAssessment.DataBind();
                //ddlResidentCareAssessment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- ALL --", "0"));
            }
            catch (Exception Ex)
            {
                exception = "EmergencyResidentDetails GetCategoryValue |" + Ex.Message.ToString();
              //  Log.Write(exception);
               
            }
            finally
            {

            }
        }

        private string Get_bindResidentCareAssessment_CarePlan(string sCareAssessment, string colorCode)
        {
            string[] redColor = { "14.4", "11.7", "11.6", "8.2", "7.5", "6.6", "6.5" };
            string[] yellowColor = { "14.3", "11.5", "11.4", "7.3", "6.3" };
            string[] greenColor = { "14.2", "11.3", "11.2", "7.2", "6.2" };

            string sValue = string.Empty;
            DataTable dt = new DataTable();
            string colorValue = "W";
            dt.Columns.Add("CareAssessmentValue", typeof(string));
            dt.Columns.Add("CareAssessment", typeof(string));
            dt.Rows.Add("<b>14.4 - </b>Accompagner l’usager dans ses activités de la vie domestique", "14.4");
            dt.Rows.Add("<b>11.7 - </b>Contrôler les troubles relationnels de l’usager", "11.7");
            dt.Rows.Add("<b>11.6 - </b>Accompagner a un usager présentant un risque ou une difficulté à développer de meilleures habiletés qui et de résolution de problème", "11.6");
            dt.Rows.Add("<b>8.2 - </b>En cas d’urgence", "8.2");
            dt.Rows.Add("<b>7.5 - </b>Accompagner un usager présentant un risque ou une difficulté dans  ses déplacements", "7.5");
            dt.Rows.Add("<b>6.6 - </b>Procéder aux transferts d’un usager présentant un risque ou une difficulté", "6.6");
            dt.Rows.Add("<b>6.5 - </b>Accompagner un usager présentant un risque ou une difficulté à  faire ses transferts", "6.5");
            dt.Rows.Add("<b>14.3 – </b>Aider, conseiller, encadrer, favoriser, rappeler, stimuler, surveiller, vérifier", "14.3");
            dt.Rows.Add("<b>11.5 – </b>Apprendre à l’usager à développer de meilleures habiletés sociales et de résolution de problème", "11.5");
            dt.Rows.Add("<b>11.4 - </b>Favoriser la sollicitation", "11.4");
            dt.Rows.Add("<b>7.3 - </b>Encadrer, rappeler, stimuler, surveiller", "7.3");
            dt.Rows.Add("<b>6.3 - </b>Encadrer, rappeler, stimuler, surveiller", "6.3");
            dt.Rows.Add("<b>14.2 – </b>Légère vérification avec ou sans adaptation de l’usager dans ses activités de la vie domestique", "14.2");
            dt.Rows.Add("<b>11.3 - </b>Aider, conseiller, encadrer, prévenir, rappeler, sensibiliser", "11.3");
            dt.Rows.Add("<b>11.2 - </b>Légère vérification avec ou sans adaptation", "11.2");
            dt.Rows.Add("<b>7.2 - </b>Légère vérification avec ou sans adaptation", "7.2");
            dt.Rows.Add("<b>6.2 - </b>Légère vérification avec ou sans adaptation", "6.2");
            dt.Rows.Add("<b>14.1 - </b>Aucune intervention", "14.1");
            dt.Rows.Add("<b>11.1 - </b>Aucune intervention", "11.1");
            dt.Rows.Add("<b>8.1 – </b>Aucune intervention", "8.1");
            dt.Rows.Add("<b>7.1 - </b>Aucune intervention", "7.1");
            dt.Rows.Add("<b>6.1 - </b>Aucune intervention", "6.1");



            bool isAffected = false;
            foreach (DataRow Drow in dt.Rows)
            {
                if (Drow["CareAssessment"].ToString() == sCareAssessment)
                {
                    sValue = Drow["CareAssessmentValue"].ToString();
                    colorValue = Drow["CareAssessment"].ToString();
                    if (isAffected == false)
                    {
                        foreach (string red in redColor)
                        {
                            if (red == colorValue)
                            {

                                _colorCode = "R";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if ((isAffected == false && _colorCode != "R") || (isAffected == true && _colorCode != "R"))
                    {
                        foreach (string yellow in yellowColor)
                        {
                            if (yellow == colorValue)
                            {

                                _colorCode = "Y";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if (isAffected == false && _colorCode != "R" && _colorCode != "G")
                    {
                        foreach (string green in greenColor)
                        {
                            if (green == colorValue)
                            {
                                _colorCode = "G";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    break;
                }
                isAffected = false;
            }
            return sValue;
        }

        private string BindCarePlanEmergencyDetails(DataSet ds)
        {
            string query = string.Empty;
            var residentList = (from residentTable in ds.Tables[0].AsEnumerable()
                                select new
                                {
                                    ResId = residentTable.Field<Int32>("fd_resident_id"),
                                    RiskOrder = residentTable.Field<Int32>("fd_risk_order")
                                }).Distinct();

            if (residentList != null)
            {
                foreach (var residents in residentList)
                {
                    query += "<tr>";
                    var residentDetail = (from resident in ds.Tables[0].AsEnumerable()
                                          select new
                                          {
                                              RID = resident.Field<Int32>("fd_resident_id"),
                                              Suite = resident.Field<String>("fd_suite_no"),
                                              RFname = resident.Field<String>("fd_first_name"),
                                              RLname = resident.Field<String>("fd_last_name"),
                                              Gender = resident.Field<String>("fd_gender"),
                                              Phone = resident.Field<String>("fd_phone"),
                                              Contact = resident.Field<String>("fd_contact_1"),
                                              HomePhone = resident.Field<String>("fd_home_phone_1"),
                                              Code = resident.Field<String>("fd_subcategory_code"),
                                              CategoryName = resident.Field<String>("fd_category_name"),
                                              RLevel = resident.Field<String>("fd_risk_level")
                                          }).Where(x => x.RID == residents.ResId);
                    int count = 0;
                    string comment = "", residentcommon = string.Empty;
                    string colorCell = "";
                    string level = string.Empty;
                    int sortOrder = 1;
                    foreach (var objRes in residentDetail)
                    {
                        Get_bindResidentCareAssessment_CarePlan(objRes.Code, "");
                        if (count == 0)
                        {
                            switch (residents.RiskOrder)
                            {
                                case 1:
                                    level = "HighFallRisk";
                                    break;
                                case 2:
                                    level = "MediumFallingRisk";
                                    break;
                                case 3:
                                    level = "LowFallingRisk";
                                    break;
                                default:
                                    level = string.Empty;
                                    break;
                            }
                            residentcommon += "<td>" + objRes.Suite + "</td>";
                            residentcommon += "<td>" + objRes.RFname + " " + objRes.RLname + "</td>";
                            residentcommon += "<td>" + objRes.Gender + "</td>";
                            string phone = objRes.Phone.Trim().Length > 0 ? objRes.Phone : "NoPhone";
                            residentcommon += "<td>" + phone + "</td>";
                            residentcommon += "<td>" + objRes.Contact + "</td>";
                            residentcommon += "<td>" + objRes.HomePhone + "</td>";

                            if (level != "")
                            {
                                comment += "<b>" + level + "</b></br>";
                            }
                        }
                        if (objRes.Code != "" || objRes.CategoryName != "")
                        {
                            comment += "<b>" + objRes.Code + " - </b>" + objRes.CategoryName + "</br>";
                        }
                        count++;
                    }
                    if (_colorCode == "R" || residents.RiskOrder == 1)
                    {

                        colorCell = "<td style='background-color:red;width:3px;'><span style='color:red;'>" + sortOrder + "</span></td>";
                    }
                    else if (_colorCode == "Y" || residents.RiskOrder == 2)
                    {

                        colorCell = "<td style='background-color:yellow;width:3px;'><span style='color:yellow;'>" + sortOrder + "</span></td>";
                    }
                    else if (_colorCode == "G")
                    {

                        colorCell = "<td style='background-color:green;width:3px;'><span style='color:green;'>" + sortOrder + "</span></td>";
                    }
                    else
                    {
                        colorCell = "<td style='background-color:white;width:3px;'><span style='color:white;'>" + sortOrder + "</span></td>";
                    }
                    query += colorCell + residentcommon + colorCell + "<td></td><td>" + comment + "</td></tr>";
                    residentcommon = string.Empty;
                    _colorCode = "W";
                }
            }
            return query;
        }

        private void PrintCarePlanEmergencyDetails(DataSet ds, Font font8B, Font font8, PdfPTable table)
        {
            string query = string.Empty;
            var residentList = (from residentTable in ds.Tables[0].AsEnumerable()
                                select new
                                {
                                    ResId = residentTable.Field<Int32>("fd_resident_id"),
                                    RiskOrder = residentTable.Field<Int32>("fd_risk_order")
                                }).Distinct();
            if (residentList != null)
            {


                foreach (var residents in residentList)
                {
                    var residentDetail = (from resident in ds.Tables[0].AsEnumerable()
                                          select new
                                          {
                                              RID = resident.Field<Int32>("fd_resident_id"),
                                              Suite = resident.Field<String>("fd_suite_no"),
                                              RFname = resident.Field<String>("fd_first_name"),
                                              RLname = resident.Field<String>("fd_last_name"),
                                              Gender = resident.Field<String>("fd_gender"),
                                              Phone = resident.Field<String>("fd_phone"),
                                              Contact = resident.Field<String>("fd_contact_1"),
                                              HomePhone = resident.Field<String>("fd_home_phone_1"),
                                              Code = resident.Field<String>("fd_subcategory_code"),
                                              CategoryName = resident.Field<String>("fd_category_name"),
                                              RLevel = resident.Field<String>("fd_risk_level")
                                          }).Where(x => x.RID == residents.ResId);
                    string suiteNo = string.Empty;
                    string residentName = string.Empty;
                    string gender = string.Empty;
                    string phone = string.Empty;
                    string contact = string.Empty;
                    string homePhone = string.Empty;
                    string level = string.Empty;
                    int count = 0;

                    Phrase comment = new Phrase();
                    string residentcommon = string.Empty;
                    foreach (var objRes in residentDetail)
                    {
                        Get_bindResidentCareAssessment_CarePlan(objRes.Code, "");
                        if (count == 0)
                        {
                            switch (residents.RiskOrder)
                            {
                                case 1:
                                    level = "HighFallRisk";
                                    break;
                                case 2:
                                    level = "MediumFallingRisk";
                                    break;
                                case 3:
                                    level = "LowFallingRisk";
                                    break;
                                default:
                                    level = string.Empty;
                                    break;
                            }
                            suiteNo = objRes.Suite;
                            residentName = objRes.RFname + " " + objRes.RLname;
                            residentcommon += "<td>" + objRes.Gender + "</td>";
                            phone = objRes.Phone.Trim().Length > 0 ? objRes.Phone : "NoPhone";
                            contact = objRes.Contact;
                            homePhone = objRes.HomePhone;

                            if (level != "")
                            {
                                comment.Add(new Chunk(level + "\n", font8B));
                            }
                        }
                        if (objRes.Code != "" || objRes.CategoryName != "")
                        {
                            comment.Add(new Chunk(objRes.Code + " - ", font8B));
                            comment.Add(new Chunk(objRes.CategoryName + "\n", font8));

                        }
                        count++;
                    }
                    PdfPCell cellBG = new PdfPCell();
                    cellBG.BackgroundColor = BaseColor.WHITE;
                    if (_colorCode == "R" || residents.RiskOrder == 1)
                    {
                        cellBG.BackgroundColor = BaseColor.RED;
                    }
                    else if (_colorCode == "Y" || residents.RiskOrder == 2)
                    {
                        cellBG.BackgroundColor = BaseColor.YELLOW;
                    }
                    else if (_colorCode == "G")
                    {
                        cellBG.BackgroundColor = BaseColor.GREEN;
                    }

                    table.AddCell(cellBG);
                    table.AddCell(new PdfPCell(new Phrase(suiteNo, font8)));
                    table.AddCell(new PdfPCell(new Phrase(residentName, font8)));
                    table.AddCell(new PdfPCell(new Phrase(gender, font8)));
                    table.AddCell(new PdfPCell(new Phrase(phone, font8)));
                    table.AddCell(new PdfPCell(new Phrase(contact, font8)));
                    table.AddCell(new PdfPCell(new Phrase(homePhone, font8)));
                    table.AddCell(new PdfPCell(cellBG));
                    table.AddCell(new PdfPCell());

                    residentcommon = string.Empty;
                    _colorCode = "W";
                }

            }

        }



        #region Dining Attendance

        public ActionResult DiningAttendance(string datesel)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            Dining_Attendance_simple LIST_VIEW_RESIDENT = new Dining_Attendance_simple();

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Today);
                string[] genderinfo = HomeDAL.get_gender_info(home.Id, DateTime.Today);
                TempData["number_attendance_array"] = genderinfo;
            }
            else
            {
                TempData["datechoose"] = datesel;
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Parse(datesel));
                string[] genderinfo = HomeDAL.get_gender_info(home.Id, DateTime.Parse(datesel));
                TempData["number_attendance_array"] = genderinfo;
            }
            TempData["LIST_VIEW_RESIDENT"] = LIST_VIEW_RESIDENT;
            TempData.Keep("LIST_VIEW_RESIDENT");
            ViewBag.LIST_VIEW_RESIDENT = LIST_VIEW_RESIDENT;
            //DateTime lastSunday = DateTime.Now;
            //while (lastSunday.DayOfWeek != DayOfWeek.Sunday)
            //    lastSunday = lastSunday.AddDays(-1);
            //TempData["Sunday"] = lastSunday;

            return View(LIST_VIEW_RESIDENT);
        }

        [HttpPost]
        public int saveButton_Dining(string arr, int whichmeal, string datesel)
        {
            string meal="";
            if (whichmeal == 1)
                meal = "Breakfast";
            else if (whichmeal == 2)
                meal = "Lunch";
            else if (whichmeal == 3)
                meal = "Dinner";
            string arr_2 = arr.Replace("Taken", "option1").Replace("Refused", "option2").Replace("Hospital", "option3").Replace("Waiver", "option4").Replace("Away", "option5").Replace("Tray Complimentary", "option6");
            arr = arr.Replace("option1", "Taken").Replace("option2", "Refused").Replace("option3", "Hospital").Replace("option4", "Waiver").Replace("option5", "Away").Replace("option6", "Tray Complimentary");
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<string> Save_sum_array = new List<string>();
            List<string> Save_sum_array_original = new List<string>();
            Dictionary<string, string> Save_summary = new Dictionary<string, string>();
            if (arr_2.Length != 0)
            {
                Save_sum_array = arr_2.Substring(0, arr_2.Length - 1).Split(',').ToList();
                Save_sum_array_original = arr.Substring(0, arr.Length - 1).Split(',').ToList();
                for (int a = 0; a < Save_sum_array.Count(); a = a + 2)
                {
                    Dining_Attendance_functions.add_progress_note(int.Parse(Save_sum_array_original[a]), DateTime.Now, "Resident "+ Save_sum_array_original[a+1]+" "+meal, user.ID, DateTime.Now);

                    if (Save_summary.ContainsKey(Save_sum_array[a + 1]) == false)
                    {
                        Save_summary.Add(Save_sum_array[a + 1], Save_sum_array[a]);
                    }
                    else
                        Save_summary[Save_sum_array[a + 1]] += "," + Save_sum_array[a];
                }
            }
            Dining_Attendance_functions.save_Button(home.Id, whichmeal, DateTime.Parse(datesel), Save_summary, user.ID, DateTime.Now);

                return 1;
        }

        [HttpGet]
        public StringBuilder Getting_Dining(int whichmeal, string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            StringBuilder returnstring=Dining_Attendance_functions.getting_LIST(whichmeal, DateTime.Parse(datesel),home.Id);
            return returnstring;

        }

        [HttpGet]
        public ActionResult View_List(int whichmeal, string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            StringBuilder returnstring = Dining_Attendance_functions.getting_LIST(whichmeal, DateTime.Parse(datesel),home.Id);
            string[] viewlist = returnstring.ToString().Split(';');

            List<dynamic> l_Json = new List<dynamic>();

            for (var c = 0; c < viewlist.Length; c++)
            {
                var cplus = c + 1;
                for (var b = 0; b < viewlist[c].Split(',').Length; b++)
                {
                    if (viewlist[c].Split(',')[b] != "")
                    {
                        dynamic l_J = new System.Dynamic.ExpandoObject();
                        var resident = ResidentsDAL.GetResidentById(int.Parse(viewlist[c].Split(',')[b]));

                        l_J.Name = resident.FirstName + " " + resident.LastName;
                        l_J.residentid = viewlist[c].Split(',')[b];
                        l_J.gender = resident.Gendar;
                        if (c == 0)
                            l_J.action = "Taken";
                        else if (c == 1)
                            l_J.action = "Refused";
                        else if (c == 2)
                            l_J.action = "Hospital";
                        else if (c == 3)
                            l_J.action = "Waiver";
                        else if (c == 4)
                            l_J.action = "Away";
                        else if (c == 5)
                            l_J.action = "Tray Complimentary";

                        l_Json.Add(l_J);

                        //returnstring.Replace(viewlist[c].Split(',')[b], resident.FirstName + resident.LastName);
                    }
                }
            }

            //return returnstring;
            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public string disable_hospital(DateTime datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            List<int> returnleaving = Dining_Attendance_functions.disable_hospital_list(datesel);
            string returnstring= string.Join(",", returnleaving.ToArray());
            return returnstring;
        }

        [HttpGet]
        public int click_progress_note(int residentid, string datesel, string note)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            int returnint = Dining_Attendance_functions.add_progress_note(residentid,DateTime.Now, note,user.ID,DateTime.Now);

            return returnint;
        }

        #endregion



        #region To Do List

        [HttpGet]
        public ActionResult HO_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_hospital_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult HO_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_hospital_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DU_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_DU_list(home.Id, user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IAA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IAA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult IDA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IDA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IFRA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IFRA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IRCA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IRCA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PN_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_PN_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AN_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_AN_list(home.Id,user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AN_VIEW_CLICK(int PNid, int residentid)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.AN_VIEW(PNid, residentid, user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RAA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RAA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RDA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RDA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RFRA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RFRA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RRCA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RRCA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RI_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RI_list(home.Id, user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void RI_Acknowledge_CLICK(int pnid,int residentid,string action)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            to_do_list_function.get_RI_Acknowledge(user.ID,pnid,residentid,action);
        }

        [HttpGet]
        public ActionResult RB_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RB_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RP_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RP_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NR_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_NR_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_SA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_SA_nextmonth_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region resident calendars
        public ActionResult SuggestedActivityCalendar()
        {
            return View();
        }

        public ActionResult BirthdayCalendar()
        {
            return View();
        }
        #endregion



    }
}

    public class EmergencyDetailspdfData
    {
        public string fd_suite_no { get; set; }
        public string fd_first_name { get; set; }
        public string fd_last_name { get; set; }
        public string fd_gender { get; set; }
        public string fd_phone { get; set; }
        public string fd_contact_1 { get; set; }
        public string fd_home_phone_1 { get; set; }
        public string sReason { get; set; }
        public int fd_risk_order { get; set; }

        public string MobiltySelectedValue { get; set; }
    }

    public class pdfHeaderFooter : PdfPageEventHelper
    {
        int i = 1;
        public override void OnStartPage(PdfWriter writer, Document document)
        {

            base.OnOpenDocument(writer, document);
            iTextSharp.text.Font headerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
            PdfPTable tabHeader = new PdfPTable(new float[] { 1F, 1F });
            tabHeader.SpacingAfter = 1F;
            PdfPCell cell;
            tabHeader.TotalWidth = 783;

            PdfPCell cell1 = new PdfPCell(new Phrase("QOLA Date printed: " + DateTime.Now, headerFont));
            cell1.Border = 0;
            cell1.HorizontalAlignment = Element.ALIGN_LEFT;

            tabHeader.AddCell(cell1);

            cell = new PdfPCell(new Phrase("".ToString(), headerFont)); //removed homename
            cell.Border = 0;

            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabHeader.AddCell(cell);
            tabHeader.WriteSelectedRows(0, -1, 29, (document.PageSize.Height - 10), writer.DirectContent);
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            base.OnEndPage(writer, document);
            iTextSharp.text.Font footerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
            PdfPTable tabFot = new PdfPTable(new float[] { 9f, 12f, 8f });

            tabFot.TotalWidth = 785;
            tabFot.WidthPercentage = 100f;

            PdfPCell cell1 = new PdfPCell(new Phrase("EmergencyResidentDetails", footerFont));
            cell1.Border = 0;

            cell1.HorizontalAlignment = Element.ALIGN_LEFT;

            tabFot.AddCell(cell1);

            PdfPTable colorTable = new PdfPTable(8);
            colorTable.TotalWidth = 430f;
            colorTable.SetWidths(new float[] { .03f, .25f, .03f, .25f, .03f, .25f, .03f, .25f });


            PdfPCell redCell = new PdfPCell();
            redCell.BackgroundColor = BaseColor.RED;
            colorTable.AddCell(redCell);


            PdfPCell red = new PdfPCell(new Phrase("HighRisk", footerFont));
            red.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(red);

            PdfPCell yellowCell = new PdfPCell();
            yellowCell.BackgroundColor = BaseColor.YELLOW;
            colorTable.AddCell(yellowCell);

            PdfPCell yellow = new PdfPCell(new Phrase("ModerateRisk", footerFont));
            yellow.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(yellow);


            PdfPCell GreenCell = new PdfPCell();
            GreenCell.BackgroundColor = BaseColor.GREEN;
            colorTable.AddCell(GreenCell);

            PdfPCell Green = new PdfPCell(new Phrase("LowRisk", footerFont));
            Green.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(Green);

            PdfPCell noRiskCell = new PdfPCell();
            noRiskCell.BackgroundColor = BaseColor.WHITE;
            colorTable.AddCell(noRiskCell);


            PdfPCell noRisk = new PdfPCell(new Phrase("NoRisk", footerFont));
            noRisk.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(noRisk);


            PdfPCell colorCell = new PdfPCell(colorTable);
            colorCell.Border = 0;

            colorCell.HorizontalAlignment = Element.ALIGN_LEFT;
            colorCell.PaddingLeft = 30;
            tabFot.AddCell(colorCell);

            PdfPCell cell = new PdfPCell(new Phrase("Page" + " - " + i++, footerFont));
            cell.Border = 0;

            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 29, document.Bottom - 3, writer.DirectContent);
        }

    }



