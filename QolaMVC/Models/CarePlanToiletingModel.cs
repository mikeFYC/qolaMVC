using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanToiletingModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public Collection<QOLACheckboxModel> Bathroom { get; set; }
        public Collection<QOLACheckboxModel> Commode { get; set; }
        public Collection<QOLACheckboxModel> Bedpan { get; set; }
        public string Toileting { get; set; }
    }
}