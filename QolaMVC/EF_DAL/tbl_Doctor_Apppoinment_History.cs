//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QolaMVC.EF_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Doctor_Apppoinment_History
    {
        public int fd_id { get; set; }
        public int fd_doctor_appoinment_id { get; set; }
        public int fd_home_id { get; set; }
        public int fd_doctor_id { get; set; }
        public int fd_resident_id { get; set; }
        public System.DateTime fd_date_time { get; set; }
        public byte fd_duration { get; set; }
        public string fd_note { get; set; }
        public string fd_comments { get; set; }
        public int fd_created_by { get; set; }
        public System.DateTime fd_created_on { get; set; }
        public string fd_status { get; set; }
    
        public virtual tbl_Doctor_Appointment tbl_Doctor_Appointment { get; set; }
        public virtual tbl_Home tbl_Home { get; set; }
        public virtual tbl_Resident tbl_Resident { get; set; }
        public virtual tbl_User tbl_User { get; set; }
        public virtual tbl_User tbl_User1 { get; set; }
    }
}
