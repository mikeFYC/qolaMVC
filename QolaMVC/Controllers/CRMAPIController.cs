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
            p_Model.Relationship1 = InputModal.Contact1Relationship;

            p_Model.Contract2 = InputModal.Contact2FirstName + " " + InputModal.Contact2LastName;
            //InputModal.Contact2Suffix;
            //InputModal.Contact2DOB;
            //InputModal.Contact2MaritalStatus;
            p_Model.Address2 = InputModal.Contact2Address + ", " + InputModal.Contact2City + ", " + InputModal.Contact2Province + ", " + InputModal.Contact2PostalCode;
            p_Model.CellPhone2 = InputModal.Contact2Phone;
            p_Model.CellPhoneType2 = 1;
            p_Model.Email2 = InputModal.Contact2Email;
            p_Model.Relationship2 = InputModal.Contact2Relationship;

            p_Model.Home = new HomeModel();
            p_Model.Home.Id = InputModal.FacilityId;
            p_Model.SuiteIds = InputModal.Suiteid.ToString();

            if (p_Model.DNRStatusIndex == true) p_Model.DNRStatus = 'Y';
            if (p_Model.FullCodeStatusIndex == true) p_Model.FullCodeStatus = 'Y';
            if (p_Model.ReligiousAffiliation == "Other") p_Model.ReligiousAffiliation = p_Model.ReligiousAffiliationOther;
            if (p_Model.Vetaran == "Other" || p_Model.Vetaran == "None") p_Model.Vetaran = p_Model.VeteranOther;
            if (p_Model.EducationLevel == "Other") p_Model.EducationLevel = p_Model.EducationLevelOther;
            if (p_Model.callHospital_replacement == true) p_Model.CallHospital = Convert.ToChar("Y");
            if (p_Model.FullCodeStatus == '\0') p_Model.FullCodeStatus = 'N';
            if (p_Model.DNRStatus == '\0') p_Model.DNRStatus = 'N';

            foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(p_Model) == null) { prop.SetValue(p_Model, ""); }
                }
            }

            ResidentsDAL.SetUp_ResidentModel_ListItems(p_Model);

            p_Model.ModifiedBy = new UserModel();
            p_Model.ModifiedBy.ID = 1;
            p_Model.Occupancy = InputModal.Occupancy;
            p_Model.ModifiedOn = DateTime.Now;
            p_Model.MoveInDate = InputModal.MoveInDate;

            int[] RR = new int[2];
            RR = ResidentsDAL.AddNewResidentGeneralInfo(p_Model);
            bool result1 = ResidentsDAL.UpdateResidentEmergencyContacts(p_Model);
            bool result2 = ResidentsDAL.UpdateResidentGeneralInfo(p_Model);
            bool result3 = ResidentsDAL.UpdateResidentMedicalInfo_mike(p_Model);

            for (int a = 0; a < 22; a++)
            {
                if (a == 0) MasterDAL.save_button(a + 1, p_Model.ModifiedBy.ID, RR[0]);
                else        MasterDAL.save_button(a + 1, RR[0]);
            }



            //Second Resident

            if (InputModal.Prospect2FirstName != null && InputModal.Prospect2FirstName != "")
            {
                ResidentModel p_Model2 = new ResidentModel();
                p_Model2.FirstName = InputModal.Prospect2FirstName;
                p_Model2.LastName = InputModal.Prospect2LastName;
                p_Model2.BirthDate = DateTime.Parse(InputModal.Prospect2DateOfBirth);
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "married") p_Model2.MaritalStatus = 1;
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "widowed") p_Model2.MaritalStatus = 2;
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "single") p_Model2.MaritalStatus = 3;
                if (InputModal.Prospect2MartialStatus.Trim().ToLower() == "divorced") p_Model2.MaritalStatus = 4;

                p_Model2.Home = new HomeModel();
                p_Model2.Home.Id = InputModal.FacilityId;
                p_Model2.SuiteIds = InputModal.Suiteid.ToString();

                p_Model2.ModifiedBy = new UserModel();
                p_Model2.ModifiedBy.ID = 1;
                p_Model2.Occupancy = InputModal.Occupancy;
                p_Model2.ModifiedOn = DateTime.Now;
                p_Model2.MoveInDate = InputModal.MoveInDate;

                foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
                {
                    if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                    {
                        if (prop.GetValue(p_Model2) == null) { prop.SetValue(p_Model2, ""); }
                    }
                }

                int[] RR2 = new int[2];
                RR2 = ResidentsDAL.AddNewResidentGeneralInfo(p_Model2);
                bool result4 = ResidentsDAL.UpdateResidentEmergencyContacts(p_Model2);
                bool result5 = ResidentsDAL.UpdateResidentGeneralInfo(p_Model2);
                bool result6 = ResidentsDAL.UpdateResidentMedicalInfo_mike(p_Model2);

                for (int a = 0; a < 22; a++)
                {
                    if (a == 0) MasterDAL.save_button(a + 1, p_Model2.ModifiedBy.ID, RR2[0]);
                    else        MasterDAL.save_button(a + 1, RR2[0]);
                }
            }





            object[] resultArray = new object[2];

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


        [HttpPost]
        public object[] POST_RESIDENT_MOVE_IN2(ResidentModal_CRM_API InputModal)
        {
            int intcheck = POST_RESIDENT_MOVE_IN2_Function(InputModal);

            object[] resultArray = new object[2];
            resultArray[0] = true;
            resultArray[1] = "Resident Move In Successfully";
            return resultArray;
        }

        public static int POST_RESIDENT_MOVE_IN2_Function(ResidentModal_CRM_API pModel)
        {
            string exception = string.Empty;
            int GGG=0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@FacilityId", pModel.FacilityId);
                l_Cmd.Parameters.AddWithValue("@Suiteid", pModel.Suiteid);
                l_Cmd.Parameters.AddWithValue("@Occupancy", pModel.Occupancy);
                l_Cmd.Parameters.AddWithValue("@MoveInDate", pModel.MoveInDate);
                l_Cmd.Parameters.AddWithValue("@DateofContract", pModel.DateofContract);
                l_Cmd.Parameters.AddWithValue("@TypesofInquiry", pModel.TypesofInquiry);
                l_Cmd.Parameters.AddWithValue("@Market", pModel.Market);
                l_Cmd.Parameters.AddWithValue("@ThirdPartyAgency", pModel.ThirdPartyAgency);
                l_Cmd.Parameters.AddWithValue("@PrimaryContact", pModel.PrimaryContact);
                l_Cmd.Parameters.AddWithValue("@ProspectType", pModel.ProspectType);
                l_Cmd.Parameters.AddWithValue("@ProspectFirstName", pModel.ProspectFirstName);
                l_Cmd.Parameters.AddWithValue("@ProspectLastName", pModel.ProspectLastName);
                l_Cmd.Parameters.AddWithValue("@ProspectSuffix", pModel.ProspectSuffix);
                l_Cmd.Parameters.AddWithValue("@ProspectDOB", pModel.ProspectDOB);
                l_Cmd.Parameters.AddWithValue("@ProspectMaritalStatus", pModel.ProspectMaritalStatus);
                l_Cmd.Parameters.AddWithValue("@ProspectAddress", pModel.ProspectAddress);
                l_Cmd.Parameters.AddWithValue("@ProspectCity", pModel.ProspectCity);
                l_Cmd.Parameters.AddWithValue("@ProspectProvince", pModel.ProspectProvince);
                l_Cmd.Parameters.AddWithValue("@ProspectPostalCode", pModel.ProspectPostalCode);
                l_Cmd.Parameters.AddWithValue("@ProspectPhone", pModel.ProspectPhone);
                l_Cmd.Parameters.AddWithValue("@ProspectEmail", pModel.ProspectEmail);
                l_Cmd.Parameters.AddWithValue("@Financing", pModel.Financing);
                l_Cmd.Parameters.AddWithValue("@Prospect2FirstName", pModel.Prospect2FirstName);
                l_Cmd.Parameters.AddWithValue("@Prospect2LastName", pModel.Prospect2LastName);
                l_Cmd.Parameters.AddWithValue("@Prospect2DateOfBirth", pModel.Prospect2DateOfBirth);
                l_Cmd.Parameters.AddWithValue("@Prospect2MartialStatus", pModel.Prospect2MartialStatus);
                l_Cmd.Parameters.AddWithValue("@Prospect2Relationship", pModel.Prospect2Relationship);
                l_Cmd.Parameters.AddWithValue("@Prospect2Finance", pModel.Prospect2Finance);
                l_Cmd.Parameters.AddWithValue("@Contact1FirstName", pModel.Contact1FirstName);
                l_Cmd.Parameters.AddWithValue("@Contact1LastName", pModel.Contact1LastName);
                l_Cmd.Parameters.AddWithValue("@Contact1Relationship", pModel.Contact1Relationship);
                l_Cmd.Parameters.AddWithValue("@Contact1Suffix", pModel.Contact1Suffix);
                l_Cmd.Parameters.AddWithValue("@Contact1DOB", pModel.Contact1DOB);
                l_Cmd.Parameters.AddWithValue("@Contact1MaritalStatus", pModel.Contact1MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@Contact1Address", pModel.Contact1Address);
                l_Cmd.Parameters.AddWithValue("@Contact1City", pModel.Contact1City);
                l_Cmd.Parameters.AddWithValue("@Contact1Province", pModel.Contact1Province);
                l_Cmd.Parameters.AddWithValue("@Contact1PostalCode", pModel.Contact1PostalCode);
                l_Cmd.Parameters.AddWithValue("@Contact1Phone", pModel.Contact1Phone);
                l_Cmd.Parameters.AddWithValue("@Contact1Email", pModel.Contact1Email);
                l_Cmd.Parameters.AddWithValue("@Contact2FirstName", pModel.Contact2FirstName);
                l_Cmd.Parameters.AddWithValue("@Contact2LastName", pModel.Contact2LastName);
                l_Cmd.Parameters.AddWithValue("@Contact2Relationship", pModel.Contact2Relationship);
                l_Cmd.Parameters.AddWithValue("@Contact2Suffix", pModel.Contact2Suffix);
                l_Cmd.Parameters.AddWithValue("@Contact2DOB", pModel.Contact2DOB);
                l_Cmd.Parameters.AddWithValue("@Contact2MaritalStatus", pModel.Contact2MaritalStatus);
                l_Cmd.Parameters.AddWithValue("@Contact2Address", pModel.Contact2Address);
                l_Cmd.Parameters.AddWithValue("@Contact2City", pModel.Contact2City);
                l_Cmd.Parameters.AddWithValue("@Contact2Province", pModel.Contact2Province);
                l_Cmd.Parameters.AddWithValue("@Contact2PostalCode", pModel.Contact2PostalCode);
                l_Cmd.Parameters.AddWithValue("@Contact2Phone", pModel.Contact2Phone);
                l_Cmd.Parameters.AddWithValue("@Contact2Email", pModel.Contact2Email);
                l_Cmd.Parameters.AddWithValue("@ProspectAssessments", pModel.ProspectAssessments);
                GGG = l_Cmd.ExecuteNonQuery();
                return GGG;
            }
            catch (Exception ex)
            {
                exception = "POST_RESIDENT_MOVE_IN2_Function |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

    }
}
