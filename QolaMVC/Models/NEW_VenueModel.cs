using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_VenueModel
    {
        public int Id { get; set; }
        [Required]
        public string Home { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Venue { get; set; }

        public string HomeName { get; set; }
    }
}