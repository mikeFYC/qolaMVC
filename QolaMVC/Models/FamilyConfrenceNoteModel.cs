using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public string onlyCare { get; set; }
        public string Goalsmet { get; set; }
        public string row1 { get; set; }
        public string row2 { get; set; }
        public string row3 { get; set; }
        public string row4 { get; set; }
        public string row5 { get; set; }
        public string row6 { get; set; }
        public string row7 { get; set; }
        public string row8 { get; set; }
        public string row9 { get; set; }
        public string row10 { get; set; }
        public string row11 { get; set; }
        public string droprow1 { get; set; }
        public string droprow2 { get; set; }
        public string droprow3 { get; set; }
        public string droprow4 { get; set; }
        public string droprow5 { get; set; }
        public string droprow6 { get; set; }
        public string droprow7 { get; set; }
        public string droprow8 { get; set; }
        public string droprow9 { get; set; }
        public string droprow10 { get; set; }
        public string droprow11 { get; set; }

        public IEnumerable<SelectListItem> dropYesorNo { get; set; }

        public string Attendees1 { get; set; }
        public string Attendees2 { get; set; }
        public string Attendees3 { get; set; }
        public string Attendees4 { get; set; }
        public string Attendees5 { get; set; }
        public string Attendees6 { get; set; }
        public string Attendees7 { get; set; }
        public string Attendees1rela { get; set; }
        public string Attendees2rela { get; set; }
        public string Attendees3rela { get; set; }
        public string Attendees4rela { get; set; }
        public string Attendees5rela { get; set; }
        public string Attendees6rela { get; set; }
        public string Attendees7rela { get; set; }

        public string completeName { get; set; }
        public string completeSign { get; set; }


    }
}