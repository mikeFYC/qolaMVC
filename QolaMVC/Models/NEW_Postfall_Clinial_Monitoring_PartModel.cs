using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_Postfall_Clinial_Monitoring_PartModel
    {
        public int Id { get; set; }
        [Required]
        public string Pf_Clinical_Monitoring_Part { get; set; }
        [Required]
        public string Table_id { get; set; }
        public DateTime Created_at { get; set; }
    }
}