namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Medication_Detail
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_medication_id { get; set; }

        public short fd_sl_no { get; set; }

        public int fd_drug_id { get; set; }

        [Required]
        [StringLength(800)]
        public string fd_dosage { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_purpose { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_timings { get; set; }

        public DateTime? fd_date { get; set; }

        public int? fd_original_RX { get; set; }

        public virtual tbl_Drug tbl_Drug { get; set; }

        public virtual tbl_Medication tbl_Medication { get; set; }
    }
}
