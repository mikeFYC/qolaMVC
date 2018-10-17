using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanSafetyModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string Safety { get; set; }
        public string Other { get; set; }
        public string Rails { get; set; }
        public bool NightOnly { get; set; }
    }
}