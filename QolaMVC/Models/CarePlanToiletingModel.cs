using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanToiletingModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string Bathroom { get; set; }
        public string Commode { get; set; }
        public string Bedpan { get; set; }
        public string Toileting { get; set; }
    }
}