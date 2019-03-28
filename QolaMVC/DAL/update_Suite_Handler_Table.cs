using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static int Hospitalization(int userid, string leaving, string ActualReturn,string hos_moveout, string hos_passaway, string notes , int reason,int RATable_ID,int SHtableID)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_Hospitalization", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@UserID", userid);
                cmdGARead.Parameters.AddWithValue("@leaving", leaving);
                cmdGARead.Parameters.AddWithValue("@actualreturn", ActualReturn);
                cmdGARead.Parameters.AddWithValue("@hos_moveout", hos_moveout);
                cmdGARead.Parameters.AddWithValue("@hos_passaway", hos_passaway);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.Parameters.AddWithValue("@RATable_ID", RATable_ID);
                cmdGARead.Parameters.AddWithValue("@SHtableID", SHtableID);
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
            cmd.CommandText =   " SELECT SH.fd_id,H.fd_name,S.fd_suite_no, SH.fd_move_in_date,SH.fd_move_out_date,CASE SH.fd_occupancy " +
                                " WHEN 1 THEN 'Single'"+
                                " WHEN 2 THEN 'Double'"+
                                " WHEN 3 THEN 'Triple'"+
                                " END as fd_occupancy, SH.fd_status,SH.fd_notes,isNULL(SH.fd_hospital,'') as hospital" +
                                " FROM tbl_Suite_Handler SH" +
                                " INNER JOIN tbl_Suite S ON S.fd_id = SH.fd_suite_id" +
                                " inner join tbl_Home H on H.fd_id = SH.fd_home_id" +
                                " WHERE SH.fd_resident_id ="+ residentID;
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            table.Append("<table class=\"table table-bordered\" id=\"showtable\" style=\"margin-left:1vw\">");
            table.Append("<thead class=\"bg-primary\">");
            table.Append("<tr>");
            table.Append("<th class=\"text-white text-center\">Home</th>");
            table.Append("<th class=\"text-white text-center\">Suite No</th>");
            table.Append("<th class=\"text-white text-center\">Move In Date</th>");
            table.Append("<th class=\"text-white text-center\">Move Out Date</th>");
            table.Append("<th class=\"text-white text-center\">Occupancy</th>");
            table.Append("<th class=\"text-white text-center\">Notes</th>");
            table.Append("<th class=\"text-white text-center\">Hospital</th>");
            table.Append("</tr>");
            table.Append("</thead>");
            table.Append("<tbody>");
             
            if (rd.HasRows)
                while (rd.Read())
                {

                    table.Append("<tr id='" + rd[0] +"'>");
                    table.Append("<td>" + rd[1] + "</td>");
                    table.Append("<td>" + rd[2] + "</td>");
                    table.Append("<td>" + DateTime.Parse(rd[3].ToString()).ToString("MMMM dd, yyyy") + "</td>");
                    
                    if (rd[4] != null && rd[4].ToString() != "")
                    {
                        table.Append("<td>" + DateTime.Parse(rd[4].ToString()).ToString("MMMM dd, yyyy") + "</td>");
                    }
                    else
                    {
                        table.Append("<td></td>");
                    }
                    table.Append("<td>" + rd[5] + "</td>");
                    table.Append("<td>" + rd[7] + "</td>");
                    table.Append("<td>" + rd[8] + "</td>");
                    table.Append("</tr>");
                }
            table.Append("</tbody>");
            table.Append("</table>");
            return table;
        }

        public static List<EDIT_SH_Model> get_innerHTML_temperary2(int residentID)
        {
            List<EDIT_SH_Model> AAlist = new List<EDIT_SH_Model>();
            EDIT_SH_Model AA;
            StringBuilder table = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " SELECT H.fd_name,S.fd_suite_no, SH.fd_move_in_date,SH.fd_move_out_date,CASE SH.fd_occupancy " +
                                " WHEN 1 THEN 'Single'" +
                                " WHEN 2 THEN 'Double'" +
                                " WHEN 3 THEN 'Triple'" +
                                " END as fd_occupancy, SH.fd_status,SH.fd_notes,isNULL(SH.fd_hospital,'') as hospital, SH.fd_id, H.fd_id, SH.fd_occupancy,SHS.fd_reason" +
                                " FROM tbl_Suite_Handler SH" +
                                " INNER JOIN tbl_Suite S ON S.fd_id = SH.fd_suite_id" +
                                " INNER JOIN tbl_Suite_Handler_Status SHS ON SHS.fd_id = SH.fd_status" +
                                " INNER JOIN tbl_Home H on H.fd_id = SH.fd_home_id" +
                                " WHERE SH.fd_resident_id =" + residentID;
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            DataSet AssesmentsReceive = new DataSet();

            if (rd.HasRows)
                while (rd.Read())
                {
                    AA = new EDIT_SH_Model();
                    AA.homename = rd[0].ToString();
                    AA.suiteno = rd[1].ToString();
                    if (rd[2] != null && rd[2].ToString() != "")
                        AA.movein = DateTime.Parse(rd[2].ToString()).ToString("yyyy-MM-dd");
                    else
                        AA.movein = "";
                    if (rd[3] != null && rd[3].ToString() != "")
                        AA.moveout = DateTime.Parse(rd[3].ToString()).ToString("yyyy-MM-dd");
                    else
                        AA.moveout = "";
                    AA.occupancy = rd[4].ToString();
                    AA.status = rd[5].ToString();
                    AA.notes = rd[6].ToString();
                    AA.hospital = rd[7].ToString();
                    AA.SHid = rd[8].ToString();
                    AA.homeid = rd[9].ToString();
                    AA.occuID = rd[10].ToString();
                    AA.reason = rd[11].ToString();


                    AAlist.Add(AA);
                }
            conn.Close();
            return AAlist;
        }



        public static List<string> get_Hospital_Info(int residentID)
        {
            List<string> retu = new List<string>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select top(1)RA.fd_id,RA.fd_leaving_date,isNULL(RA.fd_note,'') from tbl_Resident_Away_Schedule RA left join tbl_Suite_Handler SH on RA.fd_resident_id=SH.fd_resident_id" +
                              " where RA.fd_actual_return_date is null and RA.fd_home_id = 15 and SH.fd_pass_away_date is null and fd_move_out_date is null"+
                              " and RA.fd_resident_id="+residentID + " order by RA.fd_created_on DESC";
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    retu.Add(rd[0].ToString());
                    retu.Add(Convert.ToDateTime(rd[1].ToString()).ToString("yyyy-MM-dd"));
                    retu.Add(rd[2].ToString());
                }
            }

            else
            {
                retu.Add("");
                retu.Add("");
                retu.Add("");
            }
            conn.Close();
            return retu;
        }


        public static int EDIT_SAVE_function(EDIT_SH_Model sam)
        {

            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_EDIT_SAVE", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@residentid", int.Parse(sam.residentid));
                cmdGARead.Parameters.AddWithValue("@SHid", int.Parse(sam.SHid));
                cmdGARead.Parameters.AddWithValue("@homeID", int.Parse(sam.homeid));
                cmdGARead.Parameters.AddWithValue("@suiteno", sam.suiteno);
                cmdGARead.Parameters.AddWithValue("@movein", DateTime.Parse(sam.movein));
                if(sam.moveout!="" && sam.moveout!= null)
                    cmdGARead.Parameters.AddWithValue("@moveout", DateTime.Parse(sam.moveout));
                else
                    cmdGARead.Parameters.AddWithValue("@moveout", DBNull.Value);
                cmdGARead.Parameters.AddWithValue("@occu", int.Parse(sam.occuID));
                cmdGARead.Parameters.AddWithValue("@notes", sam.notes);
                cmdGARead.Parameters.AddWithValue("@hos", sam.hospital);
                cmdGARead.Parameters.Add("@returnint", SqlDbType.VarChar, 30);
                cmdGARead.Parameters["@returnint"].Direction = ParameterDirection.Output;
                cmdGARead.ExecuteNonQuery();
                int retunvalue = int.Parse(cmdGARead.Parameters["@returnint"].Value.ToString());
                return retunvalue;
            }
        }



    }
}