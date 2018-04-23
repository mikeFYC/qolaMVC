namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Bathing_Attendance
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        [Required]
        public string fd_resident_ids { get; set; }

        public DateTime fd_date { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }
    }
}
