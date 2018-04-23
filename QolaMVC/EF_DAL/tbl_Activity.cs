namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Activity()
        {
            tbl_Activity_Events = new HashSet<tbl_Activity_Events>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_activity_category_id { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_name { get; set; }

        [Required]
        [StringLength(10)]
        public string fd_color { get; set; }

        [StringLength(100)]
        public string fd_icon_path { get; set; }

        [StringLength(300)]
        public string fd_description { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_show_in_assessment { get; set; }

        public int? fd_province_id { get; set; }

        [StringLength(200)]
        public string fd_name_fr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Activity_Events> tbl_Activity_Events { get; set; }

        public virtual tbl_Activity_Category tbl_Activity_Category { get; set; }

        public virtual tbl_Province tbl_Province { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
