﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class AdmissionHeadToToeModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public DateTime Date { get; set; }
        public bool AdmissionStatus { get; set; }
        public string ReturnedFromHospital { get; set; }
        public string DiagnosisFromHospital { get; set; }
        public string Medications { get; set; }
        public string BP { get; set; }
        public string BPLocation { get; set; }
        public string RedialPulse { get; set; }
        public string PulseLocation { get; set; }
        public string Temp { get; set; }
        public string TempLocation { get; set; }
        public string Resp { get; set; }
        public string RespLocation { get; set; }
        public string SP02 { get; set; }
        public string SP02Location { get; set; }
        public string Person { get; set; }
        public string Place { get; set; }
        public string Time { get; set; }
        public string Speech { get; set; }
        public string PrimaryLanguage { get; set; }
        public string PulpilsEquals { get; set; }
        public string PulpilsReactive { get; set; }
        public string Eyes { get; set; }
        public string GeneralFace { get; set; }
        public DateTime DateEntered { get; set; }
        public UserModel EnteredBy { get; set; }
    }
}