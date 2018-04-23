namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Fall_Risk_Assessment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_resident_id { get; set; }

        [StringLength(100)]
        public string fd_physician { get; set; }

        public DateTime fd_assessment_date { get; set; }

        public byte? fd_fall_history_M2 { get; set; }

        public byte? fd_fall_history_LE2 { get; set; }

        public byte? fd_mental_confused_dementia { get; set; }

        public byte? fd_mental_non_compliance { get; set; }

        public byte? fd_NND_CVA { get; set; }

        public byte? fd_NND_parkinsons { get; set; }

        public byte? fd_NND_alzheimers { get; set; }

        public byte? fd_NND_seizure_disorder { get; set; }

        public byte? fd_NND_others { get; set; }

        public byte? fd_ODC_recent_fracture { get; set; }

        public byte? fd_ODC_cast_splint_slings { get; set; }

        public byte? fd_ODC_amputation_prosthesis { get; set; }

        public byte? fd_ODC_severe_arthritis { get; set; }

        public byte? fd_ODP_diabetes { get; set; }

        public byte? fd_ODP_osteoporosis { get; set; }

        public byte? fd_ODP_postural_hypotension { get; set; }

        public byte? fd_ODP_syncope_dizziness { get; set; }

        public byte? fd_SD_decreased_division { get; set; }

        public byte? fd_SD_decreased_hearing { get; set; }

        public byte? fd_SD_aphasia { get; set; }

        public byte? fd_incontinence_bowel { get; set; }

        public byte? fd_incontinence_bladder { get; set; }

        public byte? fd_AD_wheelchair { get; set; }

        public byte? fd_AD_walker { get; set; }

        public byte? fd_AD_cane { get; set; }

        public byte? fd_transfer_difficulties { get; set; }

        public byte? fd_unsteady_gait { get; set; }

        public byte? fd_medication_cardiac { get; set; }

        public byte? fd_medication_diuretics { get; set; }

        public byte? fd_medication_narcotics { get; set; }

        public byte? fd_medication_analgesics { get; set; }

        public byte? fd_medication_anti_psy_sedatives { get; set; }

        public byte? fd_medication_anti_anx_anti_depressants { get; set; }

        public byte? fd_medication_laxatives { get; set; }

        public int fd_total_score { get; set; }

        [Required]
        [StringLength(1)]
        public string fd_risk_level { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public byte? fd_no_risk { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
