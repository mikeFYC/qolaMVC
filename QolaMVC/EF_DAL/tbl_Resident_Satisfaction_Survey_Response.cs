namespace QolaMVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Resident_Satisfaction_Survey_Response
    {
        [Key]
        public int fd_id { get; set; }

        public int fd_survey_id { get; set; }

        public int fd_question_id { get; set; }

        public byte? fd_answer_id { get; set; }

        [StringLength(150)]
        public string fd_comment { get; set; }

        public int fd_created_by { get; set; }

        public DateTime fd_created_on { get; set; }

        public int fd_modified_by { get; set; }

        public DateTime fd_modified_on { get; set; }

        public virtual tbl_Resident_Satisfaction_Survey tbl_Resident_Satisfaction_Survey { get; set; }

        public virtual tbl_Resident_Satisfaction_Survey_Question tbl_Resident_Satisfaction_Survey_Question { get; set; }

        public virtual tbl_User tbl_User { get; set; }

        public virtual tbl_User tbl_User1 { get; set; }
    }
}
