using Microsoft.Owin.Security;
using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace QolaMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string p_Message = "")
        {
            if (p_Message != string.Empty)
            {
                ViewBag.ErrorMessage = p_Message;
            }
            if (Session["language"] == null || Session["language"].ToString() == "")
            {
                Session["language"] = "en";
            }

            return View();
        }


        public ActionResult ErrorPage()
        {
            return View();
        }


        public int GetSessionTimeLeft(DateTime timebegin)
        {
            
            return Session.Timeout- (DateTime.Now - timebegin).Minutes;
        }

        public ActionResult ChangeLanguage(string LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        public void ChangeLanguage2(string LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            if (LanguageAbbrevation == "" || LanguageAbbrevation==null || LanguageAbbrevation == "en")
            {
                Session["Language"] = "en";
            }
            else if (LanguageAbbrevation == "fr")
            {
                Session["Language"] = "fr";
            }

        }

        [WebMethod(EnableSession = true)]
        public string setLanguageId(string sLanguageId)
        {
            string result = string.Empty;

            System.Web.HttpContext.Current.Session["language"] = sLanguageId;

            return result;
        }


        [HttpPost]
        public ActionResult Auth_Decom(FormCollection p_Collection)
        {
            var l_User = new UserModel();
            TryUpdateModel(l_User, p_Collection);

            string l_Password = BitConverter.ToString(new SHA1Managed().ComputeHash(new UTF8Encoding().GetBytes(l_User.Password)));

            if (UserDAL.GetUserByUsernameAndPassword(l_User.UserName, l_Password, string.Empty, string.Empty).ID != 0)
            {
                
                return Redirect("/Home");
            }
            else
            {
                string l_ErrorMessage = "Invalid Username of password";
                return RedirectToAction("Index", new { p_Message = l_ErrorMessage } );
            }
        }

        [HttpPost]
        public ActionResult Auth(UserModel model, string ReturnUrl)
        {
            string l_Password = BitConverter.ToString(new SHA1Managed().ComputeHash(new UTF8Encoding().GetBytes(model.Password)));

            var user = UserDAL.GetUserByUsernameAndPassword(model.UserName, l_Password, string.Empty, string.Empty);

            //check username and password from database, naive checking: 
            //password should be in SHA
            if (user.ID != 0)
            {
                var claims = new[] {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                // can add more claims
            };
                TempData["User"] = user;
                
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                // Add roles into claims
                    //var roles = _roleService.GetByUserId(user.Id);
                    //if (roles.Any())
                    //{
                    //    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r.Name));
                    //    identity.AddClaims(roleClaims);
                    //}

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                
                authManager.SignIn(new AuthenticationProperties
                { IsPersistent = model.RememberMe }, identity);
                Thread.CurrentPrincipal = claimsPrincipal;

                return RedirectToAction("Index", "Home");
            }
            // login failed.   
            string l_ErrorMessage = "Invalid Username or password";
            return RedirectToAction("Index", new { p_Message = l_ErrorMessage });
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login");
        }


        //CRM-QOLA API Required Function
        //CRM-QOLA API Required Function
        //CRM-QOLA API Required Function

        [HttpGet]
        public Collection<HomeModal_CRM_API> GetHome()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            Collection<HomeModal_CRM_API> homes = GetHomeCollections_CRM_API();
            return homes;
        }
        public static Collection<HomeModal_CRM_API> GetHomeCollections_CRM_API()
        {
            string exception = string.Empty;
            Collection<HomeModal_CRM_API> homes = new Collection<HomeModal_CRM_API>();
            HomeModal_CRM_API home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_HOME, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if (homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {

                        home = new HomeModal_CRM_API();
                        home.FacilityID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        home.FacilityName = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_name"]);
                        home.FacilityCode = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_code"]);
                        home.Address = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_address"]);
                        home.City = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_city"]);                   
                        home.Province = Convert.ToString(homesReceive.Tables[0].Rows[index]["ProvinceName"]);               
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetHomeCollections_CRM_API |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        [HttpGet]
        public Collection<SuiteModal_CRM_API> GetAvailableSuites(int FacilityId)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            Collection<SuiteModal_CRM_API> suites = GetAvailableSuites_CRM_API(FacilityId, DateTime.Now);
            return suites;
        }
        public static Collection<SuiteModal_CRM_API> GetAvailableSuites_CRM_API(int homeId, DateTime dateinput)
        {
            string exception = string.Empty;
            Collection<SuiteModal_CRM_API> suites = new Collection<SuiteModal_CRM_API>();
            SuiteModal_CRM_API suite;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("Get_Available_Suite_By_home_Id_CRM_API", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeID", homeId);
                l_Cmd.Parameters.AddWithValue("@moveInDate", dateinput);
                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if (homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {

                        suite = new SuiteModal_CRM_API();
                        suite.SuiteID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        suite.Floor = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_floor"]);
                        suite.SuiteNo = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_suite_no"]);

                        suites.Add(suite);
                    }
                }
                return suites;
            }
            catch (Exception ex)
            {
                exception = "GetHomeCollections_CRM_API |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        [HttpPost]
        public Boolean POST_RESIDENT_MOVE_IN(ResidentModal_CRM_API InputModal)
        {
            
            var user = (UserModel)TempData["User"];
            //var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            //TempData.Keep("Home");
            ViewBag.User = user;

            ResidentModel p_Model = new ResidentModel();
            p_Model.FirstName = InputModal.ProspectFirstName;
            p_Model.LastName = InputModal.ProspectLastName;
            //p_Model.Gendar = InputModal.ProspectSuffix;
            p_Model.BirthDate = InputModal.ProspectDOB;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "married") p_Model.MaritalStatus = 1;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "widowed") p_Model.MaritalStatus = 2;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "single") p_Model.MaritalStatus = 3;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "divorced") p_Model.MaritalStatus = 4;
            //InputModal.ProspectAddress;
            //InputModal.ProspectCity;
            //InputModal.ProspectProvince;
            //InputModal.ProspectPostalCode;
            p_Model.Phone = InputModal.ProspectPhone;
            //InputModal.ProspectEmail;
            p_Model.Contract1 = InputModal.Contact1FirstName + " " + InputModal.Contact1LastName;
            //InputModal.Contact1Suffix;
            //InputModal.Contact1DOB;
            //InputModal.Contact1MaritalStatus;
            p_Model.Address1 = InputModal.Contact1Address + ", " + InputModal.Contact1City + ", " + InputModal.Contact1Province + ", " + InputModal.Contact1PostalCode;
            p_Model.CellPhone1 = InputModal.Contact1Phone;
            p_Model.CellPhoneType1 = 1;
            p_Model.Email1 = InputModal.Contact1Email;

            p_Model.Contract2 = InputModal.Contact2FirstName + " " + InputModal.Contact2LastName;
            //InputModal.Contact2Suffix;
            //InputModal.Contact2DOB;
            //InputModal.Contact2MaritalStatus;
            p_Model.Address2 = InputModal.Contact2Address + ", " + InputModal.Contact2City + ", " + InputModal.Contact2Province + ", " + InputModal.Contact2PostalCode;
            p_Model.CellPhone2 = InputModal.Contact2Phone;
            p_Model.CellPhoneType2 = 1;
            p_Model.Email2 = InputModal.Contact2Email;

            p_Model.Home = new HomeModel();
            p_Model.Home.Id = InputModal.FacilityId;
            p_Model.SuiteIds = InputModal.Suiteid.ToString();


            if (p_Model.DNRStatusIndex == true) p_Model.DNRStatus = 'Y';
            if (p_Model.FullCodeStatusIndex == true) p_Model.FullCodeStatus = 'Y';

            foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(p_Model) == null) { prop.SetValue(p_Model, ""); }
                }
            }

            if (p_Model.ReligiousAffiliation == "Other")
            {
                p_Model.ReligiousAffiliation = p_Model.ReligiousAffiliationOther;
            }
            if (p_Model.Vetaran == "Other" || p_Model.Vetaran == "None")
            {
                p_Model.Vetaran = p_Model.VeteranOther;
            }
            if (p_Model.EducationLevel == "Other")
            {
                p_Model.EducationLevel = p_Model.EducationLevelOther;
            }
            if (p_Model.callHospital_replacement == true)
            {
                p_Model.CallHospital = Convert.ToChar("Y");
            }

            if (p_Model.FullCodeStatus == '\0') p_Model.FullCodeStatus = 'N';
            if (p_Model.DNRStatus == '\0') p_Model.DNRStatus = 'N';

            //p_Model.SuiteIds = SuiteDAL.GetSuiteID_By_SuiteNo_and_Homeid(p_Model.SuiteNo, home.Id);

            ResidentsDAL.SetUp_ResidentModel_ListItems(p_Model);

            p_Model.ModifiedBy = user;
            p_Model.ModifiedOn = DateTime.Now;
            //p_Model.Home = home;
            int[] RR = new int[2];
            RR = ResidentsDAL.AddNewResidentGeneralInfo(p_Model);
            bool result1 = ResidentsDAL.UpdateResidentEmergencyContacts(p_Model);
            bool result2 = ResidentsDAL.UpdateResidentGeneralInfo(p_Model);
            bool result3 = ResidentsDAL.UpdateResidentMedicalInfo_mike(p_Model);

            string[] arrayMain = new string[22];
            for (int a = 0; a < arrayMain.Length; a++)
            {
                if (a == 0)
                {
                    MasterDAL.save_button(a + 1, user.ID, RR[0]);
                }
                else
                {
                    MasterDAL.save_button(a + 1, RR[0]);
                }
            }

            TempData["ResidentID"] = RR[0];
            TempData.Keep("ResidentID");

            TempData["p_Model"] = p_Model;
            TempData.Keep("p_Model");

            if(RR[0]>0 && result1==true && result2==true && result3 == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}