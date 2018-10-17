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
}