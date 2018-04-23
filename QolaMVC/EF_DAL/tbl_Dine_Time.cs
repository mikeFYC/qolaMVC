namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Dine_Time
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Dine_Time()
        {
            tbl_Dine_Attendance = new HashSet<tbl_Dine_Attendance>();
            tbl_Food = new HashSet<tbl_Food>();
            tbl_Meal_Planner_Menu = new HashSet<tbl_Meal_Planner_Menu>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_name { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [StringLength(3)]
        public string fd_short_name { get; set; }

        public byte? fd_sort_order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dine_Attendance> tbl_Dine_Attendance { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Food> tbl_Food { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Meal_Planner_Menu> tbl_Meal_Planner_Menu { get; set; }
    }
}
