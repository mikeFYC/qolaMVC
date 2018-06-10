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

            if (TempData["Message"] != null)
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
            ViewBag.Allergies = AssessmentDAL.GetAllergiesCollections();
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            Collection<nDietaryAssessmentModel> vm = new Collection<nDietaryAssessmentModel>();

            vm = AssessmentDAL.GetResidentDietaryAssesments(resident.ID);

            if (vm == null || vm.Count == 0)
            {
                var m = new nDietaryAssessmentModel();
                m.Diet = new System.Collections.ObjectModel.Collection<string>();
                m.Allergies = new System.Collections.ObjectModel.Collection<AllergiesModel>();
                QolaCulture.InitDiets(ref m);
                vm.Add(m);
            }

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            return View(vm);
        }

        [HttpPost]
        public ActionResult DietaryHistory(FormCollection collection)
        {
            ViewBag.Allergies = AssessmentDAL.GetAllergiesCollections();
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            Collection<nDietaryAssessmentModel> vm = new Collection<nDietaryAssessmentModel>();
            vm = AssessmentDAL.GetResidentDietaryAssesments(resident.ID);
            DateTime l_DateEntered = Convert.ToDateTime(collection["DateEntered"]);

           // if (vm == null || vm.Count == 0)
            //{
                var m = new nDietaryAssessmentModel();
                m.Diet = new System.Collections.ObjectModel.Collection<string>();
                m.Allergies = new System.Collections.ObjectModel.Collection<AllergiesModel>();
                QolaCulture.InitDiets(ref m);
                vm.Add(m);
            //}

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            return View(vm.Where(m2 => m2.DateEntered == l_DateEntered));
        }

        [HttpPost]
        public ActionResult AddDietaryAssessment(nDietaryAssessmentModel model)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            model.Resident = resident;
            model.EnteredBy = user;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            AssessmentDAL.AddDietaryAssesment(model);
            TempData["Message"] = "Added Dietary Assessment Note";
            return RedirectToAction("DietaryHistory");
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

            if (vm.Detail.Count == 0 || vm.HSEPDetail.Count == 0)
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
            if (familyConference.Count == 0)
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
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_Assessments = AssessmentDAL.GetAdmissionHeadToToe(resident.ID);
            return View(l_Assessments.LastOrDefault());
        }
        
        [HttpPost]
        public ActionResult AddHeadToToeAssessment(AdmissionHeadToToeModel p_Model)
        {
            try
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

                p_Model.Resident = resident;
                p_Model.EnteredBy = user;
                AssessmentDAL.AddAdmissionHeadToToe(p_Model);
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return RedirectToAction("HeadToToeAssessment");
        }
        public ActionResult HSEPTracking()
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
                var details = (List<tbl_PostfallClinicalMonitoringDetails>)TempData["clinicaldetails"];
                TempData.Keep("clinicaldetails");
                return View(details);
            }
            else
            {
                Id = resident.ID;
                string residentId = Convert.ToString(Id);
                tbl_PostfallClinicalMonitoringDetails postdetails = new tbl_PostfallClinicalMonitoringDetails();
                using (var dbContext = new test_qolaEntities())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    //var qm = dbContext.tbl_PostfallClinicalMonitoringVitalSigns.Include("")
                    var getPostMedicaLClinical = dbContext.tbl_PostfallClinicalMonitoringDetails.Include("tbl_PostfallClinicalMonitoringVitalSigns").Where(m => m.category.ToLower() == "a" && m.tbl_PostfallClinicalMonitoringVitalSigns.residentid == residentId).ToList();
                    if (getPostMedicaLClinical.Count() == 0)
                    {
                        return View();
                    }
                    return View(getPostMedicaLClinical.ToList());
                }
            }

        }

        [HttpPost]
        public ActionResult FindAssessment(string Id, string date_created)
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

            using (var dbContext = new test_qolaEntities())
            {
                var residentId = resident.ID.ToString();
                dbContext.Configuration.LazyLoadingEnabled = false;
                //var qm = dbContext.tbl_PostfallClinicalMonitoringVitalSigns.Include("")
                var getPostMedicaLClinical = dbContext.tbl_PostfallClinicalMonitoringDetails.Include("tbl_PostfallClinicalMonitoringVitalSigns").Where(m => m.category.ToLower() == Id && (m.tbl_PostfallClinicalMonitoringVitalSigns.residentid == residentId && m.tbl_PostfallClinicalMonitoringVitalSigns.date_created == date_created)).ToList();
                TempData["clinicaldetails"] = getPostMedicaLClinical;
                return RedirectToAction("PostFallClinicalMonitoring_A");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostFallClinicalMonitoring_A(tbl_PostfallClinicalMonitoringVitalSigns vitalSigns, tbl_PostfallClinicalMonitoringDetails monitoringDetails, int? Id)
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

            using (var dbContext = new test_qolaEntities())
            {
                //var residentId = Convert.ToString(Id);
                var residentId = resident.ID.ToString();
                var getVitalsign = dbContext.tbl_PostfallClinicalMonitoringVitalSigns.SingleOrDefault(m => m.residentid == residentId && (m.category.ToLower() == "a" && m.vitalsign.ToLower() == vitalSigns.vitalsign.ToLower()));
                if (getVitalsign == null)
                {
                    vitalSigns.residentid = resident.ID.ToString();
                    vitalSigns.date_created = DateTime.Now.ToShortDateString();
                    dbContext.tbl_PostfallClinicalMonitoringVitalSigns.Add(vitalSigns);
                    dbContext.tbl_PostfallClinicalMonitoringDetails.Add(monitoringDetails);
                }
                else
                {
                    var getDetails = dbContext.tbl_PostfallClinicalMonitoringDetails.SingleOrDefault(m => m.category.ToLower() == monitoringDetails.category && m.tbl_PostfallClinicalMonitoringVitalSigns.Id == getVitalsign.Id);
                    getDetails.firstcheck = monitoringDetails.firstcheck;
                    getDetails.fourtyeighthoursfifthcheck = monitoringDetails.fourtyeighthoursfifthcheck;
                    getDetails.fourtyeighthoursfirstcheck = monitoringDetails.fourtyeighthoursfirstcheck;
                    getDetails.fourtyeighthoursfourthcheck = monitoringDetails.fourtyeighthoursfourthcheck;
                    getDetails.fourtyeighthourssecondcheck = monitoringDetails.fourtyeighthourssecondcheck;
                    getDetails.fourtyeighthoursthirdcheck = monitoringDetails.fourtyeighthoursthirdcheck;
                    getDetails.onehourfirstcheck = monitoringDetails.onehourfirstcheck;
                    getDetails.onehoursecondcheck = monitoringDetails.onehoursecondcheck;
                    getDetails.threehoursfirstcheck = monitoringDetails.threehoursfirstcheck;
                    getDetails.threehourssecondcheck = monitoringDetails.threehourssecondcheck;
                    getDetails.threehoursthirdcheck = monitoringDetails.threehoursthirdcheck;

                }

                dbContext.SaveChanges();
            }
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
                var details = (List<tbl_PostfallClinicalMonitoringDetails>)TempData["clinicaldetailsb"];
                TempData.Keep("clinicaldetailsb");
                return View(details);
            }
            else
            {
                Id = resident.ID;
                string residentId = Convert.ToString(Id);
                tbl_PostfallClinicalMonitoringDetails postdetails = new tbl_PostfallClinicalMonitoringDetails();
                using (var dbContext = new test_qolaEntities())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    //var qm = dbContext.tbl_PostfallClinicalMonitoringVitalSigns.Include("")
                    var getPostMedicaLClinical = dbContext.tbl_PostfallClinicalMonitoringDetails.Include("tbl_PostfallClinicalMonitoringVitalSigns").Where(m => m.category.ToLower() == "b" && m.tbl_PostfallClinicalMonitoringVitalSigns.residentid == residentId).ToList();
                    if (getPostMedicaLClinical.Count() == 0)
                    {
                        return View();
                    }
                    return View(getPostMedicaLClinical.ToList());
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostFallClinicalMonitoring_B(tbl_PostfallClinicalMonitoringVitalSigns vitalSigns, tbl_PostfallClinicalMonitoringDetails monitoringDetails, int? Id)
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

            using (var dbContext = new test_qolaEntities())
            {
                //var residentId = Convert.ToString(Id);
                var residentId = resident.ID.ToString();
                var getVitalsign = dbContext.tbl_PostfallClinicalMonitoringVitalSigns.SingleOrDefault(m => m.residentid == residentId && (m.category.ToLower() == "b" && m.vitalsign.ToLower() == vitalSigns.vitalsign.ToLower()));
                if (getVitalsign == null)
                {
                    vitalSigns.residentid = resident.ID.ToString();
                    vitalSigns.date_created = DateTime.Now.ToShortDateString();
                    dbContext.tbl_PostfallClinicalMonitoringVitalSigns.Add(vitalSigns);
                    dbContext.tbl_PostfallClinicalMonitoringDetails.Add(monitoringDetails);
                }
                else
                {
                    var getDetails = dbContext.tbl_PostfallClinicalMonitoringDetails.SingleOrDefault(m => m.category.ToLower() == monitoringDetails.category && m.tbl_PostfallClinicalMonitoringVitalSigns.Id == getVitalsign.Id);
                    getDetails.firstcheck = monitoringDetails.firstcheck;
                    getDetails.fourtyeighthoursfifthcheck = monitoringDetails.fourtyeighthoursfifthcheck;
                    getDetails.fourtyeighthoursfirstcheck = monitoringDetails.fourtyeighthoursfirstcheck;
                    getDetails.fourtyeighthoursfourthcheck = monitoringDetails.fourtyeighthoursfourthcheck;
                    getDetails.fourtyeighthourssecondcheck = monitoringDetails.fourtyeighthourssecondcheck;
                    getDetails.fourtyeighthoursthirdcheck = monitoringDetails.fourtyeighthoursthirdcheck;
                    getDetails.onehourfirstcheck = monitoringDetails.onehourfirstcheck;
                    getDetails.onehoursecondcheck = monitoringDetails.onehoursecondcheck;
                    getDetails.threehoursfirstcheck = monitoringDetails.threehoursfirstcheck;
                    getDetails.threehourssecondcheck = monitoringDetails.threehourssecondcheck;
                    getDetails.threehoursthirdcheck = monitoringDetails.threehoursthirdcheck;

                }

                dbContext.SaveChanges();
            }
            return RedirectToAction("PostFallClinicalMonitoring_B");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostFallClinicalMonitoring_BPage2(tbl_postfallclinicalmonitoringBpage2 model)
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

            var residentId = resident_sess.ID.ToString();
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
            p_Model.Date = DateTime.Now;
            p_Model.Resident = resident;
            p_Model.ModifiedBy = user;
            p_Model.ModifiedOn = DateTime.Now;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ProgressNotesDAL.AddNewProgressNotes(p_Model);
            TempData["Message"] = "Successfully added new Progress note";

            return Redirect("/Home/ResidentMenu/?p_ResidentId=" + resident.ID);
        }

        public ActionResult UnusualIncident()
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

        public ActionResult CarePlan()
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

            var careplan = AssessmentDAL.GetCarePlan("1234");

            return View(careplan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarePlan(CarePlan_VitalSignModel model)
        {
            //var resident_sess = (ResidentModel)TempData["Resident"];

            //resident_sess.ID = 1234;

            AssessmentDAL.AddCarePlan(model, "1234");

            TempData.Keep("Resident");

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
    }
}