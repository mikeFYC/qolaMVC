namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Home
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Home()
        {
            tbl_Activity_Events = new HashSet<tbl_Activity_Events>();
            tbl_Bathing_Attendance = new HashSet<tbl_Bathing_Attendance>();
            tbl_Daily_Reports = new HashSet<tbl_Daily_Reports>();
            tbl_Dine_Attendance = new HashSet<tbl_Dine_Attendance>();
            tbl_Meal_Calendar = new HashSet<tbl_Meal_Calendar>();
            tbl_Meal_Planner = new HashSet<tbl_Meal_Planner>();
            tbl_Resident_Away_Schedule = new HashSet<tbl_Resident_Away_Schedule>();
            tbl_Resident_Profile_Image = new HashSet<tbl_Resident_Profile_Image>();
            tbl_Resident = new HashSet<tbl_Resident>();
            tbl_Resident_Track_Record = new HashSet<tbl_Resident_Track_Record>();
            tbl_Suite_Handler = new HashSet<tbl_Suite_Handler>();
            tbl_Suite = new HashSet<tbl_Suite>();
            tbl_Venue = new HashSet<tbl_Venue>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_name { get; set; }

        [Required]
        [StringLength(5)]
        public string fd_code { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_address { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_city { get; set; }

        public int fd_province { get; set; }

        [Required]
        [StringLength(6)]
        public string fd_postal_code { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_country { get; set; }

        public byte fd_no_of_floor { get; set; }

        public short fd_no_of_suites { get; set; }

        [Required]
        [StringLength(200)]
        public string fd_icon_image { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [StringLength(30)]
        public string fd_dine_time_Ids { get; set; }

        [StringLength(15)]
        public string fd_phone { get; set; }

        public Guid fd_GUID { get; set; }

        [StringLength(150)]
        public string fd_pass_time_Ids { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Activity_Events> tbl_Activity_Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Bathing_Attendance> tbl_Bathing_Attendance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Daily_Reports> tbl_Daily_Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dine_Attendance> tbl_Dine_Attendance { get; set; }

        public virtual tbl_Province tbl_Province { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Meal_Calendar> tbl_Meal_Calendar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Meal_Planner> tbl_Meal_Planner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Away_Schedule> tbl_Resident_Away_Schedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Profile_Image> tbl_Resident_Profile_Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident> tbl_Resident { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Track_Record> tbl_Resident_Track_Record { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Suite_Handler> tbl_Suite_Handler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Suite> tbl_Suite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Venue> tbl_Venue { get; set; }
    }
}
