using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ActivityEventModel_Calendar2
    {
        public int ProgramId { get; set; }
        public int ActivityId { get; set; }
        public string ProgramName { get; set; }
        public string Comments { get; set; }
        public DateTime ProgramStartDate { get; set; }
        public DateTime ProgramEndDate { get; set; }
        public string ProgramStartTime { get; set; }
        public string ProgramEndTime { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsRecurrence { get; set; }
        public string RecurrenceRule { get; set; }
        public string Venue { get; set; }
        public int Active { get; set; }
        public int Declined { get; set; }
        public string ActivityNameEnglish { get; set; }
        public string CategoryId { get; set; }

        public string Color { get; set; }
    }
}