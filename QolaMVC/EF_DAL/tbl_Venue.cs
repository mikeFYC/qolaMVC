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
    
    public partial class tbl_Venue
    {
        public int fd_id { get; set; }
        public int fd_home_id { get; set; }
        public string fd_code { get; set; }
        public string fd_name { get; set; }
        public string fd_status { get; set; }
        public int fd_modified_by { get; set; }
        public System.DateTime fd_modified_on { get; set; }
    
        public virtual tbl_Home tbl_Home { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}
