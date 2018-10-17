using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanImmunizationModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string TB { get; set; }
        public string TBDate { get; set; }
        public string ChestXRay { get; set; }
        public string ChestXRayDate { get; set; }
        public string Pneumonia { get; set; }
        public string PneumoniaDate { get; set; }
        public string FluVaccine { get; set; }
        public string FluVaccineDate { get; set; }
        public string Tetanus { get; set; }
        public string TetanusDate { get; set; }
    }
}