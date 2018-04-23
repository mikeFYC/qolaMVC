namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_User_Type
    {
        [Key]
        [Column(Order = 0)]
        public byte fd_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string fd_name { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte fd_sort_order { get; set; }
    }
}
