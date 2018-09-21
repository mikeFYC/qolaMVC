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

    }
}