namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Activity_Display_Name
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Activity_Display_Name()
        {
            tbl_Activity_Events = new HashSet<tbl_Activity_Events>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_activity_id { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_name { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int? fd_approved_by { get; set; }

        public DateTime? fd_approved_on { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int? fd_modified_by { get; set; }

        public DateTime? fd_modified_on { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Activity_Events> tbl_Activity_Events { get; set; }
    }
}
