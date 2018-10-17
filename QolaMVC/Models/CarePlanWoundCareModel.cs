using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanWoundCareModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string WoundCare { get; set; }
        public string AssistedBy { get; set; }
        public string Agency { get; set; }
    }
}