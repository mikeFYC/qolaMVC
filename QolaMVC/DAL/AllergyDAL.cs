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
    public class AllergyDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public AllergyDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public static List<NEW_AllergyModel> GetAllAllergy()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_AllergyModel> l_Collection = new List<NEW_AllergyModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_new_tbl_allergy", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_AllergyModel l_Model = new NEW_AllergyModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Allergy_Name = Convert.ToString(l_Reader["allergy_name"]);
                    l_Model.Category = Convert.ToString(l_Reader["category"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllAllergy\n" + ex.Message);
            }
        }

        public static void AddAllergy(NEW_AllergyModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_add_new_tbl_allergy", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@allergy_name", p_Model.Allergy_Name);
                l_Cmd.Parameters.AddWithValue("@category", p_Model.Category);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddAllergy |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static NEW_AllergyModel GetAllergy_By_Id(int id)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                NEW_AllergyModel l_Model = new NEW_AllergyModel();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_id_new_tbl_allergy", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Allergy_Name = Convert.ToString(l_Reader["allergy_name"]);
                    l_Model.Category = Convert.ToString(l_Reader["category"]);
                    
                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllergy_By_Id\n" + ex.Message);
            }
        }

        public static List<NEW_AllergyModel> GetAllergy_By_Column(string column, string value)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_AllergyModel> l_Collection = new List<NEW_AllergyModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_column_new_tbl_allergy", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@column", column);
                l_Cmd.Parameters.AddWithValue("@value", value);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_AllergyModel l_Model = new NEW_AllergyModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Allergy_Name = Convert.ToString(l_Reader["allergy_name"]);
                    l_Model.Category = Convert.ToString(l_Reader["category"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllergy_By_Column\n" + ex.Message);
            }
        }


        public static void EditAllergy(NEW_AllergyModel p_Model, int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_update_new_tbl_allergy", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.Parameters.AddWithValue("@allergy_name", p_Model.Allergy_Name);
                l_Cmd.Parameters.AddWithValue("@category", p_Model.Category);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "EditAllergy |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DeleteAllergy(int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_delete_new_tbl_allergy", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "DeleteAllergy |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static List<NEW_AllergyModel> GetAllergy_By_Search(string value)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_AllergyModel> l_Collection = new List<NEW_AllergyModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("get_allergy_by_search", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@value", value);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_AllergyModel l_Model = new NEW_AllergyModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Allergy_Name = Convert.ToString(l_Reader["allergy_name"]);
                    l_Model.Category = Convert.ToString(l_Reader["category"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllergy_By_Column\n" + ex.Message);
            }
        }

    }
}