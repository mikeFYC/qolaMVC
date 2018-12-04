using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ActivityEventModel_PC
    {
        public int ProgramId { get; set; }
        public int HomeId { get; set; }
        public int ResidentId { get; set; }
        public string ProgramName { get; set; }
        public DateTime ProgramStartDate { get; set; }
        public DateTime ProgramEndDate { get; set; }
        public string ProgramStartTime { get; set; }
        public string ProgramEndTime { get; set; }
        public string note { get; set; }

    }
}