namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Profile_Image
    {
        [Key]
        public int fd_id { get; set; }

        public int? fd_home_id { get; set; }

        public int fd_resident_id { get; set; }

        [StringLength(50)]
        public string fd_image_path { get; set; }

        public byte[] fd_image { get; set; }

        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int? fd_modified_by { get; set; }

        public DateTime? fd_modified_on { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }
    }
}
