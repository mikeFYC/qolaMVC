using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanBehaviourModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public Collection<QOLACheckboxModel> BehaviourCollection { get; set; }
        public bool HarmToSelf { get; set; }
        public bool Smoker { get; set; }
        public bool RiskOfWandering { get; set; }
        public string CognitiveStatus { get; set; }
        public string OtherInfo { get; set; }
    }
}