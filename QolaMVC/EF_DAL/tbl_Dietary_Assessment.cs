namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Dietary_Assessment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Dietary_Assessment()
        {
            tbl_Resident_Diet_Allergy = new HashSet<tbl_Resident_Diet_Allergy>();
            tbl_Resident_Special_Diet = new HashSet<tbl_Resident_Special_Diet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_resident_id { get; set; }

        public int fd_sl_no { get; set; }

        [StringLength(100)]
        public string fd_special_diat { get; set; }

        [StringLength(100)]
        public string fd_allergy { get; set; }

        [StringLength(500)]
        public string fd_likes { get; set; }

        [StringLength(500)]
        public string fd_dislikes { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [StringLength(100)]
        public string fd_nutritional { get; set; }

        [StringLength(100)]
        public string fd_appetite { get; set; }

        [StringLength(50)]
        public string fd_risk_assistive_device { get; set; }

        [StringLength(100)]
        public string fd_nutrition { get; set; }

        [StringLength(20)]
        public string fd_nutrition_diet_other { get; set; }

        [StringLength(20)]
        public string fd_nutrition_texture { get; set; }

        [StringLength(200)]
        public string fd_nutrition_diet_note { get; set; }

        [StringLength(1)]
        public string fd_view { get; set; }

        public int? fd_viewed_by { get; set; }

        public DateTime? fd_viewed_on { get; set; }

        [StringLength(1)]
        public string fd_known_allergy { get; set; }

        [StringLength(100)]
        public string fd_nutrition_different { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Diet_Allergy> tbl_Resident_Diet_Allergy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Special_Diet> tbl_Resident_Special_Diet { get; set; }
    }
}
