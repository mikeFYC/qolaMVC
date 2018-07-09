using Newtonsoft.Json;
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
        public static void AddCarePlan(PlanOfCareModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_PlanOfCare", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@Assessed", p_Model.Assessed);
                l_Cmd.Parameters.AddWithValue("@LevelOfCare", p_Model.LevelOfCare);
                l_Cmd.Parameters.AddWithValue("@CompleteStatus", p_Model.CompleteStatus);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);
                int l_AssessmentId = 0;
                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_AssessmentId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                    }

                    //VITAL SIGNS
                    SqlCommand l_Cmd_VitalSigns = new SqlCommand("spAB_Add_PlanOfCare_VitalSigns", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_VitalSigns.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@BP_Systolic", p_Model.VitalSigns.BPSystolic);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@BP_Diastolic", p_Model.VitalSigns.BPDiastolic);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@BP_DateCompleted", p_Model.VitalSigns.BPDateCompleted);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Temperature", p_Model.VitalSigns.Temperature);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Temp_DateCompleted", p_Model.VitalSigns.TempDateCompleted);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@WeightLBS", p_Model.VitalSigns.Weight);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Weight_DateCompleted", p_Model.VitalSigns.WeightDateCompleted);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Height_Feet", p_Model.VitalSigns.HeightFeet);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Height_Inches", p_Model.VitalSigns.HeightInches);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Height_DateCompleted", p_Model.VitalSigns.HeightDateCompleted);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Pulse", p_Model.VitalSigns.Pulse);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@Pulse_DateCompleted", p_Model.VitalSigns.PulseDateCompleted);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@PulseRegular", p_Model.VitalSigns.PulseRegular);
                    l_Cmd_VitalSigns.ExecuteNonQuery();

                    //Personal Hygiene
                    SqlCommand l_Cmd_PersonalHygiene = new SqlCommand("spAB_Add_PlanOfCare_PersonalHygiene", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_PersonalHygiene.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@AMCare", p_Model.PersonalHygiene.AMCare);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@PMCare", p_Model.PersonalHygiene.PMCare);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@Bathing", p_Model.PersonalHygiene.Bathing);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@AM_AssistedBy", p_Model.PersonalHygiene.AMAssistedBy);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@PM_AssistedBy", p_Model.PersonalHygiene.PMAssistedBy);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@Bathing_AssistedBy", p_Model.PersonalHygiene.BathingAssistedBy);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@AM_AgencyName", p_Model.PersonalHygiene.AMAgencyName);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@PM_AgencyName", p_Model.PersonalHygiene.PMAgencyName);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@Bathing_AgencyName", p_Model.PersonalHygiene.BathingAgencyName);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@AM_PreferredTime", p_Model.PersonalHygiene.AMPreferredTime);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@PM_PreferredTime", p_Model.PersonalHygiene.PMPreferredTime);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@Bathing_PreferredTime", p_Model.PersonalHygiene.BathingPreferredTime);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@AM_PreferredType", p_Model.PersonalHygiene.AMPreferredType);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@PM_PreferredType", p_Model.PersonalHygiene.PMPreferredType);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@Bathing_PreferredType", p_Model.PersonalHygiene.BathingPreferredType);
                    l_Cmd_PersonalHygiene.Parameters.AddWithValue("@PreferredDays", JsonConvert.SerializeObject(p_Model.PersonalHygiene.PreferredDaysCollection, Formatting.Indented)); // p_Model.PersonalHygiene.PreferredDays);
                    l_Cmd_PersonalHygiene.ExecuteNonQuery();

                    //Assistance with
                    SqlCommand l_Cmd_AssistanceWith = new SqlCommand("spAB_Add_PlanOfCare_AssistanceWith", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_AssistanceWith.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@Dressing", p_Model.AssistanceWith.Dressing);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@Dressing_PreferredTime", p_Model.AssistanceWith.DressingPreferredTime);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@NailCare", p_Model.AssistanceWith.NailCare);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@NailCare_PreferredTime", p_Model.AssistanceWith.NailCarePreferredTime);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@Shaving", p_Model.AssistanceWith.Shaving);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@Shaving_PreferredTime", p_Model.AssistanceWith.ShavingPreferredTime);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@FootCare", p_Model.AssistanceWith.FootCare);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@FootCare_PreferredTime", p_Model.AssistanceWith.FootCarePreferredTime);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@OralHygiene", p_Model.AssistanceWith.OralHygiene);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@OralHygiene_PreferredTime", p_Model.AssistanceWith.OralHygienePreferredTime);
                    l_Cmd_AssistanceWith.Parameters.AddWithValue("@Teeth", JsonConvert.SerializeObject(p_Model.AssistanceWith.TeethCollection, Formatting.Indented));
                    l_Cmd_AssistanceWith.ExecuteNonQuery();

                    //Mobility
                    SqlCommand l_Cmd_Mobility = new SqlCommand("spAB_Add_PlanOfCare_Mobility", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Mobility.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Mobility.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Mobility.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Mobility", p_Model.Mobility.Mobility);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Transfers", p_Model.Mobility.Transfers);
                    l_Cmd_Mobility.Parameters.AddWithValue("@MechanicalLift", p_Model.Mobility.Lift);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Lift", p_Model.Mobility.Lift);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Walker", p_Model.Mobility.Walker);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Walker_Type", p_Model.Mobility.WalkerType);
                    l_Cmd_Mobility.Parameters.AddWithValue("@WheelChair", p_Model.Mobility.WheelChair);
                    l_Cmd_Mobility.Parameters.AddWithValue("@WheelChair_Type", p_Model.Mobility.WheelChairType);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Cane", p_Model.Mobility.Cane);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Cane_Type", p_Model.Mobility.caneType);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Scooter", p_Model.Mobility.Scooter);
                    l_Cmd_Mobility.Parameters.AddWithValue("@Scooter_Type", p_Model.Mobility.ScooterType);
                    l_Cmd_Mobility.Parameters.AddWithValue("@PT", p_Model.Mobility.PT);
                    l_Cmd_Mobility.Parameters.AddWithValue("@PT_Frequency", p_Model.Mobility.PTFrequency);
                    l_Cmd_Mobility.Parameters.AddWithValue("@PT_Provider", p_Model.Mobility.PTProvider);
                    l_Cmd_Mobility.Parameters.AddWithValue("@OT", p_Model.Mobility.OT);
                    l_Cmd_Mobility.Parameters.AddWithValue("@OT_Frequency", p_Model.Mobility.OTFrequency);
                    l_Cmd_Mobility.Parameters.AddWithValue("@OT_Provider", p_Model.Mobility.OTProvider);
                    l_Cmd_Mobility.ExecuteNonQuery();

                    //Safety
                    SqlCommand l_Cmd_Safety = new SqlCommand("spAB_Add_PlanOfCare_Safety", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Safety.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Safety.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Safety.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Safety.Parameters.AddWithValue("@SafetyPASD", p_Model.Safety.Safety);
                    l_Cmd_Safety.Parameters.AddWithValue("@Other", p_Model.Safety.Other);
                    l_Cmd_Safety.Parameters.AddWithValue("@Rails", p_Model.Safety.Rails);
                    l_Cmd_Safety.Parameters.AddWithValue("@NightOnly", p_Model.Safety.NightOnly);
                    l_Cmd_Safety.ExecuteNonQuery();


                    //Meal Escort
                    SqlCommand l_Cmd_MealEscort = new SqlCommand("spAB_Add_PlanOfCare_MealEscort", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_MealEscort.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_MealEscort.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_MealEscort.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_MealEscort.Parameters.AddWithValue("@Breakfast", p_Model.MealEscort.BreakFast);
                    l_Cmd_MealEscort.Parameters.AddWithValue("@Lunch", p_Model.MealEscort.Lunch);
                    l_Cmd_MealEscort.Parameters.AddWithValue("@Dinner", p_Model.MealEscort.Dinner);
                    l_Cmd_MealEscort.ExecuteNonQuery();


                    //Beahviour
                    SqlCommand l_Cmd_Behaviour = new SqlCommand("spAB_Add_PlanOfCare_Behaviour", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Behaviour.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Behaviour.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Behaviour.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Behaviour.Parameters.AddWithValue("@Behavior", JsonConvert.SerializeObject(p_Model.Behaviour.BehaviourCollection, Formatting.Indented));
                    l_Cmd_Behaviour.Parameters.AddWithValue("@HarmToSelf", p_Model.Behaviour.HarmToSelf);
                    l_Cmd_Behaviour.Parameters.AddWithValue("@Smoker", p_Model.Behaviour.Smoker);
                    l_Cmd_Behaviour.Parameters.AddWithValue("@RiskOfWandering", p_Model.Behaviour.RiskOfWandering);
                    l_Cmd_Behaviour.Parameters.AddWithValue("@CognitiveStatus", p_Model.Behaviour.CognitiveStatus);
                    l_Cmd_Behaviour.Parameters.AddWithValue("@OtherInfo", p_Model.Behaviour.OtherInfo);
                    l_Cmd_Behaviour.ExecuteNonQuery();
                    
                    
                    //Cognitive Function
                    SqlCommand l_Cmd_CognitiveFunction = new SqlCommand("spAB_Add_PlanOfCare_CognitiveFunction", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_CognitiveFunction.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_CognitiveFunction.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_CognitiveFunction.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_CognitiveFunction.Parameters.AddWithValue("@CognitiveFunction", JsonConvert.SerializeObject(p_Model.CognitiveFunction.CognitiveFunction, Formatting.Indented));
                    l_Cmd_CognitiveFunction.ExecuteNonQuery();


                    //Orientation
                    SqlCommand l_Cmd_Orientation = new SqlCommand("spAB_Add_PlanOfCare_Orientation", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Orientation.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Orientation.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Orientation.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Orientation.Parameters.AddWithValue("@IsPerson", p_Model.Orientation.IsPerson);
                    l_Cmd_Orientation.Parameters.AddWithValue("@IsPlace", p_Model.Orientation.IsPlace);
                    l_Cmd_Orientation.Parameters.AddWithValue("@IsTime", p_Model.Orientation.IsTime);
                    l_Cmd_Orientation.Parameters.AddWithValue("@IsDementiaCare", p_Model.Orientation.IsDementiaCare);
                    l_Cmd_Orientation.ExecuteNonQuery();


                    //Nutrition
                    SqlCommand l_Cmd_Nutrition = new SqlCommand("spAB_Add_PlanOfCare_Nutrition", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Nutrition.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Nutrition.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@NutritionStatus", p_Model.Nutrition.NutritionStatus);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@Risk", p_Model.Nutrition.Risk);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@AssistiveDevices", p_Model.Nutrition.AssistiveDevices);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@Texture", p_Model.Nutrition.Texture);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@Other", p_Model.Nutrition.Other);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@Diet", p_Model.Nutrition.Diet);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@OtherDiet", p_Model.Nutrition.OtherDiet);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@Notes", p_Model.Nutrition.Notes);
                    l_Cmd_Nutrition.Parameters.AddWithValue("@Allergies", p_Model.Nutrition.Allergies);
                    l_Cmd_Nutrition.ExecuteNonQuery();


                    //Meals
                    SqlCommand l_Cmd_Meals = new SqlCommand("spAB_Add_PlanOfCare_Meals", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Meals.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Meals.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Meals.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Meals.Parameters.AddWithValue("@Appetite", p_Model.Meals.Appetite);
                    l_Cmd_Meals.Parameters.AddWithValue("@BreakFast", p_Model.Meals.BreakFast);
                    l_Cmd_Meals.Parameters.AddWithValue("@Lunch", p_Model.Meals.Lunch);
                    l_Cmd_Meals.Parameters.AddWithValue("@Dinner", p_Model.Meals.Dinner);
                    l_Cmd_Meals.ExecuteNonQuery();


                    //Elimination
                    SqlCommand l_Cmd_Elimination = new SqlCommand("spAB_Add_PlanOfCare_Elimination", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Elimination.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Elimination.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Elimination.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Elimination.Parameters.AddWithValue("@Bladder", JsonConvert.SerializeObject(p_Model.Elimination.Bladder, Formatting.Indented));
                    l_Cmd_Elimination.Parameters.AddWithValue("@Bowel", JsonConvert.SerializeObject(p_Model.Elimination.Bowel, Formatting.Indented));
                    l_Cmd_Elimination.Parameters.AddWithValue("@NameCode", p_Model.Elimination.NameCode);
                    l_Cmd_Elimination.Parameters.AddWithValue("@ContinenceProducts", p_Model.Elimination.Products);
                    l_Cmd_Elimination.Parameters.AddWithValue("@Supplier", p_Model.Elimination.Supplier);
                    l_Cmd_Elimination.Parameters.AddWithValue("@AssessmentCompletedBy", p_Model.Elimination.CompletedBy);
                    l_Cmd_Elimination.Parameters.AddWithValue("@AssessmentDate", p_Model.Elimination.AssessmentDate);
                    l_Cmd_Elimination.ExecuteNonQuery();


                    //Toileting
                    SqlCommand l_Cmd_Toileting = new SqlCommand("spAB_Add_PlanOfCare_Toileting", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Toileting.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Toileting.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Toileting.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Toileting.Parameters.AddWithValue("@Bathroom", p_Model.Toileting.Bathroom);
                    l_Cmd_Toileting.Parameters.AddWithValue("@Commode", p_Model.Toileting.Commode);
                    l_Cmd_Toileting.Parameters.AddWithValue("@Bedpan", p_Model.Toileting.Bedpan);
                    l_Cmd_Toileting.Parameters.AddWithValue("@Toileting", p_Model.Toileting.Toileting);
                    l_Cmd_Toileting.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                exception = "AddCarePlan |" + ex.ToString();
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
                        l_Assessment.PreferredDaysCollection = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["PreferredDays"]));
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
                        l_Assessment.TeethCollection = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Teeth"]));
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
 
 
 
 
 
 