using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class FallRiskAssessmentModel
    {
        public int Id { get; set; }
        public bool FallHistory_IsTwoOrMore { get; set; }
        public bool FallHistory_IsOneOrTwo { get; set; }
        public bool Neurological_IsCVA { get; set; }
        public bool Neurological_IsParkinsons { get; set; }
        public bool Neurological_IsAlzheimers { get; set; }
        public bool Neurological_IsOther { get; set; }
        public bool Neurological_IsSeizureDisorder { get; set; }
        public bool Other_IsDiabetes { get; set; }
        public bool Other_IsOsteoporosis { get; set; }
        public bool Other_IsPosturalHypotension { get; set; }
        public bool Other_IsSyncope { get; set; }
        public bool Other_IsBowel { get; set; }
        public bool Incontinence_IsBowel { get; set; }
        public bool Incontinence_IsBladder { get; set; }
        public bool Incontinence_IsTransfer { get; set; }
        public bool Incontinence_IsUnsteady { get; set; }
        public bool Medication_IsCardiac { get; set; }
        public bool Medication_IsDiuretics { get; set; }
        public bool Medication_IsNarcotics { get; set; }
        public bool Medication_IsAnalgesics { get; set; }
        public bool Medication_IsSedatives { get; set; }
        public bool Medication_IsAntiAnxiety { get; set; }
        public bool Medication_IsLaxatives { get; set; }
        public bool MentalStatus_IsConfused { get; set; }
        public bool MentalStatus_IsResidentNonCompliance { get; set; }
        public bool Orthopedic_IsRecent { get; set; }
        public bool Orthopedic_IsCast { get; set; }
        public bool Orthopedic_IsAmputation { get; set; }
        public bool Orthopedic_IsSevere { get; set; }
        public bool Sensory_IsDecreasedVision { get; set; }
        public bool Sensory_IsDecreasedHearing { get; set; }
        public bool Sensory_IsAphasia { get; set; }
        public bool Assistive_IsWheelChair { get; set; }
        public bool Assistive_IsCane { get; set; }
        public bool Assistive_IsWalker { get; set; }
        public int TotalScore { get; set; }
        public string RiskLevel { get; set; }
        public int ResidentId { get; set; }
        public DateTime DateEntered { get; set; }
        public int EnteredBy { get; set; }
    }
}