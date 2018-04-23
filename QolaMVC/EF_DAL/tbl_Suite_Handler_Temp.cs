namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Suite_Handler_Temp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int? fd_home_id { get; set; }

        public int fd_resident_id { get; set; }

        public int fd_suite_id { get; set; }

        public int fd_occupancy { get; set; }

        public DateTime fd_move_in_date { get; set; }

        public DateTime? fd_move_out_date { get; set; }

        public int? fd_status { get; set; }

        [StringLength(300)]
        public string fd_notes { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public DateTime? fd_pass_away_date { get; set; }
    }
}
