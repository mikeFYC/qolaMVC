USE [test_qola]
GO

/****** Object:  StoredProcedure [dbo].[spAB_Add_CarePlan]    Script Date: 2018-06-08 4:01:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spAB_Add_CarePlan]
@AssessedStatus nvarchar(200) NULL,
@LevelOfCare nvarchar(200) NULL,
@Bpsystolic nvarchar(200) NULL,
@BpDiastolic nvarchar(200) NULL,
@BpDateCompleted nvarchar(200) NULL,
@Temperature nvarchar(200) NULL,
@TemperatureDateCompleted nvarchar(200) NULL,
@Weight_Lbs nvarchar(200) NULL,
@WeightDateCompleted nvarchar(200) NULL,
@HeightCentimeters nvarchar(200) NULL,
@HeightInches nvarchar(200) NULL,
@HeightDateCompleted nvarchar(200) NULL,
@Pulse nvarchar(200) NULL,
@PulsedateCompleted nvarchar(200) NULL,
@Oxygen_02 nvarchar(200) NULL,
@Oxygen_02_rate nvarchar(200) NULL,
@ResidentId nvarchar(200) NULL,
@AmCare nvarchar(200) NULL,
@AmCareAssistedBy nvarchar(200) NULL,
@AmcareAgencyName nvarchar(200) NULL,
@AmCarePreferredTime nvarchar(200) NULL,
@AmCarePreferredType nvarchar(200) NULL,
@PmCare nvarchar(200) NULL,
@PmCareAssistedBy nvarchar(200) NULL,
@PmcareAgencyNPme nvarchar(200) NULL,
@PmCarePreferredTime nvarchar(200) NULL,
@PmCarePreferredType nvarchar(200) NULL,
@BathingCare nvarchar(200) NULL,
@BathingCareAssistedBy nvarchar(200) NULL,
@BathingcareAgencyNBathinge nvarchar(200) NULL,
@BathingCarePreferredTime nvarchar(200) NULL,
@BathingCarePreferredType nvarchar(200) NULL,
@CarePlanLocation nvarchar(200) NULL,
@Dressing nvarchar(200) NULL,
@NailCare nvarchar(200) NULL,
@Shaving nvarchar(200) NULL,
@FootCare nvarchar(200) NULL,
@DressingPreferredTime nvarchar(200) NULL,
@NailCarePreferredTime nvarchar(200) NULL,
@ShavingPreferredTime nvarchar(200) NULL,
@FootCarePreferredTime nvarchar(200) NULL,
@OralHygiene nvarchar(200) NULL,
@OralHygienePreferredTime nvarchar(200) NULL,
@Teeth nvarchar(200) NULL,
@Mobility nvarchar(200) NULL,
@Transfers nvarchar(200) NULL,
@Mechanical_Left nvarchar(200) NULL,
@Walker nvarchar(200) NULL,
@WheelChair nvarchar(200) NULL,
@Cane nvarchar(200) NULL,
@CarePlan_Left nvarchar(200) NULL,
@Scooter nvarchar(200) NULL,
@WalkerType nvarchar(200) NULL,
@WheelChairType nvarchar(200) NULL,
@CaneType nvarchar(200) NULL,
@ScooterType nvarchar(200) NULL,
@Pt nvarchar(200) NULL,
@PtFrequency nvarchar(200) NULL,
@PtProvider nvarchar(200) NULL,
@Ot nvarchar(200) NULL,
@OtFrequency nvarchar(200) NULL,
@OtProvider nvarchar(200) NULL,
@SafetyPasds nvarchar(200) NULL,
@Rails nvarchar(200) NULL,
@Other nvarchar(200) NULL,
@LightOnly nvarchar(200) NULL,
@Lunch nvarchar(200) NULL,
@Dinner nvarchar(200) NULL,
@Breakfast nvarchar(200) NULL,
@Behaviour nvarchar(200) NULL,
@RiskOfHarmToSelf nvarchar(200) NULL,
@Smoker nvarchar(200) NULL,
@CognitiveStatus nvarchar(200) NULL,
@RiskOfWandering nvarchar(200) NULL,
@OtherPhysicalInfo nvarchar(200) NULL,
@CognitiveFunction nvarchar(200) NULL,
@Orientation nvarchar(200) NULL,
@NutritionStatus nvarchar(200) NULL,
@NutritionRisk nvarchar(200) NULL,
@AssistiveDevices nvarchar(200) NULL,
@NutritionTexture nvarchar(200) NULL,
@NutritionOther nvarchar(200) NULL,
@Diet nvarchar(200) NULL,
@OtherDiet nvarchar(200) NULL,
@NutritionOtherPhysicalInfo nvarchar(200) NULL,
@Allergies nvarchar(200) NULL,
@Appetite nvarchar(200) NULL,
@MealsLunch nvarchar(200) NULL,
@MealsDinner nvarchar(200) NULL,
@MealsBreakfast nvarchar(200) NULL,
@Bladder nvarchar(200) NULL,
@Bowel nvarchar(200) NULL,
@ContinenceProductsName nvarchar(200) NULL,
@ContinenceProducts nvarchar(200) NULL,
@ContinenceProductsAssistiveDevices nvarchar(200) NULL,
@ContinenceProductsSupplier nvarchar(200) NULL,
@AssesmentBy nvarchar(200) NULL,
@DateCompleted nvarchar(200) NULL,
@Toileting nvarchar(200) NULL,
@MedicationAssistace nvarchar(200) NULL,
@MedicationAdministration nvarchar(200) NULL,
@MedicationCOmpletedBy nvarchar(200) NULL,
@MedicationAgency nvarchar(200) NULL,
@MedicationPharmacy nvarchar(200) NULL,
@MedicationAllergies nvarchar(200) NULL,
@OtherMedicationAllergies nvarchar(200) NULL,
@MedicationTape nvarchar(200) NULL,
@MedicationHydantoins nvarchar(200) NULL,
@SensoryAbilitiesHearing nvarchar(200) NULL,
@SensoryAbilitiesVision nvarchar(200) NULL,
@Language nvarchar(200) NULL,
@Communication nvarchar(200) NULL,
@CommunicationNotes nvarchar(200) NULL,
@WondCare nvarchar(200) NULL,
@WondCareAssistedBy nvarchar(200) NULL,
@WondCareAgencyName nvarchar(200) NULL,
@SkinCare nvarchar(200) NULL,
@SkinCareTreatments nvarchar(200) NULL,
@SpecialNeedSupplier nvarchar(200) NULL,
@SpecialNeedNotes nvarchar(200) NULL,
@SpecialNeedCpap nvarchar(200) NULL,
@SpecialEquipment nvarchar(200) NULL,
@SpecialEquipmentOtherMentalInfo nvarchar(200) NULL,
@SpecialEquipmentOther nvarchar(200) NULL,
@ResidentFamilyMeeting nvarchar(200) NULL,
@ResidentFamilyInvolvement nvarchar(200) NULL,
@Mantoux nvarchar(200) NULL,
@ChestXray nvarchar(200) NULL,
@Pneumonia nvarchar(200) NULL,
@FluVaccine nvarchar(200) NULL,
@Tetanus nvarchar(200) NULL,
@MantouxDateComplete nvarchar(200) NULL,
@ChestXrayDateComplete nvarchar(200) NULL,
@PneumoniaDateComplete nvarchar(200) NULL,
@FluVaccineDateComplete nvarchar(200) NULL,
@TetanusDateComplete nvarchar(200) NULL

AS
--20180507 chime created
BEGIN
	INSERT INTO [tbl_CarePlan] (AssessedStatus, LevelOfCare, Bpsystolic, BpDiastolic, BpDateCompleted, Temperature, TemperatureDateCompleted, Weight_Lbs, WeightDateCompleted, HeightCentimeters, HeightInches, HeightDateCompleted,Pulse,PulsedateCompleted, Oxygen_02, Oxygen_02_rate,ResidentId,AmCare,AmCareAssistedBy,AmcareAgencyName,AmCarePreferredTime,AmCarePreferredType,PmCare,PmCareAssistedBy,PmcareAgencyNPme,PmCarePreferredTime,PmCarePreferredType,BathingCare,BathingCareAssistedBy,BathingcareAgencyNBathinge,BathingCarePreferredTime,BathingCarePreferredType,CarePlanLocation,Dressing,NailCare,Shaving,FootCare,DressingPreferredTime,NailCarePreferredTime,ShavingPreferredTime,FootCarePreferredTime,OralHygiene,OralHygienePreferredTime,Teeth,Mobility,Transfers,MechanicalLeft,Walker,WheelChair,Cane,CarePlanLeft,Scooter,WalkerType,WheelChairType,CaneType,ScooterType,Pt,PtFrequency,PtProvider,Ot,OtFrequency,OtProvider,SafetyPasds,Rails,Other,LightOnly,Lunch,Dinner,Breakfast,Behaviour,RiskOfHarmToSelf,Smoker,CognitiveStatus,RiskOfWandering,OtherPhysicalInfo,CognitiveFunction,Orientation,NutritionStatus,NutritionRisk,AssistiveDevices,NutritionTexture,NutritionOther,Diet,OtherDiet,NutritionOtherPhysicalInfo,Allergies,Appetite,MealsLunch,MealsDinner,MealsBreakfast,Bladder,Bowel,ContinenceProductsName,ContinenceProducts,ContinenceProductsAssistiveDevices,ContinenceProductsSupplier,AssesmentBy,DateCompleted,Toileting,MedicationAssistace,MedicationAdministration,MedicationCOmpletedBy,MedicationAgency,MedicationPharmacy,MedicationAllergies,OtherMedicationAllergies,MedicationTape,MedicationHydantoins,SensoryAbilitiesHearing,SensoryAbilitiesVision,Language,Communication,CommunicationNotes,WondCare,WondCareAssistedBy,WondCareAgencyName,SkinCare,SkinCareTreatments,SpecialNeedSupplier,SpecialNeedNotes,SpecialNeedCpap,SpecialEquipment,SpecialEquipmentOtherMentalInfo,SpecialEquipmentOther,ResidentFamilyMeeting,ResidentFamilyInvolvement,Mantoux,ChestXray,Pneumonia,FluVaccine,Tetanus,MantouxDateComplete,ChestXrayDateComplete,PneumoniaDateComplete,FluVaccineDateComplete,TetanusDateComplete,date_added) 
	VALUES (@AssessedStatus, @LevelOfCare, @Bpsystolic, @BpDiastolic, @BpDateCompleted,@Temperature,@TemperatureDateCompleted,@Weight_Lbs,@WeightDateCompleted,@HeightCentimeters,@HeightInches,@HeightDateCompleted,@Pulse,@PulsedateCompleted,@Oxygen_02,@Oxygen_02_rate,@ResidentId,@AmCare,@AmCareAssistedBy,@AmcareAgencyName,@AmCarePreferredTime,@AmCarePreferredType,@PmCare,@PmCareAssistedBy,@PmcareAgencyNPme,@PmCarePreferredTime,@PmCarePreferredType,@BathingCare,@BathingCareAssistedBy,@BathingcareAgencyNBathinge,@BathingCarePreferredTime,@BathingCarePreferredType,@CarePlanLocation,@Dressing,@NailCare,@Shaving,@FootCare,@DressingPreferredTime,@NailCarePreferredTime,
@ShavingPreferredTime,@FootCarePreferredTime,@OralHygiene,@OralHygienePreferredTime,@Teeth,@Mobility,@Transfers,@Mechanical_Left,@Walker,@WheelChair,@Cane,@CarePlan_Left,@Scooter,@WalkerType,@WheelChairType,@CaneType,@ScooterType,@Pt,@PtFrequency,@PtProvider,@Ot,@OtFrequency,@OtProvider,@SafetyPasds,@Rails,@Other,@LightOnly,@Lunch,@Dinner,@Breakfast,@Behaviour,@RiskOfHarmToSelf,@Smoker,@CognitiveStatus,@RiskOfWandering,@OtherPhysicalInfo,@CognitiveFunction,@Orientation,@NutritionStatus,@NutritionRisk,@AssistiveDevices,@NutritionTexture,@NutritionOther,@Diet,@OtherDiet,@NutritionOtherPhysicalInfo,
@Allergies,@Appetite,@MealsLunch,@MealsDinner,@MealsBreakfast,@Bladder,@Bowel,@ContinenceProductsName,@ContinenceProducts,@ContinenceProductsAssistiveDevices,@ContinenceProductsSupplier,@AssesmentBy,@DateCompleted,@Toileting,@MedicationAssistace,@MedicationAdministration,@MedicationCOmpletedBy,@MedicationAgency,@MedicationPharmacy,@MedicationAllergies,@OtherMedicationAllergies,@MedicationTape,@MedicationHydantoins,@SensoryAbilitiesHearing,@SensoryAbilitiesVision,@Language,@Communication,@CommunicationNotes,@WondCare,@WondCareAssistedBy,@WondCareAgencyName,@SkinCare,@SkinCareTreatments,@SpecialNeedSupplier,@SpecialNeedNotes,@SpecialNeedCpap,@SpecialEquipment,@SpecialEquipmentOtherMentalInfo,@SpecialEquipmentOther,@ResidentFamilyMeeting,@ResidentFamilyInvolvement,@Mantoux,@ChestXray,@Pneumonia,@FluVaccine,@Tetanus,@MantouxDateComplete,@ChestXrayDateComplete,@PneumoniaDateComplete,@FluVaccineDateComplete,@TetanusDateComplete,GETDATE())
END
GO


