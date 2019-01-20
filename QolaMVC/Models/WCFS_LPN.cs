using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class WCFS_LPN
    {
        public int Id { get; set; }
        public int currentWound { get; set; }
        public int totalWound { get; set; }
        public DateTime dateEntered { get; set; }
        public int EnteredBy { get; set; }

        public string Wound_Size_L_Time1 { get; set; }
        public string Wound_Size_W_Time1 { get; set; }
        public string Wound_Size_D_Time1 { get; set; }
        public string Wound_Size_L_Time2 { get; set; }
        public string Wound_Size_W_Time2 { get; set; }
        public string Wound_Size_D_Time2 { get; set; }
        public string Wound_Size_L_Time3 { get; set; }
        public string Wound_Size_W_Time3 { get; set; }
        public string Wound_Size_D_Time3 { get; set; }
        public string Wound_Size_L_Time4 { get; set; }
        public string Wound_Size_W_Time4 { get; set; }
        public string Wound_Size_D_Time4 { get; set; }

        public string Wound_Undermining_Time1 { get; set; }
        public string Wound_Undermining_Time2 { get; set; }
        public string Wound_Undermining_Time3 { get; set; }
        public string Wound_Undermining_Time4 { get; set; }

        public string Wound_Eschar_Time1 { get; set; }
        public string Wound_Eschar_Time2 { get; set; }
        public string Wound_Eschar_Time3 { get; set; }
        public string Wound_Eschar_Time4 { get; set; }

        public string Wound_Pain_Time1 { get; set; }
        public string Wound_Pain_Time2 { get; set; }
        public string Wound_Pain_Time3 { get; set; }
        public string Wound_Pain_Time4 { get; set; }

        public string Wound_Drainage_Amount_Time1 { get; set; }
        public string Wound_Drainage_Amount_Time2 { get; set; }
        public string Wound_Drainage_Amount_Time3 { get; set; }
        public string Wound_Drainage_Amount_Time4 { get; set; }

        public string Wound_Drainage_Color_Time1 { get; set; }
        public string Wound_Drainage_Color_Time2 { get; set; }
        public string Wound_Drainage_Color_Time3 { get; set; }
        public string Wound_Drainage_Color_Time4 { get; set; }

        public string Wound_Odor_Time1 { get; set; }
        public string Wound_Odor_Time2 { get; set; }
        public string Wound_Odor_Time3 { get; set; }
        public string Wound_Odor_Time4 { get; set; }

        public string Wound_Skin_Time1 { get; set; }
        public string Wound_Skin_Time2 { get; set; }
        public string Wound_Skin_Time3 { get; set; }
        public string Wound_Skin_Time4 { get; set; }

        public string Wound_Edge_Time1 { get; set; }
        public string Wound_Edge_Time2 { get; set; }
        public string Wound_Edge_Time3 { get; set; }
        public string Wound_Edge_Time4 { get; set; }

        public string Wound_Dressing_Time1 { get; set; }
        public string Wound_Dressing_Time2 { get; set; }
        public string Wound_Dressing_Time3 { get; set; }
        public string Wound_Dressing_Time4 { get; set; }

        public string Wound_Comment_Time1 { get; set; }
        public string Wound_Comment_Time2 { get; set; }
        public string Wound_Comment_Time3 { get; set; }
        public string Wound_Comment_Time4 { get; set; }

        public string Wound_Signature_Time1 { get; set; }
        public string Wound_Signature_Time2 { get; set; }
        public string Wound_Signature_Time3 { get; set; }
        public string Wound_Signature_Time4 { get; set; }



    }
}