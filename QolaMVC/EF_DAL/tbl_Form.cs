namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Form()
        {
            tbl_Drug = new HashSet<tbl_Drug>();
        }

        [Key]
        public byte fd_id { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_name { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public int fd_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Drug> tbl_Drug { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
