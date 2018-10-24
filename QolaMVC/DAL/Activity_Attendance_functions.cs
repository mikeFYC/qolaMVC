using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace QolaMVC.DAL
{
    public class Activity_Attendance_functions
    {
        public static int save_Button(int homeid, int whichAEID, DateTime change_date, Dictionary<string, string> arr, int userid, DateTime modifydate)
        {
            string option1 = "";
            string option2 = "";
            string option3 = "";
            string option4 = "";
            if (arr.ContainsKey("option1"))
                option1 = arr["option1"];
            if (arr.ContainsKey("option2"))
                option2 = arr["option2"];
            if (arr.ContainsKey("option3"))
                option3 = arr["option3"];
            if (arr.ContainsKey("option4"))
                option4 = arr["option4"];
            try
            {
                using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
                using (var cmdGARead = new SqlCommand("Activity_Attendance_Saving", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                    cmdGARead.Parameters.AddWithValue("@AEID", whichAEID);
                    cmdGARead.Parameters.AddWithValue("@changingdate", change_date);
                    cmdGARead.Parameters.AddWithValue("@resident_A", option1);
                    cmdGARead.Parameters.AddWithValue("@resident_P", option2);
                    cmdGARead.Parameters.AddWithValue("@resident_R", option3);
                    cmdGARead.Parameters.AddWithValue("@resident_Away", option4);
                    cmdGARead.Parameters.AddWithValue("@UserID", userid);
                    cmdGARead.Parameters.AddWithValue("@modify_on", modifydate);
                    cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                    cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                    cmdGARead.ExecuteNonQuery();
                    int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                    return retunvalue;
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public static int save_Button2(int homeid, int whichAEID, DateTime change_date, Dictionary<string, string> arr, int userid, DateTime modifydate)
        {
            string option1 = "";
            string option2 = "";
            string option3 = "";
            string option4 = "";
            if (arr.ContainsKey("option1"))
                option1 = arr["option1"];
            if (arr.ContainsKey("option2"))
                option2 = arr["option2"];
            if (arr.ContainsKey("option3"))
                option3 = arr["option3"];
            if (arr.ContainsKey("option4"))
                option4 = arr["option4"];
            try
            {
                using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
                using (var cmdGARead = new SqlCommand("Activity_Attendance_Saving2", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                    cmdGARead.Parameters.AddWithValue("@AEID", whichAEID);
                    cmdGARead.Parameters.AddWithValue("@changingdate", change_date);
                    cmdGARead.Parameters.AddWithValue("@resident_A", option1);
                    cmdGARead.Parameters.AddWithValue("@resident_P", option2);
                    cmdGARead.Parameters.AddWithValue("@resident_R", option3);
                    cmdGARead.Parameters.AddWithValue("@resident_Away", option4);
                    cmdGARead.Parameters.AddWithValue("@UserID", userid);
                    cmdGARead.Parameters.AddWithValue("@modify_on", modifydate);
                    cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                    cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                    cmdGARead.ExecuteNonQuery();
                    int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                    return retunvalue;
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public static int save_Button3(int homeid, int whichAEID, DateTime change_date, Dictionary<string, string> arr, int userid, DateTime modifydate)
        {
            string option1 = "";
            string option2 = "";
            string option3 = "";
            string option4 = "";
            if (arr.ContainsKey("option1"))
                option1 = arr["option1"];
            if (arr.ContainsKey("option2"))
                option2 = arr["option2"];
            if (arr.ContainsKey("option3"))
                option3 = arr["option3"];
            if (arr.ContainsKey("option4"))
                option4 = arr["option4"];
            try
            {
                using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
                using (var cmdGARead = new SqlCommand("Activity_Attendance_Saving3", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                    cmdGARead.Parameters.AddWithValue("@AEID", whichAEID);
                    cmdGARead.Parameters.AddWithValue("@changingdate", change_date);
                    cmdGARead.Parameters.AddWithValue("@resident_A", option1);
                    cmdGARead.Parameters.AddWithValue("@resident_P", option2);
                    cmdGARead.Parameters.AddWithValue("@resident_R", option3);
                    cmdGARead.Parameters.AddWithValue("@resident_Away", option4);
                    cmdGARead.Parameters.AddWithValue("@UserID", userid);
                    cmdGARead.Parameters.AddWithValue("@modify_on", modifydate);
                    cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                    cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                    cmdGARead.ExecuteNonQuery();
                    int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                    return retunvalue;
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public static int save_Button4(int homeid, int whichAEID, DateTime change_date, Dictionary<string, string> arr, int userid, DateTime modifydate)
        {
            string option1 = "";
            string option2 = "";
            string option3 = "";
            string option4 = "";
            if (arr.ContainsKey("option1"))
                option1 = arr["option1"];
            if (arr.ContainsKey("option2"))
                option2 = arr["option2"];
            if (arr.ContainsKey("option3"))
                option3 = arr["option3"];
            if (arr.ContainsKey("option4"))
                option4 = arr["option4"];
            try
            {
                using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
                using (var cmdGARead = new SqlCommand("Activity_Attendance_Saving4", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                    cmdGARead.Parameters.AddWithValue("@AEID", whichAEID);
                    cmdGARead.Parameters.AddWithValue("@changingdate", change_date);
                    cmdGARead.Parameters.AddWithValue("@resident_A", option1);
                    cmdGARead.Parameters.AddWithValue("@resident_P", option2);
                    cmdGARead.Parameters.AddWithValue("@resident_R", option3);
                    cmdGARead.Parameters.AddWithValue("@resident_Away", option4);
                    cmdGARead.Parameters.AddWithValue("@UserID", userid);
                    cmdGARead.Parameters.AddWithValue("@modify_on", modifydate);
                    cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                    cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                    cmdGARead.ExecuteNonQuery();
                    int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                    return retunvalue;
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public static int add_progress_note(int residentid, DateTime dateselect, string note, int userid, DateTime modified_time)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Constants.ConnectionString.PROD;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText =
                        " insert into [dbo].[tbl_Progress_Notes]" +
                        " (fd_resident_id,fd_date,fd_title,fd_note,fd_status,fd_modified_by,fd_modified_on,fd_category)" +
                        " values" +
                        " (@residentid, @date, 'Activity Attendance', @note, 'A', @userid, @modifieddate, 3)";
                cmd.Parameters.AddWithValue("@residentid", residentid);
                cmd.Parameters.AddWithValue("@date", dateselect);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@modifieddate", modified_time);
                cmd.Connection = conn;
                SqlDataReader rd = cmd.ExecuteReader();
                conn.Close();
                return 1;
            }
            catch (Exception eee)
            {
                return 0;
            }


        }

        public static StringBuilder getting_LIST(int whichAEID, DateTime change_date, int homeid)
        {
            StringBuilder stringlist = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " SELECT * from [dbo].[tbl_AB_Activity_Attendance] where ActivityEvent_ID=@AEID and DateSelect=@datechanging and homeID=@homeid";
            cmd.Parameters.AddWithValue("@AEID", whichAEID);
            cmd.Parameters.AddWithValue("@datechanging", change_date);
            cmd.Parameters.AddWithValue("@homeid", homeid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    stringlist.Append(rd[4].ToString() + ";");
                    stringlist.Append(rd[5].ToString() + ";");
                    stringlist.Append(rd[6].ToString() + ";");
                    stringlist.Append(rd[7].ToString());
                }
            conn.Close();
            return stringlist;
        }

        public static StringBuilder getting_LIST2(int whichAEID, DateTime change_date, int homeid)
        {
            StringBuilder stringlist = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " SELECT * from [dbo].[tbl_AB_Activity_Attendance_2] where ActivityEvent_ID=@AEID and DateSelect=@datechanging and homeID=@homeid";
            cmd.Parameters.AddWithValue("@AEID", whichAEID);
            cmd.Parameters.AddWithValue("@datechanging", change_date);
            cmd.Parameters.AddWithValue("@homeid", homeid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    stringlist.Append(rd[4].ToString() + ";");
                    stringlist.Append(rd[5].ToString() + ";");
                    stringlist.Append(rd[6].ToString() + ";");
                    stringlist.Append(rd[7].ToString());
                }
            conn.Close();
            return stringlist;
        }

        public static StringBuilder getting_LIST3(int whichAEID, DateTime change_date, int homeid)
        {
            StringBuilder stringlist = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " SELECT * from [dbo].[tbl_AB_Activity_Attendance_3] where ActivityEvent_ID=@AEID and DateSelect=@datechanging and homeID=@homeid";
            cmd.Parameters.AddWithValue("@AEID", whichAEID);
            cmd.Parameters.AddWithValue("@datechanging", change_date);
            cmd.Parameters.AddWithValue("@homeid", homeid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    stringlist.Append(rd[4].ToString() + ";");
                    stringlist.Append(rd[5].ToString() + ";");
                    stringlist.Append(rd[6].ToString() + ";");
                    stringlist.Append(rd[7].ToString());
                }
            conn.Close();
            return stringlist;
        }

        public static StringBuilder getting_LIST4(int whichAEID, DateTime change_date, int homeid)
        {
            StringBuilder stringlist = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " SELECT * from [dbo].[tbl_AB_Activity_Attendance_4] where ActivityEvent_ID=@AEID and DateSelect=@datechanging and homeID=@homeid";
            cmd.Parameters.AddWithValue("@AEID", whichAEID);
            cmd.Parameters.AddWithValue("@datechanging", change_date);
            cmd.Parameters.AddWithValue("@homeid", homeid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    stringlist.Append(rd[4].ToString() + ";");
                    stringlist.Append(rd[5].ToString() + ";");
                    stringlist.Append(rd[6].ToString() + ";");
                    stringlist.Append(rd[7].ToString());
                }
            conn.Close();
            return stringlist;
        }

    }
}