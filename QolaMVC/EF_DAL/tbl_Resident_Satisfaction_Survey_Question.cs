namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Satisfaction_Survey_Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Resident_Satisfaction_Survey_Question()
        {
            tbl_Resident_Satisfaction_Survey_Response = new HashSet<tbl_Resident_Satisfaction_Survey_Response>();
        }

        [Key]
        public int fd_id { get; set; }

        public int fd_category_id { get; set; }

        [Required]
        [StringLength(200)]
        public string fd_question_name { get; set; }

        public byte fd_sort_order { get; set; }

        public byte fd_input_type { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_display_flag { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Resident_Satisfaction_Survey_Category tbl_Resident_Satisfaction_Survey_Category { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Response> tbl_Resident_Satisfaction_Survey_Response { get; set; }
    }
}
