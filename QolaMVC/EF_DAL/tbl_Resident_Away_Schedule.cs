namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Away_Schedule
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        public int fd_resident_id { get; set; }

        public DateTime fd_leaving_date { get; set; }

        public DateTime? fd_expect_return_date { get; set; }

        public DateTime? fd_actual_return_date { get; set; }

        [Required]
        [StringLength(600)]
        public string fd_note { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }
    }
}
