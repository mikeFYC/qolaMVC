namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Goal_Sheet_Details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Goal_Sheet_Details()
        {
            tbl_Goal_Sheet_Follow_Up = new HashSet<tbl_Goal_Sheet_Follow_Up>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_goal_sheet_id { get; set; }

        public string fd_intervention { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        [StringLength(1)]
        public string fd_close_type { get; set; }

        public virtual tbl_Goal_Sheet tbl_Goal_Sheet { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Goal_Sheet_Follow_Up> tbl_Goal_Sheet_Follow_Up { get; set; }
    }
}
