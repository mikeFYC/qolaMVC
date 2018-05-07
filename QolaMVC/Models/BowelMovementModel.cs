using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class BowelMovementModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public string Type { get; set; }
        public string ObservedBy { get; set; }
        public string Initials { get; set; }
        public UserModel EnteredBy { get; set; }
        public string Period { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}