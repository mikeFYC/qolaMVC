using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class nDietaryAssessmentModel
    {
        public int Id { get; set; }
        public string Apetite { get; set; }
        public string NutritionalStatus { get; set; }
        public string Risk { get; set; }
        public string AssistiveDevices { get; set; }
        public Collection<string> Diet { get; set; }
        public string Texture { get; set; }
        public string Other { get; set; }
        public string Likes { get; set; }
        public string DisLikes { get; set; }
        public string Notes { get; set; }
        public Collection<AllergiesModel> Allergies { get; set; }
        public ResidentModel Resident { get; set; }
        public UserModel EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
    }
}