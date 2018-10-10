using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_Postfall_Clinial_Monitoring_Part_Details_A2Model
    {
        public int Id { get; set; }
        [Required]
        public int Linkid { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Firstcheck { get; set; }
        [Required]
        public string Fourhourfirstcheck { get; set; }
        [Required]
        public string Fourhoursecondcheck { get; set; }
        [Required]
        public string Fourhoursthirdcheck { get; set; }
        [Required]
        public string Fourhoursforthcheck { get; set; }
        [Required]
        public string Threehoursfirstcheck { get; set; }
        [Required]
        public string Threehourssecondcheck { get; set; }
        [Required]
        public string Threehoursthirdcheck { get; set; }
        [Required]
        public string Threehoursforthcheck { get; set; }
        [Required]
        public string Threehoursfifthcheck { get; set; }
        [Required]
        public string Threehourssixthcheck { get; set; }
    }
}