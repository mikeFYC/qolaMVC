namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Venue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        [Required]
        [StringLength(2)]
        public string fd_code { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_name { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
