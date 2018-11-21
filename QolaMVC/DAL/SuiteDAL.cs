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
    public class SuiteDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public SuiteDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public static List<NEW_SuiteModel> GetAllSuite()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_SuiteModel> l_Collection = new List<NEW_SuiteModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_new_tbl_suite", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_SuiteModel l_Model = new NEW_SuiteModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Home = Convert.ToString(l_Reader["Home"]);
                    l_Model.Suite_No = Convert.ToInt32(l_Reader["suite_no"]);
                    l_Model.Floor_No = Convert.ToInt32(l_Reader["floor_no"]);
                    l_Model.No_Of_Rooms = Convert.ToInt32(l_Reader["no_of_rooms"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllActivityCategory\n" + ex.Message);
            }
        }

        public static void AddSuite(NEW_SuiteModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_add_new_tbl_suite", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Home", p_Model.Home);
                l_Cmd.Parameters.AddWithValue("@suite_no", p_Model.Suite_No);
                l_Cmd.Parameters.AddWithValue("@floor_no", p_Model.Floor_No);
                l_Cmd.Parameters.AddWithValue("@no_of_rooms", p_Model.No_Of_Rooms);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddSuite |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static NEW_SuiteModel GetSuite_By_Id(int id)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                NEW_SuiteModel l_Model = new NEW_SuiteModel();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_id_new_tbl_suite", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Home = Convert.ToString(l_Reader["Home"]);
                    l_Model.Suite_No = Convert.ToInt32(l_Reader["suite_no"]);
                    l_Model.Floor_No = Convert.ToInt32(l_Reader["floor_no"]);
                    l_Model.No_Of_Rooms = Convert.ToInt32(l_Reader["no_of_rooms"]);
                    
                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetSuite_By_Id\n" + ex.Message);
            }
        }

        public static string GetSuiteID_By_SuiteNo_and_Homeid(string SuiteNo,int homeid)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                string suiteid="";
                SqlCommand l_Cmd = new SqlCommand("select fd_id from tbl_Suite where fd_home_id="+ homeid + " and fd_suite_no='"+ SuiteNo+"'", l_Conn);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    suiteid = l_Reader["fd_id"].ToString();
                }

                return suiteid;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetSuite_By_Id\n" + ex.Message);
            }
        }

        public static List<NEW_SuiteModel> GetSuite_By_Column(string column, string value)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_SuiteModel> l_Collection = new List<NEW_SuiteModel>();
                l_Conn.Open();
                
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_column_new_tbl_suite", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@column", column);
                l_Cmd.Parameters.AddWithValue("@value", value);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_SuiteModel l_Model = new NEW_SuiteModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Home = Convert.ToString(l_Reader["Home"]);
                    l_Model.Suite_No = Convert.ToInt32(l_Reader["suite_no"]);
                    l_Model.Floor_No = Convert.ToInt32(l_Reader["floor_no"]);
                    l_Model.No_Of_Rooms = Convert.ToInt32(l_Reader["no_of_rooms"]);

                    l_Collection.Add(l_Model);

                }
                return l_Collection;
        }
            catch (Exception ex)
            {
                throw new Exception(".GetSuite_By_Column\n" + ex.Message);
            }
        }


        public static void EditSuite(NEW_SuiteModel p_Model, int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_update_new_tbl_suite", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.Parameters.AddWithValue("@Home", p_Model.Home);
                l_Cmd.Parameters.AddWithValue("@suite_no", p_Model.Suite_No);
                l_Cmd.Parameters.AddWithValue("@floor_no", p_Model.Floor_No);
                l_Cmd.Parameters.AddWithValue("@no_of_rooms", p_Model.No_Of_Rooms);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "EditSuite |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DeleteSuite(int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_delete_new_tbl_suite", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "DeleteSuite |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static List<NEW_SuiteModel> GetSuite_By_Search(string value)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_SuiteModel> l_Collection = new List<NEW_SuiteModel>();
                l_Conn.Open();

                SqlCommand l_Cmd = new SqlCommand("get_suite_by_search", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@value", value);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_SuiteModel l_Model = new NEW_SuiteModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Home = Convert.ToString(l_Reader["Home"]);
                    l_Model.Suite_No = Convert.ToInt32(l_Reader["suite_no"]);
                    l_Model.Floor_No = Convert.ToInt32(l_Reader["floor_no"]);
                    l_Model.No_Of_Rooms = Convert.ToInt32(l_Reader["no_of_rooms"]);

                    l_Collection.Add(l_Model);

                }
                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetSuite_By_Search\n" + ex.Message);
            }
        }

    }
}