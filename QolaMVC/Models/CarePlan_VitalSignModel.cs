using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlan_VitalSignModel
    {
        public int Id { get; set; }
        public string AssessedStatus { get; set; }
        public string LevelOfCare { get; set; }
        public string Bpsystolic { get; set; }

        public string BpDiastolic { get; set; }
        public string BpDateCompleted { get; set; }
        public string Temperature { get; set; }
        public string TemperatureDateCompleted { get; set; }
        public string Weight_Lbs { get; set; }
        public string WeightDateCompleted { get; set; }
        public string HeightCentimeters { get; set; }

        public string HeightInches { get; set; }
        public string HeightDateCompleted { get; set; }
        public string Pulse { get; set; }
        public string PulsedateCompleted { get; set; }

        public string Oxygen_02 { get; set; }
        public string Oxygen_02_rate { get; set; }
        
        public string AmCare { get; set; }
        public string AmCareAssistedBy { get; set; }
        public string AmcareAgencyName { get; set; }
        public string AmCarePreferredTime { get; set; }
        public string AmCarePreferredType { get; set; }

        public string PmCare { get; set; }
        public string PmCareAssistedBy { get; set; }
        public string PmcareAgencyNPme { get; set; }
        public string PmCarePreferredTime { get; set; }
        public string PmCarePreferredType { get; set; }

        public string BathingCare { get; set; }
        public string BathingCareAssistedBy { get; set; }
        public string BathingcareAgencyNBathinge { get; set; }
        public string BathingCarePreferredTime { get; set; }
        public string BathingCarePreferredType { get; set; }

        public string[] Location { get; set; }

        public string Dressing { get; set; }
        public string NailCare { get; set; }
        public string Shaving { get; set; }
        public string FootCare { get; set; }
        public string DressingPreferredTime { get; set; }
        public string NailCarePreferredTime { get; set; }
        public string ShavingPreferredTime { get; set; }
        public string FootCarePreferredTime { get; set; }

        public string OralHygiene { get; set; }
        public string OralHygienePreferredTime { get; set; }

        public string[] Teeth { get; set; }

        public string Mobility { get; set; }
        public string Transfers { get; set; }
        public string MechanicalLeft { get; set; }
        public string Walker { get; set; }
        public string WheelChair { get; set; }
        public string Cane { get; set; }
        public string Left { get; set; }
        public string Scooter { get; set; }

        public string WalkerType { get; set; }
        public string WheelChairType { get; set; }
        public string CaneType { get; set; }
        public string ScooterType { get; set; }

        public string Pt { get; set; }
        public string PtFrequency { get; set; }
        public string PtProvider { get; set; }

        public string Ot { get; set; }
        public string OtFrequency { get; set; }
        public string OtProvider { get; set; }

        public string SafetyPasds { get; set; }
        public string Rails { get; set; }
        public string Other { get; set; }
        public string LightOnly { get; set; }

        public string Lunch { get; set; }
        public string Dinner { get; set; }
        public string Breakfast { get; set; }

        public string[] Behaviour { get; set; }
        public string[] Behaviour2 { get; set; }
        public string[] Behaviour3 { get; set; }

        public string RiskOfHarmToSelf { get; set; }
        public string Smoker { get; set; }
        public string CognitiveStatus { get; set; }
        public string RiskOfWandering { get; set; }
        public string OtherPhysicalInfo { get; set; }

        public string[] CognitiveFunction { get; set; }

        public string[] Orientation { get; set; }
        
        public string NutritionStatus { get; set; }
        public string NutritionRisk { get; set; }
        public string AssistiveDevices { get; set; }
        public string NutritionTexture { get; set; }
        public string NutritionOther { get; set; }
        public string[] Diet { get; set; }
        public string OtherDiet { get; set; }
        public string NutritionOtherPhysicalInfo { get; set; }

        public string[] Allergies { get; set; }

        public string Appetite { get; set; }

        public string MealsLunch { get; set; }
        public string MealsDinner { get; set; }
        public string MealsBreakfast { get; set; }

        public string[] Bladder { get; set; }
        public string[] Bowel { get; set; }

        public string ContinenceProductsName { get; set; }
        public string ContinenceProducts { get; set; }
        public string ContinenceProductsAssistiveDevices { get; set; }
        public string ContinenceProductsSupplier { get; set; }
        public string AssesmentBy { get; set; }
        public string DateCompleted { get; set; }

        public string Toileting { get; set; }
        public string ToiletingStatus { get; set; }
        public string[] ToiletingBathroom { get; set; }
        public string[] ToiletingCommode { get; set; }
        public string[] ToiletingBedpan { get; set; }



        public string MedicationAssistace { get; set; }
        public string MedicationAdministration { get; set; }
        public string MedicationCOmpletedBy { get; set; }
        public string MedicationAgency { get; set; }
        public string MedicationPharmacy { get; set; }

        public string[] MedicationAllergies { get; set; }
        public string[] OtherMedicationAllergies { get; set; }

        public string MedicationTape { get; set; }
        public string MedicationHydantoins { get; set; }

        public string[] SensoryAbilitiesHearing { get; set; }
        public string[] SensoryAbilitiesVision { get; set; }

        public string Language { get; set; }
        public string[] Communication { get; set; }
        public string CommunicationNotes { get; set; }

        public string WondCare { get; set; }
        public string WondCareAssistedBy { get; set; }
        public string WondCareAgencyName { get; set; }

        public string SkinCare { get; set; }
        public string SkinCareTreatments { get; set; }

        public string SpecialNeedSupplier { get; set; }
        public string SpecialNeedNotes { get; set; }
        public string SpecialNeedCpapNotes { get; set; }

        public string SpecialNeedCpap { get; set; }
        //public string SpecialNeedSupplier { get; set; }
        //public string MedicationPharmacy { get; set; }

        public string[] SpecialEquipment { get; set; }
        public string SpecialEquipmentOtherMentalInfo { get; set; }
        public string SpecialEquipmentOther { get; set; }

        public string ResidentFamilyMeeting { get; set; }
        public string ResidentFamilyInvolvement { get; set; }

        public string Mantoux { get; set; }
        public string ChestXray { get; set; }
        public string Pneumonia { get; set; }
        public string FluVaccine { get; set; }
        public string Tetanus { get; set; }
       

        public string MantouxDateComplete { get; set; }
        public string ChestXrayDateComplete { get; set; }
        public string PneumoniaDateComplete { get; set; }
        public string FluVaccineDateComplete { get; set; }
        public string TetanusDateComplete { get; set; }

        public string ResidentId { get; set; }

    }
}