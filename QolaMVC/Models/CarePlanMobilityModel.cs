using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanMobilityModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string Mobility { get; set; }
        public string Transfers { get; set; }
        public string Lift { get; set; }
        public string Walker { get; set; }
        public string WalkerType { get; set; }
        public string WheelChair { get; set; }
        public string WheelChairType { get; set; }
        public string Cane { get; set; }
        public string caneType { get; set; }
        public string Scooter { get; set; }
        public string ScooterType { get; set; }
        public string PT { get; set; }
        public string PTFrequency { get; set; }
        public string PTProvider { get; set; }
        public string OT { get; set; }
        public string OTFrequency { get; set; }
        public string OTProvider { get; set; }
    }
}