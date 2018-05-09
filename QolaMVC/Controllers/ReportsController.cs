using QolaMVC.DAL;
using QolaMVC.Helpers;
using QolaMVC.Models;
using QolaMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BowelMovementReport()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;
            
            var bowelMovements = AssessmentDAL.GetResidentsBowelAssessments(resident.ID);
            
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(bowelMovements);
        }

        public ActionResult ExcerciseActivityReport()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            var vm = new ExcerciseActivityViewModel();

            vm.Detail = AssessmentDAL.GetExcerciseActivityDetail(resident.ID);
            vm.ExcerciseSummary = AssessmentDAL.GetExcerciseActivitySummary(resident.ID);
            vm.HSEPDetail = AssessmentDAL.GetHSEPDetail(resident.ID);

            if (vm.Detail.Count == 0 || vm.HSEPDetail.Count == 0)
            {
                QolaCulture.InitExcerciseActivity(ref vm);
            }
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(vm);
        }

        public ActionResult FamilyConferenceReport()
        {
            return View();
        }

        public ActionResult HeadToToeAssessment()
        {
            return View();
        }

        public ActionResult HSEPTrackingReport()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            var vm = new ExcerciseActivityViewModel();

            vm.Detail = AssessmentDAL.GetExcerciseActivityDetail(resident.ID);
            vm.ExcerciseSummary = AssessmentDAL.GetExcerciseActivitySummary(resident.ID);
            vm.HSEPDetail = AssessmentDAL.GetHSEPDetail(resident.ID);

            if (vm.Detail.Count == 0 || vm.HSEPDetail.Count == 0)
            {
                QolaCulture.InitExcerciseActivity(ref vm);
            }
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(vm);
        }

        public ActionResult PostFallClinicalMonitoring_A()
        {
            return View();
        }

        public ActionResult PostFallClinicalMonitoring_B()
        {
            return View();
        }

        public ActionResult ProgressNotes()
        {
            return View();
        }
    }
}