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
    
    public partial class tbl_Emar_MARTAR_Frequency_CustomCycle
    {
        public int ID { get; set; }
        public int OrderMARTARId { get; set; }
        public System.DateTime AdministorTime { get; set; }
        public decimal DoseAmount { get; set; }
        public string DoseUnit { get; set; }
        public string SpecialInstructions { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
