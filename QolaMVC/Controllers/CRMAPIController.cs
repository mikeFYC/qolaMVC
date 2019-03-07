using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace QolaMVC.Controllers
{
    public class CRMAPIController : ApiController
    {


        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public Collection<HomeModal_CRM_API> GetHome()
        {
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
                        home.ProvinceID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_province"]);
                        
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
        public Collection<SuiteModal_CRM_API> GetAvailableSuites(int FacilityId, int Occupancy, DateTime MoveInDate)
        {
            Collection<SuiteModal_CRM_API> suites = GetAvailableSuites_CRM_API(FacilityId, Occupancy, MoveInDate);
            return suites;
        }
        public static Collection<SuiteModal_CRM_API> GetAvailableSuites_CRM_API(int homeId, int Occupancy, DateTime dateinput)
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
                l_Cmd.Parameters.AddWithValue("@occupancy", Occupancy);
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
        public object[] POST_RESIDENT_MOVE_IN(ResidentModal_CRM_API InputModal)
        {
            bool result1;bool result2;bool result3;
            bool result4;bool result5;bool result6;
            int[] RR = new int[2];
            int[] RR2 = new int[2];
            object[] resultArray = new object[2];

            ResidentModel p_Model = new ResidentModel();

            p_Model.Home = new HomeModel();
            p_Model.Home.Id = InputModal.FacilityId;
            p_Model.SuiteIds = InputModal.Suiteid.ToString();
            p_Model.ModifiedBy = new UserModel();
            p_Model.ModifiedBy.ID = 1;
            p_Model.Occupancy = InputModal.Occupancy;
            p_Model.ModifiedOn = DateTime.Now;
            p_Model.MoveInDate = InputModal.MoveInDate;
            p_Model.MarketResource = InputModal.MarketSource;

            p_Model.FirstName = InputModal.ProspectFirstName;
            p_Model.LastName = InputModal.ProspectLastName;
            if(InputModal.ProspectGender.Trim().ToLower()=="male")  p_Model.Gendar = 'M';
            else if (InputModal.ProspectGender.Trim().ToLower() == "female")  p_Model.Gendar = 'F';
            p_Model.BirthDate = InputModal.ProspectDOB;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "married") p_Model.MaritalStatus = 1;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "widowed") p_Model.MaritalStatus = 2;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "single") p_Model.MaritalStatus = 3;
            if (InputModal.ProspectMaritalStatus.Trim().ToLower() == "divorced") p_Model.MaritalStatus = 4;
            p_Model.Phone = InputModal.ProspectPhone;
            p_Model.Contract1 = InputModal.ProspectContact1FirstName + " " + InputModal.ProspectContact1LastName;
            p_Model.Address1 = InputModal.ProspectContact1Address;
            p_Model.CellPhone1 = InputModal.ProspectContact1Phone;
            p_Model.CellPhoneType1 = 1;
            p_Model.Email1 = InputModal.ProspectContact1Email;
            p_Model.Relationship1 = InputModal.ProspectContact1Relationship;
            p_Model.Contract2 = InputModal.ProspectContact2FirstName + " " + InputModal.ProspectContact2LastName;
            p_Model.Address2 = InputModal.ProspectContact2Address;
            p_Model.CellPhone2 = InputModal.ProspectContact2Phone;
            p_Model.CellPhoneType2 = 1;
            p_Model.Email2 = InputModal.ProspectContact2Email;
            p_Model.Relationship2 = InputModal.ProspectContact2Relationship;
            if (InputModal.ProspectFinancing == true) p_Model.AHS = 'Y';
            else if (InputModal.ProspectFinancing == false) p_Model.AHS = 'N';


            //if (p_Model.DNRStatusIndex == true) p_Model.DNRStatus = 'Y';
            //if (p_Model.FullCodeStatusIndex == true) p_Model.FullCodeStatus = 'Y';
            //if (p_Model.ReligiousAffiliation == "Other") p_Model.ReligiousAffiliation = p_Model.ReligiousAffiliationOther;
            //if (p_Model.Vetaran == "Other" || p_Model.Vetaran == "None") p_Model.Vetaran = p_Model.VeteranOther;
            //if (p_Model.EducationLevel == "Other") p_Model.EducationLevel = p_Model.EducationLevelOther;
            //if (p_Model.callHospital_replacement == true) p_Model.CallHospital = Convert.ToChar("Y");
            //if (p_Model.FullCodeStatus == '\0') p_Model.FullCodeStatus = 'N';
            //if (p_Model.DNRStatus == '\0') p_Model.DNRStatus = 'N';

            foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(p_Model) == null) { prop.SetValue(p_Model, ""); }
                }
            }

            //ResidentsDAL.SetUp_ResidentModel_ListItems(p_Model);

            RR = ResidentsDAL.AddNewResidentGeneralInfo(p_Model);
            result1 = ResidentsDAL.UpdateResidentEmergencyContacts(p_Model);
            result2 = ResidentsDAL.UpdateResidentGeneralInfo(p_Model);
            result3 = ResidentsDAL.UpdateResidentMedicalInfo_mike(p_Model);

            for (int a = 0; a < 22; a++)
            {
                if (a == 0) MasterDAL.save_button(a + 1, p_Model.ModifiedBy.ID, RR[0]);
                else        MasterDAL.save_button(a + 1, RR[0]);
            }



            //Second Resident

            if (InputModal.Prospect2FirstName != null && InputModal.Prospect2FirstName != "")
            {
                ResidentModel p_Model2 = new ResidentModel();

                p_Model2.Home = new HomeModel();
                p_Model2.Home.Id = InputModal.FacilityId;
                p_Model2.SuiteIds = InputModal.Suiteid.ToString();
                p_Model2.ModifiedBy = new UserModel();
                p_Model2.ModifiedBy.ID = 1;
                p_Model2.Occupancy = InputModal.Occupancy;
                p_Model2.ModifiedOn = DateTime.Now;
                p_Model2.MoveInDate = InputModal.MoveInDate;
                p_Model2.MarketResource = InputModal.MarketSource;

                p_Model2.FirstName = InputModal.Prospect2FirstName;
                p_Model2.LastName = InputModal.Prospect2LastName;
                if (InputModal.Prospect2Gender.Trim().ToLower() == "male") p_Model2.Gendar = 'M';
                else if (InputModal.Prospect2Gender.Trim().ToLower() == "female") p_Model2.Gendar = 'F';
                p_Model2.BirthDate = InputModal.Prospect2DOB;
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "married") p_Model2.MaritalStatus = 1;
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "widowed") p_Model2.MaritalStatus = 2;
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "single") p_Model2.MaritalStatus = 3;
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "divorced") p_Model2.MaritalStatus = 4;
                p_Model2.Phone = InputModal.Prospect2Phone;
                p_Model2.Contract1 = InputModal.Prospect2Contact1FirstName + " " + InputModal.Prospect2Contact1LastName;
                p_Model2.Address1 = InputModal.Prospect2Contact1Address;
                p_Model2.CellPhone1 = InputModal.Prospect2Contact1Phone;
                p_Model2.CellPhoneType1 = 1;
                p_Model2.Email1 = InputModal.Prospect2Contact1Email;
                p_Model2.Relationship1 = InputModal.Prospect2Contact1Relationship;
                p_Model2.Contract2 = InputModal.Prospect2Contact2FirstName + " " + InputModal.Prospect2Contact2LastName;
                p_Model2.Address2 = InputModal.Prospect2Contact2Address;
                p_Model2.CellPhone2 = InputModal.Prospect2Contact2Phone;
                p_Model2.CellPhoneType2 = 1;
                p_Model2.Email2 = InputModal.Prospect2Contact2Email;
                p_Model2.Relationship2 = InputModal.Prospect2Contact2Relationship;
                if (InputModal.Prospect2Financing == true) p_Model2.AHS = 'Y';
                else if (InputModal.Prospect2Financing == false) p_Model2.AHS = 'N';

                foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
                {
                    if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                    {
                        if (prop.GetValue(p_Model2) == null) { prop.SetValue(p_Model2, ""); }
                    }
                }
 
                RR2 = ResidentsDAL.AddNewResidentGeneralInfo(p_Model2);
                result4 = ResidentsDAL.UpdateResidentEmergencyContacts(p_Model2);
                result5 = ResidentsDAL.UpdateResidentGeneralInfo(p_Model2);
                result6 = ResidentsDAL.UpdateResidentMedicalInfo_mike(p_Model2);

                for (int a = 0; a < 22; a++)
                {
                    if (a == 0) MasterDAL.save_button(a + 1, p_Model2.ModifiedBy.ID, RR2[0]);
                    else        MasterDAL.save_button(a + 1, RR2[0]);
                }
            }



            if (RR[0] > 0 && result1 == true && result2 == true && result3 == true)
            {
                resultArray[0] = true;
                resultArray[1] = "Resident Move In Successfully";

            }
            else
            {
                resultArray[0] = false;
                resultArray[1] = "Resident Move In Failed";
            }
            return resultArray;
        }



    }
}
