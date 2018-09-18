using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace QolaMVC.DAL
{
    public class update_Suite_Handler_Table
    {
        public static int ApplicationSuite(int userid,int homeid, int redidentid, string suiteno, int occupancy, DateTime movein, string notes, DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_ApplicationSuite", conn)
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
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static int ChangeOccupancy(int userid,int homeid, int redidentid, string suiteno, int occupancy, DateTime movein, DateTime moveout, string notes, DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_ChangeOccupancy", conn)
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
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static int InternalTransfer(int userid,int homeid, int redidentid, string suiteno, int occupancy, DateTime movein, DateTime moveout, string notes, DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_InternalTransfer", conn)
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
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static int TransfertoASCHOME(int userid,int homeid, int redidentid, string suiteno, int occupancy, DateTime movein, DateTime moveout, string notes, DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_TransfertoASCHOME", conn)
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
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }
                            
        public static int Normal_Move_Out(int userid,int homeid, int redidentid, string suiteno, int occupancy, DateTime moveout, string notes, DateTime modify_on, string reason)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_Normal_Move_Out", conn)
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
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static int Passed_away(int userid, int homeid, int redidentid, string suiteNo, int occupancy, DateTime moveout, string notes, DateTime modify_on, DateTime passedaway, string reason)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_Passed_away", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@UserID", userid);
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suiteno", suiteNo);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.AddWithValue("@pass_away_date", passedaway);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static int Hospitalization(int userid, int homeid, int redidentid, string suiteno, int occupancy, string leaving, string ExpectedReturn, string ActualReturn, string notes, DateTime modify_on, string reason)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_Hospitalization", conn)
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
                cmdGARead.Parameters.AddWithValue("@leaving", leaving);
                cmdGARead.Parameters.AddWithValue("@expectedreturn", ExpectedReturn);
                cmdGARead.Parameters.AddWithValue("@actualreturn", ActualReturn);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static int undo_function_for_SQL(int redidentid, string reason)
        {

            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_UNDO", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }

        public static Boolean check_date_validation(DateTime term)
        {
            DateTime today = DateTime.Now;
            DateTime twoweeksago = today.AddDays(-14);
            int result = DateTime.Compare(twoweeksago, term);
            if (result <= 0)
                return true;
            else
                return false;
        }

        public static StringBuilder get_innerHTML(int residentID)
        {
            StringBuilder table = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =   " SELECT H.fd_name,S.fd_suite_no, SH.fd_move_in_date,SH.fd_move_out_date,CASE SH.fd_occupancy "+
                                " WHEN 1 THEN 'Single'"+
                                " WHEN 2 THEN 'Double'"+
                                " WHEN 3 THEN 'Triple'"+
                                " END as fd_occupancy, SH.fd_status,SH.fd_notes" +
                                " FROM tbl_Suite S" +
                                " INNER JOIN tbl_Suite_Handler SH ON S.fd_id = SH.fd_suite_id" +
                                " inner join tbl_Home H on H.fd_id = SH.fd_home_id" +
                                " WHERE SH.fd_resident_id ="+ residentID;
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            table.Append("<table class=\"table table-bordered\" style=\"margin-left:1vw\">");
            table.Append("<thead class=\"bg-primary\">");
            table.Append("<tr>");
            table.Append("<th class=\"text-white text-center\">Home</th>");
            table.Append("<th class=\"text-white text-center\">Suite No</th>");
            table.Append("<th class=\"text-white text-center\">Move In Date</th>");
            table.Append("<th class=\"text-white text-center\">Move Out Date</th>");
            table.Append("<th class=\"text-white text-center\">Occupancy</th>");
            table.Append("<th class=\"text-white text-center\">Notes</th>");
            table.Append("</tr>");
            table.Append("</thead>");
            table.Append("<tbody>");
             
            if (rd.HasRows)
                while (rd.Read())
                {

                    table.Append("<tr>");
                    table.Append("<td>" + rd[0] + "</td>");
                    table.Append("<td>" + rd[1] + "</td>");
                    if (rd[2] != null && rd[2].ToString() != "")
                    {
                        table.Append("<td>" + DateTime.Parse(rd[2].ToString()).ToString("MMMM dd, yyyy") + "</td>");
                    }
                    else
                    {
                        table.Append("<td>" + rd[2] + "</td>");
                    }
                    if (rd[3] != null && rd[3].ToString() != "")
                    {
                        table.Append("<td>" + DateTime.Parse(rd[3].ToString()).ToString("MMMM dd, yyyy") + "</td>");
                    }
                    else
                    {
                        table.Append("<td>" + rd[3] + "</td>");
                    }
                    table.Append("<td>" + rd[4] + "</td>");
                    table.Append("<td>" + rd[6] + "</td>");
                    table.Append("</tr>");
                }
            table.Append("</tbody>");
            table.Append("</table>");
            return table;
        }

        public static string get_Leaving_date(int residentID)
        {
            string retu="";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select top(1)[fd_hospital_leaving] from [tbl_Suite_Handler] where [fd_hospital]='Y' and fd_resident_id="+ residentID + " order by fd_modified_on DESC";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
                while (rd.Read())
                {
                    if (rd[0] == null || rd[0].ToString() == "")
                        retu = "";
                    else
                        retu = rd[0].ToString();
                }
            return retu;
        }
    }
}