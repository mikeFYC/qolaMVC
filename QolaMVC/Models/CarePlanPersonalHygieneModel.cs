using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanPersonalHygieneModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string AMCare { get; set; }
        public string PMCare { get; set; }
        public string Bathing { get; set; }

        public string AMAssistedBy { get; set; }
        public string PMAssistedBy { get; set; }
        public string BathingAssistedBy { get; set; }

        public string AMPreferredTime { get; set; }
        public string PMPreferredTime { get; set; }
        public string BathingPreferredTime { get; set; }

        public string AMAgencyName { get; set; }
        public string PMAgencyName { get; set; }
        public string BathingAgencyName { get; set; }

        public string AMPreferredType { get; set; }
        public string PMPreferredType { get; set; }
        public string BathingPreferredType { get; set; }

        public string PreferredDays { get; set; }
        public Collection<QOLACheckboxModel> PreferredDaysCollection { get; set; }
    }
}