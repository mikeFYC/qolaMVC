namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SpecialDiet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_SpecialDiet()
        {
            tbl_Resident_Special_Diet = new HashSet<tbl_Resident_Special_Diet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        [StringLength(50)]
        public string fd_name { get; set; }

        public int? fd_modified_by { get; set; }

        public DateTime? fd_modified_on { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Special_Diet> tbl_Resident_Special_Diet { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
