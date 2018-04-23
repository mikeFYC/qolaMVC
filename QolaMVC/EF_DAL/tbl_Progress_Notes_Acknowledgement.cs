namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Progress_Notes_Acknowledgement
    {
        [Key]
        [Column(Order = 0)]
        public int fd_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime fd_date { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_progress_notes_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_user_id { get; set; }

        public virtual tbl_Progress_Notes tbl_Progress_Notes { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
