using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class DietaryAllergyReportModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public SuiteModel Suite { get; set; }
        public AllergiesModel Allergy { get; set; }
        public DateTime DateEntered { get; set; }
    }
}