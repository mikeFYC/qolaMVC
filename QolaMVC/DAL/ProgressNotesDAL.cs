using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.DAL
{
    #region "ProgressNotes"

    public class ProgressNotesDAL
    {
        #region "Method"        

        public static int AddNewProgressNotes(ProgressNotesModel p_ProgressNotes)
        {
            string exception = string.Empty;
            int progressNotesId = 0;
            Database db;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_ADD_PROGRESS_NOTES);
                db.AddOutParameter(cmd, "@id", DbType.Int32, p_ProgressNotes.ID);
                db.AddInParameter(cmd, "@residentId", DbType.Int32, p_ProgressNotes.Resident.ID);
                db.AddInParameter(cmd, "@date", DbType.DateTime, p_ProgressNotes.Date);
                db.AddInParameter(cmd, "@title", DbType.String, p_ProgressNotes.Title);
                db.AddInParameter(cmd, "@note", DbType.String, p_ProgressNotes.Note);
                db.AddInParameter(cmd, "@createdby", DbType.Int32, p_ProgressNotes.ModifiedBy.ID);
                db.AddInParameter(cmd, "@category", DbType.Int16, p_ProgressNotes.Category);
                db.AddInParameter(cmd, "@reminIn", DbType.Int16, p_ProgressNotes.RemainIn);


                db.AddInParameter(cmd, "@falldatetype", DbType.String, Convert.ToChar(p_ProgressNotes.FallDateType));
                if (p_ProgressNotes.sFallDate != "")
                {
                    db.AddInParameter(cmd, "@falldate", DbType.DateTime, p_ProgressNotes.sFallDate);
                }

                db.AddInParameter(cmd, "@location", DbType.String, p_ProgressNotes.Location);
                db.AddInParameter(cmd, "@witnesstype", DbType.String, Convert.ToChar(p_ProgressNotes.WitnessType));
                db.AddInParameter(cmd, "@witnessfall", DbType.String, p_ProgressNotes.WitnessFall);
                db.AddInParameter(cmd, "@Unwitnestype", DbType.String, Convert.ToChar(p_ProgressNotes.UnWitnessType));
                db.AddInParameter(cmd, "@incidentreport", DbType.String, Convert.ToChar(p_ProgressNotes.IncidentReport));
                progressNotesId = db.ExecuteNonQuery(cmd);
                if (progressNotesId != 0)
                {
                    progressNotesId = Convert.ToInt32(db.GetParameterValue(cmd, "@id"));
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes AddNewProgressNotes |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return progressNotesId;
        }

        public static bool AddNewProgressNotesVerified(string iProgressNotesId, int iUserId)
        {
            string exception = string.Empty;
            bool bResultprogressNotes = false;
            int iResultprogressNotes = 0;
            Database db;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_ADD_PROGRESS_NOTES_ACKNOWLEDGEMENT);
                db.AddInParameter(cmd, "@progressIds", DbType.String, iProgressNotesId);
                db.AddInParameter(cmd, "@userId", DbType.Int32, iUserId);
                iResultprogressNotes = db.ExecuteNonQuery(cmd);
                if (iResultprogressNotes != 0)
                {
                    bResultprogressNotes = true;
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes AddNewProgressNotesVerified |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return bResultprogressNotes;
        }

        public static int UpdateProgressNotes(Common.ProgressNotes objProgressNotes)
        {
            string exception = string.Empty;
            int progressNotesId = 0;
            Database db;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_UPDATE_PROGRESS_NOTES);
                db.AddInParameter(cmd, "@id", DbType.Int32, objProgressNotes.ID);
                db.AddInParameter(cmd, "@residentId", DbType.Int32, objProgressNotes.Resident.ID);
                db.AddInParameter(cmd, "@date", DbType.DateTime, objProgressNotes.Date);
                db.AddInParameter(cmd, "@title", DbType.String, objProgressNotes.Title);
                db.AddInParameter(cmd, "@note", DbType.String, objProgressNotes.Note);
                db.AddInParameter(cmd, "@createdby", DbType.Int32, objProgressNotes.ModifiedBy.ID);
                db.AddInParameter(cmd, "@category", DbType.Int16, objProgressNotes.Category);
                db.AddInParameter(cmd, "@reminIn", DbType.Int16, objProgressNotes.RemainIn);
                progressNotesId = db.ExecuteNonQuery(cmd);
                if (progressNotesId != 0)
                {
                    progressNotesId = 1;
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes UpdateProgressNotes |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return progressNotesId;
        }

        public static Collection<Common.ProgressNotes> GetProgressNotesCollections(int residentId, DateTime fromDate, DateTime toDate, string stype, int iUserType = 0, int iCategory = 0)
        {
            string exception = string.Empty;
            Database db;
            Collection<Common.ProgressNotes> objProgressNotes = new Collection<Common.ProgressNotes>();
            Common.ProgressNotes objProgressNote;
            Common.Residents objResident;
            Common.Users objUsers;
            Common.Users objAckUser;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_GET_PROGRESS_NOTES);
                db.AddInParameter(cmd, "@residentId", DbType.Int32, residentId);
                db.AddInParameter(cmd, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(cmd, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(cmd, "@type", DbType.String, stype);
                db.AddInParameter(cmd, "@userType", DbType.Int32, iUserType);
                db.AddInParameter(cmd, "@category", DbType.Int32, iCategory);
                DataSet progressNotesReceive = db.ExecuteDataSet(cmd);
                if (progressNotesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= progressNotesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        objProgressNote = new Common.ProgressNotes();
                        objResident = new Common.Residents();
                        objUsers = new Common.Users();
                        objAckUser = new Common.Users();
                        objProgressNote.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_id"]);
                        objResident.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_resident_id"]);
                        objProgressNote.Resident = objResident;
                        objProgressNote.Date = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_date"]);
                        objProgressNote.Category = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_category"]);
                        objProgressNote.Title = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_title"]);
                        objProgressNote.Note = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_note"]);
                        objUsers.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_first_name"]);
                        objUsers.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_last_name"]);
                        objUsers.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_user_id"]);
                        objUsers.UserTypeName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_name"]);
                        objProgressNote.RemainIn = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_remain_in"]);
                        objProgressNote.ModifiedBy = objUsers;

                        objAckUser.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_ack_fname"]);
                        objAckUser.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_ack_Lname"]);
                        objAckUser.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_acknowledged_by"]);
                        objAckUser.UserTypeName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_ack_utName"]);
                        objAckUser.ModifiedOn = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_acknowledged_on"]);

                        objProgressNote.FallDateType = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_fall_date_type"]);
                        objProgressNote.FallDate = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_fall_date"]);
                        objProgressNote.Location = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_location"]);
                        objProgressNote.WitnessType = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_witness_type"]);
                        objProgressNote.WitnessFall = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_witness_fall"]);
                        objProgressNote.UnWitnessType = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_Un_witnes_type"]);
                        objProgressNote.IncidentReport = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_incident_report"]);
                        objProgressNote.AcknowledgeNote = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_action_note"]);
                        objProgressNote.ACkFlag = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["As_flag"]);
                        objProgressNote.AcknowledgedBy = objAckUser;
                        objProgressNotes.Add(objProgressNote);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetProgressNotesCollections |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return objProgressNotes;
        }

        public static Common.ProgressNotes GetProgressNotesById(int iProgressNotesId)
        {
            string exception = string.Empty;
            Database db;
            Common.ProgressNotes objProgressNote = new Common.ProgressNotes();
            Common.Residents objResident;
            Common.Users objUsers;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_GET_PROGRESS_NOTES_BY_ID);
                db.AddInParameter(cmd, "@id", DbType.Int32, iProgressNotesId);
                DataSet progressNotesReceive = db.ExecuteDataSet(cmd);
                if (progressNotesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= progressNotesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        objResident = new Common.Residents();
                        objUsers = new Common.Users();
                        objProgressNote.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_id"]);
                        objResident.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_resident_id"]);
                        objProgressNote.Resident = objResident;
                        objProgressNote.Date = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_date"]);
                        objProgressNote.Title = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_title"]);
                        objProgressNote.Note = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_note"]);
                        objUsers.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_modified_by"]);
                        objProgressNote.Category = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_category"]);
                        objProgressNote.RemainIn = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_remain_in"]);
                        objProgressNote.ModifiedBy = objUsers;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetProgressNotesById |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return objProgressNote;
        }

        public static bool RemoveProgressNotes(int ProgressNoteId)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            Database db;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_REMOVE_PROGRESS_NOTES);
                db.AddInParameter(cmd, "@id", DbType.Int32, ProgressNoteId);
                affected = db.ExecuteNonQuery(cmd);
                if (affected == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes RemoveProgressNotes |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return result;
        }

        public static DataTable GetPersonalCalendarByResidentId(DateTime eventFromDate, DateTime eventToDate, int residentId, char cHomeFlag)
        {
            string exception = string.Empty;
            DataSet dtPersonalCalendar = null;
            Database db;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_GET_PERSONAL_CALENDAR_BY_RESIDENT_ID);
                db.AddInParameter(cmd, "@residentId", DbType.Int32, residentId);
                db.AddInParameter(cmd, "@fromDate", DbType.DateTime, eventFromDate);
                db.AddInParameter(cmd, "@toDate", DbType.DateTime, eventToDate);
                db.AddInParameter(cmd, "@homeFlag", DbType.String, cHomeFlag);
                cmd.CommandTimeout = 1000;
                dtPersonalCalendar = new DataSet();
                dtPersonalCalendar = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetPersonalCalendarByResidentId |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return dtPersonalCalendar.Tables[0];
        }
        #endregion

        public static Collection<Common.ProgressNotes> GetProgressNotesVerified(int iHomeId, int iUserId)
        {
            string exception = string.Empty;
            Database db;
            Collection<Common.ProgressNotes> objProgressNotes = new Collection<Common.ProgressNotes>();
            Common.ProgressNotes objProgressNote;
            Common.Users objUsers;
            Common.Residents objResident;
            Common.Suite objSuite;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_GET_PROGRESS_NOTES_ACKNOWLEDGEMENT);
                db.AddInParameter(cmd, "@homeId", DbType.Int32, iHomeId);
                db.AddInParameter(cmd, "@userId", DbType.Int32, iUserId);
                DataSet progressNotesReceive = db.ExecuteDataSet(cmd);
                if (progressNotesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= progressNotesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        objProgressNote = new Common.ProgressNotes();
                        objUsers = new Common.Users();
                        objResident = new Common.Residents();
                        objSuite = new Common.Suite();
                        objProgressNote.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_id"]);
                        objProgressNote.Date = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_date"]);
                        objProgressNote.Title = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_title"]);
                        objProgressNote.Note = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_note"]);
                        objUsers.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_first_name"]);
                        objUsers.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_last_name"]);
                        objSuite.SuiteNo = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_suite_no"]);
                        objResident.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_resident_first_name"]);
                        objResident.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_resident_last_name"]);
                        objProgressNote.Resident = objResident;
                        objProgressNote.Suite = objSuite;
                        objProgressNote.ModifiedBy = objUsers;
                        objProgressNotes.Add(objProgressNote);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetProgressNotesVerified |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return objProgressNotes;
        }

        public static int AddProgressNoteAcknowledge(Common.ProgressNotes objProgressNotes)
        {
            string exception = string.Empty;
            int progressNotesId = 0;
            Database db;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_ADD_PROGRESS_NOTES_ACKNOWLEDGED);
                db.AddInParameter(cmd, "@id", DbType.Int32, objProgressNotes.ID);
                db.AddInParameter(cmd, "@note", DbType.String, objProgressNotes.AcknowledgeNote);
                db.AddInParameter(cmd, "@createdby", DbType.Int32, objProgressNotes.ModifiedBy.ID);
                progressNotesId = db.ExecuteNonQuery(cmd);
                if (progressNotesId != 0)
                {
                    progressNotesId = 1;
                }
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes AddProgressNoteAcknowledge |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return progressNotesId;
        }

        #region "Resident Calender"
        public static DataTable GetResidentBirthDayCalenderByHomeId(DateTime eventFromDate, DateTime eventToDate, int homeId)
        {
            string exception = string.Empty;
            DataSet dtPersonalCalendar = null;
            Database db;
            try
            {
                db = Common.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedureName.USP_GET_RESIDENT_BIRTH_DAY_CALENDAR_BY_HOME_ID);
                db.AddInParameter(cmd, "@homeId", DbType.Int32, homeId);
                db.AddInParameter(cmd, "@fromDate", DbType.DateTime, eventFromDate);
                db.AddInParameter(cmd, "@toDate", DbType.DateTime, eventToDate);
                dtPersonalCalendar = new DataSet();
                dtPersonalCalendar = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetResideCalenderByHomeId |" + ex.ToString();
                Log.Write(exception);
                throw;
            }
            return dtPersonalCalendar.Tables[0];
        }

        #endregion "Resident Calender"
    }
    #endregion
}