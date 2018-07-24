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
    public class VenueDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public VenueDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public static List<NEW_VenueModel> GetAllVenue()
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                List<NEW_VenueModel> l_Collection = new List<NEW_VenueModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("sp_get_new_tbl_venue", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    NEW_VenueModel l_Model = new NEW_VenueModel();
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Home = Convert.ToString(l_Reader["home"]);
                    l_Model.Code = Convert.ToString(l_Reader["code"]);
                    l_Model.Venue = Convert.ToString(l_Reader["venue"]);

                    l_Collection.Add(l_Model);
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetAllVenue\n" + ex.Message);
            }
        }

        public static void AddVenue(NEW_VenueModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_add_new_tbl_venue", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@home", p_Model.Home);
                l_Cmd.Parameters.AddWithValue("@code", p_Model.Code);
                l_Cmd.Parameters.AddWithValue("@venue", p_Model.Venue);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddVenue |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static NEW_VenueModel GetVenue_By_Id(int id)
        {
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                NEW_VenueModel l_Model = new NEW_VenueModel();
                SqlCommand l_Cmd = new SqlCommand("sp_get_by_id_new_tbl_venue", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Model.Id = Convert.ToInt32(l_Reader["Id"]);
                    l_Model.Home = Convert.ToString(l_Reader["home"]);
                    l_Model.Code = Convert.ToString(l_Reader["code"]);
                    l_Model.Venue = Convert.ToString(l_Reader["venue"]);

                }

                return l_Model;
            }
            catch (Exception ex)
            {
                throw new Exception(".GetVenue_By_Id\n" + ex.Message);
            }
        }

        public static void EditVenue(NEW_VenueModel p_Model, int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_update_new_tbl_venue", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.Parameters.AddWithValue("@home", p_Model.Home);
                l_Cmd.Parameters.AddWithValue("@code", p_Model.Code);
                l_Cmd.Parameters.AddWithValue("@venue", p_Model.Venue);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "EditVenue |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DeleteVenue(int id)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("sp_delete_new_tbl_venue", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", id);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "DeleteVenue |" + ex.ToString();
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