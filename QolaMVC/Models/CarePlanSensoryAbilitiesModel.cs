using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanSensoryAbilitiesModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public Collection<QOLACheckboxModel> Vision { get; set; }
        public Collection<QOLACheckboxModel> Hearing { get; set; }
        public Collection<QOLACheckboxModel> Communication { get; set; }
        public string Language { get; set; }
        public string Notes { get; set; }
    }
}