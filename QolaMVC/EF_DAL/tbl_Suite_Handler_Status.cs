namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Suite_Handler_Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Suite_Handler_Status()
        {
            tbl_Suite_Handler = new HashSet<tbl_Suite_Handler>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_category { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_reason { get; set; }

        public byte fd_short_order { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Suite_Handler> tbl_Suite_Handler { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
