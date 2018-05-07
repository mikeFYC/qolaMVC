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
    public class AssessmentDAL
    {
        public static void AddNewBowelMovement(BowelMovementModel p_BowelMovement)
        {
            string exception = string.Empty;
            
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_BOWEL_MOVEMENT_ASSESSMENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_BowelMovement.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@Type", p_BowelMovement.Type);
                l_Cmd.Parameters.AddWithValue("@ObservedBy", p_BowelMovement.ObservedBy);
                l_Cmd.Parameters.AddWithValue("@Initials", p_BowelMovement.Initials);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_BowelMovement.EnteredBy.ID);
                l_Cmd.Parameters.AddWithValue("@Period", p_BowelMovement.Period);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddNewResidentGeneralInfo |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<BowelMovementModel> GetResidentsBowelAssessments(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<BowelMovementModel> l_BowelMovements = new Collection<BowelMovementModel>(); 
            
            ResidentModel l_Resident = new ResidentModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_BOWEL_MOVEMENT_ASSESSMENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        BowelMovementModel l_BowelMovement = new BowelMovementModel();
                        l_BowelMovement.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        //l_BowelMovement.EnteredBy = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_BowelMovement.Initials = Convert.ToString(dataReceive.Tables[0].Rows[index]["Initials"]);
                        l_BowelMovement.Period = Convert.ToString(dataReceive.Tables[0].Rows[index]["strPeriod"]);
                        l_BowelMovement.ObservedBy = Convert.ToString(dataReceive.Tables[0].Rows[index]["ObservedBy"]);
                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentSuiteNo"]);
                        l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
                        l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
                        l_BowelMovement.Resident = l_Resident;
                        l_BowelMovement.Type = Convert.ToString(dataReceive.Tables[0].Rows[index]["strType"]);
                        l_BowelMovement.TimeStamp = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["dtmTimeStamp"]);
                        l_BowelMovements.Add(l_BowelMovement);
                    }
                }
                return l_BowelMovements;
            }
            catch (Exception ex)
            {
                exception = "AssessmentDAL GetResidentsBowelAssessments |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void AddFamilyConferenceNote(FamilyConfrenceNoteModel p_FamilyConferenceNote)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_FAMILY_CONFERENCE_NOTES, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_FamilyConferenceNote.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@dtmDate", DateTime.Now);
                l_Cmd.Parameters.AddWithValue("@SuiteNumber", p_FamilyConferenceNote.SuiteNumber);
                l_Cmd.Parameters.AddWithValue("@PHN", p_FamilyConferenceNote.PHN);
                l_Cmd.Parameters.AddWithValue("@CareAndGCD", p_FamilyConferenceNote.CareandGCD);
                l_Cmd.Parameters.AddWithValue("@Medication", p_FamilyConferenceNote.Medication);
                l_Cmd.Parameters.AddWithValue("@Recreation", p_FamilyConferenceNote.Recreation);
                l_Cmd.Parameters.AddWithValue("@Diet", p_FamilyConferenceNote.Diet);
                l_Cmd.Parameters.AddWithValue("@Comments", p_FamilyConferenceNote.Comments);
                l_Cmd.Parameters.AddWithValue("@Goals", p_FamilyConferenceNote.Goals);
                l_Cmd.Parameters.AddWithValue("@Present1", p_FamilyConferenceNote.Presents1);
                l_Cmd.Parameters.AddWithValue("@Present2", p_FamilyConferenceNote.Presents2);
                l_Cmd.Parameters.AddWithValue("@Present3", p_FamilyConferenceNote.Presents3);
                l_Cmd.Parameters.AddWithValue("@DateEntered", DateTime.Now);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_FamilyConferenceNote.EnteredBy.ID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddFamilyConferenceNote |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<FamilyConfrenceNoteModel> GetFamilyConferenceNotes(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<FamilyConfrenceNoteModel> l_FamilyConferenceNotes = new Collection<FamilyConfrenceNoteModel>();

            ResidentModel l_Resident = new ResidentModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_FAMILY_CONFERENCE_NOTES, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        FamilyConfrenceNoteModel l_FamilyConferenceNote = new FamilyConfrenceNoteModel();
                        l_FamilyConferenceNote.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        //l_FamilyConferenceNote.EnteredBy = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_FamilyConferenceNote.CareandGCD = Convert.ToString(dataReceive.Tables[0].Rows[index]["CareAndGCD"]);
                        l_FamilyConferenceNote.Comments = Convert.ToString(dataReceive.Tables[0].Rows[index]["Comments"]);
                        l_FamilyConferenceNote.Date = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["dtmDate"]);
                        l_FamilyConferenceNote.Diet = Convert.ToString(dataReceive.Tables[0].Rows[index]["Diet"]);
                        //l_FamilyConferenceNote.DOB = Convert.ToString(dataReceive.Tables[0].Rows[index]["Diet"]);
                        l_FamilyConferenceNote.Goals = Convert.ToString(dataReceive.Tables[0].Rows[index]["Goals"]);
                        l_FamilyConferenceNote.Medication = Convert.ToString(dataReceive.Tables[0].Rows[index]["Medication"]);
                        l_FamilyConferenceNote.PHN = Convert.ToString(dataReceive.Tables[0].Rows[index]["PHN"]);
                        l_FamilyConferenceNote.Presents1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Present1"]);
                        l_FamilyConferenceNote.Presents2 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Present2"]);
                        l_FamilyConferenceNote.Presents3 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Present3"]);
                        l_FamilyConferenceNote.Recreation = Convert.ToString(dataReceive.Tables[0].Rows[index]["Recreation"]);
                        l_FamilyConferenceNote.SuiteNumber = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);

                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);
                        l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
                        l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
                        l_FamilyConferenceNote.Resident = l_Resident;
                        l_FamilyConferenceNotes.Add(l_FamilyConferenceNote);
                    }
                }
                return l_FamilyConferenceNotes;
            }
            catch (Exception ex)
            {
                exception = "AssessmentDAL GetFamilyConferenceNotes |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        //public static void AddAdmissionHeadToToe(AdmissionHeadToToeModel p_Model)
        //{
        //    string exception = string.Empty;

        //    SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
        //    try
        //    {
        //        SqlDataAdapter l_DA = new SqlDataAdapter();
        //        SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_FAMILY_CONFERENCE_NOTES, l_Conn);
        //        l_Conn.Open();
        //        l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
        //        l_Cmd.Parameters.AddWithValue("@dtmDate", DateTime.Now);
        //        l_Cmd.Parameters.AddWithValue("@SuiteNumber", p_Model.SuiteNumber);
        //        l_Cmd.Parameters.AddWithValue("@PHN", p_Model.PHN);
        //        l_Cmd.Parameters.AddWithValue("@CareAndGCD", p_Model.CareandGCD);
        //        l_Cmd.Parameters.AddWithValue("@Medication", p_Model.Medication);
        //        l_Cmd.Parameters.AddWithValue("@Recreation", p_Model.Recreation);
        //        l_Cmd.Parameters.AddWithValue("@Diet", p_Model.Diet);
        //        l_Cmd.Parameters.AddWithValue("@Comments", p_Model.Comments);
        //        l_Cmd.Parameters.AddWithValue("@Goals", p_Model.Goals);
        //        l_Cmd.Parameters.AddWithValue("@Present1", p_Model.Presents1);
        //        l_Cmd.Parameters.AddWithValue("@Present2", p_Model.Presents2);
        //        l_Cmd.Parameters.AddWithValue("@Present3", p_Model.Presents3);
        //        l_Cmd.Parameters.AddWithValue("@DateEntered", DateTime.Now);
        //        l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
        //        l_Cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = "AddFamilyConferenceNote |" + ex.ToString();
        //        //Log.Write(exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        l_Conn.Close();
        //    }
        //}

        //public static Collection<FamilyConfrenceNoteModel> GetFamilyConferenceNotes(int p_ResidentId)
        //{
        //    string exception = string.Empty;
        //    Collection<FamilyConfrenceNoteModel> l_FamilyConferenceNotes = new Collection<FamilyConfrenceNoteModel>();

        //    ResidentModel l_Resident = new ResidentModel();
        //    SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
        //    try
        //    {
        //        SqlDataAdapter l_DA = new SqlDataAdapter();
        //        SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_FAMILY_CONFERENCE_NOTES, l_Conn);
        //        l_Conn.Open();
        //        l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
        //        DataSet dataReceive = new DataSet();

        //        l_DA.SelectCommand = l_Cmd;
        //        l_DA.Fill(dataReceive);

        //        if ((dataReceive != null) & dataReceive.Tables.Count > 0)
        //        {
        //            for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
        //            {
        //                FamilyConfrenceNoteModel l_FamilyConferenceNote = new FamilyConfrenceNoteModel();
        //                l_FamilyConferenceNote.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
        //                //l_FamilyConferenceNote.EnteredBy = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["EnteredBy"]);
        //                l_FamilyConferenceNote.CareandGCD = Convert.ToString(dataReceive.Tables[0].Rows[index]["CareAndGCD"]);
        //                l_FamilyConferenceNote.Comments = Convert.ToString(dataReceive.Tables[0].Rows[index]["Comments"]);
        //                l_FamilyConferenceNote.Date = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["dtmDate"]);
        //                l_FamilyConferenceNote.Diet = Convert.ToString(dataReceive.Tables[0].Rows[index]["Diet"]);
        //                //l_FamilyConferenceNote.DOB = Convert.ToString(dataReceive.Tables[0].Rows[index]["Diet"]);
        //                l_FamilyConferenceNote.Goals = Convert.ToString(dataReceive.Tables[0].Rows[index]["Goals"]);
        //                l_FamilyConferenceNote.Medication = Convert.ToString(dataReceive.Tables[0].Rows[index]["Medication"]);
        //                l_FamilyConferenceNote.PHN = Convert.ToString(dataReceive.Tables[0].Rows[index]["PHN"]);
        //                l_FamilyConferenceNote.Presents1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Present1"]);
        //                l_FamilyConferenceNote.Presents2 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Present2"]);
        //                l_FamilyConferenceNote.Presents3 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Present3"]);
        //                l_FamilyConferenceNote.Recreation = Convert.ToString(dataReceive.Tables[0].Rows[index]["Recreation"]);
        //                l_FamilyConferenceNote.SuiteNumber = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);

        //                l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
        //                l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);
        //                l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
        //                l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
        //                l_FamilyConferenceNote.Resident = l_Resident;
        //                l_FamilyConferenceNotes.Add(l_FamilyConferenceNote);
        //            }
        //        }
        //        return l_FamilyConferenceNotes;
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = "AssessmentDAL GetFamilyConferenceNotes |" + ex.ToString();
        //        throw;
        //    }
        //    finally
        //    {
        //        l_Conn.Close();
        //    }
        //}
    }
}