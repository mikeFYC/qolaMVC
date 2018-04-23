namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Activity_Calendar_Names
    {
        [Key]
        public int fd_id { get; set; }

        public int? fd_home_id { get; set; }

        [StringLength(100)]
        public string fd_name { get; set; }

        public byte? fd_calendar_type { get; set; }

        public int? fd_modified_by { get; set; }

        public DateTime? fd_modified_on { get; set; }
    }
}
