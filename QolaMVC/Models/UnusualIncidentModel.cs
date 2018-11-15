using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class UnusualIncidentModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public string Location { get; set; }
        public string Employee { get; set; }
        public string Dept { get; set; }
        public string Visitor { get; set; }
        public string Room { get; set; }
        public string Other { get; set; }
        public string WasWitnessed { get; set; }
        public string WitnessName { get; set; }
        public bool IsFall { get; set; }
        public bool IsElopement { get; set; }
        public string Elopement { get; set; }
        public bool IsUnusualBehaviour { get; set; }
        public string UnusualBehaviour { get; set; }
        public bool IsPhysicalInjury { get; set; }
        public string PhysicalInjury { get; set; }
        public bool IsPropertyLoss { get; set; }
        public string PropertyLoss { get; set; }
        public bool IsSuspicious { get; set; }
        public string Suspicion { get; set; }
        public bool IsTreatment { get; set; }
        public string Treatment { get; set; }
        public bool IsOther { get; set; }
        public string SectionD { get; set; }
        public string SectionE { get; set; }
        public string SectionF { get; set; }
        public Collection<UnusualIncidentSectionGModel> SectionG { get; set; }
        public string SectionH { get; set; }
        public string IncidentDocumented { get; set; }
        public string ChangesMade { get; set; }
        public string ReferralConsult { get; set; }
        public string OHSCommitteeInformed { get; set; }
        public string RecordTrackingForm { get; set; }
        public string IncidentInformation { get; set; }
        public string SectionJ { get; set; }
        public UserModel EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }

        public string happened_Date { get; set; }
        public string happened_Time { get; set; }
        public string Reportby { get; set; }
        public string Location_Other { get; set; }
        public string Resident_involved_Name { get; set; }
        public string Resident_involved_RM { get; set; }
        public bool Resident_Status_1 { get; set; }
        public bool Resident_Status_2 { get; set; }
        public bool Resident_Status_3 { get; set; }
        public bool Resident_Status_4 { get; set; }
        public bool Resident_Status_5 { get; set; }
        public bool Resident_Status_6 { get; set; }
        public string Resident_Status_text { get; set; }
        public string C_Other1 { get; set; }
        public string C_Other2 { get; set; }
        public string C_Other3 { get; set; }
        public string C_Other4 { get; set; }
        public string C_Other5 { get; set; }
        public string D_Date { get; set; }
        public string D_Time { get; set; }
        public string E_Date { get; set; }
        public string E_Time { get; set; }
        public string F_Date { get; set; }
        public string F_Time { get; set; }
        public string H_Date { get; set; }
        public string H_Time { get; set; }
        public string J_Date { get; set; }
        public string J_Time { get; set; }





    }
}