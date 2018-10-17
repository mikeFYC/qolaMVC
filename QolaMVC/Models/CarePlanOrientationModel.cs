using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanOrientationModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public bool IsPerson { get; set; }
        public bool IsPlace { get; set; }
        public bool IsTime { get; set; }
        public bool IsDementiaCare { get; set; }
    }
}