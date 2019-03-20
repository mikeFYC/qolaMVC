using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class DietaryAllergyReportModel
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public int ResidentId { get; set; }
        public string ResidentName { get; set; }
        public string  SuiteNo { get; set; }
        public string FloorNo { get; set; }
        public string Allergy { get; set; }
        public string Note { get; set; }
        public DateTime DateEntered { get; set; }
    }
}