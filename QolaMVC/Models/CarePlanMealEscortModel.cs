using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanMealEscortModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string BreakFast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
    }
}