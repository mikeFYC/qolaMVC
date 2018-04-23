namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Satisfaction_Survey_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Resident_Satisfaction_Survey_Category()
        {
            tbl_Resident_Satisfaction_Survey_Question = new HashSet<tbl_Resident_Satisfaction_Survey_Question>();
        }

        [Key]
        public int fd_id { get; set; }

        [Required]
        [StringLength(200)]
        public string fd_category_name { get; set; }

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
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Question> tbl_Resident_Satisfaction_Survey_Question { get; set; }
    }
}
