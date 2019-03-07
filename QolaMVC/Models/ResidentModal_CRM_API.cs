using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ResidentModal_CRM_API
    {
        public int FacilityId { get; set; }
        public int Suiteid { get; set; }
        public int Occupancy { get; set; }
        public DateTime MoveInDate { get; set; }
        public string ProspectFirstName { get; set; }
        public string ProspectLastName { get; set; }
        public string ProspectGender { get; set; }
        public DateTime ProspectDOB { get; set; }
        public string ProspectMaritalStatus { get; set; }
        public string ProspectPhone { get; set; }
        public string Prospect2FirstName { get; set; }
        public string Prospect2LastName { get; set; }
        public string Prospect2Gender { get; set; }
        public DateTime Prospect2DOB { get; set; }
        public string Prospect2MartialStatus { get; set; }
        public string Prospect2Phone { get; set; }
        public string ProspectContact1FirstName { get; set; }
        public string ProspectContact1LastName { get; set; }
        public string ProspectContact1Relationship { get; set; }
        public string ProspectContact1Address { get; set; }
        public string ProspectContact1Phone { get; set; }
        public string ProspectContact1Email { get; set; }
        public string ProspectContact2FirstName { get; set; }
        public string ProspectContact2LastName { get; set; }
        public string ProspectContact2Relationship { get; set; }
        public string ProspectContact2Address { get; set; }
        public string ProspectContact2Phone { get; set; }
        public string ProspectContact2Email { get; set; }
        public string Prospect2Contact1FirstName { get; set; }
        public string Prospect2Contact1LastName { get; set; }
        public string Prospect2Contact1Relationship { get; set; }
        public string Prospect2Contact1Address { get; set; }
        public string Prospect2Contact1Phone { get; set; }
        public string Prospect2Contact1Email { get; set; }
        public string Prospect2Contact2FirstName { get; set; }
        public string Prospect2Contact2LastName { get; set; }
        public string Prospect2Contact2Relationship { get; set; }
        public string Prospect2Contact2Address { get; set; }
        public string Prospect2Contact2Phone { get; set; }
        public string Prospect2Contact2Email { get; set; }
        public List<ProspectAssessment> ProspectAssessments { get; set; }
        public string MarketSource { get; set; }
        public bool ProspectFinancing { get; set; }
        public bool Prospect2Financing { get; set; }

    }


    public class ProspectAssessment
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}