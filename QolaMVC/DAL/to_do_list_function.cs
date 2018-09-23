using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QolaMVC.DAL
{
    public class to_do_list_function
    {
        public static List<dynamic> get_hospital_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER() over(order by SH.fd_hospital_leaving) as number, " +
                                " SH.fd_resident_id, S.fd_suite_no, R.fd_first_name, R.fd_last_name, " +
                                " R.fd_gender, SH.fd_hospital_leaving, fd_hospital_expected_return" +
                                " from[dbo].[tbl_Suite_Handler] SH" +
                                " join tbl_Resident R on SH.fd_resident_id=R.fd_id" +
                                " join tbl_Suite S on S.fd_id= SH.fd_suite_id" +
                                " where SH.fd_home_id=" + homeid +
                                " and SH.fd_pass_away_date is null " +
                                " and SH.fd_move_out_date is null  " +
                                " and SH.[fd_hospital_leaving] is not null" +
                                " and GETDATE()>SH.[fd_hospital_leaving]" +
                                " and GETDATE()<isNULL(SH.[fd_hospital_return],'2200-09-01')";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.resident_id = rd[1];
                    l_J.suite_no = rd[2];
                    l_J.first_name = rd[3];
                    l_J.last_name = rd[4];
                    l_J.gender = rd[5];
                    l_J.hospital_leaving = rd[6];
                    l_J.hospital_expected_return = rd[7];

                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_DU_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select distinct ROW_NUMBER() over(order by DA.fd_modified_on) as row_number, " +
                                " SH.fd_resident_id,DA.fd_id as DA_id,S.fd_suite_no,R.fd_first_name,R.fd_last_name,DA.fd_modified_on" +
                                " from tbl_Resident R" +
                                " join tbl_Suite_Handler SH on R.fd_id = SH.fd_resident_id" +
                                " join[tbl_AB_Dietary_Assessment] DA on R.fd_id = DA.fd_resident_id" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " where SH.fd_home_id ="+ homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(DA.view_index,'')!= 'Y'";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.resident_id = rd[1];
                    l_J.DA_id = rd[2];
                    l_J.suite_no = rd[3];
                    l_J.first_name = rd[4];
                    l_J.last_name = rd[5];
                    l_J.Assessed_Date = rd[6].ToString();

                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_IAA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER() over(order by SSH.fd_move_in_date) as number, SSH.fd_resident_id," +
                                " SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date" +
                                " from tbl_Resident R" +
                                " join" +
                                " (select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, " +
                                " SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital," +
                                " SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor" +
                                " from tbl_Suite_Handler SH" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " ) " +
                                " as SSH" +
                                " on R.fd_id = SSH.fd_resident_id"+
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE() > SSH.fd_move_in_date" +
                                " and GETDATE() < isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and SSH.fd_resident_id not in (select distinct fd_resident_id from[dbo].[tbl_AB_Activity_Assessment])";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.resident_id = rd[1];
                    l_J.suite_no = rd[2];
                    l_J.first_name = rd[3];
                    l_J.last_name = rd[4];
                    l_J.move_in_date =DateTime.Parse(rd[5].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_IDA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER() over(order by SSH.fd_move_in_date) as number, SSH.fd_resident_id," +
                                " SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date" +
                                " from tbl_Resident R" +
                                " join" +
                                " (select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, " +
                                " SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital," +
                                " SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor" +
                                " from tbl_Suite_Handler SH" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " ) " +
                                " as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE() > SSH.fd_move_in_date" +
                                " and GETDATE() < isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and SSH.fd_resident_id not in (select distinct fd_resident_id from[dbo].[tbl_AB_Dietary_Assessment])";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.resident_id = rd[1];
                    l_J.suite_no = rd[2];
                    l_J.first_name = rd[3];
                    l_J.last_name = rd[4];
                    l_J.move_in_date = DateTime.Parse(rd[5].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_IFRA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER() over(order by SSH.fd_move_in_date) as number, SSH.fd_resident_id," +
                                " SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date" +
                                " from tbl_Resident R" +
                                " join" +
                                " (select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, " +
                                " SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital," +
                                " SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor" +
                                " from tbl_Suite_Handler SH" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " ) " +
                                " as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE() > SSH.fd_move_in_date" +
                                " and GETDATE() < isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and SSH.fd_resident_id not in (select distinct fd_resident_id from[dbo].[tbl_AB_Fall_Risk_Assessment])";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.resident_id = rd[1];
                    l_J.suite_no = rd[2];
                    l_J.first_name = rd[3];
                    l_J.last_name = rd[4];
                    l_J.move_in_date = DateTime.Parse(rd[5].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_PN_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by max(PP.fd_modified_on)) as number, R.fd_id, SSH.fd_suite_no, R.fd_first_name, R.fd_last_name,SSH.fd_move_in_date, max(PP.fd_modified_on) as fd_modified_on" +
                                " from tbl_Resident R" +
                                " left join tbl_Progress_Notes PP on PP.fd_resident_id = R.fd_id" +
                                " join" +
                                " (" +
                                " select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor" +
                                " from tbl_Suite_Handler SH" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " ) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and R.fd_id not in " +
                                " (" +
                                " select  R.fd_id" +
                                " from tbl_Resident R" +
                                " join tbl_Progress_Notes PN on PN.fd_resident_id = R.fd_id" +
                                " join tbl_Suite_Handler SH on R.fd_id = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and PN.fd_modified_on > DATEADD(DD, -30, GETDATE())" +
                                " )" +
                                " group by R.fd_id, SSH.fd_suite_no, R.fd_first_name, R.fd_last_name,SSH.fd_move_in_date";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.resident_id = rd[1];
                    l_J.suite_no = rd[2];
                    l_J.first_name = rd[3];
                    l_J.last_name = rd[4];
                    l_J.move_in_date = DateTime.Parse(rd[5].ToString()).ToString("yyyy-MM-dd");
                    if (rd[6] == null || rd[6].ToString() == "")
                    {
                        l_J.access_date = "";
                        l_J.due_date = "";
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).AddDays(30).ToString();
                        l_J.due_date = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddDays(30)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }


    }
}