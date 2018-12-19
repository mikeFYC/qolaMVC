using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_PROGRESS_NOTES, l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", p_ProgressNotes.ID);
                l_Cmd.Parameters.AddWithValue("@residentId", p_ProgressNotes.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@date", p_ProgressNotes.Date);
                l_Cmd.Parameters.AddWithValue("@title", p_ProgressNotes.Title);
                l_Cmd.Parameters.AddWithValue("@note", p_ProgressNotes.Note);
                l_Cmd.Parameters.AddWithValue("@createdby", p_ProgressNotes.ModifiedBy.ID);
                l_Cmd.Parameters.AddWithValue("@category", p_ProgressNotes.Category);
                l_Cmd.Parameters.AddWithValue("@reminIn", p_ProgressNotes.RemainIn);


                l_Cmd.Parameters.AddWithValue("@falldatetype", Convert.ToChar(p_ProgressNotes.FallDateType));
                if (p_ProgressNotes.sFallDate != "")
                {
                    l_Cmd.Parameters.AddWithValue("@falldate", DateTime.Parse(p_ProgressNotes.sFallDate));
                }

                l_Cmd.Parameters.AddWithValue("@location", p_ProgressNotes.Location);
                l_Cmd.Parameters.AddWithValue("@witnesstype", Convert.ToChar(p_ProgressNotes.WitnessType));
                l_Cmd.Parameters.AddWithValue("@witnessfall", p_ProgressNotes.WitnessFall);
                l_Cmd.Parameters.AddWithValue("@Unwitnestype", Convert.ToChar(p_ProgressNotes.UnWitnessType));
                l_Cmd.Parameters.AddWithValue("@incidentreport", Convert.ToChar(p_ProgressNotes.IncidentReport));
                progressNotesId = l_Cmd.ExecuteNonQuery();
                if (progressNotesId != 0)
                {
                    //progressNotesId = Convert.ToInt32(db.GetParameterValue(l_Cmd, "@id"));
                }
                return progressNotesId;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes AddNewProgressNotes |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool AddNewProgressNotesVerified(string iProgressNotesId, int iUserId)
        {
            string exception = string.Empty;
            bool bResultprogressNotes = false;
            int iResultprogressNotes = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_PROGRESS_NOTES_ACKNOWLEDGEMENT, l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@progressIds", iProgressNotesId);
                l_Cmd.Parameters.AddWithValue("@userId", iUserId);
                iResultprogressNotes = l_Cmd.ExecuteNonQuery();
                if (iResultprogressNotes != 0)
                {
                    bResultprogressNotes = true;
                }
                return bResultprogressNotes;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes AddNewProgressNotesVerified |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static int UpdateProgressNotes(ProgressNotesModel p_ProgressNotes)
        {
            string exception = string.Empty;
            int progressNotesId = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_UPDATE_PROGRESS_NOTES, l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@id", p_ProgressNotes.ID);
                l_Cmd.Parameters.AddWithValue("@residentId", p_ProgressNotes.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@date", p_ProgressNotes.Date);
                l_Cmd.Parameters.AddWithValue("@title", p_ProgressNotes.Title);
                l_Cmd.Parameters.AddWithValue("@note", p_ProgressNotes.Note);
                l_Cmd.Parameters.AddWithValue("@createdby", p_ProgressNotes.ModifiedBy);
                l_Cmd.Parameters.AddWithValue("@category", p_ProgressNotes.Category);
                l_Cmd.Parameters.AddWithValue("@reminIn", p_ProgressNotes.RemainIn);
                progressNotesId = l_Cmd.ExecuteNonQuery();
                if (progressNotesId != 0)
                {
                    progressNotesId = 1;
                }
                return progressNotesId;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes UpdateProgressNotes |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ProgressNotesModel> GetProgressNotesCollections(int residentId, DateTime fromDate, DateTime toDate, string stype, int iUserType = 0, int iCategory = 0)
        {
            string exception = string.Empty;
            Collection<ProgressNotesModel> p_ProgressNotes = new Collection<ProgressNotesModel>();
            ProgressNotesModel l_ProgressNote;
            ResidentModel l_Resident;
            UserModel l_Users;
            UserModel l_AckUser;
            int l_ResidentId = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlDataAdapter l_DA = new SqlDataAdapter();
                //SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_PROGRESS_NOTES, l_Conn);
                SqlCommand l_Cmd = new SqlCommand("Get_Progress_Notes_by_mike", l_Conn);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentId", residentId);
                l_Cmd.Parameters.AddWithValue("@fromDate", fromDate);
                l_Cmd.Parameters.AddWithValue("@toDate", toDate);
                l_Cmd.Parameters.AddWithValue("@type", stype);
                l_Cmd.Parameters.AddWithValue("@userType", iUserType);
                l_Cmd.Parameters.AddWithValue("@category", iCategory);
                l_Cmd.CommandTimeout = 900;

                DataSet progressNotesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(progressNotesReceive);

                if (progressNotesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= progressNotesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_ProgressNote = new ProgressNotesModel();
                        l_Resident = new ResidentModel();
                        l_Users = new UserModel();
                        l_AckUser = new UserModel();
                        l_ProgressNote.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_id"]);
                        l_ResidentId = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_resident_id"]);
                        l_ProgressNote.Resident = l_Resident;
                        l_ProgressNote.Date = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_date"]);
                        l_ProgressNote.Category = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_category"]);
                        l_ProgressNote.Title = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_title"]);
                        l_ProgressNote.Note = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_note"]);
                        l_Users.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_first_name"]);
                        l_Users.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_last_name"]);
                        l_Users.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_user_id"]);
                        l_Users.UserTypeName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_name"]);
                        l_ProgressNote.RemainIn = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_remain_in"]);
                        l_ProgressNote.ModifiedBy = l_Users;

                        l_AckUser.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_ack_fname"]);
                        l_AckUser.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_ack_Lname"]);
                        l_AckUser.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_acknowledged_by"]);
                        l_AckUser.UserTypeName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_ack_utName"]);
                        l_AckUser.ModifiedOn = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_acknowledged_on"]);

                        l_ProgressNote.FallDateType = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_fall_date_type"]);
                        l_ProgressNote.FallDate = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_fall_date"]);
                        l_ProgressNote.Location = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_location"]);
                        l_ProgressNote.WitnessType = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_witness_type"]);
                        l_ProgressNote.WitnessFall = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_witness_fall"]);
                        l_ProgressNote.UnWitnessType = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_Un_witnes_type"]);
                        l_ProgressNote.IncidentReport = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["fd_incident_report"]);
                        l_ProgressNote.AcknowledgeNote = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_action_note"]);
                        l_ProgressNote.ACkFlag = Convert.ToChar(progressNotesReceive.Tables[0].Rows[index]["As_flag"]);
                        l_ProgressNote.AcknowledgedBy = l_AckUser;

                        //////////change by mike
                        switch (l_ProgressNote.Category)
                        {
                            case 1: l_ProgressNote.category_full = "1"; break;
                            case 2: l_ProgressNote.category_full = "Medical Update"; break;
                            case 3: l_ProgressNote.category_full = "Social Update"; break;
                            case 4: l_ProgressNote.category_full = "Dietary Update"; break;
                            case 5: l_ProgressNote.category_full = "General Update"; break;
                            case 6: l_ProgressNote.category_full = "Resident Fall"; break;
                            case 7: l_ProgressNote.category_full = "Resident Bruised"; break;
                            default: l_ProgressNote.category_full = ""; break;
                        }
                        //////////end


                        p_ProgressNotes.Add(l_ProgressNote);
                    }
                }
                return p_ProgressNotes;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetProgressNotesCollections |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static ProgressNotesModel GetProgressNotesById(int iProgressNotesId)
        {
            string exception = string.Empty;
            ProgressNotesModel l_ProgressNote = new ProgressNotesModel();
            ResidentModel l_Resident;
            UserModel l_Users;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_PROGRESS_NOTES_BY_ID, l_Conn);
                l_Cmd.Parameters.AddWithValue("@id", iProgressNotesId);
                DataSet progressNotesReceive = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(progressNotesReceive);

                if (progressNotesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= progressNotesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Resident = new ResidentModel();
                        l_Users = new UserModel();
                        l_ProgressNote.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_id"]);
                        l_Resident.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_resident_id"]);
                        l_ProgressNote.Resident = l_Resident;
                        l_ProgressNote.Date = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_date"]);
                        l_ProgressNote.Title = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_title"]);
                        l_ProgressNote.Note = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_note"]);
                        l_Users.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_modified_by"]);
                        l_ProgressNote.Category = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_category"]);
                        l_ProgressNote.RemainIn = Convert.ToInt16(progressNotesReceive.Tables[0].Rows[index]["fd_remain_in"]);
                        l_ProgressNote.ModifiedBy = l_Users;
                    }
                }
                return l_ProgressNote;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetProgressNotesById |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static bool RemoveProgressNotes(int ProgressNoteId)
        {
            string exception = string.Empty;
            bool result = false;
            int affected = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_REMOVE_PROGRESS_NOTES, l_Conn);
                l_Cmd.Parameters.AddWithValue("@id", ProgressNoteId);
                affected = l_Cmd.ExecuteNonQuery();
                if (affected == 1)
                {
                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes RemoveProgressNotes |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static DataTable GetPersonalCalendarByResidentId(DateTime eventFromDate, DateTime eventToDate, int residentId, char cHomeFlag)
        {
            string exception = string.Empty;
            DataSet dtPersonalCalendar = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_PERSONAL_CALENDAR_BY_RESIDENT_ID, l_Conn);
                l_Cmd.Parameters.AddWithValue("@residentId", residentId);
                l_Cmd.Parameters.AddWithValue("@fromDate", eventFromDate);
                l_Cmd.Parameters.AddWithValue("@toDate", eventToDate);
                l_Cmd.Parameters.AddWithValue("@homeFlag", cHomeFlag);
                l_Cmd.CommandTimeout = 1000;
                dtPersonalCalendar = new DataSet();
                
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dtPersonalCalendar);
                return dtPersonalCalendar.Tables[0];
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetPersonalCalendarByResidentId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        #endregion

        public static Collection<ProgressNotesModel> GetProgressNotesVerified(int iHomeId, int iUserId)
        {
            string exception = string.Empty;
            Collection<ProgressNotesModel> p_ProgressNotes = new Collection<ProgressNotesModel>();
            ProgressNotesModel l_ProgressNote;
            UserModel l_Users;
            ResidentModel l_Resident;
            SuiteModel l_Suite;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_PROGRESS_NOTES_ACKNOWLEDGEMENT, l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeId", iHomeId);
                l_Cmd.Parameters.AddWithValue("@userId", iUserId);

                DataSet progressNotesReceive = new DataSet();
                
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(progressNotesReceive);

                if (progressNotesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= progressNotesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_ProgressNote = new ProgressNotesModel();
                        l_Users = new UserModel();
                        l_Resident = new ResidentModel();
                        l_Suite = new SuiteModel();
                        l_ProgressNote.ID = Convert.ToInt32(progressNotesReceive.Tables[0].Rows[index]["fd_id"]);
                        l_ProgressNote.Date = Convert.ToDateTime(progressNotesReceive.Tables[0].Rows[index]["fd_date"]);
                        l_ProgressNote.Title = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_title"]);
                        l_ProgressNote.Note = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_note"]);
                        l_Users.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_first_name"]);
                        l_Users.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_last_name"]);
                        l_Suite.SuiteNo = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_suite_no"]);
                        l_Resident.FirstName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_resident_first_name"]);
                        l_Resident.LastName = Convert.ToString(progressNotesReceive.Tables[0].Rows[index]["fd_resident_last_name"]);
                        l_ProgressNote.Resident = l_Resident;
                        l_ProgressNote.Suite = l_Suite;
                        l_ProgressNote.ModifiedBy = l_Users;
                        p_ProgressNotes.Add(l_ProgressNote);
                    }
                }
                return p_ProgressNotes;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetProgressNotesVerified |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static int AddProgressNoteAcknowledge(ProgressNotesModel p_ProgressNotes)
        {
            string exception = string.Empty;
            int progressNotesId = 0;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_PROGRESS_NOTES_ACKNOWLEDGED, l_Conn);
                l_Cmd.Parameters.AddWithValue("@id", p_ProgressNotes.ID);
                l_Cmd.Parameters.AddWithValue("@note", p_ProgressNotes.AcknowledgeNote);
                l_Cmd.Parameters.AddWithValue("@createdby", p_ProgressNotes.ModifiedBy);
                progressNotesId = l_Cmd.ExecuteNonQuery();
                if (progressNotesId != 0)
                {
                    progressNotesId = 1;
                }
                return progressNotesId;
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes AddProgressNoteAcknowledge |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        #region "Resident Calender"
        public static DataTable GetResidentBirthDayCalenderByHomeId(DateTime eventFromDate, DateTime eventToDate, int homeId)
        {
            string exception = string.Empty;
            DataSet dtPersonalCalendar = null;
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                l_Conn.Open();
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_RESIDENT_BIRTH_DAY_CALENDAR_BY_HOME_ID, l_Conn);
                l_Cmd.Parameters.AddWithValue("@homeId", homeId);
                l_Cmd.Parameters.AddWithValue("@fromDate", eventFromDate);
                l_Cmd.Parameters.AddWithValue("@toDate", eventToDate);
                dtPersonalCalendar = new DataSet();
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dtPersonalCalendar);
                return dtPersonalCalendar.Tables[0];
            }
            catch (Exception ex)
            {
                exception = "ProgressNotes GetResideCalenderByHomeId |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        #endregion "Resident Calender"
    }
    #endregion
}