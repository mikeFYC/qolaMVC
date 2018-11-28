using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ResidentEmergencyListModel
    {
        public List<ResidentEmergencyListModel_single> EmergencyResidentList;

    }


    public class ResidentEmergencyListModel_single
    {
        public int residentID { get; set; }
        public string suiteNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gendar { get; set; }
        public string phone { get; set; }
        public string contact { get; set; }
        public string contact_phone { get; set; }
        public string RiskLevel { get; set; }
        public string RiskLevel_Full { get; set; }
        public string totalScore { get; set; }

        public Collection<QOLACheckboxModel> Vision { get; set; }
        public string Vision_text { get; set; }
        public Collection<QOLACheckboxModel> Hearing { get; set; }
        public string Hearing_text { get; set; }
        public string Mobility { get; set; }
        public string Walker { get; set; }
        public string WheelChair { get; set; }
        public string Cane { get; set; }
        public Collection<QOLACheckboxModel> CognitiveFunction { get; set; }
        public string CognitiveFunction_text { get; set; }

        public string Comments { get; set; }
    }


}