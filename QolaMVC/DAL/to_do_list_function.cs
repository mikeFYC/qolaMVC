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

        public static List<dynamic> get_nextmonth_hospital_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER() over(order by SH.fd_hospital_leaving) as number, " +
                                " SH.fd_resident_id, S.fd_suite_no, R.fd_first_name, R.fd_last_name, " +
                                " R.fd_gender, SH.fd_hospital_leaving, fd_hospital_expected_return" +
                                " from[dbo].[tbl_Suite_Handler] SH" +
                                " join tbl_Resident R on SH.fd_resident_id=R.fd_id" +
                                " join tbl_Suite S on S.fd_id= SH.fd_suite_id" +
                                " where SH.fd_home_id=" + homeid +
                                " and SH.fd_pass_away_date is null " +
                                " and SH.fd_move_out_date is null  " +
                                " and SH.[fd_hospital_leaving] is not null" +
                                " and DATEADD(d, 1, EOMONTH(current_timestamp))<isNULL(SH.[fd_hospital_return],'2200-09-01')";
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

        public static List<dynamic> get_DU_list(int homeid, int userid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " declare @create datetime" +
                                " select @create = fd_created_on from tbl_User where fd_id =" + userid +
                                " select distinct ROW_NUMBER() over(order by DA.DateEntered) as row_number,  SH.fd_resident_id,DA.Id as DA_id,S.fd_suite_no,R.fd_first_name,R.fd_last_name,DA.DateEntered" +
                                " from [tbl_AB_DietaryAssessment] DA" +
                                " join tbl_Suite_Handler SH on DA.ResidentId = SH.fd_resident_id" +
                                " join tbl_Resident R on R.fd_id = DA.ResidentId" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and DA.DateEntered > @create" +
                                " and DA.Id not in (select distinct fd_DA_id from [tbl_AB_DietaryAssessment_Acknowledge] where fd_user_id =" + userid + ")";
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
                                " and SSH.fd_move_in_date is not null" +
                                " and GETDATE() < isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from [dbo].[tbl_AB_ActivityAssessment])";
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

        public static List<dynamic> get_nextmonth_IAA_list(int homeid)
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
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and DATEADD(d, 1, EOMONTH(current_timestamp))<SSH.fd_move_in_date" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from [dbo].[tbl_AB_ActivityAssessment])";
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
                                " and SSH.fd_move_in_date is not null" +
                                " and GETDATE() < isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from [dbo].[tbl_AB_DietaryAssessment])";
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

        public static List<dynamic> get_nextmonth_IDA_list(int homeid)
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
                                " and DATEADD(d, 1, EOMONTH(current_timestamp))<SSH.fd_move_in_date" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from [dbo].[tbl_AB_DietaryAssessment])";
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
                                " and SSH.fd_move_in_date is not null" +
                                " and GETDATE() < isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from [dbo].[tbl_AB_FallRiskAssessment])";
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

        public static List<dynamic> get_nextmonth_IFRA_list(int homeid)
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
                                " and DATEADD(d, 1, EOMONTH(current_timestamp))<SSH.fd_move_in_date" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from [dbo].[tbl_AB_FallRiskAssessment])";
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

        public static List<dynamic> get_IRCA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER() over(order by SSH.fd_move_in_date) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date" +
                                " from tbl_Resident R" +
                                " join" +
                                " (select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital, SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor" +
                                " from tbl_Suite_Handler SH" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " )" +
                                " as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and SSH.fd_move_in_date is not null" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!= 'Y'" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from[dbo].[tbl_AB_CarePlan])";
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

        public static List<dynamic> get_nextmonth_IRCA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER() over(order by SSH.fd_move_in_date) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date" +
                                " from tbl_Resident R" +
                                " join" +
                                " (select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital, SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor" +
                                " from tbl_Suite_Handler SH" +
                                " join tbl_Suite S on S.fd_id = SH.fd_suite_id" +
                                " )" +
                                " as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and DATEADD(d, 1, EOMONTH(current_timestamp))< SSH.fd_move_in_date" +
                                " and SSH.fd_resident_id not in (select distinct ResidentId from[dbo].[tbl_AB_CarePlan])";
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
            cmd.CommandText =   " select ROW_NUMBER()over(order by max(PP.fd_modified_on)) as number, R.fd_id, SSH.fd_suite_no, R.fd_first_name, R.fd_last_name,SSH.fd_move_in_date, max(PP.fd_modified_on) as fd_modified_on" +
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
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddDays(30)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_AN_list(int homeid, int userid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " declare @create datetime" +
                                " select @create = fd_created_on from tbl_User where fd_id =" + userid +

                                " select top (100) ROW_NUMBER()over(order by PN.fd_date DESC) as number,PN.fd_id,PN.fd_resident_id,QQ.fd_suite_no,R.fd_first_name,R.fd_last_name,SH.fd_move_in_date,PN.fd_date,PN.fd_title" +
                                " from tbl_Progress_Notes PN" +
                                " left join tbl_Suite_Handler SH on PN.fd_resident_id = SH.fd_resident_id" +
                                " left join tbl_Resident R on PN.fd_resident_id = R.fd_id and SH.fd_resident_id = R.fd_id" +
                                " left join" +
                                " (" +
                                " select SSH.fd_resident_id, S.fd_suite_no from tbl_Suite_Handler SSH join tbl_Suite S on SSH.fd_suite_id= S.fd_id" +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " ) " +
                                " as QQ on PN.fd_resident_id = QQ.fd_resident_id" +
                                " where PN.fd_date>@create" +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and PN.fd_modified_by !=" + userid +
                                " and SH.fd_home_id =" + homeid +
                                " and PN.fd_id not in (" +
                                " select fd_progress_notes_id from[dbo].[tbl_Progress_Notes_Acknowledgement]" +
                                " where fd_user_id ="+ userid + ")" +
                                " order by fd_date DESC";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.PN_number = rd[1];
                    l_J.resident_id = rd[2];
                    l_J.suite_no = rd[3];
                    l_J.first_name = rd[4];
                    l_J.last_name = rd[5];
                    l_J.move_in_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                    l_J.PN_date = rd[7].ToString();
                    l_J.PN_title = rd[8];

                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> AN_VIEW(int PNid, int residentid, int useid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " insert into [dbo].[tbl_Progress_Notes_Acknowledgement] values(@now, @PN, @userid);" +
                                " select fd_date, fd_category, U.fd_first_name,U.fd_last_name,PN.fd_note from tbl_Progress_Notes PN" +
                                " left join tbl_User U on PN.fd_modified_by = U.fd_id" +
                                " where PN.fd_id = @PN and PN.fd_resident_id = @resident";
            cmd.Parameters.AddWithValue("@userid", useid.ToString());
            cmd.Parameters.AddWithValue("@resident", residentid);
            cmd.Parameters.AddWithValue("@PN", PNid);
            cmd.Parameters.AddWithValue("@now", DateTime.Now);
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.date = rd[0].ToString();
                    l_J.category = rd[1];
                    l_J.first_name = rd[2];
                    l_J.last_name = rd[3];
                    l_J.note = rd[4];

                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_RAA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_ActivityAssessment] AAA on AAA.ResidentId = R.fd_id" +
                                " left join (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from [tbl_AB_ActivityAssessment] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM, -3, GETDATE())" +
                                " )" +
                                " and R.fd_id in (select distinct ResidentId from [tbl_AB_ActivityAssessment])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(3).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(3)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_nextmonth_RAA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_ActivityAssessment] AAA on AAA.ResidentId = R.fd_id" +
                                " left join (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from [tbl_AB_ActivityAssessment] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM,-3,DATEADD(d, 1, EOMONTH(current_timestamp)))" +
                                " )" +
                                " and R.fd_id in (select distinct ResidentId from [tbl_AB_ActivityAssessment])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(3).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(3)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_RDA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_DietaryAssessment] AAA on AAA.ResidentId = R.fd_id" +
                                " left join (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from [tbl_AB_DietaryAssessment] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM, -3, GETDATE())" +
                                " )" +
                                " and R.fd_id in (select distinct ResidentId from [tbl_AB_DietaryAssessment])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(3).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(3)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_nextmonth_RDA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_DietaryAssessment] AAA on AAA.ResidentId = R.fd_id" +
                                " left join (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from [tbl_AB_DietaryAssessment] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM,-3,DATEADD(d, 1, EOMONTH(current_timestamp)))" +
                                " )" +
                                " and R.fd_id in (select distinct ResidentId from [tbl_AB_DietaryAssessment])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(3).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(3)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_RFRA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_FallRiskAssessment] AAA on AAA.ResidentId = R.fd_id" +
                                " left join (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from [tbl_AB_FallRiskAssessment] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM, -5, GETDATE())" +
                                " )" +
                                " and R.fd_id in (select distinct ResidentId from [tbl_AB_FallRiskAssessment])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(5).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(5)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_nextmonth_RFRA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_FallRiskAssessment] AAA on AAA.ResidentId = R.fd_id" +
                                " left join (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!='Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from [tbl_AB_FallRiskAssessment] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM,-5,DATEADD(d, 1, EOMONTH(current_timestamp)))" +
                                " )" +
                                " and R.fd_id in (select distinct ResidentId from [tbl_AB_FallRiskAssessment])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(5).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(5)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_RRCA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_CarePlan] AAA on AAA.ResidentId = R.fd_id" +
                                " left join" +
                                " (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!= 'Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from[tbl_AB_CarePlan] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM, -5, GETDATE())" +
                                " ) " +
                                " and R.fd_id in (select distinct ResidentId from[tbl_AB_CarePlan])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date; ";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(5).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(5)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_nextmonth_RRCA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER()over(order by max(AAA.DateEntered)) as number, SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date,max(AAA.DateEntered) as assessed_date" +
                                " from tbl_Resident R" +
                                " left join [tbl_AB_CarePlan] AAA on AAA.ResidentId = R.fd_id" +
                                " left join" +
                                " (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on R.fd_id = SSH.fd_resident_id" +
                                " where SSH.fd_home_id =" + homeid +
                                " and GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and isNULL(SSH.fd_hospital,'')!= 'Y'" +
                                " and R.fd_id not in" +
                                " (" +
                                " select distinct AA.ResidentId" +
                                " from[tbl_AB_CarePlan] AA" +
                                " join tbl_Suite_Handler SH on AA.ResidentId = SH.fd_resident_id" +
                                " where SH.fd_home_id =" + homeid +
                                " and GETDATE()> SH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(MM,-5,DATEADD(d, 1, EOMONTH(current_timestamp)))" +
                                " ) " +
                                " and R.fd_id in (select distinct ResidentId from[tbl_AB_CarePlan])" +
                                " group by SSH.fd_resident_id,SSH.fd_suite_no, R.fd_first_name, R.fd_last_name, SSH.fd_move_in_date; ";
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
                        l_J.due_date = l_J.move_in_date;
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[5].ToString())).TotalDays);
                    }
                    else
                    {
                        l_J.access_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                        l_J.due_date = DateTime.Parse(rd[6].ToString()).AddMonths(5).ToString("yyyy-MM-dd");
                        l_J.due_days = Math.Floor((DateTime.Now - DateTime.Parse(rd[6].ToString()).AddMonths(5)).TotalDays);
                    }
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_RI_list(int homeid, int userid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " declare @mike datetime" +
                                " select @mike = fd_created_on from tbl_User where fd_id =" + userid +
                                " select ROW_NUMBER()over(order by PN.fd_fall_date) as number,PN.fd_id,PN.fd_resident_id,SSH.fd_suite_no,R.fd_first_name,R.fd_last_name,PN.fd_fall_date,PN.fd_location,PN.fd_note,PN.fd_action_note" +
                                " from tbl_Progress_Notes PN" +
                                " left join tbl_Resident R on PN.fd_resident_id = R.fd_id" +
                                " left join(select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital, SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id= S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH on SSH.fd_resident_id = PN.fd_resident_id" +
                                " where GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and fd_category = 6" +
                                " and PN.fd_modified_by !=" + userid +
                                " and isNULL(PN.mike_acknowledge,'') not like('%," + userid.ToString() + ",%')" +
                                " and SSH.fd_home_id =" + homeid +
                                " and PN.fd_date > @mike";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    dynamic l_J = new System.Dynamic.ExpandoObject();
                    l_J.number = rd[0];
                    l_J.PNid = rd[1];
                    l_J.resident_id = rd[2];
                    l_J.suite_no = rd[3];
                    l_J.first_name = rd[4];
                    l_J.last_name = rd[5];
                    l_J.fall_date =DateTime.Parse(rd[6].ToString()).Date.ToString("yyyy-MM-dd");
                    l_J.fall_time = DateTime.Parse(rd[6].ToString()).TimeOfDay.ToString();
                    l_J.fall_location = rd[7];
                    l_J.note = rd[8];
                    l_J.action_note = rd[9];
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static void get_RI_Acknowledge(int userid, int PNid, int residentid, string action)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " declare @mike nvarchar(max)" +
                                " select @mike = mike_acknowledge from tbl_Progress_Notes where fd_id = @pnid and fd_resident_id = @residentid" +
                                " if (@mike is null)" +
                                    " update tbl_Progress_Notes set mike_acknowledge = ','+@userid+',',fd_action_note=@actionnote" +
                                    " where fd_id = @pnid and fd_resident_id = @residentid" +
                                " else" +
                                    " update tbl_Progress_Notes set mike_acknowledge = mike_acknowledge + @userid +',',fd_action_note=@actionnote" +
                                    " where fd_id = @pnid and fd_resident_id = @residentid";
            cmd.Parameters.AddWithValue("@pnid", PNid);
            cmd.Parameters.AddWithValue("@residentid", residentid);
            cmd.Parameters.AddWithValue("@actionnote", action);
            cmd.Parameters.AddWithValue("@userid", userid.ToString());
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            conn.Close();
        }

        public static List<dynamic> get_RB_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER()over(order by fd_first_name) as number,SSH.fd_resident_id,SSH.fd_suite_no,R.fd_first_name,R.fd_last_name,R.fd_gender,DATEDIFF ( YEAR , R.fd_birth_date , GETDATE()) as age, R.fd_birth_date " +
                                " from tbl_Resident R" +
                                " left join(select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital, SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id= S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH on SSH.fd_resident_id = R.fd_id" +
                                " where GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and month(R.fd_birth_date)= month(GETDATE()) and day(R.fd_birth_date)= day(GETDATE())" +
                                " and SSH.fd_home_id ="+ homeid;
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
                    l_J.age = rd[6];
                    l_J.birth_date = DateTime.Parse(rd[7].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_RP_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER()over(order by fd_first_name) as number,SSH.fd_resident_id,SSH.fd_suite_no,R.fd_first_name,R.fd_last_name,R.fd_gender, R.fd_birth_date" +
                                " from tbl_Resident R" +
                                " left join(select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital, SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id= S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH on SSH.fd_resident_id = R.fd_id" +
                                " where GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and R.fd_image is null" +
                                " and SSH.fd_home_id ="+ homeid;
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
                    l_J.birth_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_NR_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " select ROW_NUMBER()over(order by R.fd_first_name) as number,R.fd_id,SSH.fd_suite_no,R.fd_first_name,R.fd_last_name,R.fd_gender,R.fd_birth_date" +
                                " from tbl_Resident R" +
                                " left join (select SH.fd_home_id,SH.fd_resident_id,SH.fd_occupancy,SH.fd_move_in_date,SH.fd_move_out_date,SH.fd_status,SH.fd_notes,SH.fd_modified_by,SH.fd_modified_on,SH.fd_pass_away_date,SH.fd_hospital,SH.fd_hospital_leaving,SH.fd_hospital_return,SH.fd_hospital_expected_return,S.fd_suite_no,S.fd_no_of_rooms,S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id = S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH on SSH.fd_resident_id = R.fd_id" +
                                " where GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and cast(R.fd_move_in_date as date)= CAST(GETDATE() AS date)" +
                                " and SSH.fd_home_id ="+ homeid;
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
                    l_J.birth_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_SA_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by AA.DateEntered) as number,SSH.fd_resident_id,SSH.fd_suite_no,R.fd_first_name,R.fd_last_name, AA.SAE,AA.DateEntered" +
                                " from [tbl_AB_ActivityAssessment_Store] AA" +
                                " left join tbl_Resident R on AA.ResidentId = R.fd_id" +
                                " left join(select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital, SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id= S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on SSH.fd_resident_id = AA.ResidentId" +
                                " where GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > DATEADD(DD, -30, GETDATE())" +
                                " and(AA.SAE != '' or AA.SAE != NULL)" +
                                " and SSH.fd_home_id = " + homeid;
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
                    l_J.suggested_event = rd[5];
                    l_J.modified_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }

        public static List<dynamic> get_SA_nextmonth_list(int homeid)
        {
            List<dynamic> l_Json = new List<dynamic>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select ROW_NUMBER()over(order by AA.DateEntered) as number,SSH.fd_resident_id,SSH.fd_suite_no,R.fd_first_name,R.fd_last_name, AA.SAE,AA.DateEntered" +
                                " from [tbl_AB_ActivityAssessment_Store] AA" +
                                " left join tbl_Resident R on AA.ResidentId = R.fd_id" +
                                " left join(select SH.fd_home_id, SH.fd_resident_id, SH.fd_occupancy, SH.fd_move_in_date, SH.fd_move_out_date, SH.fd_status, SH.fd_notes, SH.fd_modified_by, SH.fd_modified_on, SH.fd_pass_away_date, SH.fd_hospital, SH.fd_hospital_leaving, SH.fd_hospital_return, SH.fd_hospital_expected_return, S.fd_suite_no, S.fd_no_of_rooms, S.fd_floor from tbl_Suite_Handler SH join tbl_Suite S on SH.fd_suite_id= S.fd_id where GETDATE()> SH.fd_move_in_date and GETDATE()< isNULL(SH.fd_move_out_date, '2200-09-01')) as SSH" +
                                " on SSH.fd_resident_id = AA.ResidentId" +
                                " where GETDATE()> SSH.fd_move_in_date" +
                                " and GETDATE()< isNULL(SSH.fd_move_out_date, '2200-09-01')" +
                                " and AA.DateEntered > CAST(DATEADD(DAY,-DAY(GETDATE())+1, CAST(GETDATE() AS DATE)) AS datetime)" +
                                " and(AA.SAE != '' or AA.SAE != NULL)" +
                                " and SSH.fd_home_id = "+ homeid;
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
                    l_J.suggested_event = rd[5];
                    l_J.modified_date = DateTime.Parse(rd[6].ToString()).ToString("yyyy-MM-dd");
                    l_Json.Add(l_J);
                }
            conn.Close();
            return l_Json;
        }



        public static dynamic get_to_do_list_number(int userid, int homeid)
        {
            dynamic l_J = new System.Dynamic.ExpandoObject();
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("to_do_list_number_by_mike", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@UserID", userid);
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [dbo].[mike_to_do_list_number]";
                cmd.Connection = conn;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                    while (rd.Read())
                    {
                        l_J.DU_outer = rd[0];
                        l_J.DU_inner = rd[0];
                        l_J.HO_outer = rd[1];
                        l_J.HO_inner = rd[1];

                        l_J.IA =int.Parse(rd[2].ToString())+ int.Parse(rd[3].ToString())+ int.Parse(rd[4].ToString())+ int.Parse(rd[5].ToString());
                        l_J.IAA = rd[2];
                        l_J.IDA = rd[3];
                        l_J.IFRA = rd[4];
                        l_J.IRCA = rd[5];

                        l_J.PN_outer = rd[10];
                        l_J.PN = rd[10];


                        l_J.RA = int.Parse(rd[6].ToString()) + int.Parse(rd[7].ToString()) + int.Parse(rd[8].ToString()) + int.Parse(rd[9].ToString());
                        l_J.RAA = rd[6];
                        l_J.RDA = rd[7];
                        l_J.RFRA = rd[8];
                        l_J.RRCA = rd[9];

                        l_J.RI_outer = rd[12];
                        l_J.RI_inner = rd[12];
                        l_J.RB_outer = rd[13];
                        l_J.RB_inner = rd[13];
                        l_J.RP_outer = rd[14];
                        l_J.RP_inner = rd[14];
                        l_J.NR_outer = rd[15];
                        l_J.NR_inner = rd[15];
                        l_J.SAE_outer = rd[16];
                        l_J.SAE_inner = rd[16];
                    }
            }
            return l_J;
        }

        public static dynamic get_to_do_list_number_nextmonth(int userid, int homeid)
        {
            dynamic l_J = new System.Dynamic.ExpandoObject();
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("to_do_list_number_nextmonth_by_mike", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@UserID", userid);
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [dbo].[mike_to_do_list_number_nextmonth]";
                cmd.Connection = conn;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                    while (rd.Read())
                    {
                        l_J.HO_2_outer = rd[0];
                        l_J.HO_2_inner = rd[0];

                        l_J.IA_2 = int.Parse(rd[1].ToString()) + int.Parse(rd[2].ToString()) + int.Parse(rd[3].ToString()) + int.Parse(rd[4].ToString());
                        l_J.IAA_2 = rd[1];
                        l_J.IDA_2 = rd[2];
                        l_J.IFRA_2 = rd[3];
                        l_J.IRCA_2 = rd[4];


                        l_J.RA = int.Parse(rd[5].ToString()) + int.Parse(rd[6].ToString()) + int.Parse(rd[7].ToString()) + int.Parse(rd[8].ToString());
                        l_J.RAA = rd[5];
                        l_J.RDA = rd[6];
                        l_J.RFRA = rd[7];
                        l_J.RRCA = rd[8];

                        l_J.SAE_2_outer = rd[9];
                        l_J.SAE_2_inner = rd[9];
                    }
            }
            return l_J;
        }



    }
}