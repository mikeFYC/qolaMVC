using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ActivityAssessmentModel
    {
        public int Id { get; set; }
		public ActivityModel Activity { get; set; }
		public bool IsP { get; set; }
        public bool IsC { get; set; }
        public bool IsW { get; set; }
        public string Value { get; set; }
        public int ResidentId { get; set; }
        public DateTime DateEntered { get; set; }


    }
}