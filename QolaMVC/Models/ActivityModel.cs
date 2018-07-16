using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public ActivityCategoryModel Category { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }
        public string Color { get; set; }
        public string FunPicture { get; set; }
        public string Province { get; set; }
        public bool ShowInAssessment { get; set; }
        public string DisplayTitle { get; set; }
    }
}