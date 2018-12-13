﻿using QolaMVC.Models;
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

        public static void AddNewBowelMovement_mike_not_in_use(BowelMovementModel p_BowelMovement,int residentid,int userid)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                //SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("check_BowelMovement_mike", l_Conn);
                l_Conn.Open();
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@datestr", date);
                l_Cmd.Parameters.AddWithValue("@firstday", firstDayOfMonth);
                l_Cmd.Parameters.AddWithValue("@lastday", lastDayOfMonth);
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@userid", userid);
                l_Cmd.ExecuteNonQuery();

                string storePorcedure = "spAB_AddBowelMovement_mike" + p_BowelMovement.Period;
                SqlCommand l_Cmd2 = new SqlCommand(storePorcedure, l_Conn);
                l_Cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd2.Parameters.AddWithValue("@datestr", date);
                l_Cmd.Parameters.AddWithValue("@strType", p_BowelMovement.Type);
                l_Cmd.Parameters.AddWithValue("@ObservedBy", p_BowelMovement.ObservedBy);
                l_Cmd.Parameters.AddWithValue("@Initials", p_BowelMovement.Initials);
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", userid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddNewBowelMovement_mike |" + ex.ToString();
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

                        l_FamilyConferenceNote.onlyCare = Convert.ToString(dataReceive.Tables[0].Rows[index]["onlyCare"]);
                        l_FamilyConferenceNote.Goalsmet = Convert.ToString(dataReceive.Tables[0].Rows[index]["Goalsmet"]);
                        l_FamilyConferenceNote.row1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row1"]);
                        l_FamilyConferenceNote.row2 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row2"]);
                        l_FamilyConferenceNote.row3 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row3"]);
                        l_FamilyConferenceNote.row4 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row4"]);
                        l_FamilyConferenceNote.row5 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row5"]);
                        l_FamilyConferenceNote.row6 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row6"]);
                        l_FamilyConferenceNote.row7 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row7"]);
                        l_FamilyConferenceNote.row8 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row8"]);
                        l_FamilyConferenceNote.row9 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row9"]);
                        l_FamilyConferenceNote.row10 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row10"]);
                        l_FamilyConferenceNote.row11 = Convert.ToString(dataReceive.Tables[0].Rows[index]["row11"]);
                        l_FamilyConferenceNote.droprow1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow1"]);
                        l_FamilyConferenceNote.droprow2 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow2"]);
                        l_FamilyConferenceNote.droprow3 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow3"]);
                        l_FamilyConferenceNote.droprow4 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow4"]);
                        l_FamilyConferenceNote.droprow5 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow5"]);
                        l_FamilyConferenceNote.droprow6 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow6"]);
                        l_FamilyConferenceNote.droprow7 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow7"]);
                        l_FamilyConferenceNote.droprow8 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow8"]);
                        l_FamilyConferenceNote.droprow9 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow9"]);
                        l_FamilyConferenceNote.droprow10 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow10"]);
                        l_FamilyConferenceNote.droprow11 = Convert.ToString(dataReceive.Tables[0].Rows[index]["droprow11"]);
                        l_FamilyConferenceNote.Attendees1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees1"]);
                        l_FamilyConferenceNote.Attendees2 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees2"]);
                        l_FamilyConferenceNote.Attendees3 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees3"]);
                        l_FamilyConferenceNote.Attendees4 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees4"]);
                        l_FamilyConferenceNote.Attendees5 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees5"]);
                        l_FamilyConferenceNote.Attendees6 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees6"]);
                        l_FamilyConferenceNote.Attendees7 = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees7"]);
                        l_FamilyConferenceNote.Attendees1rela = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees1rela"]);
                        l_FamilyConferenceNote.Attendees2rela = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees2rela"]);
                        l_FamilyConferenceNote.Attendees3rela = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees3rela"]);
                        l_FamilyConferenceNote.Attendees4rela = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees4rela"]);
                        l_FamilyConferenceNote.Attendees5rela = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees5rela"]);
                        l_FamilyConferenceNote.Attendees6rela = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees6rela"]);
                        l_FamilyConferenceNote.Attendees7rela = Convert.ToString(dataReceive.Tables[0].Rows[index]["Attendees7rela"]);
                        l_FamilyConferenceNote.completeName = Convert.ToString(dataReceive.Tables[0].Rows[index]["completeName"]);
                        l_FamilyConferenceNote.completeSign = Convert.ToString(dataReceive.Tables[0].Rows[index]["completeSign"]);

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
                l_Cmd.Parameters.AddWithValue("@BPPosition", p_Model.BPPosition);
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
                l_Cmd.Parameters.AddWithValue("@Person", p_Model.Person);
                l_Cmd.Parameters.AddWithValue("@PulpilsEquals", p_Model.PulpilsEquals);
                l_Cmd.Parameters.AddWithValue("@PulpilsReactive", p_Model.PulpilsReactive);
                l_Cmd.Parameters.AddWithValue("@Eyes", p_Model.Eyes);

                l_Cmd.Parameters.AddWithValue("@Eyes_Clear", p_Model.Eyes_Clear);
                l_Cmd.Parameters.AddWithValue("@Eyes_Reddened", p_Model.Eyes_Reddened);
                l_Cmd.Parameters.AddWithValue("@Eyes_Swelling", p_Model.Eyes_Swelling);
                l_Cmd.Parameters.AddWithValue("@Eyes_Discharge", p_Model.Eyes_Discharge);

                // l_Cmd.Parameters.AddWithValue("@PulpilsEquals", p_Model.PulpilsEquals);
                l_Cmd.Parameters.AddWithValue("@GeneralFace", p_Model.GeneralFace);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
                l_Cmd.Parameters.AddWithValue("@PulseStrength", p_Model.PulseStrength);

                SqlParameter returnVal = l_Cmd.Parameters.Add("tempid", SqlDbType.Int);
                returnVal.Direction = ParameterDirection.ReturnValue;
                l_Cmd.ExecuteNonQuery();


                p_Model.linkid = int.Parse(returnVal.Value.ToString());


                SqlDataAdapter l_DA2 = new SqlDataAdapter();
                SqlCommand l_Cmd2 = new SqlCommand("spAB_AddHeadToToeAssessment_PART2", l_Conn);
                l_Cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd2.Parameters.AddWithValue("@linkid", p_Model.linkid);
                l_Cmd2.Parameters.AddWithValue("@c_longsound_normal", p_Model.c_longsound_normal);
                l_Cmd2.Parameters.AddWithValue("@c_longsound_describe", p_Model.c_longsound_describe);
                l_Cmd2.Parameters.AddWithValue("@c_longsound_equal", p_Model.c_longsound_equal);
                l_Cmd2.Parameters.AddWithValue("@c_c", p_Model.c_c);
                l_Cmd2.Parameters.AddWithValue("@c_c_other", p_Model.c_c_other);
                l_Cmd2.Parameters.AddWithValue("@c_heartsound", p_Model.c_heartsound);
                l_Cmd2.Parameters.AddWithValue("@c_heartsound_describe", p_Model.c_heartsound_describe);
                l_Cmd2.Parameters.AddWithValue("@a_soft", p_Model.a_soft);
                l_Cmd2.Parameters.AddWithValue("@a_soft_describe", p_Model.a_soft_describe);
                l_Cmd2.Parameters.AddWithValue("@a_pful", p_Model.a_pful);
                l_Cmd2.Parameters.AddWithValue("@a_pful_describe", p_Model.a_pful_describe);
                l_Cmd2.Parameters.AddWithValue("@a_bowelsound", p_Model.a_bowelsound);
                l_Cmd2.Parameters.AddWithValue("@a_lastbowel_date", p_Model.a_lastbowel_date);
                l_Cmd2.Parameters.AddWithValue("@a_voidingnormal", p_Model.a_voidingnormal);
                l_Cmd2.Parameters.AddWithValue("@a_voidingnormal_describe", p_Model.a_voidingnormal_describe);
                l_Cmd2.Parameters.AddWithValue("@a_voidingnormal1", p_Model.a_voidingnormal1);
                l_Cmd2.Parameters.AddWithValue("@a_voidingnormal_pads", p_Model.a_voidingnormal_pads);
                l_Cmd2.Parameters.AddWithValue("@a_voidingnormal2", p_Model.a_voidingnormal2);
                l_Cmd2.Parameters.AddWithValue("@edema_feet_normal", p_Model.edema_feet_normal);
                l_Cmd2.Parameters.AddWithValue("@edema_feet_describe", p_Model.edema_feet_describe);
                l_Cmd2.Parameters.AddWithValue("@edema_feet1", p_Model.edema_feet1);
                l_Cmd2.Parameters.AddWithValue("@edema_hands_normal", p_Model.edema_hands_normal);
                l_Cmd2.Parameters.AddWithValue("@edema_hands_describe", p_Model.edema_hands_describe);
                l_Cmd2.Parameters.AddWithValue("@edema_hands1", p_Model.edema_hands1);
                l_Cmd2.Parameters.AddWithValue("@edema_other", p_Model.edema_other);
                l_Cmd2.Parameters.AddWithValue("@edema_other_describe", p_Model.edema_other_describe);
                l_Cmd2.Parameters.AddWithValue("@skin_feet", p_Model.skin_feet);
                l_Cmd2.Parameters.AddWithValue("@skin_feet_describe", p_Model.skin_feet_describe);
                l_Cmd2.Parameters.AddWithValue("@skin_rashes", p_Model.skin_rashes);
                l_Cmd2.Parameters.AddWithValue("@skin_redness", p_Model.skin_redness);
                l_Cmd2.Parameters.AddWithValue("@skin_bruising", p_Model.skin_bruising);
                l_Cmd2.Parameters.AddWithValue("@skin_openareas", p_Model.skin_openareas);
                l_Cmd2.Parameters.AddWithValue("@skin_desc_abnormal", p_Model.skin_desc_abnormal);
                l_Cmd2.Parameters.AddWithValue("@skin_wounddressing", p_Model.skin_wounddressing);
                l_Cmd2.Parameters.AddWithValue("@skin_desc", p_Model.skin_desc);
                l_Cmd2.Parameters.AddWithValue("@p_residentp", p_Model.p_residentp);
                l_Cmd2.Parameters.AddWithValue("@p_residentp_desc", p_Model.p_residentp_desc);
                l_Cmd2.Parameters.AddWithValue("@p_pscale", p_Model.p_pscale);
                l_Cmd2.Parameters.AddWithValue("@p_aching", p_Model.p_aching);
                l_Cmd2.Parameters.AddWithValue("@p_sharp", p_Model.p_sharp);
                l_Cmd2.Parameters.AddWithValue("@p_dull", p_Model.p_dull);

                l_Cmd2.Parameters.AddWithValue("@p_burning", p_Model.p_burning);
                l_Cmd2.Parameters.AddWithValue("@p_ATTMFP", p_Model.p_ATTMFP);
                l_Cmd2.Parameters.AddWithValue("@p_ATTMFP_desc", p_Model.p_ATTMFP_desc);
                l_Cmd2.Parameters.AddWithValue("@a_voidingnormal2_other", p_Model.a_voidingnormal2_other);

                l_Cmd2.Parameters.AddWithValue("@p_radiating", p_Model.p_radiating);
                l_Cmd2.Parameters.AddWithValue("@p_where", p_Model.p_where);
                l_Cmd2.Parameters.AddWithValue("@p_whatmakes_better", p_Model.p_whatmakes_better);
                l_Cmd2.Parameters.AddWithValue("@p_whatmakes_worst", p_Model.p_whatmakes_worst);
                l_Cmd2.Parameters.AddWithValue("@p_interface_adl", p_Model.p_interface_adl);
                l_Cmd2.Parameters.AddWithValue("@p_describe", p_Model.p_describe);
                l_Cmd2.Parameters.AddWithValue("@p_other", p_Model.p_other);
                l_Cmd2.Parameters.AddWithValue("@completed_by", p_Model.completed_by);
                l_Cmd2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddAdmissionHeadToToe |" + ex.ToString();
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
            string[] PulseStrengthLIST = new string[] { "Strong", "Weak", "Regular", "Irregular" };
            string[] BPPositionLIST = new string[] { "Sit", "Stand", "Lying" };
            string[] TempLocationLIST = new string[] { "Tympanic", "Oral" };
            string[] RespLocationLIST = new string[] { "Laboured", "Shallow" };
            string[] SP02LocationLIST = new string[] { "Room Air", "02" };
            string[] SpeechLIST = new string[] { "Normal", "Slurred", "Delayed" };
            string[] PrimaryLanguageLIST = new string[] { "English" };
            string[] EyesLIST = new string[] { "Clear", "Reddened", "Swelling", "Discharge" };

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
                        l_AdmissionHeadToToe.BPPosition = Convert.ToString(dataReceive.Tables[0].Rows[index]["BPPosition"]);
                        l_AdmissionHeadToToe.RedialPulse = Convert.ToString(dataReceive.Tables[0].Rows[index]["RedialPulse"]);
                        l_AdmissionHeadToToe.PulseLocation = Convert.ToString(dataReceive.Tables[0].Rows[index]["PulseLocation"]);
                        l_AdmissionHeadToToe.PulseStrength = Convert.ToString(dataReceive.Tables[0].Rows[index]["PulseStrength"]);
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

                        l_AdmissionHeadToToe.Eyes_Clear = Convert.ToBoolean(Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Eyes_Clear"]));
                        l_AdmissionHeadToToe.Eyes_Reddened = Convert.ToBoolean(Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Eyes_Reddened"]));
                        l_AdmissionHeadToToe.Eyes_Swelling = Convert.ToBoolean(Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Eyes_Swelling"]));
                        l_AdmissionHeadToToe.Eyes_Discharge = Convert.ToBoolean(Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Eyes_Discharge"]));

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


                        if (PulseStrengthLIST.Contains(l_AdmissionHeadToToe.PulseStrength) == false )
                        {
                            l_AdmissionHeadToToe.PulseStrength_other = l_AdmissionHeadToToe.PulseStrength;
                            l_AdmissionHeadToToe.PulseStrength = "";
                        }
                        if (BPPositionLIST.Contains(l_AdmissionHeadToToe.BPPosition) ==false)
                        {
                            l_AdmissionHeadToToe.BPPosition_other = l_AdmissionHeadToToe.BPPosition;
                            l_AdmissionHeadToToe.BPPosition = "";
                        }
                        if (TempLocationLIST.Contains(l_AdmissionHeadToToe.TempLocation)==false)
                        {
                            l_AdmissionHeadToToe.TempLocation_other = l_AdmissionHeadToToe.TempLocation;
                            l_AdmissionHeadToToe.TempLocation = "";
                        }
                        if (RespLocationLIST.Contains(l_AdmissionHeadToToe.RespLocation) ==false)
                        {
                            l_AdmissionHeadToToe.RespLocation_other = l_AdmissionHeadToToe.RespLocation;
                            l_AdmissionHeadToToe.RespLocation = "";
                        }
                        if (SP02LocationLIST.Contains(l_AdmissionHeadToToe.SP02Location) ==false)
                        {
                            l_AdmissionHeadToToe.SP02Location_other = l_AdmissionHeadToToe.SP02Location;
                            l_AdmissionHeadToToe.SP02Location = "";
                        }
                        if (SpeechLIST.Contains(l_AdmissionHeadToToe.Speech) ==false)
                        {
                            l_AdmissionHeadToToe.Speech_other = l_AdmissionHeadToToe.Speech;
                            l_AdmissionHeadToToe.Speech = "";
                        }
                        if (PrimaryLanguageLIST.Contains(l_AdmissionHeadToToe.PrimaryLanguage) ==false)
                        {
                            l_AdmissionHeadToToe.PrimaryLanguage_other = l_AdmissionHeadToToe.PrimaryLanguage;
                            l_AdmissionHeadToToe.PrimaryLanguage = "";
                        }
                        if (EyesLIST.Contains(l_AdmissionHeadToToe.Eyes) ==false)
                        {
                            l_AdmissionHeadToToe.Eyes_other = l_AdmissionHeadToToe.Eyes;
                            l_AdmissionHeadToToe.Eyes = "";
                        }



                        l_AdmissionHeadToToe.PART2_id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["id"]);
                        l_AdmissionHeadToToe.linkid = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["linkid"]);
                        l_AdmissionHeadToToe.c_longsound_normal = Convert.ToString(dataReceive.Tables[0].Rows[index]["c_longsound_normal"]);
                        l_AdmissionHeadToToe.c_longsound_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["c_longsound_describe"]);
                        l_AdmissionHeadToToe.c_longsound_equal = Convert.ToString(dataReceive.Tables[0].Rows[index]["c_longsound_equal"]);
                        l_AdmissionHeadToToe.c_c = Convert.ToString(dataReceive.Tables[0].Rows[index]["c_c"]);
                        l_AdmissionHeadToToe.c_c_other = Convert.ToString(dataReceive.Tables[0].Rows[index]["c_c_other"]);
                        l_AdmissionHeadToToe.c_heartsound = Convert.ToString(dataReceive.Tables[0].Rows[index]["c_heartsound"]);
                        l_AdmissionHeadToToe.c_heartsound_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["c_heartsound_describe"]);
                        l_AdmissionHeadToToe.a_soft = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_soft"]);
                        l_AdmissionHeadToToe.a_soft_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_soft_describe"]);
                        l_AdmissionHeadToToe.a_pful = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_pful"]);
                        l_AdmissionHeadToToe.a_pful_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_pful_describe"]);
                        l_AdmissionHeadToToe.a_bowelsound = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_bowelsound"]);
                        l_AdmissionHeadToToe.a_lastbowel_date = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_lastbowel_date"]);
                        l_AdmissionHeadToToe.a_voidingnormal = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_voidingnormal"]);
                        l_AdmissionHeadToToe.a_voidingnormal_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_voidingnormal_describe"]);
                        l_AdmissionHeadToToe.a_voidingnormal1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_voidingnormal1"]);
                        l_AdmissionHeadToToe.a_voidingnormal_pads = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_voidingnormal_pads"]);
                        l_AdmissionHeadToToe.a_voidingnormal2 = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_voidingnormal2"]);
                        l_AdmissionHeadToToe.edema_feet_normal = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_feet_normal"]);
                        l_AdmissionHeadToToe.edema_feet_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_feet_describe"]);
                        l_AdmissionHeadToToe.edema_feet1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_feet1"]);
                        l_AdmissionHeadToToe.edema_hands_normal = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_hands_normal"]);
                        l_AdmissionHeadToToe.edema_hands_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_hands_describe"]);
                        l_AdmissionHeadToToe.edema_hands1 = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_hands1"]);
                        l_AdmissionHeadToToe.edema_other = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_other"]);
                        l_AdmissionHeadToToe.edema_other_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["edema_other_describe"]);
                        l_AdmissionHeadToToe.skin_feet = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_feet"]);
                        l_AdmissionHeadToToe.skin_feet_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_feet_describe"]);
                        l_AdmissionHeadToToe.skin_rashes = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_rashes"]);
                        l_AdmissionHeadToToe.skin_redness = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_redness"]);
                        l_AdmissionHeadToToe.skin_bruising = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_bruising"]);
                        l_AdmissionHeadToToe.skin_openareas = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_openareas"]);
                        l_AdmissionHeadToToe.skin_desc_abnormal = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_desc_abnormal"]);
                        l_AdmissionHeadToToe.skin_wounddressing = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_wounddressing"]);
                        l_AdmissionHeadToToe.skin_desc = Convert.ToString(dataReceive.Tables[0].Rows[index]["skin_desc"]);
                        l_AdmissionHeadToToe.p_residentp = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_residentp"]);
                        l_AdmissionHeadToToe.p_residentp_desc = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_residentp_desc"]);
                        l_AdmissionHeadToToe.p_pscale = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_pscale"]);
                        l_AdmissionHeadToToe.p_aching = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_aching"]);
                        l_AdmissionHeadToToe.p_sharp = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_sharp"]);
                        l_AdmissionHeadToToe.p_dull = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_dull"]);

                        l_AdmissionHeadToToe.p_burning = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_burning"]);
                        l_AdmissionHeadToToe.p_ATTMFP = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_ATTMFP"]);
                        l_AdmissionHeadToToe.p_ATTMFP_desc = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_ATTMFP_desc"]);
                        l_AdmissionHeadToToe.a_voidingnormal2_other = Convert.ToString(dataReceive.Tables[0].Rows[index]["a_voidingnormal2_other"]);

                        l_AdmissionHeadToToe.p_radiating = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_radiating"]);
                        l_AdmissionHeadToToe.p_where = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_where"]);
                        l_AdmissionHeadToToe.p_whatmakes_better = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_whatmakes_better"]);
                        l_AdmissionHeadToToe.p_whatmakes_worst = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_whatmakes_worst"]);
                        l_AdmissionHeadToToe.p_interface_adl = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_interface_adl"]);
                        l_AdmissionHeadToToe.p_describe = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_describe"]);
                        l_AdmissionHeadToToe.p_other = Convert.ToString(dataReceive.Tables[0].Rows[index]["p_other"]);
                        l_AdmissionHeadToToe.completed_by = Convert.ToString(dataReceive.Tables[0].Rows[index]["completed_by"]);



                        l_AdmissionHeadToToe.c_c_check1 = Convert.ToBoolean(l_AdmissionHeadToToe.c_c.Split(',')[0]);
                        l_AdmissionHeadToToe.c_c_check2 = Convert.ToBoolean(l_AdmissionHeadToToe.c_c.Split(',')[1]);
                        l_AdmissionHeadToToe.c_c_check3 = Convert.ToBoolean(l_AdmissionHeadToToe.c_c.Split(',')[2]);

                        l_AdmissionHeadToToe.edema_feet1_check1 = Convert.ToBoolean(l_AdmissionHeadToToe.edema_feet1.Split(',')[0]);
                        l_AdmissionHeadToToe.edema_feet1_check2 = Convert.ToBoolean(l_AdmissionHeadToToe.edema_feet1.Split(',')[1]);
                        l_AdmissionHeadToToe.edema_feet1_check3 = Convert.ToBoolean(l_AdmissionHeadToToe.edema_feet1.Split(',')[2]);

                        l_AdmissionHeadToToe.edema_hands1_check1 = Convert.ToBoolean(l_AdmissionHeadToToe.edema_hands1.Split(',')[0]);
                        l_AdmissionHeadToToe.edema_hands1_check2 = Convert.ToBoolean(l_AdmissionHeadToToe.edema_hands1.Split(',')[1]);
                        l_AdmissionHeadToToe.edema_hands1_check3 = Convert.ToBoolean(l_AdmissionHeadToToe.edema_hands1.Split(',')[2]);

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
                l_Cmd.Parameters.AddWithValue("@Apetite", p_Model.Appetite);
                l_Cmd.Parameters.AddWithValue("@DIFF", p_Model.DIFF);

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

                    if (p_Model.Diet != null)
                    {
                        foreach (var diet in p_Model.Diet)
                        {
                            if (diet != "")
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
                        }
                    }
                    
                    if(p_Model.Allergies != null)
                    {
                        foreach (var allergy in p_Model.Allergies)
                        {
                            SqlCommand l_Cmd_Diet = new SqlCommand("spAB_Add_DietaryAssessment_Allergies", l_Conn);
                            //  l_Conn.Open();
                            l_Cmd_Diet.CommandType = System.Data.CommandType.StoredProcedure;
                            l_Cmd_Diet.Parameters.AddWithValue("@Residentid", p_Model.Resident.ID);
                            l_Cmd_Diet.Parameters.AddWithValue("@Allergy", allergy.Name);
                            l_Cmd_Diet.Parameters.AddWithValue("@AllergyId", allergy.ID);
                            l_Cmd_Diet.Parameters.AddWithValue("@AssessmentId", l_AssessmentId);
                            if (allergy.Note == null) allergy.Note = "";
                            l_Cmd_Diet.Parameters.AddWithValue("@note", allergy.Note);
                            l_Cmd_Diet.Parameters.AddWithValue("@Enteredby", p_Model.EnteredBy.ID);
                            l_Cmd_Diet.ExecuteNonQuery();
                        }
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

        public static Collection<AllergiesModel> GetAllergiesCollections_mike(string term)
        {
            string exception = string.Empty;

            Collection<AllergiesModel> allergiess = new Collection<AllergiesModel>();
            AllergiesModel allergies;
            ResidentModel l_Resident = new ResidentModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("Get_Allergies_Mike", l_Conn);
                l_Cmd.Parameters.AddWithValue("term", term);
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
                exception = "GetAllergiesCollections_mike |" + ex.ToString();
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
                        l_Assessment.Appetite = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Apetite"]);
                        l_Assessment.Other = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Other"]);
                        l_Assessment.Likes = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Likes"]);
                        l_Assessment.DisLikes = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DisLikes"]);
                        l_Assessment.Notes = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Notes"]);
                        l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_Assessment.DIFF = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DIFF"]);

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
                                l_Allergy.Note = Convert.ToString(AllergyReceive.Tables[0].Rows[index_Diets]["note"]);

                                l_Assessment.Allergies.Add(l_Allergy);
                            }
                        }
                        else { l_Assessment.noAllergy = true; }
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




                l_Cmd.Parameters.AddWithValue("@happened_Date", p_Model.happened_Date);
                l_Cmd.Parameters.AddWithValue("@happened_Time", p_Model.happened_Time);
                l_Cmd.Parameters.AddWithValue("@Reportby", p_Model.Reportby);
                l_Cmd.Parameters.AddWithValue("@Location_Other", p_Model.Location_Other);
                l_Cmd.Parameters.AddWithValue("@Resident_involved_Name", p_Model.Resident_involved_Name);
                l_Cmd.Parameters.AddWithValue("@Resident_involved_RM", p_Model.Resident_involved_RM);
                l_Cmd.Parameters.AddWithValue("@Resident_Status_1", p_Model.Resident_Status_1);
                l_Cmd.Parameters.AddWithValue("@Resident_Status_2", p_Model.Resident_Status_2);
                l_Cmd.Parameters.AddWithValue("@Resident_Status_3", p_Model.Resident_Status_3);
                l_Cmd.Parameters.AddWithValue("@Resident_Status_4", p_Model.Resident_Status_4);
                l_Cmd.Parameters.AddWithValue("@Resident_Status_5", p_Model.Resident_Status_5);
                l_Cmd.Parameters.AddWithValue("@Resident_Status_6", p_Model.Resident_Status_6);
                l_Cmd.Parameters.AddWithValue("@Resident_Status_text", p_Model.Resident_Status_text);
                l_Cmd.Parameters.AddWithValue("@C_Other1", p_Model.C_Other1);
                l_Cmd.Parameters.AddWithValue("@C_Other2", p_Model.C_Other2);
                l_Cmd.Parameters.AddWithValue("@C_Other3", p_Model.C_Other3);
                l_Cmd.Parameters.AddWithValue("@C_Other4", p_Model.C_Other4);
                l_Cmd.Parameters.AddWithValue("@C_Other5", p_Model.C_Other5);
                l_Cmd.Parameters.AddWithValue("@D_Date", p_Model.D_Date);
                l_Cmd.Parameters.AddWithValue("@D_Time", p_Model.D_Time);
                l_Cmd.Parameters.AddWithValue("@E_Date", p_Model.E_Date);
                l_Cmd.Parameters.AddWithValue("@E_Time", p_Model.E_Time);
                l_Cmd.Parameters.AddWithValue("@F_Date", p_Model.F_Date);
                l_Cmd.Parameters.AddWithValue("@F_Time", p_Model.F_Time);
                l_Cmd.Parameters.AddWithValue("@H_Date", p_Model.H_Date);
                l_Cmd.Parameters.AddWithValue("@H_Time", p_Model.H_Time);
                l_Cmd.Parameters.AddWithValue("@J_Date", p_Model.J_Date);
                l_Cmd.Parameters.AddWithValue("@J_Time", p_Model.J_Time);

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



                        l_Assessment.happened_Date = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["happened_Date"]);
                        l_Assessment.happened_Time = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["happened_Time"]);
                        l_Assessment.Reportby = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Reportby"]);
                        l_Assessment.Location_Other = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Location_Other"]);
                        l_Assessment.Resident_involved_Name = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Resident_involved_Name"]);
                        l_Assessment.Resident_involved_RM = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Resident_involved_RM"]);
                        l_Assessment.Resident_Status_1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Resident_Status_1"]);
                        l_Assessment.Resident_Status_2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Resident_Status_2"]);
                        l_Assessment.Resident_Status_3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Resident_Status_3"]);
                        l_Assessment.Resident_Status_4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Resident_Status_4"]);
                        l_Assessment.Resident_Status_5 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Resident_Status_5"]);
                        l_Assessment.Resident_Status_6 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Resident_Status_6"]);
                        l_Assessment.Resident_Status_text = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Resident_Status_text"]);
                        l_Assessment.C_Other1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["C_Other1"]);
                        l_Assessment.C_Other2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["C_Other2"]);
                        l_Assessment.C_Other3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["C_Other3"]);
                        l_Assessment.C_Other4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["C_Other4"]);
                        l_Assessment.C_Other5 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["C_Other5"]);
                        l_Assessment.D_Date = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["D_Date"]);
                        l_Assessment.D_Time = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["D_Time"]);
                        l_Assessment.E_Date = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["E_Date"]);
                        l_Assessment.E_Time = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["E_Time"]);
                        l_Assessment.F_Date = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["F_Date"]);
                        l_Assessment.F_Time = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["F_Time"]);
                        l_Assessment.H_Date = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["H_Date"]);
                        l_Assessment.H_Time = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["H_Time"]);
                        l_Assessment.J_Date = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["J_Date"]);
                        l_Assessment.J_Time = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["J_Time"]);










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

        public static Collection<FallRiskAssessmentModel> GetFallRiskAssessment(int p_ResidentId)
        {
            string exception = string.Empty;

            Collection<FallRiskAssessmentModel> l_Assessments = new Collection<FallRiskAssessmentModel>();
            FallRiskAssessmentModel l_Assessment;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Fall_RiskAssessment", l_Conn);
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
                        l_Assessment = new FallRiskAssessmentModel();
                        l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.ResidentId = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.FallHistory_IsTwoOrMore = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["FallHistory_IsTwoOrMore"]);
                        l_Assessment.FallHistory_IsOneOrTwo = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["FallHistory_IsOneOrTwo"]);
                        l_Assessment.Neurological_IsCVA = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Neurological_IsCVA"]);
                        l_Assessment.Neurological_IsParkinsons = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Neurological_IsParkinsons"]);
                        l_Assessment.Neurological_IsAlzheimers = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Neurological_IsAlzheimers"]);
                        l_Assessment.Neurological_IsSeizureDisorder = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Neurological_IsSeizureDisorder"]);
                        l_Assessment.Neurological_IsOther = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Neurological_IsOther"]);
                        l_Assessment.Other_IsDiabetes = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Other_IsDiabetes"]);
                        l_Assessment.Other_IsOsteoporosis = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Other_IsOsteoporosis"]);
                        l_Assessment.Other_IsPosturalHypotension = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Other_IsPosturalHypotension"]);
                        l_Assessment.Other_IsSyncope = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Other_IsSyncope"]);
                        l_Assessment.Incontinence_IsBowel = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Incontinence_IsBowel"]);
                        l_Assessment.Incontinence_IsBladder = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Incontinence_IsBladder"]);
                        l_Assessment.Incontinence_IsTransfer = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Incontinence_IsTransfer"]);
                        l_Assessment.Incontinence_IsUnsteady = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Incontinence_IsUnsteady"]);
                        l_Assessment.Medication_IsCardiac = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Medication_IsCardiac"]);
                        l_Assessment.Medication_IsDiuretics = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Medication_IsDiuretics"]);
                        l_Assessment.Medication_IsNarcotics = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Medication_IsNarcotics"]);
                        l_Assessment.Medication_IsAnalgesics = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Medication_IsAnalgesics"]);
                        l_Assessment.Medication_IsSedatives = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Medication_IsSedatives"]);
                        l_Assessment.Medication_IsAntiAnxiety = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Medication_IsAntiAnxiety"]);
                        l_Assessment.Medication_IsLaxatives = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Medication_IsLaxatives"]);
                        l_Assessment.MentalStatus_IsConfused = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["MentalStatus_IsConfused"]);
                        l_Assessment.MentalStatus_IsResidentNonCompliance = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["MentalStatus_IsResidentNonCompliance"]);
                        l_Assessment.Orthopedic_IsRecent = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Orthopedic_IsRecent"]);
                        l_Assessment.Orthopedic_IsCast = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Orthopedic_IsCast"]);
                        l_Assessment.Orthopedic_IsAmputation = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Orthopedic_IsAmputation"]);
                        l_Assessment.Orthopedic_IsSevere = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Orthopedic_IsSevere"]);
                        l_Assessment.Sensory_IsDecreasedVision = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sensory_IsDecreasedVision"]);
                        l_Assessment.Sensory_IsDecreasedHearing = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sensory_IsDecreasedHearing"]);
                        l_Assessment.Sensory_IsAphasia = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sensory_IsAphasia"]);
                        l_Assessment.Assistive_IsWheelChair = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Assistive_IsWheelChair"]);
                        l_Assessment.Assistive_IsWalker = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Assistive_IsWalker"]);
                        l_Assessment.Assistive_IsCane = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Assistive_IsCane"]);

                        l_Assessment.TotalScore = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["TotalScore"]);
                        l_Assessment.RiskLevel = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["RiskLevel"]);
                        l_Assessment.ResidentId = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_Assessment.EnteredBy = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_Assessments.Add(l_Assessment);
                    }

                }
            }
            catch (Exception ex)
            {
                exception = "GetFallRiskAssessment |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            return l_Assessments;
        }

        public static void AddFallRiskAssessment(FallRiskAssessmentModel p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_Fall_RiskAssessment", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@FallHistory_IsTwoOrMore", p_Model.FallHistory_IsTwoOrMore);
                l_Cmd.Parameters.AddWithValue("@FallHistory_IsOneOrTwo", p_Model.FallHistory_IsOneOrTwo);
                l_Cmd.Parameters.AddWithValue("@Neurological_IsCVA", p_Model.Neurological_IsCVA);
                l_Cmd.Parameters.AddWithValue("@Neurological_IsParkinsons", p_Model.Neurological_IsParkinsons);
                l_Cmd.Parameters.AddWithValue("@Neurological_IsAlzheimers", p_Model.Neurological_IsAlzheimers);
                l_Cmd.Parameters.AddWithValue("@Neurological_IsSeizureDisorder", p_Model.Neurological_IsSeizureDisorder);
                l_Cmd.Parameters.AddWithValue("@Neurological_IsOther", p_Model.Neurological_IsOther);
                l_Cmd.Parameters.AddWithValue("@Other_IsDiabetes", p_Model.Other_IsDiabetes);
                l_Cmd.Parameters.AddWithValue("@Other_IsOsteoporosis", p_Model.Other_IsOsteoporosis);
                l_Cmd.Parameters.AddWithValue("@Other_IsPosturalHypotension", p_Model.Other_IsPosturalHypotension);
                l_Cmd.Parameters.AddWithValue("@Other_IsSyncope", p_Model.Other_IsSyncope);
                l_Cmd.Parameters.AddWithValue("@Incontinence_IsBowel", p_Model.Incontinence_IsBowel);
                l_Cmd.Parameters.AddWithValue("@Incontinence_IsBladder", p_Model.Incontinence_IsBladder);
                l_Cmd.Parameters.AddWithValue("@Incontinence_IsTransfer", p_Model.Incontinence_IsTransfer);
                l_Cmd.Parameters.AddWithValue("@Incontinence_IsUnsteady", p_Model.Incontinence_IsUnsteady);
                l_Cmd.Parameters.AddWithValue("@Medication_IsCardiac", p_Model.Medication_IsCardiac);
                l_Cmd.Parameters.AddWithValue("@Medication_IsDiuretics", p_Model.Medication_IsDiuretics);
                l_Cmd.Parameters.AddWithValue("@Medication_IsNarcotics", p_Model.Medication_IsNarcotics);
                l_Cmd.Parameters.AddWithValue("@Medication_IsAnalgesics", p_Model.Medication_IsAnalgesics);
                l_Cmd.Parameters.AddWithValue("@Medication_IsSedatives", p_Model.Medication_IsSedatives);
                l_Cmd.Parameters.AddWithValue("@Medication_IsAntiAnxiety", p_Model.Medication_IsAntiAnxiety);
                l_Cmd.Parameters.AddWithValue("@Medication_IsLaxatives", p_Model.Medication_IsLaxatives);
                l_Cmd.Parameters.AddWithValue("@MentalStatus_IsConfused", p_Model.MentalStatus_IsConfused);
                l_Cmd.Parameters.AddWithValue("@MentalStatus_IsResidentNonCompliance", p_Model.MentalStatus_IsResidentNonCompliance);
                l_Cmd.Parameters.AddWithValue("@Orthopedic_IsRecent", p_Model.Orthopedic_IsRecent);
                l_Cmd.Parameters.AddWithValue("@Orthopedic_IsCast", p_Model.Orthopedic_IsCast);
                l_Cmd.Parameters.AddWithValue("@Orthopedic_IsAmputation", p_Model.Orthopedic_IsAmputation);
                l_Cmd.Parameters.AddWithValue("@Orthopedic_IsSevere", p_Model.Orthopedic_IsSevere);
                l_Cmd.Parameters.AddWithValue("@Sensory_IsDecreasedVision", p_Model.Sensory_IsDecreasedVision);
                l_Cmd.Parameters.AddWithValue("@Sensory_IsDecreasedHearing", p_Model.Sensory_IsDecreasedHearing);
                l_Cmd.Parameters.AddWithValue("@Sensory_IsAphasia", p_Model.Sensory_IsAphasia);

                l_Cmd.Parameters.AddWithValue("@Assistive_IsWheelChair", p_Model.Assistive_IsWheelChair);
                l_Cmd.Parameters.AddWithValue("@Assistive_IsWalker", p_Model.Assistive_IsWalker);
                l_Cmd.Parameters.AddWithValue("@Assistive_IsCane", p_Model.Assistive_IsCane);

                l_Cmd.Parameters.AddWithValue("@TotalScore", p_Model.TotalScore);
                l_Cmd.Parameters.AddWithValue("@RiskLevel", p_Model.RiskLevel);
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.ResidentId);
                l_Cmd.Parameters.AddWithValue("@DateEntered", p_Model.DateEntered);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy);
                l_Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                exception = "AddFallRiskAssessment |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }














        public static Collection<ExcerciseActivityDetailModel_mike>getmike(int p_ResidentId)
        {
            string exception = string.Empty;

            Collection<ExcerciseActivityDetailModel_mike> l_Assessments = new Collection<ExcerciseActivityDetailModel_mike>();
            ExcerciseActivityDetailModel_mike l_Assessment;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_Excercise_Activity_Detail_mike", l_Conn);
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
                        l_Assessment = new ExcerciseActivityDetailModel_mike();
                        l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.Residentid = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["residentId"]);
                        l_Assessment.ResidentName = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ResidentName"]);
                        l_Assessment.start_time = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["start_time"]);
                        l_Assessment.end_time = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["end_time"]);
                        l_Assessment.SuiteNumber = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SuiteNumber"]);
                        l_Assessment.EnteredBy = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["modified_by"]);
                        l_Assessment.EnteredByName = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["EnteredByName"]);
                        l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["modified_on"]);

                        l_Assessment.ActivityName_1_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_1_week1"]);
                        l_Assessment.ActivityName_2_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_2_week1"]);
                        l_Assessment.ActivityName_3_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_3_week1"]);
                        l_Assessment.ActivityName_4_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_4_week1"]);
                        l_Assessment.ActivityName_5_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_5_week1"]);
                        l_Assessment.ActivityName_6_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_6_week1"]);
                        l_Assessment.ActivityName_7_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_7_week1"]);
                        l_Assessment.ActivityName_8_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_8_week1"]);
                        l_Assessment.ActivityName_9_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_9_week1"]);
                        l_Assessment.ActivityName_10_week1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_10_week1"]);
                        l_Assessment.ActivityName_1_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_1_week2"]);
                        l_Assessment.ActivityName_2_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_2_week2"]);
                        l_Assessment.ActivityName_3_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_3_week2"]);
                        l_Assessment.ActivityName_4_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_4_week2"]);
                        l_Assessment.ActivityName_5_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_5_week2"]);
                        l_Assessment.ActivityName_6_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_6_week2"]);
                        l_Assessment.ActivityName_7_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_7_week2"]);
                        l_Assessment.ActivityName_8_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_8_week2"]);
                        l_Assessment.ActivityName_9_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_9_week2"]);
                        l_Assessment.ActivityName_10_week2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_10_week2"]);
                        l_Assessment.ActivityName_1_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_1_week3"]);
                        l_Assessment.ActivityName_2_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_2_week3"]);
                        l_Assessment.ActivityName_3_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_3_week3"]);
                        l_Assessment.ActivityName_4_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_4_week3"]);
                        l_Assessment.ActivityName_5_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_5_week3"]);
                        l_Assessment.ActivityName_6_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_6_week3"]);
                        l_Assessment.ActivityName_7_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_7_week3"]);
                        l_Assessment.ActivityName_8_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_8_week3"]);
                        l_Assessment.ActivityName_9_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_9_week3"]);
                        l_Assessment.ActivityName_10_week3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_10_week3"]);
                        l_Assessment.ActivityName_1_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_1_week4"]);
                        l_Assessment.ActivityName_2_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_2_week4"]);
                        l_Assessment.ActivityName_3_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_3_week4"]);
                        l_Assessment.ActivityName_4_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_4_week4"]);
                        l_Assessment.ActivityName_5_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_5_week4"]);
                        l_Assessment.ActivityName_6_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_6_week4"]);
                        l_Assessment.ActivityName_7_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_7_week4"]);
                        l_Assessment.ActivityName_8_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_8_week4"]);
                        l_Assessment.ActivityName_9_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_9_week4"]);
                        l_Assessment.ActivityName_10_week4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["ActivityName_10_week4"]);

                        l_Assessment.ActivityID_1_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_1_week1"]);
                        l_Assessment.ActivityID_2_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_2_week1"]);
                        l_Assessment.ActivityID_3_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_3_week1"]);
                        l_Assessment.ActivityID_4_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_4_week1"]);
                        l_Assessment.ActivityID_5_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_5_week1"]);
                        l_Assessment.ActivityID_6_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_6_week1"]);
                        l_Assessment.ActivityID_7_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_7_week1"]);
                        l_Assessment.ActivityID_8_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_8_week1"]);
                        l_Assessment.ActivityID_9_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_9_week1"]);
                        l_Assessment.ActivityID_10_week1 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_10_week1"]);
                        l_Assessment.ActivityID_1_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_1_week2"]);
                        l_Assessment.ActivityID_2_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_2_week2"]);
                        l_Assessment.ActivityID_3_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_3_week2"]);
                        l_Assessment.ActivityID_4_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_4_week2"]);
                        l_Assessment.ActivityID_5_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_5_week2"]);
                        l_Assessment.ActivityID_6_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_6_week2"]);
                        l_Assessment.ActivityID_7_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_7_week2"]);
                        l_Assessment.ActivityID_8_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_8_week2"]);
                        l_Assessment.ActivityID_9_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_9_week2"]);
                        l_Assessment.ActivityID_10_week2 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_10_week2"]);
                        l_Assessment.ActivityID_1_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_1_week3"]);
                        l_Assessment.ActivityID_2_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_2_week3"]);
                        l_Assessment.ActivityID_3_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_3_week3"]);
                        l_Assessment.ActivityID_4_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_4_week3"]);
                        l_Assessment.ActivityID_5_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_5_week3"]);
                        l_Assessment.ActivityID_6_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_6_week3"]);
                        l_Assessment.ActivityID_7_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_7_week3"]);
                        l_Assessment.ActivityID_8_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_8_week3"]);
                        l_Assessment.ActivityID_9_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_9_week3"]);
                        l_Assessment.ActivityID_10_week3 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_10_week3"]);
                        l_Assessment.ActivityID_1_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_1_week4"]);
                        l_Assessment.ActivityID_2_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_2_week4"]);
                        l_Assessment.ActivityID_3_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_3_week4"]);
                        l_Assessment.ActivityID_4_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_4_week4"]);
                        l_Assessment.ActivityID_5_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_5_week4"]);
                        l_Assessment.ActivityID_6_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_6_week4"]);
                        l_Assessment.ActivityID_7_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_7_week4"]);
                        l_Assessment.ActivityID_8_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_8_week4"]);
                        l_Assessment.ActivityID_9_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_9_week4"]);
                        l_Assessment.ActivityID_10_week4 = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ActivityID_10_week4"]);

                        l_Assessment.Sunday_1_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_1_week1"]);
                        l_Assessment.Sunday_2_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_2_week1"]);
                        l_Assessment.Sunday_3_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_3_week1"]);
                        l_Assessment.Sunday_4_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_4_week1"]);
                        l_Assessment.Sunday_5_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_5_week1"]);
                        l_Assessment.Sunday_6_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_6_week1"]);
                        l_Assessment.Sunday_7_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_7_week1"]);
                        l_Assessment.Sunday_8_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_8_week1"]);
                        l_Assessment.Sunday_9_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_9_week1"]);
                        l_Assessment.Sunday_10_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_10_week1"]);
                        l_Assessment.Sunday_1_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_1_week2"]);
                        l_Assessment.Sunday_2_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_2_week2"]);
                        l_Assessment.Sunday_3_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_3_week2"]);
                        l_Assessment.Sunday_4_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_4_week2"]);
                        l_Assessment.Sunday_5_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_5_week2"]);
                        l_Assessment.Sunday_6_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_6_week2"]);
                        l_Assessment.Sunday_7_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_7_week2"]);
                        l_Assessment.Sunday_8_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_8_week2"]);
                        l_Assessment.Sunday_9_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_9_week2"]);
                        l_Assessment.Sunday_10_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_10_week2"]);
                        l_Assessment.Sunday_1_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_1_week3"]);
                        l_Assessment.Sunday_2_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_2_week3"]);
                        l_Assessment.Sunday_3_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_3_week3"]);
                        l_Assessment.Sunday_4_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_4_week3"]);
                        l_Assessment.Sunday_5_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_5_week3"]);
                        l_Assessment.Sunday_6_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_6_week3"]);
                        l_Assessment.Sunday_7_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_7_week3"]);
                        l_Assessment.Sunday_8_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_8_week3"]);
                        l_Assessment.Sunday_9_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_9_week3"]);
                        l_Assessment.Sunday_10_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_10_week3"]);
                        l_Assessment.Sunday_1_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_1_week4"]);
                        l_Assessment.Sunday_2_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_2_week4"]);
                        l_Assessment.Sunday_3_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_3_week4"]);
                        l_Assessment.Sunday_4_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_4_week4"]);
                        l_Assessment.Sunday_5_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_5_week4"]);
                        l_Assessment.Sunday_6_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_6_week4"]);
                        l_Assessment.Sunday_7_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_7_week4"]);
                        l_Assessment.Sunday_8_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_8_week4"]);
                        l_Assessment.Sunday_9_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_9_week4"]);
                        l_Assessment.Sunday_10_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Sunday_10_week4"]);

                        l_Assessment.Monday_1_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_1_week1"]);
                        l_Assessment.Monday_2_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_2_week1"]);
                        l_Assessment.Monday_3_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_3_week1"]);
                        l_Assessment.Monday_4_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_4_week1"]);
                        l_Assessment.Monday_5_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_5_week1"]);
                        l_Assessment.Monday_6_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_6_week1"]);
                        l_Assessment.Monday_7_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_7_week1"]);
                        l_Assessment.Monday_8_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_8_week1"]);
                        l_Assessment.Monday_9_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_9_week1"]);
                        l_Assessment.Monday_10_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_10_week1"]);
                        l_Assessment.Monday_1_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_1_week2"]);
                        l_Assessment.Monday_2_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_2_week2"]);
                        l_Assessment.Monday_3_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_3_week2"]);
                        l_Assessment.Monday_4_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_4_week2"]);
                        l_Assessment.Monday_5_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_5_week2"]);
                        l_Assessment.Monday_6_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_6_week2"]);
                        l_Assessment.Monday_7_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_7_week2"]);
                        l_Assessment.Monday_8_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_8_week2"]);
                        l_Assessment.Monday_9_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_9_week2"]);
                        l_Assessment.Monday_10_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_10_week2"]);
                        l_Assessment.Monday_1_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_1_week3"]);
                        l_Assessment.Monday_2_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_2_week3"]);
                        l_Assessment.Monday_3_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_3_week3"]);
                        l_Assessment.Monday_4_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_4_week3"]);
                        l_Assessment.Monday_5_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_5_week3"]);
                        l_Assessment.Monday_6_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_6_week3"]);
                        l_Assessment.Monday_7_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_7_week3"]);
                        l_Assessment.Monday_8_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_8_week3"]);
                        l_Assessment.Monday_9_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_9_week3"]);
                        l_Assessment.Monday_10_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_10_week3"]);
                        l_Assessment.Monday_1_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_1_week4"]);
                        l_Assessment.Monday_2_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_2_week4"]);
                        l_Assessment.Monday_3_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_3_week4"]);
                        l_Assessment.Monday_4_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_4_week4"]);
                        l_Assessment.Monday_5_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_5_week4"]);
                        l_Assessment.Monday_6_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_6_week4"]);
                        l_Assessment.Monday_7_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_7_week4"]);
                        l_Assessment.Monday_8_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_8_week4"]);
                        l_Assessment.Monday_9_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_9_week4"]);
                        l_Assessment.Monday_10_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Monday_10_week4"]);

                        l_Assessment.Tuesday_1_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_1_week1"]);
                        l_Assessment.Tuesday_2_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_2_week1"]);
                        l_Assessment.Tuesday_3_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_3_week1"]);
                        l_Assessment.Tuesday_4_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_4_week1"]);
                        l_Assessment.Tuesday_5_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_5_week1"]);
                        l_Assessment.Tuesday_6_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_6_week1"]);
                        l_Assessment.Tuesday_7_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_7_week1"]);
                        l_Assessment.Tuesday_8_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_8_week1"]);
                        l_Assessment.Tuesday_9_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_9_week1"]);
                        l_Assessment.Tuesday_10_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_10_week1"]);
                        l_Assessment.Tuesday_1_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_1_week2"]);
                        l_Assessment.Tuesday_2_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_2_week2"]);
                        l_Assessment.Tuesday_3_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_3_week2"]);
                        l_Assessment.Tuesday_4_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_4_week2"]);
                        l_Assessment.Tuesday_5_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_5_week2"]);
                        l_Assessment.Tuesday_6_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_6_week2"]);
                        l_Assessment.Tuesday_7_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_7_week2"]);
                        l_Assessment.Tuesday_8_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_8_week2"]);
                        l_Assessment.Tuesday_9_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_9_week2"]);
                        l_Assessment.Tuesday_10_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_10_week2"]);
                        l_Assessment.Tuesday_1_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_1_week3"]);
                        l_Assessment.Tuesday_2_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_2_week3"]);
                        l_Assessment.Tuesday_3_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_3_week3"]);
                        l_Assessment.Tuesday_4_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_4_week3"]);
                        l_Assessment.Tuesday_5_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_5_week3"]);
                        l_Assessment.Tuesday_6_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_6_week3"]);
                        l_Assessment.Tuesday_7_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_7_week3"]);
                        l_Assessment.Tuesday_8_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_8_week3"]);
                        l_Assessment.Tuesday_9_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_9_week3"]);
                        l_Assessment.Tuesday_10_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_10_week3"]);
                        l_Assessment.Tuesday_1_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_1_week4"]);
                        l_Assessment.Tuesday_2_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_2_week4"]);
                        l_Assessment.Tuesday_3_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_3_week4"]);
                        l_Assessment.Tuesday_4_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_4_week4"]);
                        l_Assessment.Tuesday_5_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_5_week4"]);
                        l_Assessment.Tuesday_6_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_6_week4"]);
                        l_Assessment.Tuesday_7_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_7_week4"]);
                        l_Assessment.Tuesday_8_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_8_week4"]);
                        l_Assessment.Tuesday_9_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_9_week4"]);
                        l_Assessment.Tuesday_10_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Tuesday_10_week4"]);

                        l_Assessment.Wednesday_1_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_1_week1"]);
                        l_Assessment.Wednesday_2_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_2_week1"]);
                        l_Assessment.Wednesday_3_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_3_week1"]);
                        l_Assessment.Wednesday_4_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_4_week1"]);
                        l_Assessment.Wednesday_5_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_5_week1"]);
                        l_Assessment.Wednesday_6_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_6_week1"]);
                        l_Assessment.Wednesday_7_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_7_week1"]);
                        l_Assessment.Wednesday_8_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_8_week1"]);
                        l_Assessment.Wednesday_9_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_9_week1"]);
                        l_Assessment.Wednesday_10_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_10_week1"]);
                        l_Assessment.Wednesday_1_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_1_week2"]);
                        l_Assessment.Wednesday_2_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_2_week2"]);
                        l_Assessment.Wednesday_3_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_3_week2"]);
                        l_Assessment.Wednesday_4_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_4_week2"]);
                        l_Assessment.Wednesday_5_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_5_week2"]);
                        l_Assessment.Wednesday_6_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_6_week2"]);
                        l_Assessment.Wednesday_7_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_7_week2"]);
                        l_Assessment.Wednesday_8_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_8_week2"]);
                        l_Assessment.Wednesday_9_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_9_week2"]);
                        l_Assessment.Wednesday_10_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_10_week2"]);
                        l_Assessment.Wednesday_1_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_1_week3"]);
                        l_Assessment.Wednesday_2_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_2_week3"]);
                        l_Assessment.Wednesday_3_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_3_week3"]);
                        l_Assessment.Wednesday_4_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_4_week3"]);
                        l_Assessment.Wednesday_5_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_5_week3"]);
                        l_Assessment.Wednesday_6_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_6_week3"]);
                        l_Assessment.Wednesday_7_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_7_week3"]);
                        l_Assessment.Wednesday_8_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_8_week3"]);
                        l_Assessment.Wednesday_9_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_9_week3"]);
                        l_Assessment.Wednesday_10_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_10_week3"]);
                        l_Assessment.Wednesday_1_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_1_week4"]);
                        l_Assessment.Wednesday_2_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_2_week4"]);
                        l_Assessment.Wednesday_3_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_3_week4"]);
                        l_Assessment.Wednesday_4_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_4_week4"]);
                        l_Assessment.Wednesday_5_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_5_week4"]);
                        l_Assessment.Wednesday_6_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_6_week4"]);
                        l_Assessment.Wednesday_7_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_7_week4"]);
                        l_Assessment.Wednesday_8_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_8_week4"]);
                        l_Assessment.Wednesday_9_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_9_week4"]);
                        l_Assessment.Wednesday_10_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Wednesday_10_week4"]);

                        l_Assessment.Thursday_1_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_1_week1"]);
                        l_Assessment.Thursday_2_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_2_week1"]);
                        l_Assessment.Thursday_3_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_3_week1"]);
                        l_Assessment.Thursday_4_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_4_week1"]);
                        l_Assessment.Thursday_5_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_5_week1"]);
                        l_Assessment.Thursday_6_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_6_week1"]);
                        l_Assessment.Thursday_7_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_7_week1"]);
                        l_Assessment.Thursday_8_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_8_week1"]);
                        l_Assessment.Thursday_9_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_9_week1"]);
                        l_Assessment.Thursday_10_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_10_week1"]);
                        l_Assessment.Thursday_1_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_1_week2"]);
                        l_Assessment.Thursday_2_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_2_week2"]);
                        l_Assessment.Thursday_3_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_3_week2"]);
                        l_Assessment.Thursday_4_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_4_week2"]);
                        l_Assessment.Thursday_5_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_5_week2"]);
                        l_Assessment.Thursday_6_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_6_week2"]);
                        l_Assessment.Thursday_7_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_7_week2"]);
                        l_Assessment.Thursday_8_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_8_week2"]);
                        l_Assessment.Thursday_9_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_9_week2"]);
                        l_Assessment.Thursday_10_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_10_week2"]);
                        l_Assessment.Thursday_1_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_1_week3"]);
                        l_Assessment.Thursday_2_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_2_week3"]);
                        l_Assessment.Thursday_3_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_3_week3"]);
                        l_Assessment.Thursday_4_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_4_week3"]);
                        l_Assessment.Thursday_5_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_5_week3"]);
                        l_Assessment.Thursday_6_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_6_week3"]);
                        l_Assessment.Thursday_7_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_7_week3"]);
                        l_Assessment.Thursday_8_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_8_week3"]);
                        l_Assessment.Thursday_9_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_9_week3"]);
                        l_Assessment.Thursday_10_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_10_week3"]);
                        l_Assessment.Thursday_1_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_1_week4"]);
                        l_Assessment.Thursday_2_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_2_week4"]);
                        l_Assessment.Thursday_3_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_3_week4"]);
                        l_Assessment.Thursday_4_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_4_week4"]);
                        l_Assessment.Thursday_5_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_5_week4"]);
                        l_Assessment.Thursday_6_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_6_week4"]);
                        l_Assessment.Thursday_7_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_7_week4"]);
                        l_Assessment.Thursday_8_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_8_week4"]);
                        l_Assessment.Thursday_9_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_9_week4"]);
                        l_Assessment.Thursday_10_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Thursday_10_week4"]);

                        l_Assessment.Friday_1_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_1_week1"]);
                        l_Assessment.Friday_2_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_2_week1"]);
                        l_Assessment.Friday_3_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_3_week1"]);
                        l_Assessment.Friday_4_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_4_week1"]);
                        l_Assessment.Friday_5_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_5_week1"]);
                        l_Assessment.Friday_6_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_6_week1"]);
                        l_Assessment.Friday_7_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_7_week1"]);
                        l_Assessment.Friday_8_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_8_week1"]);
                        l_Assessment.Friday_9_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_9_week1"]);
                        l_Assessment.Friday_10_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_10_week1"]);
                        l_Assessment.Friday_1_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_1_week2"]);
                        l_Assessment.Friday_2_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_2_week2"]);
                        l_Assessment.Friday_3_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_3_week2"]);
                        l_Assessment.Friday_4_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_4_week2"]);
                        l_Assessment.Friday_5_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_5_week2"]);
                        l_Assessment.Friday_6_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_6_week2"]);
                        l_Assessment.Friday_7_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_7_week2"]);
                        l_Assessment.Friday_8_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_8_week2"]);
                        l_Assessment.Friday_9_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_9_week2"]);
                        l_Assessment.Friday_10_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_10_week2"]);
                        l_Assessment.Friday_1_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_1_week3"]);
                        l_Assessment.Friday_2_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_2_week3"]);
                        l_Assessment.Friday_3_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_3_week3"]);
                        l_Assessment.Friday_4_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_4_week3"]);
                        l_Assessment.Friday_5_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_5_week3"]);
                        l_Assessment.Friday_6_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_6_week3"]);
                        l_Assessment.Friday_7_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_7_week3"]);
                        l_Assessment.Friday_8_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_8_week3"]);
                        l_Assessment.Friday_9_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_9_week3"]);
                        l_Assessment.Friday_10_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_10_week3"]);
                        l_Assessment.Friday_1_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_1_week4"]);
                        l_Assessment.Friday_2_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_2_week4"]);
                        l_Assessment.Friday_3_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_3_week4"]);
                        l_Assessment.Friday_4_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_4_week4"]);
                        l_Assessment.Friday_5_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_5_week4"]);
                        l_Assessment.Friday_6_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_6_week4"]);
                        l_Assessment.Friday_7_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_7_week4"]);
                        l_Assessment.Friday_8_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_8_week4"]);
                        l_Assessment.Friday_9_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_9_week4"]);
                        l_Assessment.Friday_10_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Friday_10_week4"]);

                        l_Assessment.Saturday_1_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_1_week1"]);
                        l_Assessment.Saturday_2_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_2_week1"]);
                        l_Assessment.Saturday_3_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_3_week1"]);
                        l_Assessment.Saturday_4_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_4_week1"]);
                        l_Assessment.Saturday_5_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_5_week1"]);
                        l_Assessment.Saturday_6_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_6_week1"]);
                        l_Assessment.Saturday_7_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_7_week1"]);
                        l_Assessment.Saturday_8_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_8_week1"]);
                        l_Assessment.Saturday_9_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_9_week1"]);
                        l_Assessment.Saturday_10_week1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_10_week1"]);
                        l_Assessment.Saturday_1_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_1_week2"]);
                        l_Assessment.Saturday_2_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_2_week2"]);
                        l_Assessment.Saturday_3_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_3_week2"]);
                        l_Assessment.Saturday_4_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_4_week2"]);
                        l_Assessment.Saturday_5_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_5_week2"]);
                        l_Assessment.Saturday_6_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_6_week2"]);
                        l_Assessment.Saturday_7_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_7_week2"]);
                        l_Assessment.Saturday_8_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_8_week2"]);
                        l_Assessment.Saturday_9_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_9_week2"]);
                        l_Assessment.Saturday_10_week2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_10_week2"]);
                        l_Assessment.Saturday_1_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_1_week3"]);
                        l_Assessment.Saturday_2_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_2_week3"]);
                        l_Assessment.Saturday_3_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_3_week3"]);
                        l_Assessment.Saturday_4_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_4_week3"]);
                        l_Assessment.Saturday_5_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_5_week3"]);
                        l_Assessment.Saturday_6_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_6_week3"]);
                        l_Assessment.Saturday_7_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_7_week3"]);
                        l_Assessment.Saturday_8_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_8_week3"]);
                        l_Assessment.Saturday_9_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_9_week3"]);
                        l_Assessment.Saturday_10_week3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_10_week3"]);
                        l_Assessment.Saturday_1_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_1_week4"]);
                        l_Assessment.Saturday_2_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_2_week4"]);
                        l_Assessment.Saturday_3_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_3_week4"]);
                        l_Assessment.Saturday_4_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_4_week4"]);
                        l_Assessment.Saturday_5_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_5_week4"]);
                        l_Assessment.Saturday_6_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_6_week4"]);
                        l_Assessment.Saturday_7_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_7_week4"]);
                        l_Assessment.Saturday_8_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_8_week4"]);
                        l_Assessment.Saturday_9_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_9_week4"]);
                        l_Assessment.Saturday_10_week4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[index]["Saturday_10_week4"]);


                        l_Assessments.Add(l_Assessment);
                    }

                }
            }
            catch (Exception ex)
            {
                exception = "getmike |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            return l_Assessments;
        }

        public static void updateExcerciseActivity_mike(ExcerciseActivityDetailModel_mike p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Update_Excercise_Activity_Detail_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", p_Model.Id);
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Residentid);
                l_Cmd.Parameters.AddWithValue("@modified_by", p_Model.EnteredBy);
                l_Cmd.Parameters.AddWithValue("@modified_on", p_Model.DateEntered);

                l_Cmd.Parameters.AddWithValue("@ActivityName_1_week1", p_Model.ActivityName_1_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_1_week1", p_Model.ActivityID_1_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_1_week1", p_Model.Sunday_1_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_1_week1", p_Model.Monday_1_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_1_week1", p_Model.Tuesday_1_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_1_week1", p_Model.Wednesday_1_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_1_week1", p_Model.Thursday_1_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_1_week1", p_Model.Friday_1_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_1_week1", p_Model.Saturday_1_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_2_week1", p_Model.ActivityName_2_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_2_week1", p_Model.ActivityID_2_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_2_week1", p_Model.Sunday_2_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_2_week1", p_Model.Monday_2_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_2_week1", p_Model.Tuesday_2_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_2_week1", p_Model.Wednesday_2_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_2_week1", p_Model.Thursday_2_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_2_week1", p_Model.Friday_2_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_2_week1", p_Model.Saturday_2_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_3_week1", p_Model.ActivityName_3_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_3_week1", p_Model.ActivityID_3_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_3_week1", p_Model.Sunday_3_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_3_week1", p_Model.Monday_3_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_3_week1", p_Model.Tuesday_3_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_3_week1", p_Model.Wednesday_3_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_3_week1", p_Model.Thursday_3_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_3_week1", p_Model.Friday_3_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_3_week1", p_Model.Saturday_3_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_4_week1", p_Model.ActivityName_4_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_4_week1", p_Model.ActivityID_4_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_4_week1", p_Model.Sunday_4_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_4_week1", p_Model.Monday_4_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_4_week1", p_Model.Tuesday_4_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_4_week1", p_Model.Wednesday_4_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_4_week1", p_Model.Thursday_4_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_4_week1", p_Model.Friday_4_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_4_week1", p_Model.Saturday_4_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_5_week1", p_Model.ActivityName_5_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_5_week1", p_Model.ActivityID_5_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_5_week1", p_Model.Sunday_5_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_5_week1", p_Model.Monday_5_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_5_week1", p_Model.Tuesday_5_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_5_week1", p_Model.Wednesday_5_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_5_week1", p_Model.Thursday_5_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_5_week1", p_Model.Friday_5_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_5_week1", p_Model.Saturday_5_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_6_week1", p_Model.ActivityName_6_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_6_week1", p_Model.ActivityID_6_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_6_week1", p_Model.Sunday_6_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_6_week1", p_Model.Monday_6_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_6_week1", p_Model.Tuesday_6_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_6_week1", p_Model.Wednesday_6_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_6_week1", p_Model.Thursday_6_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_6_week1", p_Model.Friday_6_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_6_week1", p_Model.Saturday_6_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_7_week1", p_Model.ActivityName_7_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_7_week1", p_Model.ActivityID_7_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_7_week1", p_Model.Sunday_7_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_7_week1", p_Model.Monday_7_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_7_week1", p_Model.Tuesday_7_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_7_week1", p_Model.Wednesday_7_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_7_week1", p_Model.Thursday_7_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_7_week1", p_Model.Friday_7_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_7_week1", p_Model.Saturday_7_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_8_week1", p_Model.ActivityName_8_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_8_week1", p_Model.ActivityID_8_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_8_week1", p_Model.Sunday_8_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_8_week1", p_Model.Monday_8_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_8_week1", p_Model.Tuesday_8_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_8_week1", p_Model.Wednesday_8_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_8_week1", p_Model.Thursday_8_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_8_week1", p_Model.Friday_8_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_8_week1", p_Model.Saturday_8_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_9_week1", p_Model.ActivityName_9_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_9_week1", p_Model.ActivityID_9_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_9_week1", p_Model.Sunday_9_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_9_week1", p_Model.Monday_9_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_9_week1", p_Model.Tuesday_9_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_9_week1", p_Model.Wednesday_9_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_9_week1", p_Model.Thursday_9_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_9_week1", p_Model.Friday_9_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_9_week1", p_Model.Saturday_9_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_10_week1", p_Model.ActivityName_10_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityID_10_week1", p_Model.ActivityID_10_week1);
                l_Cmd.Parameters.AddWithValue("@Sunday_10_week1", p_Model.Sunday_10_week1);
                l_Cmd.Parameters.AddWithValue("@Monday_10_week1", p_Model.Monday_10_week1);
                l_Cmd.Parameters.AddWithValue("@Tuesday_10_week1", p_Model.Tuesday_10_week1);
                l_Cmd.Parameters.AddWithValue("@Wednesday_10_week1", p_Model.Wednesday_10_week1);
                l_Cmd.Parameters.AddWithValue("@Thursday_10_week1", p_Model.Thursday_10_week1);
                l_Cmd.Parameters.AddWithValue("@Friday_10_week1", p_Model.Friday_10_week1);
                l_Cmd.Parameters.AddWithValue("@Saturday_10_week1", p_Model.Saturday_10_week1);
                l_Cmd.Parameters.AddWithValue("@ActivityName_1_week2", p_Model.ActivityName_1_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_1_week2", p_Model.ActivityID_1_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_1_week2", p_Model.Sunday_1_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_1_week2", p_Model.Monday_1_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_1_week2", p_Model.Tuesday_1_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_1_week2", p_Model.Wednesday_1_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_1_week2", p_Model.Thursday_1_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_1_week2", p_Model.Friday_1_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_1_week2", p_Model.Saturday_1_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_2_week2", p_Model.ActivityName_2_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_2_week2", p_Model.ActivityID_2_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_2_week2", p_Model.Sunday_2_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_2_week2", p_Model.Monday_2_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_2_week2", p_Model.Tuesday_2_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_2_week2", p_Model.Wednesday_2_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_2_week2", p_Model.Thursday_2_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_2_week2", p_Model.Friday_2_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_2_week2", p_Model.Saturday_2_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_3_week2", p_Model.ActivityName_3_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_3_week2", p_Model.ActivityID_3_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_3_week2", p_Model.Sunday_3_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_3_week2", p_Model.Monday_3_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_3_week2", p_Model.Tuesday_3_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_3_week2", p_Model.Wednesday_3_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_3_week2", p_Model.Thursday_3_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_3_week2", p_Model.Friday_3_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_3_week2", p_Model.Saturday_3_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_4_week2", p_Model.ActivityName_4_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_4_week2", p_Model.ActivityID_4_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_4_week2", p_Model.Sunday_4_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_4_week2", p_Model.Monday_4_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_4_week2", p_Model.Tuesday_4_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_4_week2", p_Model.Wednesday_4_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_4_week2", p_Model.Thursday_4_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_4_week2", p_Model.Friday_4_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_4_week2", p_Model.Saturday_4_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_5_week2", p_Model.ActivityName_5_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_5_week2", p_Model.ActivityID_5_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_5_week2", p_Model.Sunday_5_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_5_week2", p_Model.Monday_5_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_5_week2", p_Model.Tuesday_5_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_5_week2", p_Model.Wednesday_5_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_5_week2", p_Model.Thursday_5_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_5_week2", p_Model.Friday_5_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_5_week2", p_Model.Saturday_5_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_6_week2", p_Model.ActivityName_6_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_6_week2", p_Model.ActivityID_6_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_6_week2", p_Model.Sunday_6_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_6_week2", p_Model.Monday_6_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_6_week2", p_Model.Tuesday_6_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_6_week2", p_Model.Wednesday_6_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_6_week2", p_Model.Thursday_6_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_6_week2", p_Model.Friday_6_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_6_week2", p_Model.Saturday_6_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_7_week2", p_Model.ActivityName_7_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_7_week2", p_Model.ActivityID_7_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_7_week2", p_Model.Sunday_7_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_7_week2", p_Model.Monday_7_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_7_week2", p_Model.Tuesday_7_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_7_week2", p_Model.Wednesday_7_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_7_week2", p_Model.Thursday_7_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_7_week2", p_Model.Friday_7_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_7_week2", p_Model.Saturday_7_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_8_week2", p_Model.ActivityName_8_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_8_week2", p_Model.ActivityID_8_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_8_week2", p_Model.Sunday_8_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_8_week2", p_Model.Monday_8_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_8_week2", p_Model.Tuesday_8_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_8_week2", p_Model.Wednesday_8_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_8_week2", p_Model.Thursday_8_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_8_week2", p_Model.Friday_8_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_8_week2", p_Model.Saturday_8_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_9_week2", p_Model.ActivityName_9_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_9_week2", p_Model.ActivityID_9_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_9_week2", p_Model.Sunday_9_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_9_week2", p_Model.Monday_9_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_9_week2", p_Model.Tuesday_9_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_9_week2", p_Model.Wednesday_9_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_9_week2", p_Model.Thursday_9_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_9_week2", p_Model.Friday_9_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_9_week2", p_Model.Saturday_9_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_10_week2", p_Model.ActivityName_10_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityID_10_week2", p_Model.ActivityID_10_week2);
                l_Cmd.Parameters.AddWithValue("@Sunday_10_week2", p_Model.Sunday_10_week2);
                l_Cmd.Parameters.AddWithValue("@Monday_10_week2", p_Model.Monday_10_week2);
                l_Cmd.Parameters.AddWithValue("@Tuesday_10_week2", p_Model.Tuesday_10_week2);
                l_Cmd.Parameters.AddWithValue("@Wednesday_10_week2", p_Model.Wednesday_10_week2);
                l_Cmd.Parameters.AddWithValue("@Thursday_10_week2", p_Model.Thursday_10_week2);
                l_Cmd.Parameters.AddWithValue("@Friday_10_week2", p_Model.Friday_10_week2);
                l_Cmd.Parameters.AddWithValue("@Saturday_10_week2", p_Model.Saturday_10_week2);
                l_Cmd.Parameters.AddWithValue("@ActivityName_1_week3", p_Model.ActivityName_1_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_1_week3", p_Model.ActivityID_1_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_1_week3", p_Model.Sunday_1_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_1_week3", p_Model.Monday_1_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_1_week3", p_Model.Tuesday_1_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_1_week3", p_Model.Wednesday_1_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_1_week3", p_Model.Thursday_1_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_1_week3", p_Model.Friday_1_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_1_week3", p_Model.Saturday_1_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_2_week3", p_Model.ActivityName_2_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_2_week3", p_Model.ActivityID_2_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_2_week3", p_Model.Sunday_2_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_2_week3", p_Model.Monday_2_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_2_week3", p_Model.Tuesday_2_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_2_week3", p_Model.Wednesday_2_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_2_week3", p_Model.Thursday_2_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_2_week3", p_Model.Friday_2_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_2_week3", p_Model.Saturday_2_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_3_week3", p_Model.ActivityName_3_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_3_week3", p_Model.ActivityID_3_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_3_week3", p_Model.Sunday_3_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_3_week3", p_Model.Monday_3_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_3_week3", p_Model.Tuesday_3_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_3_week3", p_Model.Wednesday_3_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_3_week3", p_Model.Thursday_3_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_3_week3", p_Model.Friday_3_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_3_week3", p_Model.Saturday_3_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_4_week3", p_Model.ActivityName_4_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_4_week3", p_Model.ActivityID_4_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_4_week3", p_Model.Sunday_4_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_4_week3", p_Model.Monday_4_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_4_week3", p_Model.Tuesday_4_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_4_week3", p_Model.Wednesday_4_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_4_week3", p_Model.Thursday_4_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_4_week3", p_Model.Friday_4_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_4_week3", p_Model.Saturday_4_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_5_week3", p_Model.ActivityName_5_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_5_week3", p_Model.ActivityID_5_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_5_week3", p_Model.Sunday_5_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_5_week3", p_Model.Monday_5_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_5_week3", p_Model.Tuesday_5_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_5_week3", p_Model.Wednesday_5_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_5_week3", p_Model.Thursday_5_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_5_week3", p_Model.Friday_5_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_5_week3", p_Model.Saturday_5_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_6_week3", p_Model.ActivityName_6_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_6_week3", p_Model.ActivityID_6_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_6_week3", p_Model.Sunday_6_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_6_week3", p_Model.Monday_6_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_6_week3", p_Model.Tuesday_6_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_6_week3", p_Model.Wednesday_6_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_6_week3", p_Model.Thursday_6_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_6_week3", p_Model.Friday_6_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_6_week3", p_Model.Saturday_6_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_7_week3", p_Model.ActivityName_7_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_7_week3", p_Model.ActivityID_7_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_7_week3", p_Model.Sunday_7_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_7_week3", p_Model.Monday_7_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_7_week3", p_Model.Tuesday_7_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_7_week3", p_Model.Wednesday_7_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_7_week3", p_Model.Thursday_7_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_7_week3", p_Model.Friday_7_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_7_week3", p_Model.Saturday_7_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_8_week3", p_Model.ActivityName_8_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_8_week3", p_Model.ActivityID_8_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_8_week3", p_Model.Sunday_8_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_8_week3", p_Model.Monday_8_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_8_week3", p_Model.Tuesday_8_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_8_week3", p_Model.Wednesday_8_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_8_week3", p_Model.Thursday_8_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_8_week3", p_Model.Friday_8_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_8_week3", p_Model.Saturday_8_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_9_week3", p_Model.ActivityName_9_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_9_week3", p_Model.ActivityID_9_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_9_week3", p_Model.Sunday_9_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_9_week3", p_Model.Monday_9_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_9_week3", p_Model.Tuesday_9_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_9_week3", p_Model.Wednesday_9_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_9_week3", p_Model.Thursday_9_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_9_week3", p_Model.Friday_9_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_9_week3", p_Model.Saturday_9_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_10_week3", p_Model.ActivityName_10_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityID_10_week3", p_Model.ActivityID_10_week3);
                l_Cmd.Parameters.AddWithValue("@Sunday_10_week3", p_Model.Sunday_10_week3);
                l_Cmd.Parameters.AddWithValue("@Monday_10_week3", p_Model.Monday_10_week3);
                l_Cmd.Parameters.AddWithValue("@Tuesday_10_week3", p_Model.Tuesday_10_week3);
                l_Cmd.Parameters.AddWithValue("@Wednesday_10_week3", p_Model.Wednesday_10_week3);
                l_Cmd.Parameters.AddWithValue("@Thursday_10_week3", p_Model.Thursday_10_week3);
                l_Cmd.Parameters.AddWithValue("@Friday_10_week3", p_Model.Friday_10_week3);
                l_Cmd.Parameters.AddWithValue("@Saturday_10_week3", p_Model.Saturday_10_week3);
                l_Cmd.Parameters.AddWithValue("@ActivityName_1_week4", p_Model.ActivityName_1_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_1_week4", p_Model.ActivityID_1_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_1_week4", p_Model.Sunday_1_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_1_week4", p_Model.Monday_1_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_1_week4", p_Model.Tuesday_1_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_1_week4", p_Model.Wednesday_1_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_1_week4", p_Model.Thursday_1_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_1_week4", p_Model.Friday_1_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_1_week4", p_Model.Saturday_1_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_2_week4", p_Model.ActivityName_2_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_2_week4", p_Model.ActivityID_2_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_2_week4", p_Model.Sunday_2_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_2_week4", p_Model.Monday_2_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_2_week4", p_Model.Tuesday_2_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_2_week4", p_Model.Wednesday_2_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_2_week4", p_Model.Thursday_2_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_2_week4", p_Model.Friday_2_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_2_week4", p_Model.Saturday_2_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_3_week4", p_Model.ActivityName_3_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_3_week4", p_Model.ActivityID_3_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_3_week4", p_Model.Sunday_3_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_3_week4", p_Model.Monday_3_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_3_week4", p_Model.Tuesday_3_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_3_week4", p_Model.Wednesday_3_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_3_week4", p_Model.Thursday_3_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_3_week4", p_Model.Friday_3_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_3_week4", p_Model.Saturday_3_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_4_week4", p_Model.ActivityName_4_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_4_week4", p_Model.ActivityID_4_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_4_week4", p_Model.Sunday_4_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_4_week4", p_Model.Monday_4_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_4_week4", p_Model.Tuesday_4_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_4_week4", p_Model.Wednesday_4_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_4_week4", p_Model.Thursday_4_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_4_week4", p_Model.Friday_4_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_4_week4", p_Model.Saturday_4_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_5_week4", p_Model.ActivityName_5_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_5_week4", p_Model.ActivityID_5_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_5_week4", p_Model.Sunday_5_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_5_week4", p_Model.Monday_5_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_5_week4", p_Model.Tuesday_5_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_5_week4", p_Model.Wednesday_5_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_5_week4", p_Model.Thursday_5_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_5_week4", p_Model.Friday_5_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_5_week4", p_Model.Saturday_5_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_6_week4", p_Model.ActivityName_6_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_6_week4", p_Model.ActivityID_6_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_6_week4", p_Model.Sunday_6_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_6_week4", p_Model.Monday_6_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_6_week4", p_Model.Tuesday_6_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_6_week4", p_Model.Wednesday_6_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_6_week4", p_Model.Thursday_6_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_6_week4", p_Model.Friday_6_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_6_week4", p_Model.Saturday_6_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_7_week4", p_Model.ActivityName_7_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_7_week4", p_Model.ActivityID_7_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_7_week4", p_Model.Sunday_7_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_7_week4", p_Model.Monday_7_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_7_week4", p_Model.Tuesday_7_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_7_week4", p_Model.Wednesday_7_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_7_week4", p_Model.Thursday_7_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_7_week4", p_Model.Friday_7_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_7_week4", p_Model.Saturday_7_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_8_week4", p_Model.ActivityName_8_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_8_week4", p_Model.ActivityID_8_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_8_week4", p_Model.Sunday_8_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_8_week4", p_Model.Monday_8_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_8_week4", p_Model.Tuesday_8_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_8_week4", p_Model.Wednesday_8_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_8_week4", p_Model.Thursday_8_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_8_week4", p_Model.Friday_8_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_8_week4", p_Model.Saturday_8_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_9_week4", p_Model.ActivityName_9_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_9_week4", p_Model.ActivityID_9_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_9_week4", p_Model.Sunday_9_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_9_week4", p_Model.Monday_9_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_9_week4", p_Model.Tuesday_9_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_9_week4", p_Model.Wednesday_9_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_9_week4", p_Model.Thursday_9_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_9_week4", p_Model.Friday_9_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_9_week4", p_Model.Saturday_9_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityName_10_week4", p_Model.ActivityName_10_week4);
                l_Cmd.Parameters.AddWithValue("@ActivityID_10_week4", p_Model.ActivityID_10_week4);
                l_Cmd.Parameters.AddWithValue("@Sunday_10_week4", p_Model.Sunday_10_week4);
                l_Cmd.Parameters.AddWithValue("@Monday_10_week4", p_Model.Monday_10_week4);
                l_Cmd.Parameters.AddWithValue("@Tuesday_10_week4", p_Model.Tuesday_10_week4);
                l_Cmd.Parameters.AddWithValue("@Wednesday_10_week4", p_Model.Wednesday_10_week4);
                l_Cmd.Parameters.AddWithValue("@Thursday_10_week4", p_Model.Thursday_10_week4);
                l_Cmd.Parameters.AddWithValue("@Friday_10_week4", p_Model.Friday_10_week4);
                l_Cmd.Parameters.AddWithValue("@Saturday_10_week4", p_Model.Saturday_10_week4);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "updateExcerciseActivity_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void ADDExcerciseActivity_mike(int residentid, int userid, DateTime sameTime)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_ADD_Excercise_Activity_Detail_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@modified_by", userid);
                l_Cmd.Parameters.AddWithValue("@start_time", sameTime);
                l_Cmd.Parameters.AddWithValue("@end_time", sameTime);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "ADDExcerciseActivity_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<HSEPDetailModel_mike> GetHSEPDetail_mike(int p_ResidentId)
        {
            string exception = string.Empty;

            Collection<HSEPDetailModel_mike> l_Assessments = new Collection<HSEPDetailModel_mike>();
            HSEPDetailModel_mike l_Assessment;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("select * from [tbl_AB_HSEP_Detail_mike] where ResidentId=@ResidentId order by start_time DESC", l_Conn);
                l_Conn.Open();
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= AssesmentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment = new HSEPDetailModel_mike();
                        l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.Residentid = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.StartTime = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["start_time"]);
                        l_Assessment.EndTime = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["end_time"]);
                        l_Assessment.EnteredBy = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_Assessment.EnterDate = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_Assessment.DateOfTeaching_1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_1"]);
                        l_Assessment.SignName_1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_1"]);
                        l_Assessment.DateOfTeaching_2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_2"]);
                        l_Assessment.SignName_2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_2"]);
                        l_Assessment.DateOfTeaching_3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_3"]);
                        l_Assessment.SignName_3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_3"]);
                        l_Assessment.DateOfTeaching_4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_4"]);
                        l_Assessment.SignName_4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_4"]);
                        l_Assessment.DateOfTeaching_5 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_5"]);
                        l_Assessment.SignName_5 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_5"]);
                        l_Assessment.DateOfTeaching_6 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_6"]);
                        l_Assessment.SignName_6 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_6"]);
                        l_Assessment.DateOfTeaching_7 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_7"]);
                        l_Assessment.SignName_7 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_7"]);
                        l_Assessment.DateOfTeaching_8 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_8"]);
                        l_Assessment.SignName_8 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_8"]);
                        l_Assessment.DateOfTeaching_9 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_9"]);
                        l_Assessment.SignName_9 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_9"]);
                        l_Assessment.DateOfTeaching_10 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateOfTeaching_10"]);
                        l_Assessment.SignName_10 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SignName_10"]);

                        l_Assessments.Add(l_Assessment);
                    }

                }
            }
            catch (Exception ex)
            {
                exception = "GetHSEPDetail_mike |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            return l_Assessments;
        }

        public static void UpdateHSEPDetail_mike(HSEPDetailModel_mike p_Model,string userName)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Update_HSEP_Detail_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@Id", p_Model.Id);
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Residentid);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy);
                l_Cmd.Parameters.AddWithValue("@DateEntered", DateTime.Now);
                if (p_Model.DateOfTeaching_1 != "" && p_Model.DateOfTeaching_1 != null && p_Model.SignName_1 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_1", p_Model.DateOfTeaching_1);
                    l_Cmd.Parameters.AddWithValue("@SignName_1", userName);
                }
                else if (p_Model.DateOfTeaching_1 != null && p_Model.SignName_1 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_1", p_Model.DateOfTeaching_1);
                    l_Cmd.Parameters.AddWithValue("@SignName_1", p_Model.SignName_1);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_1", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_1", "");
                }

                if (p_Model.DateOfTeaching_2 != "" && p_Model.DateOfTeaching_2 != null && p_Model.SignName_2 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_2", p_Model.DateOfTeaching_2);
                    l_Cmd.Parameters.AddWithValue("@SignName_2", userName);
                }
                else if (p_Model.DateOfTeaching_2 != null && p_Model.SignName_2 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_2", p_Model.DateOfTeaching_2);
                    l_Cmd.Parameters.AddWithValue("@SignName_2", p_Model.SignName_2);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_2", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_2", "");
                }

                if (p_Model.DateOfTeaching_3 != "" && p_Model.DateOfTeaching_3 != null && p_Model.SignName_3 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_3", p_Model.DateOfTeaching_3);
                    l_Cmd.Parameters.AddWithValue("@SignName_3", userName);
                }
                else if (p_Model.DateOfTeaching_3 != null && p_Model.SignName_3 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_3", p_Model.DateOfTeaching_3);
                    l_Cmd.Parameters.AddWithValue("@SignName_3", p_Model.SignName_3);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_3", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_3", "");
                }

                if (p_Model.DateOfTeaching_4 != "" && p_Model.DateOfTeaching_4 != null && p_Model.SignName_4 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_4", p_Model.DateOfTeaching_4);
                    l_Cmd.Parameters.AddWithValue("@SignName_4", userName);
                }
                else if (p_Model.DateOfTeaching_4 != null && p_Model.SignName_4 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_4", p_Model.DateOfTeaching_4);
                    l_Cmd.Parameters.AddWithValue("@SignName_4", p_Model.SignName_4);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_4", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_4", "");
                }

                if (p_Model.DateOfTeaching_5 != "" && p_Model.DateOfTeaching_5 != null && p_Model.SignName_5 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_5", p_Model.DateOfTeaching_5);
                    l_Cmd.Parameters.AddWithValue("@SignName_5", userName);
                }
                else if (p_Model.DateOfTeaching_5 != null && p_Model.SignName_5 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_5", p_Model.DateOfTeaching_5);
                    l_Cmd.Parameters.AddWithValue("@SignName_5", p_Model.SignName_5);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_5", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_5", "");
                }

                if (p_Model.DateOfTeaching_6 != "" && p_Model.DateOfTeaching_6 != null && p_Model.SignName_6 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_6", p_Model.DateOfTeaching_6);
                    l_Cmd.Parameters.AddWithValue("@SignName_6", userName);
                }
                else if (p_Model.DateOfTeaching_6 != null && p_Model.SignName_6 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_6", p_Model.DateOfTeaching_6);
                    l_Cmd.Parameters.AddWithValue("@SignName_6", p_Model.SignName_6);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_6", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_6", "");
                }

                if (p_Model.DateOfTeaching_7 != "" && p_Model.DateOfTeaching_7 != null && p_Model.SignName_7 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_7", p_Model.DateOfTeaching_7);
                    l_Cmd.Parameters.AddWithValue("@SignName_7", userName);
                }
                else if (p_Model.DateOfTeaching_7 != null && p_Model.SignName_7 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_7", p_Model.DateOfTeaching_7);
                    l_Cmd.Parameters.AddWithValue("@SignName_7", p_Model.SignName_7);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_7", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_7", "");
                }

                if (p_Model.DateOfTeaching_8 != "" && p_Model.DateOfTeaching_8 != null && p_Model.SignName_8 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_8", p_Model.DateOfTeaching_8);
                    l_Cmd.Parameters.AddWithValue("@SignName_8", userName);
                }
                else if (p_Model.DateOfTeaching_8 != null && p_Model.SignName_8 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_8", p_Model.DateOfTeaching_8);
                    l_Cmd.Parameters.AddWithValue("@SignName_8", p_Model.SignName_8);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_8", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_8", "");
                }

                if (p_Model.DateOfTeaching_9 != "" && p_Model.DateOfTeaching_9 != null && p_Model.SignName_9 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_9", p_Model.DateOfTeaching_9);
                    l_Cmd.Parameters.AddWithValue("@SignName_9", userName);
                }
                else if (p_Model.DateOfTeaching_9 != null && p_Model.SignName_9 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_9", p_Model.DateOfTeaching_9);
                    l_Cmd.Parameters.AddWithValue("@SignName_9", p_Model.SignName_9);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_9", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_9", "");
                }

                if (p_Model.DateOfTeaching_10 != "" && p_Model.DateOfTeaching_10 != null && p_Model.SignName_10 == null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_10", p_Model.DateOfTeaching_10);
                    l_Cmd.Parameters.AddWithValue("@SignName_10", userName);
                }
                else if (p_Model.DateOfTeaching_10 != null && p_Model.SignName_10 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_10", p_Model.DateOfTeaching_10);
                    l_Cmd.Parameters.AddWithValue("@SignName_10", p_Model.SignName_10);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@DateOfTeaching_10", "");
                    l_Cmd.Parameters.AddWithValue("@SignName_10", "");
                }

                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "UpdateHSEPDetail_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void ADDHSEPDetail_mike(int residentid, int userid, DateTime sameTime)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_HSEP_Detail_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@start_time", sameTime);
                l_Cmd.Parameters.AddWithValue("@end_time", sameTime);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", userid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "ADDHSEPDetail_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<ExcerciseActivitySummaryModel_mike> GetExcerciseActivitySummary_mike(int p_ResidentId)
        {
            string exception = string.Empty;

            Collection<ExcerciseActivitySummaryModel_mike> l_Assessments = new Collection<ExcerciseActivitySummaryModel_mike>();
            ExcerciseActivitySummaryModel_mike l_Assessment;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("select * from [tbl_AB_Excercise_Activity_Summary_mike] where ResidentId=@ResidentId order by start_time DESC", l_Conn);
                l_Conn.Open();
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= AssesmentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment = new ExcerciseActivitySummaryModel_mike();
                        l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.Residentid = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Assessment.StartTime = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["start_time"]);
                        l_Assessment.EndTime = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["end_time"]);
                        l_Assessment.EnteredBy = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["DateEntered"]);

                        l_Assessment.BaselineDate = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["BaselineDate"]);
                        l_Assessment.BaselineTug = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["BaselineTug"]);
                        l_Assessment.BaselineVPS = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["BaselineVPS"]);

                        l_Assessment.TMonthDate = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["TMonthDate"]);
                        l_Assessment.TMonthTug = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["TMonthTug"]);
                        l_Assessment.TMonthVPS = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["TMonthVPS"]);

                        l_Assessment.SMonthDate = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SMonthDate"]);
                        l_Assessment.SMonthTug = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SMonthTug"]);
                        l_Assessment.SMonthVPS = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["SMonthVPS"]);

                        l_Assessments.Add(l_Assessment);
                    }

                }
            }
            catch (Exception ex)
            {
                exception = "GetExcerciseActivitySummary_mike |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            return l_Assessments;
        }

        public static void Add_ExcerciseSummary_mike(int residentid, int userid, DateTime sameTime)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_Excercise_Activity_Summary_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@start_time", sameTime);
                l_Cmd.Parameters.AddWithValue("@end_time", sameTime);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", userid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "Add_ExcerciseSummary_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static VPS_mike Get_VPS_mike(int p_ResidentId, int ID, int number)
        {
            string exception = string.Empty;
            VPS_mike l_Assessment;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("select * from [tbl_AB_VPS_mike] where ResidentId=@ResidentId and Summary_Table_ID=@STID and Index_in_Summary_Table=@INDEX", l_Conn);
                l_Conn.Open();
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                l_Cmd.Parameters.AddWithValue("@STID", ID);
                l_Cmd.Parameters.AddWithValue("@INDEX", number);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    l_Assessment = new VPS_mike();
                    l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["Id"]);
                    l_Assessment.Residentid = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["ResidentID"]);
                    l_Assessment.Summary_Table_ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["Summary_Table_ID"]);
                    l_Assessment.Index_in_Summary_Table = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["Index_in_Summary_Table"]);
                    l_Assessment.EnteredBy = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["EnteredBy"]);
                    l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[0]["DateEntered"]);
                    l_Assessment.TotalScore = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["Total_Score"]);

                    l_Assessment.radio1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio1"]);
                    l_Assessment.radio2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio2"]);
                    l_Assessment.radio3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio3"]);
                    l_Assessment.radio4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio4"]);
                    l_Assessment.radio5 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio5"]);
                    l_Assessment.radio6 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio6"]);
                    l_Assessment.radio7 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio7"]);
                    l_Assessment.radio8 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio8"]);
                    l_Assessment.radio9 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio9"]);
                    l_Assessment.radio10 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio10"]);
                    l_Assessment.radio11 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio11"]);
                    l_Assessment.radio12 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio12"]);
                    l_Assessment.radio13 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio13"]);
                    l_Assessment.radio14 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio14"]);
                    l_Assessment.radio15 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio15"]);
                    l_Assessment.radio16 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio16"]);
                    l_Assessment.radio17 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio17"]);
                    l_Assessment.radio18 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio18"]);
                    l_Assessment.radio19 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio19"]);
                    l_Assessment.radio20 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio20"]);
                    l_Assessment.radio21 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio21"]);
                    l_Assessment.radio22 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio22"]);
                    l_Assessment.radio23 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio23"]);
                    l_Assessment.radio24 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio24"]);
                    l_Assessment.radio25 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio25"]);
                    l_Assessment.radio26 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio26"]);
                    l_Assessment.radio27 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio27"]);
                    l_Assessment.radio28 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio28"]);
                    l_Assessment.radio29 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio29"]);
                    l_Assessment.radio30 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio30"]);
                    l_Assessment.radio31 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio31"]);
                    l_Assessment.radio32 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio32"]);
                    l_Assessment.radio33 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio33"]);
                    l_Assessment.radio34 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio34"]);
                    l_Assessment.radio35 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio35"]);
                    l_Assessment.radio36 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio36"]);
                    l_Assessment.radio37 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio37"]);
                    l_Assessment.radio38 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio38"]);
                    l_Assessment.radio39 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio39"]);
                    l_Assessment.radio40 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio40"]);
                    l_Assessment.radio41 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio41"]);
                    l_Assessment.radio42 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio42"]);
                    l_Assessment.radio43 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio43"]);
                    l_Assessment.radio44 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio44"]);
                    l_Assessment.radio45 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio45"]);
                    l_Assessment.radio46 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio46"]);
                    l_Assessment.radio47 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio47"]);
                    l_Assessment.radio48 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio48"]);
                    l_Assessment.radio49 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio49"]);
                    l_Assessment.radio50 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["radio50"]);
                }
                else
                {
                    l_Assessment = new VPS_mike();
                }
            }
            catch (Exception ex)
            {
                exception = "Get_VPS_mike |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            return l_Assessment;
        }

        public static void Add_VPS_mike(VPS_mike VPS)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_VPS_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", VPS.Residentid);
                l_Cmd.Parameters.AddWithValue("@STID", VPS.Summary_Table_ID);
                l_Cmd.Parameters.AddWithValue("@INDEX", VPS.Index_in_Summary_Table);
                l_Cmd.Parameters.AddWithValue("@DateEntered", VPS.DateEntered);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", VPS.EnteredBy);
                l_Cmd.Parameters.AddWithValue("@TotalScore", VPS.TotalScore);
                l_Cmd.Parameters.AddWithValue("@radio1", VPS.radio1);
                l_Cmd.Parameters.AddWithValue("@radio2", VPS.radio2);
                l_Cmd.Parameters.AddWithValue("@radio3", VPS.radio3);
                l_Cmd.Parameters.AddWithValue("@radio4", VPS.radio4);
                l_Cmd.Parameters.AddWithValue("@radio5", VPS.radio5);
                l_Cmd.Parameters.AddWithValue("@radio6", VPS.radio6);
                l_Cmd.Parameters.AddWithValue("@radio7", VPS.radio7);
                l_Cmd.Parameters.AddWithValue("@radio8", VPS.radio8);
                l_Cmd.Parameters.AddWithValue("@radio9", VPS.radio9);
                l_Cmd.Parameters.AddWithValue("@radio10", VPS.radio10);
                l_Cmd.Parameters.AddWithValue("@radio11", VPS.radio11);
                l_Cmd.Parameters.AddWithValue("@radio12", VPS.radio12);
                l_Cmd.Parameters.AddWithValue("@radio13", VPS.radio13);
                l_Cmd.Parameters.AddWithValue("@radio14", VPS.radio14);
                l_Cmd.Parameters.AddWithValue("@radio15", VPS.radio15);
                l_Cmd.Parameters.AddWithValue("@radio16", VPS.radio16);
                l_Cmd.Parameters.AddWithValue("@radio17", VPS.radio17);
                l_Cmd.Parameters.AddWithValue("@radio18", VPS.radio18);
                l_Cmd.Parameters.AddWithValue("@radio19", VPS.radio19);
                l_Cmd.Parameters.AddWithValue("@radio20", VPS.radio20);
                l_Cmd.Parameters.AddWithValue("@radio21", VPS.radio21);
                l_Cmd.Parameters.AddWithValue("@radio22", VPS.radio22);
                l_Cmd.Parameters.AddWithValue("@radio23", VPS.radio23);
                l_Cmd.Parameters.AddWithValue("@radio24", VPS.radio24);
                l_Cmd.Parameters.AddWithValue("@radio25", VPS.radio25);
                l_Cmd.Parameters.AddWithValue("@radio26", VPS.radio26);
                l_Cmd.Parameters.AddWithValue("@radio27", VPS.radio27);
                l_Cmd.Parameters.AddWithValue("@radio28", VPS.radio28);
                l_Cmd.Parameters.AddWithValue("@radio29", VPS.radio29);
                l_Cmd.Parameters.AddWithValue("@radio30", VPS.radio30);
                l_Cmd.Parameters.AddWithValue("@radio31", VPS.radio31);
                l_Cmd.Parameters.AddWithValue("@radio32", VPS.radio32);
                l_Cmd.Parameters.AddWithValue("@radio33", VPS.radio33);
                l_Cmd.Parameters.AddWithValue("@radio34", VPS.radio34);
                l_Cmd.Parameters.AddWithValue("@radio35", VPS.radio35);
                l_Cmd.Parameters.AddWithValue("@radio36", VPS.radio36);
                l_Cmd.Parameters.AddWithValue("@radio37", VPS.radio37);
                l_Cmd.Parameters.AddWithValue("@radio38", VPS.radio38);
                l_Cmd.Parameters.AddWithValue("@radio39", VPS.radio39);
                l_Cmd.Parameters.AddWithValue("@radio40", VPS.radio40);
                l_Cmd.Parameters.AddWithValue("@radio41", VPS.radio41);
                l_Cmd.Parameters.AddWithValue("@radio42", VPS.radio42);
                l_Cmd.Parameters.AddWithValue("@radio43", VPS.radio43);
                l_Cmd.Parameters.AddWithValue("@radio44", VPS.radio44);
                l_Cmd.Parameters.AddWithValue("@radio45", VPS.radio45);
                l_Cmd.Parameters.AddWithValue("@radio46", VPS.radio46);
                l_Cmd.Parameters.AddWithValue("@radio47", VPS.radio47);
                l_Cmd.Parameters.AddWithValue("@radio48", VPS.radio48);
                l_Cmd.Parameters.AddWithValue("@radio49", VPS.radio49);
                l_Cmd.Parameters.AddWithValue("@radio50", VPS.radio50);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "Add_VPS_mike |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }

        }

        public static TUG_mike Get_TUG_mike(int p_ResidentId, int ID, int number)
        {
            string exception = string.Empty;
            TUG_mike l_Assessment;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("select * from [tbl_AB_TUG_mike] where ResidentId=@ResidentId and Summary_Table_ID=@STID and Index_in_Summary_Table=@INDEX", l_Conn);
                l_Conn.Open();
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                l_Cmd.Parameters.AddWithValue("@STID", ID);
                l_Cmd.Parameters.AddWithValue("@INDEX", number);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    l_Assessment = new TUG_mike();
                    l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["Id"]);
                    l_Assessment.Residentid = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["ResidentID"]);
                    l_Assessment.Summary_Table_ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["Summary_Table_ID"]);
                    l_Assessment.Index_in_Summary_Table = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["Index_in_Summary_Table"]);
                    l_Assessment.EnteredBy = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["EnteredBy"]);
                    l_Assessment.DateEntered = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[0]["DateEntered"]);
                    l_Assessment.Total_Time = Convert.ToString(AssesmentsReceive.Tables[0].Rows[0]["Total_Time"]);
                    l_Assessment.userName = Convert.ToString(AssesmentsReceive.Tables[0].Rows[0]["userName"]);

                    l_Assessment.check1 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check1"]);
                    l_Assessment.check2 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check2"]);
                    l_Assessment.check3 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check3"]);
                    l_Assessment.check4 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check4"]);
                    l_Assessment.check5 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check5"]);
                    l_Assessment.check6 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check6"]);
                    l_Assessment.check7 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check7"]);
                    l_Assessment.check8 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check8"]);
                    l_Assessment.check9 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check9"]);
                    l_Assessment.check10 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check10"]);
                    l_Assessment.check11 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check11"]);
                    l_Assessment.check12 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check12"]);
                    l_Assessment.check13 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check13"]);
                    l_Assessment.check14 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check14"]);
                    l_Assessment.check15 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check15"]);
                    l_Assessment.check16 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check16"]);
                    l_Assessment.check17 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check17"]);
                    l_Assessment.check18 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check18"]);
                    l_Assessment.check19 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check19"]);
                    l_Assessment.check20 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check20"]);
                    l_Assessment.check21 = Convert.ToBoolean(AssesmentsReceive.Tables[0].Rows[0]["check21"]);

                    l_Assessment.specify1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[0]["specify1"]);
                    l_Assessment.specify2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[0]["specify2"]);
                    l_Assessment.specify3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[0]["specify3"]);
                    l_Assessment.specify4 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[0]["specify4"]);
                }
                else
                {
                    l_Assessment = new TUG_mike();
                }
            }
            catch (Exception ex)
            {
                exception = "Get_TUG_mike |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            return l_Assessment;
        }

        public static void Add_TUG_mike(TUG_mike VPS)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_TUG_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentID", VPS.Residentid);
                l_Cmd.Parameters.AddWithValue("@STID", VPS.Summary_Table_ID);
                l_Cmd.Parameters.AddWithValue("@INDEX", VPS.Index_in_Summary_Table);
                l_Cmd.Parameters.AddWithValue("@DateEntered", VPS.DateEntered);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", VPS.EnteredBy);
                l_Cmd.Parameters.AddWithValue("@Total_Time", VPS.Total_Time);
                l_Cmd.Parameters.AddWithValue("@userName", VPS.userName);

                l_Cmd.Parameters.AddWithValue("@check1", VPS.check1);
                l_Cmd.Parameters.AddWithValue("@check2", VPS.check2);
                l_Cmd.Parameters.AddWithValue("@check3", VPS.check3);
                l_Cmd.Parameters.AddWithValue("@check4", VPS.check4);
                l_Cmd.Parameters.AddWithValue("@check5", VPS.check5);
                l_Cmd.Parameters.AddWithValue("@check6", VPS.check6);
                l_Cmd.Parameters.AddWithValue("@check7", VPS.check7);
                l_Cmd.Parameters.AddWithValue("@check8", VPS.check8);
                l_Cmd.Parameters.AddWithValue("@check9", VPS.check9);
                l_Cmd.Parameters.AddWithValue("@check10", VPS.check10);
                l_Cmd.Parameters.AddWithValue("@check11", VPS.check11);
                l_Cmd.Parameters.AddWithValue("@check12", VPS.check12);
                l_Cmd.Parameters.AddWithValue("@check13", VPS.check13);
                l_Cmd.Parameters.AddWithValue("@check14", VPS.check14);
                l_Cmd.Parameters.AddWithValue("@check15", VPS.check15);
                l_Cmd.Parameters.AddWithValue("@check16", VPS.check16);
                l_Cmd.Parameters.AddWithValue("@check17", VPS.check17);
                l_Cmd.Parameters.AddWithValue("@check18", VPS.check18);
                l_Cmd.Parameters.AddWithValue("@check19", VPS.check19);
                l_Cmd.Parameters.AddWithValue("@check20", VPS.check20);
                l_Cmd.Parameters.AddWithValue("@check21", VPS.check21);
                if (VPS.specify1 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@specify1", VPS.specify1);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@specify1", "");
                }

                if (VPS.specify2 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@specify2", VPS.specify2);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@specify2", "");
                }

                if (VPS.specify3 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@specify3", VPS.specify3);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@specify3", "");
                }

                if (VPS.specify4 != null)
                {
                    l_Cmd.Parameters.AddWithValue("@specify4", VPS.specify4);
                }
                else
                {
                    l_Cmd.Parameters.AddWithValue("@specify4", "");
                }


                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "Add_TUG_mike |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }

        }



        public static Collection<SBSWTL> getSBSWTL(int p_ResidentId,int homeID)
        {
            string exception = string.Empty;

            Collection<SBSWTL> l_Assessments = new Collection<SBSWTL>();
            SBSWTL l_Assessment = new SBSWTL();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_SBSWTL_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentID", p_ResidentId);
                l_Cmd.Parameters.AddWithValue("@homeID", homeID);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= AssesmentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment = new SBSWTL();
                        l_Assessment.ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["ID"]);
                        l_Assessment.residentID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["residentID"]);
                        l_Assessment.start_time = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["start_time"]);
                        l_Assessment.modified_by = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["modified_by"]);
                        l_Assessment.modified_on = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[index]["modified_on"]);
                        l_Assessments.Add(l_Assessment);
                    }
                }       
            }
            catch (Exception ex)
            {
                exception = "getSBSWTL |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }

            for (int a = 0; a< l_Assessments.Count(); a++)
            {
                l_Assessments[a].SBSWTL_List = getSBSWTL_row(l_Assessments[a].ID);
            }


            return l_Assessments;
        }

        public static SBSWTL getSBSWTLbyID(int p_ResidentId, int homeID,int ID)
        {
            string exception = string.Empty;
            SBSWTL l_Assessment = new SBSWTL();

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_SBSWTL_by_ID_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@residentID", p_ResidentId);
                l_Cmd.Parameters.AddWithValue("@homeID", homeID);
                l_Cmd.Parameters.AddWithValue("@ID", ID);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    l_Assessment = new SBSWTL();
                    l_Assessment.ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["ID"]);
                    l_Assessment.residentID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["residentID"]);
                    l_Assessment.start_time = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[0]["start_time"]);
                    l_Assessment.modified_by = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[0]["modified_by"]);
                    l_Assessment.modified_on = Convert.ToDateTime(AssesmentsReceive.Tables[0].Rows[0]["modified_on"]);
                }
            }
            catch (Exception ex)
            {
                exception = "getSBSWTLbyID |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            l_Assessment.SBSWTL_List = getSBSWTL_row(l_Assessment.ID);



            return l_Assessment;
        }

        public static List<SBSWTL_row> getSBSWTL_row(int SBSWTL_Table_ID)
        {
            string exception = string.Empty;

            List<SBSWTL_row> l_Assessments = new List<SBSWTL_row>();
            SBSWTL_row l_Assessment;


            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Get_SBSWTL_row_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@SBSWTL_Table_ID", SBSWTL_Table_ID);
                DataSet AssesmentsReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(AssesmentsReceive);
                if (AssesmentsReceive.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; index <= AssesmentsReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        l_Assessment = new SBSWTL_row();
                        l_Assessment.Id = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Id"]);
                        l_Assessment.SBSWTL_Table_ID = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["SBSWTL_Table_ID"]);
                        l_Assessment.Residentid = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["Residentid"]);
                        l_Assessment.row_index = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["row_index"]);
                        l_Assessment.EnteredBy = Convert.ToInt32(AssesmentsReceive.Tables[0].Rows[index]["EnteredBy"]);
                        l_Assessment.userName = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["userName"]);
                        l_Assessment.userNameType = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["userNameType"]);
                        l_Assessment.DateEntered = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["DateEntered"]);
                        l_Assessment.Bath1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Bath1"]);
                        l_Assessment.Bath2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Bath2"]);
                        l_Assessment.Bath3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Bath3"]);
                        l_Assessment.Shower1 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Shower1"]);
                        l_Assessment.Shower2 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Shower2"]);
                        l_Assessment.Shower3 = Convert.ToString(AssesmentsReceive.Tables[0].Rows[index]["Shower3"]);
                        l_Assessment.userNameAndType = l_Assessment.userName +" "+ l_Assessment.userNameType;
                        l_Assessments.Add(l_Assessment);
                    }
                }

            }
            catch (Exception ex)
            {
                exception = "getSBSWTL_row |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
            return l_Assessments;
        }

        public static void Add_SBSWTL(int residentid, int userid,int homeID)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_SBSWTL", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", userid);
                l_Cmd.Parameters.AddWithValue("@homeID", homeID);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "Add_SBSWTL |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void Update_SBSWTL_row(SBSWTL p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_delete_SBSWTL_row", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ID", p_Model.ID);
                l_Cmd.ExecuteNonQuery();

                foreach (var sample in p_Model.SBSWTL_List)
                {
                    if (sample.EnteredBy != 0)
                    {
                        SqlCommand l_Cmd2 = new SqlCommand("spAB_insert_SBSWTL_row", l_Conn);
                        l_Cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        l_Cmd2.Parameters.AddWithValue("@ID", sample.SBSWTL_Table_ID);
                        l_Cmd2.Parameters.AddWithValue("@residentID", sample.Residentid);
                        l_Cmd2.Parameters.AddWithValue("@row_index", sample.row_index);
                        l_Cmd2.Parameters.AddWithValue("@enterby", sample.EnteredBy);
                        l_Cmd2.Parameters.AddWithValue("@dateentered", sample.DateEntered);
                        l_Cmd2.Parameters.AddWithValue("@barh1", sample.Bath1);
                        l_Cmd2.Parameters.AddWithValue("@barh2", sample.Bath2);
                        l_Cmd2.Parameters.AddWithValue("@barh3", sample.Bath3);
                        l_Cmd2.Parameters.AddWithValue("@shower1", sample.Shower1);
                        l_Cmd2.Parameters.AddWithValue("@shower2", sample.Shower2);
                        l_Cmd2.Parameters.AddWithValue("@shower3", sample.Shower3);
                        l_Cmd2.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = "Add_SBSWTL |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void AddNewCUOL(CUOL p_Model)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_AddCUOL", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeID", p_Model.HomeID);
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_Model.Resident.ID);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_Model.EnteredBy.ID);
                l_Cmd.Parameters.AddWithValue("@Amount", p_Model.Amount);
                l_Cmd.Parameters.AddWithValue("@Color", p_Model.Color);
                l_Cmd.Parameters.AddWithValue("@Comments", p_Model.Comments);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "AddNewCUOL |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static Collection<CUOL> GetCUOLbyID(int p_ResidentId,int homeid)
        {
            string exception = string.Empty;
            Collection<CUOL> l_CUOLs = new Collection<CUOL>();

            ResidentModel l_Resident = new ResidentModel();
            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_GetCUOLbyID", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@homeID", homeid);
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_ResidentId);
                DataSet dataReceive = new DataSet();

                l_DA.SelectCommand = l_Cmd;
                l_DA.Fill(dataReceive);

                if ((dataReceive != null) & dataReceive.Tables.Count > 0)
                {
                    for (int index = 0; index <= dataReceive.Tables[0].Rows.Count - 1; index++)
                    {
                        CUOL l_CUOL = new CUOL();
                        l_CUOL.Id = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["Id"]);
                        l_CUOL.Amount = Convert.ToString(dataReceive.Tables[0].Rows[index]["Amount"]);
                        l_CUOL.Color = Convert.ToString(dataReceive.Tables[0].Rows[index]["Color"]);
                        l_CUOL.Comments = Convert.ToString(dataReceive.Tables[0].Rows[index]["Comments"]);
                        l_Resident.ID = Convert.ToInt32(dataReceive.Tables[0].Rows[index]["ResidentId"]);
                        l_Resident.SuiteNo = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentSuiteNo"]);
                        l_Resident.FirstName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentFirstName"]);
                        l_Resident.LastName = Convert.ToString(dataReceive.Tables[0].Rows[index]["ResidentLastName"]);
                        l_CUOL.Resident = l_Resident;
                        l_CUOL.TimeStamp = Convert.ToDateTime(dataReceive.Tables[0].Rows[index]["dtmTimeStamp"]);
                        l_CUOL.userName = Convert.ToString(dataReceive.Tables[0].Rows[index]["userName"]);
                        l_CUOL.userNameType = Convert.ToString(dataReceive.Tables[0].Rows[index]["userNameType"]);
                        l_CUOLs.Add(l_CUOL);
                    }
                }
                return l_CUOLs;
            }
            catch (Exception ex)
            {
                exception = "AssessmentDAL GetCUOLbyID |" + ex.ToString();
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void SaveFamilyConferenceNote(FamilyConfrenceNoteModel p_FamilyConferenceNote)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_save_FamilyConferenceNote", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", p_FamilyConferenceNote.Resident.ID);
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
                l_Cmd.Parameters.AddWithValue("@EnteredBy", p_FamilyConferenceNote.EnteredBy.ID);

                l_Cmd.Parameters.AddWithValue("@onlyCare", p_FamilyConferenceNote.onlyCare);
                l_Cmd.Parameters.AddWithValue("@Goalsmet", p_FamilyConferenceNote.Goalsmet);
                l_Cmd.Parameters.AddWithValue("@row1", p_FamilyConferenceNote.row1);
                l_Cmd.Parameters.AddWithValue("@row2", p_FamilyConferenceNote.row2);
                l_Cmd.Parameters.AddWithValue("@row3", p_FamilyConferenceNote.row3);
                l_Cmd.Parameters.AddWithValue("@row4", p_FamilyConferenceNote.row4);
                l_Cmd.Parameters.AddWithValue("@row5", p_FamilyConferenceNote.row5);
                l_Cmd.Parameters.AddWithValue("@row6", p_FamilyConferenceNote.row6);
                l_Cmd.Parameters.AddWithValue("@row7", p_FamilyConferenceNote.row7);
                l_Cmd.Parameters.AddWithValue("@row8", p_FamilyConferenceNote.row8);
                l_Cmd.Parameters.AddWithValue("@row9", p_FamilyConferenceNote.row9);
                l_Cmd.Parameters.AddWithValue("@row10", p_FamilyConferenceNote.row10);
                l_Cmd.Parameters.AddWithValue("@row11", p_FamilyConferenceNote.row11);
                l_Cmd.Parameters.AddWithValue("@droprow1", p_FamilyConferenceNote.droprow1);
                l_Cmd.Parameters.AddWithValue("@droprow2", p_FamilyConferenceNote.droprow2);
                l_Cmd.Parameters.AddWithValue("@droprow3", p_FamilyConferenceNote.droprow3);
                l_Cmd.Parameters.AddWithValue("@droprow4", p_FamilyConferenceNote.droprow4);
                l_Cmd.Parameters.AddWithValue("@droprow5", p_FamilyConferenceNote.droprow5);
                l_Cmd.Parameters.AddWithValue("@droprow6", p_FamilyConferenceNote.droprow6);
                l_Cmd.Parameters.AddWithValue("@droprow7", p_FamilyConferenceNote.droprow7);
                l_Cmd.Parameters.AddWithValue("@droprow8", p_FamilyConferenceNote.droprow8);
                l_Cmd.Parameters.AddWithValue("@droprow9", p_FamilyConferenceNote.droprow9);
                l_Cmd.Parameters.AddWithValue("@droprow10", p_FamilyConferenceNote.droprow10);
                l_Cmd.Parameters.AddWithValue("@droprow11", p_FamilyConferenceNote.droprow11);
                l_Cmd.Parameters.AddWithValue("@Attendees1", p_FamilyConferenceNote.Attendees1);
                l_Cmd.Parameters.AddWithValue("@Attendees2", p_FamilyConferenceNote.Attendees2);
                l_Cmd.Parameters.AddWithValue("@Attendees3", p_FamilyConferenceNote.Attendees3);
                l_Cmd.Parameters.AddWithValue("@Attendees4", p_FamilyConferenceNote.Attendees4);
                l_Cmd.Parameters.AddWithValue("@Attendees5", p_FamilyConferenceNote.Attendees5);
                l_Cmd.Parameters.AddWithValue("@Attendees6", p_FamilyConferenceNote.Attendees6);
                l_Cmd.Parameters.AddWithValue("@Attendees7", p_FamilyConferenceNote.Attendees7);
                l_Cmd.Parameters.AddWithValue("@Attendees1rela", p_FamilyConferenceNote.Attendees1rela);
                l_Cmd.Parameters.AddWithValue("@Attendees2rela", p_FamilyConferenceNote.Attendees2rela);
                l_Cmd.Parameters.AddWithValue("@Attendees3rela", p_FamilyConferenceNote.Attendees3rela);
                l_Cmd.Parameters.AddWithValue("@Attendees4rela", p_FamilyConferenceNote.Attendees4rela);
                l_Cmd.Parameters.AddWithValue("@Attendees5rela", p_FamilyConferenceNote.Attendees5rela);
                l_Cmd.Parameters.AddWithValue("@Attendees6rela", p_FamilyConferenceNote.Attendees6rela);
                l_Cmd.Parameters.AddWithValue("@Attendees7rela", p_FamilyConferenceNote.Attendees7rela);
                l_Cmd.Parameters.AddWithValue("@completeName", p_FamilyConferenceNote.completeName);
                l_Cmd.Parameters.AddWithValue("@completeSign", p_FamilyConferenceNote.completeSign);

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

        public static void Add_HeadToToeAssessment_mike(int residentid, int userid)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_HeadToToeAssessment_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", userid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "Add_HeadToToeAssessment_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }

        public static void Add_FallRisk_mike(int residentid, int userid)
        {
            string exception = string.Empty;

            SqlConnection l_Conn = new SqlConnection(Constants.ConnectionString.PROD);
            try
            {
                SqlDataAdapter l_DA = new SqlDataAdapter();
                SqlCommand l_Cmd = new SqlCommand("spAB_Add_FallRisk_mike", l_Conn);
                l_Conn.Open();
                l_Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                l_Cmd.Parameters.AddWithValue("@ResidentId", residentid);
                l_Cmd.Parameters.AddWithValue("@EnteredBy", userid);
                l_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exception = "Add_FallRisk_mike |" + ex.ToString();
                //Log.Write(exception);
                throw;
            }
            finally
            {
                l_Conn.Close();
            }
        }


    }
}