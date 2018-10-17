using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ActivityAssessmentCollectionViewModel
    {
        public int Id { get; set; }
        public Collection<ActivityCategoryModel> Category { get; set; }
        public Collection<ActivityAssessmentModel> ActivityAssessments { get; set; }
        public DateTime DateEntered { get; set; }
    }
}