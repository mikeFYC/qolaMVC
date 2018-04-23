namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Medication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Medication()
        {
            tbl_Medication_Detail = new HashSet<tbl_Medication_Detail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public short fd_sl_no { get; set; }

        public int fd_resident_id { get; set; }

        [StringLength(100)]
        public string fd_physician { get; set; }

        public DateTime fd_date { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_condition { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_medication_history { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_notes { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Medication_Detail> tbl_Medication_Detail { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
