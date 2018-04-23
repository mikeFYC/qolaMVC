namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Careplan_P2_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Careplan_P2_Category()
        {
            tbl_Careplan_P2_Subcategory = new HashSet<tbl_Careplan_P2_Subcategory>();
        }

        [Key]
        public int fd_id { get; set; }

        [Required]
        [StringLength(10)]
        public string fd_code { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_name { get; set; }

        public byte fd_sort_order { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Careplan_P2_Subcategory> tbl_Careplan_P2_Subcategory { get; set; }
    }
}
