namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Goal_Sheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Goal_Sheet()
        {
            tbl_Goal_Sheet_Details = new HashSet<tbl_Goal_Sheet_Details>();
        }

        [Key]
        public int fd_id { get; set; }

        public int fd_resident_id { get; set; }

        public DateTime fd_date { get; set; }

        [StringLength(500)]
        public string fd_resident_needs { get; set; }

        [StringLength(500)]
        public string fd_intervention { get; set; }

        [StringLength(500)]
        public string fd_effectiveness { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public DateTime? fd_resolved_date { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public byte? fd_services { get; set; }

        public byte? fd_category_type { get; set; }

        [StringLength(100)]
        public string fd_point_of_contact { get; set; }

        [StringLength(50)]
        public string fd_service_other { get; set; }

        [StringLength(1)]
        public string fd_closed_type { get; set; }

        public int? fd_point_of_contact_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Goal_Sheet_Details> tbl_Goal_Sheet_Details { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
