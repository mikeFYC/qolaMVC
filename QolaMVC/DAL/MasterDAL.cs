using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QolaMVC.Models;
using static QolaMVC.Constants.EnumerationTypes;


namespace QolaMVC.DAL
{
    public class MasterDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public MasterDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public static List<ActivityCategoryModel> GetAllActivityCategory()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<ActivityCategoryModel> l_Collection = new List<ActivityCategoryModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_All_Activity_Category", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    ActivityCategoryModel l_Model = new ActivityCategoryModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.EnglishName = Convert.ToString(l_Reader["CategoryNameEnglish"]);
                    l_Model.FrenchName = Convert.ToString(l_Reader["CategoryNameFrench"]);
                    l_Model.MemoryCareColor = Convert.ToString(l_Reader["MemoryCareColor"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllActivityCategory\n" + ex.Message);
            }
        }

        public static void AddActivityCategory(ActivityCategoryModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_Activity_Category", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CategoryNameEnglish", p_Model.EnglishName);
                l_Cmd.Parameters.AddWithValue("@CategoryNameFrench", p_Model.FrenchName);
                l_Cmd.Parameters.AddWithValue("@MemoryCareColor", p_Model.MemoryCareColor);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddActivityCategory |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static List<ActivityModel> GetAllActivity()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<ActivityModel> l_Collection = new List<ActivityModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_All_Activity", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    ActivityModel l_Model = new ActivityModel();
                    ActivityCategoryModel l_Category = new ActivityCategoryModel();

                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.EnglishName = Convert.ToString(l_Reader["ActivityNameEnglish"]);
                    l_Model.FrenchName = Convert.ToString(l_Reader["ActivityNameFrench"]);
                    l_Model.Color = Convert.ToString(l_Reader["ActivityColor"]);
                    l_Model.FunPicture = Convert.ToString(l_Reader["FunPicture"]);
                    l_Model.Province = Convert.ToString(l_Reader["Province"]);
                    l_Model.ShowInAssessment = Convert.ToBoolean(l_Reader["ShowInAssessment"]);
                    l_Model.DisplayTitle = Convert.ToString(l_Reader["ActivityDisplayTitle"]);

                    l_Category.Id = Convert.ToInt32(l_Reader["CategoryId"]);
                    l_Category.FrenchName = Convert.ToString(l_Reader["CategoryNameFrench"]);
                    l_Category.EnglishName = Convert.ToString(l_Reader["CategoryNameEnglish"]);

                    l_Model.Category = l_Category;
                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllActivity\n" + ex.Message);
            }
        }

        public static void AddActivity(ActivityModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_Activity", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CategoryId", p_Model.Category.Id);
                l_Cmd.Parameters.AddWithValue("@ActivityNameEnglish", p_Model.EnglishName);
                l_Cmd.Parameters.AddWithValue("@ActivityNameFrench", p_Model.FrenchName);
                l_Cmd.Parameters.AddWithValue("@ActivityColor", p_Model.Color);
                l_Cmd.Parameters.AddWithValue("@FunPicture", p_Model.FunPicture);
                l_Cmd.Parameters.AddWithValue("@Province", p_Model.Province);
                l_Cmd.Parameters.AddWithValue("@ShowInAssessment", p_Model.ShowInAssessment);
                l_Cmd.Parameters.AddWithValue("@ActivityDisplayTitle", p_Model.DisplayTitle);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddActivity |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static List<ActivityAssessmentCollectionViewModel> GetActivityAssessments(int p_ResidentId)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<ActivityAssessmentCollectionViewModel> l_Collection = new List<ActivityAssessmentCollectionViewModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_ActivityAssessmentStore", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    List<ActivityAssessmentModel> l_Assessments = new List<ActivityAssessmentModel>();
                    ActivityAssessmentCollectionViewModel l_Model = new ActivityAssessmentCollectionViewModel();

                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.DateEntered = Convert.ToDateTime(l_Reader["DateEntered"]);

                    SqlCommand l_Cmd2 = new SqlCommand("spAB_Get_ActivityAssessmentByAssessmentId", l_Conn);
                    l_Cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd2.Parameters.AddWithValue("@AssessmentId", l_Model.Id);
                    SqlDataReader l_Reader2 = l_Cmd2.ExecuteReader();

                    while (l_Reader2.Read())
                    {
                        ActivityAssessmentModel l_Assessment = new ActivityAssessmentModel();
                        l_Assessment.Id = Convert.ToInt32(l_Reader["Id"]);
                        l_Assessment.IsC = Convert.ToBoolean(l_Reader["IsC"]);
                        l_Assessment.IsP = Convert.ToBoolean(l_Reader["IsP"]);
                        l_Assessment.IsW = Convert.ToBoolean(l_Reader["IsW"]);
                        l_Assessment.ResidentId = Convert.ToInt32(l_Reader["ResidentId"]);

                        l_Assessments.Add(l_Assessment);
                    }
                        l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllActivity\n" + ex.Message);
            }
        }

    }
}