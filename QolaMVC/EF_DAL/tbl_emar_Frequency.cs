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
    
    public partial class tbl_emar_Frequency
    {
        public int Id { get; set; }
        public Nullable<int> HomeId { get; set; }
        public string Name { get; set; }
        public Nullable<short> Type { get; set; }
        public string Time1 { get; set; }
        public string Time2 { get; set; }
        public string Time3 { get; set; }
        public string Time4 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual tbl_Home tbl_Home { get; set; }
    }
}
