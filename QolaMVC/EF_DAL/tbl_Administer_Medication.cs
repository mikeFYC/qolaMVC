namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Administer_Medication
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_medication_detail_id { get; set; }

        [StringLength(8)]
        public string fd_timing { get; set; }

        public byte fd_status { get; set; }

        [StringLength(1)]
        public string fd_site { get; set; }

        public byte? fd_pulse_rate { get; set; }

        public byte? fd_hbp { get; set; }

        public byte? fd_lbp { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [StringLength(200)]
        public string fd_reason { get; set; }

        public bool? fd_effective { get; set; }

        public virtual tbl_Prescription tbl_Prescription { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
