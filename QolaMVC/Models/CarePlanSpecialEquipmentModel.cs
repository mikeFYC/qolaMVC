﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanSpecialEquipmentModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string SpecialEquipment { get; set; }
        public string Details { get; set; }
    }
}