using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QolaMVC.Models;

namespace QolaMVC.DAL
{
    public class HomeDAL
    {
        private string _ConnectionString;
        private string _ConnectionStringDev;

        public HomeDAL()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;
            _ConnectionStringDev = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;
        }

        public List<HomeModel> GetHomes()
        {
            SqlConnection l_Conn = new SqlConnection(_ConnectionString);
            try
            {
                List<HomeModel> l_Collection = new List<HomeModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("Get_Home", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Collection.Add(new HomeModel(
                                                    Convert.ToInt32(l_Reader["fd_id"]),
                                                    Convert.ToString(l_Reader["fd_name"]),
                                                    Convert.ToString(l_Reader["fd_code"]),
                                                    Convert.ToString(l_Reader["fd_address"]),
                                                    Convert.ToString(l_Reader["fd_city"]),
                                                    Convert.ToInt32(l_Reader["fd_province"]),
                                                    Convert.ToString(l_Reader["ProvinceName"]),
                                                    Convert.ToString(l_Reader["fd_postal_code"]),
                                                    Convert.ToString(l_Reader["fd_country"]),
                                                    Convert.ToByte(l_Reader["fd_no_of_floor"]),
                                                    Convert.ToInt16(l_Reader["fd_no_of_suites"]),
                                                    Convert.ToString(l_Reader["fd_icon_image"]),
                                                    Convert.ToString(l_Reader["fd_status"]),
                                                    Convert.ToInt32(l_Reader["fd_modified_by"]),
                                                    Convert.ToDateTime(l_Reader["fd_modified_on"]),
                                                    Convert.ToString(l_Reader["fd_dine_time_Ids"]),
                                                    Convert.ToString(l_Reader["fd_phone"]),
                                                    new Guid(Convert.ToString(l_Reader["fd_GUID"])),
                                                    Convert.ToString(l_Reader["fd_pass_time_Ids"])
                                    ));
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetHomes\n" + ex.Message);
            }
        }

        public List<NamevalueIntModel> GetProvinces()
        {
            SqlConnection l_Conn = new SqlConnection(_ConnectionString);
            try
            {
                List<NamevalueIntModel> l_Collection = new List<NamevalueIntModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("Get_Province", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();

                while (l_Reader.Read())
                {
                    l_Collection.Add(new NamevalueIntModel(
                                                    Convert.ToInt32(l_Reader["ProvinceId"]),
                                                    Convert.ToString(l_Reader["ProvinceName"])
                                    ));
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetProvinces\n" + ex.Message);
            }
        }

        public List<ProvinceHomeModel> GetProvinceHomes(int p_UserId, int p_UserTypeId)
        {
            SqlConnection l_Conn = new SqlConnection(_ConnectionString);
            try
            {
                List<ProvinceHomeModel> l_Collection = new List<ProvinceHomeModel>();
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand("Get_Province_Homes", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@userId", p_UserId);
                l_Cmd.Parameters.AddWithValue("@userTypeId", p_UserTypeId);
                SqlDataReader l_Reader = l_Cmd.ExecuteReader();
               
                while (l_Reader.Read())
                {
                    l_Collection.Add(new ProvinceHomeModel(
                                                    Convert.ToInt32(l_Reader["fd_id"]),
                                                    Convert.ToString(l_Reader["fd_name"]),
                                                    Convert.ToInt32(l_Reader["fd_province"]),
                                                    Convert.ToString(l_Reader["ProvinceName"]),
                                                    Convert.ToString(l_Reader["fd_icon_image"]),
                                                    Convert.ToInt32(l_Reader["fd_Alert_Count"]),
                                                    Convert.ToInt32(l_Reader["fd_occupied"]),
                                                    Convert.ToInt32(l_Reader["fd_total_suite"]),
                                                    Convert.ToInt32(l_Reader["fd_qola_resident_count"])
                                    ));
                }

                return l_Collection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetProvinceHomes\n" + ex.Message);
            }
        }
    }
}