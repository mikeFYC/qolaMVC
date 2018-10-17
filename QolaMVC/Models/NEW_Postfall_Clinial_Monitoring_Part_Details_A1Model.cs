using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NEW_Postfall_Clinial_Monitoring_Part_Details_A1Model
    {
        public int Id { get; set; }
        [Required]
        public int Linkid { get; set; }
        [Required]
        public int tableid { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Firstcheck { get; set; }
        [Required]
        public string Onehourfirstcheck { get; set; }
        [Required]
        public string Onehoursecondcheck { get; set; }
        [Required]
        public string Threehoursfirstcheck { get; set; }
        [Required]
        public string Threehourssecondcheck { get; set; }
        [Required]
        public string Threehoursthirdcheck { get; set; }
        [Required]
        public string Fourtyeighthoursfirstcheck { get; set; }
        [Required]
        public string Fourtyeighthourssecondcheck { get; set; }
        [Required]
        public string Fourtyeighthoursthirdcheck { get; set; }
        [Required]
        public string Fourtyeighthoursfourthcheck { get; set; }
        [Required]
        public string Fourtyeighthoursfifthcheck { get; set; }
        [Required]
        public string Pf_Clinical_Monitoring_Part { get; set; }
        public DateTime Created_at { get; set; }
    }

    public class NEW_Postfall_Clinial_Monitoring_SplitModel
    {
         public int id { get; set; }
        [Required]
        public string pf_Clinical_Monitoring_Part { get; set; }
        public DateTime Created_at { get; set; }
    }

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}
    public class CheckBoxes
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
    public class MasterDetails  
    {  
        public List<NEW_Postfall_Clinial_Monitoring_Part_Details_A1Model> A1Model { get; set; }  
        public List<NEW_Postfall_Clinial_Monitoring_SplitModel> SplitMonitoring { get; set; }  
        
       // public List<NEW_Postfall_Clinial_Monitoring_PartAPage2Model> PartAPage2 { get; set; }  

 public int id { get; set; }
        [Required]
        public int linkid { get; set; }

        public string c_longsound_normal { get; set; }
          
        public string c_longsound_describe { get; set; }
         
        public string c_longsound_equal { get; set; }
        public string c_c { get; set; }
        public string c_cvalue { get; set; }
    public List<CheckBoxes>  lstPreprationRequired{ get; set; }
    public IEnumerable<Item> c_cItems { 
        get 
        {
            return new[] 
            {
                new Item { Id = 1, Name = "NORMAL" },
                new Item { Id = 2, Name = "BRUISING" },
                new Item { Id = 3, Name = "SWELLING" },
            };
        } 
    }
        [Required]
        public string c_c_other { get; set; }
        [Required]
        public string c_heartsound { get; set; }
        [Required]
        public string c_heartsound_describe { get; set; }
        [Required]
        public string a_soft { get; set; }
        [Required]
        public string a_soft_describe { get; set; }
        [Required]
        public string a_pful { get; set; }
        [Required]
        public string a_pful_describe { get; set; }
        [Required]
        public string a_bowelsound { get; set; }
        [Required]
        public string a_lastbowel_date { get; set; }
        [Required]
        public string a_voidingnormal { get; set; }
        [Required]
        public string a_voidingnormal_describe { get; set; }


        [Required]
        public string a_voidingnormal1 { get; set; }
        [Required]
        public string a_voidingnormal_pads { get; set; }
        [Required]
        public string a_voidingnormal2 { get; set; }
        [Required]
        public string edema_feet_normal { get; set; }
        [Required]
        public string edema_feet_describe { get; set; }
        [Required]
        public string edema_feet1 { get; set; }
        public string edema_feet1value { get; set; }
    public List<CheckBoxes>  lstPreprationRequired1{ get; set; }
    public IEnumerable<Item> edema_feet1Items { 
        get 
        {
            return new[] 
            {
                new Item { Id = 1, Name = "LEFT" },
                new Item { Id = 2, Name = "RIGHT" },
                new Item { Id = 3, Name = "PITTING" },
            };
        } 
    }
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
        public string p_residentp { get; set; }
        [Required]
        public string p_residentp_desc { get; set; }

        [Required]
        public string p_pscale { get; set; }
        [Required]
        public string p_aching { get; set; }
        [Required]
        public string p_sharp { get; set; }
        [Required]
        public string p_dull { get; set; }
        [Required]
        public string p_radiating { get; set; }
        [Required]
        public string p_where { get; set; }
        [Required]
        public string p_whatmakes_better { get; set; }
        [Required]
        public string p_whatmakes_worst { get; set; }
        [Required]
        public string p_interface_adl { get; set; }
        [Required]
        public string p_describe { get; set; }
        [Required]
        public string p_other { get; set; }
        [Required]
        public string completed_by { get; set; }
        public DateTime Created_at { get; set; }
    }  

}