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
                    SqlCommand l_Cmd_VitalSigns = new SqlCommand("spAB_Add_PlanOfCare_VitalSigns_mike", l_Conn);
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
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@O2_sat", p_Model.VitalSigns.O2_sat);
                    l_Cmd_VitalSigns.Parameters.AddWithValue("@O2_sat_Date", p_Model.VitalSigns.O2_sat_Date);
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
                    l_Cmd_Nutrition.Parameters.AddWithValue("@Diet", JsonConvert.SerializeObject(p_Model.Nutrition.Diet, Formatting.Indented));
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
                    l_Cmd_Toileting.Parameters.AddWithValue("@Bathroom", JsonConvert.SerializeObject(p_Model.Toileting.Bathroom, Formatting.Indented));
                    l_Cmd_Toileting.Parameters.AddWithValue("@Commode", JsonConvert.SerializeObject(p_Model.Toileting.Commode, Formatting.Indented));
                    l_Cmd_Toileting.Parameters.AddWithValue("@Bedpan", JsonConvert.SerializeObject(p_Model.Toileting.Bedpan, Formatting.Indented));
                    l_Cmd_Toileting.Parameters.AddWithValue("@Toileting", p_Model.Toileting.Toileting);
                    l_Cmd_Toileting.ExecuteNonQuery();


                    //Medication
                    SqlCommand l_Cmd_Medication = new SqlCommand("spAB_Add_PlanOfCare_Medication", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Medication.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Medication.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Medication.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Medication.Parameters.AddWithValue("@Assistance", p_Model.Medication.Assistance);
                    l_Cmd_Medication.Parameters.AddWithValue("@Administration", p_Model.Medication.Administration);
                    l_Cmd_Medication.Parameters.AddWithValue("@CompletedBy", p_Model.Medication.CompletedBy);
                    l_Cmd_Medication.Parameters.AddWithValue("@Agency", p_Model.Medication.Agency);
                    l_Cmd_Medication.Parameters.AddWithValue("@Pharmacy", p_Model.Medication.Pharmacy);
                    l_Cmd_Medication.Parameters.AddWithValue("@Allergies", p_Model.Medication.Allergies);
                    l_Cmd_Medication.ExecuteNonQuery();


                    //Sensory Abilities
                    SqlCommand l_Cmd_SensoryAbilities = new SqlCommand("spAB_Add_PlanOfCare_SensoryAbilities", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_SensoryAbilities.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_SensoryAbilities.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_SensoryAbilities.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_SensoryAbilities.Parameters.AddWithValue("@Vision", JsonConvert.SerializeObject(p_Model.SensoryAbilities.Vision, Formatting.Indented));
                    l_Cmd_SensoryAbilities.Parameters.AddWithValue("@Hearing", JsonConvert.SerializeObject(p_Model.SensoryAbilities.Hearing, Formatting.Indented));
                    l_Cmd_SensoryAbilities.Parameters.AddWithValue("@Communication", JsonConvert.SerializeObject(p_Model.SensoryAbilities.Communication, Formatting.Indented));
                    l_Cmd_SensoryAbilities.Parameters.AddWithValue("@Notes", p_Model.SensoryAbilities.Notes);
                    l_Cmd_SensoryAbilities.ExecuteNonQuery();


                    //Wound Care
                    SqlCommand l_Cmd_WoundCare = new SqlCommand("spAB_Add_PlanOfCare_WoundCare", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_WoundCare.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_WoundCare.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_WoundCare.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_WoundCare.Parameters.AddWithValue("@WoundCare", p_Model.WoundCare.WoundCare);
                    l_Cmd_WoundCare.Parameters.AddWithValue("@AssistedBy", p_Model.WoundCare.AssistedBy);
                    l_Cmd_WoundCare.Parameters.AddWithValue("@Agency", p_Model.WoundCare.Agency);
                    l_Cmd_WoundCare.ExecuteNonQuery();


                    //Skin Care
                    SqlCommand l_Cmd_SkinCare = new SqlCommand("spAB_Add_PlanOfCare_SkinCare", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_SkinCare.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_SkinCare.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_SkinCare.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_SkinCare.Parameters.AddWithValue("@SkinCare", p_Model.SkinCare.SkinCare);
                    l_Cmd_SkinCare.Parameters.AddWithValue("@SpecialTreatments", p_Model.SkinCare.SpecialTreatments);
                    l_Cmd_SkinCare.ExecuteNonQuery();


                    //Special Needs
                    SqlCommand l_Cmd_SpecialNeeds = new SqlCommand("spAB_Add_PlanOfCare_SpecialNeeds", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_SpecialNeeds.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@Oxygen", p_Model.SpecialNeeds.Oxygen);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@Oxygen_Supplier", p_Model.SpecialNeeds.OxygenSupplier);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@Oxygen_Rate", p_Model.SpecialNeeds.OxygenRate);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@Oxygen_Notes", p_Model.SpecialNeeds.OxygenNotes);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@CPAP", p_Model.SpecialNeeds.CPAP);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@CPAP_Supplier", p_Model.SpecialNeeds.CPAPSupplier);
                    l_Cmd_SpecialNeeds.Parameters.AddWithValue("@CPAP_Notes", p_Model.SpecialNeeds.CPAPNotes);
                    l_Cmd_SpecialNeeds.ExecuteNonQuery();


                    //Special Equipment
                    SqlCommand l_Cmd_SpecialEquipment = new SqlCommand("spAB_Add_PlanOfCare_SpecialEquipment", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_SpecialEquipment.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_SpecialEquipment.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_SpecialEquipment.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_SpecialEquipment.Parameters.AddWithValue("@SpecialEquipment", JsonConvert.SerializeObject(p_Model.SpecialEquipment.SpecialEquipment, Formatting.Indented));
                    l_Cmd_SpecialEquipment.Parameters.AddWithValue("@Details", p_Model.SpecialEquipment.Details);
                    l_Cmd_SpecialEquipment.ExecuteNonQuery();


                    //Family
                    SqlCommand l_Cmd_Family = new SqlCommand("spAB_Add_PlanOfCare_Family", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Family.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Family.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Family.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Family.Parameters.AddWithValue("@FamilyMeeting", p_Model.FamilySupportModel.FamilyMeeting);
                    l_Cmd_Family.Parameters.AddWithValue("@FamilyInvolvment", p_Model.FamilySupportModel.FamilyInvolvement);
                    l_Cmd_Family.ExecuteNonQuery();


                    //Immunization
                    SqlCommand l_Cmd_Immunization = new SqlCommand("spAB_Add_PlanOfCare_Immunization", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_Immunization.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_Immunization.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_Immunization.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_Immunization.Parameters.AddWithValue("@TB", p_Model.Immunization.TB);
                    l_Cmd_Immunization.Parameters.AddWithValue("@TB_Date", p_Model.Immunization.TBDate);
                    l_Cmd_Immunization.Parameters.AddWithValue("@ChestXRay", p_Model.Immunization.ChestXRay);
                    l_Cmd_Immunization.Parameters.AddWithValue("@ChestXRay_Date", p_Model.Immunization.ChestXRayDate);
                    l_Cmd_Immunization.Parameters.AddWithValue("@Pneumonia", p_Model.Immunization.Pneumonia);
                    l_Cmd_Immunization.Parameters.AddWithValue("@Pneumonia_Date", p_Model.Immunization.PneumoniaDate);
                    l_Cmd_Immunization.Parameters.AddWithValue("@FluVaccine", p_Model.Immunization.FluVaccine);
                    l_Cmd_Immunization.Parameters.AddWithValue("@FluVaccine_Date", p_Model.Immunization.FluVaccineDate);
                    l_Cmd_Immunization.Parameters.AddWithValue("@Tetanus", p_Model.Immunization.Tetanus);
                    l_Cmd_Immunization.Parameters.AddWithValue("@Tetanus_Date", p_Model.Immunization.TetanusDate);
                    l_Cmd_Immunization.ExecuteNonQuery();


                    //Infectious Diseases
                    SqlCommand l_Cmd_InfectiousDiseases = new SqlCommand("spAB_Add_PlanOfCare_InfectiousDiseases", l_Conn);
                    // l_Conn.Open();
                    l_Cmd_InfectiousDiseases.CommandType = System.Data.CommandType.StoredProcedure;
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@CarePlanId", l_AssessmentId);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@MRSA", p_Model.InfectiousDiseases.MRSA);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@MRSA_Diagnosed_Date", p_Model.InfectiousDiseases.MRSADiagnosedDate);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@MRSA_Resolved_Date", p_Model.InfectiousDiseases.MRSAResolvedDate);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@VRE", p_Model.InfectiousDiseases.VRE);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@VRE_Diagnosed_Date", p_Model.InfectiousDiseases.VREDiagnosedDate);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@VRE_Resolved_Date", p_Model.InfectiousDiseases.VREResolvedDate);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@CDiff", p_Model.InfectiousDiseases.CDiff);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@CDiff_Diagnosed_Date", p_Model.InfectiousDiseases.CDiffDiagnosedDate);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@CDiff_Resolved_Date", p_Model.InfectiousDiseases.CDiffResolvedDate);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@Other", p_Model.InfectiousDiseases.Other);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@Other_Diagnosed_Date", p_Model.InfectiousDiseases.OtherDiagnosedDate);
                    l_Cmd_InfectiousDiseases.Parameters.AddWithValue("@Other_Resolved_Date", p_Model.InfectiousDiseases.OtherResolvedDate);
                    l_Cmd_InfectiousDiseases.ExecuteNonQuery();

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
                        l_Assessment.MealEscort = GetCarePlanMealEscort(l_Assessment.Id);
                        l_Assessment.Behaviour = GetCarePlanBehaviour(l_Assessment.Id);
                        l_Assessment.CognitiveFunction = GetCarePlanCognitiveFunction(l_Assessment.Id);
                        l_Assessment.Orientation = GetCarePlanOrientation(l_Assessment.Id);
                        l_Assessment.Nutrition = GetCarePlanNutrition(l_Assessment.Id);
                        l_Assessment.Meals = GetCarePlanMeals(l_Assessment.Id);
                        l_Assessment.Elimination = GetCarePlanElimination(l_Assessment.Id);
                        l_Assessment.Toileting = GetCarePlanToileting(l_Assessment.Id);
                        l_Assessment.Medication = GetCarePlanMedication(l_Assessment.Id);
                        l_Assessment.SensoryAbilities = GetCarePlanSensoryAbilities(l_Assessment.Id);
                        l_Assessment.WoundCare = GetCarePlanWoundCare(l_Assessment.Id);
                        l_Assessment.SkinCare = GetCarePlanSkinCare(l_Assessment.Id);
                        l_Assessment.SpecialNeeds = GetCarePlanSpecialNeeds(l_Assessment.Id);
                        l_Assessment.SpecialEquipment = GetCarePlanSpecialEquipment(l_Assessment.Id);
                        l_Assessment.FamilySupportModel = GetCarePlanFamilySupport(l_Assessment.Id);
                        l_Assessment.Immunization = GetCarePlanImmunization(l_Assessment.Id);
                        l_Assessment.InfectiousDiseases = GetCarePlanInfectiousDiseases(l_Assessment.Id);

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
                        l_Assessment.O2_sat = Convert.ToString(dataReceive.Tables[0].Rows[index]["O2_sat(%)"]);
                        l_Assessment.O2_sat_Date = Convert.ToString(dataReceive.Tables[0].Rows[index]["O2_sat_Date"]);
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
        private static CarePlanMealEscortModel GetCarePlanMealEscort(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanMealEscortModel l_Assessment = new CarePlanMealEscortModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_MealEscort", l_Conn);
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
                        l_Assessment.BreakFast = Convert.ToString(dataReceive.Tables[0].Rows[index]["BreakFast"]);
                        l_Assessment.Lunch = Convert.ToString(dataReceive.Tables[0].Rows[index]["Lunch"]);
                        l_Assessment.Dinner = Convert.ToString(dataReceive.Tables[0].Rows[index]["Dinner"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanMealEscort |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanBehaviourModel GetCarePlanBehaviour(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanBehaviourModel l_Assessment = new CarePlanBehaviourModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Behaviour", l_Conn);
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
                        l_Assessment.BehaviourCollection = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Behavior"]));
                        l_Assessment.HarmToSelf = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["HarmToSelf"]);
                        l_Assessment.Smoker = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["smoker"]);
                        l_Assessment.RiskOfWandering = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["RiskOfWandering"]);
                        l_Assessment.CognitiveStatus = Convert.ToString(dataReceive.Tables[0].Rows[index]["CognitiveStatus"]);
                        l_Assessment.OtherInfo = Convert.ToString(dataReceive.Tables[0].Rows[index]["OtherInfo"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanBehaviour |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanCognitiveFunctionModel GetCarePlanCognitiveFunction(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanCognitiveFunctionModel l_Assessment = new CarePlanCognitiveFunctionModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_CognitiveFunction", l_Conn);
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
                        l_Assessment.CognitiveFunction = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["CognitiveFunction"]));
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanCognitiveFunction |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanOrientationModel GetCarePlanOrientation(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanOrientationModel l_Assessment = new CarePlanOrientationModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Orientation", l_Conn);
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
                        l_Assessment.IsDementiaCare = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["IsDementiaCare"]);
                        l_Assessment.IsPerson = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["IsPerson"]);
                        l_Assessment.IsPlace = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["IsPlace"]);
                        l_Assessment.IsTime = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["IsTime"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanOrientation |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanNutritionModel GetCarePlanNutrition(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanNutritionModel l_Assessment = new CarePlanNutritionModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Nutrition", l_Conn);
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
                        l_Assessment.NutritionStatus = Convert.ToString(dataReceive.Tables[0].Rows[index]["NutritionStatus"]);
                        l_Assessment.Risk = Convert.ToString(dataReceive.Tables[0].Rows[index]["Risk"]);
                        l_Assessment.AssistiveDevices = Convert.ToString(dataReceive.Tables[0].Rows[index]["AssistiveDevices"]);
                        l_Assessment.Texture = Convert.ToString(dataReceive.Tables[0].Rows[index]["Texture"]);
                        l_Assessment.Other = Convert.ToString(dataReceive.Tables[0].Rows[index]["Other"]);
                        l_Assessment.Diet = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Diet"]));
                        l_Assessment.OtherDiet = Convert.ToString(dataReceive.Tables[0].Rows[index]["OtherDiet"]);
                        l_Assessment.Notes = Convert.ToString(dataReceive.Tables[0].Rows[index]["Notes"]);
                        l_Assessment.Allergies = Convert.ToString(dataReceive.Tables[0].Rows[index]["Allergies"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanNutrition |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanMealsModel GetCarePlanMeals(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanMealsModel l_Assessment = new CarePlanMealsModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Meals", l_Conn);
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
                        l_Assessment.Appetite = Convert.ToString(dataReceive.Tables[0].Rows[index]["Appetite"]);
                        l_Assessment.BreakFast = Convert.ToString(dataReceive.Tables[0].Rows[index]["BreakFast"]);
                        l_Assessment.Lunch = Convert.ToString(dataReceive.Tables[0].Rows[index]["Lunch"]);
                        l_Assessment.Dinner = Convert.ToString(dataReceive.Tables[0].Rows[index]["Dinner"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanMeals |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanEliminationModel GetCarePlanElimination(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanEliminationModel l_Assessment = new CarePlanEliminationModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Elimination", l_Conn);
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
                        l_Assessment.Bladder = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Bladder"]));
                        l_Assessment.Bowel = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Bowel"]));
                        l_Assessment.NameCode = Convert.ToString(dataReceive.Tables[0].Rows[index]["NameCode"]);
                        l_Assessment.Products = Convert.ToString(dataReceive.Tables[0].Rows[index]["ContinenceProducts"]);
                        l_Assessment.AssistiveDevices = Convert.ToString(dataReceive.Tables[0].Rows[index]["AssistiveDevices"]);
                        l_Assessment.CompletedBy = Convert.ToString(dataReceive.Tables[0].Rows[index]["AssessmentCompletedBy"]);
                        l_Assessment.AssessmentDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["AssessmentDate"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanElimination |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanToiletingModel GetCarePlanToileting(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanToiletingModel l_Assessment = new CarePlanToiletingModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Toileting", l_Conn);
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
                        l_Assessment.Bathroom = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Bathroom"]));
                        l_Assessment.Commode = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Commode"]));
                        l_Assessment.Bedpan = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Bedpan"]));
                        l_Assessment.Toileting = Convert.ToString(dataReceive.Tables[0].Rows[index]["Toileting"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanToileting |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanMedication GetCarePlanMedication(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanMedication l_Assessment = new CarePlanMedication();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Medication", l_Conn);
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
                        l_Assessment.Assistance = Convert.ToString(dataReceive.Tables[0].Rows[index]["Assistance"]);
                        l_Assessment.Administration = Convert.ToString(dataReceive.Tables[0].Rows[index]["Administration"]);
                        l_Assessment.CompletedBy = Convert.ToString(dataReceive.Tables[0].Rows[index]["CompletedBy"]);
                        l_Assessment.Agency = Convert.ToString(dataReceive.Tables[0].Rows[index]["Agency"]);
                        l_Assessment.Pharmacy = Convert.ToString(dataReceive.Tables[0].Rows[index]["Pharmacy"]);
                        l_Assessment.Allergies = Convert.ToString(dataReceive.Tables[0].Rows[index]["Allergies"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanMedication |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanSensoryAbilitiesModel GetCarePlanSensoryAbilities(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanSensoryAbilitiesModel l_Assessment = new CarePlanSensoryAbilitiesModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_SensoryAbilities", l_Conn);
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
                        l_Assessment.Vision = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Vision"]));
                        l_Assessment.Hearing = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Hearing"]));
                        l_Assessment.Communication = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["Communication"]));
                        l_Assessment.Notes = Convert.ToString(dataReceive.Tables[0].Rows[index]["Notes"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanMedication |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanWoundCareModel GetCarePlanWoundCare(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanWoundCareModel l_Assessment = new CarePlanWoundCareModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_WoundCare", l_Conn);
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
                        l_Assessment.WoundCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["WoundCare"]);
                        l_Assessment.AssistedBy = Convert.ToString(dataReceive.Tables[0].Rows[index]["AssistedBy"]);
                        l_Assessment.Agency = Convert.ToString(dataReceive.Tables[0].Rows[index]["Agency"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanWoundCare |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanSkinCareModel GetCarePlanSkinCare(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanSkinCareModel l_Assessment = new CarePlanSkinCareModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_SkinCare", l_Conn);
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
                        l_Assessment.SkinCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["SkinCare"]);
                        l_Assessment.SpecialTreatments = Convert.ToString(dataReceive.Tables[0].Rows[index]["SpecialTreatments"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanSkinCare |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanSpecialNeedsModel GetCarePlanSpecialNeeds(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanSpecialNeedsModel l_Assessment = new CarePlanSpecialNeedsModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_SpecialNeeds", l_Conn);
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
                        l_Assessment.Oxygen = Convert.ToString(dataReceive.Tables[0].Rows[index]["Oxygen"]);
                        l_Assessment.OxygenSupplier = Convert.ToString(dataReceive.Tables[0].Rows[index]["Oxygen_Supplier"]);
                        l_Assessment.OxygenRate = Convert.ToString(dataReceive.Tables[0].Rows[index]["Oxygen_Rate"]);
                        l_Assessment.OxygenNotes = Convert.ToString(dataReceive.Tables[0].Rows[index]["Oxygen_Notes"]);
                        l_Assessment.CPAP = Convert.ToString(dataReceive.Tables[0].Rows[index]["CPAP"]);
                        l_Assessment.CPAPSupplier = Convert.ToString(dataReceive.Tables[0].Rows[index]["CPAP_Supplier"]);
                        l_Assessment.CPAPNotes = Convert.ToString(dataReceive.Tables[0].Rows[index]["CPAP_Notes"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanSpecialNeeds |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanSpecialEquipmentModel GetCarePlanSpecialEquipment(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanSpecialEquipmentModel l_Assessment = new CarePlanSpecialEquipmentModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_SpecialEquipment", l_Conn);
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
                        l_Assessment.SpecialEquipment = JsonConvert.DeserializeObject<Collection<QOLACheckboxModel>>(Convert.ToString(dataReceive.Tables[0].Rows[index]["SpecialEquipment"]));
                        //adjuct by mike change Oxygen_Supplier to Details l_Assessment.Details = Convert.ToString(dataReceive.Tables[0].Rows[index]["Oxygen_Supplier"]);
                        l_Assessment.Details = Convert.ToString(dataReceive.Tables[0].Rows[index]["Details"]); 
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanSpecialEquipment |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanFamilySupportModel GetCarePlanFamilySupport(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanFamilySupportModel l_Assessment = new CarePlanFamilySupportModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_FamilySupport", l_Conn);
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
                        l_Assessment.FamilyMeeting = Convert.ToString(dataReceive.Tables[0].Rows[index]["FamilyMeeting"]);
                        l_Assessment.FamilyInvolvement = Convert.ToString(dataReceive.Tables[0].Rows[index]["FamilyInvolvement"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanFamilySupport |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanImmunizationModel GetCarePlanImmunization(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanImmunizationModel l_Assessment = new CarePlanImmunizationModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_Immunization", l_Conn);
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
                        l_Assessment.TB = Convert.ToString(dataReceive.Tables[0].Rows[index]["TB"]);
                        l_Assessment.TBDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["TB_Date"]);
                        l_Assessment.ChestXRay = Convert.ToString(dataReceive.Tables[0].Rows[index]["ChestXRay"]);
                        l_Assessment.ChestXRayDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["ChestXRay_Date"]);
                        l_Assessment.Pneumonia = Convert.ToString(dataReceive.Tables[0].Rows[index]["Pneumonia"]);
                        l_Assessment.PneumoniaDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["Pneumonia_Date"]);
                        l_Assessment.FluVaccine = Convert.ToString(dataReceive.Tables[0].Rows[index]["FluVaccine"]);
                        l_Assessment.FluVaccineDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["FluVaccine_Date"]);
                        l_Assessment.Tetanus = Convert.ToString(dataReceive.Tables[0].Rows[index]["Tetanus"]);
                        l_Assessment.TetanusDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["Tetanus_Date"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanImmunization |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }
        private static CarePlanInfectiousDiseasesModel GetCarePlanInfectiousDiseases(int p_CarePlanId)
        {
            string exception = string.Empty;
            CarePlanInfectiousDiseasesModel l_Assessment = new CarePlanInfectiousDiseasesModel();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_PlanOfCare_InfectiousDiseases", l_Conn);
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
                        l_Assessment.MRSA = Convert.ToString(dataReceive.Tables[0].Rows[index]["MRSA"]);
                        l_Assessment.MRSADiagnosedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["MRSA_Diagnosed_Date"]);
                        l_Assessment.MRSAResolvedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["MRSA_Resolved_Date"]);
                        l_Assessment.VRE = Convert.ToString(dataReceive.Tables[0].Rows[index]["VRE"]);
                        l_Assessment.VREDiagnosedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["VRE_Diagnosed_Date"]);
                        l_Assessment.VREResolvedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["VRE_Resolved_Date"]);
                        l_Assessment.CDiff = Convert.ToString(dataReceive.Tables[0].Rows[index]["CDiff"]);
                        l_Assessment.CDiffDiagnosedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["CDiff_Diagnosed_Date"]);
                        l_Assessment.CDiffResolvedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["CDiff_Resolved_Date"]);
                        l_Assessment.Other = Convert.ToString(dataReceive.Tables[0].Rows[index]["Other"]);
                        l_Assessment.OtherDiagnosedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["Other_Diagnosed_Date"]);
                        l_Assessment.OtherResolvedDate = Convert.ToString(dataReceive.Tables[0].Rows[index]["Other_Resolved_Date"]);
                    }
                }
                return l_Assessment;
            }
            catch (Exception ex)
            {
                exception = "CarePlanDAL GetCarePlanInfectiousDiseases |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

    }
}
 
 
 
 
 
 
 
 
 
 