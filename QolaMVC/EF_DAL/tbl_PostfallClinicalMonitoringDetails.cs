//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QolaMVC.EF_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_PostfallClinicalMonitoringDetails
    {
        public int Id { get; set; }
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
        public string category { get; set; }
        public int vitalsign { get; set; }
    
        public virtual tbl_PostfallClinicalMonitoringVitalSigns tbl_PostfallClinicalMonitoringVitalSigns { get; set; }
    }
}
