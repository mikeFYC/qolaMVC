namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Immunization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        public int fd_planeofcare_id { get; set; }

        public DateTime? fd_pheumovax { get; set; }

        public DateTime? fd_flu_vaccine { get; set; }

        public DateTime? fd_mantoux { get; set; }

        public DateTime? fd_chest_Xray { get; set; }

        public DateTime? fd_tetanus { get; set; }

        [StringLength(7)]
        public string fd_BP_value { get; set; }

        public DateTime? fd_BP_date { get; set; }

        public byte? fd_pulse_value { get; set; }

        public DateTime? fd_pulse_date { get; set; }

        public double? fd_weight_value { get; set; }

        public DateTime? fd_weight_date { get; set; }

        public double? fd_height_value { get; set; }

        public DateTime? fd_height_date { get; set; }

        public DateTime? fd_MRSA_diagnosed { get; set; }

        public DateTime? fd_MRSA_resolved { get; set; }

        public DateTime? fd_VRE_diagnosed { get; set; }

        public DateTime? fd_VRE_resolved { get; set; }

        [StringLength(300)]
        public string fd_others { get; set; }

        [StringLength(300)]
        public string fd_consultants { get; set; }

        public DateTime? fd_pheumovax_due_date { get; set; }

        public DateTime? fd_flu_vaccine_due_date { get; set; }

        public DateTime? fd_mantoux_due_date { get; set; }

        public DateTime? fd_chest_Xray_due_date { get; set; }

        public DateTime? fd_tetanus_due_date { get; set; }

        [StringLength(50)]
        public string fd_consults_specialist1 { get; set; }

        public DateTime? fd_consults_specialist1_date { get; set; }

        [StringLength(50)]
        public string fd_consults_specialist2 { get; set; }

        public DateTime? fd_consults_specialist2_date { get; set; }

        [StringLength(50)]
        public string fd_consults_specialist3 { get; set; }

        public DateTime? fd_consults_specialist3_date { get; set; }

        public byte? fd_weight_measurement { get; set; }

        public byte? fd_height_measurement { get; set; }

        [StringLength(200)]
        public string fd_immunization { get; set; }

        public DateTime? fd_Cdiff_diagnosed { get; set; }

        public DateTime? fd_Cdiff_resolved { get; set; }

        public byte? fd_BP_systolic { get; set; }

        public decimal? fd_temperature { get; set; }

        public DateTime? fd_temperature_date { get; set; }

        public DateTime? fd_other_diagnosed { get; set; }

        public DateTime? fd_other_resolved { get; set; }

        [StringLength(100)]
        public string fd_infection_others { get; set; }

        public virtual tbl_Plan_Of_Care tbl_Plan_Of_Care { get; set; }
    }
}
