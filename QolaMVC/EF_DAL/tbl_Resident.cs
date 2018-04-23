namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Resident()
        {
            tbl_Care_Plan_P2 = new HashSet<tbl_Care_Plan_P2>();
            tbl_Dementia_CheckList = new HashSet<tbl_Dementia_CheckList>();
            tbl_Dietary_Assessment = new HashSet<tbl_Dietary_Assessment>();
            tbl_Fall_Risk_Assessment = new HashSet<tbl_Fall_Risk_Assessment>();
            tbl_Goal_Sheet = new HashSet<tbl_Goal_Sheet>();
            tbl_Initial_Activity_Assessment = new HashSet<tbl_Initial_Activity_Assessment>();
            tbl_Medication = new HashSet<tbl_Medication>();
            tbl_Plan_Of_Care = new HashSet<tbl_Plan_Of_Care>();
            tbl_Prescription = new HashSet<tbl_Prescription>();
            tbl_Progress_Notes = new HashSet<tbl_Progress_Notes>();
            tbl_Resident_Away_Schedule = new HashSet<tbl_Resident_Away_Schedule>();
            tbl_Resident_Medical_Allergy = new HashSet<tbl_Resident_Medical_Allergy>();
            tbl_Resident_Profile_Image = new HashSet<tbl_Resident_Profile_Image>();
            tbl_Resident_Satisfaction_Survey = new HashSet<tbl_Resident_Satisfaction_Survey>();
            tbl_Resident_Track_Record = new HashSet<tbl_Resident_Track_Record>();
            tbl_Suite_Handler = new HashSet<tbl_Suite_Handler>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_home_id { get; set; }

        [Required]
        [StringLength(50)]
        public string fd_suite_id { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_first_name { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_last_name { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_gender { get; set; }

        public DateTime fd_move_in_date { get; set; }

        public DateTime? fd_move_out_date { get; set; }

        public byte? fd_occupancy { get; set; }

        [StringLength(15)]
        public string fd_phone { get; set; }

        public DateTime fd_birth_date { get; set; }

        [Required]
        [StringLength(20)]
        public string fd_MB_health_number { get; set; }

        [StringLength(200)]
        public string fd_insurance_company { get; set; }

        [StringLength(50)]
        public string fd_contract_number { get; set; }

        [StringLength(50)]
        public string fd_group_number { get; set; }

        [StringLength(50)]
        public string fd_contact_1 { get; set; }

        [StringLength(200)]
        public string fd_address_1 { get; set; }

        [StringLength(50)]
        public string fd_relationship_1 { get; set; }

        [StringLength(15)]
        public string fd_home_phone_1 { get; set; }

        [StringLength(15)]
        public string fd_business_phone_1 { get; set; }

        [StringLength(15)]
        public string fd_cell_phone_1 { get; set; }

        [StringLength(200)]
        public string fd_email_1 { get; set; }

        [StringLength(50)]
        public string fd_contact_2 { get; set; }

        [StringLength(200)]
        public string fd_address_2 { get; set; }

        [StringLength(50)]
        public string fd_relationship_2 { get; set; }

        [StringLength(15)]
        public string fd_home_phone_2 { get; set; }

        [StringLength(15)]
        public string fd_business_phone_2 { get; set; }

        [StringLength(15)]
        public string fd_cell_phone_2 { get; set; }

        [StringLength(200)]
        public string fd_email_2 { get; set; }

        [StringLength(50)]
        public string fd_contact_3 { get; set; }

        [StringLength(200)]
        public string fd_address_3 { get; set; }

        [StringLength(50)]
        public string fd_relationship_3 { get; set; }

        [StringLength(15)]
        public string fd_home_phone_3 { get; set; }

        [StringLength(15)]
        public string fd_business_phone_3 { get; set; }

        [StringLength(15)]
        public string fd_cell_phone_3 { get; set; }

        [StringLength(200)]
        public string fd_email_3 { get; set; }

        [StringLength(100)]
        public string fd_POA_care { get; set; }

        [StringLength(15)]
        public string fd_care_home_phone_no { get; set; }

        [StringLength(50)]
        public string fd_physician { get; set; }

        [StringLength(15)]
        public string fd_physician_phone { get; set; }

        [StringLength(200)]
        public string fd_alergies { get; set; }

        public string fd_health_history { get; set; }

        [StringLength(1)]
        public string fd_assess_frequency { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_status { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        [StringLength(50)]
        public string fd_birth_place { get; set; }

        public byte fd_marital_status { get; set; }

        [StringLength(50)]
        public string fd_significat_other { get; set; }

        [StringLength(50)]
        public string fd_relationship_family { get; set; }

        [StringLength(50)]
        public string fd_registered_voter { get; set; }

        [StringLength(50)]
        public string fd_veteran { get; set; }

        [StringLength(50)]
        public string fd_religious_affiliation { get; set; }

        [StringLength(50)]
        public string fd_personal_involvement { get; set; }

        [StringLength(50)]
        public string fd_education_level { get; set; }

        [StringLength(50)]
        public string fd_ability_to_read { get; set; }

        [StringLength(50)]
        public string fd_ability_to_write { get; set; }

        [StringLength(50)]
        public string fd_other_language { get; set; }

        [StringLength(50)]
        public string fd_past_occupations_jobs { get; set; }

        public int? fd_hand_dominance { get; set; }

        [StringLength(50)]
        public string fd_image { get; set; }

        [StringLength(15)]
        public string fd_care_work_phone_no { get; set; }

        [StringLength(50)]
        public string fd_care_email { get; set; }

        [StringLength(100)]
        public string fd_POA_finance { get; set; }

        [StringLength(15)]
        public string fd_finance_home_phone_no { get; set; }

        [StringLength(15)]
        public string fd_finance_work_phone_no { get; set; }

        [StringLength(50)]
        public string fd_finance_email { get; set; }

        public DateTime? fd_admitted_from { get; set; }

        [StringLength(200)]
        public string fd_care_address { get; set; }

        [StringLength(200)]
        public string fd_care_relationship { get; set; }

        public byte? fd_care_type { get; set; }

        [StringLength(200)]
        public string fd_finance_address { get; set; }

        [StringLength(200)]
        public string fd_finance_relationship { get; set; }

        [StringLength(200)]
        public string fd_DNR { get; set; }

        [StringLength(200)]
        public string fd_full_code { get; set; }

        [StringLength(200)]
        public string fd_funeral_argument { get; set; }

        [StringLength(200)]
        public string fd_pharmacy_self { get; set; }

        [StringLength(200)]
        public string fd_pharmacy_nursing { get; set; }

        [StringLength(50)]
        public string fd_pharmacy_fax_no { get; set; }

        [StringLength(50)]
        public string fd_pharmacy_phone_no { get; set; }

        [StringLength(100)]
        public string fd_religion_contact { get; set; }

        [StringLength(50)]
        public string fd_religion_home_phone { get; set; }

        [StringLength(50)]
        public string fd_religon_office { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_qola_resident { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_call_hospital { get; set; }

        [StringLength(20)]
        public string fd_short_name { get; set; }

        [StringLength(100)]
        public string fd_contact_1_password { get; set; }

        [StringLength(100)]
        public string fd_contact_2_password { get; set; }

        [StringLength(100)]
        public string fd_contact_3_password { get; set; }

        [StringLength(15)]
        public string fd_physician_fax_no { get; set; }

        [StringLength(1)]
        public string fd_DNR_status { get; set; }

        [StringLength(1)]
        public string fd_fullcode_status { get; set; }

        public DateTime? fd_aniversary_date { get; set; }

        public byte? fd_religious_affiliation_type { get; set; }

        public byte? fd_read_type { get; set; }

        public byte? fd_write_type { get; set; }

        [StringLength(50)]
        public string fd_voter_type { get; set; }

        [StringLength(50)]
        public string fd_veteran_type { get; set; }

        public byte? fd_education_type { get; set; }

        public byte? fd_profile_type { get; set; }

        public Guid fd_GUID { get; set; }

        public string fd_current_diagnoses { get; set; }

        public byte? fd_home_phone_type_1 { get; set; }

        public byte? fd_business_phone_type_1 { get; set; }

        public byte? fd_cell_phone_type_1 { get; set; }

        public byte? fd_home_phone_type_2 { get; set; }

        public byte? fd_business_phone_type_2 { get; set; }

        public byte? fd_cell_phone_type_2 { get; set; }

        public byte? fd_home_phone_type_3 { get; set; }

        public byte? fd_business_phone_type_3 { get; set; }

        public byte? fd_cell_phone_type_3 { get; set; }

        public byte? fd_POA_care_type { get; set; }

        [StringLength(1)]
        public string fd_POA_care_type_status { get; set; }

        public byte? fd_POA_finance_type { get; set; }

        [StringLength(1)]
        public string fd_POA_finance_type_status { get; set; }

        [StringLength(15)]
        public string fd_POA_care_cell_no { get; set; }

        [StringLength(15)]
        public string fd_POA_finance_cell_no { get; set; }

        public byte? fd_POA_care_home_type { get; set; }

        public byte? fd_POA_care_business_type { get; set; }

        public byte? fd_POA_care_cell_type { get; set; }

        public byte? fd_POA_finance_home_type { get; set; }

        public byte? fd_POA_finance_business_type { get; set; }

        public byte? fd_POA_finance_cell_type { get; set; }

        [StringLength(150)]
        public string fd_cultural_preferences { get; set; }

        [StringLength(1)]
        public string fd_POA_care_type_2_status { get; set; }

        [StringLength(1)]
        public string fd_POA_care_type_3_status { get; set; }

        [StringLength(1)]
        public string fd_POA_finance_type_2_status { get; set; }

        [StringLength(1)]
        public string fd_POA_finance_type_3_status { get; set; }

        public string fd_Favourite_song { get; set; }

        public string fd_Favourite_movie { get; set; }

        public string fd_Number_of_children { get; set; }

        public string fd_Number_of_grandchildren { get; set; }

        public string fd_Favourite_dessert { get; set; }

        public string fd_Favourite_drink { get; set; }

        public string fd_Favourite_flower { get; set; }

        public string fd_Favourite_pets { get; set; }

        public string fd_Wakeup_time { get; set; }

        public string fd_Go_to_bed_at { get; set; }

        public string fd_Favourite_past_time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Care_Plan_P2> tbl_Care_Plan_P2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dementia_CheckList> tbl_Dementia_CheckList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Dietary_Assessment> tbl_Dietary_Assessment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Fall_Risk_Assessment> tbl_Fall_Risk_Assessment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Goal_Sheet> tbl_Goal_Sheet { get; set; }

        public virtual tbl_Home tbl_Home { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Initial_Activity_Assessment> tbl_Initial_Activity_Assessment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Medication> tbl_Medication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Plan_Of_Care> tbl_Plan_Of_Care { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Prescription> tbl_Prescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Progress_Notes> tbl_Progress_Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Away_Schedule> tbl_Resident_Away_Schedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Medical_Allergy> tbl_Resident_Medical_Allergy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Profile_Image> tbl_Resident_Profile_Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Satisfaction_Survey> tbl_Resident_Satisfaction_Survey { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Resident_Track_Record> tbl_Resident_Track_Record { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Suite_Handler> tbl_Suite_Handler { get; set; }
    }
}
