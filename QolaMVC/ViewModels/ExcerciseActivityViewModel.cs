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
        public ExcerciseActivitySummaryModel ExcerciseSummary { get; set; }
        public Collection<HSEPDetailModel> HSEPDetail { get; set; }
        public Collection<ExcerciseActivityDetailModel> Detail { get; set; }
    }
}