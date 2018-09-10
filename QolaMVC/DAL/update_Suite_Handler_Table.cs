﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QolaMVC.DAL
{
    public class update_Suite_Handler_Table
    {
        public static void ApplicationSuite(int homeid,int redidentid,int suiteid,int occupancy,DateTime movein,string notes,int modify_by,DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_ApplicationSuite", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suitid", suiteid);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_by", modify_by);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.ExecuteNonQuery();
            }
        }

        public static void ChangeOccupancy(int homeid, int redidentid, int suiteid, int occupancy, DateTime movein, DateTime moveout, string notes, int modify_by, DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_ChangeOccupancy", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suitid", suiteid);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_by", modify_by);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.ExecuteNonQuery();
            }
        }

        public static void InternalTransfer(int homeid, int redidentid, int suiteid, int occupancy, DateTime movein, DateTime moveout, string notes, int modify_by, DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_InternalTransfer", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suitid", suiteid);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_by", modify_by);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.ExecuteNonQuery();
            }
        }

        public static void TransfertoASCHOME(int homeid, int redidentid, int suiteid, int occupancy, DateTime movein, DateTime moveout, string notes, int modify_by, DateTime modify_on)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_TransfertoASCHOME", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suitid", suiteid);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@moveInDate", movein);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_by", modify_by);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.ExecuteNonQuery();
            }
        }

        public static void Normal_Move_Out(int homeid, int redidentid, int suiteid, int occupancy, DateTime moveout, string notes, DateTime modify_on, string reason)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_Normal_Move_Out", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suitid", suiteid);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.ExecuteNonQuery();
            }
        }

        public static void Passed_away(int homeid, int redidentid, int suiteid, int occupancy, DateTime moveout, string notes, DateTime modify_on, DateTime passedaway, string reason)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_Passed_away", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suitid", suiteid);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@moveoutdate", moveout);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.AddWithValue("@pass_away_date", passedaway);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.ExecuteNonQuery();
            }
        }

        public static void Hospitalization(int homeid, int redidentid, int suiteid, int occupancy, DateTime leaving, DateTime ExpectedReturn, DateTime ActualReturn, string notes, DateTime modify_on, string reason)
        {
            using (var conn = new SqlConnection(Constants.ConnectionString.PROD))
            using (var cmdGARead = new SqlCommand("Suite_Handler_Hospitalization", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                cmdGARead.Parameters.AddWithValue("@homeID", homeid);
                cmdGARead.Parameters.AddWithValue("@redidentid", redidentid);
                cmdGARead.Parameters.AddWithValue("@suitid", suiteid);
                cmdGARead.Parameters.AddWithValue("@occupancy", occupancy);
                cmdGARead.Parameters.AddWithValue("@leaving", leaving);
                cmdGARead.Parameters.AddWithValue("@expectedreturn", ExpectedReturn);
                cmdGARead.Parameters.AddWithValue("@actualreturn", ActualReturn);
                cmdGARead.Parameters.AddWithValue("@notes", notes);
                cmdGARead.Parameters.AddWithValue("@modify_on", modify_on);
                cmdGARead.Parameters.AddWithValue("@reason", reason);
                cmdGARead.ExecuteNonQuery();
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




    }
}