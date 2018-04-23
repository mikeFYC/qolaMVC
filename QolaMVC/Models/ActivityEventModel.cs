using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ActivityEventModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Comments { get; set; }
        public DateTime ProgramStartTime { get; set; }
        public DateTime ProgramEndTime { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsRecurrence { get; set; }
        public string RecurrenceRule { get; set; }
    }
}