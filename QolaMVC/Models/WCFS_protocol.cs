using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class WCFS_protocol
    {
        public int Id { get; set; }
        public int currentWound { get; set; }
        public int totalWound { get; set; }
        public DateTime dateEntered { get; set; }
        public int EnteredBy { get; set; }

        public string Wound_Image_Location1 { get; set; }
        public string Wound_Image_Location2 { get; set; }

        public bool Wound_Type_Pressure { get; set; }
        public bool Wound_Type_Arterial { get; set; }
        public bool Wound_Type_Venous { get; set; }
        public bool Wound_Type_SurgicalWound { get; set; }
        public bool Wound_Type_SkinTear { get; set; }
        public bool Wound_Type_Other { get; set; }
        public string Wound_Type_Other_Text { get; set; }

        public DateTime Wound_Protocol_Date { get; set; }
        public string Wound_Protocol_Frequency { get; set; }
        public string Wound_Protocol_Initiated { get; set; }
        public string Wound_Protocol_Protocol_Text { get; set; }

        public bool Wound_HCA_Yes { get; set; }
        public bool Wound_HCA_No { get; set; }
        public DateTime Wound_HCA_initial_Date{ get; set; }
        public string Wound_HCA_Assigned_by { get; set; }

    }
}