using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    //public class Dining_Attendance
    //{
    //    public List<ResidentModel> LIST_RESIDENT;
        
    //}

    public class Dining_Attendance_simple
    {
        public List<ResidentModelsimple> LIST_RESIDENT;

    }

    public class ResidentModelsimple
    {
        public int ID {get;set;}

        public string FirstName {get;set;}

        public string LastName {get;set;}

        public int No_of_floor { get; set; }

    }
}