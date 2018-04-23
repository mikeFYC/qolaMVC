namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_User_IP_MAC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_user_id { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_type { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_name { get; set; }

        [Required]
        [StringLength(20)]
        public string fd_value { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_sort_order { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }
    }
}
