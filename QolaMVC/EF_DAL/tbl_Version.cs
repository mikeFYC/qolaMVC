namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Version
    {
        [Key]
        public int fd_id { get; set; }

        [Required]
        [StringLength(10)]
        public string fd_version { get; set; }

        public DateTime fd_modified_on { get; set; }
    }
}
