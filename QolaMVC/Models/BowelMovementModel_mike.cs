using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class BowelMovementModel_mike
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public string dateStr { get; set; }
        public string Type_D { get; set; }
        public string ObservedBy_D { get; set; }
        public string Initials_D { get; set; }
        public string Type_E { get; set; }
        public string ObservedBy_E { get; set; }
        public string Initials_E { get; set; }
        public string Type_N { get; set; }
        public string ObservedBy_N { get; set; }
        public string Initials_N { get; set; }
        public UserModel EnteredBy { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}