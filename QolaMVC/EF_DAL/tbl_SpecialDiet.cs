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
    
    public partial class tbl_SpecialDiet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SpecialDiet()
        {
            this.tbl_Resident_Special_Diet = new HashSet<tbl_Resident_Special_Diet>();
        }
    
        public int fd_id { get; set; }
        public string fd_name { get; set; }
        public Nullable<int> fd_modified_by { get; set; }
        public Nullable<System.DateTime> fd_modified_on { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Special_Diet> tbl_Resident_Special_Diet { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}
