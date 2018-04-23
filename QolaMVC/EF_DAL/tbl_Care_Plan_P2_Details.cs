namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Care_Plan_P2_Details
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_CP2_id { get; set; }

        public int fd_CP2_subcategory_id { get; set; }

        [StringLength(1500)]
        public string fd_intervention { get; set; }

        [StringLength(200)]
        public string fd_note { get; set; }

        public DateTime? fd_activity_from { get; set; }

        public DateTime? fd_activity_to { get; set; }

        [StringLength(300)]
        public string fd_particulars { get; set; }

        [StringLength(1)]
        public string fd_yesNo_option { get; set; }

        public virtual tbl_Care_Plan_P2 tbl_Care_Plan_P2 { get; set; }

        public virtual tbl_Careplan_P2_Subcategory tbl_Careplan_P2_Subcategory { get; set; }
    }
}
