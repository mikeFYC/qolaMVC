using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_AllergyModel
    {
        public int Id { get; set; }
        [Required]
        public string Allergy_Name { get; set; }
        [Required]
        public string Category { get; set; }
    }
}