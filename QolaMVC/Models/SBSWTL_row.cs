using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class SBSWTL_row
    {
        public int Id { get; set; }
        public int SBSWTL_Table_ID { get; set; }
        public int Residentid { get; set; }
        public int row_index { get; set; }
        public int EnteredBy { get; set; }
        public string userName { get; set; }
        public string userNameType { get; set; }
        public DateTime DateEntered { get; set; }
        public string Bath1 { get; set; }
        public string Bath2 { get; set; }
        public string Bath3 { get; set; }
        public string Shower1 { get; set; }
        public string Shower2 { get; set; }
        public string Shower3 { get; set; }

    }
}