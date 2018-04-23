namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_User()
        {
            tbl_Activity = new HashSet<tbl_Activity>();
            tbl_Activity_Category = new HashSet<tbl_Activity_Category>();
            tbl_Activity_Events = new HashSet<tbl_Activity_Events>();
            tbl_Administer_Medication = new HashSet<tbl_Administer_Medication>();
            tbl_Allergy = new HashSet<tbl_Allergy>();
            tbl_Bathing_Attendance = new HashSet<tbl_Bathing_Attendance>();
            tbl_Bathing_Attendance1 = new HashSet<tbl_Bathing_Attendance>();
            tbl_Care_Plan_P2 = new HashSet<tbl_Care_Plan_P2>();
            tbl_Careplan_P2_Category = new HashSet<tbl_Careplan_P2_Category>();
            tbl_Careplan_P2_Category1 = new HashSet<tbl_Careplan_P2_Category>();
            tbl_Careplan_P2_Subcategory = new HashSet<tbl_Careplan_P2_Subcategory>();
            tbl_Careplan_P2_Subcategory1 = new HashSet<tbl_Careplan_P2_Subcategory>();
            tbl_Daily_Reports = new HashSet<tbl_Daily_Reports>();
            tbl_Dementia_CheckList = new HashSet<tbl_Dementia_CheckList>();
            tbl_Dementia_Questions = new HashSet<tbl_Dementia_Questions>();
            tbl_Dietary_Assessment = new HashSet<tbl_Dietary_Assessment>();
            tbl_Dine_Attendance = new HashSet<tbl_Dine_Attendance>();
            tbl_Dine_Time = new HashSet<tbl_Dine_Time>();
            tbl_Direction = new HashSet<tbl_Direction>();
            tbl_Drug = new HashSet<tbl_Drug>();
            tbl_Fall_Risk_Assessment = new HashSet<tbl_Fall_Risk_Assessment>();
            tbl_Food = new HashSet<tbl_Food>();
            tbl_Form = new HashSet<tbl_Form>();
            tbl_Goal_Sheet = new HashSet<tbl_Goal_Sheet>();
            tbl_Goal_Sheet_Details = new HashSet<tbl_Goal_Sheet_Details>();
            tbl_Goal_Sheet_Follow_Up = new HashSet<tbl_Goal_Sheet_Follow_Up>();
            tbl_Home = new HashSet<tbl_Home>();
            tbl_Initial_Activity_Assessment = new HashSet<tbl_Initial_Activity_Assessment>();
            tbl_Meal_Calendar = new HashSet<tbl_Meal_Calendar>();
            tbl_Meal_Planner = new HashSet<tbl_Meal_Planner>();
            tbl_Meal_Planner_Menu = new HashSet<tbl_Meal_Planner_Menu>();
            tbl_Medication = new HashSet<tbl_Medication>();
            tbl_Pass_Times = new HashSet<tbl_Pass_Times>();
            tbl_Pass_Times1 = new HashSet<tbl_Pass_Times>();
            tbl_Plan_Of_Care = new HashSet<tbl_Plan_Of_Care>();
            tbl_Prescription = new HashSet<tbl_Prescription>();
            tbl_Prescription1 = new HashSet<tbl_Prescription>();
            tbl_Progress_Notes = new HashSet<tbl_Progress_Notes>();
            tbl_Resident = new HashSet<tbl_Resident>();
            tbl_Resident_Away_Schedule = new HashSet<tbl_Resident_Away_Schedule>();
            tbl_Resident_Away_Schedule1 = new HashSet<tbl_Resident_Away_Schedule>();
            tbl_Resident_Profile_Image = new HashSet<tbl_Resident_Profile_Image>();
            tbl_Resident_Profile_Image1 = new HashSet<tbl_Resident_Profile_Image>();
            tbl_Resident_Satisfaction_Survey = new HashSet<tbl_Resident_Satisfaction_Survey>();
            tbl_Resident_Satisfaction_Survey1 = new HashSet<tbl_Resident_Satisfaction_Survey>();
            tbl_Resident_Satisfaction_Survey_Category = new HashSet<tbl_Resident_Satisfaction_Survey_Category>();
            tbl_Resident_Satisfaction_Survey_Category1 = new HashSet<tbl_Resident_Satisfaction_Survey_Category>();
            tbl_Resident_Satisfaction_Survey_Question = new HashSet<tbl_Resident_Satisfaction_Survey_Question>();
            tbl_Resident_Satisfaction_Survey_Question1 = new HashSet<tbl_Resident_Satisfaction_Survey_Question>();
            tbl_Resident_Satisfaction_Survey_Response = new HashSet<tbl_Resident_Satisfaction_Survey_Response>();
            tbl_Resident_Satisfaction_Survey_Response1 = new HashSet<tbl_Resident_Satisfaction_Survey_Response>();
            tbl_Resident_Track_Record = new HashSet<tbl_Resident_Track_Record>();
            tbl_SpecialDiet = new HashSet<tbl_SpecialDiet>();
            tbl_Suite = new HashSet<tbl_Suite>();
            tbl_Suite_Handler = new HashSet<tbl_Suite_Handler>();
            tbl_Suite_Handler_Status = new HashSet<tbl_Suite_Handler_Status>();
            tbl_Prescription_Details = new HashSet<tbl_Prescription_Details>();
            tbl_Progress_Notes_Acknowledgement = new HashSet<tbl_Progress_Notes_Acknowledgement>();
            tbl_User_IP_MAC = new HashSet<tbl_User_IP_MAC>();
            tbl_User_IP_MAC1 = new HashSet<tbl_User_IP_MAC>();
            tbl_Venue = new HashSet<tbl_Venue>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_home_id { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_first_name { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_last_name { get; set; }

        public int fd_user_type { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_user_name { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_password { get; set; }

        [StringLength(100)]
        public string fd_address { get; set; }

        [StringLength(20)]
        public string fd_city { get; set; }

        [StringLength(20)]
        public string fd_postal_code { get; set; }

        [StringLength(30)]
        public string fd_province { get; set; }

        [StringLength(60)]
        public string fd_email { get; set; }

        [StringLength(50)]
        public string fd_work_phone { get; set; }

        public int? fd_ext { get; set; }

        [StringLength(50)]
        public string fd_home_phone { get; set; }

        [StringLength(15)]
        public string fd_mobile { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public DateTime fd_modified_on { get; set; }

        public int fd_modified_by { get; set; }

        [StringLength(50)]
        public string fd_country { get; set; }

        public Guid fd_GUID { get; set; }

        public DateTime? fd_image_service_last_access { get; set; }

        public int? fd_created_by { get; set; }

        public DateTime? fd_created_on { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Activity> tbl_Activity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Activity_Category> tbl_Activity_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Activity_Events> tbl_Activity_Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Administer_Medication> tbl_Administer_Medication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Allergy> tbl_Allergy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Bathing_Attendance> tbl_Bathing_Attendance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Bathing_Attendance> tbl_Bathing_Attendance1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Care_Plan_P2> tbl_Care_Plan_P2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Careplan_P2_Category> tbl_Careplan_P2_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Careplan_P2_Category> tbl_Careplan_P2_Category1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Careplan_P2_Subcategory> tbl_Careplan_P2_Subcategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Careplan_P2_Subcategory> tbl_Careplan_P2_Subcategory1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Daily_Reports> tbl_Daily_Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dementia_CheckList> tbl_Dementia_CheckList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dementia_Questions> tbl_Dementia_Questions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dietary_Assessment> tbl_Dietary_Assessment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dine_Attendance> tbl_Dine_Attendance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dine_Time> tbl_Dine_Time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Direction> tbl_Direction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Drug> tbl_Drug { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Fall_Risk_Assessment> tbl_Fall_Risk_Assessment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Food> tbl_Food { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Form> tbl_Form { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Goal_Sheet> tbl_Goal_Sheet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Goal_Sheet_Details> tbl_Goal_Sheet_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Goal_Sheet_Follow_Up> tbl_Goal_Sheet_Follow_Up { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Home> tbl_Home { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Initial_Activity_Assessment> tbl_Initial_Activity_Assessment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Meal_Calendar> tbl_Meal_Calendar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Meal_Planner> tbl_Meal_Planner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Meal_Planner_Menu> tbl_Meal_Planner_Menu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Medication> tbl_Medication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Pass_Times> tbl_Pass_Times { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Pass_Times> tbl_Pass_Times1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Plan_Of_Care> tbl_Plan_Of_Care { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Prescription> tbl_Prescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Prescription> tbl_Prescription1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Progress_Notes> tbl_Progress_Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident> tbl_Resident { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Away_Schedule> tbl_Resident_Away_Schedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Away_Schedule> tbl_Resident_Away_Schedule1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Profile_Image> tbl_Resident_Profile_Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Profile_Image> tbl_Resident_Profile_Image1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey> tbl_Resident_Satisfaction_Survey { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey> tbl_Resident_Satisfaction_Survey1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Category> tbl_Resident_Satisfaction_Survey_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Category> tbl_Resident_Satisfaction_Survey_Category1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Question> tbl_Resident_Satisfaction_Survey_Question { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Question> tbl_Resident_Satisfaction_Survey_Question1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Response> tbl_Resident_Satisfaction_Survey_Response { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey_Response> tbl_Resident_Satisfaction_Survey_Response1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Track_Record> tbl_Resident_Track_Record { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SpecialDiet> tbl_SpecialDiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Suite> tbl_Suite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Suite_Handler> tbl_Suite_Handler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Suite_Handler_Status> tbl_Suite_Handler_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Prescription_Details> tbl_Prescription_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Progress_Notes_Acknowledgement> tbl_Progress_Notes_Acknowledgement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_User_IP_MAC> tbl_User_IP_MAC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_User_IP_MAC> tbl_User_IP_MAC1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Venue> tbl_Venue { get; set; }
    }
}
