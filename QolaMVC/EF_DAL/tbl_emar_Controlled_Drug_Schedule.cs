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
    
    public partial class tbl_emar_Controlled_Drug_Schedule
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public byte DrugScheduleId { get; set; }
        public Nullable<bool> DoubleSignMar { get; set; }
        public Nullable<bool> DoubleSignAdminister { get; set; }
        public Nullable<bool> DisplayWarningAdminiScreen { get; set; }
        public string WarningText { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Schedule { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    
        public virtual tbl_Home tbl_Home { get; set; }
    }
}
