using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanAssistanceWithModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string Dressing { get; set; }
        public string DressingPreferredTime { get; set; }

        public string NailCare { get; set; }
        public string NailCarePreferredTime { get; set; }

        public string Shaving { get; set; }
        public string ShavingPreferredTime { get; set; }

        public string FootCare { get; set; }
        public string FootCarePreferredTime { get; set; }

        public string OralHygiene { get; set; }
        public string OralHygienePreferredTime { get; set; }

        public string Teeth { get; set; }
    }
}