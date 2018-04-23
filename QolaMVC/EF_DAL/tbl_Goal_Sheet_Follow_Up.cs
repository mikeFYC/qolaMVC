namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Goal_Sheet_Follow_Up
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_goal_sheet_detail_id { get; set; }

        [StringLength(500)]
        public string fd_follow_up_note { get; set; }

        [StringLength(1)]
        public string fd_close_type { get; set; }

        public DateTime? fd_close_date { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_reminder { get; set; }

        public DateTime? fd_reminder_date { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public virtual tbl_Goal_Sheet_Details tbl_Goal_Sheet_Details { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
