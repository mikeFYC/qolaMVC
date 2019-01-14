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

        public static int Hospitalization(int userid, int homeid, int redidentid, string suiteno, int occupancy, string leaving, string ExpectedReturn, string ActualReturn, string notes, DateTime modify_on, string reason,int RATable_ID)
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
                cmdGARead.Parameters.AddWithValue("@RATable_ID", RATable_ID);
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
                                " END as fd_occupancy, SH.fd_status,SH.fd_notes,isNULL(SH.fd_hospital,'') as hospital" +
                                " FROM tbl_Suite S" +
                                " INNER JOIN tbl_Suite_Handler SH ON S.fd_id = SH.fd_suite_id" +
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
                    table.Append("<td>" + rd[7] + "</td>");
                    table.Append("</tr>");
                }
            table.Append("</tbody>");
            table.Append("</table>");
            return table;
        }

        public static StringBuilder get_innerHTML_temperary(int residentID)
        {
            StringBuilder table = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " SELECT H.fd_name,S.fd_suite_no, SH.fd_move_in_date,SH.fd_move_out_date,CASE SH.fd_occupancy " +
                                " WHEN 1 THEN 'Single'" +
                                " WHEN 2 THEN 'Double'" +
                                " WHEN 3 THEN 'Triple'" +
                                " END as fd_occupancy, SH.fd_status,SH.fd_notes,isNULL(SH.fd_hospital,'') as hospital, SH.fd_id, H.fd_id" +
                                " FROM tbl_Suite S" +
                                " INNER JOIN tbl_Suite_Handler SH ON S.fd_id = SH.fd_suite_id" +
                                " inner join tbl_Home H on H.fd_id = SH.fd_home_id" +
                                " WHERE SH.fd_resident_id =" + residentID;
            cmd.Connection = conn;
            SqlDataReader rd = cmd.ExecuteReader();
            table.Append("<table class=\"table table-bordered\" style=\"margin-left:1vw\">");
            table.Append("<thead class=\"bg-primary\">");
            table.Append("<tr>");
            table.Append("<th class=\"text-white text-center\"></th>");
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

                    table.Append("<tr>");
                    table.Append("<td><input type=\"checkbox\" class=\"checkboxlist\" id="+ rd[8] + " /></td>");
                    table.Append("<td><select id=\"edit_home\" value=" + rd[9] + ">");

                    table.Append("<option value=\"15\">Auburn Heights Retirement Residence</option>");
                    table.Append("<option value=\"5\"> Laurel Heights Retirement Residence </option>");
                    table.Append("<option value=\"26\">Lewis Estates</option>");
                    table.Append("<option value=\"35\">Mactaggart Place Retirement Residence</option>");
                    table.Append("<option value=\"1\"> Rutherford Heights Retirement Residence </option>");
                    table.Append("<option value=\"33\">Sage Hill Retirement Residence</option>");
                    table.Append("<option value=\"31\">St. Albert Retirement Residence</option>");
                    table.Append("<option value=\"3\"> Summerwood Village Retirement Residence </option>");
                    table.Append("<option value=\"30\">River Ridge 2 Retirement Residence</option>");
                    table.Append("<option value=\"4\"> River Ridge Retirement Residence </option>");
                    table.Append("<option value=\"17\">Seine River Retirement Residence</option>");
                    table.Append("<option value=\"6\"> Shaftesbury Park Retirement Residence </option>");
                    table.Append("<option value=\"18\">Sturgeon Creek I Retirement Residence</option>");
                    table.Append("<option value=\"13\">Sturgeon Creek II Retirement Residence</option>");
                    table.Append("<option value=\"12\">Victoria Landing Retirement Residence</option>");
                    table.Append("<option value=\"2\"> Beacon Heights Retirement Residence </option>");
                    table.Append("<option value=\"7\"> Cedarcroft Place Retirement Residence </option>");
                    table.Append("<option value=\"27\">Chapel Hill Retirement Residence</option>");
                    table.Append("<option value=\"32\">Cit&#233; Parkway Retirement Residence</option>");
                    table.Append("<option value=\"34\">Eagleson Retirement Residence</option>");
                    table.Append("<option value=\"28\">Mccarthy Place</option>");
                    table.Append("<option value=\"29\">Sample</option>");
                    table.Append("<option value=\"16\">College Park II Retirement Residence</option>");
                    table.Append("<option value=\"8\"> College Park Retirement Residence </option>");
                    table.Append("<option value=\"14\">Preston Park II Retirement Residence</option>");
                    table.Append("<option value=\"11\">Preston Park Retirement Residence</option>");
                    table.Append("<option value=\"20\">465, Boulevard De La Gappe</option>");
                    table.Append("<option value=\"21\">475 Boulevard De La Gappe</option>");
                    table.Append("<option value=\"22\">485 Boulevard De La Gappe</option>");
                    table.Append("<option value=\"23\">495 Boulevard De La Gappe</option>");
                    table.Append("<option value=\"19\">Chateau Symmes Retirement Residence</option>");

                    table.Append("</select></td>");

                    table.Append("<td>");
                    table.Append("<select name = \"\" value=" + rd[1] + " id = \"available_Suite_EDIT\" class=\"form-control\" onfocus=\"suitenumber4()\"></select>");
                    table.Append("</td>");



                    

                    if (rd[2] != null && rd[2].ToString() != "")
                    {
                        table.Append("<td><input id=\"edit_timein\" type=\"date\" value="+ DateTime.Parse(rd[2].ToString()).ToShortDateString() + " /></td>");
                    }
                    else
                    {
                        table.Append("<td><input type=\"date\" value=" + rd[2] + " /></td>");
                    }
                    if (rd[3] != null && rd[3].ToString() != "")
                    {
                        table.Append("<td>" + DateTime.Parse(rd[3].ToString()).ToShortDateString() + "</td>");
                    }
                    else
                    {
                        table.Append("<td>" + rd[3] + "</td>");
                    }
                    table.Append("<td>" + rd[4] + "</td>");
                    table.Append("<td>" + rd[6] + "</td>");
                    table.Append("<td>" + rd[7] + "</td>");
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
                                " END as fd_occupancy, SH.fd_status,SH.fd_notes,isNULL(SH.fd_hospital,'') as hospital, SH.fd_id, H.fd_id, SH.fd_occupancy" +
                                " FROM tbl_Suite S" +
                                " INNER JOIN tbl_Suite_Handler SH ON S.fd_id = SH.fd_suite_id" +
                                " inner join tbl_Home H on H.fd_id = SH.fd_home_id" +
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
                    if (AA.status == "1") AA.reason = "Single suite";
                    else if (AA.status == "2") AA.reason = "Double suite";
                    else if (AA.status == "3") AA.reason = "Triple suite";
                    else if (AA.status == "4") AA.reason = "Transfer to another ASC Home";
                    else if (AA.status == "5") AA.reason = "End of Lease";
                    else if (AA.status == "6") AA.reason = "Palliative care";
                    else if (AA.status == "7") AA.reason = "LTC";
                    else if (AA.status == "8") AA.reason = "Transfer to Other Retirement home";
                    else if (AA.status == "9") AA.reason = " Passed Away";
                    else if (AA.status == "10") AA.reason = "Other";
                    else if (AA.status == "11") AA.reason = "Hospitalization";
                    else if (AA.status == "12") AA.reason = "Personal Leave";
                    else if (AA.status == "13") AA.reason = "Medical Leave";
                    else if (AA.status == "14") AA.reason = "Reason Unknown";
                    else if (AA.status == "15") AA.reason = "New Resident";
                    else  AA.reason = "";
                   

                    AAlist.Add(AA);
                }
            conn.Close();
            return AAlist;
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
            conn.Close();
            return retu;
        }

        public static string get_Resident_Away_Schedule(int residentID)
        {
            string retu = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Constants.ConnectionString.PROD;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select top(1)RA.fd_id from tbl_Resident_Away_Schedule RA left join tbl_Suite_Handler SH on RA.fd_resident_id=SH.fd_resident_id"+
                              " where RA.fd_actual_return_date is null and RA.fd_home_id = 15 and SH.fd_pass_away_date is null and fd_move_out_date is null"+
                              " and RA.fd_resident_id="+residentID + " order by RA.fd_created_on DESC";
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