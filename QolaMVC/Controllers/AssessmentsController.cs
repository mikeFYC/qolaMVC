using QolaMVC.DAL;
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

        public ActionResult PostFallClinicalMonitoring_A(int? Id)
        {
            var resident = (ResidentModel)TempData["Resident"];

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
            var resident = (ResidentModel)TempData["Resident"];

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
            var resident = (ResidentModel)TempData["Resident"];

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
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            
            return View(l_Activity.LastOrDefault());
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

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            return RedirectToAction("Activity");
        }
    }
}