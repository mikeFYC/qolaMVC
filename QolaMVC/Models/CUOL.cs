using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CUOL
    {
        public int Id { get; set; }
        public int HomeID { get; set; }
        public ResidentModel Resident { get; set; }
        public string Amount { get; set; }
        public string Color { get; set; }
        public string Comments { get; set; }
        public string userName { get; set; }
        public string userNameType { get; set; }
        public UserModel EnteredBy { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}