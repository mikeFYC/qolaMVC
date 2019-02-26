using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class PlanOfCareModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public string Assessed { get; set; }
        public string Assessed_Full { get; set; }
        public string LevelOfCare { get; set; }
        public string LevelOfCare_Full { get; set; }

        public string MedicalHistory { get; set; }
        public string CurrentDiagnosis { get; set; }

        public CarePlanVitalSignsModel VitalSigns { get; set; }
        public CarePlanPersonalHygieneModel PersonalHygiene { get; set; }
        public CarePlanAssistanceWithModel AssistanceWith { get; set; }
        public CarePlanMobilityModel Mobility { get; set; }
        public CarePlanSafetyModel Safety { get; set; }
        public CarePlanMealEscortModel MealEscort { get; set; }
        public CarePlanBehaviourModel Behaviour { get; set; }
        public CarePlanCognitiveFunctionModel CognitiveFunction { get; set; }
        public CarePlanOrientationModel Orientation { get; set; }
        public CarePlanNutritionModel Nutrition { get; set; }
        public CarePlanMealsModel Meals { get; set; }
        public CarePlanEliminationModel Elimination { get; set; }
        public CarePlanToiletingModel Toileting { get; set; }
        public CarePlanMedication Medication { get; set; }
        public CarePlanSensoryAbilitiesModel SensoryAbilities { get; set; }
        public CarePlanWoundCareModel WoundCare { get; set; }
        public CarePlanSkinCareModel SkinCare { get; set; }
        public CarePlanSpecialNeedsModel SpecialNeeds { get; set; }
        public CarePlanSpecialEquipmentModel SpecialEquipment { get; set; }
        public CarePlanFamilySupportModel FamilySupportModel { get; set; }
        public CarePlanImmunizationModel Immunization { get; set; }
        public CarePlanInfectiousDiseasesModel InfectiousDiseases { get; set; }
        public string CompleteStatus { get; set; }
        public UserModel EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
    }
}