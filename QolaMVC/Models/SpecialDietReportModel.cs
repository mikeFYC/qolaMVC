using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class SpecialDietReportModel
    {
        public int Id { get; set; }
        public int ResidentID { get; set; }
        public string ResidentName { get; set; }
        public string SuiteNo { get; set; }
        public string FloorNo { get; set; }
        public string Likes { get; set; }
        public string DisLikes { get; set; }
        public DateTime DateEntered { get; set; }
        public string DietType { get; set; }
        public string Texture { get; set; }
        public string Allergies { get; set; }
        public string Notes { get; set; }
        public Collection<QOLACheckboxModel> Diet { get; set; }

    }
}