namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Prescription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Prescription()
        {
            tbl_Administer_Medication = new HashSet<tbl_Administer_Medication>();
            tbl_Prescription_Details = new HashSet<tbl_Prescription_Details>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fd_id { get; set; }

        [Required]
        [StringLength(15)]
        public string fd_tx_number { get; set; }

        public int fd_resident_id { get; set; }

        public int fd_drug_id { get; set; }

        [Required]
        [StringLength(800)]
        public string fd_dosage { get; set; }

        [Required]
        [StringLength(100)]
        public string fd_purpose { get; set; }

        [StringLength(15)]
        public string fd_DIN { get; set; }

        public int? fd_pharmacy_drug_id { get; set; }

        [StringLength(15)]
        public string fd_pharmacy_Rx_number { get; set; }

        public DateTime? fd_pharmacy_Rx_date { get; set; }

        [StringLength(200)]
        public string fd_duration { get; set; }

        [StringLength(200)]
        public string fd_administer_time { get; set; }

        [StringLength(1)]
        public string fd_PRN { get; set; }

        public DateTime fd_start_date { get; set; }

        public DateTime? fd_end_date { get; set; }

        [StringLength(200)]
        public string fd_pharmacy_note { get; set; }

        [StringLength(200)]
        public string fd_physician_note { get; set; }

        [Required]
        [StringLength(3)]
        public string fd_status { get; set; }

        [StringLength(200)]
        public string fd_status_note { get; set; }

        public int fd_created_by { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public DateTime fd_modified_on { get; set; }

        public DateTime? fd_status_date { get; set; }

        [StringLength(800)]
        public string fd_temp_dosage { get; set; }

        [StringLength(3)]
        public string fd_frequency { get; set; }

        [StringLength(3)]
        public string fd_frequency_value { get; set; }

        public int? fd_acknowledged_by { get; set; }

        public DateTime? fd_acknowledged_on { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Administer_Medication> tbl_Administer_Medication { get; set; }

        public virtual tbl_Drug tbl_Drug { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Prescription_Details> tbl_Prescription_Details { get; set; }

        public virtual tbl_Resident tbl_Resident { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }
    }
}
