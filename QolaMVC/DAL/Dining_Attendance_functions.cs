using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace QolaMVC.DAL
{
    public class Dining_Attendance_functions
    {
        public static int save_Button(int homeid, int whichmeal,DateTime change_date ,Dictionary<string,string> arr,int userid, DateTime modifydate)
        {
            string option1 = "";
            string option2 = "";
            string option3 = "";
            string option4 = "";
            string option5 = "";
            string option6 = "";
            if (arr.ContainsKey("option1"))
                option1 = arr["option1"];
            if (arr.ContainsKey("option2"))
                option2 = arr["option2"];
            if (arr.ContainsKey("option3"))
                option3 = arr["option3"];
            if (arr.ContainsKey("option4"))
                option4 = arr["option4"];
            if (arr.ContainsKey("option5"))
                option5 = arr["option5"];
            if (arr.ContainsKey("option6"))
                option6 = arr["option6"];

            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Dining_Attendance_Saving", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@mealindex", whichmeal);
                cmdGARead.Parameters.AddWithValue("@changingdate", change_date);
                cmdGARead.Parameters.AddWithValue("@resident_T", option1);
                cmdGARead.Parameters.AddWithValue("@resident_R", option2);
                cmdGARead.Parameters.AddWithValue("@resident_H", option3);
                cmdGARead.Parameters.AddWithValue("@resident_W", option4);
                cmdGARead.Parameters.AddWithValue("@resident_A", option5);
                cmdGARead.Parameters.AddWithValue("@resident_TC", option6);
                cmdGARead.Parameters.AddWithValue("@UserID", userid);
                cmdGARead.Parameters.AddWithValue("@modify_on", modifydate);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static StringBuilder getting_LIST( int whichmeal, DateTime change_date, int homeid)
        {
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            StringBuilder stringlist = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " SELECT * from [dbo].[tbl_AB_Dine_Attendance] where fd_dine_time_id=@mealindex and fd_date=@datechanging and fd_home_id=@homeid";
            cmd.Parameters.AddWithValue("@mealindex", whichmeal);
            cmd.Parameters.AddWithValue("@datechanging", change_date);
            cmd.Parameters.AddWithValue("@homeid", homeid);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    //dic.Add("option1", rd[4].ToString());
                    //dic.Add("option2", rd[5].ToString());
                    //dic.Add("option3", rd[6].ToString());
                    //dic.Add("option4", rd[7].ToString());
                    //dic.Add("option5", rd[8].ToString());
                    //dic.Add("option6", rd[9].ToString());
                    stringlist.Append(rd[4].ToString()+";");
                    stringlist.Append(rd[5].ToString() + ";");
                    stringlist.Append(rd[6].ToString() + ";");
                    stringlist.Append(rd[7].ToString() + ";");
                    stringlist.Append(rd[8].ToString() + ";");
                    stringlist.Append(rd[9].ToString());


                }
            conn.Close();
            return stringlist;
        }

        public static List<int> disable_hospital_list(DateTime dateselect)
        {
            List<int> retu = new List<int>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select distinct [fd_resident_id] from [tbl_Suite_Handler] where [fd_hospital_leaving]<'"+ dateselect.ToString("yyyy-MM-dd") +
                              "' and isNULL([fd_hospital_return],'2200-09-01')>'" + dateselect.ToString("yyyy-MM-dd")+"'";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    retu.Add(int.Parse(rd[0].ToString()));
                }
            return retu;
        }

    }
}