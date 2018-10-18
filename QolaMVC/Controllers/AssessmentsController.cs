﻿using QolaMVC.DAL;
using QolaMVC.EF_DAL;
using QolaMVC.Helpers;
using QolaMVC.Models;
using QolaMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script;
using System.Web.Script.Serialization;

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

            TempData["archive"] = "NO";

            return View();
        }

        public ActionResult Index2()
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

            TempData["archive"] = "YES";

            return View("Index");
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
            DataErrorInfoModelValidatorProvider dataErrorInfoModelValidatorProvider = new DataErrorInfoModelValidatorProvider();
            
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            AssessmentDAL.AddNewBowelMovement(p_BowelMovement);
            TempData["Message"] = "Added Bowel Assessment";
            return Redirect(p_ReturnUrl);
        }

        public ActionResult DietaryHistory()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;


            //var l_DietaryAssessment = AssessmentDAL.GetResidentDietaryAssesments(resident.ID);
            //if (l_DietaryAssessment.Count == 0)
            //{
            //    l_DietaryAssessment = new Collection<nDietaryAssessmentModel>();
            //    l_DietaryAssessment.Add(new nDietaryAssessmentModel());
            //}
            //List<DateTime> l_AssessmentDates = new List<DateTime>();
            //foreach (var l_A in l_DietaryAssessment)
            //{
            //    l_AssessmentDates.Add(l_A.DateEntered);
            //}
            //ViewBag.AssessmentDates = l_AssessmentDates;
            //return View(l_DietaryAssessment.LastOrDefault());

            return View();
        }

        public ActionResult DietaryHistory2(int p_ResidentId)
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
            return RedirectToAction("DietaryHistory");
        }



        #region EXERCISE ACTIVITY

        public ActionResult ExerciseActivity(string index,string number)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            var vm = new ExcerciseActivityViewModel();
            //vm.Detail = AssessmentDAL.GetExcerciseActivityDetail(resident.ID);
            //vm.ExcerciseSummary = AssessmentDAL.GetExcerciseActivitySummary(resident.ID);
            //vm.HSEPDetail = AssessmentDAL.GetHSEPDetail(resident.ID);
            //if(vm.Detail.Count == 0 || vm.HSEPDetail.Count == 0)
            //{
            //    QolaCulture.InitExcerciseActivity(ref vm);
            //}
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");



            vm.mike= AssessmentDAL.getmike(resident.ID);
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

        [HttpPost]
        public ActionResult UpdateExcerciseActivity_mike(ExcerciseActivityViewModel vm,string HSEP, string FORM)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            if (FORM != null)
            {
                vm.mike_single.DateEntered = DateTime.Now;
                vm.mike_single.EnteredBy = user.ID;
                vm.mike_single.EnteredByName = user.Name;
                vm.mike_single.Residentid = resident.ID;
                vm.mike_single.ResidentName = resident.FirstName + " " + resident.LastName;
                vm.mike_single.SuiteNumber = resident.SuiteNo;
                AssessmentDAL.updateExcerciseActivity_mike(vm.mike_single);
            }
            else if (HSEP != null)
            {
                vm.HSEPDetail_mike_single.EnteredBy = user.ID;
                vm.HSEPDetail_mike_single.Residentid = resident.ID;
                AssessmentDAL.UpdateHSEPDetail_mike(vm.HSEPDetail_mike_single, user.FirstName+" "+user.LastName);
            }

            //vm.ExcerciseSummary.Resident = resident;
            //vm.ExcerciseSummary.DateEntered = DateTime.Now;
            //vm.ExcerciseSummary.EnteredBy = user;
            //AssessmentDAL.AddExcerciseActivitySummary(vm.ExcerciseSummary);

            var ind = TempData["index"];
            var num = TempData["number"];

            return RedirectToAction("ExerciseActivity", new { index = ind , number = num});
        }

        public ActionResult Add_ExcerciseActivity_mike()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            DateTime sameTime = DateTime.Now;
            AssessmentDAL.ADDExcerciseActivity_mike(resident.ID,user.ID, sameTime);
            AssessmentDAL.ADDHSEPDetail_mike(resident.ID, user.ID, sameTime);
            string ind = TempData["index"].ToString();
            string num = TempData["number"].ToString();
            return RedirectToAction("ExerciseActivity", new { index = 0, number = num });
        }

        public ActionResult Add_ExcerciseSummary_mike()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            DateTime sameTime = DateTime.Now;
            AssessmentDAL.Add_ExcerciseSummary_mike(resident.ID, user.ID, sameTime);
            string ind=TempData["index"].ToString();
            string num=TempData["number"].ToString();
            return RedirectToAction("ExerciseActivity" , new { index = ind, number=0 });
        }

        public ActionResult TUG_CLICK(string ID,string number)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            string ind = TempData["index"].ToString();
            string num = TempData["number"].ToString();
            TempData.Keep("index");
            TempData.Keep("number");
            return View();
        }

        public ActionResult VPS_CLICK(string ID, string number)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            string ind = TempData["index"].ToString();
            string num = TempData["number"].ToString();
            TempData.Keep("index");
            TempData.Keep("number");
            return View();
        }

        #endregion



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

            List<DateTime> l_AssessmentDates = new List<DateTime>();
            foreach (var l_Ass in familyConference)
            {
                l_AssessmentDates.Add(l_Ass.Date);
            }

            ViewBag.AssessmentDates = l_AssessmentDates;

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
            var l_Home = (HomeModel)TempData["Home"];
            var l_User = (UserModel)TempData["User"];
            var l_Resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = l_User;
            ViewBag.Home = l_Home;
            ViewBag.Resident = l_Resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_Assessments = AssessmentDAL.GetAdmissionHeadToToe(l_Resident.ID);

            List<DateTime> l_AssessmentDates = new List<DateTime>();
            foreach( var l_Ass in l_Assessments)
            {
                l_AssessmentDates.Add(l_Ass.DateEntered);
            }

            ViewBag.AssessmentDates = l_AssessmentDates;
            if(l_Assessments.Count == 0)
            {
                l_Assessments.Add(new AdmissionHeadToToeModel());
            }
            return View(l_Assessments.LastOrDefault());
        }

        [HttpPost]
        public ActionResult HeadToToeAssessment(FormCollection p_Form)
        {
            var l_Home = (HomeModel)TempData["Home"];
            var l_User = (UserModel)TempData["User"];
            var l_Resident = (ResidentModel)TempData["Resident"];

            DateTime l_DateEntered = Convert.ToDateTime(Request.Form["DateEntered"]);

            ViewBag.User = l_User;
            ViewBag.Home = l_Home;
            ViewBag.Resident = l_Resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_Assessments = AssessmentDAL.GetAdmissionHeadToToe(l_Resident.ID);

            List<DateTime> l_AssessmentDates = new List<DateTime>();
            foreach (var l_Ass in l_Assessments)
            {
                l_AssessmentDates.Add(l_Ass.DateEntered);
            }

            ViewBag.AssessmentDates = l_AssessmentDates;
            return View(l_Assessments.Where(m => m.DateEntered == l_DateEntered).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult AddHeadToToeAssessment(AdmissionHeadToToeModel p_Model)
        {
            var l_Home = (HomeModel)TempData["Home"];
            var l_User = (UserModel)TempData["User"];
            var l_Resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = l_User;
            ViewBag.Home = l_Home;
            ViewBag.Resident = l_Resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            p_Model.DateEntered = DateTime.Now;
            p_Model.Date = DateTime.Now;
            p_Model.EnteredBy = l_User;
            p_Model.Resident = l_Resident;

            AssessmentDAL.AddAdmissionHeadToToe(p_Model);
            return RedirectToAction("HeadToToeAssessment");
        }

        public ActionResult HSEPTracking()
        {
            return View();
        }

        public ActionResult PostFallClinicalMonitoring_A(int? Id)
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

            if (TempData["clinicaldetails"] != null)
            {
                var details = (List<PostFallClinicalMonitoringModel>)TempData["clinicaldetails"];
                TempData.Keep("clinicaldetails");
                return View(details);
            }
            else
            {
                Id = resident.ID;
                string residentId = Convert.ToString(Id);
                PostFallClinicalMonitoringModel postdetails = new PostFallClinicalMonitoringModel();
                var postFallDetails = AssessmentDAL.GetPostFall(Id, "a", DateTime.Now.ToShortDateString());
                TempData.Keep("Resident");
                return View(postFallDetails);
            }
          
        }

        [HttpPost]
        public ActionResult FindAssessment(int? Id, string category, string date_created)
        {
            var resident = (ResidentModel)TempData["Resident"];

            var postFallDetails = AssessmentDAL.GetPostFall(resident.ID, category, date_created);
            TempData["clinicaldetails"] = postFallDetails;
            TempData.Keep("Resident");
            return RedirectToAction("PostFallClinicalMonitoring_A");
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostFallClinicalMonitoring_A(VitalSignsModel vitalSigns, PostFallClinicalMonitoringModel monitoringDetails, int? Id)
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

            //using (var dbContext = new test_qolaEntities())
            //{
            //    var residentId = Convert.ToString(Id);
            //    var residentId = "1234";
            //    var getVitalsign = dbContext.tbl_PostfallClinicalMonitoringVitalSigns.SingleOrDefault(m => m.residentid == residentId && (m.category.ToLower() == "a" && m.vitalsign.ToLower() == vitalSigns.vitalsign.ToLower()));
            //    if (getVitalsign == null)
            //    {
            //        vitalSigns.residentid = "1234";
            //        vitalSigns.date_created = DateTime.Now.ToShortDateString();
            //        dbContext.tbl_PostfallClinicalMonitoringVitalSigns.Add(vitalSigns);
            //        dbContext.tbl_PostfallClinicalMonitoringDetails.Add(monitoringDetails);
            //    }
            //    else
            //    {
            //        var getDetails = dbContext.tbl_PostfallClinicalMonitoringDetails.SingleOrDefault(m => m.category.ToLower() == monitoringDetails.category && m.tbl_PostfallClinicalMonitoringVitalSigns.Id == getVitalsign.Id);
            //        getDetails.firstcheck = monitoringDetails.firstcheck;
            //        getDetails.fourtyeighthoursfifthcheck = monitoringDetails.fourtyeighthoursfifthcheck;
            //        getDetails.fourtyeighthoursfirstcheck = monitoringDetails.fourtyeighthoursfirstcheck;
            //        getDetails.fourtyeighthoursfourthcheck = monitoringDetails.fourtyeighthoursfourthcheck;
            //        getDetails.fourtyeighthourssecondcheck = monitoringDetails.fourtyeighthourssecondcheck;
            //        getDetails.fourtyeighthoursthirdcheck = monitoringDetails.fourtyeighthoursthirdcheck;
            //        getDetails.onehourfirstcheck = monitoringDetails.onehourfirstcheck;
            //        getDetails.onehoursecondcheck = monitoringDetails.onehoursecondcheck;
            //        getDetails.threehoursfirstcheck = monitoringDetails.threehoursfirstcheck;
            //        getDetails.threehourssecondcheck = monitoringDetails.threehourssecondcheck;
            //        getDetails.threehoursthirdcheck = monitoringDetails.threehoursthirdcheck;

            //    }

            //    dbContext.SaveChanges();
            //}
            AssessmentDAL.AddPostFall(monitoringDetails, vitalSigns, resident.ID);
            TempData.Keep("Resident");
            return RedirectToAction("PostFallClinicalMonitoring_A");
        }


        public ActionResult PostFallClinicalMonitoring_B(int? Id)
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

            if (TempData["clinicaldetailsb"] != null)
            {
                var details = (List<PostFallClinicalMonitoringModel>)TempData["clinicaldetailsb"];
                TempData.Keep("clinicaldetailsb");
                return View(details);
            }
            else
            {
                Id = resident.ID;
                string residentId = Convert.ToString(Id);
                PostFallClinicalMonitoringModel postdetails = new PostFallClinicalMonitoringModel();
                var postFallDetails = AssessmentDAL.GetPostFall(Id, "b", DateTime.Now.ToShortDateString());
                TempData.Keep("Resident");
                return View(postFallDetails);
            }

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostFallClinicalMonitoring_B(VitalSignsModel vitalSigns, PostFallClinicalMonitoringModel monitoringDetails, int? Id)
        {
            var resident = (ResidentModel)TempData["Resident"];
            AssessmentDAL.AddPostFall(monitoringDetails, vitalSigns, resident.ID);
            TempData.Keep("Resident");
            
            return RedirectToAction("PostFallClinicalMonitoring_B");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostFallClinicalMonitoring_BPage2(tbl_postfallclinicalmonitoringBpage2 model)
        {
            var residentId = "1234";
            using (var dbContext = new test_qolaEntities())
            {
                var resident = dbContext.tbl_postfallclinicalmonitoringBpage2.SingleOrDefault(m => m.residentid == residentId && m.category.ToLower() == model.category.ToLower());
                if (resident == null)
                {
                    model.residentid = residentId;
                    dbContext.tbl_postfallclinicalmonitoringBpage2.Add(model);
                }
                else
                {
                    var updatepage2 = UpdateAssessmentPage2(model, resident);
                }
               
                dbContext.SaveChanges();
                //dbContext.
            }
            return View();
        }

        public tbl_postfallclinicalmonitoringBpage2 UpdateAssessmentPage2(tbl_postfallclinicalmonitoringBpage2 model, tbl_postfallclinicalmonitoringBpage2 residentdata)
        {
            residentdata.abnormalareasdescription = model.abnormalareasdescription;
            residentdata.adlsdescription = model.adlsdescription;
            residentdata.bowelsoundsheard = model.bowelsoundsheard;
            residentdata.bruising = model.bruising;
            residentdata.category = model.category;
            residentdata.chest = model.chest;
            residentdata.chestother = model.chestother;
            residentdata.completedby = model.completedby;
            residentdata.date_completed = model.date_completed;
            residentdata.edmafeet = model.edmafeet;
            residentdata.edmafeetdescription = model.edmafeetdescription;
            residentdata.edmafeetposition = model.edmafeetposition;
            residentdata.hands = model.hands;
            residentdata.handsdescription = model.handsdescription;
            residentdata.handsother = model.handsother;
            residentdata.handsotherdescription = model.handsotherdescription;
            residentdata.handsposition = model.handsposition;
            residentdata.heartsounds = model.heartsounds;
            residentdata.heartsoundsdescribe = model.heartsoundsdescribe;
            residentdata.ispainsurfacewithdl = model.ispainsurfacewithdl;
            residentdata.lastbowelmovement = model.lastbowelmovement;
            residentdata.longsounds = model.longsounds;
            residentdata.longsoundsdescribe = model.longsoundsdescribe;
            residentdata.longsoundsequal = model.longsoundsequal;
            residentdata.openareas = model.openareas;
            residentdata.painful = model.painful;
            residentdata.painfuldescribe = model.painfuldescribe;
            residentdata.painlocation = model.painlocation;
            residentdata.painother = model.painother;
            residentdata.painscale = model.painscale;
            residentdata.painwhatmakesitbetter = model.painwhatmakesitbetter;
            residentdata.painwhatmakesitworse = model.painwhatmakesitworse;
            residentdata.p_qaching = model.p_qaching;
            residentdata.p_qdull = model.p_qdull;
            residentdata.p_qradiating = model.p_qradiating;
            residentdata.p_qsharp = model.p_qsharp;
            residentdata.rashes = model.rashes;
            residentdata.redness = model.redness;
            residentdata.residentishavingpain = model.residentishavingpain;
            residentdata.residentpaindescription = model.residentpaindescription;
            residentdata.skinfeet = model.skinfeet;
            residentdata.skinfeetdescription = model.skinfeetdescription;
            residentdata.soft = model.soft;
            residentdata.softdescribe = model.softdescribe;
            residentdata.voiding = model.voiding;
            residentdata.voidingdesciption = model.voidingdesciption;
            residentdata.voidingincontinentpadsused = model.voidingincontinentpadsused;
            residentdata.voidingiscather = model.voidingiscather;
            residentdata.voidingiscontinent = model.voidingiscontinent;
            residentdata.wonddescription = model.wonddescription;
            residentdata.wound = model.wound;

            return residentdata;
        }

        public ActionResult ProgressNotes(string p_ResidentId)
        {
            if (p_ResidentId != null)
            {
                var resident = ResidentsDAL.GetResidentById(int.Parse(p_ResidentId));
                var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");
                ViewBag.Message = TempData["Message"];
                TempData["Resident"] = resident;
                TempData.Keep("Resident");
                ViewBag.Resident = resident;
                ViewBag.ProgressNotes = progressNotes;
                ProgressNotesHelper.RegisterSession(resident);
            }
            else
            {
                var resident = (ResidentModel)TempData["Resident"];
                ViewBag.Resident = resident;
                TempData.Keep("Resident");
            }

            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            //var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            //ViewBag.Resident = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            //TempData.Keep("Resident");

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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_IncidentReports = AssessmentDAL.GetUnusualIncidentReports(resident.ID);
            List<DateTime> l_Dates = l_IncidentReports.Select(m => m.DateEntered).ToList();
            ViewBag.AssessmentDates = l_Dates;

            var model = new UnusualIncidentModel();
            model.SectionG = new Collection<UnusualIncidentSectionGModel>();
            var l_Array = new string[]{ "Physician", "Family", "Alberta Health Services", "On Call Manager", "Director of Care", "Maintenance", "General Service Mgr", "Senior Management", "Other" };

            foreach(var l_Item in l_Array)
            {
                var l_SectionG = new UnusualIncidentSectionGModel();
                l_SectionG.Notify = l_Item;
                l_SectionG.Name = string.Empty;
                l_SectionG.IncidentId = 0;
                l_SectionG.ResidentId = resident.ID;
                l_SectionG.Via = string.Empty;

                model.SectionG.Add(l_SectionG);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UnusualIncident(FormCollection collection)
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

            var l_IncidentReports = AssessmentDAL.GetUnusualIncidentReports(resident.ID);

            List<DateTime> l_Dates = l_IncidentReports.Select(m => m.DateEntered).ToList();

            ViewBag.AssessmentDates = l_Dates;
            DateTime l_DateEntered = Convert.ToDateTime(collection["DateEntered"]);

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var model = l_IncidentReports.FirstOrDefault(m2 => m2.DateEntered.Date == l_DateEntered.Date && m2.DateEntered.Hour == l_DateEntered.Hour && m2.DateEntered.Minute == l_DateEntered.Minute);
            if (model == null)
            {
                model = new UnusualIncidentModel();
                var l_Array = new string[] { "Physician", "Family", "Alberta Health Services", "On Call Manager", "Director of Care", "Maintenance", "General Service Mgr", "Senior Management", "Other" };

                foreach (var l_Item in l_Array)
                {
                    var l_SectionG = new UnusualIncidentSectionGModel();
                    l_SectionG.Notify = l_Item;
                    l_SectionG.Name = string.Empty;
                    l_SectionG.IncidentId = 0;
                    l_SectionG.ResidentId = resident.ID;
                    l_SectionG.Via = string.Empty;

                    model.SectionG.Add(l_SectionG);
                }
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddUnusualIncident(UnusualIncidentModel p_Model)
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

            p_Model.EnteredBy = user;
            p_Model.Resident = resident;
            p_Model.DateEntered = DateTime.Now;

            AssessmentDAL.AddUnusualIncident(p_Model);

            return RedirectToAction("UnusualIncident");
        }

        //public ActionResult CarePlan()
        //{
        //    var home = (HomeModel)TempData["Home"];
        //    var user = (UserModel)TempData["User"];
        //    var resident_sess = (ResidentModel)TempData["Resident"];

        //    ViewBag.User = user;
        //    ViewBag.Resident = resident_sess;
        //    ViewBag.Home = home;


        //    TempData.Keep("User");
        //    TempData.Keep("Home");
        //    TempData.Keep("Resident");

        //    var careplan = AssessmentDAL.GetCarePlan("1234");

        //    return View(careplan);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CarePlan(CarePlan_VitalSignModel model)
        //{
        //    //var resident_sess = (ResidentModel)TempData["Resident"];

        //    //resident_sess.ID = 1234;

        //    AssessmentDAL.AddCarePlan(model, "1234");

        //    TempData.Keep("Resident");

        //    return RedirectToAction("CarePlan");

        //}

        public ActionResult CarePlan()
        {
            //care plan
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var careplan = CarePlanDAL.GetResidentsPlanOfCare(resident.ID);
            PlanOfCareModel l_Model = new PlanOfCareModel();

            var l_PersonalHygiene = new CarePlanPersonalHygieneModel();
            l_PersonalHygiene.PreferredDaysCollection = new Collection<QOLACheckboxModel>();
            QolaCulture.InitPreferredDays(ref l_PersonalHygiene);
            l_Model.PersonalHygiene = l_PersonalHygiene;

            var l_AssistanceWith = new CarePlanAssistanceWithModel();
            l_AssistanceWith.TeethCollection = new Collection<QOLACheckboxModel>();
            QolaCulture.InitAssistanceWithTeeth(ref l_AssistanceWith);
            l_Model.AssistanceWith = l_AssistanceWith;

            var l_Behaviour = new CarePlanBehaviourModel();
            l_Behaviour.BehaviourCollection = new Collection<QOLACheckboxModel>();
            QolaCulture.InitBehaviour(ref l_Behaviour);
            l_Model.Behaviour = l_Behaviour;

            var l_CognitiveFunction = new CarePlanCognitiveFunctionModel();
            l_CognitiveFunction.CognitiveFunction = new Collection<QOLACheckboxModel>();
            QolaCulture.InitCognitiveFunction(ref l_CognitiveFunction);
            l_Model.CognitiveFunction = l_CognitiveFunction;

            var l_Nutrition = new CarePlanNutritionModel();
            l_Nutrition.Diet = new Collection<QOLACheckboxModel>();
            QolaCulture.InitNutrition(ref l_Nutrition);
            l_Model.Nutrition = l_Nutrition;

            var l_Elimination = new CarePlanEliminationModel();
            l_Elimination.Bladder = new Collection<QOLACheckboxModel>();
            l_Elimination.Bowel = new Collection<QOLACheckboxModel>();
            QolaCulture.InitElimination(ref l_Elimination);
            l_Model.Elimination = l_Elimination;

            var l_Toilet = new CarePlanToiletingModel();
            l_Toilet.Bathroom = new Collection<QOLACheckboxModel>();
            l_Toilet.Commode = new Collection<QOLACheckboxModel>();
            l_Toilet.Bedpan = new Collection<QOLACheckboxModel>();
            QolaCulture.InitToileting(ref l_Toilet);
            l_Model.Toileting = l_Toilet;

            var l_Sensory = new CarePlanSensoryAbilitiesModel();
            l_Sensory.Vision = new Collection<QOLACheckboxModel>();
            l_Sensory.Hearing = new Collection<QOLACheckboxModel>();
            l_Sensory.Communication = new Collection<QOLACheckboxModel>();
            QolaCulture.InitSensoryAbilities(ref l_Sensory);
            l_Model.SensoryAbilities = l_Sensory;

            var l_SpecialEquipment = new CarePlanSpecialEquipmentModel();
            l_SpecialEquipment.SpecialEquipment = new Collection<QOLACheckboxModel>();
            QolaCulture.InitSpecialEquipment(ref l_SpecialEquipment);
            l_Model.SpecialEquipment = l_SpecialEquipment;

            if (careplan.Count >0)
            {
                l_Model = careplan.LastOrDefault();
            }
            return View(l_Model);
        }

        public ActionResult CarePlan2(int p_ResidentId)
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
            return RedirectToAction("CarePlan");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarePlan(PlanOfCareModel p_Model)
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

            p_Model.EnteredBy = user;
            p_Model.Resident = resident;
            p_Model.DateEntered = DateTime.Now;

            CarePlanDAL.AddCarePlan(p_Model);
            return RedirectToAction("CarePlan");
        }

        public ActionResult SpecificGoals()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident_sess = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident_sess;
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
            var l_Activity = MasterDAL.GetActivityAssessments(resident.ID);
            if(l_Activity.Count == 0)
            {
                l_Activity = MasterDAL.InitActivityAssessments();
            }
            List<DateTime> l_AssessmentDates = new List<DateTime>();
            foreach(var l_A in l_Activity)
            {
                l_AssessmentDates.Add(l_A.DateEntered);
            }
            ViewBag.AssessmentDates = l_AssessmentDates;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            
            return View(l_Activity.LastOrDefault());
        }

        public ActionResult Activity2(int p_ResidentId)
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
            return RedirectToAction("Activity");
        }

        [HttpPost]
        public ActionResult Activity(FormCollection p_Form)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            var l_Activity = MasterDAL.GetActivityAssessments(resident.ID);
            if (l_Activity.Count == 0)
            {
                l_Activity = MasterDAL.InitActivityAssessments();
            }

            List<DateTime> l_AssessmentDates = new List<DateTime>();
            foreach (var l_A in l_Activity)
            {
                l_AssessmentDates.Add(l_A.DateEntered);
            }

            ViewBag.AssessmentDates = l_AssessmentDates;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            DateTime l_Date = DateTime.Now;
            ActivityAssessmentCollectionViewModel model = new ActivityAssessmentCollectionViewModel();

            if (Request.Form["AssessmentDate"] == "new")
            {
                model = MasterDAL.InitActivityAssessments().LastOrDefault();
            }
            else
            {
                l_Date = Convert.ToDateTime(Request.Form["AssessmentDate"]);
                model = l_Activity.FirstOrDefault(m2 => m2.DateEntered.Date == l_Date.Date && m2.DateEntered.Hour == l_Date.Hour && m2.DateEntered.Minute == l_Date.Minute);
            }

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitActivityAssessment(ActivityAssessmentCollectionViewModel p_Model)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            MasterDAL.AddActivityAssessments(resident.ID, user.ID, p_Model);

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return RedirectToAction("Activity");
        }

        public ActionResult FallRisk()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            var l_FallRisk = AssessmentDAL.GetFallRiskAssessment(resident.ID);

            if (l_FallRisk.Count == 0)
            {
                l_FallRisk = new Collection<FallRiskAssessmentModel>();
                l_FallRisk.Add(new FallRiskAssessmentModel());
            }

            List<DateTime> l_AssessmentDates = new List<DateTime>();

            foreach (var l_A in l_FallRisk)
            {
                l_AssessmentDates.Add(l_A.DateEntered);
            }

            ViewBag.AssessmentDates = l_AssessmentDates;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return View(l_FallRisk.LastOrDefault());
        }

        public ActionResult FallRisk2(int p_ResidentId)
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
            return RedirectToAction("FallRisk");
        }


        [HttpPost]
        public ActionResult FallRisk(FormCollection p_Form)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            var l_FallRisk = AssessmentDAL.GetFallRiskAssessment(resident.ID);

            if (l_FallRisk.Count == 0)
            {
                l_FallRisk = new Collection<FallRiskAssessmentModel>();
                l_FallRisk.Add(new FallRiskAssessmentModel());
            }

            List<DateTime> l_AssessmentDates = new List<DateTime>();

            foreach (var l_A in l_FallRisk)
            {
                l_AssessmentDates.Add(l_A.DateEntered);
            }

            ViewBag.AssessmentDates = l_AssessmentDates;
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            DateTime l_Date = DateTime.Now;
            FallRiskAssessmentModel model = new FallRiskAssessmentModel();

            if (Request.Form["AssessmentDate"] != "new")
            {
                l_Date = Convert.ToDateTime(Request.Form["AssessmentDate"]);
                model = l_FallRisk.FirstOrDefault(m2 => m2.DateEntered.Date == l_Date.Date && m2.DateEntered.Hour == l_Date.Hour && m2.DateEntered.Minute == l_Date.Minute);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddFallRiskAssessment(FallRiskAssessmentModel p_Model)
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

            p_Model.EnteredBy = user.ID;
            p_Model.ResidentId = resident.ID;
            p_Model.DateEntered = DateTime.Now;

            AssessmentDAL.AddFallRiskAssessment(p_Model);
            return RedirectToAction("FallRisk");
        }



       
        [HttpGet]
        public string saveButtonMain(string [] arrayMain)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            for(int a = 0; a < arrayMain.Length; a++)
            {
                if (arrayMain[a]== "checked")
                {
                    MasterDAL.save_button(a+1, user.ID, resident.ID);
                }
                else if (arrayMain[a] == "unchecked")
                {
                    MasterDAL.save_button(a+1, resident.ID);
                }
            }
            string str = MasterDAL.get_checklist(resident.ID);
            return str;
        }


        [HttpGet]
        public string get_checklist_data()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            string str = MasterDAL.get_checklist(resident.ID);
            return str;
        }

        public ActionResult PostfallClinicalMonitoringA(int linkid=0)
        {
            Postfall_Clinial_Monitoring_PartDAL objDet = new Postfall_Clinial_Monitoring_PartDAL();  
             MasterDetails CustData = new MasterDetails(); 
List<MasterDetails> MasterData = objDet.GetPostfall_clinial_monitoring_details_a1_by_id(linkid,"A").ToList();  
            CustData.A1Model = MasterData[0].A1Model;  
            CustData.SplitMonitoring = MasterData[0].SplitMonitoring; 
                return View(CustData); 
            //return View();

        }

   [HttpPost]
          public string PostfallClinicalMonitoringA(MasterDetails data,string c_c,string edema_hands1,string edema_feet1,int linkid)
        {
               
                 Postfall_Clinial_Monitoring_PartDAL.AddPartAPage2(data,c_c, edema_hands1, edema_feet1, linkid);
                //return RedirectToAction("/");
         
            return "true"; 
  
        }
          
         public JsonResult GetPostfallPage2PartB (int linkid)
        {
             MasterDetails l_model=Postfall_Clinial_Monitoring_PartDAL.GetPartAPage2(linkid);
             
             return Json(l_model, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PostfallClinicalMonitoringB(int linkid=0)
        {
            Postfall_Clinial_Monitoring_PartDAL objDet = new Postfall_Clinial_Monitoring_PartDAL();  
             MasterDetails CustData = new MasterDetails(); 
List<MasterDetails> MasterData = objDet.GetPostfall_clinial_monitoring_details_a1_by_id(linkid,"B").ToList();  
            CustData.A1Model = MasterData[0].A1Model;  
            CustData.SplitMonitoring = MasterData[0].SplitMonitoring; 
return View(CustData); 
        }

   [HttpPost]
   [WebMethod]
   public string PostfallClinicalMonitoringA1(string[][] list1,string[][] list2,string[][] list3)
   {
     string result = string.Empty;
       try
       {
           Random rnd=new Random();
           int linkid=(int)rnd.Next(9999);
           DataTable dt1=dataTable1(list1, linkid, 1);
           DataTable dt2=dataTable1(list2, linkid, 2);
           DataTable dt3=dataTable1(list3, linkid, 3);
            
             result=Postfall_Clinial_Monitoring_PartDAL.AddNewPostfall_clinial_monitoring_partA1("sp_add_new_tbl_postfall_clinial_monitoring_details_a1", "A", linkid, dt1, dt2, dt3);
             //return RedirectToAction("List");
             return result;
//return Convert.ToString(list2);
}
       catch (Exception ex)
       {
           result = ex.Message;
            return "d  dsrd "+result;
       }
   }
   [HttpPost]
   [WebMethod]
   public string EditPostfallClinicalMonitoringA(string[][] list1,string[][] list2,string[][] list3,string[] tempid)
   {
     string result = string.Empty;
       try
       {
           DataTable dt1=dataTable1(list1, Int32.Parse(tempid[0]), 1);
           DataTable dt2=dataTable1(list2, Int32.Parse(tempid[0]), 2);
           DataTable dt3=dataTable1(list3, Int32.Parse(tempid[0]), 3);
            
             result=Postfall_Clinial_Monitoring_PartDAL.EditNewPostfall_clinial_monitoring_partA1("sp_update_new_tbl_postfall_clinial_monitoring_details_a1", tempid[0], dt1, dt2, dt3);
             //return RedirectToAction("List");
             return result;
//return Convert.ToString(list2);
}
       catch (Exception ex)
       {
           result = ex.Message;
            return "d  dsrd "+result;
       }
   }
   [HttpPost]
   [WebMethod]
   public string PostfallClinicalMonitoringB(string[][] list1)
   {
     string result = string.Empty;
       try
       {
           Random rnd=new Random();
           int linkid=(int)rnd.Next(9999);
           DataTable dt1=dataTable1(list1, linkid, 1);
            
             result=Postfall_Clinial_Monitoring_PartDAL.AddNewPostfall_clinial_monitoring_partB1("sp_add_new_tbl_postfall_clinial_monitoring_details_b1", "B", linkid, dt1);
             //return RedirectToAction("List");
             return result;
}
       catch (Exception ex)
       {
           result = ex.Message;
            return "d  dsrd "+result;
       }
   }
   [HttpPost]
   [WebMethod]
   public string EditPostfallClinicalMonitoringB(string[][] list1,string[] tempid)
   {
     string result = string.Empty;
       try
       {
           DataTable dt1=dataTable1(list1, Int32.Parse(tempid[0]), 1);
            
             result=Postfall_Clinial_Monitoring_PartDAL.EditNewPostfall_clinial_monitoring_partB1("sp_update_new_tbl_postfall_clinial_monitoring_details_b1", tempid[0], dt1);
             //return RedirectToAction("List");
             return result;
//return Convert.ToString(list2);
}
       catch (Exception ex)
       {
           result = ex.Message;
            return "d  dsrd "+result;
       }
   }
private DataTable dataTable1(string[][] array, int linkid, int tableid){
           Random rnd=new Random();

           DataTable dt=new DataTable();
           dt.Columns.Add("linkid");
           dt.Columns.Add("tableid");
           dt.Columns.Add("guid");
           dt.Columns.Add("category");
           dt.Columns.Add("firstcheck");
           dt.Columns.Add("onehourfirstcheck");
           dt.Columns.Add("onehoursecondcheck");
           dt.Columns.Add("threehoursfirstcheck");
           dt.Columns.Add("threehourssecondcheck");
           dt.Columns.Add("threehoursthirdcheck");
           dt.Columns.Add("fourtyeighthoursfirstcheck");
           dt.Columns.Add("fourtyeighthourssecondcheck");
           dt.Columns.Add("fourtyeighthoursthirdcheck");
           dt.Columns.Add("fourtyeighthoursfourthcheck");
           dt.Columns.Add("fourtyeighthoursfifthcheck");
               foreach (var arr in array)
           {
               DataRow dr = dt.NewRow();
               //dr["id"] = 1;
               dr["tableid"] = tableid;
               dr["linkid"] = linkid;
               dr["guid"] = (int)rnd.Next(999999);
               dr["category"] = arr[0];
               dr["firstcheck"] = arr[1];
               dr["onehourfirstcheck"] = arr[2];
               dr["onehoursecondcheck"] = arr[3];
               dr["threehoursfirstcheck"] = arr[4];
               dr["threehourssecondcheck"] = arr[5];
               dr["threehoursthirdcheck"] = arr[6];
               dr["fourtyeighthoursfirstcheck"] = arr[7];
               dr["fourtyeighthourssecondcheck"] = arr[8];
               dr["fourtyeighthoursthirdcheck"] = arr[9];
               dr["fourtyeighthoursfourthcheck"] = arr[10];
               dr["fourtyeighthoursfifthcheck"] = arr[11];
               dt.Rows.Add(dr);
}
return dt;
}


    }
}