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
    
    public partial class tbl_emar_Additional_Option_Select
    {
        public int Id { get; set; }
        public Nullable<int> HomeId { get; set; }
        public Nullable<byte> AddiOptionId { get; set; }
        public string Value { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    
        public virtual tbl_emar_Additional_Options tbl_emar_Additional_Options { get; set; }
        public virtual tbl_Home tbl_Home { get; set; }
    }
}
