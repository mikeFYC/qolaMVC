﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanEliminationModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public Collection<QOLACheckboxModel> Bladder { get; set; }
        public Collection<QOLACheckboxModel> Bowel { get; set; }
        public string NameCode { get; set; }
        public string Products { get; set; }
        public string AssistiveDevices { get; set; }
        public string Supplier { get; set; }
        public string CompletedBy { get; set; }
        public string AssessmentDate { get; set; }
    }
}