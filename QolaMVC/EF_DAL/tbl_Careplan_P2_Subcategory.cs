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
    
    public partial class tbl_Careplan_P2_Subcategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Careplan_P2_Subcategory()
        {
            this.tbl_Care_Plan_P2_Details = new HashSet<tbl_Care_Plan_P2_Details>();
        }
    
        public int fd_id { get; set; }
        public int fd_category_id { get; set; }
        public string fd_code { get; set; }
        public string fd_name { get; set; }
        public string fd_note { get; set; }
        public byte fd_sort_order { get; set; }
        public string fd_status { get; set; }
        public int fd_created_by { get; set; }
        public System.DateTime fd_created_on { get; set; }
        public int fd_modified_by { get; set; }
        public System.DateTime fd_modified_on { get; set; }
        public string fd_intervention_flag { get; set; }
        public string fd_intervention_note { get; set; }
        public string fd_particular_flag { get; set; }
        public string fd_duration { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Care_Plan_P2_Details> tbl_Care_Plan_P2_Details { get; set; }
        public virtual tbl_Careplan_P2_Category tbl_Careplan_P2_Category { get; set; }
        public virtual tbl_User tbl_User { get; set; }
        public virtual tbl_User tbl_User1 { get; set; }
    }
}
