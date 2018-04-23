namespace QolaMVC.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QolaEFContext : DbContext
    {
        public QolaEFContext()
            : base("name=QolaEFContext")
        {
        }

        public virtual DbSet<tbl_Activity> tbl_Activity { get; set; }
        public virtual DbSet<tbl_Activity_Calendar_Names> tbl_Activity_Calendar_Names { get; set; }
        public virtual DbSet<tbl_Activity_Category> tbl_Activity_Category { get; set; }
        public virtual DbSet<tbl_Activity_Display_Name> tbl_Activity_Display_Name { get; set; }
        public virtual DbSet<tbl_Activity_Event_Attendance> tbl_Activity_Event_Attendance { get; set; }
        public virtual DbSet<tbl_Activity_Events> tbl_Activity_Events { get; set; }
        public virtual DbSet<tbl_Administer_Medication> tbl_Administer_Medication { get; set; }
        public virtual DbSet<tbl_Allergy> tbl_Allergy { get; set; }
        public virtual DbSet<tbl_Bathing_Attendance> tbl_Bathing_Attendance { get; set; }
        public virtual DbSet<tbl_Care_Plan_P2> tbl_Care_Plan_P2 { get; set; }
        public virtual DbSet<tbl_Care_Plan_P2_Details> tbl_Care_Plan_P2_Details { get; set; }
        public virtual DbSet<tbl_Careplan_P2_Category> tbl_Careplan_P2_Category { get; set; }
        public virtual DbSet<tbl_Careplan_P2_Subcategory> tbl_Careplan_P2_Subcategory { get; set; }
        public virtual DbSet<tbl_Daily_Reports> tbl_Daily_Reports { get; set; }
        public virtual DbSet<tbl_Dementia_CheckList> tbl_Dementia_CheckList { get; set; }
        public virtual DbSet<tbl_Dementia_Questions> tbl_Dementia_Questions { get; set; }
        public virtual DbSet<tbl_Dietary_Assessment> tbl_Dietary_Assessment { get; set; }
        public virtual DbSet<tbl_Dine_Attendance> tbl_Dine_Attendance { get; set; }
        public virtual DbSet<tbl_Dine_Time> tbl_Dine_Time { get; set; }
        public virtual DbSet<tbl_Direction> tbl_Direction { get; set; }
        public virtual DbSet<tbl_Drug> tbl_Drug { get; set; }
        public virtual DbSet<tbl_Fall_Risk_Assessment> tbl_Fall_Risk_Assessment { get; set; }
        public virtual DbSet<tbl_Food> tbl_Food { get; set; }
        public virtual DbSet<tbl_Food_Connecting_Word> tbl_Food_Connecting_Word { get; set; }
        public virtual DbSet<tbl_Form> tbl_Form { get; set; }
        public virtual DbSet<tbl_Goal_Sheet> tbl_Goal_Sheet { get; set; }
        public virtual DbSet<tbl_Goal_Sheet_Details> tbl_Goal_Sheet_Details { get; set; }
        public virtual DbSet<tbl_Goal_Sheet_Follow_Up> tbl_Goal_Sheet_Follow_Up { get; set; }
        public virtual DbSet<tbl_Home> tbl_Home { get; set; }
        public virtual DbSet<tbl_Immunization> tbl_Immunization { get; set; }
        public virtual DbSet<tbl_Initial_Activity_Assessment> tbl_Initial_Activity_Assessment { get; set; }
        public virtual DbSet<tbl_Meal_Calendar> tbl_Meal_Calendar { get; set; }
        public virtual DbSet<tbl_Meal_Planner> tbl_Meal_Planner { get; set; }
        public virtual DbSet<tbl_Meal_Planner_Menu> tbl_Meal_Planner_Menu { get; set; }
        public virtual DbSet<tbl_Medication> tbl_Medication { get; set; }
        public virtual DbSet<tbl_Medication_Detail> tbl_Medication_Detail { get; set; }
        public virtual DbSet<tbl_Pass_Times> tbl_Pass_Times { get; set; }
        public virtual DbSet<tbl_Plan_Of_Care> tbl_Plan_Of_Care { get; set; }
        public virtual DbSet<tbl_Prescription> tbl_Prescription { get; set; }
        public virtual DbSet<tbl_Progress_Notes> tbl_Progress_Notes { get; set; }
        public virtual DbSet<tbl_Province> tbl_Province { get; set; }
        public virtual DbSet<tbl_Resident> tbl_Resident { get; set; }
        public virtual DbSet<tbl_Resident_Away_Schedule> tbl_Resident_Away_Schedule { get; set; }
        public virtual DbSet<tbl_Resident_Diet_Allergy> tbl_Resident_Diet_Allergy { get; set; }
        public virtual DbSet<tbl_Resident_Medical_Allergy> tbl_Resident_Medical_Allergy { get; set; }
        public virtual DbSet<tbl_Resident_Profile_Image> tbl_Resident_Profile_Image { get; set; }
        public virtual DbSet<tbl_Resident_Satisfaction_Survey> tbl_Resident_Satisfaction_Survey { get; set; }
        public virtual DbSet<tbl_Resident_Satisfaction_Survey_Category> tbl_Resident_Satisfaction_Survey_Category { get; set; }
        public virtual DbSet<tbl_Resident_Satisfaction_Survey_Question> tbl_Resident_Satisfaction_Survey_Question { get; set; }
        public virtual DbSet<tbl_Resident_Satisfaction_Survey_Response> tbl_Resident_Satisfaction_Survey_Response { get; set; }
        public virtual DbSet<tbl_Resident_Special_Diet> tbl_Resident_Special_Diet { get; set; }
        public virtual DbSet<tbl_Resident_Temp> tbl_Resident_Temp { get; set; }
        public virtual DbSet<tbl_Resident_Track_Record> tbl_Resident_Track_Record { get; set; }
        public virtual DbSet<tbl_SpecialDiet> tbl_SpecialDiet { get; set; }
        public virtual DbSet<tbl_Suite> tbl_Suite { get; set; }
        public virtual DbSet<tbl_Suite_Handler> tbl_Suite_Handler { get; set; }
        public virtual DbSet<tbl_Suite_Handler_Status> tbl_Suite_Handler_Status { get; set; }
        public virtual DbSet<tbl_Suite_Handler_Temp> tbl_Suite_Handler_Temp { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
        public virtual DbSet<tbl_User_IP_MAC> tbl_User_IP_MAC { get; set; }
        public virtual DbSet<tbl_Venue> tbl_Venue { get; set; }
        public virtual DbSet<tbl_Version> tbl_Version { get; set; }
        public virtual DbSet<tbl_Initial_Activity_Assessment_20150131> tbl_Initial_Activity_Assessment_20150131 { get; set; }
        public virtual DbSet<tbl_Prescription_Details> tbl_Prescription_Details { get; set; }
        public virtual DbSet<tbl_Progress_Notes_Acknowledgement> tbl_Progress_Notes_Acknowledgement { get; set; }
        public virtual DbSet<tbl_User_Type> tbl_User_Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Activity>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity>()
                .Property(e => e.fd_color)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity>()
                .Property(e => e.fd_icon_path)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity>()
                .Property(e => e.fd_description)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity>()
                .Property(e => e.fd_show_in_assessment)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity>()
                .HasMany(e => e.tbl_Activity_Events)
                .WithRequired(e => e.tbl_Activity)
                .HasForeignKey(e => e.fd_activity_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Activity_Category>()
                .Property(e => e.fd_category)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Category>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Category>()
                .Property(e => e.fd_color)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Category>()
                .HasMany(e => e.tbl_Activity)
                .WithRequired(e => e.tbl_Activity_Category)
                .HasForeignKey(e => e.fd_activity_category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Activity_Display_Name>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Display_Name>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Display_Name>()
                .HasMany(e => e.tbl_Activity_Events)
                .WithOptional(e => e.tbl_Activity_Display_Name)
                .HasForeignKey(e => e.fd_activity_display_name_id);

            modelBuilder.Entity<tbl_Activity_Events>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Events>()
                .Property(e => e.fd_previous_month_activity)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Events>()
                .Property(e => e.fd_sign_up)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Activity_Events>()
                .HasMany(e => e.tbl_Activity_Event_Attendance)
                .WithRequired(e => e.tbl_Activity_Events)
                .HasForeignKey(e => e.fd_Activity_Event_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Administer_Medication>()
                .Property(e => e.fd_timing)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Administer_Medication>()
                .Property(e => e.fd_site)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Administer_Medication>()
                .Property(e => e.fd_reason)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Allergy>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Allergy>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Allergy>()
                .HasMany(e => e.tbl_Resident_Diet_Allergy)
                .WithRequired(e => e.tbl_Allergy)
                .HasForeignKey(e => e.fd_allergy_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Allergy>()
                .HasMany(e => e.tbl_Resident_Medical_Allergy)
                .WithRequired(e => e.tbl_Allergy)
                .HasForeignKey(e => e.fd_allergy_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Care_Plan_P2>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Care_Plan_P2>()
                .Property(e => e.fd_reanimation)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Care_Plan_P2>()
                .Property(e => e.fd_communication)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Care_Plan_P2>()
                .Property(e => e.fd_guide_lines)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Care_Plan_P2>()
                .HasMany(e => e.tbl_Care_Plan_P2_Details)
                .WithRequired(e => e.tbl_Care_Plan_P2)
                .HasForeignKey(e => e.fd_CP2_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Care_Plan_P2_Details>()
                .Property(e => e.fd_intervention)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Care_Plan_P2_Details>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Care_Plan_P2_Details>()
                .Property(e => e.fd_particulars)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Care_Plan_P2_Details>()
                .Property(e => e.fd_yesNo_option)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Category>()
                .Property(e => e.fd_code)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Category>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Category>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Category>()
                .HasMany(e => e.tbl_Careplan_P2_Subcategory)
                .WithRequired(e => e.tbl_Careplan_P2_Category)
                .HasForeignKey(e => e.fd_category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_code)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_intervention_flag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_intervention_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_particular_flag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .Property(e => e.fd_duration)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Careplan_P2_Subcategory>()
                .HasMany(e => e.tbl_Care_Plan_P2_Details)
                .WithRequired(e => e.tbl_Careplan_P2_Subcategory)
                .HasForeignKey(e => e.fd_CP2_subcategory_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_suite_notice)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_perm_suite_rented)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_on_deposit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_suite_left_to_fill)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_outstating_receivable)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_daily_bank_deposit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_suite_breakdown_applicable)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_tours)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_marketing)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_notice)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_hospital_updates)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_nurse_report)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_advertising)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_key_operating)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Daily_Reports>()
                .Property(e => e.fd_neysas_deficiency_list)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dementia_CheckList>()
                .Property(e => e.fd_result)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dementia_CheckList>()
                .Property(e => e.fd_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dementia_Questions>()
                .Property(e => e.fd_question)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dementia_Questions>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_special_diat)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_allergy)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_likes)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_dislikes)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_nutritional)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_appetite)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_risk_assistive_device)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_nutrition)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_nutrition_diet_other)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_nutrition_texture)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_nutrition_diet_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_view)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_known_allergy)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .Property(e => e.fd_nutrition_different)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .HasMany(e => e.tbl_Resident_Diet_Allergy)
                .WithRequired(e => e.tbl_Dietary_Assessment)
                .HasForeignKey(e => e.fd_dietary_assessment_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Dietary_Assessment>()
                .HasMany(e => e.tbl_Resident_Special_Diet)
                .WithRequired(e => e.tbl_Dietary_Assessment)
                .HasForeignKey(e => e.fd_dietary_assessment_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Dine_Attendance>()
                .Property(e => e.fd_resident)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dine_Time>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dine_Time>()
                .Property(e => e.fd_short_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Dine_Time>()
                .HasMany(e => e.tbl_Dine_Attendance)
                .WithRequired(e => e.tbl_Dine_Time)
                .HasForeignKey(e => e.fd_dine_time_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Dine_Time>()
                .HasMany(e => e.tbl_Food)
                .WithRequired(e => e.tbl_Dine_Time)
                .HasForeignKey(e => e.fd_dinetime_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Dine_Time>()
                .HasMany(e => e.tbl_Meal_Planner_Menu)
                .WithRequired(e => e.tbl_Dine_Time)
                .HasForeignKey(e => e.fd_dine_time_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Direction>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Direction>()
                .Property(e => e.fd_token)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Drug>()
                .Property(e => e.fd_drug_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Drug>()
                .Property(e => e.fd_generic)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Drug>()
                .Property(e => e.fd_strength)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Drug>()
                .HasMany(e => e.tbl_Medication_Detail)
                .WithRequired(e => e.tbl_Drug)
                .HasForeignKey(e => e.fd_drug_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Drug>()
                .HasMany(e => e.tbl_Prescription)
                .WithRequired(e => e.tbl_Drug)
                .HasForeignKey(e => e.fd_drug_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Fall_Risk_Assessment>()
                .Property(e => e.fd_physician)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Fall_Risk_Assessment>()
                .Property(e => e.fd_risk_level)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Food>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Food_Connecting_Word>()
                .Property(e => e.fd_key)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Food_Connecting_Word>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Form>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Form>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Form>()
                .HasMany(e => e.tbl_Drug)
                .WithRequired(e => e.tbl_Form)
                .HasForeignKey(e => e.fd_form)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Goal_Sheet>()
                .Property(e => e.fd_resident_needs)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet>()
                .Property(e => e.fd_intervention)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet>()
                .Property(e => e.fd_effectiveness)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet>()
                .Property(e => e.fd_point_of_contact)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet>()
                .Property(e => e.fd_service_other)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet>()
                .Property(e => e.fd_closed_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet>()
                .HasMany(e => e.tbl_Goal_Sheet_Details)
                .WithRequired(e => e.tbl_Goal_Sheet)
                .HasForeignKey(e => e.fd_goal_sheet_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Goal_Sheet_Details>()
                .Property(e => e.fd_intervention)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet_Details>()
                .Property(e => e.fd_close_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet_Details>()
                .HasMany(e => e.tbl_Goal_Sheet_Follow_Up)
                .WithRequired(e => e.tbl_Goal_Sheet_Details)
                .HasForeignKey(e => e.fd_goal_sheet_detail_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Goal_Sheet_Follow_Up>()
                .Property(e => e.fd_follow_up_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet_Follow_Up>()
                .Property(e => e.fd_close_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Goal_Sheet_Follow_Up>()
                .Property(e => e.fd_reminder)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_code)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_city)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_postal_code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_country)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_icon_image)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_dine_time_Ids)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .Property(e => e.fd_pass_time_Ids)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Activity_Events)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Bathing_Attendance)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Daily_Reports)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Dine_Attendance)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Meal_Calendar)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Meal_Planner)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Resident_Away_Schedule)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Resident_Profile_Image)
                .WithOptional(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Resident)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Resident_Track_Record)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Suite_Handler)
                .WithOptional(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Suite)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Home>()
                .HasMany(e => e.tbl_Venue)
                .WithRequired(e => e.tbl_Home)
                .HasForeignKey(e => e.fd_home_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_BP_value)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_others)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_consultants)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_consults_specialist1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_consults_specialist2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_consults_specialist3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_immunization)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_temperature)
                .HasPrecision(5, 2);

            modelBuilder.Entity<tbl_Immunization>()
                .Property(e => e.fd_infection_others)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Initial_Activity_Assessment>()
                .Property(e => e.fd_result)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Initial_Activity_Assessment>()
                .Property(e => e.fd_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Initial_Activity_Assessment>()
                .Property(e => e.fd_suggest_event)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Meal_Calendar>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Meal_Calendar>()
                .Property(e => e.fd_notes)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Meal_Calendar>()
                .Property(e => e.fd_previous_meal_planner)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Meal_Planner>()
                .HasMany(e => e.tbl_Meal_Calendar)
                .WithRequired(e => e.tbl_Meal_Planner)
                .HasForeignKey(e => e.fd_meal_planner_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Meal_Planner>()
                .HasMany(e => e.tbl_Meal_Planner_Menu)
                .WithRequired(e => e.tbl_Meal_Planner)
                .HasForeignKey(e => e.fd_meal_planner_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Meal_Planner_Menu>()
                .Property(e => e.fd_menu_items)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication>()
                .Property(e => e.fd_physician)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication>()
                .Property(e => e.fd_condition)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication>()
                .Property(e => e.fd_medication_history)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication>()
                .Property(e => e.fd_notes)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication>()
                .HasMany(e => e.tbl_Medication_Detail)
                .WithRequired(e => e.tbl_Medication)
                .HasForeignKey(e => e.fd_medication_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Medication_Detail>()
                .Property(e => e.fd_dosage)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication_Detail>()
                .Property(e => e.fd_purpose)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medication_Detail>()
                .Property(e => e.fd_timings)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Pass_Times>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_physician)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_assessed)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_appetite)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_SDR)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_FAS)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_elimination_bladder)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_elimination_B_incontinence)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_elimination_bowel)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_elimination_ostomy)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_elimination_managed)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_mobility)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_mobility_prothesis)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_mobility_concerns)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_personal_hygiene_appropriate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_personal_hygiene)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PH_bathing_time)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PH_bathing_frequency)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PH_service)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PH_service_agency)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_special_needs)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SN_O2_supplier)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SN_O2_rate)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SN_other)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SN_comments)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SA_vision)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SA_vision_comments)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SA_hearing)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_cognitive_function)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_behaviour)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_behaviour_comments)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_communication)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_communication_comments)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_smoking)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alcohol)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alcohol_amt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_AD_abuse)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_activities)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_medication_administration)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_MA_pharmacy)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_MA_injections)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_advanced_directive_completed)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_wound_care)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_WC_agency_servicing)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_WC_comments)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_family_meeting)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_bladder_toileting)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_bladder_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_bowel_toileting)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_bowel_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_mobility_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_walker_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_wheelchair_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_cane_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_scooter_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_transfers_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_skin_care_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_toileting)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_continence_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_eating)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_eating_on_unit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_eating_main_dr)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_eating_breakfast)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_eating_lunch)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_eating_dinner)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutritional)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_describe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutritional_reg)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutritional_minced)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutritional_purced)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutritional_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_secured_unit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_wanderer)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_unsafe_smoker)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_substance_abuse)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_seizures)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_diabetic)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_pacemaker)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_resist_to_care)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_choking_risk)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_alert_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_restraints_physical_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_restraints_frequency)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_restraints_environemental)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_restraints_elopement)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_restraints_check_hourly)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_restraints_reposition)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_memory)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_safety_pasd)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_safety_pasd_other)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_language_spoken)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SE_pendant)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SE_ted_stocking)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SE_support_brace)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SE_others)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_LOC_total_care)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_LOC_partial_assist)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_LOC_independent)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_news_paper)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_laundry)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_CCAC)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_OT_physio)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_OT_program_started)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_OT_weekly_scheduled)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PT_program_started)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PT_weekly_scheduled)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_recreation_programs)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_activity_preferences)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_exercise_group)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_spiritual)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_spiritual_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_family)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_family_involvement_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_family_counselling_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_special_alerts)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_safty_pasd_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_spceial_equipment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_level_of_care)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_OT_PT)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_special_alert_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_diet)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_allergies)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_allergy_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_diet_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_AM_agency_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PM_agency_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_bathing_agency_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_PT_frequency)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_OT_frequency)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_continence_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_continence_assistive_device)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_continence_supplier)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_continence_completed_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_medication_Pharmacy)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_wound_agency)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_skincare_treatment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_WC_assists)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_risk_assistive_device)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_texture)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_diet_other)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_nutrition_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_SE_details)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_pt_provider)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_ot_provider)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_o2_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_stroke)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_stroke_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .Property(e => e.fd_complete_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Plan_Of_Care>()
                .HasMany(e => e.tbl_Immunization)
                .WithRequired(e => e.tbl_Plan_Of_Care)
                .HasForeignKey(e => e.fd_planeofcare_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_tx_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_dosage)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_purpose)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_DIN)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_pharmacy_Rx_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_duration)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_administer_time)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_PRN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_pharmacy_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_physician_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_status_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_temp_dosage)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_frequency)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .Property(e => e.fd_frequency_value)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription>()
                .HasMany(e => e.tbl_Administer_Medication)
                .WithRequired(e => e.tbl_Prescription)
                .HasForeignKey(e => e.fd_medication_detail_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Prescription>()
                .HasMany(e => e.tbl_Prescription_Details)
                .WithRequired(e => e.tbl_Prescription)
                .HasForeignKey(e => e.fd_prescription_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_title)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_action_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_fall_date_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_location)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_witness_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_witness_fall)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_Un_witnes_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .Property(e => e.fd_incident_report)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Progress_Notes>()
                .HasMany(e => e.tbl_Progress_Notes_Acknowledgement)
                .WithRequired(e => e.tbl_Progress_Notes)
                .HasForeignKey(e => e.fd_progress_notes_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Province>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Province>()
                .HasMany(e => e.tbl_Activity)
                .WithOptional(e => e.tbl_Province)
                .HasForeignKey(e => e.fd_province_id);

            modelBuilder.Entity<tbl_Province>()
                .HasMany(e => e.tbl_Home)
                .WithRequired(e => e.tbl_Province)
                .HasForeignKey(e => e.fd_province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_suite_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_MB_health_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_insurance_company)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_contract_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_group_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_contact_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_address_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_relationship_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_home_phone_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_business_phone_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_cell_phone_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_email_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_contact_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_address_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_relationship_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_home_phone_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_business_phone_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_cell_phone_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_email_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_contact_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_address_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_relationship_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_home_phone_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_business_phone_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_cell_phone_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_email_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_care)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_care_home_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_physician)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_physician_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_alergies)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_health_history)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_assess_frequency)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_birth_place)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_significat_other)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_relationship_family)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_registered_voter)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_veteran)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_religious_affiliation)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_personal_involvement)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_education_level)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_ability_to_read)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_ability_to_write)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_other_language)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_past_occupations_jobs)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_image)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_care_work_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_care_email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_finance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_finance_home_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_finance_work_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_finance_email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_care_address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_care_relationship)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_finance_address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_finance_relationship)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_DNR)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_full_code)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_funeral_argument)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_pharmacy_self)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_pharmacy_nursing)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_pharmacy_fax_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_pharmacy_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_religion_contact)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_religion_home_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_religon_office)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_qola_resident)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_call_hospital)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_short_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_contact_1_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_contact_2_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_contact_3_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_physician_fax_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_DNR_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_fullcode_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_voter_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_veteran_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_current_diagnoses)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_care_type_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_finance_type_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_care_cell_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_finance_cell_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_cultural_preferences)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_care_type_2_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_care_type_3_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_finance_type_2_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .Property(e => e.fd_POA_finance_type_3_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Care_Plan_P2)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Dementia_CheckList)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Dietary_Assessment)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Fall_Risk_Assessment)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Goal_Sheet)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Initial_Activity_Assessment)
                .WithOptional(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Medication)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Plan_Of_Care)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Prescription)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Progress_Notes)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Resident_Away_Schedule)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Resident_Medical_Allergy)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Resident_Profile_Image)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Resident_Track_Record)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident>()
                .HasMany(e => e.tbl_Suite_Handler)
                .WithRequired(e => e.tbl_Resident)
                .HasForeignKey(e => e.fd_resident_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident_Away_Schedule>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Diet_Allergy>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Medical_Allergy>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Profile_Image>()
                .Property(e => e.fd_image_path)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Profile_Image>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey>()
                .Property(e => e.fd_completed_by)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey>()
                .Property(e => e.fd_specify)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey>()
                .Property(e => e.fd_relation_ship)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey>()
                .Property(e => e.fd_survey_completed_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Response)
                .WithRequired(e => e.tbl_Resident_Satisfaction_Survey)
                .HasForeignKey(e => e.fd_survey_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey_Category>()
                .Property(e => e.fd_category_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey_Category>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey_Category>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Question)
                .WithRequired(e => e.tbl_Resident_Satisfaction_Survey_Category)
                .HasForeignKey(e => e.fd_category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey_Question>()
                .Property(e => e.fd_question_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey_Question>()
                .Property(e => e.fd_display_flag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey_Question>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Response)
                .WithRequired(e => e.tbl_Resident_Satisfaction_Survey_Question)
                .HasForeignKey(e => e.fd_question_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Resident_Satisfaction_Survey_Response>()
                .Property(e => e.fd_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Special_Diet>()
                .Property(e => e.fd_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_suite_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_MB_health_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_insurance_company)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_contract_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_group_number)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_contact_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_address_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_relationship_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_home_phone_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_business_phone_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_cell_phone_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_email_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_contact_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_address_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_relationship_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_home_phone_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_business_phone_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_cell_phone_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_email_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_contact_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_address_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_relationship_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_home_phone_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_business_phone_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_cell_phone_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_email_3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_care)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_care_home_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_physician)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_physician_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_alergies)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_health_history)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_assess_frequency)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_birth_place)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_significat_other)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_relationship_family)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_registered_voter)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_veteran)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_religious_affiliation)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_personal_involvement)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_education_level)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_ability_to_read)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_ability_to_write)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_other_language)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_past_occupations_jobs)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_image)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_care_work_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_care_email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_finance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_finance_home_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_finance_work_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_finance_email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_care_address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_care_relationship)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_finance_address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_finance_relationship)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_DNR)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_full_code)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_funeral_argument)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_pharmacy_self)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_pharmacy_nursing)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_pharmacy_fax_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_pharmacy_phone_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_religion_contact)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_religion_home_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_religon_office)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_qola_resident)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_call_hospital)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_short_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_contact_1_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_contact_2_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_contact_3_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_physician_fax_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_DNR_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_fullcode_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_voter_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_veteran_type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_current_diagnoses)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_care_type_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_finance_type_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_care_cell_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_finance_cell_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_cultural_preferences)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_care_type_2_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_care_type_3_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_finance_type_2_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Temp>()
                .Property(e => e.fd_POA_finance_type_3_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Track_Record>()
                .Property(e => e.fd_title)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Track_Record>()
                .Property(e => e.fd_description)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Track_Record>()
                .Property(e => e.fd_attendance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Resident_Track_Record>()
                .Property(e => e.fd_progress_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SpecialDiet>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SpecialDiet>()
                .HasMany(e => e.tbl_Resident_Special_Diet)
                .WithRequired(e => e.tbl_SpecialDiet)
                .HasForeignKey(e => e.fd_SpecialDiet_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Suite>()
                .Property(e => e.fd_suite_no)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Suite>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Suite_Handler_Status>()
                .Property(e => e.fd_category)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Suite_Handler_Status>()
                .Property(e => e.fd_reason)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Suite_Handler_Status>()
                .HasMany(e => e.tbl_Suite_Handler)
                .WithOptional(e => e.tbl_Suite_Handler_Status)
                .HasForeignKey(e => e.fd_status);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_home_id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_first_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_last_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_user_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_city)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_province)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_mobile)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.fd_country)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Activity)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Activity_Category)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Activity_Events)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Administer_Medication)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Allergy)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Bathing_Attendance)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Bathing_Attendance1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Care_Plan_P2)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Careplan_P2_Category)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Careplan_P2_Category1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Careplan_P2_Subcategory)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Careplan_P2_Subcategory1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Daily_Reports)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Dementia_CheckList)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Dementia_Questions)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Dietary_Assessment)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Dine_Attendance)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Dine_Time)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Direction)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Drug)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Fall_Risk_Assessment)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Food)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Form)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Goal_Sheet)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Goal_Sheet_Details)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Goal_Sheet_Follow_Up)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Home)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Initial_Activity_Assessment)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Meal_Calendar)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Meal_Planner)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Meal_Planner_Menu)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Medication)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Pass_Times)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Pass_Times1)
                .WithOptional(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Plan_Of_Care)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Prescription)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Prescription1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Progress_Notes)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Away_Schedule)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Away_Schedule1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Profile_Image)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Profile_Image1)
                .WithOptional(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Category)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Category1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Question)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Question1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Response)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Satisfaction_Survey_Response1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Resident_Track_Record)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_SpecialDiet)
                .WithOptional(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Suite)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Suite_Handler)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Suite_Handler_Status)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Prescription_Details)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_created_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Progress_Notes_Acknowledgement)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_User_IP_MAC)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_User_IP_MAC1)
                .WithRequired(e => e.tbl_User1)
                .HasForeignKey(e => e.fd_user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_Venue)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.fd_modified_by)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_User_IP_MAC>()
                .Property(e => e.fd_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User_IP_MAC>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User_IP_MAC>()
                .Property(e => e.fd_value)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User_IP_MAC>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Venue>()
                .Property(e => e.fd_code)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Venue>()
                .Property(e => e.fd_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Venue>()
                .Property(e => e.fd_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Version>()
                .Property(e => e.fd_version)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Initial_Activity_Assessment_20150131>()
                .Property(e => e.fd_result)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Initial_Activity_Assessment_20150131>()
                .Property(e => e.fd_comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Initial_Activity_Assessment_20150131>()
                .Property(e => e.fd_suggest_event)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription_Details>()
                .Property(e => e.fd_status)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Prescription_Details>()
                .Property(e => e.fd_status_note)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User_Type>()
                .Property(e => e.fd_name)
                .IsUnicode(false);
        }
    }
}
