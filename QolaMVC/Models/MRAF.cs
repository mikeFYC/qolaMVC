using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class MRAF
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public int ResidentId { get; set; }
        public int EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
        public string DateEnteredString { get; set; }
        public string BriefHistory { get; set; }
        public string DescriptionBehaviour { get; set; }
        public string DescriptionRisk { get; set; }
        public string RiskAgreement { get; set; }
        public string GoalsRisk  { get; set; }
        public string Termunder { get; set; }
        public string Stafftraining  { get; set; }

        public string ImplementationDate { get; set; }
        public string ScheduledReviewDate { get; set; }
        public string FollowupReviewDate { get; set; }
        public string GuardianName { get; set; }
        public string TrusteeName { get; set; }
        public string GuardianAgentName { get; set; }
        public string ServiceProviderName { get; set; }
        public string ClientCareName { get; set; }
        public string Copy1 { get; set; }
        public string Copy2 { get; set; }

    }
}