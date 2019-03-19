using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class PleuralCatheterDrainageFlowSheetModel
    {
        public int Id { get; set; }
        public string BloodPressure { get; set; }
        public string Pulse { get; set; }
        public string RespiratoryRate { get; set; }
        public string Temperature { get; set; }
        public string OxygenSaturation { get; set; }

        public bool Dyspnea { get; set; }
        public bool Cough { get; set; }
        public string ChestAsessment { get; set; }

        public string DrainageTime { get; set; }
        public string AmountOfDrainage { get; set; }
        public string ColorOfDrainage { get; set; }

       // public string BloodPressure { get; set; }
        public int MyProperty { get; set; }


    }
}