namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Satisfaction_Survey
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Resident_Satisfaction_Survey()
        {
            tbl_Resident_Satisfaction_Survey_Response = new HashSet<tbl_Resident_Satisfaction_Survey_Response>();
        }

        [Key]
        public int fd_id { get; set; }

        public int fd_resident_id { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_completed_by { get; set; }

        [StringLength(100)]
        public string fd_specify { get; set; }

        [StringLength(100)]
        public string fd_relation_ship { get; set; }

        public DateTime fd_date { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_survey_completed_status { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Response> tbl_Resident_Satisfaction_Survey_Response { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }
    }
}
