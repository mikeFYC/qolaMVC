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
    
    public partial class tbl_Medication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Medication()
        {
            this.tbl_Medication_Detail = new HashSet<tbl_Medication_Detail>();
        }
    
        public int fd_id { get; set; }
        public short fd_sl_no { get; set; }
        public int fd_resident_id { get; set; }
        public string fd_physician { get; set; }
        public System.DateTime fd_date { get; set; }
        public string fd_condition { get; set; }
        public string fd_medication_history { get; set; }
        public string fd_notes { get; set; }
        public string fd_status { get; set; }
        public int fd_modified_by { get; set; }
        public System.DateTime fd_modified_on { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Medication_Detail> tbl_Medication_Detail { get; set; }
        public virtual tbl_Resident tbl_Resident { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}
