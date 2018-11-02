using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class FamilyConfrenceNoteModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string SuiteNumber { get; set; }
        public ResidentModel Resident { get; set; }
        public string DOB { get; set; }
        public string PHN { get; set; }
        public string CareandGCD { get; set; }
        public string Medication { get; set; }
        public string Recreation { get; set; }
        public string Diet { get; set; }
        public string Comments { get; set; }
        public string Goals { get; set; }
        public string Presents1 { get; set; }
        public string Presents2 { get; set; }
        public string Presents3 { get; set; }
        public UserModel EnteredBy { get; set; }

    }
}