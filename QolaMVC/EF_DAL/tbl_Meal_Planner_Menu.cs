namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Meal_Planner_Menu
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_meal_planner_id { get; set; }

        public int fd_dine_time_id { get; set; }

        public byte fd_day { get; set; }

        [Required]
        [StringLength(300)]
        public string fd_menu_items { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Dine_Time tbl_Dine_Time { get; set; }

        public virtual tbl_Meal_Planner tbl_Meal_Planner { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
