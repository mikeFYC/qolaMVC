namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Dine_Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        public int fd_dine_time_id { get; set; }

        public DateTime fd_date { get; set; }

        [Required]
        public string fd_resident { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Dine_Time tbl_Dine_Time { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
