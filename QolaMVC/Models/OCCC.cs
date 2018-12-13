using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class OCCC
    {
        public int ID { get; set; }
        public int residentID { get; set; }
        public bool checkYes1 { get; set; }
        public bool checkNo1 { get; set; }
        public bool checkYes2 { get; set; }
        public bool checkNo2 { get; set; }
        public bool checkYes3 { get; set; }
        public bool checkNo3 { get; set; }
        public bool checkYes4 { get; set; }
        public bool checkNo4 { get; set; }
        public bool checkYes5 { get; set; }
        public bool checkNo5 { get; set; }
        public bool checkYes6 { get; set; }
        public bool checkNo6 { get; set; }
        public bool checkYes7 { get; set; }
        public bool checkNo7 { get; set; }
        public bool checkYes8 { get; set; }
        public bool checkNo8 { get; set; }
        public bool checkYes9 { get; set; }
        public bool checkNo9 { get; set; }
        public string comments { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_on { get; set; }
    }
}