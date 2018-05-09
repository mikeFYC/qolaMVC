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
    public class AssessmentsController : Controller
    {
        // GET: Assessments
        public ActionResult Index()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View();
        }

        public ActionResult BowelMovement()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            if(TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;
            ViewBag.ReturnUrl = "/Assessments/BowelMovement";

            var bowelMovement = new BowelMovementModel();
            bowelMovement.Resident = resident;
            bowelMovement.EnteredBy = user;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(bowelMovement);
        }

        [HttpPost]
        public ActionResult AddBowelMovement(BowelMovementModel p_BowelMovement, string p_ReturnUrl)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            
            p_BowelMovement.Resident = resident;
            p_BowelMovement.EnteredBy = user;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            AssessmentDAL.AddNewBowelMovement(p_BowelMovement);
            TempData["Message"] = "Added Bowel Assessment";
            return Redirect(p_ReturnUrl);
        }

        public ActionResult DietaryHistory()
        {
            return View();
        }

        public ActionResult ExerciseActivity()
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
            
            if(vm.Detail.Count == 0 || vm.HSEPDetail.Count == 0)
            {
                QolaCulture.InitExcerciseActivity(ref vm);
            }
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(vm);
        }

        public ActionResult AddExcerciseActivity(ExcerciseActivityViewModel vm)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            foreach (var d in vm.Detail)
            {
                d.DateEntered = DateTime.Now;
                d.EnteredBy = user;
                d.Resident = resident;
                AssessmentDAL.AddExcerciseActivityDetail(d);
            }
            
            foreach (var hs in vm.HSEPDetail)
            {
                hs.EnteredBy = user;
                hs.Resident = resident;
                AssessmentDAL.AddHSEPDetail(hs);
            }

            vm.ExcerciseSummary.Resident = resident;
            vm.ExcerciseSummary.DateEntered = DateTime.Now;
            vm.ExcerciseSummary.EnteredBy = user;
            AssessmentDAL.AddExcerciseActivitySummary(vm.ExcerciseSummary);

            return RedirectToAction("ExerciseActivity");
        }
        public ActionResult FamilyConference()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            var familyConference = AssessmentDAL.GetFamilyConferenceNotes(resident.ID);

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            if(familyConference.Count == 0)
            {
                familyConference.Add(new FamilyConfrenceNoteModel());
            }
            return View(familyConference.LastOrDefault());
        }

        [HttpPost]
        public ActionResult AddFamilyConferenceNote(FamilyConfrenceNoteModel p_FamilyConferenceNote)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            p_FamilyConferenceNote.Resident = resident;
            p_FamilyConferenceNote.EnteredBy = user;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            AssessmentDAL.AddFamilyConferenceNote(p_FamilyConferenceNote);
            TempData["Message"] = "Added Family Conference Note";
            return RedirectToAction("FamilyConference");
        }

        public ActionResult HeadToToeAssessment()
        {
            return View();
        }

        public ActionResult HSEPTracking()
        {
            return View();
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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View();
        }

        [HttpPost]
        public ActionResult AddProgressNote(ProgressNotesModel p_Model)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            
            p_Model.Resident = resident;
            p_Model.ModifiedBy = user;
            p_Model.ModifiedOn = DateTime.Now;
            
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ProgressNotesDAL.AddNewProgressNotes(p_Model);
            TempData["Message"] = "Progress not added successfully";

            return Redirect("/Home/ResidentMenu/?p_ResidentId="+resident.ID);
        }

        public ActionResult UnusualIncident()
        {
            return View();
        }
    }
}