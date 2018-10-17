using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class HSEPDetailModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public string ActivityName { get; set; }
        public string DateOfTeaching { get; set; }
        public UserModel EnteredBy { get; set; }
    }

    public class HSEPDetailModel_mike
    {
        public int Id { get; set; }
        public int Residentid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EnterDate { get; set; }
        public string DateOfTeaching_1 { get; set; }
        public string SignName_1 { get; set; }
        public string DateOfTeaching_2 { get; set; }
        public string SignName_2 { get; set; }
        public string DateOfTeaching_3 { get; set; }
        public string SignName_3 { get; set; }
        public string DateOfTeaching_4 { get; set; }
        public string SignName_4 { get; set; }
        public string DateOfTeaching_5 { get; set; }
        public string SignName_5 { get; set; }
        public string DateOfTeaching_6 { get; set; }
        public string SignName_6 { get; set; }
        public string DateOfTeaching_7 { get; set; }
        public string SignName_7 { get; set; }
        public string DateOfTeaching_8 { get; set; }
        public string SignName_8 { get; set; }
        public string DateOfTeaching_9 { get; set; }
        public string SignName_9 { get; set; }
        public string DateOfTeaching_10 { get; set; }
        public string SignName_10 { get; set; }

    }
}