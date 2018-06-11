using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanSensoryAbilitiesModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string Vision { get; set; }
        public string Hearing { get; set; }
        public string Communication { get; set; }
        public string Notes { get; set; }
    }
}