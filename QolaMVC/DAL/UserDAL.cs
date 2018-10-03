using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QolaMVC.DAL
{
    #region "Users"

    public class UserDAL
    {
       
        #region "Methods"
        public static int AddNewUsers(UserModel addUsers)
        {
            string exception = string.Empty;
            int result = 0;
            int homeId = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_USER, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", addUsers.ID);
                l_Cmd.Parameters.AddWithValue("@homeId", addUsers.Home);
                l_Cmd.Parameters.AddWithValue("@firstName", addUsers.FirstName);
                l_Cmd.Parameters.AddWithValue("@lastName", addUsers.LastName);
                l_Cmd.Parameters.AddWithValue("@userType", addUsers.UserType);
                l_Cmd.Parameters.AddWithValue("@userName", addUsers.UserName);
                l_Cmd.Parameters.AddWithValue("@password", addUsers.Password);
                l_Cmd.Parameters.AddWithValue("@address", addUsers.Address);
                l_Cmd.Parameters.AddWithValue("@city", addUsers.City);
                l_Cmd.Parameters.AddWithValue("@postalCode", addUsers.PostalCode);
                l_Cmd.Parameters.AddWithValue("@province", addUsers.Province);
                l_Cmd.Parameters.AddWithValue("@email", addUsers.Email);
                l_Cmd.Parameters.AddWithValue("@workPhone", addUsers.WorkPhone);
                l_Cmd.Parameters.AddWithValue("@ext", addUsers.Ext);
                l_Cmd.Parameters.AddWithValue("@homePhone", addUsers.HomePhone);
                l_Cmd.Parameters.AddWithValue("@mobile", addUsers.Mobile);
                l_Cmd.Parameters.AddWithValue("@status", Convert.ToString(addUsers.Status));
                l_Cmd.Parameters.AddWithValue("@createdby", addUsers.ModifiedBy);
                l_Cmd.Parameters.AddWithValue("@country", addUsers.Country);
                homeId = l_Cmd.ExecuteNonQuery();
                if (homeId != 0)
                {

                    //result = Convert.ToInt32(l_Cmd.Parameters..GetParameterValue(cmd, "@id"));
                }
            }
            catch (Exception ex)
            {
                exception = "Users AddNewUsers |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            return result;
        }

        public static bool UpdateUsers(UserModel updateUsers)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_USER, l_Conn);
                l_Cmd.Parameters.AddWithValue("@id", updateUsers.ID);
                l_Cmd.Parameters.AddWithValue("@homeId", updateUsers.Home);
                l_Cmd.Parameters.AddWithValue("@firstName", updateUsers.FirstName);
                l_Cmd.Parameters.AddWithValue("@lastName", updateUsers.LastName);
                l_Cmd.Parameters.AddWithValue("@userType", updateUsers.UserType);
                l_Cmd.Parameters.AddWithValue("@userName", updateUsers.UserName);
                l_Cmd.Parameters.AddWithValue("@password", updateUsers.Password);
                l_Cmd.Parameters.AddWithValue("@address", updateUsers.Address);
                l_Cmd.Parameters.AddWithValue("@city", updateUsers.City);
                l_Cmd.Parameters.AddWithValue("@postalCode", updateUsers.PostalCode);
                l_Cmd.Parameters.AddWithValue("@province", updateUsers.Province);
                l_Cmd.Parameters.AddWithValue("@email", updateUsers.Email);
                l_Cmd.Parameters.AddWithValue("@workPhone", updateUsers.WorkPhone);
                l_Cmd.Parameters.AddWithValue("@ext", updateUsers.Ext);
                l_Cmd.Parameters.AddWithValue("@homePhone", updateUsers.HomePhone);
                l_Cmd.Parameters.AddWithValue("@mobile", updateUsers.Mobile);
                //l_Cmd.Parameters.AddWithValue("@status", updateUsers.Status);
                l_Cmd.Parameters.AddWithValue("@modifiedby", updateUsers.ModifiedBy);
                l_Cmd.Parameters.AddWithValue("@country", updateUsers.Country);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                exception = "Users UpdateUsers |" + ex.ToString();
                throw;
            }
            return result;
        }

        public static bool UpdateUsersWithoutPassword(UserModel updateUsers)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_USER_WITHOUT_PASSWORD, l_Conn);
                l_Cmd.Parameters.AddWithValue("@id", updateUsers.ID);
                l_Cmd.Parameters.AddWithValue("@homeId", updateUsers.Home);
                l_Cmd.Parameters.AddWithValue("@firstName", updateUsers.FirstName);
                l_Cmd.Parameters.AddWithValue("@lastName", updateUsers.LastName);
                l_Cmd.Parameters.AddWithValue("@userType", updateUsers.UserType);
                l_Cmd.Parameters.AddWithValue("@userName", updateUsers.UserName);
                l_Cmd.Parameters.AddWithValue("@address", updateUsers.Address);
                l_Cmd.Parameters.AddWithValue("@city", updateUsers.City);
                l_Cmd.Parameters.AddWithValue("@postalCode", updateUsers.PostalCode);
                l_Cmd.Parameters.AddWithValue("@province", updateUsers.Province);
                l_Cmd.Parameters.AddWithValue("@email", updateUsers.Email);
                l_Cmd.Parameters.AddWithValue("@workPhone", updateUsers.WorkPhone);
                l_Cmd.Parameters.AddWithValue("@ext", updateUsers.Ext);
                l_Cmd.Parameters.AddWithValue("@homePhone", updateUsers.HomePhone);
                l_Cmd.Parameters.AddWithValue("@mobile", updateUsers.Mobile);
                //l_Cmd.Parameters.AddWithValue("@status", updateUsers.Status);
                l_Cmd.Parameters.AddWithValue("@createdby", updateUsers.ModifiedBy);
                l_Cmd.Parameters.AddWithValue("@country", updateUsers.Country);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                exception = "Users UpdateUsersWithoutPassword |" + ex.ToString();
                throw;
            }
            return result;
        }

        public static bool RemoveUsers(int usersId)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_REMOVE_USER, l_Conn);
                l_Cmd.Parameters.AddWithValue("@id", usersId);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                exception = "Users RemoveUsers |" + ex.ToString();
                throw;
            }
            return result;
        }

        public static Collection<UserModel> GetUsersCollections(string homeIds, int userTypeId, char cStatus = 'A')
        {
            string exception = string.Empty;
            Database db;
            Collection<UserModel> users = new Collection<UserModel>();
            UserModel user;
            //Common.Home home;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_USER, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeIds);
                l_Cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                l_Cmd.Parameters.AddWithValue("@status", cStatus);

                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if ((homesReceive != null) && homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        user = new UserModel();
                        //home = new Common.Home();
                        user.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        user.Home = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_home_id"]); ;
                        user.FirstName = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_first_name"]);
                        user.LastName = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_last_name"]);
                        user.Name = Convert.ToString(homesReceive.Tables[0].Rows[index]["Name"]);
                        user.UserType = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_user_type"]);
                        user.UserName = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_user_name"]);
                        user.Password = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_user_name"]);
                        user.Address = Convert.IsDBNull(homesReceive.Tables[0].Rows[index]["fd_address"]) ? "" : Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_address"]);
                        user.City = Convert.IsDBNull(homesReceive.Tables[0].Rows[index]["fd_city"]) ? "" : Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_city"]);
                        user.PostalCode = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_postal_code"]);
                        user.Province = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_province"]);
                        user.Email = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_email"]);
                        user.WorkPhone = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_work_phone"]);
                        user.Ext = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_ext"]);
                        user.HomePhone = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_home_phone"]);
                        user.Mobile = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_mobile"]);
                        if (homesReceive.Tables[0].Rows[index].ToString() == "A")
                        {
                            //user.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            //user.Status = AvailabilityStatus.I;
                        }
                        user.UserTypeName = Convert.ToString(homesReceive.Tables[0].Rows[index]["UserTypeName"]);
                        user.HomeName = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_home_name"]);
                        user.Country = Convert.IsDBNull(homesReceive.Tables[0].Rows[index]["fd_country"]) ? "" : Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_country"]);
                        users.Add(user);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                exception = "Users GetUsersCollections |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet dsUsersReceiveByHomeIdAndUserType(string homeIds, int userTypeId)
        {
            string exception = string.Empty;
            DataSet dsUsersReceive = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_USER, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeIds);
                l_Cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                dsUsersReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsUsersReceive);
                return dsUsersReceive;
            }
            catch (Exception ex)
            {
                exception = "dsUsersReceiveByHomeIdAndUserType |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet dsUsersReceiveByHomeIdAndUserTypeAndUserStatus(string homeIds, int userTypeId, char cStatus = 'A')
        {
            string exception = string.Empty;
            DataSet dsUsersReceive = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_USER, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeIds);
                l_Cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                l_Cmd.Parameters.AddWithValue("@status", cStatus);
                dsUsersReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsUsersReceive);
                return dsUsersReceive;
            }
            catch (Exception ex)
            {
                exception = "dsUsersReceiveByHomeIdAndUserType |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        public static UserModel GetUserById(int usersId)
        {
            string exception = string.Empty;
            UserModel user = new UserModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_USER_BY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", usersId);

                DataSet userReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(userReceive);

                if ((userReceive != null) & userReceive.Tables.Count > 0)
                {
                    foreach (DataRow userTypeRow in userReceive.Tables[0].Rows)
                    {
                        user.ID = Convert.ToInt32(userTypeRow["fd_id"]);
                        user.Home = Convert.ToString(userTypeRow["fd_home_id"]); ;
                        user.FirstName = Convert.ToString(userTypeRow["fd_first_name"]);
                        user.LastName = Convert.ToString(userTypeRow["fd_last_name"]);
                        user.UserType = Convert.ToInt32(userTypeRow["fd_user_type"]);
                        user.UserName = Convert.ToString(userTypeRow["fd_user_name"]);
                        user.Password = Convert.ToString(userTypeRow["fd_user_name"]);
                        user.Address = Convert.ToString(userTypeRow["fd_address"]);
                        user.City = Convert.ToString(userTypeRow["fd_city"]);
                        user.PostalCode = Convert.ToString(userTypeRow["fd_postal_code"]);
                        user.Province = Convert.ToString(userTypeRow["fd_province"]);
                        user.Email = Convert.ToString(userTypeRow["fd_email"]);
                        user.WorkPhone = Convert.ToString(userTypeRow["fd_work_phone"]);
                        user.Ext = Convert.ToInt32(userTypeRow["fd_ext"]);
                        user.HomePhone = Convert.ToString(userTypeRow["fd_home_phone"]);
                        user.Mobile = Convert.ToString(userTypeRow["fd_mobile"]);
                        if (userTypeRow["fd_status"].ToString() == "A")
                        {
                            //user.Status = AvailabilityStatus.A;
                        }
                        else
                        {
                            //user.Status = AvailabilityStatus.I;
                        }
                        user.Country = Convert.ToString(userTypeRow["fd_country"]);
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                exception = "Users GetUserById |" + ex.ToString();
                throw;
            }
        }


        public static int ResetPassword(UserModel resetUserPWD)
        {
            string exception = string.Empty;
            UserModel users = new UserModel();
            int affected = 0;
            int result = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_RESET_USER_PASSWORD, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", resetUserPWD.ID);
                l_Cmd.Parameters.AddWithValue("@oldPassword", resetUserPWD.OldPassword);
                l_Cmd.Parameters.AddWithValue("@newPassword", resetUserPWD.Password);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    //result = Convert.ToInt32(db.GetParameterValue(cmd, "@id"));
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "Users ResetPassword |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static UserModel GetUserByUsernameAndPassword(string UserName, string Password, string ClientMAC, string ClientIP)
        {
            string exception = string.Empty;
            Database db;
            UserModel user = new UserModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_USER_BY_USERNAME_PASSWORD, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Username", UserName);
                l_Cmd.Parameters.AddWithValue("@Password", Password);
                l_Cmd.Parameters.AddWithValue("@ClientMac", ClientMAC);
                l_Cmd.Parameters.AddWithValue("@ClientIp", ClientIP);
                DataSet userReceive = new DataSet();
                
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(userReceive);

                if ((userReceive != null) & userReceive.Tables.Count > 0)
                {
                    foreach (DataRow userRow in userReceive.Tables[0].Rows)
                    {

                        user.ID = Convert.ToInt32(userRow["fd_id"]);
                        user.Home = Convert.ToString(userRow["fd_home_id"]);
                        user.FirstName = Convert.ToString(userRow["fd_first_name"]);
                        user.LastName = Convert.ToString(userRow["fd_last_name"]);
                        user.UserName = Convert.ToString(userRow["fd_user_name"]);
                        user.UserType = Convert.ToInt32(userRow["fd_user_type"]);
                        user.Province = Convert.ToString(userRow["fd_province"]);
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                exception = "Users GetUserByUsernameAndPassword |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<UserModel> bindActivityDirector(string homeId)
        {
            string exception = string.Empty;
            Collection<UserModel> users = new Collection<UserModel>();
            UserModel user;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_ACTIVITY_DIRECTOR_BY_HOME_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if ((homesReceive != null) && homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        user = new UserModel();
                        user.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        user.Address = Convert.ToString(homesReceive.Tables[0].Rows[index]["ActivityDirector"]);
                        users.Add(user);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                exception = "Users bindActivityDirector |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<UserModel> GetActivityDirectorByHomeAndActivityId(int homeId, string activityId)
        {
            string exception = string.Empty;
            Database db;
            Collection<UserModel> users = new Collection<UserModel>();
            UserModel user;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_ACTIVITY_DIRECTOR_BY_HOME_ID_AND_ACTIVITY_ID, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@activityId", activityId);
                DataSet homesReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if ((homesReceive != null) && homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        user = new UserModel();
                        user.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        user.Address = Convert.ToString(homesReceive.Tables[0].Rows[index]["ActivityDirector"]);
                        users.Add(user);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                exception = "Users GetActivityDirectorByHomeAndActivityId |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static string GetCurrentVersion()
        {
            string exception = string.Empty;
            string version = string.Empty;
            Database db;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_CURRENT_VERSION, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet dsVersion = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsVersion);

                if ((dsVersion != null) & dsVersion.Tables.Count > 0)
                {
                    version = Convert.ToString(dsVersion.Tables[0].Rows[0]["fd_version"]);
                }
                return version;
            }
            catch (Exception ex)
            {
                exception = "Users GetCurrentVersion |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<UserModel> GetUserByHome(string homeId, int iUserType, string sStatus)
        {
            string exception = string.Empty;
            Database db;
            Collection<UserModel> users = new Collection<UserModel>();
            UserModel user;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_USER_BY_HOME, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@userTypeId", iUserType);
                l_Cmd.Parameters.AddWithValue("@status", sStatus);
                DataSet homesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(homesReceive);

                if ((homesReceive != null) && homesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= homesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        user = new UserModel();

                        user.ID = Convert.ToInt32(homesReceive.Tables[0].Rows[index]["fd_id"]);
                        user.FirstName = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_first_name"]);
                        user.LastName = Convert.ToString(homesReceive.Tables[0].Rows[index]["fd_last_name"]);
                        users.Add(user);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                exception = "Users bindActivityDirector |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataSet dsUsersReceiveByHeadOfficeUserType(string homeId, int userTypeId)
        {
            string exception = string.Empty;
            Database db;
            DataSet dsUsersReceive = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_HEAD_OFFICE_USER, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                dsUsersReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dsUsersReceive);
                return dsUsersReceive;
            }
            catch (Exception ex)
            {
                exception = "dsUsersReceiveByHomeIdAndUserType |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        #endregion
    }
    #endregion
}