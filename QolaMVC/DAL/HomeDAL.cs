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
                l_Cmd.Parameters.AddWithValue("@status", addHome.Status);
                l_Cmd.Parameters.AddWithValue("@createdby", addHome.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@dineTimeIds", addHome.DineTimeIds);
                l_Cmd.Parameters.AddWithValue("@phone", addHome.Phone);
                l_Cmd.Parameters.AddWithValue("@passTimeIds", addHome.PassTimeIds);
                homeId = l_Cmd.ExecuteNonQuery();
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
                l_Cmd.Parameters.AddWithValue("@status", updateHome.Status);
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

        public static bool RemoveHome(int homeId)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_REMOVE_HOME, l_Conn);
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
    }
}