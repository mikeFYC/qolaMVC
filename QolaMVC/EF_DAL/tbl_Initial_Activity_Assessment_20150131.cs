namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Initial_Activity_Assessment_20150131
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int? fd_resident_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6000)]
        public string fd_result { get; set; }

        public string fd_comment { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_modified_by { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime fd_modified_on { get; set; }

        [StringLength(300)]
        public string fd_suggest_event { get; set; }
    }
}
