using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class VitalSignsModel
    {
        public int Id { get; set; }
        public string residentid { get; set; }
        public string[] vitalsign { get; set; }
        public string date_created { get; set; }
        public string category { get; set; }
        public virtual ICollection<PostFallClinicalMonitoringModel> PostfallClinicalMonitoringDetails { get; set; }

    }
}