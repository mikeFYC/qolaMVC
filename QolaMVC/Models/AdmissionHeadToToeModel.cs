using System;
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
        public string AdmissionStatus { get; set; }
        public string ReturnedFromHospital { get; set; }
        public string DiagnosisFromHospital { get; set; }
        public string Medications { get; set; }
        public string BP { get; set; }
        public string BPLocation { get; set; }
        public string BPPosition { get; set; }
        public string BPPosition_other { get; set; }
        public string RedialPulse { get; set; }
        public string PulseLocation { get; set; }
        public string PulseStrength { get; set; }
        public string PulseStrength_other { get; set; }
        public string Temp { get; set; }
        public string TempLocation { get; set; }
        public string TempLocation_other { get; set; }
        public string Resp { get; set; }
        public string RespLocation { get; set; }
        public string RespLocation_other { get; set; }
        public string SP02 { get; set; }
        public string SP02Location { get; set; }
        public string SP02Location_other { get; set; }
        public string Person { get; set; }
        public string Place { get; set; }
        public string Time { get; set; }
        public string Speech { get; set; }
        public string Speech_other { get; set; }
        public string PrimaryLanguage { get; set; }
        public string PrimaryLanguage_other { get; set; }
        public string PulpilsEquals { get; set; }
        public string PulpilsReactive { get; set; }
        public string Eyes { get; set; }
        public string Eyes_other { get; set; }
        public string GeneralFace { get; set; }
        public DateTime DateEntered { get; set; }
        public UserModel EnteredBy { get; set; }



        //PART 2

        public int PART2_id { get; set; }        
        public int linkid { get; set; }
        public string c_longsound_normal { get; set; }
        public string c_longsound_describe { get; set; }
        public string c_longsound_equal { get; set; }
        public string c_c { get; set; }
        public string c_cvalue { get; set; }      
        public string c_c_other { get; set; }        
        public string c_heartsound { get; set; }        
        public string c_heartsound_describe { get; set; }        
        public string a_soft { get; set; }        
        public string a_soft_describe { get; set; }       
        public string a_pful { get; set; }       
        public string a_pful_describe { get; set; }      
        public string a_bowelsound { get; set; }        
        public string a_lastbowel_date { get; set; }        
        public string a_voidingnormal { get; set; }        
        public string a_voidingnormal_describe { get; set; }      
        public string a_voidingnormal1 { get; set; }       
        public string a_voidingnormal_pads { get; set; }        
        public string a_voidingnormal2 { get; set; }        
        public string edema_feet_normal { get; set; }        
        public string edema_feet_describe { get; set; }        
        public string edema_feet1 { get; set; }
        public string edema_feet1value { get; set; }    
        public string edema_hands_normal { get; set; }       
        public string edema_hands_describe { get; set; }        
        public string edema_hands1 { get; set; }        
        public string edema_other { get; set; }        
        public string edema_other_describe { get; set; }        
        public string skin_feet { get; set; }        
        public string skin_feet_describe { get; set; }        
        public string skin_rashes { get; set; }        
        public string skin_redness { get; set; }        
        public string skin_bruising { get; set; }        
        public string skin_openareas { get; set; }        
        public string skin_desc_abnormal { get; set; }        
        public string skin_wounddressing { get; set; }        
        public string skin_desc { get; set; }       
        public string p_residentp { get; set; }        
        public string p_residentp_desc { get; set; }        
        public string p_pscale { get; set; }        
        public string p_aching { get; set; }        
        public string p_sharp { get; set; }        
        public string p_dull { get; set; }        
        public string p_radiating { get; set; }
        public string p_where { get; set; }
        public string p_whatmakes_better { get; set; }
        public string p_whatmakes_worst { get; set; }
        public string p_interface_adl { get; set; }
        public string p_describe { get; set; }
        public string p_other { get; set; }
        public string completed_by { get; set; }
        public DateTime Created_at { get; set; }
        public bool c_c_check1 { get; set; }
        public bool c_c_check2 { get; set; }
        public bool c_c_check3 { get; set; }
        public bool edema_feet1_check1 { get; set; }
        public bool edema_feet1_check2 { get; set; }
        public bool edema_feet1_check3 { get; set; }
        public bool edema_hands1_check1 { get; set; }
        public bool edema_hands1_check2 { get; set; }
        public bool edema_hands1_check3 { get; set; }
    }
}