using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.ViewModels
{
    public class ExcerciseActivityViewModel
    {
        //public Collection<ExcerciseActivityDetailModel> FIRST_WEEK { get; set; }
        //public Collection<ExcerciseActivityDetailModel> SECOND_WEEK { get; set; }
        //public Collection<ExcerciseActivityDetailModel> THIRD_WEEK { get; set; }
        //public Collection<ExcerciseActivityDetailModel> FORTH_WEEK { get; set; }

        public Collection<ExcerciseActivityDetailModel_mike> mike { get; set; }

        public ExcerciseActivityDetailModel_mike mike_single { get; set; }

        public ExcerciseActivitySummaryModel ExcerciseSummary { get; set; }
        public Collection<HSEPDetailModel> HSEPDetail { get; set; }
        public Collection<ExcerciseActivityDetailModel> Detail { get; set; }
    }
}