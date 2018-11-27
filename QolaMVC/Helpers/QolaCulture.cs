using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.Security;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using QolaMVC.ViewModels;
using QolaMVC.Models;
using System.Collections.ObjectModel;

namespace QolaMVC.Helpers
{
     public class QolaCulture : System.Web.UI.Page
    {
        public const string CRYPTO_KEY = "morpheus";
        public static string[] dateFormats = { "yyyy/MM/dd", "yyyy-MM-dd", "MM/dd/yyyy hh:mm:ss", "MM/dd/yyyy hh:mm", "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy HH:mm:ss tt", "MM/dd/yyyy HH:mm:ss", "MM/dd/yyyy", "MM-dd-yyyy", "MM/dd/yyyy H:mm:ss tt", "MM/dd/yyyy h:mm:ss tt" };
        public static string[] timeFormats = { "hh:mm:ss", "hh:mm tt", "hh tt", "hh:mm", "HH:mm:ss tt", "HH:mm:ss", "HH:mm" };
        
        protected override void InitializeCulture()
        {
            if (Session["CultureInfo"] != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["CultureInfo"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CultureInfo"].ToString());
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
            base.InitializeCulture();
        }
        public static DateTime stringToDateFormat(string sDate)
        {
            DateTime convertedDate = new DateTime();
            string exception = string.Empty;
            try
            {
                convertedDate = DateTime.ParseExact(sDate, dateFormats, new CultureInfo("en-US"), DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | stringToDateFormat " + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return convertedDate;
        }
        public static DateTime stringToDateFormat(string sDate, string sDateFormat)
        {
            DateTime convertedDate = new DateTime();
            string exception = string.Empty;
            try
            {
                convertedDate = DateTime.ParseExact(sDate, sDateFormat, new CultureInfo("en-US"), DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | stringToDateFormat " + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return convertedDate;
        }
        public static string dateStringToUSDateStringFormat(string sDate)
        {
            string SConvertedDate = string.Empty;
            string exception = string.Empty;
            try
            {
                DateTime convertedDate = DateTime.ParseExact(sDate, dateFormats, new CultureInfo("en-US"), DateTimeStyles.None);
                SConvertedDate = dateToUSDateStringFormat(convertedDate, "MM/dd/yyyy");
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | stringToDateFormate" + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return SConvertedDate;
        }
        public static string dateToUSDateStringFormat(int iDays)
        {
            string result = string.Empty;
            string exception = string.Empty;
            try
            {
                DateTimeFormatInfo usDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                result = DateTime.Now.AddDays(iDays).ToString("MM/dd/yyyy", usDTFI);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | dateToUSDateStringFormate" + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }
        public static string dateToUSDateStringFormat(int iDays, string sDateFormat)
        {
            string result = string.Empty;
            string exception = string.Empty;
            try
            {
                DateTimeFormatInfo usDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                result = DateTime.Now.AddDays(iDays).ToString(sDateFormat, usDTFI);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | dateToUSDateStringFormate" + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }
        public static string dateToUSDateStringFormat(DateTime dtDays)
        {
            string result = string.Empty;
            string exception = string.Empty;
            try
            {
                DateTimeFormatInfo usDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                result = dtDays.ToString("MM/dd/yyyy", usDTFI);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | dateToUSDateStringFormate" + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }
        public static string dateToUSDateStringFormat(DateTime dtDays, string sDateFormat)
        {
            string result = string.Empty;
            string exception = string.Empty;
            try
            {
                DateTimeFormatInfo usDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                result = dtDays.ToString(sDateFormat, usDTFI);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | dateToUSDateStringFormate" + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }
        public static string dateToUSTimeStringFormat(DateTime dtDate, string stimeFormat)
        {
            string result = string.Empty;
            string exception = string.Empty;
            try
            {
                DateTimeFormatInfo usDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                result = dtDate.ToString(stimeFormat, usDTFI);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | dateToUSTimeStringFormate" + ex.ToString();
                //Log.Write(exception);
                //Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }

        public static string staticdateToUSDateStringFormat(DateTime dtDays, string sDateFormat)
        {
            string result = string.Empty;
            string exception = string.Empty;
            try
            {
                DateTimeFormatInfo usDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                result = dtDays.ToString(sDateFormat, usDTFI);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture |static dateToUSDateStringFormate" + ex.ToString();
                //Log.Write(exception);
            }
            return result;
        }

        public static DateTime staticStringToDateFormat(string sDate, string sDateFormat)
        {
            DateTime convertedDate = new DateTime();
            string exception = string.Empty;
            try
            {
                convertedDate = DateTime.ParseExact(sDate, sDateFormat, new CultureInfo("en-US"), DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                exception = "QolaCulture | stringToDateFormat " + ex.ToString();
                //Log.Write(exception);
            }
            return convertedDate;
        }

        public static string getMonthShortName(int month)
        {
            string[] months;
            months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            return months[month - 1];
        }

        public static string GetJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public static DataTable GetDayOfWeek()
        {
            DataTable dtDaysOfWeek = new DataTable("DaysOfWeek");
            dtDaysOfWeek.Columns.Add("ID");
            dtDaysOfWeek.Columns.Add("Code");
            dtDaysOfWeek.Columns.Add("DayName");
            foreach (DayOfWeek val in Enum.GetValues(typeof(DayOfWeek)))
            {
                dtDaysOfWeek.Rows.Add((int)val, val.ToString().Substring(0, 2).ToUpper(), val.ToString());
            }

            return dtDaysOfWeek;
        }

        public static DataTable GetDays()
        {
            DataTable dtDays = new DataTable("Days");
            dtDays.Columns.Add("ID");
            dtDays.Columns.Add("Days");
            for (int iDays = 1; iDays < 32; iDays++)
            {
                dtDays.Rows.Add((int)iDays, (int)iDays);
            }
            return dtDays;
        }

        public static string GetFileHash(string path)
        {
            string hash = (string)HttpContext.Current.Cache["_hash_" + path];

            if (hash == null)
            {
                string file = HttpContext.Current.Server.MapPath(path);

                hash = GetMD5(file);

                HttpContext.Current.Cache.Insert("_hash_" + path, hash, new System.Web.Caching.CacheDependency(file));
            }

            return hash;
        }

        public static string GetMD5(string file)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 8192);
            md5.ComputeHash(stream);
            stream.Close();
            byte[] hash = md5.Hash;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(string.Format("{0:X2}", b));
            }
            return sb.ToString();
        }
        public static string Sha1Hash(string password)
        {
            return BitConverter.ToString(new SHA1Managed().ComputeHash(new UTF8Encoding().GetBytes(password)));
        }

        public static string Sha1HashOld(string password)
        {
            return Encoding.UTF8.GetString(new SHA1Managed().ComputeHash(new UTF8Encoding().GetBytes(password)));
        }

        public static void InitExcerciseActivity(ref ExcerciseActivityViewModel vm)
        {
            if (vm.Detail.Count == 0)
            {


                    string[] activities = new string[]{"Walking",
                                                    "Wall Push-ups",
                                                    "Rise Up on toes",
                                                    "Toe Taps",
                                                    "Seat Walk",
                                                    "Get up from chair",
                                                    "Leg Lift",
                                                    "Reaching",
                                                    "Standing Stretch",
                                                    "Seated Stretch"
                                                };

                    foreach (var a in activities)
                    {
                        var ex = new Models.ExcerciseActivityDetailModel();
                        ex.ActivityName = a;
                        ex.DateEntered = DateTime.Now;

                        vm.Detail.Add(ex);
                        var hsep = new Models.HSEPDetailModel();
                        hsep.ActivityName = a;

                        vm.HSEPDetail.Add(hsep);
                    }
                
            }
        }


        //public static void InitExcerciseActivity_week1(ref ExcerciseActivityViewModel vm)
        //{
        //    if (vm.FIRST_WEEK.Count == 0)
        //    {
        //        string[] activities = new string[]{"Walking",
        //                                            "Wall Push-ups",
        //                                            "Rise Up on toes",
        //                                            "Toe Taps",
        //                                            "Seat Walk",
        //                                            "Get up from chair",
        //                                            "Leg Lift",
        //                                            "Reaching",
        //                                            "Standing Stretch",
        //                                            "Seated Stretch"
        //                                        };
        //        foreach (var a in activities)
        //        {
        //            var ex = new Models.ExcerciseActivityDetailModel();
        //            ex.ActivityName = a;
        //            ex.DateEntered = DateTime.Now;
        //            vm.FIRST_WEEK.Add(ex);
        //        }
        //    }
        //}
        //public static void InitExcerciseActivity_week2(ref ExcerciseActivityViewModel vm)
        //{
        //    if (vm.SECOND_WEEK.Count == 0)
        //    {
        //        string[] activities = new string[]{"Walking",
        //                                            "Wall Push-ups",
        //                                            "Rise Up on toes",
        //                                            "Toe Taps",
        //                                            "Seat Walk",
        //                                            "Get up from chair",
        //                                            "Leg Lift",
        //                                            "Reaching",
        //                                            "Standing Stretch",
        //                                            "Seated Stretch"
        //                                        };
        //        foreach (var a in activities)
        //        {
        //            var ex = new Models.ExcerciseActivityDetailModel();
        //            ex.ActivityName = a;
        //            ex.DateEntered = DateTime.Now;
        //            vm.SECOND_WEEK.Add(ex);
        //        }
        //    }
        //}
        //public static void InitExcerciseActivity_week3(ref ExcerciseActivityViewModel vm)
        //{
        //    if (vm.THIRD_WEEK.Count == 0)
        //    {
        //        string[] activities = new string[]{"Walking",
        //                                            "Wall Push-ups",
        //                                            "Rise Up on toes",
        //                                            "Toe Taps",
        //                                            "Seat Walk",
        //                                            "Get up from chair",
        //                                            "Leg Lift",
        //                                            "Reaching",
        //                                            "Standing Stretch",
        //                                            "Seated Stretch"
        //                                        };
        //        foreach (var a in activities)
        //        {
        //            var ex = new Models.ExcerciseActivityDetailModel();
        //            ex.ActivityName = a;
        //            ex.DateEntered = DateTime.Now;
        //            vm.THIRD_WEEK.Add(ex);
        //        }
        //    }
        //}
        //public static void InitExcerciseActivity_week4(ref ExcerciseActivityViewModel vm)
        //{
        //    if (vm.FORTH_WEEK.Count == 0)
        //    {
        //        string[] activities = new string[]{"Walking",
        //                                            "Wall Push-ups",
        //                                            "Rise Up on toes",
        //                                            "Toe Taps",
        //                                            "Seat Walk",
        //                                            "Get up from chair",
        //                                            "Leg Lift",
        //                                            "Reaching",
        //                                            "Standing Stretch",
        //                                            "Seated Stretch"
        //                                        };
        //        foreach (var a in activities)
        //        {
        //            var ex = new Models.ExcerciseActivityDetailModel();
        //            ex.ActivityName = a;
        //            ex.DateEntered = DateTime.Now;
        //            vm.FORTH_WEEK.Add(ex);
        //        }
        //    }
        //}



        public static void InitDiets(ref nDietaryAssessmentModel vm)
        {
            if (vm.Diet2 == null)
            {
                vm.Diet2 = new Collection <string>();
                string[] Diets = new string[]{"Regular Diet",
                                                    "Diabetic",
                                                    "Low Fat",
                                                    "Low Cholesterol",
                                                    "Low Potassium",
                                                    "Glutten Free",
                                                    "Vegetarian Diet",
                                                    "Low Sodium",
                                                    "Low Vitamin K",
                                                    "Other"
                                                };

                foreach (var a in Diets)
                {
                    vm.Diet2.Add(a);
                }
            }
        }

        public static void InitPreferredDays(ref CarePlanPersonalHygieneModel m)
        {
            if (m.PreferredDaysCollection != null && m.PreferredDaysCollection.Count == 0)
            {
                string[] l_Days = new string[]{"N/A",
                                                    "Every Day",
                                                    "Monday",
                                                    "Tuesday",
                                                    "Wednesday",
                                                    "Thursday",
                                                    "Friday",
                                                    "Saturday",
                                                    "Sunday"
                                                };

                foreach (var d in l_Days)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.PreferredDaysCollection.Add(l_CBM);
                }
            }
        }

        public static void InitAssistanceWithTeeth(ref CarePlanAssistanceWithModel m)
        {
            if (m.TeethCollection != null && m.TeethCollection.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"N/A",
                                                    "Own Teeth",
                                                    "Full",
                                                    "Partial",
                                                    "Upper",
                                                    "Lower"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.TeethCollection.Add(l_CBM);
                }
            }
        }

        public static void InitBehaviour(ref CarePlanBehaviourModel m)
        {
            if (m.BehaviourCollection != null && m.BehaviourCollection.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"No Concerns",
                                                    "Agitated",
                                                    "Aggressive",
                                                    "Depressed",
                                                    "Suicidal",
                                                    "Anxious Behaviour",
                                                    "Withdrawn",
                                                    "Demanding",
                                                    "Disruptive",
                                                    "Hoarding",
                                                    "Sad",
                                                    "Defective Coping",
                                                    "Resists/Refuses care and/or treatments",
                                                    "Suspicious",
                                                    "Ingests Foriegn Items",
                                                    "Inappropriate Sexual Behaviour",
                                                    "Inappropriate / Unsafe smoker",
                                                    "Substance Abuse",
                                                    "Alcohol Abuse",
                                                    "Seeks Attention"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.BehaviourCollection.Add(l_CBM);
                }
            }
        }

        public static void InitCognitiveFunction(ref CarePlanCognitiveFunctionModel m)
        {
            if (m.CognitiveFunction != null && m.CognitiveFunction.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"Unimpaired",
                                                    "Forgetful",
                                                    "Poor Judgement",
                                                    "Confused",
                                                    "Short term loss",
                                                    "Significant Impairment"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.CognitiveFunction.Add(l_CBM);
                }
            }
        }

        public static void InitNutrition(ref CarePlanNutritionModel m)
        {
            if (m.Diet != null && m.Diet.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"Regular Diet",
                                                    "Vegetarian Diet",
                                                    "Low Sodium",
                                                    "Diabetic",
                                                    "Low Fat",
                                                    "Low Cholesterol",
                                                    "Low Potassium",
                                                    "Gluten Free",
                                                    "Low Vitamin K",
                                                    "Other"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Diet.Add(l_CBM);
                }
            }
        }

        public static void InitElimination(ref CarePlanEliminationModel m)
        {

            if (m.Bladder != null && m.Bladder.Count == 0)
            {

                string[] l_Checkboxes = new string[]{"Continent",
                                                    "Incontinence",
                                                    "Occasionally Incontinent",
                                                    "Catheter"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Bladder.Add(l_CBM);
                }
            }

            if (m.Bowel != null && m.Bowel.Count == 0)
            {

                string[] l_Checkboxes = new string[]{"Continent",
                                                    "Incontinence",
                                                    "Occasionally Incontinent",
                                                    "Ostomy"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Bowel.Add(l_CBM);
                }
            }
        }

        public static void InitToileting(ref CarePlanToiletingModel m)
        {
            string[] l_Checkboxes = new string[]{"Day",
                                                    "Evening",
                                                    "Night"
                                                };

            if (m.Bathroom != null && m.Bathroom.Count == 0)
            {

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Bathroom.Add(l_CBM);
                }
            }

            if (m.Commode != null && m.Commode.Count == 0)
            {

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Commode.Add(l_CBM);
                }
            }

            if (m.Bedpan != null && m.Bedpan.Count == 0)
            {

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Bedpan.Add(l_CBM);
                }
            }
        }

         public static void InitSensoryAbilities(ref CarePlanSensoryAbilitiesModel m)
        {
            if (m.Vision != null && m.Vision.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"Unimpaired",
                                                    "Impaired",
                                                    "Impaired Left",
                                                    "Impaired Right",
                                                    "Blind",
                                                    "Blind Left",
                                                    "Blind Right",
                                                    "Glasses",
                                                    "Contact Lens"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Vision.Add(l_CBM);
                }
            }

            if (m.Hearing != null && m.Hearing.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"Unimpaired",
                                                    "Impaired",
                                                    "Impaired Left",
                                                    "Impaired Right",
                                                    "Deaf",
                                                    "Deaf Left",
                                                    "Deaf Right",
                                                    "Hearing Aid",
                                                    "Hearing Aid - Left",
                                                    "Hearing Aid - Right"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Hearing.Add(l_CBM);
                }
            }

            if (m.Communication != null && m.Communication.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"Unimpaired",
                                                    "Impaired ability to communicate and understand",
                                                    "Difficulty communicating but understands",
                                                    "Difficulty communicating but can understand",
                                                    "Language barrier",
                                                    "Understands Instruction"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.Communication.Add(l_CBM);
                }
            }
        }

        public static void InitSpecialEquipment(ref CarePlanSpecialEquipmentModel m)
        {
            if (m.SpecialEquipment != null && m.SpecialEquipment.Count == 0)
            {
                string[] l_Checkboxes = new string[]{"N/A",
                                                    "Pendant",
                                                    "TED Stocking",
                                                    "Support brace",
                                                    "Prosthesis",
                                                    "Other"
                                                };

                foreach (var d in l_Checkboxes)
                {
                    var l_CBM = new QOLACheckboxModel();
                    l_CBM.IsSelected = false;
                    l_CBM.Name = d;
                    l_CBM.Notes = string.Empty;

                    m.SpecialEquipment.Add(l_CBM);
                }
            }
        }

    }
}