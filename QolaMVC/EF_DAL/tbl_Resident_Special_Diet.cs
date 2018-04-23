namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Special_Diet
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
        public int fd_SpecialDiet_id { get; set; }

        [StringLength(100)]
        public string fd_note { get; set; }

        public virtual tbl_Dietary_Assessment tbl_Dietary_Assessment { get; set; }

        public virtual tbl_SpecialDiet tbl_SpecialDiet { get; set; }
    }
}
