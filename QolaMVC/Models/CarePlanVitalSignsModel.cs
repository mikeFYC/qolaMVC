using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanVitalSignsModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string BPSystolic { get; set; }
        public string BPDiastolic { get; set; }
        public string BPDateCompleted { get; set; }
        public string Temperature { get; set; }
        public string TempDateCompleted { get; set; }
        public string Weight { get; set; }
        public string WeightDateCompleted { get; set; }
        public string HeightFeet { get; set; }
        public string HeightInches { get; set; }
        public string HeightDateCompleted { get; set; }
        public string Pulse { get; set; }
        public string PulseDateCompleted { get; set; }
        public string PulseRegular { get; set; }
    }
}