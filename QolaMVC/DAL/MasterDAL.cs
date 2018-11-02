using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

        public static ActivityCategoryModel GetActivityCategoryById(int p_Id)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                ActivityCategoryModel l_Model = new ActivityCategoryModel();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Category_By_Id", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CategoryId", p_Id);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.EnglishName = Convert.ToString(l_Reader["CategoryNameEnglish"]);
                    l_Model.FrenchName = Convert.ToString(l_Reader["CategoryNameFrench"]);
                    l_Model.MemoryCareColor = Convert.ToString(l_Reader["MemoryCareColor"]);
                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetActivityCategoryById\n" + ex.Message);
            }
        }

        public static void UpdateActivityCategory(ActivityCategoryModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Update_Activity_Category", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CategoryId", p_Model.Id);
                l_Cmd.Parameters.AddWithValue("@CategoryNameEnglish", p_Model.EnglishName);
                l_Cmd.Parameters.AddWithValue("@CategoryNameFrench", p_Model.FrenchName);
                l_Cmd.Parameters.AddWithValue("@MemoryCareColor", p_Model.MemoryCareColor);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "UpdateActivityCategory |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void DeleteActivityCategory(int p_CategoryId)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Delete_Activity_Category", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CategoryId", p_CategoryId);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "DeleteActivityCategory |" + ex.ToString();
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

        public static ActivityModel GetActivityById(int p_ActivityId)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                ActivityModel l_Model = new ActivityModel();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_By_Id", l_Conn);
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_ActivityId);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
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
                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetActivityById\n" + ex.Message);
            }
        }

        public static void UpdateActivity(ActivityModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Update_Activity", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_Model.Id);
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
                exception = "UpdateActivity |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void DeleteActivity(int p_ActivityId)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Delete_Activity", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_ActivityId);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "DeleteActivity |" + ex.ToString();
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
                    Collection<ActivityAssessmentModel> l_Assessments = new Collection<ActivityAssessmentModel>();
                    ActivityAssessmentCollectionViewModel l_Model = new ActivityAssessmentCollectionViewModel();

                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.DateEntered = Convert.ToDateTime(l_Reader["DateEntered"]);
                    l_Model.Comment = Convert.ToString(l_Reader["Comments"]);
                    l_Model.SAE = Convert.ToString(l_Reader["SAE"]);

                    SqlCommand l_Cmd2 = new SqlCommand("spAB_Get_ActivityAssessmentByAssessmentId", l_Conn);
                    l_Cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd2.Parameters.AddWithValue("@AssessmentId", l_Model.Id);
                    SqlDataReader l_Reader2 = l_Cmd2.ExecuteReader();

                    while (l_Reader2.Read())
                    {
                        ActivityAssessmentModel l_Assessment = new ActivityAssessmentModel();
                        l_Assessment.Id = Convert.ToInt32(l_Reader2["Id"]);
                        l_Assessment.Value = Convert.ToString(l_Reader2["CheckedValue"]);
                        l_Assessment.ResidentId = Convert.ToInt32(l_Reader2["ResidentId"]);

                        var l_AssessmentActivity = new ActivityModel();
                        l_AssessmentActivity.Id = Convert.ToInt32(l_Reader2["ActivityId"]);
                        l_AssessmentActivity.EnglishName = Convert.ToString(l_Reader2["ActivityNameEnglish"]);
                        l_AssessmentActivity.FrenchName = Convert.ToString(l_Reader2["ActivityNameFrench"]);
                        l_AssessmentActivity.Category = new ActivityCategoryModel();
                        l_AssessmentActivity.Category.Id = Convert.ToInt32(l_Reader2["CategoryId"]);

                        l_Assessment.Activity = l_AssessmentActivity;
                        l_Assessments.Add(l_Assessment);
                    }

                    Collection<ActivityCategoryModel> l_CategoryCollection = new Collection<ActivityCategoryModel>();
                    SqlCommand l_CmdCategory = new SqlCommand("spAB_Get_All_Activity_Category", l_Conn);
                    l_CmdCategory.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader l_CategoryReader = l_CmdCategory.ExecuteReader();

                    while (l_CategoryReader.Read())
                    {
                        ActivityCategoryModel l_CategoryModel = new ActivityCategoryModel();
                        l_CategoryModel.Id = Convert.ToInt32(l_CategoryReader["Id"]);
                        l_CategoryModel.EnglishName = Convert.ToString(l_CategoryReader["CategoryNameEnglish"]);
                        l_CategoryModel.FrenchName = Convert.ToString(l_CategoryReader["CategoryNameFrench"]);
                        l_CategoryModel.MemoryCareColor = Convert.ToString(l_CategoryReader["MemoryCareColor"]);

                        l_CategoryCollection.Add(l_CategoryModel);
                    }

                    l_Model.Category = l_CategoryCollection;
                    l_Model.ActivityAssessments = l_Assessments;
                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetActivityAssessments\n" + ex.Message);
            }
        }

        public static List<ActivityAssessmentCollectionViewModel> InitActivityAssessments()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<ActivityAssessmentCollectionViewModel> l_Collection = new List<ActivityAssessmentCollectionViewModel>();
                l_Conn.Open();
                Collection<ActivityAssessmentModel> l_Assessments = new Collection<ActivityAssessmentModel>();
                ActivityAssessmentCollectionViewModel l_Model = new ActivityAssessmentCollectionViewModel();

                l_Model.Id = 0;
                l_Model.DateEntered = DateTime.Now;

                SqlCommand l_Cmd2 = new SqlCommand("spAB_Get_All_Activity", l_Conn);
                l_Cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd2.ExecuteReader();

                while (l_Reader.Read())
                {
                    ActivityAssessmentModel l_Assessment = new ActivityAssessmentModel();
                    l_Assessment.Id = 0;
                    
                    var l_AssessmentActivity = new ActivityModel();
                    l_AssessmentActivity.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_AssessmentActivity.EnglishName = Convert.ToString(l_Reader["ActivityNameEnglish"]);
                    l_AssessmentActivity.FrenchName= Convert.ToString(l_Reader["ActivityNameFrench"]);
                    l_AssessmentActivity.Category = new ActivityCategoryModel();
                    l_AssessmentActivity.Category.Id = Convert.ToInt32(l_Reader["CategoryId"]);

                    l_Assessment.Activity = l_AssessmentActivity;
                    l_Assessments.Add(l_Assessment);
                }

                Collection<ActivityCategoryModel> l_CategoryCollection = new Collection<ActivityCategoryModel>();
                SqlCommand l_CmdCategory = new SqlCommand("spAB_Get_All_Activity_Category", l_Conn);
                l_CmdCategory.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_CategoryReader = l_CmdCategory.ExecuteReader();

                while (l_CategoryReader.Read())
                {
                    ActivityCategoryModel l_CategoryModel = new ActivityCategoryModel();
                    l_CategoryModel.Id = Convert.ToInt32(l_CategoryReader["Id"]);
                    l_CategoryModel.EnglishName = Convert.ToString(l_CategoryReader["CategoryNameEnglish"]);
                    l_CategoryModel.FrenchName = Convert.ToString(l_CategoryReader["CategoryNameFrench"]);
                    l_CategoryModel.MemoryCareColor = Convert.ToString(l_CategoryReader["MemoryCareColor"]);

                    l_CategoryCollection.Add(l_CategoryModel);
                }

                l_Model.Category = l_CategoryCollection;
                l_Model.ActivityAssessments = l_Assessments;
                l_Collection.Add(l_Model);
            
                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".InitActivityAssessments\n" + ex.Message);
            }
        }

        public static void AddActivityAssessments(int p_ResidentId, int p_EnteredBy, ActivityAssessmentCollectionViewModel p_Model)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                int l_AssessmentId = 0;
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_ActivityAssessmentStore", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_EnteredBy);
                l_Cmd.Parameters.AddWithValue("@Comments", p_Model.Comment);
                l_Cmd.Parameters.AddWithValue("@SAE", p_Model.SAE);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_AssessmentId = Convert.ToInt32(l_Reader["AssessmentStoreId"]);
                }

                foreach(var l_Assessment in p_Model.ActivityAssessments)
                {
                    SqlCommand l_Cmd2 = new SqlCommand("spAB_Add_ActivityAssessment", l_Conn);
                    l_Cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd2.Parameters.AddWithValue("@AssessmentId", l_AssessmentId);
                    l_Cmd2.Parameters.AddWithValue("@ActivityId", l_Assessment.Activity.Id);
                    l_Cmd2.Parameters.AddWithValue("@CheckedValue", l_Assessment.Value);
                    l_Cmd2.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                    l_Cmd2.Parameters.AddWithValue("@DateEntered", DateTime.Now);
                    SqlDataReader l_Reader2 = l_Cmd2.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(".AddActivityAssessments\n" + ex.Message);
            }
        }



        public static void save_button(int tb_number, int userid, int residentid)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " delete from [tbl_AB_Admission_checklist] where fd_resident_id=@resident and fd_tb_num=@tbnum" +
                              " insert into [tbl_AB_Admission_checklist] values(@resident,@tbnum,@user,@date)";
            cmd.Parameters.AddWithValue("@resident", residentid);
            cmd.Parameters.AddWithValue("@tbnum", tb_number);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@user", userid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            conn.Close();
        }

        public static void save_button(int tb_number, int residentid)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " delete from [tbl_AB_Admission_checklist] where fd_resident_id=@resident and fd_tb_num=@tbnum" +
                              " insert into [tbl_AB_Admission_checklist] values(@resident,@tbnum,null,null)";
            cmd.Parameters.AddWithValue("@resident", residentid);
            cmd.Parameters.AddWithValue("@tbnum", tb_number);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            conn.Close();
        }


        public static string get_checklist(int residentid)
        {
            string a = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select U.fd_first_name+' '+U.fd_last_name as u_name, " +
                                " AC.fd_modified_on from [tbl_AB_Admission_checklist] AC" +
                                " left join tbl_User U on AC.fd_modified_by = U.fd_id" +
                                " where AC.fd_resident_id ="+ residentid+ "order by fd_tb_num";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    if (rd[1] == null)
                    {
                        a = a + "," + ",";
                    }
                    else
                        a = a + rd[1] + "," + rd[0] + ",";
                }
                a = a.Substring(0, a.Length - 1);
            }
            conn.Close();
            return a;
        }

    }
}
 