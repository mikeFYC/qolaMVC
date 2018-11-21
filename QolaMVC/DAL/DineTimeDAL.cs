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
    public class DineTimeDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public DineTimeDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public static List<NEW_DineTimeModel> GetAllDineTime()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_DineTimeModel> l_Collection = new List<NEW_DineTimeModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_new_tbl_dine_time", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_DineTimeModel l_Model = new NEW_DineTimeModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.DineTime = Convert.ToString(l_Reader["dinetime"]);
                    l_Model.ShortName = Convert.ToString(l_Reader["shortname"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllDineTime\n" + ex.Message);
            }
        }

        public static void AddDineTime(NEW_DineTimeModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_add_new_tbl_dine_time", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@dinetime", p_Model.DineTime);
                l_Cmd.Parameters.AddWithValue("@shortname", p_Model.ShortName);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddDineTime |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static NEW_DineTimeModel GetDineTime_By_Id(int id)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                NEW_DineTimeModel l_Model = new NEW_DineTimeModel();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_id_new_tbl_dine_time", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.DineTime = Convert.ToString(l_Reader["dinetime"]);
                    l_Model.ShortName = Convert.ToString(l_Reader["shortname"]);

                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetDineTime_By_Id\n" + ex.Message);
            }
        }


        public static List<NEW_DineTimeModel> GetDineTime_By_Column(string column, string value)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_DineTimeModel> l_Collection = new List<NEW_DineTimeModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_column_new_tbl_dine_time", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@column", column);
                l_Cmd.Parameters.AddWithValue("@value", value);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_DineTimeModel l_Model = new NEW_DineTimeModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.DineTime = Convert.ToString(l_Reader["dinetime"]);
                    l_Model.ShortName = Convert.ToString(l_Reader["shortname"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllDineTime\n" + ex.Message);
            }
        }

        public static void EditDineTime(NEW_DineTimeModel p_Model, int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_update_new_tbl_dine_time", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.Parameters.AddWithValue("@dinetime", p_Model.DineTime);
                l_Cmd.Parameters.AddWithValue("@shortname", p_Model.ShortName);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "EditDineTime |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DeleteDineTime(int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_delete_new_tbl_dine_time", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "DeleteDineTime |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static List<NEW_DineTimeModel> GetDineTime_By_Search(string value)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_DineTimeModel> l_Collection = new List<NEW_DineTimeModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("get_dine_time_by_search", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@value", value);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_DineTimeModel l_Model = new NEW_DineTimeModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.DineTime = Convert.ToString(l_Reader["dinetime"]);
                    l_Model.ShortName = Convert.ToString(l_Reader["shortname"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetDineTime_By_Search\n" + ex.Message);
            }
        }


    }
}