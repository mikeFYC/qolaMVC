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
    public class CarePlanDAL
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

        public static Collection<PlanOfCareModel> GetResidentsPlanOfCare(int p_ResidentId)
        {
            string exception = string.Empty;
            Collection<PlanOfCareModel> l_Assessments = new Collection<PlanOfCareModel>();

            ResidentModel l_Resident = new ResidentModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare", l_Conn);
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
                        PlanOfCareModel l_Assessment = new PlanOfCareModel();
                        l_Assessment.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.Assessed = Convert.ToString(dataReceive.Tables[0].Rows[index]["Assessed"]);
                        l_Assessment.LevelOfCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["LevelOfCare"]);
                        l_Assessment.CompleteStatus = Convert.ToString(dataReceive.Tables[0].Rows[index]["CompleteStatus"]);
                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.Resident = l_Resident;
                        l_Assessment.DateEntered = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["DateEntered"]);

                        //Make call to other methods to get other properties of the planofcare model
                        l_Assessment.VitalSigns = GetCarePlanVitalSigns(l_Assessment.Id);
                        l_Assessment.PersonalHygiene = GetCarePlanPersonalHygiene(l_Assessment.Id);
                        l_Assessment.AssistanceWith = GetCarePlanAssistanceWith(l_Assessment.Id);
                        l_Assessment.Mobility = GetCarePlanMobility(l_Assessment.Id);
                        l_Assessment.Safety = GetCarePlanSafety(l_Assessment.Id);

                        //hold on other methods. Go and work on adding a care plan
                        l_Assessments.Add(l_Assessment);
                    }
                }
                return l_Assessments;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetResidentsPlanOfCare |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        private static CarePlanVitalSignsModel GetCarePlanVitalSigns(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanVitalSignsModel l_Assessment = new CarePlanVitalSignsModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_VitalSigns", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CarePlanId", p_CarePlanId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.ResidentId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.CarePlanId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["CarePlanId"]);
                        l_Assessment.BPSystolic = Convert.ToString(dataReceive.Tables[0].Rows[index]["BP_Systolic"]);
                        l_Assessment.BPDiastolic = Convert.ToString(dataReceive.Tables[0].Rows[index]["BP_Diastolic"]);
                        l_Assessment.BPDateCompleted = Convert.ToString(dataReceive.Tables[0].Rows[index]["BP_DateCompleted"]);
                        l_Assessment.Temperature = Convert.ToString(dataReceive.Tables[0].Rows[index]["Temperature"]);
                        l_Assessment.TempDateCompleted = Convert.ToString(dataReceive.Tables[0].Rows[index]["Temp_DateCompleted"]);
                        l_Assessment.Weight = Convert.ToString(dataReceive.Tables[0].Rows[index]["WeightLBS"]);
                        l_Assessment.WeightDateCompleted = Convert.ToString(dataReceive.Tables[0].Rows[index]["Weight_DateCompleted"]);
                        l_Assessment.HeightFeet = Convert.ToString(dataReceive.Tables[0].Rows[index]["Height_Feet"]);
                        l_Assessment.HeightInches = Convert.ToString(dataReceive.Tables[0].Rows[index]["Height_Inches"]);
                        l_Assessment.HeightDateCompleted = Convert.ToString(dataReceive.Tables[0].Rows[index]["Height_DateCompleted"]);
                        l_Assessment.Pulse = Convert.ToString(dataReceive.Tables[0].Rows[index]["Pulse"]);
                        l_Assessment.PulseDateCompleted = Convert.ToString(dataReceive.Tables[0].Rows[index]["Pulse_DateCompleted"]);
                        l_Assessment.PulseRegular = Convert.ToString(dataReceive.Tables[0].Rows[index]["PulseRegular"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanVitalSigns |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanPersonalHygieneModel GetCarePlanPersonalHygiene(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanPersonalHygieneModel l_Assessment = new CarePlanPersonalHygieneModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_PersonalHygiene", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CarePlanId", p_CarePlanId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.ResidentId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.CarePlanId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["CarePlanId"]);
                        l_Assessment.AMCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["AMCare"]);
                        l_Assessment.PMCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["PMCare"]);
                        l_Assessment.Bathing = Convert.ToString(dataReceive.Tables[0].Rows[index]["Bathing"]);
                        l_Assessment.AMAssistedBy = Convert.ToString(dataReceive.Tables[0].Rows[index]["AM_AssistedBy"]);
                        l_Assessment.PMAssistedBy = Convert.ToString(dataReceive.Tables[0].Rows[index]["PM_AssistedBy"]);
                        l_Assessment.BathingAssistedBy = Convert.ToString(dataReceive.Tables[0].Rows[index]["Bathing_AssistedBy"]);
                        l_Assessment.AMAgencyName = Convert.ToString(dataReceive.Tables[0].Rows[index]["AM_AgencyName"]);
                        l_Assessment.PMAgencyName = Convert.ToString(dataReceive.Tables[0].Rows[index]["PM_AgencyName"]);
                        l_Assessment.BathingAgencyName = Convert.ToString(dataReceive.Tables[0].Rows[index]["Bathing_AgencyName"]);
                        l_Assessment.AMPreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["AM_PreferredTime"]);
                        l_Assessment.PMPreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["PM_PreferredTime"]);
                        l_Assessment.BathingPreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["Bathing_PreferredTime"]);
                        l_Assessment.AMPreferredType = Convert.ToString(dataReceive.Tables[0].Rows[index]["AM_PreferredType"]);

                        l_Assessment.PMPreferredType = Convert.ToString(dataReceive.Tables[0].Rows[index]["PM_PreferredType"]);
                        l_Assessment.BathingPreferredType = Convert.ToString(dataReceive.Tables[0].Rows[index]["Bathing_PreferredType"]);
                        l_Assessment.PreferredDays = Convert.ToString(dataReceive.Tables[0].Rows[index]["PreferredDays"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanVitalSigns |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanAssistanceWithModel GetCarePlanAssistanceWith(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanAssistanceWithModel l_Assessment = new CarePlanAssistanceWithModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_AssistanceWith", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CarePlanId", p_CarePlanId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.ResidentId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.CarePlanId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["CarePlanId"]);
                        l_Assessment.Dressing = Convert.ToString(dataReceive.Tables[0].Rows[index]["Dressing"]);
                        l_Assessment.DressingPreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["Dressing_PreferredTime"]);
                        l_Assessment.NailCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["NailCare"]);
                        l_Assessment.NailCarePreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["NailCare_PreferredTime"]);
                        l_Assessment.Shaving = Convert.ToString(dataReceive.Tables[0].Rows[index]["Shaving"]);
                        l_Assessment.ShavingPreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["Shaving_PreferredTime"]);
                        l_Assessment.FootCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["FootCare"]);
                        l_Assessment.FootCarePreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["FootCare_PreferredTime"]);
                        l_Assessment.OralHygiene = Convert.ToString(dataReceive.Tables[0].Rows[index]["OralHygiene"]);
                        l_Assessment.OralHygienePreferredTime = Convert.ToString(dataReceive.Tables[0].Rows[index]["OralHygiene_PreferredTime"]);
                        l_Assessment.Teeth = Convert.ToString(dataReceive.Tables[0].Rows[index]["Teeth"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanAssistanceWith |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanMobilityModel GetCarePlanMobility(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanMobilityModel l_Assessment = new CarePlanMobilityModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Mobility", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CarePlanId", p_CarePlanId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.ResidentId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.CarePlanId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["CarePlanId"]);
                        l_Assessment.Mobility = Convert.ToString(dataReceive.Tables[0].Rows[index]["Mobility"]);
                        l_Assessment.Transfers = Convert.ToString(dataReceive.Tables[0].Rows[index]["Transfers"]);
                        l_Assessment.Lift = Convert.ToString(dataReceive.Tables[0].Rows[index]["MechanicalLift"]);
                        l_Assessment.Walker = Convert.ToString(dataReceive.Tables[0].Rows[index]["Walker"]);
                        l_Assessment.WalkerType = Convert.ToString(dataReceive.Tables[0].Rows[index]["Walker_Type"]);
                        l_Assessment.WheelChair = Convert.ToString(dataReceive.Tables[0].Rows[index]["WheelChair"]);
                        l_Assessment.WheelChairType = Convert.ToString(dataReceive.Tables[0].Rows[index]["WheelChair_Type"]);
                        l_Assessment.Cane = Convert.ToString(dataReceive.Tables[0].Rows[index]["Cane"]);
                        l_Assessment.caneType = Convert.ToString(dataReceive.Tables[0].Rows[index]["Cane_Type"]);
                        l_Assessment.Scooter = Convert.ToString(dataReceive.Tables[0].Rows[index]["Scooter"]);
                        l_Assessment.ScooterType = Convert.ToString(dataReceive.Tables[0].Rows[index]["Scooter_Type"]);
                        l_Assessment.PTFrequency = Convert.ToString(dataReceive.Tables[0].Rows[index]["PT_Frequency"]);
                        l_Assessment.PTProvider = Convert.ToString(dataReceive.Tables[0].Rows[index]["PT_Provider"]);
                        l_Assessment.OT = Convert.ToString(dataReceive.Tables[0].Rows[index]["OT"]);
                        l_Assessment.OTFrequency = Convert.ToString(dataReceive.Tables[0].Rows[index]["OT_Frequency"]);
                        l_Assessment.OTProvider = Convert.ToString(dataReceive.Tables[0].Rows[index]["OT_Provider"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanMobility |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanSafetyModel GetCarePlanSafety(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanSafetyModel l_Assessment = new CarePlanSafetyModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Safety", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@CarePlanId", p_CarePlanId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.ResidentId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.CarePlanId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["CarePlanId"]);
                        l_Assessment.Safety = Convert.ToString(dataReceive.Tables[0].Rows[index]["SafetyPASD"]);
                        l_Assessment.Other = Convert.ToString(dataReceive.Tables[0].Rows[index]["Other"]);
                        l_Assessment.Rails = Convert.ToString(dataReceive.Tables[0].Rows[index]["Rails"]);
                        l_Assessment.NightOnly = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["NightOnly"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanSafety |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
    }
}