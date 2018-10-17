using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.ViewModels
{
   
    public class PostFallClinicalMonitoringViewModel
    {

        public string residentid { get; set; }
        public string vitalsign { get; set; }
        public string date_created { get; set; }
        public string category { get; set; }
        public string firstcheck { get; set; }
        public string onehourfirstcheck { get; set; }
        public string onehoursecondcheck { get; set; }
        public string threehoursfirstcheck { get; set; }
        public string threehourssecondcheck { get; set; }
        public string threehoursthirdcheck { get; set; }
        public string fourtyeighthoursfirstcheck { get; set; }
        public string fourtyeighthourssecondcheck { get; set; }
        public string fourtyeighthoursthirdcheck { get; set; }
        public string fourtyeighthoursfourthcheck { get; set; }
        public string fourtyeighthoursfifthcheck { get; set; }
    }
    class ItemEqualityComparer : IEqualityComparer<PostFallClinicalMonitoringViewModel>
    {
        public bool Equals(PostFallClinicalMonitoringViewModel x, PostFallClinicalMonitoringViewModel y)
        {
            // Two items are equal if their keys are equal.
            return x.date_created == y.date_created;
        }

        public int GetHashCode(PostFallClinicalMonitoringViewModel obj)
        {
            return obj.date_created.GetHashCode();
        }
    }
}