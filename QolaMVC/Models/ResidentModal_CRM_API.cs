﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ResidentModal_CRM_API
    {
        public int FacilityId { get; set; }
        public int Suiteid { get; set; }

        public DateTime ProspectDOB { get; set; }
        public DateTime Contact1DOB { get; set; }
        public DateTime Contact2DOB { get; set; }
        public DateTime Contact3DOB { get; set; }

        public string ProspectFirstName { get; set; }
        public string ProspectLastName { get; set; }
        public string ProspectSuffix { get; set; }
        public string ProspectMaritalStatus { get; set; }
        public string ProspectAddress { get; set; }
        public string ProspectCity { get; set; }
        public string ProspectProvince { get; set; }
        public string ProspectPostalCode { get; set; }
        public string ProspectPhone { get; set; }
        public string ProspectEmail { get; set; }

        public string Contact1FirstName { get; set; }
        public string Contact1LastName { get; set; }
        public string Contact1Suffix { get; set; }
        public string Contact1MaritalStatus { get; set; }
        public string Contact1Address { get; set; }
        public string Contact1City { get; set; }
        public string Contact1Province { get; set; }
        public string Contact1PostalCode { get; set; }
        public string Contact1Phone { get; set; }
        public string Contact1Email { get; set; }

        public string Contact2FirstName { get; set; }
        public string Contact2LastName { get; set; }
        public string Contact2Suffix { get; set; }
        public string Contact2MaritalStatus { get; set; }
        public string Contact2Address { get; set; }
        public string Contact2City { get; set; }
        public string Contact2Province { get; set; }
        public string Contact2PostalCode { get; set; }
        public string Contact2Phone { get; set; }
        public string Contact2Email { get; set; }

        public string Community { get; set; }
        public string CommunityNote { get; set; }

    }
}