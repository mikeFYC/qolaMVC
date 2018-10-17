﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanFamilySupportModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string FamilyMeeting { get; set; }
        public string FamilyInvolvement { get; set; }
    }
}