namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Diet_Allergy
    {
        [Key]
        [Column(Order = 0)]
        public int fd_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_dietary_assessment_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_allergy_id { get; set; }

        [StringLength(100)]
        public string fd_note { get; set; }

        public bool? fd_allergy_new { get; set; }

        public virtual tbl_Allergy tbl_Allergy { get; set; }

        public virtual tbl_Dietary_Assessment tbl_Dietary_Assessment { get; set; }
    }
}
