using QolaMVC.Models;
using QolaMVC.ViewModels;
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

        public static void AddCarePlan(CarePlan_VitalSignModel model, string id)
        {
            string exception = string.Empty;
            var location = String.Join(",", model.Location);
            var Teeth = String.Join(",", model.Teeth);
            var Behaviour = String.Join(",", model.Behaviour);
            var Behaviour2 = String.Join(",", model.Behaviour2);
            var Behaviour3 = String.Join(",", model.Behaviour3);
            var CognitiveFunction = String.Join(",", model.CognitiveFunction);
            var Orientation = String.Join(",", model.Orientation);
            var Diet = String.Join(",", model.Diet);
            var Allergies = String.Join(",", model.Allergies);
            var Bladder = String.Join(",", model.Bladder);
            var Bowel = String.Join(",", model.Bowel);
            var ToiletingBathroom = String.Join(",", model.ToiletingBathroom);
            var ToiletingCommode = String.Join(",", model.ToiletingCommode);
            var ToiletingBedpan = String.Join(",", model.ToiletingBedpan);
            var MedicationAllergies = String.Join(",", model.MedicationAllergies);
            var OtherMedicationAllergies = String.Join(",", model.OtherMedicationAllergies);
            var SensoryAbilitiesHearing = String.Join(",", model.SensoryAbilitiesHearing);
            var SensoryAbilitiesVision = String.Join(",", model.SensoryAbilitiesVision);

            var Communication = String.Join(",", model.Communication);
            var SpecialEquipment = String.Join(",", model.SpecialEquipment);
            
            try
            {
                using (var connection = new SqlConnection(Constants.ConnectionString.PROD))
                {
                    var sqlAdapter = new SqlDataAdapter();
                    var sqlCommand = new SqlCommand(Constants.StoredProcedureName.USP_ADD_CARE_PLAN, connection);
                    connection.Open();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@AssessedStatus", model.AssessedStatus);
                    sqlCommand.Parameters.AddWithValue("@LevelOfCare", model.LevelOfCare);
                    sqlCommand.Parameters.AddWithValue("@Bpsystolic", model.Bpsystolic);
                    sqlCommand.Parameters.AddWithValue("@BpDiastolic",model.BpDiastolic);
                    sqlCommand.Parameters.AddWithValue("@BpDateCompleted", model.BpDateCompleted);
                    sqlCommand.Parameters.AddWithValue("@Temperature", model.Temperature);
                    sqlCommand.Parameters.AddWithValue("@TemperatureDateCompleted", model.TemperatureDateCompleted);
                    sqlCommand.Parameters.AddWithValue("@Weight_Lbs", model.Weight_Lbs);
                    sqlCommand.Parameters.AddWithValue("@WeightDateCompleted", model.WeightDateCompleted);
                    sqlCommand.Parameters.AddWithValue("@HeightCentimeters", model.HeightCentimeters);
                    sqlCommand.Parameters.AddWithValue("@HeightInches", model.HeightInches);
                    sqlCommand.Parameters.AddWithValue("@HeightDateCompleted", model.HeightDateCompleted);
                    sqlCommand.Parameters.AddWithValue("@Pulse", model.Pulse);
                    sqlCommand.Parameters.AddWithValue("@PulsedateCompleted", model.PulsedateCompleted);
                    sqlCommand.Parameters.AddWithValue("@Oxygen_02", model.Oxygen_02);
                    sqlCommand.Parameters.AddWithValue("@Oxygen_02_rate", model.Oxygen_02_rate);
                    sqlCommand.Parameters.AddWithValue("@ResidentId", id);
                    sqlCommand.Parameters.AddWithValue("@AmCare", model.AmCare);
                    sqlCommand.Parameters.AddWithValue("@AmCareAssistedBy", model.AmCareAssistedBy);
                    sqlCommand.Parameters.AddWithValue("@AmcareAgencyName", model.AmcareAgencyName);
                    sqlCommand.Parameters.AddWithValue("@AmCarePreferredTime", model.AmCarePreferredTime);
                    sqlCommand.Parameters.AddWithValue("@AmCarePreferredType", model.AmCarePreferredType);
                    sqlCommand.Parameters.AddWithValue("@PmCare", model.PmCare);
                    sqlCommand.Parameters.AddWithValue("@PmCareAssistedBy", model.PmCareAssistedBy);
                    sqlCommand.Parameters.AddWithValue("@PmcareAgencyNPme", model.PmcareAgencyNPme);
                    sqlCommand.Parameters.AddWithValue("@PmCarePreferredTime", model.PmCarePreferredTime);
                    sqlCommand.Parameters.AddWithValue("@PmCarePreferredType", model.PmCarePreferredType);
                    sqlCommand.Parameters.AddWithValue("@BathingCare", model.BathingCare);
                    sqlCommand.Parameters.AddWithValue("@BathingCareAssistedBy", model.BathingCareAssistedBy);
                    sqlCommand.Parameters.AddWithValue("@BathingcareAgencyNBathinge", model.BathingcareAgencyNBathinge);
                    sqlCommand.Parameters.AddWithValue("@BathingCarePreferredTime", model.BathingCarePreferredTime);
                    sqlCommand.Parameters.AddWithValue("@BathingCarePreferredType", model.BathingCarePreferredType);
                    sqlCommand.Parameters.AddWithValue("@CarePlanLocation", location);
                    sqlCommand.Parameters.AddWithValue("@Dressing", model.Dressing);
                    sqlCommand.Parameters.AddWithValue("@NailCare", model.NailCare);
                    sqlCommand.Parameters.AddWithValue("@Shaving", model.Shaving);
                    sqlCommand.Parameters.AddWithValue("@FootCare", model.FootCare);
                    sqlCommand.Parameters.AddWithValue("@DressingPreferredTime", model.DressingPreferredTime);
                    sqlCommand.Parameters.AddWithValue("@NailCarePreferredTime", model.NailCarePreferredTime);
                    sqlCommand.Parameters.AddWithValue("@ShavingPreferredTime", model.ShavingPreferredTime);
                    sqlCommand.Parameters.AddWithValue("@FootCarePreferredTime", model.FootCarePreferredTime);
                    sqlCommand.Parameters.AddWithValue("@OralHygiene", model.OralHygiene);
                    sqlCommand.Parameters.AddWithValue("@OralHygienePreferredTime", model.OralHygienePreferredTime);
                    sqlCommand.Parameters.AddWithValue("@Teeth", Teeth);
                    sqlCommand.Parameters.AddWithValue("@Mobility", model.Mobility);
                    sqlCommand.Parameters.AddWithValue("@Transfers", model.Transfers);
                    sqlCommand.Parameters.AddWithValue("@Mechanical_Left", model.MechanicalLeft);
                    sqlCommand.Parameters.AddWithValue("@Walker", model.Walker);
                    sqlCommand.Parameters.AddWithValue("@WheelChair", model.WheelChair);
                    sqlCommand.Parameters.AddWithValue("@Cane", model.Cane);
                    sqlCommand.Parameters.AddWithValue("@CarePlan_Left", model.Left);
                    sqlCommand.Parameters.AddWithValue("@Scooter", model.Scooter);
                    sqlCommand.Parameters.AddWithValue("@WalkerType", model.WalkerType);
                    sqlCommand.Parameters.AddWithValue("@WheelChairType", model.WheelChairType);
                    sqlCommand.Parameters.AddWithValue("@CaneType", model.CaneType);
                    sqlCommand.Parameters.AddWithValue("@ScooterType", model.ScooterType);
                    sqlCommand.Parameters.AddWithValue("@Pt", model.Pt);
                    sqlCommand.Parameters.AddWithValue("@PtFrequency", model.PtFrequency);
                    sqlCommand.Parameters.AddWithValue("@PtProvider", model.PtProvider);
                    sqlCommand.Parameters.AddWithValue("@Ot", model.Ot);
                    sqlCommand.Parameters.AddWithValue("@OtFrequency", model.OtFrequency);
                    sqlCommand.Parameters.AddWithValue("@OtProvider", model.OtProvider);
                    sqlCommand.Parameters.AddWithValue("@SafetyPasds", model.SafetyPasds);
                    sqlCommand.Parameters.AddWithValue("@Rails", model.Rails);
                    sqlCommand.Parameters.AddWithValue("@Other", model.Other);
                    sqlCommand.Parameters.AddWithValue("@LightOnly", model.LightOnly);
                    sqlCommand.Parameters.AddWithValue("@Lunch", model.Lunch);
                    sqlCommand.Parameters.AddWithValue("@Dinner", model.Dinner);
                    sqlCommand.Parameters.AddWithValue("@Breakfast", model.Breakfast);
                    sqlCommand.Parameters.AddWithValue("@Behaviour", Behaviour);
                    sqlCommand.Parameters.AddWithValue("@RiskOfHarmToSelf", model.RiskOfHarmToSelf);
                    sqlCommand.Parameters.AddWithValue("@Smoker", model.Smoker);
                    sqlCommand.Parameters.AddWithValue("@CognitiveStatus", model.CognitiveStatus);
                    sqlCommand.Parameters.AddWithValue("@RiskOfWandering", model.RiskOfWandering);
                    sqlCommand.Parameters.AddWithValue("@OtherPhysicalInfo", model.OtherPhysicalInfo);
                    sqlCommand.Parameters.AddWithValue("@CognitiveFunction", CognitiveFunction);
                    sqlCommand.Parameters.AddWithValue("@Orientation", Orientation);
                    sqlCommand.Parameters.AddWithValue("@NutritionStatus", model.NutritionStatus);
                    sqlCommand.Parameters.AddWithValue("@NutritionRisk", model.NutritionRisk);
                    sqlCommand.Parameters.AddWithValue("@AssistiveDevices", model.AssistiveDevices);
                    sqlCommand.Parameters.AddWithValue("@NutritionTexture", model.NutritionTexture);
                    sqlCommand.Parameters.AddWithValue("@NutritionOther", model.NutritionOther);
                    sqlCommand.Parameters.AddWithValue("@Diet", Diet);
                    sqlCommand.Parameters.AddWithValue("@OtherDiet", model.OtherDiet);
                    sqlCommand.Parameters.AddWithValue("@NutritionOtherPhysicalInfo", model.NutritionOtherPhysicalInfo);
                    sqlCommand.Parameters.AddWithValue("@Allergies", Allergies);
                    sqlCommand.Parameters.AddWithValue("@Appetite", model.Appetite);
                    sqlCommand.Parameters.AddWithValue("@MealsLunch", model.MealsLunch);
                    sqlCommand.Parameters.AddWithValue("@MealsDinner", model.MealsDinner);
                    sqlCommand.Parameters.AddWithValue("@MealsBreakfast", model.MealsBreakfast);
                    sqlCommand.Parameters.AddWithValue("@Bladder", Bladder);
                    sqlCommand.Parameters.AddWithValue("@Bowel", Bowel);
                    sqlCommand.Parameters.AddWithValue("@ContinenceProducts", model.ContinenceProducts);
                    sqlCommand.Parameters.AddWithValue("@ContinenceProductsName", model.ContinenceProductsName);
                    sqlCommand.Parameters.AddWithValue("@ContinenceProductsAssistiveDevices", model.ContinenceProductsAssistiveDevices);
                    sqlCommand.Parameters.AddWithValue("@ContinenceProductsSupplier", model.ContinenceProductsSupplier);
                    sqlCommand.Parameters.AddWithValue("@AssesmentBy", model.AssesmentBy);
                    sqlCommand.Parameters.AddWithValue("@DateCompleted", model.DateCompleted);
                    sqlCommand.Parameters.AddWithValue("@Toileting", model.Toileting);
                    sqlCommand.Parameters.AddWithValue("@ToiletingStatus", model.ToiletingStatus);
                    sqlCommand.Parameters.AddWithValue("@MedicationAssistace", model.MedicationAssistace);
                    sqlCommand.Parameters.AddWithValue("@MedicationAdministration", model.MedicationAdministration);
                    sqlCommand.Parameters.AddWithValue("@MedicationCOmpletedBy", model.MedicationCOmpletedBy);
                    sqlCommand.Parameters.AddWithValue("@MedicationAgency", model.MedicationAgency);
                    sqlCommand.Parameters.AddWithValue("@MedicationPharmacy", model.MedicationPharmacy);
                    sqlCommand.Parameters.AddWithValue("@MedicationAllergies", MedicationAllergies);
                    sqlCommand.Parameters.AddWithValue("@OtherMedicationAllergies", OtherMedicationAllergies);
                    sqlCommand.Parameters.AddWithValue("@MedicationTape", model.MedicationTape);
                    sqlCommand.Parameters.AddWithValue("@MedicationHydantoins", model.MedicationHydantoins);
                    sqlCommand.Parameters.AddWithValue("@SensoryAbilitiesHearing", SensoryAbilitiesHearing);
                    sqlCommand.Parameters.AddWithValue("@SensoryAbilitiesVision", SensoryAbilitiesVision);
                    sqlCommand.Parameters.AddWithValue("@Language", model.Language);
                    sqlCommand.Parameters.AddWithValue("@Communication", Communication);
                    sqlCommand.Parameters.AddWithValue("@CommunicationNotes", model.CommunicationNotes);
                    sqlCommand.Parameters.AddWithValue("@WondCare", model.WondCare);
                    sqlCommand.Parameters.AddWithValue("@WondCareAssistedBy", model.WondCareAssistedBy);
                    sqlCommand.Parameters.AddWithValue("@WondCareAgencyName", model.WondCareAgencyName);
                    sqlCommand.Parameters.AddWithValue("@SkinCare", model.SkinCare);
                    sqlCommand.Parameters.AddWithValue("@SkinCareTreatments", model.SkinCareTreatments);
                    sqlCommand.Parameters.AddWithValue("@SpecialNeedSupplier", model.SpecialNeedSupplier);
                    sqlCommand.Parameters.AddWithValue("@SpecialNeedNotes", model.SpecialNeedNotes);
                    sqlCommand.Parameters.AddWithValue("@SpecialNeedCpap", model.SpecialNeedCpap);
                    sqlCommand.Parameters.AddWithValue("@SpecialEquipment", SpecialEquipment);
                    sqlCommand.Parameters.AddWithValue("@SpecialEquipmentOtherMentalInfo", model.SpecialEquipmentOtherMentalInfo);
                    sqlCommand.Parameters.AddWithValue("@SpecialEquipmentOther", model.SpecialEquipmentOther);
                    sqlCommand.Parameters.AddWithValue("@ResidentFamilyMeeting", model.ResidentFamilyMeeting);
                    sqlCommand.Parameters.AddWithValue("@ResidentFamilyInvolvement", model.ResidentFamilyInvolvement);
                    sqlCommand.Parameters.AddWithValue("@Mantoux", model.Mantoux);
                    sqlCommand.Parameters.AddWithValue("@ChestXray", model.ChestXray);
                    sqlCommand.Parameters.AddWithValue("@Pneumonia", model.Pneumonia);
                    sqlCommand.Parameters.AddWithValue("@FluVaccine", model.FluVaccine);
                    sqlCommand.Parameters.AddWithValue("@Tetanus", model.Tetanus);
                    sqlCommand.Parameters.AddWithValue("@MantouxDateComplete", model.MantouxDateComplete);
                    sqlCommand.Parameters.AddWithValue("@ChestXrayDateComplete", model.ChestXrayDateComplete);
                    sqlCommand.Parameters.AddWithValue("@PneumoniaDateComplete", model.PneumoniaDateComplete);
                    sqlCommand.Parameters.AddWithValue("@FluVaccineDateComplete", model.FluVaccineDateComplete);
                    sqlCommand.Parameters.AddWithValue("@TetanusDateComplete", model.TetanusDateComplete);

                    sqlCommand.ExecuteNonQuery();

                }
            }
            
            catch (Exception ex)
            {
                exception = "AddCarePlan |" + ex.ToString();
                //Log.Write(exception);
                throw;
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


        public static Collection<PostFallClinicalMonitoringViewModel> GetPostFall(int? residentId, string category, string date_created)
        {
            string exception = string.Empty;
            var postFalldetails = new Collection<PostFallClinicalMonitoringViewModel>();
            return postFalldetails;
            //using (var connection = new SqlConnection(Constants.ConnectionString.PROD))
            //{

            //    try
            //    {
            //        var sqlAdapter = new SqlDataAdapter();
            //        //var reader
            //        var sqlCommand = new SqlCommand(Constants.StoredProcedureName.USP_GetPostFall, connection);
            //        sqlCommand.CommandType = CommandType.StoredProcedure;
            //        connection.Open();
            //        sqlCommand.Parameters.AddWithValue("@Id", residentId);
            //        sqlCommand.Parameters.AddWithValue("@category", category);
            //        sqlCommand.Parameters.AddWithValue("@date_created", date_created);
            //        DataSet getData = new DataSet();
            //        sqlAdapter.SelectCommand = sqlCommand;
            //        sqlAdapter.Fill(getData);
            //        if (getData != null && getData.Tables.Count > 0)
            //        {
            //            for (int i = 0; i <= getData.Tables[0].Rows.Count - 1; i++)
            //            {
            //                var postFallDetail = new PostFallClinicalMonitoringViewModel();
            //                postFallDetail.residentid = Convert.ToString(getData.Tables[0].Rows[i]["residentid"]);
            //                postFallDetail.vitalsign = Convert.ToString(getData.Tables[0].Rows[i]["vitalsign"]);
            //                postFallDetail.date_created = Convert.ToString(getData.Tables[0].Rows[i]["date_created"]);
            //                postFallDetail.category = Convert.ToString(getData.Tables[0].Rows[i]["category"]);
            //                postFallDetail.firstcheck = Convert.ToString(getData.Tables[0].Rows[i]["firstcheck"]);
            //                postFallDetail.onehourfirstcheck = Convert.ToString(getData.Tables[0].Rows[i]["onehourfirstcheck"]);
            //                postFallDetail.onehoursecondcheck = Convert.ToString(getData.Tables[0].Rows[i]["onehoursecondcheck"]);
            //                postFallDetail.threehoursfirstcheck = Convert.ToString(getData.Tables[0].Rows[i]["threehoursfirstcheck"]);
            //                postFallDetail.threehourssecondcheck = Convert.ToString(getData.Tables[0].Rows[i]["threehourssecondcheck"]);
            //                postFallDetail.threehoursthirdcheck = Convert.ToString(getData.Tables[0].Rows[i]["threehoursthirdcheck"]);
            //                postFallDetail.fourtyeighthoursfirstcheck = Convert.ToString(getData.Tables[0].Rows[i]["fourtyeighthoursfirstcheck"]);
            //                postFallDetail.fourtyeighthourssecondcheck = Convert.ToString(getData.Tables[0].Rows[i]["fourtyeighthourssecondcheck"]);
            //                postFallDetail.fourtyeighthoursthirdcheck = Convert.ToString(getData.Tables[0].Rows[i]["fourtyeighthoursthirdcheck"]);
            //                postFallDetail.fourtyeighthoursfourthcheck = Convert.ToString(getData.Tables[0].Rows[i]["fourtyeighthoursfourthcheck"]);
            //                postFallDetail.fourtyeighthoursfifthcheck = Convert.ToString(getData.Tables[0].Rows[i]["fourtyeighthoursfifthcheck"]);
            //                postFalldetails.Add(postFallDetail);
            //                //var a  = Convert.ToString(getData.Tables[0].Rows[i]["residentid"]);
            //                //postFalldetails.onehourfirstcheck[i] = Convert.ToString(getData.Tables[0].Rows[i]["onehourfirstcheck"]);

            //            }
            //        }
                    
            //        return postFalldetails;
            //    }
            //    catch (Exception ex)
            //    {
            //        exception = "PostFallClinicalMonitoring |" + ex.ToString();
            //        //Log.Write(exception);
            //        throw;
            //    }
            //    finally
            //    {
            //        connection.Close();
            //    }
            //}
        }

        public static CarePlan_VitalSignModel GetCarePlan(string id)
        {
            string exception = string.Empty;
            try
            {
                var CarePlans = new Collection<CarePlan_VitalSignModel>();

                using (var connection = new SqlConnection(Constants.ConnectionString.PROD))
                {
                    var sqlAdapter = new SqlDataAdapter();
                    var sqlCommand = new SqlCommand(Constants.StoredProcedureName.USP_GET_CARE_PLAN, connection);
                    connection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ResidentId", id);

                    DataSet getData = new DataSet();

                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(getData);

                    if ((getData != null) & getData.Tables.Count > 0)
                    {
                        for (int index = 0; index <= getData.Tables[0].Rows.Count - 1; index++)
                        {
                            var plan = new CarePlan_VitalSignModel();
                            plan.Id = Convert.ToInt32(getData.Tables[0].Rows[index]["Id"]);
                            //plan.EnteredBy = Convert.ToInt32(getData.Tables[0].Rows[index]["EnteredBy"]);
                            plan.AssessedStatus = Convert.ToString(getData.Tables[0].Rows[index]["AssessedStatus"]);
                            plan.LevelOfCare = Convert.ToString(getData.Tables[0].Rows[index]["LevelOfCare"]);
                            plan.Bpsystolic = Convert.ToString(getData.Tables[0].Rows[index]["Bpsystolic"]);
                            plan.BpDiastolic = Convert.ToString(getData.Tables[0].Rows[index]["BpDiastolic"]);
                            plan.BpDateCompleted = Convert.ToString(getData.Tables[0].Rows[index]["BpDateCompleted"]);
                            plan.Temperature = Convert.ToString(getData.Tables[0].Rows[index]["Temperature"]);
                            plan.TemperatureDateCompleted = Convert.ToString(getData.Tables[0].Rows[index]["TemperatureDateCompleted"]);
                            plan.Weight_Lbs = Convert.ToString(getData.Tables[0].Rows[index]["Weight_Lbs"]);
                            plan.WeightDateCompleted = Convert.ToString(getData.Tables[0].Rows[index]["WeightDateCompleted"]);
                            plan.HeightCentimeters = Convert.ToString(getData.Tables[0].Rows[index]["HeightCentimeters"]);
                            plan.HeightInches = Convert.ToString(getData.Tables[0].Rows[index]["HeightInches"]);
                            plan.HeightDateCompleted = Convert.ToString(getData.Tables[0].Rows[index]["HeightDateCompleted"]);
                            plan.Pulse = Convert.ToString(getData.Tables[0].Rows[index]["Pulse"]);
                            plan.PulsedateCompleted = Convert.ToString(getData.Tables[0].Rows[index]["PulsedateCompleted"]);
                            plan.Oxygen_02 = Convert.ToString(getData.Tables[0].Rows[index]["Oxygen_02"]);
                            plan.Oxygen_02_rate = Convert.ToString(getData.Tables[0].Rows[index]["Oxygen_02_rate"]); ;
                            plan.ResidentId = Convert.ToString(getData.Tables[0].Rows[index]["ResidentId"]);
                            CarePlans.Add(plan);
                        }
                    }

                    return CarePlans.ToList().OrderByDescending(m => m.Id).FirstOrDefault();
                }
            }

            catch (Exception ex)
            {
                exception = "GetCarePlan |" + ex.ToString();
                //Log.Write(exception);
                throw;
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

        //SE add postfallclinicalmonitoring assessment
        public static PostFallClinicalMonitoringModel AddPostFall(PostFallClinicalMonitoringModel model, VitalSignsModel vitalSigns, int? Id)
        {
            string exception = string.Empty;

            using (var connection = new SqlConnection(Constants.ConnectionString.PROD))
            {

                try
                {
                    var sqlAdapter = new SqlDataAdapter();
                    var sqlCommand = new SqlCommand(Constants.StoredProcedureName.USP_AddPostFall, connection);
                    connection.Open();
                    for (int i = 0; i < model.firstcheck.Count(); i++)
                    {
                        var vitalSignId = AddVitalSign(vitalSigns, i);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@firstcheck", model.firstcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@onehourfirstcheck", model.onehourfirstcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@onehoursecondcheck", model.onehoursecondcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@threehoursfirstcheck", model.threehoursfirstcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@threehourssecondcheck", model.threehourssecondcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@threehoursthirdcheck", model.threehoursthirdcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@fourtyeighthoursfirstcheck", model.fourtyeighthoursfirstcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@fourtyeighthourssecondcheck", model.fourtyeighthourssecondcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@fourtyeighthoursthirdcheck", model.fourtyeighthoursthirdcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@fourtyeighthoursfourthcheck", model.fourtyeighthoursfourthcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@fourtyeighthoursfifthcheck", model.fourtyeighthoursfifthcheck[i]);
                        sqlCommand.Parameters.AddWithValue("@category", model.category);
                        sqlCommand.Parameters.AddWithValue("@vitalsign", vitalSignId);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();
                    }
                    return model;
                }
                catch (Exception ex)
                {
                    exception = "PostFallClinicalMonitoring |" + ex.ToString();
                    //Log.Write(exception);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static string AddVitalSign(VitalSignsModel model, int index)
        {
            string exception = string.Empty;
            using (var connection = new SqlConnection(Constants.ConnectionString.PROD))
            {
                try
                {

                    model.date_created = DateTime.Now.ToShortDateString();
                    var sqlAdapter = new SqlDataAdapter();
                    var sqlCommand = new SqlCommand(Constants.StoredProcedureName.USP_AddPostFallVitalSign, connection);
                    connection.Open();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    
                    sqlCommand.Parameters.AddWithValue("@residentid", "1234");
                    sqlCommand.Parameters.AddWithValue("@vitalsign", model.vitalsign[index]);
                    sqlCommand.Parameters.AddWithValue("@category", model.category);
                    sqlCommand.Parameters.AddWithValue("@date_created", model.date_created);
                    //SqlParameter myid = new SqlParameter("@myid", SqlDbType.Int);
                    //myid.Direction = ParameterDirection.Output;
                    //sqlCommand.Parameters.Add(myid);
                    sqlCommand.Parameters.Add("@myid", SqlDbType.Int).Direction = ParameterDirection.Output;

                    DataSet getData = new DataSet();
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlAdapter.Fill(getData);

                    if (getData != null && getData.Tables.Count > 0)
                    {
                        return Convert.ToString(getData.Tables[0].Rows[0]["Id"].ToString());
                    }
                    else
                    {
                        //sqlCommand.ExecuteNonQuery();

                        string id = sqlCommand.Parameters["@myid"].Value.ToString();
                        return id;
                    }

                }
                catch (Exception ex)
                {
                    exception = "PostFallClinicalMonitoringVitalSigns |" + ex.ToString();
                    //Log.Write(exception);
                    throw;
                }

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
               // l_Cmd.Parameters.AddWithValue("@PulpilsEquals", p_Model.PulpilsEquals);
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
                        l_AdmissionHeadToToe.AdmissionStatus = Convert.ToString(dataReceive.Tables[0].Rows[index]["AdmissionStatus"]);
                        //l_FamilyConferenceNote.DOB = Convert.ToString(dataReceive.Tables[0].Rows[index]["Diet"]);
                        l_AdmissionHeadToToe.ReturnedFromHospital = Convert.ToString(dataReceive.Tables[0].Rows[index]["ReturnedFromHospital"]);
                        l_AdmissionHeadToToe.DiagnosisFromHospital = Convert.ToString(dataReceive.Tables[0].Rows[index]["DiagnosisFromHospital"]);
                        l_AdmissionHeadToToe.Medications = Convert.ToString(dataReceive.Tables[0].Rows[index]["Medications"]);
                        l_AdmissionHeadToToe.BP = Convert.ToString(dataReceive.Tables[0].Rows[index]["BP"]);
                        l_AdmissionHeadToToe.BPLocation = Convert.ToString(dataReceive.Tables[0].Rows[index]["BPLocation"]);
                        l_AdmissionHeadToToe.RedialPulse = Convert.ToString(dataReceive.Tables[0].Rows[index]["RedialPulse"]);
                        l_AdmissionHeadToToe.PulseLocation = Convert.ToString(dataReceive.Tables[0].Rows[index]["PulseLocation"]);
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

        public static void AddExcerciseActivityDetail(ExcerciseActivityDetailModel p_Model)
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
                        l_Model.Friday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["Friday"]);
                        l_Model.Saturday = Convert.ToBoolean(dataReceive.Tables[0].Rows[index]["Saturday"]);
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

        public static Collection<HSEPDetailModel> GetHSEPDetail(int p_ResidentId)
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
                        l_Model.DateOfTeaching = Convert.ToString(dataReceive.Tables[0].Rows[index]["DateOfTeaching"]);
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
                return l_Models;
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

        public static void AddDietaryAssesment(nDietaryAssessmentModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_DietaryAssessment", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Residentid", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@Likes", p_Model.Likes);
                l_Cmd.Parameters.AddWithValue("@DisLikes", p_Model.DisLikes);
                l_Cmd.Parameters.AddWithValue("@Enteredby", p_Model.EnteredBy.ID);
                l_Cmd.Parameters.AddWithValue("@Notes", p_Model.Notes);
                l_Cmd.Parameters.AddWithValue("@AssistiveDevices", p_Model.AssistiveDevices);
                l_Cmd.Parameters.AddWithValue("@Risk", p_Model.Risk);
                l_Cmd.Parameters.AddWithValue("@Texture", p_Model.Texture);

                l_Cmd.Parameters.AddWithValue("@Other", p_Model.Other);
                l_Cmd.Parameters.AddWithValue("@NutritionalStatus", p_Model.NutritionalStatus);
                l_Cmd.Parameters.AddWithValue("@Apetite", p_Model.Apetite);
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

                    foreach(var diet in p_Model.Diet)
                    {
                        SqlCommand l_Cmd_Diet = new SqlCommand("spAB_Add_DietaryAssessment_Diets", l_Conn);
                       // l_Conn.Open();
                        l_Cmd_Diet.CommandType = System.Data.CommandType.StoredProcedure;
                        l_Cmd_Diet.Parameters.AddWithValue("@Residentid", p_Model.Resident.ID);
                        l_Cmd_Diet.Parameters.AddWithValue("@Diet", diet);
                        l_Cmd_Diet.Parameters.AddWithValue("@AssessmentId", l_AssessmentId);
                        l_Cmd_Diet.Parameters.AddWithValue("@Enteredby", p_Model.EnteredBy.ID);
                        l_Cmd_Diet.ExecuteNonQuery();
                    }

                    foreach (var allergy in p_Model.Allergies)
                    {
                        SqlCommand l_Cmd_Diet = new SqlCommand("spAB_Add_DietaryAssessment_Allergies", l_Conn);
                      //  l_Conn.Open();
                        l_Cmd_Diet.CommandType = System.Data.CommandType.StoredProcedure;
                        l_Cmd_Diet.Parameters.AddWithValue("@Residentid", p_Model.Resident.ID);
                        l_Cmd_Diet.Parameters.AddWithValue("@Allergy", allergy.Name);
                        l_Cmd_Diet.Parameters.AddWithValue("@AllergyId", allergy.ID);
                        l_Cmd_Diet.Parameters.AddWithValue("@AssessmentId", l_AssessmentId);
                        l_Cmd_Diet.Parameters.AddWithValue("@Enteredby", p_Model.EnteredBy.ID);
                        l_Cmd_Diet.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                exception = "AddDieterayAssesment |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<AllergiesModel> GetAllergiesCollections()
        {
            string exception = string.Empty;
            
            Collection<AllergiesModel> allergiess = new Collection<AllergiesModel>();
            AllergiesModel allergies;
            ResidentModel l_Resident = new ResidentModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand(Constants.StoredProcedureName.USP_GET_ALLERGIES, l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet AllergiesReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AllergiesReceive);
                if (AllergiesReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= AllergiesReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        allergies = new AllergiesModel();
                        allergies.ID = Convert.ToInt32(AllergiesReceive.Tables[0].Rows[index]["fd_id"]);
                        allergies.Name = Convert.ToString(AllergiesReceive.Tables[0].Rows[index]["fd_name"]);
                        allergies.Catogery = Convert.ToInt32(AllergiesReceive.Tables[0].Rows[index]["fd_category"]);
                        allergiess.Add(allergies);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = "GetAllergiesCollections |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            return allergiess;
        }

        public static Collection<nDietaryAssessmentModel> GetResidentDietaryAssesments(int p_ResidentId)
        {
            string exception = string.Empty;

            Collection<nDietaryAssessmentModel> l_Assessments = new Collection<nDietaryAssessmentModel>();
            nDietaryAssessmentModel l_Assessment;
            AllergiesModel l_Allergy;
            ResidentModel l_Resident = new ResidentModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Resident_DietaryAssessments", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                DataSet AssesmentsReceive = new DataSet();
                
                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= AssesmentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment = new nDietaryAssessmentModel();
                        l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Id"]);
                        l_Resident.ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.NutritionalStatus = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["NutritionalStatus"]);
                        l_Assessment.Risk = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Risk"]);
                        l_Assessment.AssistiveDevices = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["AssistiveDevices"]);
                        l_Assessment.Texture = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Texture"]);

                        l_Assessment.Other = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Other"]);
                        l_Assessment.Likes = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Likes"]);
                        l_Assessment.DisLikes = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DisLikes"]);
                        l_Assessment.Notes = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Notes"]);
                        l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["DateEntered"]);

                        SqlDataAdapter l_DA_Diets = new SqlDataAdapter();
                        SqlCommand l_Cmd_Diets = new SqlCommand("spAB_Get_Resident_DietaryAssessmentDiets", l_Conn);
                        l_Cmd_Diets.CommandType = System.Data.CommandType.StoredProcedure;
                        l_Cmd_Diets.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                        l_Cmd_Diets.Parameters.AddWithValue("@AssessmentId", l_Assessment.Id);
                        DataSet DietsReceive = new DataSet();

                        l_DA_Diets.SelectCommand = l_Cmd_Diets;
                        l_DA_Diets.Fill(DietsReceive);
                        l_Assessment.Diet = new Collection<string>();
                        if (DietsReceive.Tables[0].Rows.Count > 0)
                        {
                            for (int index_Diets = 0; index_Diets <= DietsReceive.Tables[0].Rows.Count - 1; index_Diets++)
                            {
                                l_Assessment.Diet.Add(Convert.ToString(DietsReceive.Tables[0].Rows[index_Diets]["Diet"]));
                            }
                        }

                        SqlDataAdapter l_DA_Allergies = new SqlDataAdapter();
                        SqlCommand l_Cmd_Allergies = new SqlCommand("spAB_Get_Resident_DietaryAssessmentAllergies", l_Conn);
                        l_Cmd_Allergies.CommandType = System.Data.CommandType.StoredProcedure;
                        l_Cmd_Allergies.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                        l_Cmd_Allergies.Parameters.AddWithValue("@AssessmentId", l_Assessment.Id);
                        DataSet AllergyReceive = new DataSet();
                        l_Assessment.Allergies = new Collection<AllergiesModel>();

                        l_DA_Allergies.SelectCommand = l_Cmd_Allergies;
                        l_DA_Allergies.Fill(AllergyReceive);
                        if (AllergyReceive.Tables[0].Rows.Count > 0)
                        {
                            for (int index_Diets = 0; index_Diets <= AllergyReceive.Tables[0].Rows.Count - 1; index_Diets++)
                            {
                                l_Allergy = new AllergiesModel();

                                l_Allergy.ID = Convert.ToInt32(AllergyReceive.Tables[0].Rows[index_Diets]["AllergyId"]);
                                l_Allergy.Name = Convert.ToString(AllergyReceive.Tables[0].Rows[index_Diets]["Allergy"]);

                                l_Assessment.Allergies.Add(l_Allergy);
                            }
                        }
                        l_Assessments.Add(l_Assessment);
                    }

                }
            }
            catch (Exception ex)
            {
                exception = "GetResidentDietaryAssesments |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            return l_Assessments;
        }

        public static void AddUnusualIncident(UnusualIncidentModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_UnusualIncident", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@strLocation", p_Model.Location);
                l_Cmd.Parameters.AddWithValue("@Employee", p_Model.Employee);
                l_Cmd.Parameters.AddWithValue("@Dept", p_Model.Dept);
                l_Cmd.Parameters.AddWithValue("@Visitor", p_Model.Visitor);
                l_Cmd.Parameters.AddWithValue("@Room", p_Model.Room);
                l_Cmd.Parameters.AddWithValue("@Other", p_Model.Other);
                l_Cmd.Parameters.AddWithValue("@WasWitnessed", p_Model.WasWitnessed);
                l_Cmd.Parameters.AddWithValue("@WitnessName", p_Model.WitnessName);
                l_Cmd.Parameters.AddWithValue("@IsFall", p_Model.IsFall);
                l_Cmd.Parameters.AddWithValue("@IsElopement", p_Model.IsElopement);
                l_Cmd.Parameters.AddWithValue("@ElopementValue", p_Model.Elopement);
                l_Cmd.Parameters.AddWithValue("@IsUnusualBehavior", p_Model.IsUnusualBehaviour);
                l_Cmd.Parameters.AddWithValue("@UnusualBehaviorvalue", p_Model.UnusualBehaviour);
                l_Cmd.Parameters.AddWithValue("@IsPhysicalInjury", p_Model.IsPhysicalInjury);
                l_Cmd.Parameters.AddWithValue("@PhysicalInjuryValue", p_Model.PhysicalInjury);
                l_Cmd.Parameters.AddWithValue("@IsPropertyLoss", p_Model.IsPropertyLoss);
                l_Cmd.Parameters.AddWithValue("@PropertyLossValue", p_Model.PropertyLoss);
                l_Cmd.Parameters.AddWithValue("@IsSuspicious", p_Model.IsSuspicious);
                l_Cmd.Parameters.AddWithValue("@SuspicionValue", p_Model.Suspicion);
                l_Cmd.Parameters.AddWithValue("@IsTreatment", p_Model.IsTreatment);
                l_Cmd.Parameters.AddWithValue("@TreatmentValue", p_Model.Treatment);
                l_Cmd.Parameters.AddWithValue("@IsOther", p_Model.IsOther);
                l_Cmd.Parameters.AddWithValue("@SectionD", p_Model.SectionD);
                l_Cmd.Parameters.AddWithValue("@SectionE", p_Model.SectionE);
                l_Cmd.Parameters.AddWithValue("@SectionF", p_Model.SectionF);
                l_Cmd.Parameters.AddWithValue("@SectionH", p_Model.SectionH);
                l_Cmd.Parameters.AddWithValue("@IncidentDocumented", p_Model.IncidentDocumented);
                l_Cmd.Parameters.AddWithValue("@ChangesMade", p_Model.ChangesMade);
                l_Cmd.Parameters.AddWithValue("@ReferralConsult", p_Model.ReferralConsult);
                l_Cmd.Parameters.AddWithValue("@OHSCommitteeInformed", p_Model.OHSCommitteeInformed);
                l_Cmd.Parameters.AddWithValue("@RecordTrackingForm", p_Model.RecordTrackingForm);

                l_Cmd.Parameters.AddWithValue("@IncidentInformation", p_Model.IncidentInformation);
                l_Cmd.Parameters.AddWithValue("@SectionJ", p_Model.SectionJ);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);

                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);
                int l_IncidentId = 0;
                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_IncidentId = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                    }

                    if(p_Model.SectionG != null)
                    {
                        foreach (var sectionG in p_Model.SectionG)
                        {
                            SqlCommand l_Cmd_SectionG = new SqlCommand("spAB_Add_UnusualIncident_SectionG", l_Conn);
                            // l_Conn.Open();
                            l_Cmd_SectionG.CommandType = System.Data.CommandType.StoredProcedure;
                            l_Cmd_SectionG.Parameters.AddWithValue("@Residentid", p_Model.Resident.ID);
                            l_Cmd_SectionG.Parameters.AddWithValue("@IncidentId", l_IncidentId);
                            l_Cmd_SectionG.Parameters.AddWithValue("@Notify", sectionG.Notify);
                            l_Cmd_SectionG.Parameters.AddWithValue("@Name", sectionG.Name);
                            l_Cmd_SectionG.Parameters.AddWithValue("@Date", sectionG.Date);
                            l_Cmd_SectionG.Parameters.AddWithValue("@ByWhom", sectionG.ByWhom);
                            l_Cmd_SectionG.Parameters.AddWithValue("@Via", sectionG.Via);
                            l_Cmd_SectionG.Parameters.AddWithValue("@Enteredby", p_Model.EnteredBy.ID);
                            l_Cmd_SectionG.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = "AddUnusualIncident |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<UnusualIncidentModel> GetUnusualIncidentReports(int p_ResidentId)
        {
            string exception = string.Empty;

            Collection<UnusualIncidentModel> l_Assessments = new Collection<UnusualIncidentModel>();
            UnusualIncidentModel l_Assessment;

            UnusualIncidentSectionGModel l_SectionG;
            ResidentModel l_Resident = new ResidentModel();
            UserModel l_User = new UserModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_UnusualIncident", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= AssesmentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment = new UnusualIncidentModel();
                        l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Id"]);
                        l_Resident.ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.Location = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["strLocation"]);
                        l_Assessment.Employee = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Employee"]);
                        l_Assessment.Dept = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Dept"]);
                        l_Assessment.Visitor = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Visitor"]);
                        l_Assessment.Other = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Other"]);
                        l_Assessment.Room = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Room"]);
                        l_Assessment.WasWitnessed = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["WasWitnessed"]);
                        l_Assessment.WitnessName = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["WitnessName"]);
                        l_Assessment.IsFall = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsFall"]);
                        l_Assessment.IsElopement = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsElopement"]);
                        l_Assessment.Elopement = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ElopementValue"]);
                        l_Assessment.IsUnusualBehaviour = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsUnusualBehavior"]);
                        l_Assessment.UnusualBehaviour = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["UnusualBehaviorvalue"]);
                        l_Assessment.IsPhysicalInjury = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsPhysicalInjury"]);
                        l_Assessment.PhysicalInjury = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["PhysicalInjuryValue"]);
                        l_Assessment.IsPropertyLoss = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsPropertyLoss"]);
                        l_Assessment.PropertyLoss = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["PropertyLossValue"]);
                        l_Assessment.IsSuspicious = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsSuspicious"]);
                        l_Assessment.Suspicion = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SuspicionValue"]);
                        l_Assessment.IsTreatment = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsTreatment"]);
                        l_Assessment.Treatment = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["TreatmentValue"]);
                        l_Assessment.IsOther = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["IsOther"]);
                        l_Assessment.SectionD = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SectionD"]);
                        l_Assessment.SectionE = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SectionE"]);
                        l_Assessment.SectionF = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SectionF"]);
                        l_Assessment.SectionH = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SectionH"]);
                        l_Assessment.IncidentDocumented = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["IncidentDocumented"]);
                        l_Assessment.ChangesMade = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ChangesMade"]);
                        l_Assessment.ReferralConsult = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ReferralConsult"]);
                        l_Assessment.OHSCommitteeInformed = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["OHSCommitteeInformed"]);
                        l_Assessment.RecordTrackingForm = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["RecordTrackingForm"]);
                        l_Assessment.IncidentInformation = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["IncidentInformation"]);
                        l_Assessment.SectionJ = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SectionJ"]);
                        l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["DateEntered"]);

                        l_User.ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_Assessment.EnteredBy = l_User;

                        SqlDataAdapter l_DA_SectionG = new SqlDataAdapter();
                        SqlCommand l_Cmd_SectionG = new SqlCommand("spAB_Get_UnusualIncident_SectionG", l_Conn);
                        l_Cmd_SectionG.CommandType = System.Data.CommandType.StoredProcedure;
                        l_Cmd_SectionG.Parameters.AddWithValue("@IncidentId", l_Assessment.Id);
                        DataSet SectionGReceive = new DataSet();

                        l_DA_SectionG.SelectCommand = l_Cmd_SectionG;
                        l_DA_SectionG.Fill(SectionGReceive);
                        l_Assessment.SectionG = new Collection<UnusualIncidentSectionGModel>();
                        if (SectionGReceive.Tables[0].Rows.Count > 0)
                        {
                            for (int index_Diets = 0; index_Diets <= SectionGReceive.Tables[0].Rows.Count - 1; index_Diets++)
                            {
                                l_SectionG = new UnusualIncidentSectionGModel();

                                l_SectionG.IncidentId = l_Assessment.Id;
                                l_SectionG.Notify = Convert.ToString(SectionGReceive.Tables[0].Rows[index_Diets]["Notify"]);
                                l_SectionG.Name = Convert.ToString(SectionGReceive.Tables[0].Rows[index_Diets]["strName"]);
                                l_SectionG.Date = Convert.ToString(SectionGReceive.Tables[0].Rows[index_Diets]["dtmDate"]);
                                l_SectionG.ByWhom = Convert.ToString(SectionGReceive.Tables[0].Rows[index_Diets]["ByWhom"]);
                                l_SectionG.Via = Convert.ToString(SectionGReceive.Tables[0].Rows[index_Diets]["Via"]);
                                l_SectionG.EnteredBy = Convert.ToInt32(SectionGReceive.Tables[0].Rows[index_Diets]["EnteredBy"]);
                                l_SectionG.DateEntered = Convert.ToDateTime(SectionGReceive.Tables[0].Rows[index_Diets]["DateEntered"]);

                                l_Assessment.SectionG.Add(l_SectionG);
                            }
                        }

                        l_Assessments.Add(l_Assessment);
                    }

                }
            }
            catch (Exception ex)
            {
                exception = "GetUnusualIncidentReports |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            return l_Assessments;
        }

    }
}