﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ExcerciseActivityDetailModel
    {
        public int Id { get; set; }
        public ResidentModel Resident { get; set; }
        public string ActivityName { get; set; }
        public int Week { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public UserModel EnteredBy { get; set; }
        public DateTime DateEntered { get; set; }
    }

    public class ExcerciseActivityDetailModel_mike
    {
        public int Id { get; set; }
        public int Residentid { get; set; }
        public string ResidentName { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public string SuiteNumber { get; set; }
        public int EnteredBy { get; set; }
        public string EnteredByName { get; set; }
        public DateTime DateEntered { get; set; }

        public string ActivityName_1_week1 { get; set; }
        public int ActivityID_1_week1 { get; set; }
        public bool Sunday_1_week1 { get; set; }
        public bool Monday_1_week1 { get; set; }
        public bool Tuesday_1_week1 { get; set; }
        public bool Wednesday_1_week1 { get; set; }
        public bool Thursday_1_week1 { get; set; }
        public bool Friday_1_week1 { get; set; }
        public bool Saturday_1_week1 { get; set; }
        public string ActivityName_2_week1 { get; set; }
        public int ActivityID_2_week1 { get; set; }
        public bool Sunday_2_week1 { get; set; }
        public bool Monday_2_week1 { get; set; }
        public bool Tuesday_2_week1 { get; set; }
        public bool Wednesday_2_week1 { get; set; }
        public bool Thursday_2_week1 { get; set; }
        public bool Friday_2_week1 { get; set; }
        public bool Saturday_2_week1 { get; set; }
        public string ActivityName_3_week1 { get; set; }
        public int ActivityID_3_week1 { get; set; }
        public bool Sunday_3_week1 { get; set; }
        public bool Monday_3_week1 { get; set; }
        public bool Tuesday_3_week1 { get; set; }
        public bool Wednesday_3_week1 { get; set; }
        public bool Thursday_3_week1 { get; set; }
        public bool Friday_3_week1 { get; set; }
        public bool Saturday_3_week1 { get; set; }
        public string ActivityName_4_week1 { get; set; }
        public int ActivityID_4_week1 { get; set; }
        public bool Sunday_4_week1 { get; set; }
        public bool Monday_4_week1 { get; set; }
        public bool Tuesday_4_week1 { get; set; }
        public bool Wednesday_4_week1 { get; set; }
        public bool Thursday_4_week1 { get; set; }
        public bool Friday_4_week1 { get; set; }
        public bool Saturday_4_week1 { get; set; }
        public string ActivityName_5_week1 { get; set; }
        public int ActivityID_5_week1 { get; set; }
        public bool Sunday_5_week1 { get; set; }
        public bool Monday_5_week1 { get; set; }
        public bool Tuesday_5_week1 { get; set; }
        public bool Wednesday_5_week1 { get; set; }
        public bool Thursday_5_week1 { get; set; }
        public bool Friday_5_week1 { get; set; }
        public bool Saturday_5_week1 { get; set; }
        public string ActivityName_6_week1 { get; set; }
        public int ActivityID_6_week1 { get; set; }
        public bool Sunday_6_week1 { get; set; }
        public bool Monday_6_week1 { get; set; }
        public bool Tuesday_6_week1 { get; set; }
        public bool Wednesday_6_week1 { get; set; }
        public bool Thursday_6_week1 { get; set; }
        public bool Friday_6_week1 { get; set; }
        public bool Saturday_6_week1 { get; set; }
        public string ActivityName_7_week1 { get; set; }
        public int ActivityID_7_week1 { get; set; }
        public bool Sunday_7_week1 { get; set; }
        public bool Monday_7_week1 { get; set; }
        public bool Tuesday_7_week1 { get; set; }
        public bool Wednesday_7_week1 { get; set; }
        public bool Thursday_7_week1 { get; set; }
        public bool Friday_7_week1 { get; set; }
        public bool Saturday_7_week1 { get; set; }
        public string ActivityName_8_week1 { get; set; }
        public int ActivityID_8_week1 { get; set; }
        public bool Sunday_8_week1 { get; set; }
        public bool Monday_8_week1 { get; set; }
        public bool Tuesday_8_week1 { get; set; }
        public bool Wednesday_8_week1 { get; set; }
        public bool Thursday_8_week1 { get; set; }
        public bool Friday_8_week1 { get; set; }
        public bool Saturday_8_week1 { get; set; }
        public string ActivityName_9_week1 { get; set; }
        public int ActivityID_9_week1 { get; set; }
        public bool Sunday_9_week1 { get; set; }
        public bool Monday_9_week1 { get; set; }
        public bool Tuesday_9_week1 { get; set; }
        public bool Wednesday_9_week1 { get; set; }
        public bool Thursday_9_week1 { get; set; }
        public bool Friday_9_week1 { get; set; }
        public bool Saturday_9_week1 { get; set; }
        public string ActivityName_10_week1 { get; set; }
        public int ActivityID_10_week1 { get; set; }
        public bool Sunday_10_week1 { get; set; }
        public bool Monday_10_week1 { get; set; }
        public bool Tuesday_10_week1 { get; set; }
        public bool Wednesday_10_week1 { get; set; }
        public bool Thursday_10_week1 { get; set; }
        public bool Friday_10_week1 { get; set; }
        public bool Saturday_10_week1 { get; set; }

        public string ActivityName_1_week2 { get; set; }
        public int ActivityID_1_week2 { get; set; }
        public bool Sunday_1_week2 { get; set; }
        public bool Monday_1_week2 { get; set; }
        public bool Tuesday_1_week2 { get; set; }
        public bool Wednesday_1_week2 { get; set; }
        public bool Thursday_1_week2 { get; set; }
        public bool Friday_1_week2 { get; set; }
        public bool Saturday_1_week2 { get; set; }
        public string ActivityName_2_week2 { get; set; }
        public int ActivityID_2_week2 { get; set; }
        public bool Sunday_2_week2 { get; set; }
        public bool Monday_2_week2 { get; set; }
        public bool Tuesday_2_week2 { get; set; }
        public bool Wednesday_2_week2 { get; set; }
        public bool Thursday_2_week2 { get; set; }
        public bool Friday_2_week2 { get; set; }
        public bool Saturday_2_week2 { get; set; }
        public string ActivityName_3_week2 { get; set; }
        public int ActivityID_3_week2 { get; set; }
        public bool Sunday_3_week2 { get; set; }
        public bool Monday_3_week2 { get; set; }
        public bool Tuesday_3_week2 { get; set; }
        public bool Wednesday_3_week2 { get; set; }
        public bool Thursday_3_week2 { get; set; }
        public bool Friday_3_week2 { get; set; }
        public bool Saturday_3_week2 { get; set; }
        public string ActivityName_4_week2 { get; set; }
        public int ActivityID_4_week2 { get; set; }
        public bool Sunday_4_week2 { get; set; }
        public bool Monday_4_week2 { get; set; }
        public bool Tuesday_4_week2 { get; set; }
        public bool Wednesday_4_week2 { get; set; }
        public bool Thursday_4_week2 { get; set; }
        public bool Friday_4_week2 { get; set; }
        public bool Saturday_4_week2 { get; set; }
        public string ActivityName_5_week2 { get; set; }
        public int ActivityID_5_week2 { get; set; }
        public bool Sunday_5_week2 { get; set; }
        public bool Monday_5_week2 { get; set; }
        public bool Tuesday_5_week2 { get; set; }
        public bool Wednesday_5_week2 { get; set; }
        public bool Thursday_5_week2 { get; set; }
        public bool Friday_5_week2 { get; set; }
        public bool Saturday_5_week2 { get; set; }
        public string ActivityName_6_week2 { get; set; }
        public int ActivityID_6_week2 { get; set; }
        public bool Sunday_6_week2 { get; set; }
        public bool Monday_6_week2 { get; set; }
        public bool Tuesday_6_week2 { get; set; }
        public bool Wednesday_6_week2 { get; set; }
        public bool Thursday_6_week2 { get; set; }
        public bool Friday_6_week2 { get; set; }
        public bool Saturday_6_week2 { get; set; }
        public string ActivityName_7_week2 { get; set; }
        public int ActivityID_7_week2 { get; set; }
        public bool Sunday_7_week2 { get; set; }
        public bool Monday_7_week2 { get; set; }
        public bool Tuesday_7_week2 { get; set; }
        public bool Wednesday_7_week2 { get; set; }
        public bool Thursday_7_week2 { get; set; }
        public bool Friday_7_week2 { get; set; }
        public bool Saturday_7_week2 { get; set; }
        public string ActivityName_8_week2 { get; set; }
        public int ActivityID_8_week2 { get; set; }
        public bool Sunday_8_week2 { get; set; }
        public bool Monday_8_week2 { get; set; }
        public bool Tuesday_8_week2 { get; set; }
        public bool Wednesday_8_week2 { get; set; }
        public bool Thursday_8_week2 { get; set; }
        public bool Friday_8_week2 { get; set; }
        public bool Saturday_8_week2 { get; set; }
        public string ActivityName_9_week2 { get; set; }
        public int ActivityID_9_week2 { get; set; }
        public bool Sunday_9_week2 { get; set; }
        public bool Monday_9_week2 { get; set; }
        public bool Tuesday_9_week2 { get; set; }
        public bool Wednesday_9_week2 { get; set; }
        public bool Thursday_9_week2 { get; set; }
        public bool Friday_9_week2 { get; set; }
        public bool Saturday_9_week2 { get; set; }
        public string ActivityName_10_week2 { get; set; }
        public int ActivityID_10_week2 { get; set; }
        public bool Sunday_10_week2 { get; set; }
        public bool Monday_10_week2 { get; set; }
        public bool Tuesday_10_week2 { get; set; }
        public bool Wednesday_10_week2 { get; set; }
        public bool Thursday_10_week2 { get; set; }
        public bool Friday_10_week2 { get; set; }
        public bool Saturday_10_week2 { get; set; }

        public string ActivityName_1_week3 { get; set; }
        public int ActivityID_1_week3 { get; set; }
        public bool Sunday_1_week3 { get; set; }
        public bool Monday_1_week3 { get; set; }
        public bool Tuesday_1_week3 { get; set; }
        public bool Wednesday_1_week3 { get; set; }
        public bool Thursday_1_week3 { get; set; }
        public bool Friday_1_week3 { get; set; }
        public bool Saturday_1_week3 { get; set; }
        public string ActivityName_2_week3 { get; set; }
        public int ActivityID_2_week3 { get; set; }
        public bool Sunday_2_week3 { get; set; }
        public bool Monday_2_week3 { get; set; }
        public bool Tuesday_2_week3 { get; set; }
        public bool Wednesday_2_week3 { get; set; }
        public bool Thursday_2_week3 { get; set; }
        public bool Friday_2_week3 { get; set; }
        public bool Saturday_2_week3 { get; set; }
        public string ActivityName_3_week3 { get; set; }
        public int ActivityID_3_week3 { get; set; }
        public bool Sunday_3_week3 { get; set; }
        public bool Monday_3_week3 { get; set; }
        public bool Tuesday_3_week3 { get; set; }
        public bool Wednesday_3_week3 { get; set; }
        public bool Thursday_3_week3 { get; set; }
        public bool Friday_3_week3 { get; set; }
        public bool Saturday_3_week3 { get; set; }
        public string ActivityName_4_week3 { get; set; }
        public int ActivityID_4_week3 { get; set; }
        public bool Sunday_4_week3 { get; set; }
        public bool Monday_4_week3 { get; set; }
        public bool Tuesday_4_week3 { get; set; }
        public bool Wednesday_4_week3 { get; set; }
        public bool Thursday_4_week3 { get; set; }
        public bool Friday_4_week3 { get; set; }
        public bool Saturday_4_week3 { get; set; }
        public string ActivityName_5_week3 { get; set; }
        public int ActivityID_5_week3 { get; set; }
        public bool Sunday_5_week3 { get; set; }
        public bool Monday_5_week3 { get; set; }
        public bool Tuesday_5_week3 { get; set; }
        public bool Wednesday_5_week3 { get; set; }
        public bool Thursday_5_week3 { get; set; }
        public bool Friday_5_week3 { get; set; }
        public bool Saturday_5_week3 { get; set; }
        public string ActivityName_6_week3 { get; set; }
        public int ActivityID_6_week3 { get; set; }
        public bool Sunday_6_week3 { get; set; }
        public bool Monday_6_week3 { get; set; }
        public bool Tuesday_6_week3 { get; set; }
        public bool Wednesday_6_week3 { get; set; }
        public bool Thursday_6_week3 { get; set; }
        public bool Friday_6_week3 { get; set; }
        public bool Saturday_6_week3 { get; set; }
        public string ActivityName_7_week3 { get; set; }
        public int ActivityID_7_week3 { get; set; }
        public bool Sunday_7_week3 { get; set; }
        public bool Monday_7_week3 { get; set; }
        public bool Tuesday_7_week3 { get; set; }
        public bool Wednesday_7_week3 { get; set; }
        public bool Thursday_7_week3 { get; set; }
        public bool Friday_7_week3 { get; set; }
        public bool Saturday_7_week3 { get; set; }
        public string ActivityName_8_week3 { get; set; }
        public int ActivityID_8_week3 { get; set; }
        public bool Sunday_8_week3 { get; set; }
        public bool Monday_8_week3 { get; set; }
        public bool Tuesday_8_week3 { get; set; }
        public bool Wednesday_8_week3 { get; set; }
        public bool Thursday_8_week3 { get; set; }
        public bool Friday_8_week3 { get; set; }
        public bool Saturday_8_week3 { get; set; }
        public string ActivityName_9_week3 { get; set; }
        public int ActivityID_9_week3 { get; set; }
        public bool Sunday_9_week3 { get; set; }
        public bool Monday_9_week3 { get; set; }
        public bool Tuesday_9_week3 { get; set; }
        public bool Wednesday_9_week3 { get; set; }
        public bool Thursday_9_week3 { get; set; }
        public bool Friday_9_week3 { get; set; }
        public bool Saturday_9_week3 { get; set; }
        public string ActivityName_10_week3 { get; set; }
        public int ActivityID_10_week3 { get; set; }
        public bool Sunday_10_week3 { get; set; }
        public bool Monday_10_week3 { get; set; }
        public bool Tuesday_10_week3 { get; set; }
        public bool Wednesday_10_week3 { get; set; }
        public bool Thursday_10_week3 { get; set; }
        public bool Friday_10_week3 { get; set; }
        public bool Saturday_10_week3 { get; set; }

        public string ActivityName_1_week4 { get; set; }
        public int ActivityID_1_week4 { get; set; }
        public bool Sunday_1_week4 { get; set; }
        public bool Monday_1_week4 { get; set; }
        public bool Tuesday_1_week4 { get; set; }
        public bool Wednesday_1_week4 { get; set; }
        public bool Thursday_1_week4 { get; set; }
        public bool Friday_1_week4 { get; set; }
        public bool Saturday_1_week4 { get; set; }
        public string ActivityName_2_week4 { get; set; }
        public int ActivityID_2_week4 { get; set; }
        public bool Sunday_2_week4 { get; set; }
        public bool Monday_2_week4 { get; set; }
        public bool Tuesday_2_week4 { get; set; }
        public bool Wednesday_2_week4 { get; set; }
        public bool Thursday_2_week4 { get; set; }
        public bool Friday_2_week4 { get; set; }
        public bool Saturday_2_week4 { get; set; }
        public string ActivityName_3_week4 { get; set; }
        public int ActivityID_3_week4 { get; set; }
        public bool Sunday_3_week4 { get; set; }
        public bool Monday_3_week4 { get; set; }
        public bool Tuesday_3_week4 { get; set; }
        public bool Wednesday_3_week4 { get; set; }
        public bool Thursday_3_week4 { get; set; }
        public bool Friday_3_week4 { get; set; }
        public bool Saturday_3_week4 { get; set; }
        public string ActivityName_4_week4 { get; set; }
        public int ActivityID_4_week4 { get; set; }
        public bool Sunday_4_week4 { get; set; }
        public bool Monday_4_week4 { get; set; }
        public bool Tuesday_4_week4 { get; set; }
        public bool Wednesday_4_week4 { get; set; }
        public bool Thursday_4_week4 { get; set; }
        public bool Friday_4_week4 { get; set; }
        public bool Saturday_4_week4 { get; set; }
        public string ActivityName_5_week4 { get; set; }
        public int ActivityID_5_week4 { get; set; }
        public bool Sunday_5_week4 { get; set; }
        public bool Monday_5_week4 { get; set; }
        public bool Tuesday_5_week4 { get; set; }
        public bool Wednesday_5_week4 { get; set; }
        public bool Thursday_5_week4 { get; set; }
        public bool Friday_5_week4 { get; set; }
        public bool Saturday_5_week4 { get; set; }
        public string ActivityName_6_week4 { get; set; }
        public int ActivityID_6_week4 { get; set; }
        public bool Sunday_6_week4 { get; set; }
        public bool Monday_6_week4 { get; set; }
        public bool Tuesday_6_week4 { get; set; }
        public bool Wednesday_6_week4 { get; set; }
        public bool Thursday_6_week4 { get; set; }
        public bool Friday_6_week4 { get; set; }
        public bool Saturday_6_week4 { get; set; }
        public string ActivityName_7_week4 { get; set; }
        public int ActivityID_7_week4 { get; set; }
        public bool Sunday_7_week4 { get; set; }
        public bool Monday_7_week4 { get; set; }
        public bool Tuesday_7_week4 { get; set; }
        public bool Wednesday_7_week4 { get; set; }
        public bool Thursday_7_week4 { get; set; }
        public bool Friday_7_week4 { get; set; }
        public bool Saturday_7_week4 { get; set; }
        public string ActivityName_8_week4 { get; set; }
        public int ActivityID_8_week4 { get; set; }
        public bool Sunday_8_week4 { get; set; }
        public bool Monday_8_week4 { get; set; }
        public bool Tuesday_8_week4 { get; set; }
        public bool Wednesday_8_week4 { get; set; }
        public bool Thursday_8_week4 { get; set; }
        public bool Friday_8_week4 { get; set; }
        public bool Saturday_8_week4 { get; set; }
        public string ActivityName_9_week4 { get; set; }
        public int ActivityID_9_week4 { get; set; }
        public bool Sunday_9_week4 { get; set; }
        public bool Monday_9_week4 { get; set; }
        public bool Tuesday_9_week4 { get; set; }
        public bool Wednesday_9_week4 { get; set; }
        public bool Thursday_9_week4 { get; set; }
        public bool Friday_9_week4 { get; set; }
        public bool Saturday_9_week4 { get; set; }
        public string ActivityName_10_week4 { get; set; }
        public int ActivityID_10_week4 { get; set; }
        public bool Sunday_10_week4 { get; set; }
        public bool Monday_10_week4 { get; set; }
        public bool Tuesday_10_week4 { get; set; }
        public bool Wednesday_10_week4 { get; set; }
        public bool Thursday_10_week4 { get; set; }
        public bool Friday_10_week4 { get; set; }
        public bool Saturday_10_week4 { get; set; }


    }


}