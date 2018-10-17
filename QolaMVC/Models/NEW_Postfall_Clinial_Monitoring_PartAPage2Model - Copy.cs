using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_Postfall_Clinial_Monitoring_PartAPage2Model
    {
        public int id { get; set; }
        [Required]
        public int linkid { get; set; }
        [Required]
        public string chest_longsound_normal { get; set; }
        [Required]
        public string chest_longsound_describe { get; set; }
        [Required]
        public string chest_longsound_equal { get; set; }
        [Required]
        public string chest_chest { get; set; }
        [Required]
        public string chest_heartsound { get; set; }
        [Required]
        public string chest_heartsound_describe { get; set; }
        [Required]
        public string abdomen_soft { get; set; }
        [Required]
        public string abdomen_soft_describe { get; set; }
        [Required]
        public string abdomen_painful { get; set; }
        [Required]
        public string abdomen_painful_describe { get; set; }
        [Required]
        public string abdomen_bowelsound { get; set; }
        [Required]
        public string abdomen_lastbowel_date { get; set; }
        [Required]
        public string abdomen_voidingnormal { get; set; }
        [Required]
        public string abdomen_voidingnormal_describe { get; set; }


        [Required]
        public string abdomen_voidingnormal1 { get; set; }
        [Required]
        public string abdomen_voidingnormal_pads { get; set; }
        [Required]
        public string abdomen_voidingnormal2 { get; set; }
        [Required]
        public string edema_feet_normal { get; set; }
        [Required]
        public string edema_feet_describe { get; set; }
        [Required]
        public string edema_feet1 { get; set; }
        [Required]
        public string edema_hands_normal { get; set; }
        [Required]
        public string edema_hands_describe { get; set; }
        [Required]
        public string edema_hands1 { get; set; }
        [Required]
        public string edema_other { get; set; }
        [Required]
        public string edema_other_describe { get; set; }

        [Required]
        public string skin_feet { get; set; }
        [Required]
        public string skin_feet_describe { get; set; }
        [Required]
        public string skin_rashes { get; set; }
        [Required]
        public string skin_redness { get; set; }
        [Required]
        public string skin_bruising { get; set; }
        [Required]
        public string skin_openareas { get; set; }
        [Required]
        public string skin_desc_abnormal { get; set; }
        [Required]
        public string skin_wounddressing { get; set; }
        [Required]
        public string skin_desc { get; set; }
        [Required]
        public string pain_residentpain { get; set; }
        [Required]
        public string pain_residentpain_desc { get; set; }

        [Required]
        public string pain_painscale { get; set; }
        [Required]
        public string pain_aching { get; set; }
        [Required]
        public string pain_sharp { get; set; }
        [Required]
        public string pain_dull { get; set; }
        [Required]
        public string pain_radiating { get; set; }
        [Required]
        public string pain_where { get; set; }
        [Required]
        public string pain_whatmakes_better { get; set; }
        [Required]
        public string pain_whatmakes_worst { get; set; }
        [Required]
        public string pain_interface_adl { get; set; }
        [Required]
        public string pain_describe { get; set; }
        [Required]
        public string pain_other { get; set; }
        [Required]
        public string completed_by { get; set; }
        public DateTime Created_at { get; set; }
    }

 

}