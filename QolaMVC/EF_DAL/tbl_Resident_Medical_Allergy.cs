namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Medical_Allergy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_resident_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_allergy_id { get; set; }

        [StringLength(100)]
        public string fd_note { get; set; }

        public DateTime? fd_effective_from { get; set; }

        public DateTime? fd_effective_to { get; set; }

        public virtual tbl_Allergy tbl_Allergy { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }
    }
}
