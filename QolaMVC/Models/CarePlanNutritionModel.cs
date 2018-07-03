using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanNutritionModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string NutritionStatus { get; set; }
        public string Risk { get; set; }
        public string AssistiveDevices { get; set; }
        public string Texture { get; set; }
        public string Other { get; set; }
        public Collection<QOLACheckboxModel> Diet { get; set; }
        public string OtherDiet { get; set; }
        public string Notes { get; set; }
        public string Allergies { get; set; }
    }
}