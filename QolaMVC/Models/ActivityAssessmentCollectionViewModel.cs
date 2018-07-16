using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ActivityAssessmentCollectionViewModel
    {
        public ActivityCategoryModel Category { get; set; }
        public Collection<ActivityAssessmentModel> ActivityAssessments { get; set; }
    }
}