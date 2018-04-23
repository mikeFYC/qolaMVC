namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Activity_Events
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Activity_Events()
        {
            tbl_Activity_Event_Attendance = new HashSet<tbl_Activity_Event_Attendance>();
        }

        [Key]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        public int fd_activity_id { get; set; }

        public DateTime fd_date { get; set; }

        public int fd_venue_id { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_note { get; set; }

        [StringLength(1)]
        public string fd_previous_month_activity { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [StringLength(1)]
        public string fd_sign_up { get; set; }

        public byte fd_type { get; set; }

        public int? fd_activity_display_name_id { get; set; }

        public bool? fd_out_break { get; set; }

        [ForeignKey(nameof(fd_activity_id))]
        public virtual tbl_Activity tbl_Activity { get; set; }

        public virtual tbl_Activity_Display_Name tbl_Activity_Display_Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Activity_Event_Attendance> tbl_Activity_Event_Attendance { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
