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
    
    public partial class tbl_Care_Plan_P2_Details
    {
        public int fd_id { get; set; }
        public int fd_CP2_id { get; set; }
        public int fd_CP2_subcategory_id { get; set; }
        public string fd_intervention { get; set; }
        public string fd_note { get; set; }
        public Nullable<System.DateTime> fd_activity_from { get; set; }
        public Nullable<System.DateTime> fd_activity_to { get; set; }
        public string fd_particulars { get; set; }
        public string fd_yesNo_option { get; set; }
    
        public virtual tbl_Care_Plan_P2 tbl_Care_Plan_P2 { get; set; }
        public virtual tbl_Careplan_P2_Subcategory tbl_Careplan_P2_Subcategory { get; set; }
    }
}
