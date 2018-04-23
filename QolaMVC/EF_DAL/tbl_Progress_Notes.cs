namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Progress_Notes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Progress_Notes()
        {
            tbl_Progress_Notes_Acknowledgement = new HashSet<tbl_Progress_Notes_Acknowledgement>();
        }

        [Key]
        public int fd_id { get; set; }

        public int fd_resident_id { get; set; }

        public DateTime fd_date { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_title { get; set; }

        public string fd_note { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public byte? fd_category { get; set; }

        public byte? fd_remain_in { get; set; }

        public int? fd_acknowledged_by { get; set; }

        public DateTime? fd_acknowledged_on { get; set; }

        [StringLength(300)]
        public string fd_action_note { get; set; }

        [StringLength(1)]
        public string fd_fall_date_type { get; set; }

        public DateTime? fd_fall_date { get; set; }

        [StringLength(100)]
        public string fd_location { get; set; }

        [StringLength(1)]
        public string fd_witness_type { get; set; }

        [StringLength(100)]
        public string fd_witness_fall { get; set; }

        [StringLength(1)]
        public string fd_Un_witnes_type { get; set; }

        [StringLength(1)]
        public string fd_incident_report { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Progress_Notes_Acknowledgement> tbl_Progress_Notes_Acknowledgement { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
