namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Food_Connecting_Word
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        [Required]
        [StringLength(3)]
        public string fd_key { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_name { get; set; }

        public byte fd_short_order { get; set; }

        [StringLength(100)]
        public string fd_name_fr { get; set; }
    }
}
