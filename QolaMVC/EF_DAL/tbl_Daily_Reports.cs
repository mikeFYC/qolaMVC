namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Daily_Reports
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        public int? fd_suite_census { get; set; }

        public int? fd_suite_perm { get; set; }

        public int? fd_suite_temp { get; set; }

        [StringLength(4000)]
        public string fd_suite_notice { get; set; }

        public int? fd_suite_capacity { get; set; }

        public DateTime? fd_date { get; set; }

        [StringLength(4000)]
        public string fd_perm_suite_rented { get; set; }

        [StringLength(50)]
        public string fd_on_deposit { get; set; }

        [StringLength(4000)]
        public string fd_suite_left_to_fill { get; set; }

        [StringLength(50)]
        public string fd_outstating_receivable { get; set; }

        [StringLength(100)]
        public string fd_daily_bank_deposit { get; set; }

        [StringLength(4000)]
        public string fd_suite_breakdown_applicable { get; set; }

        [StringLength(4000)]
        public string fd_tours { get; set; }

        [StringLength(4000)]
        public string fd_marketing { get; set; }

        [StringLength(4000)]
        public string fd_notice { get; set; }

        [StringLength(4000)]
        public string fd_hospital_updates { get; set; }

        [StringLength(4000)]
        public string fd_nurse_report { get; set; }

        public int? fd_second_person { get; set; }

        [StringLength(4000)]
        public string fd_advertising { get; set; }

        [StringLength(4000)]
        public string fd_key_operating { get; set; }

        [StringLength(4000)]
        public string fd_neysas_deficiency_list { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
