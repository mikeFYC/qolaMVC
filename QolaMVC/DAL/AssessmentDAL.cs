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

        public static void AddAdmissionHeadToToe(AdmissionHeadToToeModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_ADMISSION_HEADTOTOE_ASSESSMENT, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@dtmDate", DateTime.Now);
                l_Cmd.Parameters.AddWithValue("@AdmissionStatus", p_Model.AdmissionStatus);
                l_Cmd.Parameters.AddWithValue("@ReturnedFromHospital", p_Model.ReturnedFromHospital);
                l_Cmd.Parameters.AddWithValue("@DiagnosisFromHospital", p_Model.DiagnosisFromHospital);
                l_Cmd.Parameters.AddWithValue("@Medications", p_Model.Medications);
                l_Cmd.Parameters.AddWithValue("@BP", p_Model.BP);
                l_Cmd.Parameters.AddWithValue("@BPLocation", p_Model.BPLocation);
                l_Cmd.Parameters.AddWithValue("@RedialPulse", p_Model.RedialPulse);
                l_Cmd.Parameters.AddWithValue("@PulseLocation", p_Model.PulseLocation);
                l_Cmd.Parameters.AddWithValue("@Temp", p_Model.Temp);
                l_Cmd.Parameters.AddWithValue("@TempLocation", p_Model.TempLocation);
                l_Cmd.Parameters.AddWithValue("@Resp", p_Model.Resp);

                l_Cmd.Parameters.AddWithValue("@RespLocation", p_Model.RespLocation);
                l_Cmd.Parameters.AddWithValue("@SP02", p_Model.SP02);
                l_Cmd.Parameters.AddWithValue("@SP02Location", p_Model.SP02Location);
                l_Cmd.Parameters.AddWithValue("@Place", p_Model.Place);
                l_Cmd.Parameters.AddWithValue("@strTime", p_Model.Time);
                l_Cmd.Parameters.AddWithValue("@Speech", p_Model.Speech);
                l_Cmd.Parameters.AddWithValue("@PrimaryLanguage", p_Model.PrimaryLanguage);

                l_Cmd.Parameters.AddWithValue("@PulpilsEquals", p_Model.PulpilsEquals);
                l_Cmd.Parameters.AddWithValue("@PulpilsReactive", p_Model.PulpilsReactive);
                l_Cmd.Parameters.AddWithValue("@Eyes", p_Model.Eyes);
                l_Cmd.Parameters.AddWithValue("@PulpilsEquals", p_Model.PulpilsEquals);
                l_Cmd.Parameters.AddWithValue("@GeneralFace", p_Model.GeneralFace);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
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

        public static Collection<AdmissionHeadToToeModel> GetAdmissionHeadToToe(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<AdmissionHeadToToeModel> l_AdmissionHeadToToes = new Collection<AdmissionHeadToToeModel>();

            ResidentModel l_Resident = new ResidentModel();
            UserModel l_User = new UserModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_ADMISSION_HEADTOTOE_ASSESSMENT, l_Conn);
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
                        AdmissionHeadToToeModel l_AdmissionHeadToToe = new AdmissionHeadToToeModel();
                        l_AdmissionHeadToToe.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_AdmissionHeadToToe.Date = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["dtmDate"]);
                        l_AdmissionHeadToToe.AdmissionStatus = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["AdmissionStatus"]);
                        //l_FamilyConferenceNote.DOB = Convert.ToString(dataReceive.Tables[0].Rows[index]["Diet"]);
                        l_AdmissionHeadToToe.ReturnedFromHospital = Convert.ToString(dataReceive.Tables[0].Rows[index]["ReturnedFromHospital"]);
                        l_AdmissionHeadToToe.DiagnosisFromHospital = Convert.ToString(dataReceive.Tables[0].Rows[index]["DiagnosisFromHospital"]);
                        l_AdmissionHeadToToe.Medications = Convert.ToString(dataReceive.Tables[0].Rows[index]["Medications"]);
                        l_AdmissionHeadToToe.BP = Convert.ToString(dataReceive.Tables[0].Rows[index]["BP"]);
                        l_AdmissionHeadToToe.BPLocation = Convert.ToString(dataReceive.Tables[0].Rows[index]["BPLocation"]);
                        l_AdmissionHeadToToe.RedialPulse = Convert.ToString(dataReceive.Tables[0].Rows[index]["RedialPulse"]);
                        l_AdmissionHeadToToe.PulseLocation = Convert.ToString(dataReceive.Tables[0].Rows[index]["Recreation"]);
                        l_AdmissionHeadToToe.Temp = Convert.ToString(dataReceive.Tables[0].Rows[index]["Temp"]);
                        l_AdmissionHeadToToe.TempLocation = Convert.ToString(dataReceive.Tables[0].Rows[index]["TempLocation"]);
                        l_AdmissionHeadToToe.Resp = Convert.ToString(dataReceive.Tables[0].Rows[index]["Resp"]);
                        l_AdmissionHeadToToe.RespLocation = Convert.ToString(dataReceive.Tables[0].Rows[index]["RespLocation"]);
                        l_AdmissionHeadToToe.SP02 = Convert.ToString(dataReceive.Tables[0].Rows[index]["SP02"]);
                        l_AdmissionHeadToToe.SP02Location = Convert.ToString(dataReceive.Tables[0].Rows[index]["SP02Location"]);
                        l_AdmissionHeadToToe.Person = Convert.ToString(dataReceive.Tables[0].Rows[index]["Person"]);
                        l_AdmissionHeadToToe.Place = Convert.ToString(dataReceive.Tables[0].Rows[index]["Place"]);
                        l_AdmissionHeadToToe.Time = Convert.ToString(dataReceive.Tables[0].Rows[index]["strTime"]);
                        l_AdmissionHeadToToe.Speech = Convert.ToString(dataReceive.Tables[0].Rows[index]["Speech"]);
                        l_AdmissionHeadToToe.PrimaryLanguage = Convert.ToString(dataReceive.Tables[0].Rows[index]["PrimaryLanguage"]);
                        l_AdmissionHeadToToe.PulpilsEquals = Convert.ToString(dataReceive.Tables[0].Rows[index]["PulpilsEquals"]);
                        l_AdmissionHeadToToe.PulpilsReactive = Convert.ToString(dataReceive.Tables[0].Rows[index]["PulpilsReactive"]);
                        l_AdmissionHeadToToe.Eyes = Convert.ToString(dataReceive.Tables[0].Rows[index]["Eyes"]);
                        l_AdmissionHeadToToe.GeneralFace = Convert.ToString(dataReceive.Tables[0].Rows[index]["GeneralFace"]);
                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);
                        l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
                        l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
                        l_AdmissionHeadToToe.Resident = l_Resident;
                        l_AdmissionHeadToToe.DateEntered = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_User.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_User.Name = Convert.ToString(dataReceive.Tables[0].Rows[index]["EnteredByName"]);
                        l_AdmissionHeadToToe.EnteredBy = l_User;
                        l_AdmissionHeadToToes.Add(l_AdmissionHeadToToe);
                    }
                }
                return l_AdmissionHeadToToes;
            }
            catch (Exception ex)
            {
                exception = "AssessmentDAL GetAdmissionHeadToToes |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void AddExcerciseActivitySummary(ExcerciseActivitySummaryModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_EXCERCISE_ACTIVITY_SUMMARY, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@BaselineDate", p_Model.BaselineDate);
                l_Cmd.Parameters.AddWithValue("@BaselineTug", p_Model.BaselineTug);
                l_Cmd.Parameters.AddWithValue("@BaselineVPS", p_Model.BaselineVPS);
                l_Cmd.Parameters.AddWithValue("@TMonthDate", p_Model.TMonthDate);
                l_Cmd.Parameters.AddWithValue("@TMonthTug", p_Model.TMonthTug);
                l_Cmd.Parameters.AddWithValue("@TMonthVPS", p_Model.TMonthVPS);
                l_Cmd.Parameters.AddWithValue("@SMonthDate", p_Model.SMonthDate);
                l_Cmd.Parameters.AddWithValue("@SMonthTug", p_Model.SMonthTug);
                l_Cmd.Parameters.AddWithValue("@SMonthVPS", p_Model.SMonthVPS);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddExcerciseActivitySummary |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static ExcerciseActivitySummaryModel GetExcerciseActivitySummary(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<ExcerciseActivitySummaryModel> l_Models = new Collection<ExcerciseActivitySummaryModel>();

            ResidentModel l_Resident = new ResidentModel();
            UserModel l_User = new UserModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXCERCISE_ACTIVITY_SUMMARY, l_Conn);
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
                        ExcerciseActivitySummaryModel l_Model = new ExcerciseActivitySummaryModel();
                        l_Model.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Model.BaselineDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["BaselineDate"]);
                        l_Model.BaselineTug = Convert.ToString(dataReceive.Tables[0].Rows[index]["BaselineTug"]);
                        l_Model.BaselineVPS = Convert.ToString(dataReceive.Tables[0].Rows[index]["BaselineVPS"]);
                        l_Model.TMonthDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["TMonthDate"]);
                        l_Model.TMonthTug = Convert.ToString(dataReceive.Tables[0].Rows[index]["TMonthTug"]);
                        l_Model.TMonthVPS = Convert.ToString(dataReceive.Tables[0].Rows[index]["TMonthVPS"]);
                        l_Model.SMonthDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["SMonthDate"]);
                        l_Model.SMonthTug = Convert.ToString(dataReceive.Tables[0].Rows[index]["SMonthTug"]);
                        l_Model.SMonthVPS = Convert.ToString(dataReceive.Tables[0].Rows[index]["SMonthVPS"]);
                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);
                        l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
                        l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
                        l_Model.Resident = l_Resident;
                        l_Model.DateEntered = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_User.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_User.Name = Convert.ToString(dataReceive.Tables[0].Rows[index]["EnteredByName"]);
                        l_Model.EnteredBy = l_User;
                        l_Models.Add(l_Model);
                    }
                }
                return l_Models.LastOrDefault();
            }
            catch (Exception ex)
            {
                exception = "AssessmentDAL GetExcerciseActivitySummary |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void AddExcerciseActivityDetail (ExcerciseActivityDetailModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_EXCERCISE_ACTIVITY_DETAIL, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@ActivityName", p_Model.ActivityName);
                l_Cmd.Parameters.AddWithValue("@WeekId", p_Model.Week);
                l_Cmd.Parameters.AddWithValue("@Sunday", p_Model.Sunday);
                l_Cmd.Parameters.AddWithValue("@Monday", p_Model.Monday);
                l_Cmd.Parameters.AddWithValue("@Tuesday", p_Model.Tuesday);
                l_Cmd.Parameters.AddWithValue("@Wednesday", p_Model.Wednesday);
                l_Cmd.Parameters.AddWithValue("@Thursday", p_Model.Thursday);
                l_Cmd.Parameters.AddWithValue("@Friday", p_Model.Friday);
                l_Cmd.Parameters.AddWithValue("@Saturday", p_Model.Saturday);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddExcerciseActivityDetail |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ExcerciseActivityDetailModel> GetExcerciseActivityDetail(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<ExcerciseActivityDetailModel> l_Models = new Collection<ExcerciseActivityDetailModel>();

            ResidentModel l_Resident = new ResidentModel();
            UserModel l_User = new UserModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_EXCERCISE_ACTIVITY_DETAIL, l_Conn);
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
                        ExcerciseActivityDetailModel l_Model = new ExcerciseActivityDetailModel();
                        l_Model.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Model.ActivityName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ActivityName"]);
                        l_Model.Week = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["WeekId"]);
                        l_Model.Sunday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["Sunday"]);
                        l_Model.Monday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["Monday"]);
                        l_Model.Tuesday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["Tuesday"]);
                        l_Model.Wednesday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["Wednesday"]);
                        l_Model.Thursday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["Thursday"]);
                        l_Model.Friday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["SMonthTug"]);
                        l_Model.Saturday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["SMonthVPS"]);
                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);
                        l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
                        l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
                        l_Model.Resident = l_Resident;
                        l_Model.DateEntered = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_User.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_User.Name = Convert.ToString(dataReceive.Tables[0].Rows[index]["EnteredByName"]);
                        l_Model.EnteredBy = l_User;
                        l_Models.Add(l_Model);
                    }
                }
                return l_Models;
            }
            catch (Exception ex)
            {
                exception = "AssessmentDAL GetExcerciseActivitySummary |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void AddHSEPDetail(HSEPDetailModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_ADD_HSEP_DETAIL, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@ActivityName", p_Model.ActivityName);
                l_Cmd.Parameters.AddWithValue("@DateOfTeaching", p_Model.DateOfTeaching);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddHSEPDetail |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static HSEPDetailModel GetHSEPDetail(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<HSEPDetailModel> l_Models = new Collection<HSEPDetailModel>();

            ResidentModel l_Resident = new ResidentModel();
            UserModel l_User = new UserModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_HSEP_DETAIL, l_Conn);
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
                        HSEPDetailModel l_Model = new HSEPDetailModel();
                        l_Model.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Model.ActivityName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ActivityName"]);
                        l_Model.DateOfTeaching = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["DateOfTeaching"]);
                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["SuiteNumber"]);
                        l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
                        l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
                        l_Model.Resident = l_Resident;
                        //l_Model.DateEntered = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_User.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_User.Name = Convert.ToString(dataReceive.Tables[0].Rows[index]["EnteredByName"]);
                        l_Model.EnteredBy = l_User;
                        l_Models.Add(l_Model);
                    }
                }
                return l_Models.LastOrDefault();
            }
            catch (Exception ex)
            {
                exception = "AssessmentDAL GetHSEPDetail |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
    }
}