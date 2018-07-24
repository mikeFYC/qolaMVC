using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_SuiteModel
    {
        public int Id { get; set; }
        [Required]
        public string Home { get; set; }
        [Required]
        public int Suite_No { get; set; }
        [Required]
        public int Floor_No { get; set; }
        [Required]
        public int No_Of_Rooms { get; set; }
    }
}