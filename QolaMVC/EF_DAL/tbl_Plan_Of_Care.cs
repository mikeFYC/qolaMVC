namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Plan_Of_Care
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Plan_Of_Care()
        {
            tbl_Immunization = new HashSet<tbl_Immunization>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_resident_id { get; set; }

        [StringLength(100)]
        public string fd_physician { get; set; }

        public DateTime fd_date { get; set; }

        [StringLength(25)]
        public string fd_assessed { get; set; }

        [StringLength(12)]
        public string fd_appetite { get; set; }

        [StringLength(200)]
        public string fd_nutrition { get; set; }

        public string fd_nutrition_SDR { get; set; }

        public string fd_nutrition_FAS { get; set; }

        [StringLength(32)]
        public string fd_elimination_bladder { get; set; }

        [StringLength(50)]
        public string fd_elimination_B_incontinence { get; set; }

        [StringLength(32)]
        public string fd_elimination_bowel { get; set; }

        [StringLength(50)]
        public string fd_elimination_ostomy { get; set; }

        [StringLength(1)]
        public string fd_elimination_managed { get; set; }

        [StringLength(330)]
        public string fd_mobility { get; set; }

        [StringLength(50)]
        public string fd_mobility_prothesis { get; set; }

        [StringLength(1)]
        public string fd_mobility_concerns { get; set; }

        [StringLength(1)]
        public string fd_personal_hygiene_appropriate { get; set; }

        [StringLength(640)]
        public string fd_personal_hygiene { get; set; }

        [StringLength(10)]
        public string fd_PH_bathing_time { get; set; }

        [StringLength(2)]
        public string fd_PH_bathing_frequency { get; set; }

        [StringLength(20)]
        public string fd_PH_service { get; set; }

        [StringLength(100)]
        public string fd_PH_service_agency { get; set; }

        [StringLength(110)]
        public string fd_special_needs { get; set; }

        [StringLength(100)]
        public string fd_SN_O2_supplier { get; set; }

        [StringLength(100)]
        public string fd_SN_O2_rate { get; set; }

        [StringLength(100)]
        public string fd_SN_other { get; set; }

        [StringLength(100)]
        public string fd_SN_comments { get; set; }

        [StringLength(60)]
        public string fd_SA_vision { get; set; }

        [StringLength(100)]
        public string fd_SA_vision_comments { get; set; }

        [StringLength(90)]
        public string fd_SA_hearing { get; set; }

        [StringLength(40)]
        public string fd_cognitive_function { get; set; }

        [StringLength(145)]
        public string fd_behaviour { get; set; }

        [StringLength(100)]
        public string fd_behaviour_comments { get; set; }

        [StringLength(50)]
        public string fd_communication { get; set; }

        [StringLength(100)]
        public string fd_communication_comments { get; set; }

        [StringLength(16)]
        public string fd_smoking { get; set; }

        [StringLength(1)]
        public string fd_alcohol { get; set; }

        [Column(TypeName = "money")]
        public decimal? fd_alcohol_amt { get; set; }

        [StringLength(16)]
        public string fd_AD_abuse { get; set; }

        [StringLength(25)]
        public string fd_activities { get; set; }

        [StringLength(100)]
        public string fd_medication_administration { get; set; }

        [StringLength(100)]
        public string fd_MA_pharmacy { get; set; }

        [StringLength(100)]
        public string fd_MA_injections { get; set; }

        [StringLength(1)]
        public string fd_advanced_directive_completed { get; set; }

        [StringLength(1)]
        public string fd_wound_care { get; set; }

        [StringLength(50)]
        public string fd_WC_agency_servicing { get; set; }

        [StringLength(100)]
        public string fd_WC_comments { get; set; }

        [StringLength(1)]
        public string fd_family_meeting { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [StringLength(50)]
        public string fd_bladder_toileting { get; set; }

        [StringLength(100)]
        public string fd_bladder_comment { get; set; }

        [StringLength(50)]
        public string fd_bowel_toileting { get; set; }

        [StringLength(100)]
        public string fd_bowel_comment { get; set; }

        [StringLength(100)]
        public string fd_mobility_comment { get; set; }

        [StringLength(50)]
        public string fd_walker_type { get; set; }

        [StringLength(50)]
        public string fd_wheelchair_type { get; set; }

        [StringLength(50)]
        public string fd_cane_type { get; set; }

        [StringLength(50)]
        public string fd_scooter_type { get; set; }

        [StringLength(100)]
        public string fd_transfers_comment { get; set; }

        [StringLength(100)]
        public string fd_skin_care_comment { get; set; }

        [StringLength(120)]
        public string fd_toileting { get; set; }

        [StringLength(100)]
        public string fd_continence_comment { get; set; }

        [StringLength(140)]
        public string fd_eating { get; set; }

        [StringLength(50)]
        public string fd_eating_on_unit { get; set; }

        [StringLength(50)]
        public string fd_eating_main_dr { get; set; }

        [StringLength(50)]
        public string fd_eating_breakfast { get; set; }

        [StringLength(50)]
        public string fd_eating_lunch { get; set; }

        [StringLength(50)]
        public string fd_eating_dinner { get; set; }

        [StringLength(80)]
        public string fd_nutritional { get; set; }

        [StringLength(100)]
        public string fd_describe { get; set; }

        [StringLength(50)]
        public string fd_nutritional_reg { get; set; }

        [StringLength(50)]
        public string fd_nutritional_minced { get; set; }

        [StringLength(50)]
        public string fd_nutritional_purced { get; set; }

        [StringLength(100)]
        public string fd_nutritional_comment { get; set; }

        [StringLength(50)]
        public string fd_alert_secured_unit { get; set; }

        [StringLength(50)]
        public string fd_alert_wanderer { get; set; }

        [StringLength(50)]
        public string fd_alert_unsafe_smoker { get; set; }

        [StringLength(50)]
        public string fd_alert_substance_abuse { get; set; }

        [StringLength(50)]
        public string fd_alert_seizures { get; set; }

        [StringLength(50)]
        public string fd_alert_diabetic { get; set; }

        [StringLength(50)]
        public string fd_alert_pacemaker { get; set; }

        [StringLength(50)]
        public string fd_alert_resist_to_care { get; set; }

        [StringLength(50)]
        public string fd_alert_choking_risk { get; set; }

        [StringLength(50)]
        public string fd_alert_comment { get; set; }

        [StringLength(50)]
        public string fd_restraints_physical_type { get; set; }

        [StringLength(50)]
        public string fd_restraints_frequency { get; set; }

        [StringLength(50)]
        public string fd_restraints_environemental { get; set; }

        [StringLength(50)]
        public string fd_restraints_elopement { get; set; }

        [StringLength(50)]
        public string fd_restraints_check_hourly { get; set; }

        [StringLength(50)]
        public string fd_restraints_reposition { get; set; }

        public DateTime? fd_restraints_rxdate { get; set; }

        [StringLength(70)]
        public string fd_memory { get; set; }

        [StringLength(130)]
        public string fd_safety_pasd { get; set; }

        [StringLength(50)]
        public string fd_safety_pasd_other { get; set; }

        [StringLength(50)]
        public string fd_language_spoken { get; set; }

        [StringLength(50)]
        public string fd_SE_pendant { get; set; }

        [StringLength(50)]
        public string fd_SE_ted_stocking { get; set; }

        [StringLength(50)]
        public string fd_SE_support_brace { get; set; }

        [StringLength(50)]
        public string fd_SE_others { get; set; }

        [StringLength(50)]
        public string fd_LOC_total_care { get; set; }

        [StringLength(50)]
        public string fd_LOC_partial_assist { get; set; }

        [StringLength(50)]
        public string fd_LOC_independent { get; set; }

        [StringLength(50)]
        public string fd_news_paper { get; set; }

        [StringLength(50)]
        public string fd_laundry { get; set; }

        [StringLength(50)]
        public string fd_CCAC { get; set; }

        [StringLength(50)]
        public string fd_OT_physio { get; set; }

        [StringLength(50)]
        public string fd_OT_program_started { get; set; }

        [StringLength(50)]
        public string fd_OT_weekly_scheduled { get; set; }

        [StringLength(50)]
        public string fd_PT_program_started { get; set; }

        [StringLength(50)]
        public string fd_PT_weekly_scheduled { get; set; }

        [StringLength(20)]
        public string fd_recreation_programs { get; set; }

        [StringLength(100)]
        public string fd_activity_preferences { get; set; }

        [StringLength(100)]
        public string fd_exercise_group { get; set; }

        [StringLength(10)]
        public string fd_spiritual { get; set; }

        [StringLength(20)]
        public string fd_spiritual_phone { get; set; }

        [StringLength(60)]
        public string fd_family { get; set; }

        [StringLength(100)]
        public string fd_family_involvement_comment { get; set; }

        [StringLength(100)]
        public string fd_family_counselling_comment { get; set; }

        public byte? fd_alcohol_unit { get; set; }

        [StringLength(60)]
        public string fd_special_alerts { get; set; }

        [StringLength(100)]
        public string fd_safty_pasd_comment { get; set; }

        [StringLength(50)]
        public string fd_spceial_equipment { get; set; }

        [StringLength(20)]
        public string fd_level_of_care { get; set; }

        [StringLength(60)]
        public string fd_OT_PT { get; set; }

        [StringLength(100)]
        public string fd_special_alert_comment { get; set; }

        [StringLength(500)]
        public string fd_nutrition_diet { get; set; }

        [StringLength(500)]
        public string fd_nutrition_allergies { get; set; }

        [StringLength(200)]
        public string fd_nutrition_allergy_note { get; set; }

        [StringLength(200)]
        public string fd_nutrition_diet_note { get; set; }

        [StringLength(50)]
        public string fd_AM_agency_name { get; set; }

        [StringLength(50)]
        public string fd_PM_agency_name { get; set; }

        [StringLength(50)]
        public string fd_bathing_agency_name { get; set; }

        [StringLength(50)]
        public string fd_PT_frequency { get; set; }

        [StringLength(50)]
        public string fd_OT_frequency { get; set; }

        [StringLength(50)]
        public string fd_continence_Name { get; set; }

        [StringLength(50)]
        public string fd_continence_assistive_device { get; set; }

        [StringLength(50)]
        public string fd_continence_supplier { get; set; }

        public DateTime? fd_continence_date { get; set; }

        [StringLength(50)]
        public string fd_continence_completed_by { get; set; }

        [StringLength(50)]
        public string fd_medication_Pharmacy { get; set; }

        [StringLength(50)]
        public string fd_wound_agency { get; set; }

        [StringLength(50)]
        public string fd_skincare_treatment { get; set; }

        [StringLength(50)]
        public string fd_WC_assists { get; set; }

        [StringLength(50)]
        public string fd_risk_assistive_device { get; set; }

        public DateTime? fd_AM_preferred_time { get; set; }

        public DateTime? fd_PM_preferred_time { get; set; }

        public DateTime? fd_bathing_preferred_time { get; set; }

        [StringLength(20)]
        public string fd_nutrition_texture { get; set; }

        [StringLength(20)]
        public string fd_nutrition_diet_other { get; set; }

        [StringLength(300)]
        public string fd_nutrition_note { get; set; }

        [StringLength(300)]
        public string fd_SE_details { get; set; }

        [StringLength(50)]
        public string fd_pt_provider { get; set; }

        [StringLength(50)]
        public string fd_ot_provider { get; set; }

        [StringLength(100)]
        public string fd_o2_note { get; set; }

        [StringLength(50)]
        public string fd_stroke { get; set; }

        [StringLength(50)]
        public string fd_stroke_note { get; set; }

        [StringLength(1)]
        public string fd_complete_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Immunization> tbl_Immunization { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
