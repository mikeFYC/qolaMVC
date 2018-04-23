namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Care_Plan_P2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Care_Plan_P2()
        {
            tbl_Care_Plan_P2_Details = new HashSet<tbl_Care_Plan_P2_Details>();
        }

        [Key]
        public int fd_id { get; set; }

        public int fd_resident_id { get; set; }

        public DateTime fd_date { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        [StringLength(1)]
        public string fd_reanimation { get; set; }

        [StringLength(100)]
        public string fd_communication { get; set; }

        [StringLength(300)]
        public string fd_guide_lines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Care_Plan_P2_Details> tbl_Care_Plan_P2_Details { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
