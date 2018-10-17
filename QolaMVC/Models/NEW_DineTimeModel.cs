using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_DineTimeModel
    {
        public int Id { get; set; }
        [Required]
        public string DineTime { get; set; }
        [Required]
        public string ShortName { get; set; }
    }
}