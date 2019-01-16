using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using QolaMVC.Models;
using static QolaMVC.Constants.EnumerationTypes;

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
                    //l_Collection.Add(new HomeModel(
                    //                                Convert.ToInt32(l_Reader["fd_id"]),
                    //                                Convert.ToString(l_Reader["fd_name"]),
                    //                                Convert.ToString(l_Reader["fd_code"]),
                    //                                Convert.ToString(l_Reader["fd_address"]),
                    //                                Convert.ToString(l_Reader["fd_city"]),
                    //                                Convert.ToInt32(l_Reader["fd_province"]),
                    //                                Convert.ToString(l_Reader["ProvinceName"]),
                    //                                Convert.ToString(l_Reader["fd_postal_code"]),
                    //                                Convert.ToString(l_Reader["fd_country"]),
                    //                                Convert.ToByte(l_Reader["fd_no_of_floor"]),
                    //                                Convert.ToInt16(l_Reader["fd_no_of_suites"]),
                    //                                Convert.ToString(l_Reader["fd_icon_image"]),
                    //                                Convert.ToString(l_Reader["fd_status"]),
                    //                                Convert.ToInt32(l_Reader["fd_modified_by"]),
                    //                                Convert.ToDateTime(l_Reader["fd_modified_on"]),
                    //                                Convert.ToString(l_Reader["fd_dine_time_Ids"]),
                    //                                Convert.ToString(l_Reader["fd_phone"]),
                    //                                new Guid(Convert.ToString(l_Reader["fd_GUID"])),
                    //                                Convert.ToString(l_Reader["fd_pass_time_Ids"])
                    //                ));
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

        public static int AddNewHome(HomeModel addHome)
        {
            string exception = string.Empty;
            int homeId = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_HOME, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //db.AddOutParameter(cmd, "@id", addHome.ID);
                l_Cmd.Parameters.AddWithValue("@name", addHome.Name);
                l_Cmd.Parameters.AddWithValue("@code", addHome.Code);
                l_Cmd.Parameters.AddWithValue("@address", addHome.Address);
                l_Cmd.Parameters.AddWithValue("@city", addHome.City);
                l_Cmd.Parameters.AddWithValue("@province", addHome.Province.ID);
                l_Cmd.Parameters.AddWithValue("@postalCode", addHome.PostalCode);
                l_Cmd.Parameters.AddWithValue("@country", addHome.Country);
                l_Cmd.Parameters.AddWithValue("@noOfFloor", addHome.NumberOfFloors);
                l_Cmd.Parameters.AddWithValue("@noOfSuites", addHome.NumberOfSuites);
                l_Cmd.Parameters.AddWithValue("@iconImage", addHome.IconImage);
                l_Cmd.Parameters.AddWithValue("@status", addHome.status_mike);
                

                l_Cmd.Parameters.AddWithValue("@createdby", addHome.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@dineTimeIds", addHome.DineTimeIds);
                l_Cmd.Parameters.AddWithValue("@phone", addHome.Phone);
                l_Cmd.Parameters.AddWithValue("@passTimeIds", addHome.PassTimeIds);

                l_Cmd.Parameters.Add("@id", SqlDbType.Int);
                l_Cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                l_Cmd.ExecuteNonQuery();
                homeId = int.Parse(l_Cmd.Parameters["@id"].Value.ToString());
                //homeId = l_Cmd.ExecuteNonQuery();
                if (homeId > 0)
                {
                    //homeId = Convert.ToInt32(db.GetParameterValue(cmd, "@id"));
                }
                return homeId;
            }
            catch (Exception ex)
            {
                exception = "AddNewHome |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool UpdateHome(HomeModel updateHome)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_HOME, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", updateHome.Id);
                l_Cmd.Parameters.AddWithValue("@name", updateHome.Name);
                l_Cmd.Parameters.AddWithValue("@code", updateHome.Code);
                l_Cmd.Parameters.AddWithValue("@address", updateHome.Address);
                l_Cmd.Parameters.AddWithValue("@city", updateHome.City);
                l_Cmd.Parameters.AddWithValue("@province", updateHome.Province.ID);
                l_Cmd.Parameters.AddWithValue("@postalCode", updateHome.PostalCode);
                l_Cmd.Parameters.AddWithValue("@country", updateHome.Country);
                l_Cmd.Parameters.AddWithValue("@noOfFloor", updateHome.NumberOfFloors);
                l_Cmd.Parameters.AddWithValue("@noOfSuites", updateHome.NumberOfSuites);
                l_Cmd.Parameters.AddWithValue("@iconImage", updateHome.IconImage);
                l_Cmd.Parameters.AddWithValue("@status", updateHome.status_mike);
                l_Cmd.Parameters.AddWithValue("@createdby", updateHome.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@dineTimeIds", updateHome.DineTimeIds);
                l_Cmd.Parameters.AddWithValue("@phone", updateHome.Phone);
                l_Cmd.Parameters.AddWithValue("@passTimeIds", updateHome.PassTimeIds);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "UpdateHome |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool RemoveHome_InactiveforNow(int homeId)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("Inactive_Home", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", homeId);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "RemoveHome |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<HomeModel> GetHomeCollections()
        {
            string exception = string.Empty;
            Collection<HomeModel> homes = new Collection<HomeModel>();
            HomeModel home;
            ProvinceModel province;
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

                        home = new HomeModel();
                        province = new ProvinceModel();
                        home.Id = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        home.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_name"]);
                        home.Code = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_code"]);
                        home.Address = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_address"]);
                        home.City = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_city"]);
                        home.NumberOfSuites = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_no_of_suites"]);
                        home.Country = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_country"]);
                        home.NumberOfFloors = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_no_of_floor"]);
                        province.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_province"]);
                        province.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["ProvinceName"]);
                        home.Province = province;
                        home.PostalCode = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_postal_code"]);
                        home.Phone = homesReceive.Tables[0].Rows[index]["fd_phone"] == DBNull.Value ? string.Empty : Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_phone"]);
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetHomeCollections |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static List<HomeModel> GetHomeCollections_mike()
        {
            string exception = string.Empty;
            List<HomeModel> homes = new List<HomeModel>();
            HomeModel home;
            ProvinceModel province;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("Get_Home_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;

                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if (homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {

                        home = new HomeModel();
                        province = new ProvinceModel();
                        home.Id = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        home.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_name"]);
                        home.Code = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_code"]);
                        home.Address = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_address"]);
                        home.City = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_city"]);
                        home.NumberOfSuites = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_no_of_suites"]);
                        home.Country = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_country"]);
                        home.NumberOfFloors = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_no_of_floor"]);
                        province.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_province"]);
                        province.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["ProvinceName"]);
                        home.Province = province;
                        home.PostalCode = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_postal_code"]);
                        home.Phone = homesReceive.Tables[0].Rows[index]["fd_phone"] == DBNull.Value ? string.Empty : Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_phone"]);
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetHomeCollections_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static List<HomeModel> GetHomeCollections_mike_Filter(string str)
        {
            string exception = string.Empty;
            List<HomeModel> homes = new List<HomeModel>();
            HomeModel home;
            ProvinceModel province;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("Get_Home_mike_Filter", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@str", str);
                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if (homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {

                        home = new HomeModel();
                        province = new ProvinceModel();
                        home.Id = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        home.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_name"]);
                        home.Code = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_code"]);
                        home.Address = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_address"]);
                        home.City = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_city"]);
                        home.NumberOfSuites = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_no_of_suites"]);
                        home.Country = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_country"]);
                        home.NumberOfFloors = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_no_of_floor"]);
                        province.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_province"]);
                        province.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["ProvinceName"]);
                        home.Province = province;
                        home.PostalCode = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_postal_code"]);
                        home.Phone = homesReceive.Tables[0].Rows[index]["fd_phone"] == DBNull.Value ? string.Empty : Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_phone"]);
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetHomeCollections_mike_Filter |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<HomeModel> GetHomeFill(int userId, int userTypeId, string flag = null)
        {
            string exception = string.Empty;
            Collection<HomeModel> homes = new Collection<HomeModel>();
            HomeModel home;
            ProvinceModel l_Province;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_PROVINCE_HOMES, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@userId", userId);
                l_Cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                if (flag != null)
                {
                    l_Cmd.Parameters.AddWithValue("@flag", flag);
                }
                l_Cmd.CommandTimeout = 10000;

                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if (homesReceive != null & homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index < homesReceive.Tables[0].Rows.Count; index++)
                    {
                        home = new HomeModel();
                        l_Province = new ProvinceModel();
                        home.Id = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        home.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_name"]);
                        l_Province.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_province"]);
                        l_Province.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["ProvinceName"]);
                        home.Province = l_Province;
                        home.ProvinceName = l_Province.Name;
                        home.IconImage = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_icon_image"]);
                        home.NumberOfSuites = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_Alert_Count"]);
                        home.OccupiedSuites = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_occupied"]);
                        home.TotalSuites = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_total_suite"]);
                        home.NumberOfSuites = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_no_of_suites"]);
                        home.PostalCode = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_qola_resident_count"]);
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetHomeFill |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static HomeModel GetHomeById(int homeId)
        {
            string exception = string.Empty;
            HomeModel home = new HomeModel();
            ProvinceModel l_Province = new ProvinceModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_HOME_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", homeId);
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) & homeReceive.Tables.Count > 0)
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {

                        home.Id = Convert.ToInt32(homeTypeRow["fd_id"]);
                        home.Name = Convert.ToString(homeTypeRow["fd_name"]);
                        home.Code = Convert.ToString(homeTypeRow["fd_code"]);
                        home.Address = Convert.ToString(homeTypeRow["fd_address"]);
                        home.City = Convert.ToString(homeTypeRow["fd_city"]);
                        l_Province.ID = Convert.ToInt32(homeTypeRow["fd_province"]);
                        home.Province = l_Province;
                        home.PostalCode = Convert.ToString(homeTypeRow["fd_postal_code"]);
                        home.Country = Convert.ToString(homeTypeRow["fd_country"]);
                        home.NumberOfFloors = Convert.ToInt32(homeTypeRow["fd_no_of_floor"]);
                        home.NumberOfSuites = Convert.ToInt32(homeTypeRow["fd_no_of_suites"]);
                        home.IconImage = Convert.ToString(homeTypeRow["fd_icon_image"]);
                        home.Phone = homeTypeRow["fd_phone"] == DBNull.Value ? string.Empty : Convert.ToString(homeTypeRow["fd_phone"]);
                        if (homeTypeRow["fd_status"].ToString() == "A")
                        {
                            home.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            home.Status = AvailabilityStatus.I;
                        }
                        home.DineTimeIds = Convert.ToString(homeTypeRow["fd_dine_time_ids"]);
                        home.PassTimeIds = Convert.ToString(homeTypeRow["fd_pass_time_Ids"] != DBNull.Value ? homeTypeRow["fd_pass_time_Ids"] : string.Empty);
                    }
                }
                return home;
            }
            catch (Exception ex)
            {
                exception = "GetHomeById |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static string GetOccupybyID(int homeId)
        {
            string occu = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select count(*) from tbl_Suite_Handler " +
                                " where GETDATE()> fd_move_in_date" +
                                " and GETDATE()< isNULL(fd_move_out_date, '2200-09-01')" +
                                " and fd_home_id ="+ homeId;
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    occu = rd[0].ToString();


                }
            conn.Close();

            return occu;
        }

        public static Collection<HomeModel> GetUsersHomeActive()
        {
            string exception = string.Empty;
            Collection<HomeModel> homes = new Collection<HomeModel>();
            HomeModel home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_BY_USER_ACTIVE, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet dsData = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsData);

                if (dsData != null & dsData.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index < dsData.Tables[0].Rows.Count; index++)
                    {
                        home = new HomeModel();
                        home.Id = Convert.ToInt32(dsData.Tables[0].Rows[index]["fd_id"]);
                        home.Name = Convert.ToString(dsData.Tables[0].Rows[index]["fd_name"]);
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetUsersHomeActive |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<HomeModel> GetHomeByProvince(int userID, int strProvince)
        {
            string exception = string.Empty;
            Collection<HomeModel> homes = new Collection<HomeModel>();
            HomeModel home;
            ProvinceModel l_Province;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_HOME_BY_PROVINCE, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@userId", userID);
                l_Cmd.Parameters.AddWithValue("@province", strProvince);
                DataSet dsData = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsData);

                if (dsData != null & dsData.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index < dsData.Tables[0].Rows.Count; index++)
                    {
                        home = new HomeModel();
                        l_Province = new ProvinceModel();
                        home.Id = Convert.ToInt32(dsData.Tables[0].Rows[index]["fd_id"]);
                        home.Name = Convert.ToString(dsData.Tables[0].Rows[index]["fd_name"]);
                        l_Province.ID = Convert.ToInt32(dsData.Tables[0].Rows[index]["fd_province"]);
                        home.Province = l_Province;
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetHomeByProvince |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet GetProvince(int iUserId, int iUserTypeId)
        {
            string exception = string.Empty;
            DataSet ds = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_PROVINCE, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@userId", iUserId);
                l_Cmd.Parameters.AddWithValue("@userTypeId", iUserTypeId);
                ds = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                exception = "GetProvince |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<HomeModel> GetHomeByUser(int userID)
        {
            string exception = string.Empty;
            Collection<HomeModel> homes = new Collection<HomeModel>();
            HomeModel home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_HOME_BY_USER, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@userId", userID);
                DataSet dsData = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsData);

                if (dsData != null & dsData.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index < dsData.Tables[0].Rows.Count; index++)
                    {
                        home = new HomeModel();
                        home.Id = Convert.ToInt32(dsData.Tables[0].Rows[index]["fd_id"]);
                        home.Name = Convert.ToString(dsData.Tables[0].Rows[index]["fd_name"]);
                        homes.Add(home);
                    }
                }
                return homes;
            }
            catch (Exception ex)
            {
                exception = "GetHomeByUser |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static HomeModel ValidateHomeByUserId(int homeId, int iUserId)
        {
            string exception = string.Empty;
            HomeModel home = new HomeModel();
            ProvinceModel l_Province = new ProvinceModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_VALIDATE_HOME_BY_USERID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@HomeId", homeId);
                l_Cmd.Parameters.AddWithValue("@UserId", iUserId);
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        home.Id = Convert.ToInt32(homeTypeRow["fd_id"]);
                        home.Name = Convert.ToString(homeTypeRow["fd_name"]);
                        l_Province.ID = Convert.ToInt32(homeTypeRow["fd_province"]);
                        home.Province = l_Province;
                    }
                }
                return home;
            }
            catch (Exception ex)
            {
                exception = "ValidateHomeByUserId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<DietaryAllergyReportModel> GetDietaryAllergyReport(int p_HomeId)
        {
            string exception = string.Empty;
            Collection<DietaryAllergyReportModel> l_Reports = new Collection<DietaryAllergyReportModel>();
            DietaryAllergyReportModel l_Report;
            UserModel l_User;
            ResidentModel l_Resident;
            SuiteModel l_Suite;
            AllergiesModel l_Allergy;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_DietaryAssessmentAllergies", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@HomeId", p_HomeId);
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Report = new DietaryAllergyReportModel();
                        l_User = new UserModel();
                        l_Resident = new ResidentModel();
                        l_Suite = new SuiteModel();
                        l_Allergy = new AllergiesModel();
                        
                        l_Report.Id = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Resident.ShortName = Convert.ToString(homeTypeRow["ResidentName"]);
                        l_Resident.ID = Convert.ToInt32(homeTypeRow["ResidentId"]);
                        l_Suite.SuiteNo = Convert.ToString(homeTypeRow["Suite"]);
                        l_Allergy.ID = Convert.ToInt32(homeTypeRow["AllergyId"]);
                        l_Allergy.Name = Convert.ToString(homeTypeRow["Allergy"]);
                        l_User.ID = Convert.ToInt16(homeTypeRow["EnteredBy"]);
                        l_Report.DateEntered = Convert.ToDateTime(homeTypeRow["DateEntered"]);

                        l_Report.Allergy = l_Allergy;
                        l_Report.Resident = l_Resident;
                        l_Report.Suite = l_Suite;

                        l_Reports.Add(l_Report);
                    }
                }
                return l_Reports;
            }
            catch (Exception ex)
            {
                exception = "GetDietaryAllergyReport |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<SpecialDietReportModel> GetSpecialDietReport(int p_HomeId)
        {
            string exception = string.Empty;
            Collection<SpecialDietReportModel> l_Reports = new Collection<SpecialDietReportModel>();
            SpecialDietReportModel l_Report;
            UserModel l_User;
            ResidentModel l_Resident;
            SuiteModel l_Suite;
            
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_DietaryAssessmentDiets", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@HomeId", p_HomeId);
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Report = new SpecialDietReportModel();
                        l_User = new UserModel();
                        l_Resident = new ResidentModel();
                        l_Suite = new SuiteModel();
                        
                        l_Report.Id = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Resident.ShortName = Convert.ToString(homeTypeRow["ResidentName"]);
                        l_Resident.ID = Convert.ToInt32(homeTypeRow["ResidentId"]);
                        l_Suite.SuiteNo = Convert.ToString(homeTypeRow["Suite"]);
                        l_Report.Diet = Convert.ToString(homeTypeRow["Diet"]);
                        l_Report.Likes = Convert.ToString(homeTypeRow["Likes"]);
                        l_Report.DisLikes = Convert.ToString(homeTypeRow["DisLikes"]);
                        l_User.ID = Convert.ToInt16(homeTypeRow["EnteredBy"]);
                        l_Report.DateEntered = Convert.ToDateTime(homeTypeRow["DateEntered"]);

                        l_Report.Resident = l_Resident;
                        l_Report.Suite = l_Suite;

                        l_Reports.Add(l_Report);
                    }
                }
                return l_Reports;
            }
            catch (Exception ex)
            {
                exception = "GetSpecialDietReport |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<DietaryLikesModel> GetLikesReport(int p_HomeId)
        {
            string exception = string.Empty;
            Collection<DietaryLikesModel> l_Reports = new Collection<DietaryLikesModel>();
            DietaryLikesModel l_Report;
            UserModel l_User;
            ResidentModel l_Resident;
            SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_DietaryAssessment_Likes", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@HomeId", p_HomeId);
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Report = new DietaryLikesModel();
                        l_User = new UserModel();
                        l_Resident = new ResidentModel();
                        l_Suite = new SuiteModel();

                        l_Resident.ShortName = Convert.ToString(homeTypeRow["ResidentName"]);
                        l_Resident.ID = Convert.ToInt32(homeTypeRow["ResidentId"]);
                        l_Suite.SuiteNo = Convert.ToString(homeTypeRow["Suite"]);
                        l_Report.Likes = Convert.ToString(homeTypeRow["Likes"]);
                        l_Report.DateEntered = Convert.ToDateTime(homeTypeRow["DateEntered"]);

                        l_Report.Resident = l_Resident;
                        l_Report.Suite = l_Suite;

                        l_Reports.Add(l_Report);
                    }
                }
                return l_Reports;
            }
            catch (Exception ex)
            {
                exception = "GetLikesReport |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<DietaryLikesModel> GetDisLikesReport(int p_HomeId)
        {
            string exception = string.Empty;
            Collection<DietaryLikesModel> l_Reports = new Collection<DietaryLikesModel>();
            DietaryLikesModel l_Report;
            UserModel l_User;
            ResidentModel l_Resident;
            SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_DietaryAssessment_DisLikes", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@HomeId", p_HomeId);
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Report = new DietaryLikesModel();
                        l_User = new UserModel();
                        l_Resident = new ResidentModel();
                        l_Suite = new SuiteModel();

                        l_Resident.ShortName = Convert.ToString(homeTypeRow["ResidentName"]);
                        l_Resident.ID = Convert.ToInt32(homeTypeRow["ResidentId"]);
                        l_Suite.SuiteNo = Convert.ToString(homeTypeRow["Suite"]);
                        l_Report.DisLikes = Convert.ToString(homeTypeRow["DisLikes"]);
                        l_Report.DateEntered = Convert.ToDateTime(homeTypeRow["DateEntered"]);

                        l_Report.Resident = l_Resident;
                        l_Report.Suite = l_Suite;

                        l_Reports.Add(l_Report);
                    }
                }
                return l_Reports;
            }
            catch (Exception ex)
            {
                exception = "GetLikesReport |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }











        #region tabs in activity calendar

        public static Collection<ActivityEventModel> GetActivityEvents_mike(DateTime datemike, int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("[spAB_Get_Activity_Events_mike]", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["venueName"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel_Calendar2> GetActivityEvents_Calendar2_mike(DateTime datemike, int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel_Calendar2> l_Events = new Collection<ActivityEventModel_Calendar2>();
            ActivityEventModel_Calendar2 l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_mike_2", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel_Calendar2();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["venueName"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel_Calendar3> GetActivityEvents_Calendar3_mike(DateTime datemike, int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel_Calendar3> l_Events = new Collection<ActivityEventModel_Calendar3>();
            ActivityEventModel_Calendar3 l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_mike_3", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel_Calendar3();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["venueName"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel_Calendar4> GetActivityEvents_Calendar4_mike(DateTime datemike, int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel_Calendar4> l_Events = new Collection<ActivityEventModel_Calendar4>();
            ActivityEventModel_Calendar4 l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_mike_4", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel_Calendar4();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["venueName"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        #endregion

        #region SAC

        public static Collection<ActivityEventModel> GetActivityEvents_SACnotColor(int homeid,int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_SACnotColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents_SACColor(int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_SACColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents_C2_SACnotColor(int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C2_SACnotColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents_C2_SACColor(int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C2_SACColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents_C3_SACnotColor(int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C3_SACnotColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents_C3_SACColor(int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C3_SACColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents_C4_SACnotColor(int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C4_SACnotColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents_C4_SACColor(int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C4_SACColor", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static Collection<ActivityEventModel> GetActivityEvents_mike_SAC(DateTime datemike, int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("[spAB_Get_Activity_Events_mike_SAC]", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Event.Color = Convert.ToString(homeTypeRow["Color"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel_Calendar2> GetActivityEvents_Calendar2_mike_SAC(DateTime datemike, int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel_Calendar2> l_Events = new Collection<ActivityEventModel_Calendar2>();
            ActivityEventModel_Calendar2 l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_mike_2_SAC", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel_Calendar2();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Event.Color = Convert.ToString(homeTypeRow["Color"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel_Calendar3> GetActivityEvents_Calendar3_mike_SAC(DateTime datemike, int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel_Calendar3> l_Events = new Collection<ActivityEventModel_Calendar3>();
            ActivityEventModel_Calendar3 l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_mike_3_SAC", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel_Calendar3();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Event.Color = Convert.ToString(homeTypeRow["Color"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel_Calendar4> GetActivityEvents_Calendar4_mike_SAC(DateTime datemike, int homeid, int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel_Calendar4> l_Events = new Collection<ActivityEventModel_Calendar4>();
            ActivityEventModel_Calendar4 l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_mike_4_SAC", l_Conn);
                l_Cmd.Parameters.AddWithValue("@date", datemike.ToShortDateString());
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel_Calendar4();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToDateTime(homeTypeRow["StartTime"].ToString()).ToShortTimeString();
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.Active = Convert.ToInt32(homeTypeRow["Active"]);
                        l_Event.Declined = Convert.ToInt32(homeTypeRow["Declined"]);

                        l_Event.ActivityNameEnglish = homeTypeRow["ActivityNameEnglish"].ToString();
                        l_Event.CategoryId = homeTypeRow["CategoryId"].ToString();

                        l_Event.Color = Convert.ToString(homeTypeRow["Color"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }



        #endregion




        public static int GetCategoryIdbyActivityID(int activityID)
        {
            int retunvalue = 0;
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_CategoryId_by_ActivityID", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ActivityId", activityID);
                l_Cmd.Parameters.Add("@out", SqlDbType.VarChar, 20);
                l_Cmd.Parameters["@out"].Direction = ParameterDirection.Output;
                l_Cmd.ExecuteNonQuery();
                retunvalue = int.Parse(l_Cmd.Parameters["@out"].Value.ToString());
            }
            catch (Exception ex)
            {
                exception = "GetCategoryIdbyActivityID |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();

            }
            return retunvalue;
        }

        public static string GetCodebyVenue(int VenueID)
        {
            string retunvalue = "";
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Code_by_VenueID", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Venue", VenueID);
                l_Cmd.Parameters.Add("@out", SqlDbType.VarChar, 20);
                l_Cmd.Parameters["@out"].Direction = ParameterDirection.Output;
                l_Cmd.ExecuteNonQuery();
                retunvalue = l_Cmd.Parameters["@out"].Value.ToString();
            }
            catch (Exception ex)
            {
                exception = "GetCodebyVenue |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();

            }
            return retunvalue;
        }

        public static int AddNewActivityEvent_All(ActivityEventModel p_Model, int homeid, int Special,string tab)
        {
            int retunvalue = 0;
            string exception = string.Empty;
            string storeString = "";

            if(tab=="" || tab == "a")   storeString = "spAB_Add_Activity_Events";
            else if (tab == "b")        storeString = "spAB_Add_Activity_Events_C2";
            else if (tab == "c")        storeString = "spAB_Add_Activity_Events_C3";
            else if (tab == "d")        storeString = "spAB_Add_Activity_Events_C4";

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(storeString, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_Model.ActivityId);
                l_Cmd.Parameters.AddWithValue("@EventTitle", p_Model.ProgramName);
                l_Cmd.Parameters.AddWithValue("@StartDate", p_Model.ProgramStartDate);
                l_Cmd.Parameters.AddWithValue("@EndDate", p_Model.ProgramEndDate);
                l_Cmd.Parameters.AddWithValue("@StartTime", p_Model.ProgramStartTime);
                l_Cmd.Parameters.AddWithValue("@EndTime", p_Model.ProgramEndTime);
                l_Cmd.Parameters.AddWithValue("@Venue", p_Model.Venue);
                l_Cmd.Parameters.AddWithValue("@Note", p_Model.note);
                l_Cmd.Parameters.AddWithValue("@homtId", homeid);
                l_Cmd.Parameters.AddWithValue("@Special", Special);
                l_Cmd.Parameters.Add("@out", SqlDbType.VarChar, 20);
                l_Cmd.Parameters["@out"].Direction = ParameterDirection.Output;
                l_Cmd.ExecuteNonQuery();
                retunvalue = int.Parse(l_Cmd.Parameters["@out"].Value.ToString());


            }
            catch (Exception ex)
            {
                exception = "AddNewActivityEvent_All |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();

            }
            return retunvalue;
        }

        public static void EditNewActivityEvent_All(int EventID, ActivityEventModel p_Model, int homeid,string tab)
        {
            string exception = string.Empty;
            string storeString = "";

            if (tab == "" || tab == "a") storeString = "spAB_EDIT_Activity_Events";
            else if (tab == "b")        storeString = "spAB_EDIT_Activity_Events_C2";
            else if (tab == "c")        storeString = "spAB_EDIT_Activity_Events_C3";
            else if (tab == "d")        storeString = "spAB_EDIT_Activity_Events_C4";

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(storeString, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@EventID", EventID);
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_Model.ActivityId);
                l_Cmd.Parameters.AddWithValue("@EventTitle", p_Model.ProgramName);
                l_Cmd.Parameters.AddWithValue("@StartDate", p_Model.ProgramStartDate);
                l_Cmd.Parameters.AddWithValue("@EndDate", p_Model.ProgramEndDate);
                l_Cmd.Parameters.AddWithValue("@StartTime", p_Model.ProgramStartTime);
                l_Cmd.Parameters.AddWithValue("@EndTime", p_Model.ProgramEndTime);
                l_Cmd.Parameters.AddWithValue("@Venue", p_Model.Venue);
                l_Cmd.Parameters.AddWithValue("@Note", p_Model.note);
                l_Cmd.Parameters.AddWithValue("@homtId", homeid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EditNewActivityEvent_All |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetActivityEvents(int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel> GetActivityEvents_between(int homeid,string start,string end)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_between", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@start", start);
                l_Cmd.Parameters.AddWithValue("@end", end);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void EditNewActivityEvent(int EventID,ActivityEventModel p_Model, int homeid)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_EDIT_Activity_Events", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@EventID", EventID);
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_Model.ActivityId);
                l_Cmd.Parameters.AddWithValue("@EventTitle", p_Model.ProgramName);
                l_Cmd.Parameters.AddWithValue("@StartDate", p_Model.ProgramStartDate);
                l_Cmd.Parameters.AddWithValue("@EndDate", p_Model.ProgramEndDate);
                l_Cmd.Parameters.AddWithValue("@StartTime", p_Model.ProgramStartTime);
                l_Cmd.Parameters.AddWithValue("@EndTime", p_Model.ProgramEndTime);
                l_Cmd.Parameters.AddWithValue("@Venue", p_Model.Venue);
                l_Cmd.Parameters.AddWithValue("@Note", p_Model.note);
                l_Cmd.Parameters.AddWithValue("@homtId", homeid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EditNewActivityEvent |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static Collection<ActivityEventModel> GetActivityEvents_C2(int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C2", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel> GetActivityEvents_C2_between(int homeid, string start, string end)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C2_between", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@start", start);
                l_Cmd.Parameters.AddWithValue("@end", end);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void EditNewActivityEvent_C2(int EventID, ActivityEventModel p_Model, int homeid)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_EDIT_Activity_Events_C2", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@EventID", EventID);
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_Model.ActivityId);
                l_Cmd.Parameters.AddWithValue("@EventTitle", p_Model.ProgramName);
                l_Cmd.Parameters.AddWithValue("@StartDate", p_Model.ProgramStartDate);
                l_Cmd.Parameters.AddWithValue("@EndDate", p_Model.ProgramEndDate);
                l_Cmd.Parameters.AddWithValue("@StartTime", p_Model.ProgramStartTime);
                l_Cmd.Parameters.AddWithValue("@EndTime", p_Model.ProgramEndTime);
                l_Cmd.Parameters.AddWithValue("@Venue", p_Model.Venue);
                l_Cmd.Parameters.AddWithValue("@Note", p_Model.note);
                l_Cmd.Parameters.AddWithValue("@homtId", homeid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EditNewActivityEvent_C2 |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static Collection<ActivityEventModel> GetActivityEvents_C3(int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C3", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel> GetActivityEvents_C3_between(int homeid, string start, string end)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C3_between", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@start", start);
                l_Cmd.Parameters.AddWithValue("@end", end);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void EditNewActivityEvent_C3(int EventID, ActivityEventModel p_Model, int homeid)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_EDIT_Activity_Events_C3", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@EventID", EventID);
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_Model.ActivityId);
                l_Cmd.Parameters.AddWithValue("@EventTitle", p_Model.ProgramName);
                l_Cmd.Parameters.AddWithValue("@StartDate", p_Model.ProgramStartDate);
                l_Cmd.Parameters.AddWithValue("@EndDate", p_Model.ProgramEndDate);
                l_Cmd.Parameters.AddWithValue("@StartTime", p_Model.ProgramStartTime);
                l_Cmd.Parameters.AddWithValue("@EndTime", p_Model.ProgramEndTime);
                l_Cmd.Parameters.AddWithValue("@Venue", p_Model.Venue);
                l_Cmd.Parameters.AddWithValue("@Note", p_Model.note);
                l_Cmd.Parameters.AddWithValue("@homtId", homeid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EditNewActivityEvent_C3 |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static Collection<ActivityEventModel> GetActivityEvents_C4(int homeid)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C4", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel> GetActivityEvents_C4_between(int homeid, string start, string end)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Activity_Events_C4_between", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@start", start);
                l_Cmd.Parameters.AddWithValue("@end", end);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);
                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);

                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void EditNewActivityEvent_C4(int EventID, ActivityEventModel p_Model, int homeid)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_EDIT_Activity_Events_C4", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@EventID", EventID);
                l_Cmd.Parameters.AddWithValue("@ActivityId", p_Model.ActivityId);
                l_Cmd.Parameters.AddWithValue("@EventTitle", p_Model.ProgramName);
                l_Cmd.Parameters.AddWithValue("@StartDate", p_Model.ProgramStartDate);
                l_Cmd.Parameters.AddWithValue("@EndDate", p_Model.ProgramEndDate);
                l_Cmd.Parameters.AddWithValue("@StartTime", p_Model.ProgramStartTime);
                l_Cmd.Parameters.AddWithValue("@EndTime", p_Model.ProgramEndTime);
                l_Cmd.Parameters.AddWithValue("@Venue", p_Model.Venue);
                l_Cmd.Parameters.AddWithValue("@Note", p_Model.note);
                l_Cmd.Parameters.AddWithValue("@homtId", homeid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EditNewActivityEvent_C4 |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }





        public static StringBuilder get_listview(int homeid, DateTime todaydate)
        {
            StringBuilder table = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " with tab as (" +
                                " select distinct R.fd_first_name + ' ' + R.fd_last_name as fd_full_name, SH.fd_resident_id,SH.fd_suite_id," +
                                " SH.fd_home_id from[dbo].[tbl_Suite_Handler] SH left join[dbo].[tbl_Resident] R on R.fd_id=SH.fd_resident_id" +
                                " where SH.fd_home_id=@homeid and @GETDATE>SH.fd_move_in_date and @GETDATE<isNULL(SH.fd_move_out_date,'2200-09-13')" +
                                " ) select fd_full_name from tab" ;

            cmd.Parameters.AddWithValue("@GETDATE", todaydate);
            cmd.Parameters.AddWithValue("@homeid", homeid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            table.Append("<table class=\"attend\">");
            table.Append("<tbody>");

            if (rd.HasRows)
                while (rd.Read())
                {
                    table.Append("<tr>");
                    table.Append("<td>"+rd[0]+"</td>");
                    table.Append("<td>");
                    table.Append("<div class=\"form-check form-check-inline\">");
                    table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                    table.Append("<label class=\"form-check-label\">T</label>");
                    table.Append("</div>");
                    table.Append("</td>");
                    table.Append("<td>");
                    table.Append("<div class=\"form-check form-check-inline\">");
                    table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                    table.Append("<label class=\"form-check-label\">R</label>");
                    table.Append("</div>");
                    table.Append("</td>");
                    table.Append("<td>");
                    table.Append("<div class=\"form-check form-check-inline\">");
                    table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                    table.Append("<label class=\"form-check-label\">H</label>");
                    table.Append("</div>");
                    table.Append("</td>");
                    table.Append("<td>");
                    table.Append("<div class=\"form-check form-check-inline\">");
                    table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                    table.Append("<label class=\"form-check-label\">W</label>");
                    table.Append("</div>");
                    table.Append("</td>");
                    table.Append("<td>");
                    table.Append("<div class=\"form-check form-check-inline\">");
                    table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                    table.Append("<label class=\"form-check-label\">A</label>");
                    table.Append("</div>");
                    table.Append("</td>");
                    table.Append("<td>");
                    table.Append("<div class=\"form-check form-check-inline\">");
                    table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                    table.Append("<label class=\"form-check-label\">TC</label>");
                    table.Append("</div>");
                    table.Append("</td>");
                    table.Append("<td>");
                    table.Append("<div class=\"form-check form-check-inline\">");
                    table.Append("<i class=\"fa fa-file-text-o\"></i>");
                    table.Append("</div>");
                    table.Append("</td>");
                    table.Append("</tr>");
                }
            table.Append("</tbody>");
            table.Append("</table>");
            return table;
        }

        public static StringBuilder get_floorview(int homeid, DateTime todaydate)
        {
            int floor_number=0;
            StringBuilder table = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " with tab as (" +
                                " select distinct R.fd_first_name+' '+R.fd_last_name as fd_full_name,R.fd_gender, SH.fd_resident_id,SH.fd_suite_id,SH.fd_home_id," +
                                " S.fd_suite_no,S.fd_floor from[dbo].[tbl_Suite_Handler] SH join[dbo].[tbl_Resident] R on R.fd_id=SH.fd_resident_id join [dbo].[tbl_Suite] S on S.fd_id=SH.fd_suite_id" +
                                " where SH.fd_home_id=@homeid and @GETDATE>SH.fd_move_in_date and @GETDATE<isNULL(SH.fd_move_out_date,'2200-09-13')" +
                                " ) select max(fd_floor) from tab";

            cmd.Parameters.AddWithValue("@GETDATE", todaydate);
            cmd.Parameters.AddWithValue("@homeid", homeid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    floor_number = int.Parse(rd[0].ToString());
                }
            rd.Close();
            for (int a = 1; a <= floor_number; a++)
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandText =   " with tab as (" +
                                    " select distinct R.fd_first_name+' '+R.fd_last_name as fd_full_name,R.fd_gender, SH.fd_resident_id,SH.fd_suite_id,SH.fd_home_id," +
                                    " S.fd_suite_no,S.fd_floor from[dbo].[tbl_Suite_Handler] SH join[dbo].[tbl_Resident] R on R.fd_id=SH.fd_resident_id join [dbo].[tbl_Suite] S on S.fd_id=SH.fd_suite_id" +
                                    " where SH.fd_home_id=@homeid and @GETDATE>SH.fd_move_in_date and @GETDATE<isNULL(SH.fd_move_out_date,'2200-09-13')" +
                                    " )  select fd_full_name from tab where fd_floor=" + a;

                cmd2.Parameters.AddWithValue("@GETDATE", todaydate);
                cmd2.Parameters.AddWithValue("@homeid", homeid);
                cmd2.Connection = conn;
                SqlDataReader rd2 = cmd2.ExecuteReader();
                table.Append("<div class=\"col-md-6\" style=\"border:1px solid #000;min-width:83.5vw\">");
                table.Append("<table class=\"attend\">");
                table.Append("<tbody>");
                table.Append("<tr>");
                table.Append("<th colspan = \"7\" height=30vw> " + a + "st Floor</th >");
                table.Append("</tr>");
                if (rd2.HasRows)
                    while (rd2.Read())
                    {


                        table.Append("<tr>");
                        table.Append("<td>" + rd2[0] + "</td>");
                        table.Append("<td>");
                        table.Append("<div class=\"form-check form-check-inline\">");
                        table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                        table.Append("<label class=\"form-check-label\">T</label>");
                        table.Append("</div>");
                        table.Append("</td>");
                        table.Append("<td>");
                        table.Append("<div class=\"form-check form-check-inline\">");
                        table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                        table.Append("<label class=\"form-check-label\">R</label>");
                        table.Append("</div>");
                        table.Append("</td>");
                        table.Append("<td>");
                        table.Append("<div class=\"form-check form-check-inline\">");
                        table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                        table.Append("<label class=\"form-check-label\">H</label>");
                        table.Append("</div>");
                        table.Append("</td>");
                        table.Append("<td>");
                        table.Append("<div class=\"form-check form-check-inline\">");
                        table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                        table.Append("<label class=\"form-check-label\">W</label>");
                        table.Append("</div>");
                        table.Append("</td>");
                        table.Append("<td>");
                        table.Append("<div class=\"form-check form-check-inline\">");
                        table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                        table.Append("<label class=\"form-check-label\">A</label>");
                        table.Append("</div>");
                        table.Append("</td>");
                        table.Append("<td>");
                        table.Append("<div class=\"form-check form-check-inline\">");
                        table.Append("<input class=\"form-check-input\" type=\"radio\" name=\"inlineRadioOptions\" id=\"inlineRadio2\" value=\"option2\">");
                        table.Append("<label class=\"form-check-label\">TC</label>");
                        table.Append("</div>");
                        table.Append("</td>");
                        table.Append("<td>");
                        table.Append("<div class=\"form-check form-check-inline\">");
                        table.Append("<i class=\"fa fa-file-text-o\"></i>");
                        table.Append("</div>");
                        table.Append("</td>");
                        table.Append("</tr>");


                    }

                rd2.Close();
                table.Append("</tbody>");
                table.Append("</table>");
                table.Append("</div>");
                table.Append("<br/>");
            }
            conn.Close();
            return table;
        }

        public static Dining_Attendance_simple get_list_resident(int homeid, DateTime todaydate)
        {
            //Dining_Attendance LIST_VIEW = new Dining_Attendance();
            //LIST_VIEW.LIST_RESIDENT = new List<ResidentModel>();
            //using (SqlConnection conn = new SqlConnection(Constants.ConnectionString.PROD))
            //{
            //    //SqlConnection conn = new SqlConnection(Constants.ConnectionString.PROD);
            //    //conn.ConnectionString = Constants.ConnectionString.PROD;
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = " select distinct fd_resident_id, fd_floor from[dbo].[tbl_Suite_Handler] SH" +
            //                        " join[dbo].[tbl_Suite] S on S.fd_id=SH.fd_suite_id" +
            //                        " where SH.fd_home_id = @homeid and @GETDATE> fd_move_in_date and @GETDATE< isNULL(fd_move_out_date, '2200-09-13')";
            //    cmd.Parameters.AddWithValue("@GETDATE", todaydate);
            //    cmd.Parameters.AddWithValue("@homeid", homeid);
            //    cmd.Connection = conn;
            //    SqlDataReader rd = cmd.ExecuteReader();
            //    if (rd.HasRows)
            //        while (rd.Read())
            //        {
            //            var resident = ResidentsDAL.GetResidentById(int.Parse(rd[0].ToString()));
            //            resident.No_of_floor = int.Parse(rd[1].ToString());
            //            LIST_VIEW.LIST_RESIDENT.Add(resident);
            //        }
            //}
            //return LIST_VIEW;


            Dining_Attendance_simple LIST_VIEW = new Dining_Attendance_simple();
            LIST_VIEW.LIST_RESIDENT = new List<ResidentModelsimple>();

            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString.PROD))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =   " select distinct SH.fd_resident_id, S.fd_floor, R.fd_first_name,R.fd_last_name from[dbo].[tbl_Suite_Handler] SH" +
                                    " join[dbo].[tbl_Suite] S on S.fd_id=SH.fd_suite_id" +
                                    " join[tbl_Resident] R on SH.fd_resident_id=R.fd_id" +
                                    " where SH.fd_home_id = @homeid and @GETDATE> SH.fd_move_in_date and @GETDATE< isNULL(SH.fd_move_out_date, '2200-09-13') order by R.fd_last_name";
                cmd.Parameters.AddWithValue("@GETDATE", todaydate);
                cmd.Parameters.AddWithValue("@homeid", homeid);
                cmd.Connection = conn;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                    while (rd.Read())
                    {
                        ResidentModelsimple sample = new ResidentModelsimple();
                        sample.ID = int.Parse(rd[0].ToString());
                        sample.No_of_floor = int.Parse(rd[1].ToString());
                        sample.FirstName = rd[2].ToString();
                        sample.LastName = rd[3].ToString(); ;
                        LIST_VIEW.LIST_RESIDENT.Add(sample);

                    }
            }
            return LIST_VIEW;



        }

        public static string[] get_TR_info(int homeid, DateTime todaydate)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Get_Taken_Refused_Information", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@changingdate", todaydate);
                cmdGARead.Parameters.Add("@returnstring", SqlDbType.VarChar, 100000);
                cmdGARead.Parameters["@returnstring"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                string retunvalue = cmdGARead.Parameters["@returnstring"].Value.ToString();
                string[] returnstring = retunvalue.Split(',');
                conn.Close();

                return returnstring;
            }
        }

        public static Collection<ActivityEventModel> GetBirthdayCalendar(int p_HomeId)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Birthday_Calendar", l_Conn);
                l_Cmd.Parameters.AddWithValue("@HomeId", p_HomeId);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        try
                        {
                            l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                            l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                            l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                            l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                            l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                            l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                            l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                            l_Event.note = Convert.ToString(homeTypeRow["EventTitle_title"]);
                            l_Event.Venue = Convert.ToString(homeTypeRow["EventTitle_suite"]);
                            l_Events.Add(l_Event);

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetBirthdayCalendar |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> GetSuggestedActivityEvents(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_SuggestedActivityCalendar", l_Conn);
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetSuggestedActivityEvents |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static int Save_Archive(int userid, int homeid, int redidentid, string suiteno, int occupancy, DateTime returndate , string notes,int status)
        {
            try
            {
                using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
                using (var cmdGARead = new SqlCommand("AB_Resident_Archive_Submit_by_mike", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmdGARead.Parameters.AddWithValue("@UserID", userid);
                    cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                    cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                    cmdGARead.Parameters.AddWithValue("@suiteno", suiteno);
                    cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                    cmdGARead.Parameters.AddWithValue("@status", status);
                    cmdGARead.Parameters.AddWithValue("@returndate", returndate);
                    cmdGARead.Parameters.AddWithValue("@notes", notes);
                    cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                    cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                    cmdGARead.ExecuteNonQuery();
                    int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                    return retunvalue;
                }
            }
            catch(Exception ee)
            {
                return 0;
            }
        }

        public static int Save_Image(int homeid, int redidentid)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update [tbl_Resident] set [fd_image]='Images/Home/'+@homeID+'/Resident_Image/'+@redidentid+'.PNG' where fd_id=@redidentid and fd_home_id=@homeID";

            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@homeID", homeid.ToString());
            cmd.Parameters.AddWithValue("@redidentid", redidentid.ToString());
            cmd.ExecuteNonQuery();
            return 1;
            

            
        }


        public static void EVENT_DragandDrop(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_DragandDrop", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@DT", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EVENT_DragandDrop |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void EVENT_DragandDrop2(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_DragandDrop2", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@DT", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EVENT_DragandDrop |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void EVENT_DragandDrop3(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_DragandDrop3", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@DT", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EVENT_DragandDrop |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void EVENT_DragandDrop4(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_DragandDrop4", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@DT", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "EVENT_DragandDrop |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void DELETE_EVENT(int EventID, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Delete", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "DELETE_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DELETE_EVENT2(int EventID, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Delete2", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "DELETE_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DELETE_EVENT3(int EventID, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Delete3", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "DELETE_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static void DELETE_EVENT4(int EventID, int HomeID)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Delete4", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "DELETE_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel> COPY_EVENT(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            SqlDataAdapter l_DA = new SqlDataAdapter();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Copy", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@NewDate", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);

                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);
                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "COPY_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel> COPY_EVENT2(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            SqlDataAdapter l_DA = new SqlDataAdapter();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Copy2", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@NewDate", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);

                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);
                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "COPY_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel> COPY_EVENT3(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            SqlDataAdapter l_DA = new SqlDataAdapter();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Copy3", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@NewDate", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);

                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);
                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "COPY_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static Collection<ActivityEventModel> COPY_EVENT4(int EventID, DateTime DT, int HomeID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel> l_Events = new Collection<ActivityEventModel>();
            ActivityEventModel l_Event;
            SqlDataAdapter l_DA = new SqlDataAdapter();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Activity_Events_Copy4", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@eventID", EventID);
                l_Cmd.Parameters.AddWithValue("@NewDate", DT);
                l_Cmd.Parameters.AddWithValue("@homtId", HomeID);

                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel();

                        l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                        l_Event.ActivityId = Convert.ToInt32(homeTypeRow["ActivityId"]);
                        l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                        l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                        l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                        l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                        l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                        l_Event.Venue = Convert.ToString(homeTypeRow["Venue"]);
                        l_Event.note = Convert.ToString(homeTypeRow["note"]);

                        l_Event.CategoryId = Convert.ToString(homeTypeRow["categoryID"]);
                        l_Event.Special = Convert.ToInt32(homeTypeRow["Special"]);
                        l_Event.Code = Convert.ToString(homeTypeRow["code"]);

                        l_Events.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "COPY_EVENT |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static ResidentEmergencyListModel get_EmergencyList(int homeid,int orderType)
        {
            string exception = string.Empty;
            ResidentEmergencyListModel l_Events = new ResidentEmergencyListModel();
            l_Events.EmergencyResidentList = new List<ResidentEmergencyListModel_single>();
            ResidentEmergencyListModel_single l_Event;
            string storeProcedure = "";
            if (orderType == 1) storeProcedure = "GET_Emergency_Resident_Details_mike_FAST";
            else if (orderType == 2) storeProcedure = "GET_Emergency_Resident_Details_mike_LowHigh_FAST";
            else if (orderType == 3) storeProcedure = "GET_Emergency_Resident_Details_mike_HighLow_FAST";
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(storeProcedure, l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ResidentEmergencyListModel_single();

                        l_Event.residentID = Convert.ToInt32(homeTypeRow["fd_id"]);
                        l_Event.suiteNo = Convert.ToString(homeTypeRow["fd_suite_no"]);
                        l_Event.FirstName = Convert.ToString(homeTypeRow["fd_first_name"]);
                        l_Event.LastName = Convert.ToString(homeTypeRow["fd_last_name"]);
                        l_Event.FullName = l_Event.FirstName + " " + l_Event.LastName;
                        l_Event.Gendar = Convert.ToString(homeTypeRow["fd_gender"]);
                        l_Event.phone = Convert.ToString(homeTypeRow["fd_phone"]);
                        l_Event.contact = Convert.ToString(homeTypeRow["fd_contact_1"]);
                        l_Event.contact_phone1 = Convert.ToString(homeTypeRow["fd_home_phone_1"]);
                        l_Event.contact_phone_type1 = Convert.ToString(homeTypeRow["fd_home_phone_type_1"]);
                        l_Event.contact_phone2 = Convert.ToString(homeTypeRow["fd_cell_phone_1"]);
                        l_Event.contact_phone_type2 = Convert.ToString(homeTypeRow["fd_cell_phone_type_1"]);
                        l_Event.contact_phone3 = Convert.ToString(homeTypeRow["fd_business_phone_1"]);
                        l_Event.contact_phone_type3 = Convert.ToString(homeTypeRow["fd_business_phone_type_1"]);
                        l_Event.RiskLevel = Convert.ToString(homeTypeRow["RiskLevel"]);
                        l_Event.totalScore = Convert.ToString(homeTypeRow["TotalScore"]);
                        if (l_Event.RiskLevel == "High Risk") l_Event.RiskLevel_Full = "High Falling Risk";
                        else if (l_Event.RiskLevel == "Medium Risk") l_Event.RiskLevel_Full = "Medium Falling Risk";
                        else if (l_Event.RiskLevel == "Low Risk") l_Event.RiskLevel_Full = "Low Falling Risk";
                        else if (l_Event.RiskLevel == "No Risk") l_Event.RiskLevel_Full = "No Falling Risk";
                        else  l_Event.RiskLevel_Full = "";
                        l_Event.Mobility = Convert.ToString(homeTypeRow["Mobility"]);
                        l_Event.Walker = Convert.ToString(homeTypeRow["Walker"]);
                        l_Event.WheelChair = Convert.ToString(homeTypeRow["WheelChair"]);
                        l_Event.Cane = Convert.ToString(homeTypeRow["Cane"]);
                        l_Event.Transfer = Convert.ToString(homeTypeRow["Transfers"]);
                        l_Event.Lift = Convert.ToString(homeTypeRow["Lift"]);
                        l_Event.Scooter = Convert.ToString(homeTypeRow["Scooter"]);
                        l_Event.Oxygen = Convert.ToString(homeTypeRow["Oxygen"]);
                        l_Event.CPAP = Convert.ToString(homeTypeRow["CPAP"]);
                        l_Event.CognitiveFunction = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(homeTypeRow["Vision"]));
                        l_Event.Vision = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(homeTypeRow["Vision"]));
                        l_Event.Hearing = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(homeTypeRow["Vision"]));
                        l_Event.Communication = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(homeTypeRow["Communication"]));
                        l_Event.SpecialEquip = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(homeTypeRow["SpecialEquipment"]));

                        l_Event.Vision_text = "";
                        if (l_Event.Vision != null)
                        {
                            foreach (var aa in l_Event.Vision)
                            {
                                if (aa.IsSelected == true) l_Event.Vision_text += aa.Name + ",";
                            }
                            if (l_Event.Vision_text.Length >= 1) l_Event.Vision_text = l_Event.Vision_text.Substring(0, l_Event.Vision_text.Length - 1);
                        }
                        
                        l_Event.Hearing_text = "";
                        if (l_Event.Hearing != null)
                        {
                            foreach (var aa in l_Event.Hearing)
                            {
                                if (aa.IsSelected == true) l_Event.Hearing_text += aa.Name + ",";
                            }
                            if (l_Event.Hearing_text.Length >= 1) l_Event.Hearing_text = l_Event.Hearing_text.Substring(0, l_Event.Hearing_text.Length - 1);
                        }
                        
                        l_Event.CognitiveFunction_text = "";
                        if (l_Event.CognitiveFunction != null)
                        {
                            foreach (var aa in l_Event.CognitiveFunction)
                            {
                                if (aa.IsSelected == true) l_Event.CognitiveFunction_text += aa.Name + ",";
                            }
                            if (l_Event.CognitiveFunction_text.Length >= 1) l_Event.CognitiveFunction_text = l_Event.CognitiveFunction_text.Substring(0, l_Event.CognitiveFunction_text.Length - 1);
                        }

                        l_Event.Communication_text = "";
                        if (l_Event.Communication != null)
                        {
                            foreach (var aa in l_Event.Communication)
                            {
                                if (aa.IsSelected == true) l_Event.Communication_text += aa.Name + ",";
                            }
                            if (l_Event.Communication_text.Length >= 1) l_Event.Communication_text = l_Event.Communication_text.Substring(0, l_Event.Communication_text.Length - 1);
                        }

                        l_Event.SpecialEquip_text = "";
                        if (l_Event.SpecialEquip != null)
                        {
                            foreach (var aa in l_Event.SpecialEquip)
                            {
                                if (aa.IsSelected == true) l_Event.SpecialEquip_text += aa.Name + ",";
                            }
                            if (l_Event.SpecialEquip_text.Length >= 1) l_Event.SpecialEquip_text = l_Event.SpecialEquip_text.Substring(0, l_Event.SpecialEquip_text.Length - 1);
                        }

                        l_Event.Comments = "<b>"+l_Event.RiskLevel+"</b>, ";

                        if (l_Event.Vision_text != "") l_Event.Comments += "<b>Vision</b>(" + l_Event.Vision_text + "), ";
                        if (l_Event.Hearing_text != "") l_Event.Comments += "<b>Hearing</b>(" + l_Event.Hearing_text + "), ";
                        if (l_Event.Communication_text != "") l_Event.Comments += "<b>Communication</b>(" + l_Event.Communication_text + "), ";
                        if (l_Event.Mobility != "") l_Event.Comments += "<b>Mobility</b>(" + l_Event.Mobility + "), ";
                        if (l_Event.Walker != "") l_Event.Comments += "<b>Walker</b>(" + l_Event.Walker + "), ";
                        if (l_Event.Transfer != "") l_Event.Comments += "<b>Transfers</b>(" + l_Event.Transfer + "), ";
                        if (l_Event.Lift != "") l_Event.Comments += "<b>Lift</b>(" + l_Event.Lift + "), ";
                        if (l_Event.WheelChair != "") l_Event.Comments += "<b>WheelChair</b>(" + l_Event.WheelChair + "), ";
                        if (l_Event.Scooter != "") l_Event.Comments += "<b>Scooter</b>(" + l_Event.Scooter + "), ";
                        if (l_Event.Cane != "") l_Event.Comments += "<b>Cane</b>(" + l_Event.Cane + "), ";
                        if (l_Event.CognitiveFunction_text != "") l_Event.Comments += "<b>CognitiveFunction</b>(" + l_Event.CognitiveFunction_text + "), ";
                        if (l_Event.Oxygen != "") l_Event.Comments += "<b>Oxygen</b>(" + l_Event.Oxygen + "), ";
                        if (l_Event.CPAP != "") l_Event.Comments += "<b>CPAP</b>(" + l_Event.CPAP + "), ";
                        if (l_Event.SpecialEquip_text != "") l_Event.Comments += "<b>SpecialEquipment</b>(" + l_Event.SpecialEquip_text + "), ";

                        l_Event.Comments = l_Event.Comments.Substring(0, l_Event.Comments.Length - 2);
                        l_Event.contact_phone_final = "";
                        if (l_Event.contact_phone_type1 == "1") l_Event.contact_phone_final = l_Event.contact_phone1;
                        else if (l_Event.contact_phone_type2 == "1") l_Event.contact_phone_final = l_Event.contact_phone2;
                        else if (l_Event.contact_phone_type3 == "1") l_Event.contact_phone_final = l_Event.contact_phone3;


                        l_Events.EmergencyResidentList.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "get_EmergencyList |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static NursingNoteReport get_NursingNoteReport(int homeid, DateTime date)
        {
            string exception = string.Empty;
            NursingNoteReport l_Events = new NursingNoteReport();
            l_Events.NursingNoteList = new List<NursingNotesModel>();
            NursingNotesModel l_Event;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("GET_NursingNoteReport", l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeid", homeid);
                l_Cmd.Parameters.AddWithValue("@modifiedon", date);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new NursingNotesModel();

                        l_Event.userName = Convert.ToString(homeTypeRow["UserName"]);
                        l_Event.userNameType = Convert.ToString(homeTypeRow["UserTypeName"]);
                        l_Event.suiteNo = Convert.ToString(homeTypeRow["fd_suite_no"]);
                        l_Event.FullName = Convert.ToString(homeTypeRow["ResidentName"]);
                        l_Event.Category = Convert.ToInt32(homeTypeRow["fd_category"]);

                        switch (l_Event.Category)
                        {
                            case 1:
                                l_Event.CategoryFull = "1";
                                break;
                            case 2:
                                l_Event.CategoryFull = "Medical Update";
                                break;
                            case 3:
                                l_Event.CategoryFull = "Social/activity update";
                                break;
                            case 4:
                                l_Event.CategoryFull = "Dietary Update";
                                break;
                            case 5:
                                l_Event.CategoryFull = "General Update";
                                break;
                            case 6:
                                l_Event.CategoryFull = "Resident Fall";
                                break;
                            case 7:
                                l_Event.CategoryFull = "Resident Bruised";
                                break;

                        }

                        l_Event.Note = Convert.ToString(homeTypeRow["fd_note"]);
                        l_Event.DateEntered = Convert.ToDateTime(homeTypeRow["fd_modified_on"]);


                        l_Events.NursingNoteList.Add(l_Event);
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "get_NursingNoteReport |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


        public static void Change_Calendar_Name(int homeId,int number, string newName,int userId)
        {
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_Change_Calendar_Name", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@number", number);
                l_Cmd.Parameters.AddWithValue("@homtId", homeId);
                l_Cmd.Parameters.AddWithValue("@newName", newName);
                l_Cmd.Parameters.AddWithValue("@userId", userId);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "Change_Calendar_Name |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static string[] get_Activity_Calendar_Name(int homeId)
        {
            string[] attayStr = new string[4] {"","","",""};
            string exception = string.Empty;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand("spAB_get_Activity_Calendar_Name", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homtId", homeId);

                SqlDataReader rd = l_Cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (int.Parse(rd[0].ToString()) == 1) attayStr[0] = rd[1].ToString();
                        else if (int.Parse(rd[0].ToString()) == 2) attayStr[1] = rd[1].ToString();
                        else if (int.Parse(rd[0].ToString()) == 3) attayStr[2] = rd[1].ToString();
                        else if (int.Parse(rd[0].ToString()) == 4) attayStr[3] = rd[1].ToString();
                    }
                }
                return attayStr;
            }
            catch (Exception ex)
            {
                exception = "get_Activity_Calendar_Name |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ActivityEventModel_PC> GetPersonalCalendar(int p_HomeId,int residentID)
        {
            string exception = string.Empty;
            Collection<ActivityEventModel_PC> l_Events = new Collection<ActivityEventModel_PC>();
            ActivityEventModel_PC l_Event;
            //UserModel l_User;
            //ResidentModel l_Resident;
            //SuiteModel l_Suite;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Personal_Calendar", l_Conn);
                l_Cmd.Parameters.AddWithValue("@HomeId", p_HomeId);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet homeReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homeReceive);

                if ((homeReceive != null) && (homeReceive.Tables.Count > 0) && (homeReceive.Tables[0].Rows.Count > 0))
                {
                    foreach (DataRow homeTypeRow in homeReceive.Tables[0].Rows)
                    {
                        l_Event = new ActivityEventModel_PC();

                        try
                        {
                            l_Event.ProgramId = Convert.ToInt32(homeTypeRow["Id"]);
                            l_Event.HomeId = Convert.ToInt32(homeTypeRow["homeId"]);
                            l_Event.ResidentId = Convert.ToInt32(homeTypeRow["residentID"]);
                            l_Event.ProgramName = Convert.ToString(homeTypeRow["EventTitle"]);
                            l_Event.ProgramStartDate = Convert.ToDateTime(homeTypeRow["StartDate"]);
                            l_Event.ProgramEndDate = Convert.ToDateTime(homeTypeRow["EndDate"]);
                            l_Event.ProgramStartTime = Convert.ToString(homeTypeRow["StartTime"]);
                            l_Event.ProgramEndTime = Convert.ToString(homeTypeRow["EndTime"]);
                            l_Event.note = Convert.ToString(homeTypeRow["note"]);
                            l_Events.Add(l_Event);

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                return l_Events;
            }
            catch (Exception ex)
            {
                exception = "GetBirthdayCalendar |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void AddPersonalCalendar(int p_HomeId, int residentID,string title,int userid)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_add_Personal_Calendar", l_Conn);
                l_Conn.Open();
                l_Cmd.Parameters.AddWithValue("@HomeId", p_HomeId);
                l_Cmd.Parameters.AddWithValue("@residentID", residentID);
                l_Cmd.Parameters.AddWithValue("@title", title);
                l_Cmd.Parameters.AddWithValue("@startDate", DateTime.Now.Date);
                l_Cmd.Parameters.AddWithValue("@startTime", DateTime.Now.ToLongTimeString());
                l_Cmd.Parameters.AddWithValue("@userid", userid);  
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddPersonalCalendar |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataTable Get_Activity_Calendar1234_ExporttoWord(int homeId, DateTime eventFromDate, DateTime eventToDate,string CaNumber)
        {
            string exception = string.Empty;
            DataSet dtPersonalCalendar = null;
            DataTable dtResident = null;
            string storeProString = "";
            switch (CaNumber)
            {
                case "a":
                    storeProString = "Get_Activity_Calendar1_ExporttoWord";
                    break;
                case "b":
                    storeProString = "Get_Activity_Calendar2_ExporttoWord";
                    break;
                case "c":
                    storeProString = "Get_Activity_Calendar3_ExporttoWord";
                    break;
                case "d":
                    storeProString = "Get_Activity_Calendar4_ExporttoWord";
                    break;
            }
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(storeProString, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@fromDate", eventFromDate);
                l_Cmd.Parameters.AddWithValue("@toDate", eventToDate);
                dtPersonalCalendar = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dtPersonalCalendar);

                

                if (dtPersonalCalendar.Tables[0].Rows.Count > 0)
                {
                    dtResident = new DataTable();
                    dtResident = dtPersonalCalendar.Tables[0];
                }
                return dtResident;
            }
            catch (Exception ex)
            {
                exception = "Get_Activity_Calendar1_ExporttoWord |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static List<Dictionary<string, string>> addEVENTStol_Events(Collection<ActivityEventModel> EVENTS)
        {
            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in EVENTS)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", l_Data.CategoryId},
                    { "ActivityId", l_Data.ActivityId.ToString()},
                    { "Special", l_Data.Special.ToString()},
                    { "Code", l_Data.Code}
                };

                l_Events.Add(columns);
            }

            return l_Events;
        }

        public static List<Dictionary<string, string>> addEVENTStol_Events_single(ActivityEventModel l_Data)
        {
            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
            var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

            var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", l_Data.CategoryId},
                    { "ActivityId", l_Data.ActivityId.ToString()},
                    { "Special", l_Data.Special.ToString()},
                    { "Code", l_Data.Code}
                };

            l_Events.Add(columns);


            return l_Events;
        }


    }
}