using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ExcerciseActivitySummaryModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public string BaselineDate { get; set; }
        public string BaselineTug { get; set; }
        public string BaselineVPS { get; set; }

        public string TMonthDate { get; set; }
        public string TMonthTug { get; set; }
        public string TMonthVPS { get; set; }

        public string SMonthDate { get; set; }
        public string SMonthTug { get; set; }
        public string SMonthVPS { get; set; }
        public UserModel EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
    }
}