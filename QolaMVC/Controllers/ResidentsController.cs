using QolaMVC.DAL;
using QolaMVC.Helpers;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class ResidentsController : Controller
    {
        // GET: Residents
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string term)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            List<dynamic> l_Json = new List<dynamic>();
            var residents = ResidentsDAL.GetResidentSearchByHomeId(home.Id, term, 'N');

            foreach(var r in residents)
            {
                dynamic l_J = new System.Dynamic.ExpandoObject();
                l_J.label = r.ShortName;
                l_J.FirstName = r.FirstName;
                l_J.LastName = r.LastName;
                l_J.SuiteNo = r.SuiteNo;
                l_J.value = r.ID;
                l_J.image = r.ResidentImage;
                l_Json.Add(l_J);
            }

            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SuiteHandler(int p_ResidentId)
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
            return View(resident);
        }

        public ActionResult SearchResident()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AvailableSuite(DateTime term,int occu)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            List<dynamic> l_Json = new List<dynamic>();
            var residents = ResidentsDAL.GetAvailableSuitesNumber(home.Id, term, occu);

            foreach (var r in residents)
            {
                dynamic l_J = new System.Dynamic.ExpandoObject();
                l_J.number = r.ToString();

                l_Json.Add(l_J);
            }
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Available2(int home_value, DateTime term, int occu)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            List<dynamic> l_Json = new List<dynamic>();
            var residents = ResidentsDAL.GetAvailableSuitesNumber(home_value, term, occu);

            foreach (var r in residents)
            {
                dynamic l_J = new System.Dynamic.ExpandoObject();
                l_J.number = r.ToString();

                l_Json.Add(l_J);
            }
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public int saveButton_ApplicationSuite(DateTime term, int occu, int suitid, string notes)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if (update_Suite_Handler_Table.check_date_validation(term) == false && user.ID != 1338)
            {
                return 3;
            }
            else
            {
                int returnint = update_Suite_Handler_Table.ApplicationSuite(home.Id, resident.ID, suitid, occu, term, notes, 3, DateTime.Now);
                return returnint;
            }
        }

        [HttpPost]
        public int saveButton_ChangeOccupancy(DateTime transferdate, int occu, string notes)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if (occu == resident.Occupancy)
            {
                return 2;
            }
            else if (update_Suite_Handler_Table.check_date_validation(transferdate) == false && user.ID != 1338)
            {
                return 3;
            }
            else
            {
                DateTime moveout = transferdate;
                DateTime movein = transferdate;
                int returnint = update_Suite_Handler_Table.ChangeOccupancy(home.Id, resident.ID, int.Parse(resident.SuiteNo), occu, movein, moveout, notes, 3, DateTime.Now);
                return returnint;
            }
        }

        [HttpPost]
        public int saveButton_InternalTransfer(DateTime transferdate, int occu, int suitid, string notes)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if (int.Parse(resident.SuiteNo) == suitid)
            {
                return 2;
            }
            else if (update_Suite_Handler_Table.check_date_validation(transferdate) == false && user.ID != 1338)
            {
                return 3;
            }
            else
            {
                DateTime moveout = transferdate;
                DateTime movein = transferdate.AddDays(1);
                int returnint = update_Suite_Handler_Table.InternalTransfer(home.Id, resident.ID, suitid, occu, movein, moveout, notes, 3, DateTime.Now);
                return returnint;
            }
        }

        [HttpPost]
        public int saveButton_TransfertoASCHOME(DateTime transferdate, int occu, int suitid, string notes, int homeid)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if (home.Id == homeid)
            {
                return 2;
            }
            else if (update_Suite_Handler_Table.check_date_validation(transferdate) == false && user.ID != 1338)
            {
                return 3;
            }
            else
            {
                DateTime moveout = transferdate;
                DateTime movein = transferdate.AddDays(1);
                int returnint = update_Suite_Handler_Table.TransfertoASCHOME(homeid, resident.ID, suitid, occu, movein, moveout, notes, 3, DateTime.Now);
                return returnint;
            }
        }

        [HttpPost]
        public int saveButton_Normal_Move_Out(DateTime moveout, string notes, string reason)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if (update_Suite_Handler_Table.check_date_validation(moveout) == false && user.ID != 1338)
            {
                return 3;
            }
            else
            {
                int returnint = update_Suite_Handler_Table.Normal_Move_Out(home.Id, resident.ID, int.Parse(resident.SuiteNo), resident.Occupancy, moveout, notes, DateTime.Now, reason);
                return returnint;
            }
        }

        [HttpPost]
        public int saveButton_Passed_Away(DateTime moveout, string notes, DateTime passaway, string reason)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if ((update_Suite_Handler_Table.check_date_validation(moveout) == false || update_Suite_Handler_Table.check_date_validation(passaway) == false) && user.ID != 1338)
            {
                return 3;
            }
            else
            {
                int returnint = update_Suite_Handler_Table.Passed_away(home.Id, resident.ID, int.Parse(resident.SuiteNo), resident.Occupancy, moveout, notes, DateTime.Now, passaway, reason);
                return returnint;
            }
        }

        [HttpPost]
        public int saveButton_Hospitalization(DateTime leaving, DateTime ExpectedReturn, DateTime ActualReturn, string notes, string reason)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if ((update_Suite_Handler_Table.check_date_validation(leaving) == false || update_Suite_Handler_Table.check_date_validation(ExpectedReturn) == false || update_Suite_Handler_Table.check_date_validation(ActualReturn) == false) && user.ID != 1338)
            {
                return 3;
            }
            else
            {
                int returnint = update_Suite_Handler_Table.Hospitalization(home.Id, resident.ID, int.Parse(resident.SuiteNo), resident.Occupancy, leaving, ExpectedReturn, ActualReturn, notes, DateTime.Now, reason);
                return returnint;
            }
        }

        [HttpGet]
        public int undo_function(string reason)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            int returnint=update_Suite_Handler_Table.undo_function_for_SQL(resident.ID, reason);
            return returnint;
        }




    }
}