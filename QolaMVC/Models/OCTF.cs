using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class OCTF
    {
        public int Id { get; set; }
        public int homeID { get; set; }
        public int ResidentID { get; set; }
        public string Type { get; set; }
        public string AssessmentResult { get; set; }
        public string LevelProtocol { get; set; }
        public string LevelOfAssistance { get; set; }
        public string Initials { get; set; }
        public string Comments { get; set; }
        public int EnteredBy { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}