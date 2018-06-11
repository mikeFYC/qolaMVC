using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanSpecialNeedsModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string Oxygen { get; set; }
        public string OxygenSupplier { get; set; }
        public string OxygenRate { get; set; }
        public string OxygenNotes { get; set; }
        public string CPAP { get; set; }
        public string CPAPSupplier { get; set; }
        public string CPAPNotes { get; set; }
    }
}