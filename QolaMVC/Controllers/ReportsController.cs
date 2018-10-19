using QolaMVC.DAL;
using QolaMVC.Helpers;
using QolaMVC.Models;
using QolaMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            string index = TempData["index"].ToString();
            string number = TempData["number"].ToString();
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            var vm = new ExcerciseActivityViewModel();
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            vm.mike = AssessmentDAL.getmike(resident.ID);
            if (vm.mike.Count == 0)
            {
                vm.mike = new Collection<ExcerciseActivityDetailModel_mike>();
                vm.mike.Add(new ExcerciseActivityDetailModel_mike());
            }

            vm.HSEPDetail_mike = AssessmentDAL.GetHSEPDetail_mike(resident.ID);
            if (vm.HSEPDetail_mike.Count == 0)
            {
                vm.HSEPDetail_mike = new Collection<HSEPDetailModel_mike>();
                vm.HSEPDetail_mike.Add(new HSEPDetailModel_mike());
            }

            vm.ExcerciseSummary_mike = AssessmentDAL.GetExcerciseActivitySummary_mike(resident.ID);
            if (vm.ExcerciseSummary_mike.Count == 0)
            {
                vm.ExcerciseSummary_mike = new Collection<ExcerciseActivitySummaryModel_mike>();
                vm.ExcerciseSummary_mike.Add(new ExcerciseActivitySummaryModel_mike());
            }

            List<DateTime> l_AssessmentDates = new List<DateTime>();
            foreach (var l_A in vm.mike)
            {
                l_AssessmentDates.Add(l_A.start_time);
            }
            ViewBag.AssessmentDates = l_AssessmentDates;

            List<DateTime> l_AssessmentDates2 = new List<DateTime>();
            foreach (var l_A in vm.ExcerciseSummary_mike)
            {
                l_AssessmentDates2.Add(l_A.StartTime);
            }
            ViewBag.AssessmentDates2 = l_AssessmentDates2;


            if (index == null || index == "")
            {
                TempData["index"] = "0";
                vm.mike_single = vm.mike[0];
                vm.HSEPDetail_mike_single = vm.HSEPDetail_mike[0];
            }
            else
            {
                TempData["index"] = index;
                vm.mike_single = vm.mike[int.Parse(index)];
                vm.HSEPDetail_mike_single = vm.HSEPDetail_mike[int.Parse(index)];
            }

            if (number == null || number == "")
            {
                TempData["number"] = "0";
                vm.ExcerciseSummary_mike_single = vm.ExcerciseSummary_mike[0];
            }
            else
            {
                TempData["number"] = number;
                vm.ExcerciseSummary_mike_single = vm.ExcerciseSummary_mike[int.Parse(number)];
            }

            TempData.Keep("index");
            TempData.Keep("number");
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

        public ActionResult CarePlan()
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
            return new Rotativa.ViewAsPdf("CarePlan", resident);
            //return new MvcRazorToPdf.PdfActionResult(resident);
        }

        public ActionResult DietAllergyReport()
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

            var l_DietaryAllergyReport = HomeDAL.GetDietaryAllergyReport(home.Id);
            return View(l_DietaryAllergyReport);
            //return new Rotativa.ViewAsPdf("CarePlan", resident);
            //return new MvcRazorToPdf.PdfActionResult(resident);
        }

        public ActionResult SpecialDietReport()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            ViewBag.HomeId = home.Id;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_SpecialDietReport = HomeDAL.GetSpecialDietReport(home.Id);
            return View(l_SpecialDietReport);
            //return new Rotativa.ViewAsPdf("CarePlan", resident);
            //return new MvcRazorToPdf.PdfActionResult(resident);
        }

        public ActionResult LikesReport()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            ViewBag.HomeId = home.Id;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_SpecialDietReport = HomeDAL.GetLikesReport(home.Id);
            return View(l_SpecialDietReport);
            //return new Rotativa.ViewAsPdf("CarePlan", resident);
            //return new MvcRazorToPdf.PdfActionResult(resident);
        }

        public ActionResult DislikesReport()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            ViewBag.HomeId = home.Id;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_SpecialDietReport = HomeDAL.GetDisLikesReport(home.Id);
            return View(l_SpecialDietReport);
            //return new Rotativa.ViewAsPdf("CarePlan", resident);
            //return new MvcRazorToPdf.PdfActionResult(resident);
        }
    }
}