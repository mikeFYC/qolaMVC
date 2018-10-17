using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanMedication
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string Assistance { get; set; }
        public string Administration { get; set; }
        public string CompletedBy { get; set; }
        public string Agency { get; set; }
        public string Pharmacy { get; set; }
        public string Allergies { get; set; }
    }
}