using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class PostFallClinicalMonitoringModel
    {
        public int Id { get; set; }
        public string[] firstcheck { get; set; }
        public string[] onehourfirstcheck { get; set; }
        public string[] onehoursecondcheck { get; set; }
        public string[] threehoursfirstcheck { get; set; }
        public string[] threehourssecondcheck { get; set; }
        public string[] threehoursthirdcheck { get; set; }
        public string[] fourtyeighthoursfirstcheck { get; set; }
        public string[] fourtyeighthourssecondcheck { get; set; }
        public string[] fourtyeighthoursthirdcheck { get; set; }
        public string[] fourtyeighthoursfourthcheck { get; set; }
        public string[] fourtyeighthoursfifthcheck { get; set; }
        public string category { get; set; }
        public int? VitalSignsId { get; set; }
        public VitalSignsModel VitalSigns { get; set; }

    }
}