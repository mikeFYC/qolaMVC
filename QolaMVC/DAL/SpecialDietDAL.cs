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
    public class SpecialDietDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public SpecialDietDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public static List<NEW_SpecialDietModel> GetAllSpecialDiet()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_SpecialDietModel> l_Collection = new List<NEW_SpecialDietModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_new_tbl_special_diet", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_SpecialDietModel l_Model = new NEW_SpecialDietModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Name = Convert.ToString(l_Reader["name"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllSpecialDiet\n" + ex.Message);
            }
        }

        public static void AddSpecialDiet(NEW_SpecialDietModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_add_new_tbl_special_diet", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@name", p_Model.Name);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddSpecialDiet |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static NEW_SpecialDietModel GetSpecialDiet_By_Id(int id)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                NEW_SpecialDietModel l_Model = new NEW_SpecialDietModel();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_id_new_tbl_special_diet", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Name = Convert.ToString(l_Reader["name"]);
                    
                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetSpecialDiet_By_Id\n" + ex.Message);
            }
        }

        public static void EditSpecialDiet(NEW_SpecialDietModel p_Model, int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_update_new_tbl_special_diet", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.Parameters.AddWithValue("@name", p_Model.Name);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "EditSpecialDiet |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DeleteSpecialDiet(int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_delete_new_tbl_special_diet", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "DeleteSpecialDiet |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }




    }
}