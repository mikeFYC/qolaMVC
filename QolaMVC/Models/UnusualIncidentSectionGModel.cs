using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class UnusualIncidentSectionGModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int IncidentId { get; set; }
        public string Notify { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string ByWhom { get; set; }
        public string Via { get; set; }
        public int EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
    }
}