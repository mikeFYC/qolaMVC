using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class SpecialDietReportModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public SuiteModel Suite { get; set; }
        public string Diet { get; set; }
        public string Likes { get; set; }
        public string DisLikes { get; set; }
        public DateTime DateEntered { get; set; }
    }
}