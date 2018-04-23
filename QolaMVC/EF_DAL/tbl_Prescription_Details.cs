namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Prescription_Details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_prescription_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string fd_status { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime fd_status_date { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string fd_status_note { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_created_by { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime fd_created_on { get; set; }

        public virtual tbl_Prescription tbl_Prescription { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
