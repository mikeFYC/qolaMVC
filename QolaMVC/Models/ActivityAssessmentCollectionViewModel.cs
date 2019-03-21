using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Models
{
    public class ActivityAssessmentCollectionViewModel
    {
        public int Id { get; set; }
        public Collection<ActivityCategoryModel> Category { get; set; }
        public Collection<ActivityAssessmentModel> ActivityAssessments { get; set; }
        public DateTime DateEntered { get; set; }

        public string Comment { get; set; }
        public string SAE { get; set; }

        public string SignificatOther { get; set; }
        public DateTime AnniversaryDate { get; set; }
        public string Number_of_children { get; set; }
        public string Number_of_grandchildren { get; set; }
        public string OtherLanguage { get; set; }
        public string Vetaran { get; set; }
        public string VeteranOther { get; set; }
        public string TshirtSize { get; set; }

        public IEnumerable<SelectListItem> TshirtSizeList { get; set; }


    }
}