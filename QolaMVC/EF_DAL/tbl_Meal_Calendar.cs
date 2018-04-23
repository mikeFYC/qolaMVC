namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Meal_Calendar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        public DateTime fd_from_date { get; set; }

        public DateTime fd_to_date { get; set; }

        public int fd_meal_planner_id { get; set; }

        [StringLength(2000)]
        public string fd_name { get; set; }

        [StringLength(360)]
        public string fd_notes { get; set; }

        [StringLength(1)]
        public string fd_previous_meal_planner { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public byte fd_type { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        public virtual tbl_Meal_Planner tbl_Meal_Planner { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
