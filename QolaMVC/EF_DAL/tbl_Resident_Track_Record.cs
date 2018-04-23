namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Track_Record
    {
        [Key]
        public long fd_id { get; set; }

        public byte fd_category_id { get; set; }

        public int fd_referance_id { get; set; }

        public int fd_home_id { get; set; }

        public int fd_resident_id { get; set; }

        public DateTime fd_date_time { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_title { get; set; }

        [Required]
        [StringLength(500)]
        public string fd_description { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        [StringLength(50)]
        public string fd_attendance { get; set; }

        [StringLength(500)]
        public string fd_progress_note { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
