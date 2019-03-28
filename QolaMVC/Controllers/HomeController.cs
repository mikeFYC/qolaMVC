using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using QolaMVC.Helpers;
using static QolaMVC.Helpers.ProgressNotesHelper;
using System.Data;
using System.Collections;
using System.Text;
using System.Reflection;
using SamDoc = Xceed.Words.NET;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net;

namespace QolaMVC.Controllers
{
    public class HomeController : Controller
    {
        private string _colorCode = "W";
        private string _mobiltySelectedValue = string.Empty;
        private string _previousValue = string.Empty;

        public void exportToWord(string titleDate, string DWM, string CaName, string CaNumber)
        {
            if (DWM == "month")
            {
                CreateDocMonth(titleDate, CaName, CaNumber);
            }
            else if (DWM == "week")
            {
                CreateDocWeek(titleDate, CaName, CaNumber);
            }
            else if (DWM == "day" || DWM == "")
            {
                CretaeDocDay(titleDate, CaName, CaNumber);
            }
        }



        public void CreateDocMonth(string titleDate, string CaName, string CaNumber)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            int homeId = home.Id;
            try
            {

                DateTime CurDate = DateTime.Parse(titleDate);
                DateTime fromDate = new DateTime(CurDate.Year, CurDate.Month, 01);
                DateTime toDate = new DateTime(CurDate.Year, CurDate.Month, DateTime.DaysInMonth(CurDate.Year, CurDate.Month));
                //int intCategoryId = Convert.ToInt32("ddl_m_Category.SelectedValue");
                //int intActivityId = Convert.ToInt32("ddl_m_Activity.SelectedValue");
                //int iCalendarType = Convert.ToInt32("hdnCalendarType.Value");
                DateTime tmpDate;
                int[,] MonthCalendar = new int[6, 7];
                int Week = 0;
                int maxCellheight;
                int[] cellHeight = new int[3] { 119, 143, 179 };
                string days = string.Empty;
                int date = 0;
                for (date = 1; date <= DateTime.DaysInMonth(CurDate.Year, CurDate.Month); date++)
                {
                    tmpDate = new DateTime(CurDate.Year, CurDate.Month, date);
                    if (days == "Saturday")
                        Week += 1;

                    days = tmpDate.DayOfWeek.ToString();

                    if (days == "Sunday")
                        MonthCalendar[Week, 0] = date;
                    else if (days == "Monday")
                        MonthCalendar[Week, 1] = date;
                    else if (days == "Tuesday")
                        MonthCalendar[Week, 2] = date;
                    else if (days == "Wednesday")
                        MonthCalendar[Week, 3] = date;
                    else if (days == "Thursday")
                        MonthCalendar[Week, 4] = date;
                    else if (days == "Friday")
                        MonthCalendar[Week, 5] = date;
                    else if (days == "Saturday")
                        MonthCalendar[Week, 6] = date;
                }

                if (MonthCalendar[5, 0] > 0)
                {
                    maxCellheight = cellHeight[0];
                }
                else if (MonthCalendar[4, 0] > 0)
                {
                    maxCellheight = cellHeight[1];
                }
                else if (MonthCalendar[3, 0] > 0)
                {
                    maxCellheight = cellHeight[2];
                }
                else
                {
                    maxCellheight = cellHeight[0];
                }

                DataTable dtActivityCalendar = HomeDAL.Get_Activity_Calendar1234_ExporttoWord(homeId, fromDate, toDate, CaNumber);
                //DataTable dtActivityCalendar = new DataTable();
                DataRow[] drEachDate;
                string strActivityEventDate = string.Empty, strDynamTime = string.Empty, strVenue = string.Empty, clsName = string.Empty;
                DateTime dtActivityEventDate;
                int intDynamicColsPan = 0, intDynamicColsPanTop = 0;

                using (SamDoc.DocX document = SamDoc.DocX.Load(Server.MapPath("/Content/CalendarTheme/Html/ActivityCalendarMonth_mike.docx")))
                {

                    //Table
                    var table = document.Tables.FirstOrDefault();
                    if (table != null)
                    {
                        table.Rows[0].Cells[0].ReplaceText("[%Day1%]", "Sunday");
                        table.Rows[0].Cells[1].ReplaceText("[%Day2%]", "Monday");
                        table.Rows[0].Cells[2].ReplaceText("[%Day3%]", "Tuesday");
                        table.Rows[0].Cells[3].ReplaceText("[%Day4%]", "Wednesday");
                        table.Rows[0].Cells[4].ReplaceText("[%Day5%]", "Thursday");
                        table.Rows[0].Cells[5].ReplaceText("[%Day6%]", "Friday");
                        table.Rows[0].Cells[6].ReplaceText("[%Day7%]", "Saturday");

                        if (table.RowCount > 1)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    table.Rows[(i + 1)].Cells[j].Width = 90;
                                    if (MonthCalendar[i, j] > 0)
                                    {
                                        table.Rows[(i + 1)].MinHeight = maxCellheight;
                                        table.Rows[(i + 1)].Cells[j].Paragraphs[0].Append(MonthCalendar[i, j].ToString()).FontSize(9).Bold().Alignment = SamDoc.Alignment.right;
                                        table.Rows[(i + 1)].Cells[j].Paragraphs[0].LineSpacingAfter = 0.5f;
                                        //table.Rows[(i + 1)].Cells[j].Paragraphs.LastOrDefault().Alignment = SamDoc.Alignment.left;

                                        strActivityEventDate = ProperDateFormat(MonthCalendar[i, j], CurDate.Month, CurDate.Year);
                                        dtActivityEventDate = stringToDateFormat(strActivityEventDate);
                                        strActivityEventDate = dateToUSDateStringFormat(dtActivityEventDate);
                                        if (dtActivityCalendar != null && dtActivityCalendar.Rows.Count > 0)
                                        {
                                            drEachDate = dtActivityCalendar.Select("StartDate ='" + strActivityEventDate + "' ");
                                            if (drEachDate.Length > 0)
                                            {
                                                for (int index = 0; index <= drEachDate.Length - 1; index++)
                                                {
                                                    string temSignUPVal = drEachDate[index]["EventTitle"].ToString();
                                                    clsName = temSignUPVal;

                                                    strDynamTime = DateTime.Parse(drEachDate[index]["StartTime"].ToString()).ToShortTimeString();


                                                    if (drEachDate[index]["Venue"].ToString() == "")
                                                    {
                                                        strVenue = "";
                                                    }
                                                    else
                                                    {
                                                        strVenue = "- " + drEachDate[index]["Venue"];
                                                    }
                                                    string phrActAndNotes = strDynamTime + "- " + (CultureInfo.CurrentCulture.Name == "fr-BE" ? drEachDate[index]["EventTitle"].ToString() : drEachDate[index]["EventTitle"].ToString()) + " " + drEachDate[index]["note"].ToString() + strVenue;
                                                    table.Rows[(i + 1)].Cells[j].InsertParagraph(phrActAndNotes).FontSize(9);
                                                    table.Rows[(i + 1)].Cells[j].Paragraphs.LastOrDefault().LineSpacingAfter = 0.5f;
                                                    table.Rows[(i + 1)].Cells[j].Paragraphs.LastOrDefault().Alignment = SamDoc.Alignment.left;
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                            if (MonthCalendar[5, 0] == 0)
                                table.Rows[6].Remove();

                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    if (MonthCalendar[i, j] > 0)
                                    {
                                        if (intDynamicColsPan != 0)
                                        {
                                            string sTopImg = string.Empty;

                                            if (CultureInfo.CurrentCulture.Name == "fr-BE")
                                            {

                                                sTopImg = Server.MapPath("/Content/CalendarTheme/Images/MonthlyActivity/" + titleDate.Substring(0, 3).ToLower() + CurDate.Year.ToString() + "Theme1_" + intDynamicColsPan.ToString() + ".JPG");
                                            }
                                            else
                                            {
                                                sTopImg = Server.MapPath("/Content/CalendarTheme/Images/MonthlyActivity/" + titleDate.Substring(0, 3).ToLower() + CurDate.Year.ToString() + "Theme1_" + intDynamicColsPan.ToString() + ".JPG");
                                            }
                                            if (System.IO.File.Exists(sTopImg))
                                            {
                                                table.Rows[(i + 1)].Cells[0].RemoveParagraphAt(0);
                                                if (intDynamicColsPan > 1)
                                                    table.Rows[(i + 1)].MergeCells(0, (intDynamicColsPan - 1));
                                                int width = (Convert.ToInt32(table.Rows[(i + 1)].Cells[0].Width) + 54) * intDynamicColsPan;

                                                var image = document.AddImage(sTopImg);
                                                var picture = image.CreatePicture(maxCellheight, width);
                                                table.Rows[(i + 1)].Cells[0].MarginLeft = 0;
                                                if (table.Rows[(i + 1)].Cells[0].Paragraphs.Count == 0)
                                                    table.Rows[(i + 1)].Cells[0].InsertParagraph("");
                                                table.Rows[(i + 1)].Cells[0].Paragraphs[0].AppendPicture(picture);
                                                intDynamicColsPanTop = intDynamicColsPan;
                                            }
                                        }
                                        intDynamicColsPan = 0;
                                    }
                                    else
                                    {
                                        intDynamicColsPan++;
                                    }
                                }

                                if (intDynamicColsPan != 0 && intDynamicColsPan < 7)
                                {
                                    string sBtmImg = string.Empty;
                                    sBtmImg = "";

                                    if (CultureInfo.CurrentCulture.Name == "fr-BE")
                                    {
                                        sBtmImg = Server.MapPath("/Content/CalendarTheme/Images/MonthlyActivity/" + titleDate.Substring(0, 3).ToLower() + CurDate.Year.ToString() + "Theme1_" + intDynamicColsPan.ToString() + ".JPG");
                                    }
                                    else
                                    {
                                        sBtmImg = Server.MapPath("/Content/CalendarTheme/Images/MonthlyActivity/" + titleDate.Substring(0, 3).ToLower() + CurDate.Year.ToString() + "Theme1_" + intDynamicColsPan.ToString() + ".JPG");
                                    }

                                    if (System.IO.File.Exists(sBtmImg))
                                    {
                                        int CellStartPoint = table.Rows[table.RowCount - 1].Cells.Count - intDynamicColsPan;
                                        //table.Rows[table.RowCount - 1].Cells[CellStartPoint].RemoveParagraphAt(0);

                                        if (intDynamicColsPan > 1)
                                        {
                                            table.Rows[table.RowCount - 1].MergeCells(CellStartPoint, CellStartPoint + (intDynamicColsPan - 1));
                                        }

                                        int width = (Convert.ToInt32(table.Rows[table.RowCount - 1].Cells[CellStartPoint].Width) + 54) * intDynamicColsPan;

                                        var image = document.AddImage(sBtmImg);
                                        var picture = image.CreatePicture(maxCellheight, width);
                                        table.Rows[table.RowCount - 1].Cells[CellStartPoint].MarginLeft = 0;

                                        table.Rows[table.RowCount - 1].Cells[CellStartPoint].Paragraphs[0].AppendPicture(picture);
                                        intDynamicColsPanTop = intDynamicColsPan;
                                    }
                                }
                            }

                            //HomeModel newhome = new HomeModel();
                            //home = HomeDAL.GetHomeById(homeId);

                            SamDoc.Footer footer = document.Footers.Odd;
                            footer.ReplaceText("[#CalendarName#]", CaName);
                            footer.ReplaceText("[#HomeDescription#]",
                                home.Name + (home.Phone != "" ? "  Ph. " + String.Format("{0:(###) ###-####}", Convert.ToInt64(home.Phone)) : "  Ph. " + "")
                                );

                            string reportName = RemoveSpecialCharacter(home.Name) + RemoveSpecialCharacter(CaName) + fromDate.ToString("MMM") + ".docx";

                            //document.SaveAs(Server.MapPath("/Content/CalendarTheme/Html/") + reportName);

                            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                            document.SaveAs(mStream);
                            Response.Clear();
                            Response.AddHeader("content-disposition", "attachment; filename=" + reportName);
                            Response.ContentType = "application/msword";
                            mStream.WriteTo(Response.OutputStream);
                            Response.End();


                            //FileStream file = new FileStream(Server.MapPath("/Content/CalendarTheme/Html/") + reportName, FileMode.Open, FileAccess.Read);
                            //file.CopyTo(mStream);
                            //Response.ContentType = "application/octet-stream";
                            //Response.AddHeader("Content-Disposition", "attachment; filename="+ reportName);
                            //Response.Clear();
                            //Response.BinaryWrite(mStream.ToArray());

                            //if (System.IO.File.Exists(Server.MapPath("/Content/CalendarTheme/Html/") + reportName))
                            //{
                            //    System.IO.File.Delete(Server.MapPath("/Content/CalendarTheme/Html/") + reportName);
                            //}
                            //System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                string exception = "ActivityCalendar CreateDocMonth |" + Ex.Message.ToString();
                //Log.Write(exception);
                Response.Redirect("ErrorPage.aspx", false);
            }
        }
        private void CreateDocWeek(string titleDate, string CaName, string CaNumber)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            string exception = string.Empty;
            try
            {
                int homeId = home.Id;

                int dashindex = titleDate.IndexOf("–");

                DateTime fromDate = DateTime.Parse(titleDate.Substring(0, dashindex - 1) + ", " + titleDate.Substring(titleDate.Length - 4, 4));
                DateTime toDate = fromDate.AddDays(7).AddSeconds(-1);
                string s_MonthName = "Weekly Calendar " + fromDate.ToString("MMMM dd, yyyy") + " - " + toDate.ToString("MMMM dd, yyyy");


                DateTime fDt = fromDate;
                DateTime tDt = toDate;

                DataTable dtActivityCalendar = HomeDAL.Get_Activity_Calendar1234_ExporttoWord(homeId, fromDate, toDate, CaNumber);

                using (SamDoc.DocX document = SamDoc.DocX.Load(Server.MapPath("/Content/CalendarTheme/Html/ActivityCalendarWeek_mike.docx")))
                {

                    var table = document.Tables.FirstOrDefault();
                    if (table != null)
                    {
                        if (table.RowCount > 1)
                        {
                            for (int i = 1; i <= 7; i++)
                            {
                                table.Rows[1].Cells[(i - 1)].ReplaceText("[%Day" + i + "%]", fromDate.AddDays(i - 1).ToString("dddd") + "  " + fromDate.AddDays(i - 1).ToString("MMM dd, yyyy"));
                            }
                            if (dtActivityCalendar != null && dtActivityCalendar.Rows.Count > 0)
                            {
                                string strEventDate = string.Empty;
                                DataRow[] drEachDate;
                                string clsName = string.Empty;
                                string strVenue = string.Empty;
                                string strDynamTime = string.Empty;

                                for (int j = 1; j <= 7; j++)
                                {
                                    strEventDate = dateToUSDateStringFormat(fromDate);
                                    drEachDate = dtActivityCalendar.Select("StartDate ='" + strEventDate + "' ");
                                    //table.Rows[2].Cells[(j - 1)].RemoveParagraphAt(0);
                                    if (drEachDate.Length > 0)
                                    {
                                        for (int index1 = 0; index1 <= drEachDate.Length - 1; index1++)
                                        {
                                            string ActivityDetails = string.Empty;
                                            strDynamTime = DateTime.Parse(drEachDate[index1]["StartTime"].ToString()).ToShortTimeString();
                                            ActivityDetails = drEachDate[index1]["EventTitle"].ToString();
                                            ActivityDetails = strDynamTime + " " + ActivityDetails.Trim() + " " + drEachDate[index1]["note"].ToString() + strVenue;
                                            table.Rows[2].Cells[(j - 1)].InsertParagraph(ActivityDetails).FontSize(9);
                                            table.Rows[2].Cells[(j - 1)].MarginLeft = 0;
                                        }
                                    }
                                    fromDate = fromDate.AddDays(1);
                                }
                            }

                            SamDoc.Header header = document.Headers.Odd;
                            header.ReplaceText("[#CalendarName#]", CaName);
                            header.ReplaceText("[#DateTime#]", s_MonthName);

                            SamDoc.Footer footer = document.Footers.Odd;
                            footer.ReplaceText("[#HomeDescription#]",
                                home.Name + (home.Phone != "" ? "  Ph. " + String.Format("{0:(###) ###-####}", Convert.ToInt64(home.Phone)) : "  Ph. " + "")
                                );

                            string reportName = RemoveSpecialCharacter(home.Name) + RemoveSpecialCharacter(CaName) + fDt.ToString("MMM-dd") + tDt.ToString("-dd") + ".docx";

                            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                            document.SaveAs(mStream);
                            Response.Clear();
                            Response.AddHeader("content-disposition", "attachment; filename=" + reportName);
                            Response.ContentType = "application/msword";
                            mStream.WriteTo(Response.OutputStream);
                            Response.End();
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                exception = "ActivityCalendar CreateDocWeek |" + Ex.Message.ToString();
                //Log.Write(exception);
                Response.Redirect("ErrorPage.aspx", false);
            }
        }
        private void CreateDocWeek_backup(string titleDate, string CaName, string CaNumber)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            string exception = string.Empty;
            try
            {
                int homeId = home.Id;

                int dashindex = titleDate.IndexOf("–");

                //DateTime CurDate = stringToDateFormat(titleDate);

                DateTime fromDate = DateTime.Parse(titleDate.Substring(0, dashindex - 1) + ", " + titleDate.Substring(titleDate.Length - 4, 4));
                //fromDate = CurDate.AddDays(-(int)CurDate.DayOfWeek);
                DateTime toDate = fromDate.AddDays(7).AddSeconds(-1);
                //int intCategoryId = Convert.ToInt32(ddl_m_Category.SelectedValue);
                //int intActivityId = Convert.ToInt32(ddl_m_Activity.SelectedValue);
                //int iCalendarType = Convert.ToInt32(hdnCalendarType.Value);
                string s_MonthName = "Weekly Calendar " + fromDate.ToString("MMMM dd, yyyy") + " - " + toDate.ToString("MMMM dd, yyyy");


                DateTime fDt = fromDate;
                DateTime tDt = toDate;

                DataTable dtActivityCalendar = HomeDAL.Get_Activity_Calendar1234_ExporttoWord(homeId, fromDate, toDate, CaNumber);
                //DataTable dtActivityCalendar = new DataTable();

                using (SamDoc.DocX document = SamDoc.DocX.Load(Server.MapPath("/Content/CalendarTheme/Html/ActivityCalendarWeek.docx")))
                {
                    //Table
                    //string sTopImg = string.Empty;

                    //if (CultureInfo.CurrentCulture.Name == "fr-BE")
                    //{
                    //    sTopImg = Server.MapPath("/Content/CalendarTheme/Images/WeeklyActivity/apr2018WTheme1.jpg");
                    //}
                    //else
                    //{
                    //    sTopImg = Server.MapPath("/Content/CalendarTheme/Images/WeeklyActivity/apr2018WTheme1.jpg");
                    //}

                    var table = document.Tables.FirstOrDefault();
                    if (table != null)
                    {
                        if (table.RowCount > 1)
                        {
                            //if (System.IO.File.Exists(sTopImg))
                            //{
                            //    var images = document.AddImage(sTopImg);
                            //    var picture = images.CreatePicture(100, 1590);
                            //    table.Rows[0].Cells[0].MarginLeft = 0;
                            //    table.Rows[0].Cells[0].Paragraphs[0].AppendPicture(picture);
                            //}

                            for (int i = 1; i <= 7; i++)
                            {
                                table.Rows[1].Cells[(i - 1)].ReplaceText("[%Day" + i + "%]", fromDate.AddDays(i - 1).ToString("dddd") + "  " + fromDate.AddDays(i - 1).ToString("MMM dd, yyyy"));
                            }
                            if (dtActivityCalendar != null && dtActivityCalendar.Rows.Count > 0)
                            {
                                string strEventDate = string.Empty;
                                DataRow[] drEachDate;
                                string clsName = string.Empty;
                                string strVenue = string.Empty;
                                //string strActivityNameAndNotes = string.Empty;
                                string strDynamTime = string.Empty;

                                for (int j = 1; j <= 7; j++)
                                {
                                    strEventDate = dateToUSDateStringFormat(fromDate);
                                    drEachDate = dtActivityCalendar.Select("StartDate ='" + strEventDate + "' ");
                                    table.Rows[2].Cells[(j - 1)].RemoveParagraphAt(0);
                                    if (drEachDate.Length > 0)
                                    {
                                        for (int index1 = 0; index1 <= drEachDate.Length - 1; index1++)
                                        {
                                            //string temSignUPVal = drEachDate[index1]["StartDate"].ToString();
                                            //string sActivityImage = string.Empty;
                                            //string root = Server.MapPath(".");
                                            //string imagename = root + "\\" + drEachDate[index1]["fd_icon_path"].ToString();
                                            //FileInfo file = new FileInfo(imagename);

                                            //if (temSignUPVal == "A")
                                            //{
                                            //    clsName = "activitySignUP";
                                            //}
                                            //else
                                            //{
                                            //    clsName = "";
                                            //}
                                            if (drEachDate[index1]["Venue"].ToString() == "")
                                            {
                                                strVenue = "";
                                            }
                                            else
                                            {
                                                strVenue = " - " + drEachDate[index1]["Venue"];
                                            }

                                            string ActivityDetails = string.Empty;
                                            //if (drEachDate[index1]["ActivityId"].ToString() == "1" && drEachDate[index1]["fd_activity_event_time"].ToString() == "12:00AM")
                                            //{
                                            //    strDynamTime = "ALL DAY";
                                            //}
                                            //else
                                            //{
                                            strDynamTime = (CultureInfo.CurrentCulture.Name == "fr-BE" ? DateTime.Parse(drEachDate[index1]["StartTime"].ToString()).ToShortTimeString() : DateTime.Parse(drEachDate[index1]["StartTime"].ToString()).ToShortTimeString());
                                            //}

                                            ActivityDetails = (CultureInfo.CurrentCulture.Name == "fr-BE" ? drEachDate[index1]["EventTitle"].ToString() : drEachDate[index1]["EventTitle"].ToString());
                                            if (clsName != "")
                                            {
                                                ActivityDetails = (CultureInfo.CurrentCulture.Name == "fr-BE" ? drEachDate[index1]["EventTitle"].ToString() : drEachDate[index1]["EventTitle"].ToString());
                                            }
                                            ActivityDetails = strDynamTime + " " + ActivityDetails.Trim() + " " + drEachDate[index1]["note"].ToString() + strVenue;
                                            table.Rows[2].Cells[(j - 1)].InsertParagraph(ActivityDetails).FontSize(9);
                                            table.Rows[2].Cells[(j - 1)].MarginLeft = 0;
                                            //if (file.Exists)
                                            //{
                                            //    var image = document.AddImage(imagename);
                                            //    var picture = image.CreatePicture(30, 30);
                                            //    table.Rows[2].Cells[(j - 1)].InsertParagraph("").AppendPicture(picture).Alignment = SamDoc.Alignment.center;
                                            //}
                                        }
                                    }
                                    fromDate = fromDate.AddDays(1);
                                }
                            }


                            //Header and Footer
                            SamDoc.Header header = document.Headers.Odd;
                            header.ReplaceText("[#CalendarName#]", CaName);
                            header.ReplaceText("[#DateTime#]", s_MonthName);

                            SamDoc.Footer footer = document.Footers.Odd;
                            footer.ReplaceText("[#HomeDescription#]",
                                home.Name + (home.Phone != "" ? "  Ph. " + String.Format("{0:(###) ###-####}", Convert.ToInt64(home.Phone)) : "  Ph. " + "")
                                );

                            string reportName = RemoveSpecialCharacter(home.Name) + RemoveSpecialCharacter(CaName) + fDt.ToString("MMM-dd") + tDt.ToString("-dd") + ".docx";

                            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                            document.SaveAs(mStream);
                            Response.Clear();
                            Response.AddHeader("content-disposition", "attachment; filename=" + reportName);
                            Response.ContentType = "application/msword";
                            mStream.WriteTo(Response.OutputStream);
                            Response.End();

                            //document.SaveAs(Server.MapPath("/Content/CalendarTheme/Html/") + reportName);
                            //Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + reportName);
                            //Response.WriteFile(Server.MapPath("/Content/CalendarTheme/Html/") + reportName);
                            //Response.Flush();
                            //if (System.IO.File.Exists(Server.MapPath("/Content/CalendarTheme/Html/") + reportName))
                            //{
                            //    System.IO.File.Delete(Server.MapPath("/Content/CalendarTheme/Html/") + reportName);
                            //}
                            //System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                exception = "ActivityCalendar CreateDocWeek |" + Ex.Message.ToString();
                //Log.Write(exception);
                Response.Redirect("ErrorPage.aspx", false);
            }
        }
        private void CretaeDocDay(string titleDate, string CaName, string CaNumber)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            int homeId = home.Id;
            try
            {

                DateTime CurDate = DateTime.Parse(titleDate);
                DateTime fromDate = CurDate;
                DateTime toDate = CurDate;

                //int intCategoryId = Convert.ToInt32(ddl_m_Category.SelectedValue);
                //int intActivityId = Convert.ToInt32(ddl_m_Activity.SelectedValue);
                //int iCalendarType = Convert.ToInt32(hdnCalendarType.Value);

                using (SamDoc.DocX document = SamDoc.DocX.Load(
                    Server.MapPath("/Content/CalendarTheme/Html/DayCalendar.docx"))
                    )
                {
                    //Table
                    var table = document.Tables.FirstOrDefault();
                    if (table != null)
                    {
                        if (table.RowCount > 1)
                        {
                            var rowPattern = table.Rows[1];
                            DataTable dtDayCalendar = HomeDAL.Get_Activity_Calendar1234_ExporttoWord(homeId, fromDate, toDate, CaNumber);
                            //DataTable dtDayCalendar = new DataTable();


                            table.Rows[0].Cells[0].ReplaceText("[%th1%]", "Time");
                            table.Rows[0].Cells[1].ReplaceText("[%th2%]", "TodayActivity");
                            table.Rows[0].Cells[2].ReplaceText("[%th3%]", "Venue");
                            if (dtDayCalendar != null)
                            {
                                for (int i = 0; i < dtDayCalendar.Rows.Count; i++)
                                {
                                    var NewRow = table.InsertRow(rowPattern, table.RowCount - 1);
                                    //NewRow.ReplaceText("[#Time#]", dtDayCalendar.Rows[i]["fd_activity_event_time"].ToString());
                                    NewRow.ReplaceText("[#Time#]", CultureInfo.CurrentCulture.Name == "fr-BE" ? DateTime.Parse(dtDayCalendar.Rows[i]["StartTime"].ToString()).ToShortTimeString() : DateTime.Parse(dtDayCalendar.Rows[i]["StartTime"].ToString()).ToShortTimeString());
                                    NewRow.ReplaceText("[#Activity#]", CultureInfo.CurrentCulture.Name == "fr-BE" ? dtDayCalendar.Rows[i]["EventTitle"].ToString() : dtDayCalendar.Rows[i]["EventTitle"].ToString());
                                    NewRow.ReplaceText("[#Venue#]", dtDayCalendar.Rows[i]["Venue"].ToString());
                                }
                            }
                            rowPattern.Remove();
                        }
                    }

                    //Header and Footer
                    SamDoc.Header header = document.Headers.Odd;
                    header.ReplaceText("[#CalendarName#]", CaName);
                    header.ReplaceText("[#DateTime#]", CurDate.ToString("MMMM dd, yyyy"));


                    SamDoc.Footer footer = document.Footers.Odd;
                    footer.ReplaceText("[#HomeDescription#]",
                        home.Name + (home.Phone != "" ? "  Ph. " + String.Format("{0:(###) ###-####}", Convert.ToInt64(home.Phone)) : "  Ph. " + "")
                        );

                    string reportName = RemoveSpecialCharacter(home.Name) + RemoveSpecialCharacter(CaName) + fromDate.ToString("MMM-dd") + ".docx";

                    System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                    document.SaveAs(mStream);
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment; filename=" + reportName);
                    Response.ContentType = "application/msword";
                    mStream.WriteTo(Response.OutputStream);
                    Response.End();

                    //document.SaveAs(Server.MapPath("/Content/CalendarTheme/Html/") + reportName);
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + reportName);
                    //Response.WriteFile(Server.MapPath("/Content/CalendarTheme/Html/") + reportName);
                    //Response.Flush();
                    //if (System.IO.File.Exists(Server.MapPath("/CalendarTheme/Html/") + reportName))
                    //{
                    //    System.IO.File.Delete(Server.MapPath("/CalendarTheme/Html/") + reportName);
                    //}
                    //System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

            }
            catch (Exception Ex)
            {
                string exception = "ActivityCalendar CreateDocWeek |" + Ex.Message.ToString();
                //Log.Write(exception);
                Response.Redirect("ErrorPage.aspx", false);
            }
        }
        public string ProperDateFormat(int d, int m, int y)
        {
            string strDay = string.Empty, strMonth = string.Empty;
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            if (d < 10)
                strDay = "0" + d;
            else
                strDay = d.ToString();

            if (m < 10)
                strMonth = "0" + m;
            else
                strMonth = m.ToString();

            return strMonth + "/" + strDay + "/" + y.ToString();

        }
        public DateTime stringToDateFormat(string sDate)
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
                Response.Redirect("ErrorPage.aspx", true);
            }
            return convertedDate;
        }
        public DateTime stringToDateFormat(string sDate, string sDateFormat)
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
                Response.Redirect("ErrorPage.aspx", true);
            }
            return convertedDate;
        }
        public static string[] dateFormats = { "MM/dd/yyyy hh:mm:ss", "MM/dd/yyyy hh:mm", "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy HH:mm:ss tt", "MM/dd/yyyy HH:mm:ss", "MM/dd/yyyy", "MM-dd-yyyy", "MM/dd/yyyy H:mm:ss tt", "MM/dd/yyyy h:mm:ss tt" };
        public static string[] timeFormats = { "hh:mm:ss", "hh:mm tt", "hh tt", "hh:mm", "HH:mm:ss tt", "HH:mm:ss", "HH:mm" };
        public string dateToUSDateStringFormat(DateTime dtDays)
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
                Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }
        public string dateToUSDateStringFormat(DateTime dtDays, string sDateFormat)
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
                Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }
        public string dateToUSTimeStringFormat(DateTime dtDate, string stimeFormat)
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
                Response.Redirect("ErrorPage.aspx", true);
            }
            return result;
        }
        private string RemoveSpecialCharacter(string residentName)
        {
            Regex rgx = new Regex("[^a-zA-Z]");
            residentName = rgx.Replace(residentName, "");
            return residentName;
        }

        public void CreateSampleDocument()
        {
            // Modify to suit your machine:
            string fileName = @"C:\Users\Mike.Feng\Desktop\DocXExample.docx";

            // Create a document in memory:
            var doc = SamDoc.DocX.Create(fileName);

            // Insert a paragrpah:
            doc.InsertParagraph("This is my first paragraph");

            // Save to the output directory:
            doc.Save();

            // Open in Word:
            Process.Start("WINWORD.EXE", fileName);
        }
        public void downloadSampleDocument_test()
        {
            string remoteUri = Server.MapPath("/Content/CalendarTheme/Html/");
            string fileName = "AuburnHeightsRetirementResidenceIndependentCalendarDec.docx", myStringWebResource = null;
            WebClient myWebClient = new WebClient();
            myStringWebResource = remoteUri + fileName;
            myWebClient.DownloadFile(myStringWebResource, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + fileName);
        }
        public void downloadSampleDocument_test2()
        {
            try
            {
                string reportName = "AuburnHeightsRetirementResidenceIndependentCalendarDec.docx";
                string savePath = Server.MapPath("/Content/CalendarTheme/Html/AAA.docx")//保存路劲
                , downFileUrl = Server.MapPath("/Content/CalendarTheme/Html/" + reportName);//下载文件链接地址
                WebClient wcClient = new WebClient();
                WebRequest webReq = WebRequest.Create(downFileUrl);
                WebResponse webRes = webReq.GetResponse();
                long fileLength = webRes.ContentLength;
                Stream srm = webRes.GetResponseStream();
                StreamReader srmReader = new StreamReader(srm);
                byte[] bufferbyte = new byte[fileLength];
                int allByte = (int)bufferbyte.Length;
                int startByte = 0;
                while (fileLength > 0)
                {
                    //Application.DoEvents();
                    int downByte = srm.Read(bufferbyte, startByte, allByte);
                    if (downByte == 0) { break; };
                    startByte += downByte;
                    allByte -= downByte;
                }
                if (!System.IO.File.Exists(savePath))
                {
                    string[] dirArray = savePath.Split('\\');
                    string temp = string.Empty;
                    for (int i = 0; i < dirArray.Length - 1; i++)
                    {
                        temp += dirArray[i].Trim() + "\\";
                        if (!Directory.Exists(temp))
                            Directory.CreateDirectory(temp);
                    }
                }
                FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(bufferbyte, 0, bufferbyte.Length);
                srm.Close();
                srmReader.Close();
                fs.Close();
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }


        public ActionResult Index()
        {
            //var context = Request.GetOwinContext();
            //var authManager = context.Authentication;

            //var identity = (ClaimsIdentity)authManager.User.Identity;
            //IEnumerable<Claim> claims = identity.Claims;

            //var l_Name = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
            //       .Select(c => c.Value).SingleOrDefault();
            //var l_UserId = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            //                   .Select(c => c.Value).SingleOrDefault();

            var user = (UserModel)TempData["User"];

            if (user != null)
            {
                Collection<HomeModel> l_Homes = HomeDAL.GetHomeFill(user.ID, user.UserType);// Convert.ToInt32(l_UserId));
                UserModel l_User = UserDAL.GetUserById(user.ID);// Convert.ToInt32(l_UserId));
                TempData["User"] = l_User;

                ViewBag.User = l_User;
                TempData.Keep("User");
                
                Collection<HomeModel> Home_temp = new Collection<HomeModel>();
                foreach ( var Home in l_Homes)
                {
                    if(Home.Province.ID==1 || Home.Id==27)//Chapel Hill
                    {
                        Home_temp.Add(Home);
                    }
                }
                return View(Home_temp);
                //return View(l_Homes.Where(m => m.ProvinceName == "Alberta"));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public void ERROR()
        {
            var user = (UserModel)TempData["User"];
            Session["USER"] = user;         
        }

        [HttpGet]
        public ActionResult Menu(int p_HomeId)
        {
            Session["check"] = "live";
            TempData["Home"] = HomeDAL.GetHomeById(p_HomeId);
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            HomeModel l_Home = HomeDAL.GetHomeById(p_HomeId);
            TempData["occupy"] = HomeDAL.GetOccupybyID(p_HomeId);

            TempData["ToDoListinNav"] = "true";

            return View(l_Home);
        }

        [HttpGet]
        public ActionResult Number()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            dynamic l_Json = to_do_list_function.get_to_do_list_number(user.ID, home.Id, user.UserType);

            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Number_FAST()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            dynamic l_Json = to_do_list_function.get_to_do_list_number_FAST(user.ID, home.Id, user.UserType);

            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Number_nextmonth()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            dynamic l_Json = to_do_list_function.get_to_do_list_number_nextmonth(user.ID, home.Id, user.UserType);

            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public void Progress_Note_Reminder_Dismiss_Click(int PN_ID)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            to_do_list_function.Progress_Note_Reminder_Dismiss_Click(PN_ID);


        }

        [HttpGet]
        public ActionResult Number_nextmonth_FAST()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            dynamic l_Json = to_do_list_function.get_to_do_list_number_nextmonth_FAST(home.Id, user.UserType);

            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        public PlanOfCareModel GET_one_CarePlan()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            //var careplan = CarePlanDAL.GetResidentsPlanOfCare(resident.ID);
            var careplan = CarePlanDAL.GetResidentsPlanOfCare_SpeedUp(resident.ID);
            PlanOfCareModel l_Model = new PlanOfCareModel();
            if (careplan.Count == 0)
            {
                

                var l_PersonalHygiene = new CarePlanPersonalHygieneModel();
                l_PersonalHygiene.PreferredDaysCollection = new Collection<QOLACheckboxModel>();
                QolaCulture.InitPreferredDays(ref l_PersonalHygiene);
                l_Model.PersonalHygiene = l_PersonalHygiene;

                var l_AssistanceWith = new CarePlanAssistanceWithModel();
                l_AssistanceWith.TeethCollection = new Collection<QOLACheckboxModel>();
                QolaCulture.InitAssistanceWithTeeth(ref l_AssistanceWith);
                l_Model.AssistanceWith = l_AssistanceWith;

                var l_Behaviour = new CarePlanBehaviourModel();
                l_Behaviour.BehaviourCollection = new Collection<QOLACheckboxModel>();
                QolaCulture.InitBehaviour(ref l_Behaviour);
                l_Behaviour.HarmToSelf = "";
                l_Behaviour.Smoker = "";
                l_Behaviour.RiskOfWandering = "";
                l_Model.Behaviour = l_Behaviour;

                var l_CognitiveFunction = new CarePlanCognitiveFunctionModel();
                l_CognitiveFunction.CognitiveFunction = new Collection<QOLACheckboxModel>();
                QolaCulture.InitCognitiveFunction(ref l_CognitiveFunction);
                l_Model.CognitiveFunction = l_CognitiveFunction;

                var l_Nutrition = new CarePlanNutritionModel();
                l_Nutrition.Diet = new Collection<QOLACheckboxModel>();
                QolaCulture.InitNutrition(ref l_Nutrition);
                l_Model.Nutrition = l_Nutrition;

                var l_Elimination = new CarePlanEliminationModel();
                l_Elimination.Bladder = new Collection<QOLACheckboxModel>();
                l_Elimination.Bowel = new Collection<QOLACheckboxModel>();
                QolaCulture.InitElimination(ref l_Elimination);
                l_Model.Elimination = l_Elimination;

                var l_Toilet = new CarePlanToiletingModel();
                l_Toilet.Bathroom = new Collection<QOLACheckboxModel>();
                l_Toilet.Commode = new Collection<QOLACheckboxModel>();
                l_Toilet.Bedpan = new Collection<QOLACheckboxModel>();
                QolaCulture.InitToileting(ref l_Toilet);
                l_Model.Toileting = l_Toilet;

                var l_Sensory = new CarePlanSensoryAbilitiesModel();
                l_Sensory.Vision = new Collection<QOLACheckboxModel>();
                l_Sensory.Hearing = new Collection<QOLACheckboxModel>();
                l_Sensory.Communication = new Collection<QOLACheckboxModel>();
                QolaCulture.InitSensoryAbilities(ref l_Sensory);
                l_Model.SensoryAbilities = l_Sensory;

                var l_SpecialEquipment = new CarePlanSpecialEquipmentModel();
                l_SpecialEquipment.SpecialEquipment = new Collection<QOLACheckboxModel>();
                QolaCulture.InitSpecialEquipment(ref l_SpecialEquipment);
                l_Model.SpecialEquipment = l_SpecialEquipment;


                var l_VitalSigns = new CarePlanVitalSignsModel(); l_Model.VitalSigns = l_VitalSigns;
                var l_Mobility = new CarePlanMobilityModel(); ; l_Model.Mobility = l_Mobility;
                var l_Safety = new CarePlanSafetyModel(); ; l_Model.Safety = l_Safety;
                var l_MealEscort = new CarePlanMealEscortModel(); ; l_Model.MealEscort = l_MealEscort;
                var l_Orientation = new CarePlanOrientationModel(); ; l_Model.Orientation = l_Orientation;
                var l_Meals = new CarePlanMealsModel(); ; l_Model.Meals = l_Meals;
                var l_Medication = new CarePlanMedication(); ; l_Model.Medication = l_Medication;
                var l_WoundCare = new CarePlanWoundCareModel(); ; l_Model.WoundCare = l_WoundCare;
                var l_SkinCare = new CarePlanSkinCareModel(); ; l_Model.SkinCare = l_SkinCare;
                var l_SpecialNeeds = new CarePlanSpecialNeedsModel(); ; l_Model.SpecialNeeds = l_SpecialNeeds;
                var l_FamilySupportModel = new CarePlanFamilySupportModel(); ; l_Model.FamilySupportModel = l_FamilySupportModel;
                var l_Immunization = new CarePlanImmunizationModel(); ; l_Model.Immunization = l_Immunization;
                var l_InfectiousDiseases = new CarePlanInfectiousDiseasesModel(); ; l_Model.InfectiousDiseases = l_InfectiousDiseases;



                careplan.Add(l_Model);
            }
            l_Model = careplan[0];

            return l_Model;
        }

        public ActivityAssessmentCollectionViewModel GET_one_Activity()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ActivityAssessmentCollectionViewModel l_Model = new ActivityAssessmentCollectionViewModel();

            var l_Activity = MasterDAL.GetActivityAssessments(resident.ID);
            if (l_Activity.Count == 0)
            {
                foreach (PropertyInfo prop in typeof(ActivityAssessmentCollectionViewModel).GetProperties())
                {
                    if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                    {
                        if (prop.GetValue(l_Model) == null) { prop.SetValue(l_Model, ""); }
                    }
                }
            }
            else
            {
                l_Model = l_Activity[0];
            }
            return l_Model;
        }

        public void PhoneArrangement(ResidentModel resident)
        {
            if (resident.HomePhoneType1 == 1) { resident.First_phone1 = resident.HomePhone1; resident.First_phone_type1 = 1; }
            if (resident.HomePhoneType1 == 2) { resident.Second_phone1 = resident.HomePhone1; resident.Second_phone_type1 = 1; }
            if (resident.HomePhoneType1 == 3) { resident.Third_phone1 = resident.HomePhone1; resident.Third_phone_type1 = 1; }

            if (resident.HomePhoneType2 == 1) { resident.First_phone2 = resident.HomePhone2; resident.First_phone_type2 = 1; }
            if (resident.HomePhoneType2 == 2) { resident.Second_phone2 = resident.HomePhone2; resident.Second_phone_type2 = 1; }
            if (resident.HomePhoneType2 == 3) { resident.Third_phone2 = resident.HomePhone2; resident.Third_phone_type2 = 1; }

            if (resident.HomePhoneType3 == 1) { resident.First_phone3 = resident.HomePhone3; resident.First_phone_type3 = 1; }
            if (resident.HomePhoneType3 == 2) { resident.Second_phone3 = resident.HomePhone3; resident.Second_phone_type3 = 1; }
            if (resident.HomePhoneType3 == 3) { resident.Third_phone3 = resident.HomePhone3; resident.Third_phone_type3 = 1; }

            if (resident.BusinessPhoneType1 == 1) { resident.First_phone1 = resident.BusinessPhone1; resident.First_phone_type1 = 2; }
            if (resident.BusinessPhoneType1 == 2) { resident.Second_phone1 = resident.BusinessPhone1; resident.Second_phone_type1 = 2; }
            if (resident.BusinessPhoneType1 == 3) { resident.Third_phone1 = resident.BusinessPhone1; resident.Third_phone_type1 = 2; }

            if (resident.BusinessPhoneType2 == 1) { resident.First_phone2 = resident.BusinessPhone2; resident.First_phone_type2 = 2; }
            if (resident.BusinessPhoneType2 == 2) { resident.Second_phone2 = resident.BusinessPhone2; resident.Second_phone_type2 = 2; }
            if (resident.BusinessPhoneType2 == 3) { resident.Third_phone2 = resident.BusinessPhone2; resident.Third_phone_type2 = 2; }

            if (resident.BusinessPhoneType3 == 1) { resident.First_phone3 = resident.BusinessPhone3; resident.First_phone_type3 = 2; }
            if (resident.BusinessPhoneType3 == 2) { resident.Second_phone3 = resident.BusinessPhone3; resident.Second_phone_type3 = 2; }
            if (resident.BusinessPhoneType3 == 3) { resident.Third_phone3 = resident.BusinessPhone3; resident.Third_phone_type3 = 2; }

            if (resident.CellPhoneType1 == 1) { resident.First_phone1 = resident.CellPhone1; resident.First_phone_type1 = 3; }
            if (resident.CellPhoneType1 == 2) { resident.Second_phone1 = resident.CellPhone1; resident.Second_phone_type1 = 3; }
            if (resident.CellPhoneType1 == 3) { resident.Third_phone1 = resident.CellPhone1; resident.Third_phone_type1 = 3; }

            if (resident.CellPhoneType2 == 1) { resident.First_phone2 = resident.CellPhone2; resident.First_phone_type2 = 3; }
            if (resident.CellPhoneType2 == 2) { resident.Second_phone2 = resident.CellPhone2; resident.Second_phone_type2 = 3; }
            if (resident.CellPhoneType2 == 3) { resident.Third_phone2 = resident.CellPhone2; resident.Third_phone_type2 = 3; }

            if (resident.CellPhoneType3 == 1) { resident.First_phone3 = resident.CellPhone3; resident.First_phone_type3 = 3; }
            if (resident.CellPhoneType3 == 2) { resident.Second_phone3 = resident.CellPhone3; resident.Second_phone_type3 = 3; }
            if (resident.CellPhoneType3 == 3) { resident.Third_phone3 = resident.CellPhone3; resident.Third_phone_type3 = 3; }

            if (resident.First_phone_type1 == 1) { resident.First_phone_type1_text = "Home"; }
            else if (resident.First_phone_type1 == 2) { resident.First_phone_type1_text = "Business"; }
            else if (resident.First_phone_type1 == 3) { resident.First_phone_type1_text = "Mobile"; }
            if (resident.Second_phone_type1 == 1) { resident.Second_phone_type1_text = "Home"; }
            else if (resident.Second_phone_type1 == 2) { resident.Second_phone_type1_text = "Business"; }
            else if (resident.Second_phone_type1 == 3) { resident.Second_phone_type1_text = "Mobile"; }
            if (resident.Third_phone_type1 == 1) { resident.Third_phone_type1_text = "Home"; }
            else if (resident.Third_phone_type1 == 2) { resident.Third_phone_type1_text = "Business"; }
            else if (resident.Third_phone_type1 == 3) { resident.Third_phone_type1_text = "Mobile"; }

            if (resident.First_phone_type2 == 1) { resident.First_phone_type2_text = "Home"; }
            else if (resident.First_phone_type2 == 2) { resident.First_phone_type2_text = "Business"; }
            else if (resident.First_phone_type2 == 3) { resident.First_phone_type2_text = "Mobile"; }
            if (resident.Second_phone_type2 == 1) { resident.Second_phone_type2_text = "Home"; }
            else if (resident.Second_phone_type2 == 2) { resident.Second_phone_type2_text = "Business"; }
            else if (resident.Second_phone_type2 == 3) { resident.Second_phone_type2_text = "Mobile"; }
            if (resident.Third_phone_type2 == 1) { resident.Third_phone_type2_text = "Home"; }
            else if (resident.Third_phone_type2 == 2) { resident.Third_phone_type2_text = "Business"; }
            else if (resident.Third_phone_type2 == 3) { resident.Third_phone_type2_text = "Mobile"; }

            if (resident.First_phone_type3 == 1) { resident.First_phone_type3_text = "Home"; }
            else if (resident.First_phone_type3 == 2) { resident.First_phone_type3_text = "Business"; }
            else if (resident.First_phone_type3 == 3) { resident.First_phone_type3_text = "Mobile"; }
            if (resident.Second_phone_type3 == 1) { resident.Second_phone_type3_text = "Home"; }
            else if (resident.Second_phone_type3 == 2) { resident.Second_phone_type3_text = "Business"; }
            else if (resident.Second_phone_type3 == 3) { resident.Second_phone_type3_text = "Mobile"; }
            if (resident.Third_phone_type3 == 1) { resident.Third_phone_type3_text = "Home"; }
            else if (resident.Third_phone_type3 == 2) { resident.Third_phone_type3_text = "Business"; }
            else if (resident.Third_phone_type3 == 3) { resident.Third_phone_type3_text = "Mobile"; }

        }

        public ActionResult ResidentMenu(int p_ResidentId)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = ResidentsDAL.GetResidentById(p_ResidentId);

            if (user.UserType == 5)
            {
                return RedirectToAction("ResidentMenu2", new { p_ResidentId=p_ResidentId });
            }
            else
            {
                var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");
                PhoneArrangement(resident);
                //ViewBag.Message = TempData["Message"];
                ViewBag.Message = "";
                TempData["Resident"] = resident;
                TempData.Keep("User");
                TempData.Keep("Home");
                TempData.Keep("Resident");
                ViewBag.User = user;
                ViewBag.Home = home;
                ViewBag.Resident = resident;
                ViewBag.ProgressNotes = progressNotes;
                ProgressNotesHelper.RegisterSession(resident);
                TempData["NOTE"] = "NO";
                TempData["archive"] = "NO";
                TempData["pass"] = "";
                ViewBag.TableSH = update_Suite_Handler_Table.get_innerHTML_temperary2(resident.ID);
                PlanOfCareModel l_Model = GET_one_CarePlan();
                ViewBag.careplan = l_Model;

                ActivityAssessmentCollectionViewModel l_Model2 = GET_one_Activity();
                ViewBag.activity = l_Model2;

                return View(resident);
            }


        }

        public void pointArrangement(FallRiskAssessmentModel model)
        {
            if (model.FallHistory_IsTwoOrMore == true) model.FallHistory_IsTwoOrMore_point = "2";
            if (model.FallHistory_IsOneOrTwo == true) model.FallHistory_IsOneOrTwo_point = "1";
            if (model.Neurological_IsCVA == true) model.Neurological_IsCVA_point = "1";
            if (model.Neurological_IsParkinsons == true) model.Neurological_IsParkinsons_point = "1";
            if (model.Neurological_IsAlzheimers == true) model.Neurological_IsAlzheimers_point = "1";
            if (model.Neurological_IsOther == true) model.Neurological_IsOther_point = "1";
            if (model.Neurological_IsSeizureDisorder == true) model.Neurological_IsSeizureDisorder_point = "1";
            if (model.Other_IsDiabetes == true) model.Other_IsDiabetes_point = "1";
            if (model.Other_IsOsteoporosis == true) model.Other_IsOsteoporosis_point = "1";
            if (model.Other_IsPosturalHypotension == true) model.Other_IsPosturalHypotension_point = "1";
            if (model.Other_IsSyncope == true) model.Other_IsSyncope_point = "1";
            if (model.Other_IsBowel == true) model.Other_IsBowel_point = "1";
            if (model.Incontinence_IsBowel == true) model.Incontinence_IsBowel_point = "2";
            if (model.Incontinence_IsBladder == true) model.Incontinence_IsBladder_point = "2";
            if (model.Incontinence_IsTransfer == true) model.Incontinence_IsTransfer_point = "1";
            if (model.Incontinence_IsUnsteady == true) model.Incontinence_IsUnsteady_point = "1";
            if (model.Medication_IsCardiac == true) model.Medication_IsCardiac_point = "1";
            if (model.Medication_IsDiuretics == true) model.Medication_IsDiuretics_point = "1";
            if (model.Medication_IsNarcotics == true) model.Medication_IsNarcotics_point = "1";
            if (model.Medication_IsAnalgesics == true) model.Medication_IsAnalgesics_point = "1";
            if (model.Medication_IsSedatives == true) model.Medication_IsSedatives_point = "1";
            if (model.Medication_IsAntiAnxiety == true) model.Medication_IsAntiAnxiety_point = "1";
            if (model.Medication_IsLaxatives == true) model.Medication_IsLaxatives_point = "1";
            if (model.MentalStatus_IsConfused == true) model.MentalStatus_IsConfused_point = "2";
            if (model.MentalStatus_IsResidentNonCompliance == true) model.MentalStatus_IsResidentNonCompliance_point = "2";
            if (model.Orthopedic_IsRecent == true) model.Orthopedic_IsRecent_point = "1";
            if (model.Orthopedic_IsCast == true) model.Orthopedic_IsCast_point = "1";
            if (model.Orthopedic_IsAmputation == true) model.Orthopedic_IsAmputation_point = "1";
            if (model.Orthopedic_IsSevere == true) model.Orthopedic_IsSevere_point = "1";
            if (model.Sensory_IsDecreasedVision == true) model.Sensory_IsDecreasedVision_point = "1";
            if (model.Sensory_IsDecreasedHearing == true) model.Sensory_IsDecreasedHearing_point = "1";
            if (model.Sensory_IsAphasia == true) model.Sensory_IsAphasia_point = "1";
            if (model.Assistive_IsWheelChair == true) model.Assistive_IsWheelChair_point = "1";
            if (model.Assistive_IsCane == true) model.Assistive_IsCane_point = "1";
            if (model.Assistive_IsWalker == true) model.Assistive_IsWalker_point = "1";
        }

        public ActionResult QOLAFIED()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = (ResidentModel)TempData["Resident"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            PlanOfCareModel l_Model = GET_one_CarePlan();
            ViewBag.careplan = l_Model;

            var l_FallRisk = AssessmentDAL.GetFallRiskAssessment(resident.ID);
            FallRiskAssessmentModel single = new FallRiskAssessmentModel();
            if (l_FallRisk.Count == 0)
            {
                l_FallRisk = new Collection<FallRiskAssessmentModel>();
                l_FallRisk.Add(new FallRiskAssessmentModel());
            }
            foreach (PropertyInfo prop in typeof(FallRiskAssessmentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(l_FallRisk[0]) == null) { prop.SetValue(l_FallRisk[0], ""); }
                }
            }
            if (l_FallRisk[0].DateEntered != DateTime.MinValue) { l_FallRisk[0].DateEntered_text = l_FallRisk[0].DateEntered.ToString("yyyy-MM-dd"); }
            pointArrangement(l_FallRisk[0]);
            single = l_FallRisk[0];
            ViewBag.fallrisk = single;

            return View(resident);
        }

        public ActionResult EmergencyList_mike(int orderType = 1)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            ResidentEmergencyListModel samples = HomeDAL.get_EmergencyList(home.Id, orderType);

            TempData["orderType"] = orderType.ToString();

            return View(samples);
        }

        public void btnPdf_Click_EmergencyList_mike(int orderType = 1)
        {
            string sException = string.Empty;
            int iHomeId = 0;
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            //Int32.TryParse(Session["HomeId"].ToString(), out iHomeId
            try
            {

                if (home != null)
                {
                    iHomeId = home.Id;
                    ResidentEmergencyListModel ds = new ResidentEmergencyListModel();
                    //ds = ResidentsDAL.GetEmergencyResidentDetails(iHomeId, "0");
                    ds = HomeDAL.get_EmergencyList(iHomeId, orderType);
                    if (ds != null)
                    {
                        Document doc = new Document(PageSize.A4, 30f, 30f, 40f, 20f);
                        doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(doc, mStream);


                        writer.PageEvent = new pdfHeaderFooter(home.Name);

                        doc.Open();

                        iTextSharp.text.Font fontCellSize12 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize12B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                        Paragraph paragraph = new Paragraph("Resident Emergency Evacuation Details", fontCellSize11B);
                        paragraph.Alignment = Element.TITLE;



                        if (ds.EmergencyResidentList.Count > 0)
                        {
                            string sReason = string.Empty;
                            string sReasonValue = string.Empty;

                            PdfPTable PdfTable1;

                            PdfTable1 = new PdfPTable(10);
                            float[] wthtbl1 = new float[] { 1.5f, 2f, 8f, 3f, 7f, 10f, 7f, 1.5f, 6f, 19f };
                            PdfTable1.SetWidths(wthtbl1);
                            PdfTable1.WidthPercentage = 100f;
                            PdfTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            PdfTable1.SpacingAfter = 5f;
                            PdfTable1.SpacingBefore = 5f;

                            PdfPCell cell = new PdfPCell(new Phrase("Resident Emergency Evacuation Details", fontCellSize11B));
                            cell.Border = 0;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Colspan = 10;
                            cell.PaddingBottom = 10f;
                            PdfTable1.AddCell(cell);

                            PdfPCell PdfTable1HeaderSuitNo = new PdfPCell(new Phrase("SuiteNo", fontCellSize9B));
                            PdfTable1HeaderSuitNo.Colspan = 2;
                            PdfTable1HeaderSuitNo.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderSuitNo);

                            PdfPCell PdfTable1HeaderName = new PdfPCell(new Phrase("Name", fontCellSize9B));
                            PdfTable1HeaderName.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderName);

                            PdfPCell PdfTable1HeaderGender = new PdfPCell(new Phrase("Gender", fontCellSize9B));
                            PdfTable1HeaderGender.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderGender);

                            PdfPCell PdfTable1HeaderPhone = new PdfPCell(new Phrase("Phone", fontCellSize9B));
                            PdfTable1HeaderPhone.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderPhone);

                            PdfPCell PdfTable1HeaderContactPerson = new PdfPCell(new Phrase("ContactPerson", fontCellSize9B));
                            PdfTable1HeaderContactPerson.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderContactPerson);

                            PdfPCell PdfTable1HeaderEmergencyContact = new PdfPCell(new Phrase("EmergencyContact", fontCellSize9B));
                            PdfTable1HeaderEmergencyContact.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderEmergencyContact);

                            PdfPCell PdfTable1HeaderMobilty = new PdfPCell(new Phrase("Mobility", fontCellSize9B));
                            PdfTable1HeaderMobilty.Colspan = 2;
                            PdfTable1HeaderMobilty.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderMobilty);

                            PdfPCell PdfTable1HeaderComment = new PdfPCell(new Phrase("Comments", fontCellSize9B));
                            PdfTable1HeaderComment.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderComment);

                            //if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))


                            for (int iRow = 0; iRow < ds.EmergencyResidentList.Count; iRow++)
                            {

                                sReasonValue = string.Empty;
                                sReason = string.Empty;
                                sReason = ds.EmergencyResidentList[iRow].Comments;

                                PdfPCell suiteColorCell = new PdfPCell();

                                if (ds.EmergencyResidentList[iRow].RiskLevel == "High Risk")
                                {
                                    suiteColorCell.BackgroundColor = BaseColor.RED;
                                }
                                else if (ds.EmergencyResidentList[iRow].RiskLevel == "Medium Risk")
                                {
                                    suiteColorCell.BackgroundColor = BaseColor.YELLOW;
                                }
                                else if (ds.EmergencyResidentList[iRow].RiskLevel == "Low Risk")
                                {
                                    suiteColorCell.BackgroundColor = BaseColor.GREEN;
                                }
                                else if (ds.EmergencyResidentList[iRow].RiskLevel == "No Risk")
                                {

                                }
                                PdfTable1.AddCell(suiteColorCell);
                                PdfPCell PdfTable1SuitNoValue = new PdfPCell(new Phrase(ds.EmergencyResidentList[iRow].suiteNo, fontCellSize9));
                                PdfTable1SuitNoValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1SuitNoValue);

                                PdfPCell PdfTable1NameValue = new PdfPCell(new Phrase(ds.EmergencyResidentList[iRow].FullName, fontCellSize9));
                                PdfTable1NameValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1NameValue);

                                PdfPCell PdfTable1GenderValue = new PdfPCell(new Phrase(ds.EmergencyResidentList[iRow].Gendar, fontCellSize9));
                                PdfTable1GenderValue.HorizontalAlignment = Element.ALIGN_CENTER;
                                PdfTable1.AddCell(PdfTable1GenderValue);

                                //string phone = ds.EmergencyResidentList[iRow].phone.Trim().Length > 0 ? ds.EmergencyResidentList[iRow].phone : "NoPhone";
                                PdfPCell PdfTable1AttendeesValue = new PdfPCell(new Phrase(ds.EmergencyResidentList[iRow].phone, fontCellSize9));
                                PdfTable1AttendeesValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1AttendeesValue);

                                PdfPCell PdfTable1PhoneValue = new PdfPCell(new Phrase(ds.EmergencyResidentList[iRow].contact, fontCellSize9));
                                PdfTable1PhoneValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1PhoneValue);

                                PdfPCell PdfTable1EmergencyContact = new PdfPCell(new Phrase(ds.EmergencyResidentList[iRow].contact_phone_final, fontCellSize9));
                                PdfTable1EmergencyContact.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1EmergencyContact);

                                PdfTable1.AddCell(suiteColorCell);



                                PdfPCell PdfTable1Mobility = new PdfPCell(new Phrase(ds.EmergencyResidentList[iRow].Mobility, fontCellSize9B));
                                PdfTable1Mobility.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1Mobility);

                                PdfPCell PdfTable1Comment = new PdfPCell(new Phrase(sReason.Replace("<b>", string.Empty).Replace("</b>", string.Empty), fontCellSize9));
                                PdfTable1Comment.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1Comment);
                            }


                            doc.Add(PdfTable1);
                        }

                        doc.Close();
                        string reportName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Emergency_Resident_Details_" + reportName + ".pdf");
                        Response.Clear();
                        Response.BinaryWrite(mStream.ToArray());
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                }
            }
            catch (Exception Ex)
            {
                sException = "frmEmergencyResidentDetails btnPdf_Click |" + Ex.Message.ToString();
                //Log.Write(sException);

            }
        }

        public ActionResult Nursing_Note()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;

            NursingNoteReport samples = HomeDAL.get_NursingNoteReport(home.Id, DateTime.Now);
            

            return View(samples);
        }

        public JsonResult Nursing_Note2(string datestring)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            DateTime date;
            if (datestring == "" || datestring == null)
            {
                date = DateTime.Now;
            }
            else
            {
                date = DateTime.Parse(datestring);
            }
            var samples = HomeDAL.get_NursingNoteReport(home.Id, date);

            List<dynamic> l_Json = new List<dynamic>();

            foreach (var r in samples.NursingNoteList)
            {
                dynamic l_J = new System.Dynamic.ExpandoObject();
                l_J.userName = r.userName;
                l_J.userNameType = r.userNameType;
                l_J.suiteNo = r.suiteNo;
                l_J.FullName = r.FullName;
                l_J.CategoryFull = r.CategoryFull;
                l_J.Note = r.Note;
                l_Json.Add(l_J);
            }
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }


        public void btnPdf_Click_Nursing_Note(string datestring = "")
        {
            string sException = string.Empty;
            int iHomeId = 0;
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            DateTime date = DateTime.Parse(datestring);


            //Int32.TryParse(Session["HomeId"].ToString(), out iHomeId
            try
            {

                if (home != null)
                {
                    iHomeId = home.Id;
                    //ds = ResidentsDAL.GetEmergencyResidentDetails(iHomeId, "0");
                    NursingNoteReport ds = HomeDAL.get_NursingNoteReport(home.Id, date);
                    if (ds != null)
                    {
                        Document doc = new Document(PageSize.A4, 30f, 30f, 40f, 20f);
                        doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(doc, mStream);


                        writer.PageEvent = new pdfHeaderFooter2(home.Name);

                        doc.Open();

                        iTextSharp.text.Font fontCellSize12 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize12B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                        Paragraph paragraph = new Paragraph("Nursing Note", fontCellSize11B);
                        paragraph.Alignment = Element.TITLE;



                        if (ds.NursingNoteList.Count > 0)
                        {
                            string sReason = string.Empty;
                            string sReasonValue = string.Empty;

                            PdfPTable PdfTable1;

                            PdfTable1 = new PdfPTable(6);
                            float[] wthtbl1 = new float[] { 10f, 5f, 10f, 10f, 40f, 10f};
                            PdfTable1.SetWidths(wthtbl1);
                            PdfTable1.WidthPercentage = 100f;
                            PdfTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            PdfTable1.SpacingAfter = 5f;
                            PdfTable1.SpacingBefore = 5f;

                            PdfPCell cell = new PdfPCell(new Phrase("Nursing Note" + " (" + date.ToString("yyyy-MM-dd") + ")", fontCellSize11B));
                            cell.Border = 0;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Colspan = 10;
                            cell.PaddingBottom = 10f;
                            PdfTable1.AddCell(cell);

                            PdfPCell PdfTable1HeaderSuitNo = new PdfPCell(new Phrase("User Name", fontCellSize9B));
                            //PdfTable1HeaderSuitNo.Colspan = 2;
                            PdfTable1HeaderSuitNo.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderSuitNo);

                            PdfPCell PdfTable1HeaderName = new PdfPCell(new Phrase("Suite", fontCellSize9B));
                            PdfTable1HeaderName.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderName);


                            PdfPCell PdfTable1HeaderPhone = new PdfPCell(new Phrase("Resident Name", fontCellSize9B));
                            PdfTable1HeaderPhone.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderPhone);

                            PdfPCell PdfTable1HeaderContactPerson = new PdfPCell(new Phrase("Category", fontCellSize9B));
                            PdfTable1HeaderContactPerson.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderContactPerson);

                            PdfPCell PdfTable1HeaderEmergencyContact = new PdfPCell(new Phrase("Note", fontCellSize9B));
                            PdfTable1HeaderEmergencyContact.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderEmergencyContact);

                            PdfPCell PdfTable1HeaderComment = new PdfPCell(new Phrase("Date", fontCellSize9B));
                            PdfTable1HeaderComment.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderComment);

                            //if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))


                            for (int iRow = 0; iRow < ds.NursingNoteList.Count; iRow++)
                            {

                                sReasonValue = string.Empty;
                                sReason = string.Empty;
                                sReason = ds.NursingNoteList[iRow].Note;

                                PdfPCell PdfTable1userName = new PdfPCell(new Phrase(ds.NursingNoteList[iRow].userName+" ("+ ds.NursingNoteList[iRow].userNameType+")", fontCellSize9));
                                PdfTable1userName.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1userName);

                                PdfPCell PdfTable1suiteNo = new PdfPCell(new Phrase(ds.NursingNoteList[iRow].suiteNo, fontCellSize9));
                                PdfTable1suiteNo.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1suiteNo);


                                //string phone = ds.EmergencyResidentList[iRow].phone.Trim().Length > 0 ? ds.EmergencyResidentList[iRow].phone : "NoPhone";
                                PdfPCell PdfTable1FullName = new PdfPCell(new Phrase(ds.NursingNoteList[iRow].FullName, fontCellSize9));
                                PdfTable1FullName.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1FullName);

                                PdfPCell PdfTable1CategoryFull = new PdfPCell(new Phrase(ds.NursingNoteList[iRow].CategoryFull, fontCellSize9));
                                PdfTable1CategoryFull.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1CategoryFull);

                                PdfPCell PdfTable1Note = new PdfPCell(new Phrase(ds.NursingNoteList[iRow].Note, fontCellSize9));
                                PdfTable1Note.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1Note);

                                PdfPCell PdfTable1DateEntered = new PdfPCell(new Phrase(ds.NursingNoteList[iRow].DateEntered.ToString("yyyy-MM-dd")+" "+ ds.NursingNoteList[iRow].DateEntered.ToShortTimeString(), fontCellSize9));
                                PdfTable1DateEntered.HorizontalAlignment = Element.ALIGN_LEFT;
                                PdfTable1.AddCell(PdfTable1DateEntered);


                                //PdfPCell PdfTable1Comment = new PdfPCell(new Phrase(sReason.Replace("<b>", string.Empty).Replace("</b>", string.Empty), fontCellSize9));
                                //PdfTable1Comment.HorizontalAlignment = Element.ALIGN_LEFT;
                                //PdfTable1.AddCell(PdfTable1Comment);
                            }


                            doc.Add(PdfTable1);
                        }

                        doc.Close();
                        string reportName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Nursing_Notes_List_" + reportName + ".pdf");
                        Response.Clear();
                        Response.BinaryWrite(mStream.ToArray());
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                }
            }
            catch (Exception Ex)
            {
                sException = "frmEmergencyResidentDetails btnPdf_Click |" + Ex.Message.ToString();
                //Log.Write(sException);

            }
        }


        public ActionResult ERDetails()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = (ResidentModel)TempData["Resident"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            PlanOfCareModel l_Model = GET_one_CarePlan();
            ViewBag.careplan = l_Model;



            //return new Rotativa.ViewAsPdf("ERDetails", resident);
            return View(resident);
        }

        public ActionResult ResidentMenu2(int p_ResidentId)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = ResidentsDAL.GetResidentById(p_ResidentId);
            var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");

            //ViewBag.Message = TempData["Message"];
            ViewBag.Message = "";

            TempData["Resident"] = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            ViewBag.ProgressNotes = progressNotes;
            ProgressNotesHelper.RegisterSession(resident);
            TempData["NOTE"] = "YES";
            TempData["archive"] = "NO";
            TempData["pass"] = "";

            PlanOfCareModel l_Model = GET_one_CarePlan();
            ViewBag.careplan = l_Model;

            return View("ResidentMenu", resident);

        }

        public ActionResult ResidentMenu3(int p_ResidentId, string pass)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = ResidentsDAL.GetResidentById(p_ResidentId);
            var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");

            //ViewBag.Message = TempData["Message"];
            ViewBag.Message = "";

            TempData["Resident"] = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            ViewBag.ProgressNotes = progressNotes;
            ProgressNotesHelper.RegisterSession(resident);
            TempData["archive"] = "YES";
            TempData["NOTE"] = "NO";
            TempData["pass"] = pass;
            TempData["tempRID"] = resident.ID.ToString();

            PlanOfCareModel l_Model = GET_one_CarePlan();
            ViewBag.careplan = l_Model;

            return View("ResidentMenu", resident);

        }



        public ActionResult ManageResidents(int p_HomeId)
        {
            var l_Residents = ResidentsDAL.GetResidentCollections(p_HomeId);
            return View(l_Residents);
        }

        public ActionResult AddNewResident()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;

            ResidentModel AA = new ResidentModel();
            ResidentsDAL.SetUp_ResidentModel_ListItems(AA);

            //ResidentModel p_Model = (ResidentModel)TempData["p_Model"];

            AA.BirthDate = DateTime.Now;
            AA.MoveInDate = DateTime.Now;
            return View(AA);

        }

        [HttpPost]
        public ActionResult CreateNewResident(ResidentModel p_Model, string Sub)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;

            if (p_Model.DNRStatusIndex == true) p_Model.DNRStatus = 'Y';
            if (p_Model.FullCodeStatusIndex == true)  p_Model.FullCodeStatus = 'Y';
            //if (p_Model.AHS_index == true) p_Model.AHS = 'Y';

            foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(p_Model) == null) { prop.SetValue(p_Model, ""); }
                }
            }


            if (p_Model.ReligiousAffiliation == "Other")
            {
                p_Model.ReligiousAffiliation = p_Model.ReligiousAffiliationOther;
            }
            if (p_Model.Vetaran == "Other" || p_Model.Vetaran == "None")
            {
                p_Model.Vetaran = p_Model.VeteranOther;
            }
            if (p_Model.EducationLevel == "Other")
            {
                p_Model.EducationLevel = p_Model.EducationLevelOther;
            }
            if (p_Model.callHospital_replacement == true)
            {
                p_Model.CallHospital = Convert.ToChar("Y");
            }

            #region Phone arrangement in second Page

            if (p_Model.First_phone_type1 == 1) { p_Model.HomePhone1 = p_Model.First_phone1; p_Model.HomePhoneType1 = 1; }
            else if (p_Model.First_phone_type1 == 2) { p_Model.BusinessPhone1 = p_Model.First_phone1; p_Model.BusinessPhoneType1 = 1; }
            else if (p_Model.First_phone_type1 == 3) { p_Model.CellPhone1 = p_Model.First_phone1; p_Model.CellPhoneType1 = 1; }

            if (p_Model.First_phone_type2 == 1) { p_Model.HomePhone2 = p_Model.First_phone2; p_Model.HomePhoneType2 = 1; }
            else if (p_Model.First_phone_type2 == 2) { p_Model.BusinessPhone2 = p_Model.First_phone2; p_Model.BusinessPhoneType2 = 1; }
            else if (p_Model.First_phone_type2 == 3) { p_Model.CellPhone2 = p_Model.First_phone2; p_Model.CellPhoneType2 = 1; }

            if (p_Model.First_phone_type3 == 1) { p_Model.HomePhone3 = p_Model.First_phone3; p_Model.HomePhoneType3 = 1; }
            else if (p_Model.First_phone_type3 == 2) { p_Model.BusinessPhone3 = p_Model.First_phone3; p_Model.BusinessPhoneType3 = 1; }
            else if (p_Model.First_phone_type3 == 3) { p_Model.CellPhone3 = p_Model.First_phone3; p_Model.CellPhoneType3 = 1; }

            if (p_Model.Second_phone_type1 == 1) { p_Model.HomePhone1 = p_Model.Second_phone1; p_Model.HomePhoneType1 = 2; }
            else if (p_Model.Second_phone_type1 == 2) { p_Model.BusinessPhone1 = p_Model.Second_phone1; p_Model.BusinessPhoneType1 = 2; }
            else if (p_Model.Second_phone_type1 == 3) { p_Model.CellPhone1 = p_Model.Second_phone1; p_Model.CellPhoneType1 = 2; }

            if (p_Model.Second_phone_type2 == 1) { p_Model.HomePhone2 = p_Model.Second_phone2; p_Model.HomePhoneType2 = 2; }
            else if (p_Model.Second_phone_type2 == 2) { p_Model.BusinessPhone2 = p_Model.Second_phone2; p_Model.BusinessPhoneType2 = 2; }
            else if (p_Model.Second_phone_type2 == 3) { p_Model.CellPhone2 = p_Model.Second_phone2; p_Model.CellPhoneType2 = 2; }

            if (p_Model.Second_phone_type3 == 1) { p_Model.HomePhone3 = p_Model.Second_phone3; p_Model.HomePhoneType3 = 2; }
            else if (p_Model.Second_phone_type3 == 2) { p_Model.BusinessPhone3 = p_Model.Second_phone3; p_Model.BusinessPhoneType3 = 2; }
            else if (p_Model.Second_phone_type3 == 3) { p_Model.CellPhone3 = p_Model.Second_phone3; p_Model.CellPhoneType3 = 2; }

            if (p_Model.Third_phone_type1 == 1) { p_Model.HomePhone1 = p_Model.Third_phone1; p_Model.HomePhoneType1 = 3; }
            else if (p_Model.Third_phone_type1 == 2) { p_Model.BusinessPhone1 = p_Model.Third_phone1; p_Model.BusinessPhoneType1 = 3; }
            else if (p_Model.Third_phone_type1 == 3) { p_Model.CellPhone1 = p_Model.Third_phone1; p_Model.CellPhoneType1 = 3; }

            if (p_Model.Third_phone_type2 == 1) { p_Model.HomePhone2 = p_Model.Third_phone2; p_Model.HomePhoneType2 = 3; }
            else if (p_Model.Third_phone_type2 == 2) { p_Model.BusinessPhone2 = p_Model.Third_phone2; p_Model.BusinessPhoneType2 = 3; }
            else if (p_Model.Third_phone_type2 == 3) { p_Model.CellPhone2 = p_Model.Third_phone2; p_Model.CellPhoneType2 = 3; }

            if (p_Model.Third_phone_type3 == 1) { p_Model.HomePhone3 = p_Model.Third_phone3; p_Model.HomePhoneType3 = 3; }
            else if (p_Model.Third_phone_type3 == 2) { p_Model.BusinessPhone3 = p_Model.Third_phone3; p_Model.BusinessPhoneType3 = 3; }
            else if (p_Model.Third_phone_type3 == 3) { p_Model.CellPhone3 = p_Model.Third_phone3; p_Model.CellPhoneType3 = 3; }

            if (p_Model.Care_First_phone_type == 1) { p_Model.CareHomePhone = p_Model.Care_First_phone; p_Model.POACareHomePhoneType = 1; }
            else if (p_Model.Care_First_phone_type == 2) { p_Model.CareWorkPhone = p_Model.Care_First_phone; p_Model.POACareBusinessPhoneType = 1; }
            else if (p_Model.Care_First_phone_type == 3) { p_Model.CareCellPhone = p_Model.Care_First_phone; p_Model.POACareCellPhoneType = 1; }

            if (p_Model.Care_Second_phone_type == 1) { p_Model.CareHomePhone = p_Model.Care_Second_phone; p_Model.POACareHomePhoneType = 2; }
            else if (p_Model.Care_Second_phone_type == 2) { p_Model.CareWorkPhone = p_Model.Care_Second_phone; p_Model.POACareBusinessPhoneType = 2; }
            else if (p_Model.Care_Second_phone_type == 3) { p_Model.CareCellPhone = p_Model.Care_Second_phone; p_Model.POACareCellPhoneType = 2; }

            if (p_Model.Care_Third_phone_type == 1) { p_Model.CareHomePhone = p_Model.Care_Third_phone; p_Model.POACareHomePhoneType = 3; }
            else if (p_Model.Care_Third_phone_type == 2) { p_Model.CareWorkPhone = p_Model.Care_Third_phone; p_Model.POACareBusinessPhoneType = 3; }
            else if (p_Model.Care_Third_phone_type == 3) { p_Model.CareCellPhone = p_Model.Care_Third_phone; p_Model.POACareCellPhoneType = 3; }

            if (p_Model.Finance_First_phone_type == 1) { p_Model.FinanceHomePhone = p_Model.Finance_First_phone; p_Model.POAFinanceHomePhoneType = 1; }
            else if (p_Model.Finance_First_phone_type == 2) { p_Model.FinanceWorkPhone = p_Model.Finance_First_phone; p_Model.POAFinanceBusinessPhoneType = 1; }
            else if (p_Model.Finance_First_phone_type == 3) { p_Model.FinanceCellPhone = p_Model.Finance_First_phone; p_Model.POAFinanceCellPhoneType = 1; }

            if (p_Model.Finance_Second_phone_type == 1) { p_Model.FinanceHomePhone = p_Model.Finance_Second_phone; p_Model.POAFinanceHomePhoneType = 2; }
            else if (p_Model.Finance_Second_phone_type == 2) { p_Model.FinanceWorkPhone = p_Model.Finance_Second_phone; p_Model.POAFinanceBusinessPhoneType = 2; }
            else if (p_Model.Finance_Second_phone_type == 3) { p_Model.FinanceCellPhone = p_Model.Finance_Second_phone; p_Model.POAFinanceCellPhoneType = 2; }

            if (p_Model.Finance_Third_phone_type == 1) { p_Model.FinanceHomePhone = p_Model.Finance_Third_phone; p_Model.POAFinanceHomePhoneType = 3; }
            else if (p_Model.Finance_Third_phone_type == 2) { p_Model.FinanceWorkPhone = p_Model.Finance_Third_phone; p_Model.POAFinanceBusinessPhoneType = 3; }
            else if (p_Model.Finance_Third_phone_type == 3) { p_Model.FinanceCellPhone = p_Model.Finance_Third_phone; p_Model.POAFinanceCellPhoneType = 3; }

            #endregion Phone arrangement in second Page

            if (p_Model.FullCodeStatus == '\0') p_Model.FullCodeStatus = 'N';
            if (p_Model.DNRStatus == '\0') p_Model.DNRStatus = 'N';

            p_Model.SuiteIds= SuiteDAL.GetSuiteID_By_SuiteNo_and_Homeid(p_Model.SuiteNo, home.Id);

            ResidentsDAL.SetUp_ResidentModel_ListItems(p_Model);

            p_Model.ModifiedBy = user;
            p_Model.ModifiedOn = DateTime.Now;
            p_Model.Home = home;
            int[] RR = new int[2];
            RR=ResidentsDAL.AddNewResidentGeneralInfo(p_Model);
            bool result1 = ResidentsDAL.UpdateResidentEmergencyContacts(p_Model);
            bool result2 = ResidentsDAL.UpdateResidentGeneralInfo(p_Model);

            bool result3 = ResidentsDAL.UpdateResidentMedicalInfo_mike(p_Model);


            int returnint2 = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                if (fileSize > 0)
                {
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/Content/assets/Images/Home/" + home.Id + "/Resident_Image/") + RR[0].ToString() + ".png");
                    returnint2 = HomeDAL.Save_Image(home.Id, RR[0]);

                    TempData["Photo"] = "TRUE";
                    TempData.Keep("Photo");
                    p_Model.ResidentImage= "Images/Home/" + home.Id + "/Resident_Image/" +RR[0].ToString() + ".png";
                }

            }
            string[] arrayMain = new string[22];
            for (int a = 0; a < arrayMain.Length; a++)
            {
                if (a == 0)
                {
                    MasterDAL.save_button(a + 1, user.ID, RR[0]);
                }
                else
                {
                    MasterDAL.save_button(a + 1, RR[0]);
                }
            }

            //ResidentsDAL.update_checklist(user.ID,RR[0]);
            //TempData["p_Model"] = p_Model;
            //TempData.Keep("p_Model");

            return RedirectToAction("ResidentMenu",new { p_ResidentId= RR[0] });
        }


        #region SAC

        public JsonResult getEvents_mike_SACnotColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;           

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_ActivityEvents = HomeDAL.GetActivityEvents_SACnotColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_mike_SACColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            var l_ActivityEvents = HomeDAL.GetActivityEvents_SACColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C2_mike_SACnotColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C2_SACnotColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C2_mike_SACColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C2_SACColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C3_mike_SACnotColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C3_SACnotColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C3_mike_SACColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C3_SACColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C4_mike_SACnotColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C4_SACnotColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C4_mike_SACColor()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C4_SACColor(home.Id, resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramEndDate.Day);

                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "Venue", l_Data.Venue},
                    { "note", l_Data.note},
                    { "Category", "1"},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getEvents_mike_SACnotColor_between_ALL(string start,string end,int calendar_number)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_ActivityEvents = HomeDAL.GetActivityEvents_SACnotColor_between_ALL(home.Id, resident.ID,start,end, calendar_number);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_mike_SACColor_between_ALL(string start, string end, int calendar_number)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_ActivityEvents = HomeDAL.GetActivityEvents_SACColor_between_ALL(home.Id, resident.ID, start, end, calendar_number);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult update_day_div1_SAC(string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<ActivityEventModel> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_mike_SAC(DateTime.Parse(datesel).Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Color", l_Data.Color.ToString()}

                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult update_day_div2_SAC(string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<ActivityEventModel_Calendar2> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_Calendar2_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_Calendar2_mike_SAC(DateTime.Parse(datesel).Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Color", l_Data.Color.ToString()}
                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult update_day_div3_SAC(string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<ActivityEventModel_Calendar3> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_Calendar3_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_Calendar3_mike_SAC(DateTime.Parse(datesel).Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Color", l_Data.Color.ToString()}
                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult update_day_div4_SAC(string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<ActivityEventModel_Calendar4> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_Calendar4_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_Calendar4_mike_SAC(DateTime.Parse(datesel).Date, home.Id, resident.ID);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Color", l_Data.Color.ToString()}
                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }

        #endregion



        #region ajax requests

        public JsonResult getEvents_mike()
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get(home.Id);//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getEvents_mike_between(string start, string end)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = HomeDAL.GetActivityEvents_between(home.Id,start,end);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AddEvents_mike_All(string aa, string bb, string cc, string dd, string ee, string ff, string gg, string hh, string ii, string fre,string tab)
        {
            Collection<ActivityEventModel> EVENTS = new Collection<ActivityEventModel>();
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            ActivityEventModel l_Model;
            var frequency = Convert.ToInt32(fre);
            List<DateTime> l_Dates = new List<DateTime>();

            if (frequency == 1) //date & time
            {
                l_Model = new ActivityEventModel();
                l_Model.ActivityId = Convert.ToInt32(bb);
                l_Model.ProgramName = Convert.ToString(aa);
                l_Model.Venue = hh;
                l_Model.note = ii;

                var startDate = Convert.ToDateTime(cc);
                var time = startDate.ToLongTimeString();

                l_Model.ProgramStartDate = startDate;
                l_Model.ProgramStartTime = time;
                l_Model.ProgramEndDate = startDate;
                l_Model.ProgramEndTime = time;

                int temp = HomeDAL.AddNewActivityEvent_All(l_Model, home.Id, 0,tab);
                l_Model.ProgramId = temp;
                l_Model.CategoryId = HomeDAL.GetCategoryIdbyActivityID(l_Model.ActivityId).ToString();
                l_Model.MemoryCardColor = MasterDAL.GetActivityCategoryById(Int32.Parse(l_Model.CategoryId)).MemoryCareColor;
                if (l_Model.Venue.Trim() != "")
                {
                    l_Model.Code = HomeDAL.GetCodebyVenue(Int32.Parse(l_Model.Venue));
                }
                else
                {
                    l_Model.Code = "";
                }
                l_Model.Special = 0;
                EVENTS.Add(l_Model);
            }
            else if (frequency == 2) //every week day
            {
                var time = Convert.ToString(gg);
                var weekDay = Convert.ToString(dd);
                l_Dates = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                .Where(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d).ToString("dddd").Equals(weekDay))
               .Select(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d)).ToList();

                foreach (var l_D in l_Dates)
                {
                    l_Model = new ActivityEventModel();
                    l_Model.ActivityId = Convert.ToInt32(bb);
                    l_Model.ProgramName = Convert.ToString(aa);
                    l_Model.Venue = hh;
                    l_Model.note = ii;

                    l_Model.ProgramStartDate = l_D;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = l_D;
                    l_Model.ProgramEndTime = time;

                    int temp = HomeDAL.AddNewActivityEvent_All(l_Model, home.Id, 0,tab);
                    l_Model.ProgramId = temp;
                    l_Model.CategoryId = HomeDAL.GetCategoryIdbyActivityID(l_Model.ActivityId).ToString();
                    l_Model.MemoryCardColor = MasterDAL.GetActivityCategoryById(Int32.Parse(l_Model.CategoryId)).MemoryCareColor;
                    if (l_Model.Venue != "")
                    {
                        l_Model.Code = HomeDAL.GetCodebyVenue(Int32.Parse(l_Model.Venue));
                    }
                    else
                    {
                        l_Model.Code = "";
                    }
                    l_Model.Special = 0;
                    EVENTS.Add(l_Model);
                }
            }
            else if (frequency == 3) //date between
            {
                var dateFrom = Convert.ToDateTime(ee);
                var dateTo = Convert.ToDateTime(ff);
                var time = Convert.ToString(gg);

                for (var dt = dateFrom; dt <= dateTo; dt = dt.AddDays(1))
                {
                    l_Model = new ActivityEventModel();
                    l_Model.ActivityId = Convert.ToInt32(bb);
                    l_Model.ProgramName = Convert.ToString(aa);
                    l_Model.Venue = hh;
                    l_Model.note = ii;

                    l_Model.ProgramStartDate = dt;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = dt;
                    l_Model.ProgramEndTime = time;

                    int temp = HomeDAL.AddNewActivityEvent_All(l_Model, home.Id, 0,tab);
                    l_Model.ProgramId = temp;
                    l_Model.CategoryId = HomeDAL.GetCategoryIdbyActivityID(l_Model.ActivityId).ToString();
                    l_Model.MemoryCardColor = MasterDAL.GetActivityCategoryById(Int32.Parse(l_Model.CategoryId)).MemoryCareColor;
                    if (l_Model.Venue != "")
                    {
                        l_Model.Code = HomeDAL.GetCodebyVenue(Int32.Parse(l_Model.Venue));
                    }
                    else
                    {
                        l_Model.Code = "";
                    }
                    
                    l_Model.Special = 0;
                    EVENTS.Add(l_Model);
                }
            }

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(EVENTS);
            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddEvents_mike_EDIT_All(string ID, string aa, string bb, string cc, string hh, string ii,string tab)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            ActivityEventModel l_Model = new ActivityEventModel();
            l_Model.ActivityId = Convert.ToInt32(bb);
            l_Model.ProgramName = Convert.ToString(aa);
            l_Model.Venue = hh;
            l_Model.note = ii;

            var startDate = Convert.ToDateTime(cc);
            var time = startDate.ToLongTimeString();

            l_Model.ProgramStartDate = startDate;
            l_Model.ProgramStartTime = time;
            l_Model.ProgramEndDate = startDate;
            l_Model.ProgramEndTime = time;

            HomeDAL.EditNewActivityEvent_All(int.Parse(ID), l_Model, home.Id,tab);
            l_Model.ProgramId = int.Parse(ID);

            l_Model.CategoryId = HomeDAL.GetCategoryIdbyActivityID(l_Model.ActivityId).ToString();
            l_Model.MemoryCardColor = MasterDAL.GetActivityCategoryById(Int32.Parse(l_Model.CategoryId)).MemoryCareColor;
            if (l_Model.Venue.Trim() != "")
            {
                l_Model.Code = HomeDAL.GetCodebyVenue(Int32.Parse(l_Model.Venue));
            }
            else
            {
                l_Model.Code = "";
            }
            l_Model.Special = 0;




            //var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get();//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events_single(l_Model);

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddEvents_mike_Special_All(string aa, string bb, string cc, string dd, string ee, string ff, string gg, string hh, string ii, string fre, string AddType, string homestring,string tab)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;

            if (user.UserType==1)
            {
                Collection<ActivityEventModel> EVENTS = new Collection<ActivityEventModel>();
                ActivityEventModel l_Model;
                var frequency = Convert.ToInt32(fre);
                List<DateTime> l_Dates = new List<DateTime>();

                if (frequency == 1) //date & time
                {
                    l_Model = new ActivityEventModel();
                    l_Model.ActivityId = Convert.ToInt32(bb);
                    l_Model.ProgramName = Convert.ToString(aa);
                    l_Model.Venue = hh;
                    l_Model.note = ii;

                    var startDate = Convert.ToDateTime(cc);
                    var time = startDate.ToLongTimeString();

                    l_Model.ProgramStartDate = startDate;
                    l_Model.ProgramStartTime = time;
                    l_Model.ProgramEndDate = startDate;
                    l_Model.ProgramEndTime = time;


                    int temp = Add_Special_Event_Function_All(l_Model, home.Id, AddType, homestring,tab);


                    //int temp = HomeDAL.AddNewActivityEvent(l_Model, home.Id, 1);
                    if (temp > 0)
                    {
                        l_Model.ProgramId = temp;
                        l_Model.CategoryId = HomeDAL.GetCategoryIdbyActivityID(l_Model.ActivityId).ToString();
                        l_Model.MemoryCardColor = MasterDAL.GetActivityCategoryById(Int32.Parse(l_Model.CategoryId)).MemoryCareColor;
                        if (l_Model.Venue.Trim() != "")
                        {
                            l_Model.Code = HomeDAL.GetCodebyVenue(Int32.Parse(l_Model.Venue));
                        }
                        else
                        {
                            l_Model.Code = "";
                        }
                        l_Model.Special = 1;
                        EVENTS.Add(l_Model);
                    }

                }
                else if (frequency == 2) //every week day
                {
                    var time = Convert.ToString(gg);
                    var weekDay = Convert.ToString(dd);
                    l_Dates = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                    .Where(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d).ToString("dddd").Equals(weekDay))
                   .Select(d => new DateTime(DateTime.Now.Year, DateTime.Now.Month, d)).ToList();

                    foreach (var l_D in l_Dates)
                    {
                        l_Model = new ActivityEventModel();
                        l_Model.ActivityId = Convert.ToInt32(bb);
                        l_Model.ProgramName = Convert.ToString(aa);
                        l_Model.Venue = hh;
                        l_Model.note = ii;

                        l_Model.ProgramStartDate = l_D;
                        l_Model.ProgramStartTime = time;
                        l_Model.ProgramEndDate = l_D;
                        l_Model.ProgramEndTime = time;

                        int temp = Add_Special_Event_Function_All(l_Model, home.Id, AddType, homestring,tab);


                        //int temp = HomeDAL.AddNewActivityEvent(l_Model, home.Id, 1);
                        if (temp > 0)
                        {
                            l_Model.ProgramId = temp;
                            l_Model.CategoryId = HomeDAL.GetCategoryIdbyActivityID(l_Model.ActivityId).ToString();
                            l_Model.MemoryCardColor = MasterDAL.GetActivityCategoryById(Int32.Parse(l_Model.CategoryId)).MemoryCareColor;
                            if (l_Model.Venue.Trim() != "")
                            {
                                l_Model.Code = HomeDAL.GetCodebyVenue(Int32.Parse(l_Model.Venue));
                            }
                            else
                            {
                                l_Model.Code = "";
                            }
                            l_Model.Special = 1;
                            EVENTS.Add(l_Model);
                        }
                    }
                }
                else if (frequency == 3) //date between
                {
                    var dateFrom = Convert.ToDateTime(ee);
                    var dateTo = Convert.ToDateTime(ff);
                    var time = Convert.ToString(gg);

                    for (var dt = dateFrom; dt <= dateTo; dt = dt.AddDays(1))
                    {
                        l_Model = new ActivityEventModel();
                        l_Model.ActivityId = Convert.ToInt32(bb);
                        l_Model.ProgramName = Convert.ToString(aa);
                        l_Model.Venue = hh;
                        l_Model.note = ii;

                        l_Model.ProgramStartDate = dt;
                        l_Model.ProgramStartTime = time;
                        l_Model.ProgramEndDate = dt;
                        l_Model.ProgramEndTime = time;

                        int temp = Add_Special_Event_Function_All(l_Model, home.Id, AddType, homestring,tab);


                        //int temp = HomeDAL.AddNewActivityEvent(l_Model, home.Id, 1);
                        if (temp > 0)
                        {
                            l_Model.ProgramId = temp;
                            l_Model.CategoryId = HomeDAL.GetCategoryIdbyActivityID(l_Model.ActivityId).ToString();
                            l_Model.MemoryCardColor = MasterDAL.GetActivityCategoryById(Int32.Parse(l_Model.CategoryId)).MemoryCareColor;
                            if (l_Model.Venue.Trim() != "")
                            {
                                l_Model.Code = HomeDAL.GetCodebyVenue(Int32.Parse(l_Model.Venue));
                            }
                            else
                            {
                                l_Model.Code = "";
                            }
                            l_Model.Special = 1;
                            EVENTS.Add(l_Model);
                        }
                    }
                }

                List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(EVENTS);
                return Json(l_Events, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
            }



            //var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get();//HomeDAL.GetActivityEvents();


        }

        public int Add_Special_Event_Function_All(ActivityEventModel l_Model, int homeID, string AddType, string homestring,string tab)
        {
            int temp = 0;
            if (AddType == "1")
            {
                Collection<HomeModel> HomeModels = HomeDAL.GetHomeCollections();
                foreach (var sin in HomeModels)
                {
                    if (sin.Id == homeID) temp = HomeDAL.AddNewActivityEvent_All(l_Model, sin.Id, 1, tab);
                    else HomeDAL.AddNewActivityEvent_All(l_Model, sin.Id, 1,tab);
                }
            }
            else if (AddType == "2")
            {
                Collection<HomeModel> HomeModels = HomeDAL.GetHomeCollections();
                foreach (var sin in HomeModels)
                {
                    if (sin.Province.ID == Int32.Parse(homestring))
                    {
                        if (sin.Id == homeID) temp = HomeDAL.AddNewActivityEvent_All(l_Model, sin.Id, 1,tab);
                        else HomeDAL.AddNewActivityEvent_All(l_Model, sin.Id, 1,tab);
                    }

                }
            }
            else if (AddType == "3")
            {
                foreach (var sin in homestring.Split(','))
                {
                    if (sin.Trim() != "")
                    {
                        if (Int32.Parse(sin) == homeID) temp = HomeDAL.AddNewActivityEvent_All(l_Model, Int32.Parse(sin), 1,tab);
                        else HomeDAL.AddNewActivityEvent_All(l_Model, Int32.Parse(sin), 1,tab);
                    }



                }
            }

            return temp;
        }

        [HttpGet]
        public JsonResult Add_All_Activity_From_Prev_Month_Mike(int calendarnumber, string destinationFirstDate)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;

            int daydiff = 0;
            string destinationFirstDateFinal = DateTime.Parse(destinationFirstDate).ToString("yyyy-MM-dd");
            int destinationFirstDateFinal_int = (int)(DateTime.Parse(destinationFirstDate).DayOfWeek);
            int previousFirstDateFinal_int = (int)(DateTime.Parse(destinationFirstDate).AddMonths(-1).DayOfWeek);
            if(destinationFirstDateFinal_int>= previousFirstDateFinal_int)
            {
                daydiff = 28;
            }
            else
            {
                daydiff = 35;
            }

            var l_ActivityEvents = HomeDAL.Add_All_Activity_From_Prev_Month_Mike(home.Id, daydiff, calendarnumber, destinationFirstDateFinal);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public JsonResult Delete_All_Activity_From_Current_Month_Mike(int calendarnumber, string destinationFirstDate)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;

            string destinationFirstDateFinal = DateTime.Parse(destinationFirstDate).ToString("yyyy-MM-dd");

            var l_ActivityEvents = HomeDAL.Delete_All_Activity_From_Current_Month_Mike(home.Id, calendarnumber, destinationFirstDateFinal);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);

            return Json(l_Events, JsonRequestBehavior.AllowGet);


        }







        #region Activity_Calendar_Drag_And_Drop
        [HttpPost]
        public void EVENT_DragandDrop(string eventID, string destinationDT)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.EVENT_DragandDrop(Int32.Parse(eventID),DateTime.Parse(destinationDT),home.Id);

        }
        [HttpPost]
        public void EVENT_DragandDrop2(string eventID, string destinationDT)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.EVENT_DragandDrop2(Int32.Parse(eventID), DateTime.Parse(destinationDT), home.Id);

        }
        [HttpPost]
        public void EVENT_DragandDrop3(string eventID, string destinationDT)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.EVENT_DragandDrop3(Int32.Parse(eventID), DateTime.Parse(destinationDT), home.Id);

        }
        [HttpPost]
        public void EVENT_DragandDrop4(string eventID, string destinationDT)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.EVENT_DragandDrop4(Int32.Parse(eventID), DateTime.Parse(destinationDT), home.Id);

        }

        #endregion


        #region Activity_Calendar_Delete

        [HttpGet]
        public int DELETE_EVENT(string eventID)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.DELETE_EVENT(Int32.Parse(eventID),  home.Id);
            return int.Parse(eventID);
        }
        [HttpGet]
        public int DELETE_EVENT2(string eventID)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.DELETE_EVENT2(Int32.Parse(eventID), home.Id);
            return int.Parse(eventID);
        }
        [HttpGet]
        public int DELETE_EVENT3(string eventID)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.DELETE_EVENT3(Int32.Parse(eventID), home.Id);
            return int.Parse(eventID);
        }
        [HttpGet]
        public int DELETE_EVENT4(string eventID)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            HomeDAL.DELETE_EVENT4(Int32.Parse(eventID), home.Id);
            return int.Parse(eventID);
        }

        #endregion

        #region Activity_Calendar_Copy

        [HttpGet]
        public JsonResult COPY_EVENT(string eventID,string NewDate)
        {
            DateTime DT = DateTime.Parse(NewDate);
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;

            var l_ActivityEvents = HomeDAL.COPY_EVENT(Int32.Parse(eventID), DT, home.Id);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult COPY_EVENT2(string eventID, string NewDate)
        {
            DateTime DT = DateTime.Parse(NewDate);
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;

            var l_ActivityEvents = HomeDAL.COPY_EVENT2(Int32.Parse(eventID), DT, home.Id);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult COPY_EVENT3(string eventID, string NewDate)
        {
            DateTime DT = DateTime.Parse(NewDate);
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;

            var l_ActivityEvents = HomeDAL.COPY_EVENT3(Int32.Parse(eventID), DT, home.Id);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult COPY_EVENT4(string eventID, string NewDate)
        {
            DateTime DT = DateTime.Parse(NewDate);
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;

            var l_ActivityEvents = HomeDAL.COPY_EVENT4(Int32.Parse(eventID), DT, home.Id);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);


        }

        #endregion

        [HttpGet]
        public ActionResult update_day_div1(string datesel)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;

            Collection<ActivityEventModel> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Category", l_Data.CategoryId.ToString()}
                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }
            
            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult update_day_div2(string datesel)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;

            Collection<ActivityEventModel_Calendar2> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_Calendar2_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_Calendar2_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Category", l_Data.CategoryId.ToString()}
                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult update_day_div3(string datesel)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;

            Collection<ActivityEventModel_Calendar3> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_Calendar3_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_Calendar3_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Category", l_Data.CategoryId.ToString()}
                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult update_day_div4(string datesel)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;

            Collection<ActivityEventModel_Calendar4> l_Events;

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                l_Events = HomeDAL.GetActivityEvents_Calendar4_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events = l_Events;
            }
            else
            {
                TempData["datechoose"] = datesel;
                l_Events = HomeDAL.GetActivityEvents_Calendar4_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events = l_Events;
            }

            List<Dictionary<string, string>> JsonEVENTs = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Events)
            {
                try
                {
                    var columns = new Dictionary<string, string>
                    {
                        { "ProgramId", l_Data.ProgramId.ToString()},
                        { "ActivityId", l_Data.ActivityId.ToString()},
                        { "ProgramName", l_Data.ProgramName},
                        { "ActivityNameEnglish", l_Data.ActivityNameEnglish},
                        { "ProgramStartTime", DateTime.Parse(l_Data.ProgramStartTime).ToShortTimeString()},
                        { "Venue", l_Data.Venue},
                        { "Active", l_Data.Active.ToString()},
                        { "Declined", l_Data.Declined.ToString()},
                        { "Category", l_Data.CategoryId.ToString()}
                    };
                    JsonEVENTs.Add(columns);
                }
                catch (Exception ex)
                {

                }
            }

            return Json(JsonEVENTs, JsonRequestBehavior.AllowGet);

        }





        public JsonResult getCategoriesForCalendar()
        {
            List<ActivityModel> l_Model = MasterDAL.GetAllActivity();
            List<Dictionary<string, string>> l_ActivityCategories = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Model)
            {
                var columns = new Dictionary<string, string>
                {
                    { "Id", l_Data.Id.ToString() },
                    {"Name", l_Data.EnglishName},
                    {"Category", l_Data.Category.Id.ToString()}
                };

                l_ActivityCategories.Add(columns);
            }

            return Json(l_ActivityCategories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getVenuesForCalendar()
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            List<Venue> l_Model = MasterDAL.GetAllVenuebyHome(home.Id);
            List<Dictionary<string, string>> l_Venues = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_Model)
            {
                var columns = new Dictionary<string, string>
                {
                    { "Id", l_Data.Id.ToString() },
                    {"code", l_Data.code},
                    {"venue", l_Data.venue}
                };

                l_Venues.Add(columns);
            }

            return Json(l_Venues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBirthdayCalendar()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            
            ViewBag.Message = TempData["Message"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            var l_ActivityEvents = new QolaMVC.WebAPI.BirthdayCalendarController().Get(home.Id);//HomeDAL.GetActivityEvents();

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramEndDate.Year, l_Data.ProgramEndDate.Month, l_Data.ProgramEndDate.Day);
                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.note},
                    { "suite", l_Data.Venue},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "ActivityId", l_Data.ActivityId.ToString()}

                };

                l_Events.Add(columns);
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuggestedActivityCalendar()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetSuggestedActivityEvents(resident.ID);
            
            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", l_Data.ProgramStartDate.ToShortDateString()},
                    { "endDate", l_Data.ProgramEndDate.ToShortDateString()},
                    { "startTime", l_Data.ProgramStartTime},
                    { "endTime", l_Data.ProgramEndTime}
                };

                l_Events.Add(columns);
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        #region calendar 2

        public JsonResult getEvents_C2_mike()
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C2(home.Id);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C2_mike_between(string start, string end)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C2_between(home.Id, start, end);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region calendar 3

        public JsonResult getEvents_C3_mike()
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C3(home.Id);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C3_mike_between(string start, string end)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C3_between(home.Id, start, end);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region calendar 4

        public JsonResult getEvents_C4_mike()
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = new QolaMVC.WebAPI.ActivityCalendarController().Get_C4(home.Id);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEvents_C4_mike_between(string start, string end)
        {
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("Home");
            ViewBag.Home = home;
            var l_ActivityEvents = HomeDAL.GetActivityEvents_C4_between(home.Id, start, end);

            List<Dictionary<string, string>> l_Events = HomeDAL.addEVENTStol_Events(l_ActivityEvents);


            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        [HttpPost]
        public void btnPdf_Click()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Collection<ProgressNotesModel> progressNotes;
            int residentId = 0;
            string[] arrSuiteNo;
            string exception = string.Empty;
            int iSno = 1;
            int selectedUserType = 0; //Convert.ToInt32(hdnUserType.Value);
            int selectedCategory = 0;// Convert.ToInt32(hdnCategory.Value);
            try
            {
                //arrSuiteNo = Request.Form["lblResident"].Split(',');
                ResidentModel l_Resident = (ResidentModel)TempData["Resident"];
                //s_FromDate = txtFromDate.Text;
                //s_ToDate = txtToDate.Text;
                Document doc = new Document(PageSize.A4, 30f, 30f, 77f, 20f);
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, mStream);

                writer.PageEvent = new PDFFooter();
                doc.Open();
                iTextSharp.text.Font tableFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font fontBoldText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font fontBoldHeadText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font fontCellHeader = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font font6B = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 7f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                PdfPTable tblProgress = new PdfPTable(7);
                tblProgress.WidthPercentage = 100f;

                float[] wthProgress = new float[] { 0.51f, 1.2f, 0.8f, 1f, 3f, 1.5f, 1.2f };
                tblProgress.SetWidths(wthProgress);
                tblProgress.SpacingAfter = 5f;

                PdfPTable tblNote = new PdfPTable(1);
                tblNote.WidthPercentage = 100f;
                float[] wthNote = new float[] { 1f };
                tblNote.SetWidths(wthNote);
                try
                {
                    progressNotes = new Collection<ProgressNotesModel>();
                    if (l_Resident != null)
                    {
                        DateTime fromDate = QolaCulture.stringToDateFormat(Request.Form["From"].ToString());
                        DateTime toDate = QolaCulture.stringToDateFormat(Request.Form["To"].ToString());
                        residentId = l_Resident.ID;
                        progressNotes = ProgressNotesDAL.GetProgressNotesCollections(residentId, fromDate, toDate, "R", selectedUserType, selectedCategory);
                    }
                    if (progressNotes != null)
                    {
                        foreach (ProgressNotesModel progressNote in progressNotes)
                        {
                            PdfPCell snoCell = new PdfPCell(new Phrase(iSno.ToString(), tableFont));
                            snoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblProgress.AddCell(snoCell);
                            PdfPCell dateCell = new PdfPCell(new Phrase(QolaCulture.dateToUSDateStringFormat(progressNote.Date), tableFont));
                            dateCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblProgress.AddCell(dateCell);
                            string times = QolaCulture.dateToUSTimeStringFormat(progressNote.Date, "HH:mm");

                            PdfPCell timeCell = new PdfPCell(new Phrase(times, tableFont));
                            timeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tblProgress.AddCell(timeCell);

                            var parsedHtmlElements = HTMLWorker.ParseToList(new System.IO.StringReader("<span style='font-size: 9pt;'>" + progressNote.Title + "</span><br/>" + "<span style='font-size: 9pt;'>" + progressNote.Note + "</span>"), null);
                            if (progressNote.Category == 6)
                            {
                                PdfPCell fallDateCell = new PdfPCell(new Phrase("Date" + ": " + QolaCulture.dateToUSDateStringFormat(progressNote.FallDate) + "\n", tableFont));
                                fallDateCell.Border = 0;
                                tblNote.AddCell(fallDateCell);
                                PdfPCell timeFallCell = new PdfPCell(new Phrase("Time" + ": " + QolaCulture.dateToUSDateStringFormat(progressNote.FallDate, "hh:mm tt") + "\n", tableFont));
                                timeFallCell.Border = 0;
                                tblNote.AddCell(timeFallCell);
                                PdfPCell locationCell = new PdfPCell(new Phrase("ProgLocation" + ": " + progressNote.Location + "\n", tableFont));
                                locationCell.Border = 0;
                                tblNote.AddCell(locationCell);
                                if (progressNote.WitnessFall != "")
                                {
                                    PdfPCell witnessCell = new PdfPCell(new Phrase("Witnessed Fall: " + progressNote.WitnessFall + "\n\n", tableFont));
                                    witnessCell.Border = 0;
                                    tblNote.AddCell(witnessCell);
                                }
                                if (progressNote.UnWitnessType != 'Y')
                                {
                                    PdfPCell witnessCell = new PdfPCell(new Phrase("Unwitnessed " + "\n\n", tableFont));
                                    witnessCell.Border = 0;
                                    tblNote.AddCell(witnessCell);
                                }
                            }
                            foreach (var htmlElement in parsedHtmlElements)
                            {
                                if (htmlElement.Chunks.Count > 0)
                                {
                                    PdfPCell Note = new PdfPCell(new Phrase((htmlElement as Phrase)));
                                    Note.Border = 0;
                                    tblNote.AddCell(Note);
                                }
                            }
                            PdfPCell categoryCell = new PdfPCell(new Phrase(ProgressNotesHelper.GetCategory(progressNote.Category), tableFont));
                            categoryCell.PaddingBottom = 5f;
                            categoryCell.PaddingLeft = 1f;
                            tblProgress.AddCell(categoryCell);

                            PdfPCell titleCell = new PdfPCell(new PdfPTable(tblNote));
                            titleCell.PaddingBottom = 5f;
                            titleCell.PaddingLeft = 1f;
                            tblProgress.AddCell(titleCell);
                            tblNote.Rows.Clear();


                            if (progressNote.ACkFlag == 'N')
                            {
                                Phrase userType = new Phrase(progressNote.ModifiedBy.FirstName + " " + progressNote.ModifiedBy.LastName + "\n(", tableFont);
                                userType.Add(new Chunk(progressNote.ModifiedBy.UserTypeName, font6B));
                                userType.Add(new Chunk(")", font6B));
                                PdfPCell userCell = new PdfPCell(userType);
                                tblProgress.AddCell(userCell);
                            }

                            else if (progressNote.ACkFlag == 'A')
                            {
                                Phrase ackUserType = new Phrase();
                                ackUserType.Add(new Chunk(progressNote.AcknowledgedBy.FirstName + " " + progressNote.AcknowledgedBy.LastName, tableFont));
                                ackUserType.Add(new Chunk("\n(" + progressNote.AcknowledgedBy.UserTypeName, font6B));
                                ackUserType.Add(new Chunk(")", font6B));

                                PdfPCell AckUserCell = new PdfPCell(ackUserType);
                                tblProgress.AddCell(AckUserCell);
                            }
                            else
                            {
                                tblProgress.AddCell(new PdfPCell());
                            }
                            PdfPCell signatureCell = new PdfPCell(new Phrase("", tableFont));
                            tblProgress.AddCell(signatureCell);

                            iSno += 1;
                        }
                    }
                    doc.Add(tblProgress);
                    string reportName = string.Empty;
                    if (Session["ResidentStatus"] != null && Session["ResidentStatus"].ToString() == "A")
                    {
                        reportName = l_Resident.FirstName + "_" + l_Resident.LastName + "_" + DateTime.Now.ToString("yyyyMMdd");
                    }
                    else
                    {
                        reportName = "ProgressNotes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    }
                    doc.Close();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + reportName + ".pdf");
                    Response.Clear();
                    Response.BinaryWrite(mStream.ToArray());
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                catch (System.Threading.ThreadAbortException lException)
                {
                    throw (lException);
                }
                catch (Exception Ex)
                {
                    exception = "frmProgressNotes btnPdf_Click" + Ex.Message.ToString();
                    //Log.Write(exception);

                }
            }
            catch (DocumentException dex)
            {
                throw (dex);
            }
            catch (IOException ioex)
            {
                throw (ioex);
            }
        }

        public ActionResult EmergencyList()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            
            TempData.Keep("User");
            TempData.Keep("Home");
            
            ViewBag.User = user;
            ViewBag.Home = home;
            
            var ds = new DataSet();
            ds = ResidentsDAL.GetEmergencyResidentDetails(home.Id, "0");
            return View(ds);
        }

        public void btnPdf_Click_EmergencyList()
        {
            string sException = string.Empty;
            int iHomeId = 0;
            DataSet ds = null;
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            //Int32.TryParse(Session["HomeId"].ToString(), out iHomeId
            try
            {
                if (home != null)
                {
                    iHomeId = home.Id;
                    ds = new DataSet();
                    ds = ResidentsDAL.GetEmergencyResidentDetails(iHomeId, "0");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Document doc = new Document(PageSize.A4, 30f, 30f, 40f, 20f);
                        doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(doc, mStream);


                        writer.PageEvent = new pdfHeaderFooter(home.Name);

                        doc.Open();

                        iTextSharp.text.Font fontCellSize12 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize12B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize11B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize10B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize9B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize8B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        iTextSharp.text.Font fontCellSize7B = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                        Paragraph paragraph = new Paragraph("EmergencyResidentDetails", fontCellSize11B);
                        paragraph.Alignment = Element.TITLE;



                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string sReason = string.Empty;
                            string sReasonValue = string.Empty;

                            PdfPTable PdfTable1;

                            PdfTable1 = new PdfPTable(10);
                            float[] wthtbl1 = new float[] { .8f, 3f, 10f, 5f, 8f, 10f, 8f, .8f, 7.9f, 10f };
                            PdfTable1.SetWidths(wthtbl1);
                            PdfTable1.WidthPercentage = 100f;
                            PdfTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            PdfTable1.SpacingAfter = 5f;
                            PdfTable1.SpacingBefore = 5f;

                            PdfPCell cell = new PdfPCell(new Phrase("EmergencyResidentDetails", fontCellSize11B));
                            cell.Border = 0;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Colspan = 10;
                            cell.PaddingBottom = 10f;
                            PdfTable1.AddCell(cell);

                            PdfPCell PdfTable1HeaderSuitNo = new PdfPCell(new Phrase("SuiteNo", fontCellSize9B));
                            PdfTable1HeaderSuitNo.Colspan = 2;
                            PdfTable1HeaderSuitNo.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderSuitNo);

                            PdfPCell PdfTable1HeaderName = new PdfPCell(new Phrase("Name", fontCellSize9B));
                            PdfTable1HeaderName.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderName);

                            PdfPCell PdfTable1HeaderGender = new PdfPCell(new Phrase("Gender", fontCellSize9B));
                            PdfTable1HeaderGender.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderGender);

                            PdfPCell PdfTable1HeaderPhone = new PdfPCell(new Phrase("Phone", fontCellSize9B));
                            PdfTable1HeaderPhone.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderPhone);

                            PdfPCell PdfTable1HeaderContactPerson = new PdfPCell(new Phrase("ContactPerson", fontCellSize9B));
                            PdfTable1HeaderContactPerson.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderContactPerson);

                            PdfPCell PdfTable1HeaderEmergencyContact = new PdfPCell(new Phrase("EmergencyContact", fontCellSize9B));
                            PdfTable1HeaderEmergencyContact.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderEmergencyContact);

                            PdfPCell PdfTable1HeaderMobilty = new PdfPCell(new Phrase("Mobility", fontCellSize9B));
                            PdfTable1HeaderMobilty.Colspan = 2;
                            PdfTable1HeaderMobilty.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderMobilty);

                            PdfPCell PdfTable1HeaderComment = new PdfPCell(new Phrase("Comments", fontCellSize9B));
                            PdfTable1HeaderComment.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfTable1.AddCell(PdfTable1HeaderComment);

                            if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))
                            {

                                int ActualOrder = 1; // ddlResidentCareAssessment.SelectedValue == "H" ? 1 : ddlResidentCareAssessment.SelectedValue == "M" ? 2 : ddlResidentCareAssessment.SelectedValue == "L" ? 3 : ddlResidentCareAssessment.SelectedValue == "N" ? 4 : 0;
                                for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                                {

                                    sReasonValue = string.Empty;
                                    sReason = string.Empty;
                                    sReason = sReason + ds.Tables[0].Rows[iRow]["fd_SA_vision"] + ds.Tables[0].Rows[iRow]["fd_mobility"] + ds.Tables[0].Rows[iRow]["fd_SA_hearing"] + ds.Tables[0].Rows[iRow]["fd_cognitive_function"] + ds.Tables[0].Rows[iRow]["fd_risk_level"].ToString() + ",";
                                    if (sReason != string.Empty && sReason != null)
                                    {
                                        sReason = sReason.Remove(sReason.Length - 1);
                                        string sResult;
                                        string[] sValue = sReason.Split(',');
                                        foreach (string word in sValue)
                                        {
                                            if (word == "0MA" || word == "0MD" || word == "0MI" || word == "0MS")
                                            {
                                                GetMobilityValue(word);
                                            }
                                            sResult = Get_bindResidentCareAssessment(word, "");
                                            if (sResult != string.Empty && sResult != null)
                                            {
                                                if (sReasonValue.Length > 0)
                                                {
                                                    sReasonValue = sReasonValue + ',' + ' ' + Get_bindResidentCareAssessment(word, "W");
                                                }
                                                else
                                                {
                                                    sReasonValue = Get_bindResidentCareAssessment(word, "W");
                                                }
                                            }

                                            sResult = string.Empty;
                                        }
                                    }
                                    int order = Convert.ToInt32(ds.Tables[0].Rows[iRow]["fd_risk_order"]);
                                    int AcceptedOrder = 0;
                                    if (ActualOrder > 0)
                                    {
                                        AcceptedOrder = _colorCode == "R" || order == 1 ? 1 : _colorCode == "Y" || order == 2 ? 2 : _colorCode == "G" ? 3 : 4;
                                        if (AcceptedOrder != ActualOrder)
                                        {
                                            _colorCode = "W";
                                            _mobiltySelectedValue = string.Empty;
                                            continue;
                                        }
                                    }
                                    PdfPCell suiteColorCell = new PdfPCell();
                                    string level = string.Empty;
                                    switch (order)
                                    {
                                        case 1:
                                            level = "HighFallRisk";
                                            break;
                                        case 2:
                                            level = "MediumFallingRisk";
                                            break;
                                        case 3:
                                            level = "LowFallingRisk";
                                            break;
                                        default:
                                            level = string.Empty;
                                            break;
                                    }
                                    if (_colorCode == "R" || order == 1)
                                    {
                                        suiteColorCell.BackgroundColor = BaseColor.RED;
                                    }
                                    else if (_colorCode == "Y" || order == 2)
                                    {
                                        suiteColorCell.BackgroundColor = BaseColor.YELLOW;
                                    }
                                    else if (_colorCode == "G")
                                    {
                                        suiteColorCell.BackgroundColor = BaseColor.GREEN;
                                    }
                                    PdfTable1.AddCell(suiteColorCell);
                                    _colorCode = "W";
                                    PdfPCell PdfTable1SuitNoValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_suite_no"].ToString(), fontCellSize9));
                                    PdfTable1SuitNoValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1SuitNoValue);

                                    PdfPCell PdfTable1NameValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_first_name"] + " " + ds.Tables[0].Rows[iRow]["fd_last_name"], fontCellSize9));
                                    PdfTable1NameValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1NameValue);

                                    PdfPCell PdfTable1GenderValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_gender"].ToString(), fontCellSize9));
                                    PdfTable1GenderValue.HorizontalAlignment = Element.ALIGN_CENTER;
                                    PdfTable1.AddCell(PdfTable1GenderValue);

                                    string phone = ds.Tables[0].Rows[iRow]["fd_phone"].ToString().Trim().Length > 0 ? ds.Tables[0].Rows[iRow]["fd_phone"].ToString() : "NoPhone";
                                    PdfPCell PdfTable1AttendeesValue = new PdfPCell(new Phrase(phone, fontCellSize9));
                                    PdfTable1AttendeesValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1AttendeesValue);

                                    PdfPCell PdfTable1PhoneValue = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_contact_1"].ToString(), fontCellSize9));
                                    PdfTable1PhoneValue.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1PhoneValue);

                                    PdfPCell PdfTable1EmergencyContact = new PdfPCell(new Phrase(ds.Tables[0].Rows[iRow]["fd_home_phone_1"].ToString(), fontCellSize9));
                                    PdfTable1EmergencyContact.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1EmergencyContact);

                                    PdfTable1.AddCell(suiteColorCell);



                                    PdfPCell PdfTable1Mobility = new PdfPCell(new Phrase(_mobiltySelectedValue, fontCellSize9B));
                                    PdfTable1Mobility.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1Mobility);
                                    _mobiltySelectedValue = string.Empty;

                                    PdfPCell PdfTable1Comment = new PdfPCell(new Phrase(level + "," + sReasonValue.Replace("<b>", string.Empty).Replace("</b>", string.Empty), fontCellSize9));
                                    PdfTable1Comment.HorizontalAlignment = Element.ALIGN_LEFT;
                                    PdfTable1.AddCell(PdfTable1Comment);
                                };
                            }
                            else
                            {
                                PrintCarePlanEmergencyDetails(ds, fontCellSize9B, fontCellSize9, PdfTable1);
                            }
                            doc.Add(PdfTable1);
                        }

                        doc.Close();
                        string reportName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Emergency_Resident_Details_" + reportName + ".pdf");
                        Response.Clear();
                        Response.BinaryWrite(mStream.ToArray());
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                }
            }
            catch (Exception Ex)
            {
                sException = "frmEmergencyResidentDetails btnPdf_Click |" + Ex.Message.ToString();
                //Log.Write(sException);
               
            }
        }



        private string Get_bindResidentCareAssessment(string sCareAssessment, string colorCode)
        {
            string[] redColor = { "5HS", "6HA", "0MA", "0MD", "2WS", "8CS", "9CA", "2VI", "3VB", "3HD", "6CI" };
            string[] yellowColor = { "4HI", "22HM", "23HE", "3WA", "1WI", "0MS", "7CI", "7CG", "2HI" };
            string[] greenColor = { "8VIL", "9VIR", "6VL", "7VR", "9HM", "10HP", "11HE", "12HA" };

            string sValue = string.Empty;
            DataTable dt = new DataTable();
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            string colorValue = "W";
            dt.Columns.Add("CareAssessmentValue", typeof(string));
            dt.Columns.Add("CareAssessment", typeof(string));
            dt.Rows.Add("<b>" + "Mobility" + "</b>(Assistance)", "0MA");
            dt.Rows.Add("<b>Mobility </b>(Dependent)", "0MD");
            dt.Rows.Add("<b>Mobility</b>(Supervision)", "0MS");
            dt.Rows.Add("<b>Mobility</b>(Independent)", "0MI");
            dt.Rows.Add("<b>Walker</b>(Independent)", "1WI");
            dt.Rows.Add("<b>Walker</b>(Supervised)", "2WS");
            dt.Rows.Add("<b>Walker</b>(Assisted)", "3WA");
            dt.Rows.Add("<b>Wheelchair</b>(Independent)", "4HI");
            dt.Rows.Add("<b>Wheelchair</b>(Supervised)", "5HS");
            dt.Rows.Add("<b>Wheelchair</b>(Assisted)", "6HA");
            dt.Rows.Add("<b>Wheelchair</b>(Manual)", "22HM");
            dt.Rows.Add("<b>Wheelchair</b>(Electric)", "23HE");
            dt.Rows.Add("<b>Cane</b>(Independent)", "7CI");
            dt.Rows.Add("<b>Cane</b>(Supervised)", "8CS");
            dt.Rows.Add("<b>Cane</b>(Assisted)", "9CA");
            dt.Rows.Add("<b>SensoryAbilities</b>(Impaired)", "2VI");
            dt.Rows.Add("<b>SensoryAbilities</b>(ImpairedLeft)", "8VIL");
            dt.Rows.Add("<b>SensoryAbilities</b>(ImpairedRight)", "9VIR");
            dt.Rows.Add("<b>SensoryAbilities</b>(Blind)", "3VB");
            dt.Rows.Add("<b>SensoryAbilities</b>(BlindLeft)", "6VL");
            dt.Rows.Add("<b>SensoryAbilities</b>(BlindRight)", "7VR");
            dt.Rows.Add("<b>Hearing</b>(Impaired)", "2HI");
            dt.Rows.Add("<b>Hearing</b>(ImpairedLeft)", "9HM");
            dt.Rows.Add("<b>Hearing</b>(ImpairedRight)", "10HP");
            dt.Rows.Add("<b>Hearing</b>(Deaf)", "3HD");
            dt.Rows.Add("<b>Hearing</b>(DeafLeft)", "11HE");
            dt.Rows.Add("<b>Hearing</b>(DeafRight)", "12HA");
            dt.Rows.Add("<b>Cognitivefunction</b>(Confused)", "4CC");
            dt.Rows.Add("<b>Cognitivefunction</b>(Shorttermloss)", "5CS");
            dt.Rows.Add("<b>Cognitivefunction</b>(Significantimpairment)", "6CI");
            dt.Rows.Add("<b>Cognitivefunction</b>(Cuing)", "7CG");


            bool isAffected = false;
            foreach (DataRow Drow in dt.Rows)
            {
                if (Drow["CareAssessment"].ToString() == sCareAssessment)
                {
                    sValue = Drow["CareAssessmentValue"].ToString();
                    colorValue = Drow["CareAssessment"].ToString();
                    if (isAffected == false)
                    {
                        foreach (string red in redColor)
                        {
                            if (red == colorValue)
                            {

                                _colorCode = "R";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if (isAffected == false && _colorCode != "R")
                    {
                        foreach (string yellow in yellowColor)
                        {
                            if (yellow == colorValue)
                            {

                                _colorCode = "Y";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if (isAffected == false && _colorCode != "R" && _colorCode != "G")
                    {
                        foreach (string green in greenColor)
                        {
                            if (green == colorValue)
                            {
                                _colorCode = "G";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    break;
                }
                isAffected = false;
            }
            return sValue;
        }

        protected void ddlResidentCareAssessment_Change(object sender, EventArgs e)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            int homeId = home.Id;
            string exception = string.Empty;
            DataSet ds = null;
            string sTick = string.Empty;
            string sUntick = string.Empty;
            bool bDataFlag = false;
            string sReason = string.Empty;
            string sReasonValue = string.Empty;
            string color = "W";
            try
            {
                int ActualOrder = 1;// ddlResidentCareAssessment.SelectedValue == "H" ? 1 : ddlResidentCareAssessment.SelectedValue == "M" ? 2 : ddlResidentCareAssessment.SelectedValue == "L" ? 3 : ddlResidentCareAssessment.SelectedValue == "N" ? 4 : 0;
                System.Text.StringBuilder sbReport = new System.Text.StringBuilder();
                ds = new DataSet();
                ds = ResidentsDAL.GetEmergencyResidentDetails(homeId, "0");
               // divEmergencyResidentDetails.Visible = true;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sbReport.Append("<div> <script src='JS/jquery-ui.min.js' type='text/javascript'></script><script src='JS/tableSorting.js' type='text/javascript'></script><table width='100%' class='tableStatic' id='tblSort'><thead><th  style='width:.1px;' data-sort='string'>RiskLevel</th><th style='width:10px;' data-sort='int'>SuiteNo </th><th style='width:25px;' data-sort='string' class=''>Name </th><th style='text-align: center;width:10px;'data-sort='string'  class=''>Gender </th><th style='width:10px;'data-sort='string'  class=''>Phone </th> <th style='width:10px;' data-sort='string' class=''>ContactPerson </th> <th style='width:15px;' data-sort='string'  class=''>EmergencyContact </th><th style='width:.1px;' data-sort='string'>RiskLevel </th><th style='width:15px;'data-sort='string'  class=''>Mobility </th> <th style='width:25px;' data-sort='string'>Comments </th> </thead><tbody>");
                        if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))
                        {
                            bDataFlag = true;
                            sbReport.Append(BindCarePlanEmergencyDetails(ds));
                            sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script></div>");
                        }
                        else
                        {
                            bDataFlag = true;

                            for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                            {
                                sReasonValue = string.Empty;
                                sReason = string.Empty;
                                sReason = sReason + ds.Tables[0].Rows[iRow]["fd_SA_vision"] + ds.Tables[0].Rows[iRow]["fd_mobility"] + ds.Tables[0].Rows[iRow]["fd_SA_hearing"] + ds.Tables[0].Rows[iRow]["fd_cognitive_function"] + ds.Tables[0].Rows[iRow]["fd_risk_level"].ToString() + ",";
                                if (sReason != string.Empty && sReason != null && !String.IsNullOrEmpty(sReason) && !String.IsNullOrWhiteSpace(sReason))
                                {
                                    sReason = sReason.Remove(sReason.Length - 1);
                                    string sResult;
                                    string[] sValue = sReason.Split(',');
                                    foreach (string word in sValue)
                                    {
                                        if (!String.IsNullOrEmpty(sReason) && !String.IsNullOrWhiteSpace(sReason))
                                        {
                                            if (word == "0MA" || word == "0MD" || word == "0MI" || word == "0MS")
                                            {
                                                GetMobilityValue(word);
                                            }
                                            sResult = Get_bindResidentCareAssessment(word, color);
                                            if (sResult != string.Empty && sResult != null)
                                            {
                                                if (sReasonValue.Length > 0)
                                                {
                                                    sReasonValue = sReasonValue + ',' + ' ' + Get_bindResidentCareAssessment(word, color);
                                                }
                                                else
                                                {
                                                    sReasonValue = Get_bindResidentCareAssessment(word, color);
                                                }
                                            }
                                        }
                                        sResult = string.Empty;
                                    }
                                }

                                int order = Convert.ToInt32(ds.Tables[0].Rows[iRow]["fd_risk_order"]);
                                int AcceptedOrder = 0;
                                if (ActualOrder > 0)
                                {
                                    AcceptedOrder = _colorCode == "R" || order == 1 ? 1 : _colorCode == "Y" || order == 2 ? 2 : _colorCode == "G" ? 3 : 4;
                                    if (AcceptedOrder != ActualOrder)
                                    {
                                        _colorCode = "W";
                                        _mobiltySelectedValue = string.Empty;
                                        continue;
                                    }
                                }

                                string phone = ds.Tables[0].Rows[iRow]["fd_phone"].ToString().Trim().Length > 0 ? ds.Tables[0].Rows[iRow]["fd_phone"].ToString() : "NoPhone";
                                string level = string.Empty;
                                int sortOrder = 1;
                                switch (order)
                                {
                                    case 1:
                                        level = "HighFallRisk";
                                        break;
                                    case 2:
                                        level = "MediumFallingRisk";
                                        break;
                                    case 3:
                                        level = "LowFallingRisk";
                                        break;
                                    default:
                                        level = string.Empty;
                                        break;
                                }

                                string colorQuery = "<td style='background-color:white; width:2px;'></td>";

                                if (_colorCode == "R" || order == 1)
                                {
                                    sortOrder = 4;
                                    colorQuery = "<td style='background-color:red;width:3px;'><span style='color:red;'>" + sortOrder + "</span></td>";
                                }
                                else if (_colorCode == "Y" || order == 2)
                                {
                                    sortOrder = 3;
                                    colorQuery = "<td style='background-color:yellow'><span style='color:yellow;'>" + sortOrder + "</span></td>";
                                }
                                else if (_colorCode == "G")
                                {
                                    sortOrder = 2;
                                    colorQuery = "<td style='background-color:green'><span style='color:green;'>" + sortOrder + "</span></td>";
                                }
                                string designString = "<tr style='text-align:left;'>" + colorQuery + "<td style='width:5px;'>" + ds.Tables[0].Rows[iRow]["fd_suite_no"] + "</td>";
                                designString += "<td>" + ds.Tables[0].Rows[iRow]["fd_first_name"] + " " + ds.Tables[0].Rows[iRow]["fd_last_name"] + "</td><td>"
                                    + ds.Tables[0].Rows[iRow]["fd_gender"] + "</td><td>" + phone + "</td><td>" + ds.Tables[0].Rows[iRow]["fd_contact_1"] + "</td><td>"
                                    + ds.Tables[0].Rows[iRow]["fd_home_phone_1"] + "</td>"
                                    + colorQuery
                                    + "<td style='width:10px;'><b>" + _mobiltySelectedValue + "</b></td><td><b>" + level + "</b>";
                                if (sReasonValue.Length > 0 && level.Length > 0)
                                {
                                    designString += "<b>, </b>";
                                }
                                designString += sReasonValue + "</td></tr>";
                                sbReport.Append(designString);
                                _colorCode = "W";
                                _mobiltySelectedValue = string.Empty;

                            }
                            sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script></div>");
                        }
                    }
                    else
                    {
                        sbReport.Append("<div><table width='100%' class='tableStatic'><tr><th>No Record</th></tr></table></div>");
                    }
                    if (!bDataFlag)
                    {
                        //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                    //divEmergencyResidentDetails.InnerHtml = sbReport.ToString();
                }
                else
                {
                    //AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                }
            }

            catch (Exception Ex)
            {
                exception = "frmEmergencyResidentDetails EmergencyResidentDetails |" + Ex.Message.ToString();
                //Log.Write(exception);
               
            }
        }

        protected void ddlResidentCareAssessment_Change_Old(object sender, EventArgs e)
        {
            int iHomeId = 0;
            string exception = string.Empty;
            DataSet ds = null;
            string sTick = string.Empty;
            string sUntick = string.Empty;
            bool bDataFlag = false;
            string sReason = string.Empty;
            string sReasonValue = string.Empty;

            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;


            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            try
            {
                if (home != null == true)
                {
                    iHomeId = home.Id;
                    int ActualOrder = 1; // ddlResidentCareAssessment.SelectedValue == "H" ? 1 : ddlResidentCareAssessment.SelectedValue == "M" ? 2 : ddlResidentCareAssessment.SelectedValue == "L" ? 3 : ddlResidentCareAssessment.SelectedValue == "N" ? 4 : 0;
                    System.Text.StringBuilder sbReport = new System.Text.StringBuilder();
                    ds = new DataSet();
                    ds = ResidentsDAL.GetEmergencyResidentDetails(iHomeId, "0");

                    //divEmergencyResidentDetails.Visible = true;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            sbReport.Append("<div> <script src='JS/jquery-ui.min.js' type='text/javascript'></script><script src='JS/tableSorting.js' type='text/javascript'></script><table width='100%' class='tableStatic' id='tblSort'><thead><th  style='width:.1px;' data-sort='string'>RiskLevel</th><th style='width:10px;' data-sort='int'>SuiteNo </th><th style='width:25px;' data-sort='string' class=''>Name </th><th style='text-align: center;width:10px;'data-sort='string'  class=''>Gender</th><th style='width:10px;'data-sort='string'  class=''>Phone </th> <th style='width:10px;' data-sort='string' class=''>ContactPerson </th> <th style='width:20px;' data-sort='string'  class=''>EmergencyContact </th><th style='width:.1px;' data-sort='string'>RiskLevel </th><th style='width:15px;'data-sort='string'  class=''>Mobility </th> <th style='width:25px;' data-sort='string'>Comments </th> </thead><tbody>");
                            if (Convert.ToInt32(Session["CarePlanP2HomeId"]) == Convert.ToInt32(Session["HomeId"]))
                            {
                                bDataFlag = true;
                                sbReport.Append(BindCarePlanEmergencyDetails(ds));
                                sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script></div>");
                            }
                            else
                            {

                                bDataFlag = true;

                                for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                                {
                                    sReasonValue = string.Empty;
                                    sReason = string.Empty;

                                    sReason = sReason + ds.Tables[0].Rows[iRow]["fd_SA_vision"] + ds.Tables[0].Rows[iRow]["fd_mobility"] + ds.Tables[0].Rows[iRow]["fd_SA_hearing"] + ds.Tables[0].Rows[iRow]["fd_cognitive_function"] + ds.Tables[0].Rows[iRow]["fd_risk_level"].ToString() + ",";

                                    if (sReason != string.Empty && sReason != null)
                                    {
                                        sReason = sReason.Remove(sReason.Length - 1);
                                        string sResult = "";
                                        string[] sValue = sReason.Split(',');
                                        foreach (string word in sValue)
                                        {

                                            if (word == "0MA" || word == "0MD" || word == "0MI" || word == "0MS")
                                            {
                                                GetMobilityValue(word);
                                            }
                                            sResult = Get_bindResidentCareAssessment(word, "W");


                                            if (sResult != string.Empty && sResult != null)
                                            {

                                                if (sReasonValue.Length > 0)
                                                {
                                                    sReasonValue = sReasonValue + ',' + ' ' + Get_bindResidentCareAssessment(word, _colorCode);
                                                }
                                                else
                                                {
                                                    sReasonValue = Get_bindResidentCareAssessment(word, _colorCode);
                                                }

                                            }

                                            sResult = string.Empty;
                                        }
                                    }

                                    int order = Convert.ToInt32(ds.Tables[0].Rows[iRow]["fd_risk_order"]);
                                    int AcceptedOrder = 0;
                                    if (ActualOrder > 0)
                                    {
                                        AcceptedOrder = _colorCode == "R" || order == 1 ? 1 : _colorCode == "Y" || order == 2 ? 2 : _colorCode == "G" || order == 3 ? 3 : 4;
                                        if (AcceptedOrder != ActualOrder)
                                        {
                                            _colorCode = "W";
                                            _previousValue = _mobiltySelectedValue;
                                            _mobiltySelectedValue = string.Empty;
                                            continue;
                                        }
                                    }

                                    int sortOrder = 1;
                                    string phone = ds.Tables[0].Rows[iRow]["fd_phone"].ToString().Trim().Length > 0 ? ds.Tables[0].Rows[iRow]["fd_phone"].ToString() : "NoPhone";
                                    string colorQuery = "<td style='background-color:white; width:2px;' ></td>";
                                    string level = string.Empty;
                                    switch (order)
                                    {
                                        case 1:
                                            level = "HighFallRisk";
                                            break;
                                        case 2:
                                            level = "MediumFallingRisk";
                                            break;
                                        case 3:
                                            level = "LowFallingRisk";
                                            break;
                                        default:
                                            level = string.Empty;
                                            break;
                                    }
                                    if (_colorCode == "R" || order == 1)
                                    {
                                        sortOrder = 4;
                                        colorQuery = "<td style='background-color:red;width:3px;'><span style='color:red;'>" + sortOrder + "</span></td>";
                                    }
                                    else if (_colorCode == "Y" || order == 2)
                                    {
                                        sortOrder = 3;
                                        colorQuery = "<td style='background-color:yellow'><span style='color:yellow;'>" + sortOrder + "</span></td>";
                                    }
                                    else if (_colorCode == "G" || order == 3)
                                    {
                                        sortOrder = 2;
                                        colorQuery = "<td style='background-color:green'><span style='color:green;'>" + sortOrder + "</span></td>";
                                    }
                                    else if (order == 3 && sReasonValue == "")
                                    {
                                        sortOrder = 1;
                                        colorQuery = "<td style='background-color:white; width:2px;'></td>";
                                    }
                                    string designString = "<tr style='text-align:left;'>" + colorQuery + "<td style='width:5px;'>" + ds.Tables[0].Rows[iRow]["fd_suite_no"] + "</td>";
                                    designString += "<td>" + ds.Tables[0].Rows[iRow]["fd_first_name"] + " " + ds.Tables[0].Rows[iRow]["fd_last_name"] + "</td><td>"
                                        + ds.Tables[0].Rows[iRow]["fd_gender"] + "</td><td>" + phone + "</td><td>" + ds.Tables[0].Rows[iRow]["fd_contact_1"] + "</td><td>"
                                        + ds.Tables[0].Rows[iRow]["fd_home_phone_1"] + "</td>"
                                        + colorQuery
                                        + "<td style='width:10px;'><b>" + _mobiltySelectedValue + "</b></td><td>" + "<b>" + level + "</b>";
                                    if (sReasonValue.Length > 0 && level.Length > 0)
                                    {
                                        designString += "<b>, </b>";
                                    }
                                    designString += sReasonValue + "</td></tr>";
                                    sbReport.Append(designString);
                                    _colorCode = "W";
                                    _previousValue = _mobiltySelectedValue;
                                    _mobiltySelectedValue = string.Empty;
                                }
                                string style = "<style type='text/css'>th[data-sort]{cursor:pointer;}</style>";
                                sbReport.Append("</tbody></table><script>$(function  () {$('#tblSort').stupidtable();});</script>" + style + "</div>");
                            }
                        }
                        else
                        {
                            sbReport.Append("<div><table width='100%' class='tableStatic'><tr><th>No Record</th></tr></table></div>");
                        }
                        if (!bDataFlag)
                        {
                           // AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                        }
                        //divEmergencyResidentDetails.InnerHtml = sbReport.ToString();
                    }
                    else
                    {
                        //divEmergencyResidentDetails.InnerHtml = sbReport.ToString();
                       // AlertMessage.ShowErrorMsg(this.Page, Resources.Qola.CustomMessages.NoRecord, Resources.Qola.UIverbiage.EmergencyResidentDetails);
                    }
                }
            }

            catch (Exception Ex)
            {
                exception = "frmEmergencyResidentDetails EmergencyResidentDetails |" + Ex.Message.ToString();
               // Log.Write(exception);
               
            }

        }

        private string GetMobilityValue(string dataValue)
        {
            switch (dataValue)
            {
                case "0MI":
                    if (_mobiltySelectedValue == "Assisted" || _mobiltySelectedValue == "Dependent" ||
                          _mobiltySelectedValue == "Supervised")
                    {
                        _mobiltySelectedValue = _previousValue;
                    }
                    else
                    {
                        _mobiltySelectedValue = "Independent";
                        _previousValue = _mobiltySelectedValue;
                    }
                    break;
                case "0MS":
                    if (_mobiltySelectedValue == "Assisted" || _mobiltySelectedValue == "Dependent")
                    {
                        _previousValue = _mobiltySelectedValue;
                    }
                    else
                    {
                        _mobiltySelectedValue = "Supervised";
                        _previousValue = _mobiltySelectedValue;
                    }
                    break;
                case "0MA":
                    _mobiltySelectedValue = "Assisted";
                    _previousValue = _mobiltySelectedValue;
                    break;
                case "0MD":
                    _mobiltySelectedValue = "Dependent";
                    _previousValue = _mobiltySelectedValue;
                    break;
                default:
                    if (_mobiltySelectedValue != string.Empty)
                    {
                        _mobiltySelectedValue = string.Empty;
                    }
                    break;
            }

            return _mobiltySelectedValue;
        }

        private void GetCategoryValue()
        {
            string[] redColor = { "14.4", "11.7", "11.6", "8.2", "7.6", "6.6", "6.5" };
            string[] yellowColor = { "14.3", "11.5", "11.4", "7.3", "6.3" };
            string[] greenColor = { "14.2", "11.3", "11.2", "7.2", "6.2" };
            string exception = string.Empty;
            try
            {
                ArrayList list = new ArrayList();
                list.Add(new System.Web.UI.WebControls.ListItem("6.1" + "-Aucune intervention", "6.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.2" + "-Légère vérification avec ou sans adaptation", "6.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.3" + "-Encadrer, rappeler, stimuler, surveiller", "6.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.5" + "-Accompagner un usager présentant un risque ou une difficulté à  faire ses transferts", "6.5"));
                list.Add(new System.Web.UI.WebControls.ListItem("6.6" + "-Procéder aux transferts d’un usager présentant un risque ou une difficulté", "6.6"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.1" + "-Aucune intervention", "7.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.2" + "-Légère vérification avec ou sans adaptation", "7.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.3" + "-Encadrer, rappeler, stimuler, surveiller", "7.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("7.5" + "-Accompagner un usager présentant un risque ou une difficulté dans  ses déplacements", "7.5"));
                list.Add(new System.Web.UI.WebControls.ListItem("8.1" + "-Aucune intervention", "8.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("8.2" + "-En cas d’urgence", "8.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.1" + "-Aucune intervention", "11.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.2" + "-Légère vérification avec ou sans adaptation", "11.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.3" + "-Aider, conseiller, encadrer, prévenir, rappeler, sensibiliser", "11.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.4" + "-Favoriser la sollicitation", "11.4"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.5" + "-Apprendre à l’usager à développer de meilleures habiletés sociales et de résolution de problème", "11.5"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.6" + "-Accompagner a un usager présentant un risque ou une difficulté à développer de meilleures habiletés qui et de résolution de problème", "11.6"));
                list.Add(new System.Web.UI.WebControls.ListItem("11.7" + "-Contrôler les troubles relationnels de l’usager", "11.7"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.1" + "-Aucune intervention", "14.1"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.2" + "-Légère vérification avec ou sans adaptation de l’usager dans ses activités de la vie domestique", "14.2"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.3" + "-Aider, conseiller, encadrer, favoriser, rappeler, stimuler, surveiller, vérifier", "14.3"));
                list.Add(new System.Web.UI.WebControls.ListItem("14.4" + "-Accompagner l’usager dans ses activités de la vie domestique", "14.4"));

                list = new ArrayList();
                list.Add(new System.Web.UI.WebControls.ListItem("HighRisk", "H"));
                list.Add(new System.Web.UI.WebControls.ListItem("ModerateRisk", "M"));
                list.Add(new System.Web.UI.WebControls.ListItem("LowRisk", "L"));
                list.Add(new System.Web.UI.WebControls.ListItem("NoRisk", "N"));

                //ddlResidentCareAssessment.DataSource = list;
                //ddlResidentCareAssessment.DataTextField = "Text";
                //ddlResidentCareAssessment.DataValueField = "Value";
                //ddlResidentCareAssessment.DataBind();
                //ddlResidentCareAssessment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- ALL --", "0"));
            }
            catch (Exception Ex)
            {
                exception = "EmergencyResidentDetails GetCategoryValue |" + Ex.Message.ToString();
              //  Log.Write(exception);
               
            }
            finally
            {

            }
        }

        private string Get_bindResidentCareAssessment_CarePlan(string sCareAssessment, string colorCode)
        {
            string[] redColor = { "14.4", "11.7", "11.6", "8.2", "7.5", "6.6", "6.5" };
            string[] yellowColor = { "14.3", "11.5", "11.4", "7.3", "6.3" };
            string[] greenColor = { "14.2", "11.3", "11.2", "7.2", "6.2" };

            string sValue = string.Empty;
            DataTable dt = new DataTable();
            string colorValue = "W";
            dt.Columns.Add("CareAssessmentValue", typeof(string));
            dt.Columns.Add("CareAssessment", typeof(string));
            dt.Rows.Add("<b>14.4 - </b>Accompagner l’usager dans ses activités de la vie domestique", "14.4");
            dt.Rows.Add("<b>11.7 - </b>Contrôler les troubles relationnels de l’usager", "11.7");
            dt.Rows.Add("<b>11.6 - </b>Accompagner a un usager présentant un risque ou une difficulté à développer de meilleures habiletés qui et de résolution de problème", "11.6");
            dt.Rows.Add("<b>8.2 - </b>En cas d’urgence", "8.2");
            dt.Rows.Add("<b>7.5 - </b>Accompagner un usager présentant un risque ou une difficulté dans  ses déplacements", "7.5");
            dt.Rows.Add("<b>6.6 - </b>Procéder aux transferts d’un usager présentant un risque ou une difficulté", "6.6");
            dt.Rows.Add("<b>6.5 - </b>Accompagner un usager présentant un risque ou une difficulté à  faire ses transferts", "6.5");
            dt.Rows.Add("<b>14.3 – </b>Aider, conseiller, encadrer, favoriser, rappeler, stimuler, surveiller, vérifier", "14.3");
            dt.Rows.Add("<b>11.5 – </b>Apprendre à l’usager à développer de meilleures habiletés sociales et de résolution de problème", "11.5");
            dt.Rows.Add("<b>11.4 - </b>Favoriser la sollicitation", "11.4");
            dt.Rows.Add("<b>7.3 - </b>Encadrer, rappeler, stimuler, surveiller", "7.3");
            dt.Rows.Add("<b>6.3 - </b>Encadrer, rappeler, stimuler, surveiller", "6.3");
            dt.Rows.Add("<b>14.2 – </b>Légère vérification avec ou sans adaptation de l’usager dans ses activités de la vie domestique", "14.2");
            dt.Rows.Add("<b>11.3 - </b>Aider, conseiller, encadrer, prévenir, rappeler, sensibiliser", "11.3");
            dt.Rows.Add("<b>11.2 - </b>Légère vérification avec ou sans adaptation", "11.2");
            dt.Rows.Add("<b>7.2 - </b>Légère vérification avec ou sans adaptation", "7.2");
            dt.Rows.Add("<b>6.2 - </b>Légère vérification avec ou sans adaptation", "6.2");
            dt.Rows.Add("<b>14.1 - </b>Aucune intervention", "14.1");
            dt.Rows.Add("<b>11.1 - </b>Aucune intervention", "11.1");
            dt.Rows.Add("<b>8.1 – </b>Aucune intervention", "8.1");
            dt.Rows.Add("<b>7.1 - </b>Aucune intervention", "7.1");
            dt.Rows.Add("<b>6.1 - </b>Aucune intervention", "6.1");



            bool isAffected = false;
            foreach (DataRow Drow in dt.Rows)
            {
                if (Drow["CareAssessment"].ToString() == sCareAssessment)
                {
                    sValue = Drow["CareAssessmentValue"].ToString();
                    colorValue = Drow["CareAssessment"].ToString();
                    if (isAffected == false)
                    {
                        foreach (string red in redColor)
                        {
                            if (red == colorValue)
                            {

                                _colorCode = "R";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if ((isAffected == false && _colorCode != "R") || (isAffected == true && _colorCode != "R"))
                    {
                        foreach (string yellow in yellowColor)
                        {
                            if (yellow == colorValue)
                            {

                                _colorCode = "Y";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    if (isAffected == false && _colorCode != "R" && _colorCode != "G")
                    {
                        foreach (string green in greenColor)
                        {
                            if (green == colorValue)
                            {
                                _colorCode = "G";
                                isAffected = true;
                                break;
                            }
                        }
                    }
                    break;
                }
                isAffected = false;
            }
            return sValue;
        }

        private string BindCarePlanEmergencyDetails(DataSet ds)
        {
            string query = string.Empty;
            var residentList = (from residentTable in ds.Tables[0].AsEnumerable()
                                select new
                                {
                                    ResId = residentTable.Field<Int32>("fd_resident_id"),
                                    RiskOrder = residentTable.Field<Int32>("fd_risk_order")
                                }).Distinct();

            if (residentList != null)
            {
                foreach (var residents in residentList)
                {
                    query += "<tr>";
                    var residentDetail = (from resident in ds.Tables[0].AsEnumerable()
                                          select new
                                          {
                                              RID = resident.Field<Int32>("fd_resident_id"),
                                              Suite = resident.Field<String>("fd_suite_no"),
                                              RFname = resident.Field<String>("fd_first_name"),
                                              RLname = resident.Field<String>("fd_last_name"),
                                              Gender = resident.Field<String>("fd_gender"),
                                              Phone = resident.Field<String>("fd_phone"),
                                              Contact = resident.Field<String>("fd_contact_1"),
                                              HomePhone = resident.Field<String>("fd_home_phone_1"),
                                              Code = resident.Field<String>("fd_subcategory_code"),
                                              CategoryName = resident.Field<String>("fd_category_name"),
                                              RLevel = resident.Field<String>("fd_risk_level")
                                          }).Where(x => x.RID == residents.ResId);
                    int count = 0;
                    string comment = "", residentcommon = string.Empty;
                    string colorCell = "";
                    string level = string.Empty;
                    int sortOrder = 1;
                    foreach (var objRes in residentDetail)
                    {
                        Get_bindResidentCareAssessment_CarePlan(objRes.Code, "");
                        if (count == 0)
                        {
                            switch (residents.RiskOrder)
                            {
                                case 1:
                                    level = "HighFallRisk";
                                    break;
                                case 2:
                                    level = "MediumFallingRisk";
                                    break;
                                case 3:
                                    level = "LowFallingRisk";
                                    break;
                                default:
                                    level = string.Empty;
                                    break;
                            }
                            residentcommon += "<td>" + objRes.Suite + "</td>";
                            residentcommon += "<td>" + objRes.RFname + " " + objRes.RLname + "</td>";
                            residentcommon += "<td>" + objRes.Gender + "</td>";
                            string phone = objRes.Phone.Trim().Length > 0 ? objRes.Phone : "NoPhone";
                            residentcommon += "<td>" + phone + "</td>";
                            residentcommon += "<td>" + objRes.Contact + "</td>";
                            residentcommon += "<td>" + objRes.HomePhone + "</td>";

                            if (level != "")
                            {
                                comment += "<b>" + level + "</b></br>";
                            }
                        }
                        if (objRes.Code != "" || objRes.CategoryName != "")
                        {
                            comment += "<b>" + objRes.Code + " - </b>" + objRes.CategoryName + "</br>";
                        }
                        count++;
                    }
                    if (_colorCode == "R" || residents.RiskOrder == 1)
                    {

                        colorCell = "<td style='background-color:red;width:3px;'><span style='color:red;'>" + sortOrder + "</span></td>";
                    }
                    else if (_colorCode == "Y" || residents.RiskOrder == 2)
                    {

                        colorCell = "<td style='background-color:yellow;width:3px;'><span style='color:yellow;'>" + sortOrder + "</span></td>";
                    }
                    else if (_colorCode == "G")
                    {

                        colorCell = "<td style='background-color:green;width:3px;'><span style='color:green;'>" + sortOrder + "</span></td>";
                    }
                    else
                    {
                        colorCell = "<td style='background-color:white;width:3px;'><span style='color:white;'>" + sortOrder + "</span></td>";
                    }
                    query += colorCell + residentcommon + colorCell + "<td></td><td>" + comment + "</td></tr>";
                    residentcommon = string.Empty;
                    _colorCode = "W";
                }
            }
            return query;
        }

        private void PrintCarePlanEmergencyDetails(DataSet ds, Font font8B, Font font8, PdfPTable table)
        {
            string query = string.Empty;
            var residentList = (from residentTable in ds.Tables[0].AsEnumerable()
                                select new
                                {
                                    ResId = residentTable.Field<Int32>("fd_resident_id"),
                                    RiskOrder = residentTable.Field<Int32>("fd_risk_order")
                                }).Distinct();
            if (residentList != null)
            {


                foreach (var residents in residentList)
                {
                    var residentDetail = (from resident in ds.Tables[0].AsEnumerable()
                                          select new
                                          {
                                              RID = resident.Field<Int32>("fd_resident_id"),
                                              Suite = resident.Field<String>("fd_suite_no"),
                                              RFname = resident.Field<String>("fd_first_name"),
                                              RLname = resident.Field<String>("fd_last_name"),
                                              Gender = resident.Field<String>("fd_gender"),
                                              Phone = resident.Field<String>("fd_phone"),
                                              Contact = resident.Field<String>("fd_contact_1"),
                                              HomePhone = resident.Field<String>("fd_home_phone_1"),
                                              Code = resident.Field<String>("fd_subcategory_code"),
                                              CategoryName = resident.Field<String>("fd_category_name"),
                                              RLevel = resident.Field<String>("fd_risk_level")
                                          }).Where(x => x.RID == residents.ResId);
                    string suiteNo = string.Empty;
                    string residentName = string.Empty;
                    string gender = string.Empty;
                    string phone = string.Empty;
                    string contact = string.Empty;
                    string homePhone = string.Empty;
                    string level = string.Empty;
                    int count = 0;

                    Phrase comment = new Phrase();
                    string residentcommon = string.Empty;
                    foreach (var objRes in residentDetail)
                    {
                        Get_bindResidentCareAssessment_CarePlan(objRes.Code, "");
                        if (count == 0)
                        {
                            switch (residents.RiskOrder)
                            {
                                case 1:
                                    level = "HighFallRisk";
                                    break;
                                case 2:
                                    level = "MediumFallingRisk";
                                    break;
                                case 3:
                                    level = "LowFallingRisk";
                                    break;
                                default:
                                    level = string.Empty;
                                    break;
                            }
                            suiteNo = objRes.Suite;
                            residentName = objRes.RFname + " " + objRes.RLname;
                            residentcommon += "<td>" + objRes.Gender + "</td>";
                            phone = objRes.Phone.Trim().Length > 0 ? objRes.Phone : "NoPhone";
                            contact = objRes.Contact;
                            homePhone = objRes.HomePhone;

                            if (level != "")
                            {
                                comment.Add(new Chunk(level + "\n", font8B));
                            }
                        }
                        if (objRes.Code != "" || objRes.CategoryName != "")
                        {
                            comment.Add(new Chunk(objRes.Code + " - ", font8B));
                            comment.Add(new Chunk(objRes.CategoryName + "\n", font8));

                        }
                        count++;
                    }
                    PdfPCell cellBG = new PdfPCell();
                    cellBG.BackgroundColor = BaseColor.WHITE;
                    if (_colorCode == "R" || residents.RiskOrder == 1)
                    {
                        cellBG.BackgroundColor = BaseColor.RED;
                    }
                    else if (_colorCode == "Y" || residents.RiskOrder == 2)
                    {
                        cellBG.BackgroundColor = BaseColor.YELLOW;
                    }
                    else if (_colorCode == "G")
                    {
                        cellBG.BackgroundColor = BaseColor.GREEN;
                    }

                    table.AddCell(cellBG);
                    table.AddCell(new PdfPCell(new Phrase(suiteNo, font8)));
                    table.AddCell(new PdfPCell(new Phrase(residentName, font8)));
                    table.AddCell(new PdfPCell(new Phrase(gender, font8)));
                    table.AddCell(new PdfPCell(new Phrase(phone, font8)));
                    table.AddCell(new PdfPCell(new Phrase(contact, font8)));
                    table.AddCell(new PdfPCell(new Phrase(homePhone, font8)));
                    table.AddCell(new PdfPCell(cellBG));
                    table.AddCell(new PdfPCell());

                    residentcommon = string.Empty;
                    _colorCode = "W";
                }

            }

        }



        #region Dining Attendance

        public ActionResult DiningAttendance(string datesel)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            Dining_Attendance_simple LIST_VIEW_RESIDENT = new Dining_Attendance_simple();

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Today);
                string[] genderinfo = HomeDAL.get_TR_info(home.Id, DateTime.Today);
                TempData["number_attendance_array"] = genderinfo;
            }
            else
            {
                TempData["datechoose"] = datesel;
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Parse(datesel));
                string[] genderinfo = HomeDAL.get_TR_info(home.Id, DateTime.Parse(datesel));
                TempData["number_attendance_array"] = genderinfo;
            }
            TempData["LIST_VIEW_RESIDENT"] = LIST_VIEW_RESIDENT;
            TempData.Keep("LIST_VIEW_RESIDENT");
            ViewBag.LIST_VIEW_RESIDENT = LIST_VIEW_RESIDENT;
            //DateTime lastSunday = DateTime.Now;
            //while (lastSunday.DayOfWeek != DayOfWeek.Sunday)
            //    lastSunday = lastSunday.AddDays(-1);
            //TempData["Sunday"] = lastSunday;

            return View(LIST_VIEW_RESIDENT);
        }

        [HttpPost]
        public int saveButton_Dining(string arr, int whichmeal, string datesel)
        {
            string meal="";
            if (whichmeal == 1)
                meal = "Breakfast";
            else if (whichmeal == 2)
                meal = "Lunch";
            else if (whichmeal == 3)
                meal = "Dinner";
            string arr_2 = arr.Replace("Taken", "option1").Replace("Refused", "option2").Replace("Hospital", "option3").Replace("Waiver", "option4").Replace("Away", "option5").Replace("Tray Complimentary", "option6");
            arr = arr.Replace("option1", "Taken").Replace("option2", "Refused").Replace("option3", "Hospital").Replace("option4", "Waiver").Replace("option5", "Away").Replace("option6", "Tray Complimentary");
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<string> Save_sum_array = new List<string>();
            List<string> Save_sum_array_original = new List<string>();
            Dictionary<string, string> Save_summary = new Dictionary<string, string>();
            if (arr_2.Length != 0)
            {
                Save_sum_array = arr_2.Substring(0, arr_2.Length - 1).Split(',').ToList();
                Save_sum_array_original = arr.Substring(0, arr.Length - 1).Split(',').ToList();
                for (int a = 0; a < Save_sum_array.Count(); a = a + 2)
                {
                    Dining_Attendance_functions.add_progress_note(int.Parse(Save_sum_array_original[a]), DateTime.Now, "Resident "+ Save_sum_array_original[a+1]+" "+meal, user.ID, DateTime.Now);

                    HomeDAL.AddPersonalCalendar(home.Id,int.Parse(Save_sum_array_original[a]), meal + " - <b>" + Save_sum_array_original[a + 1] + "</b> by Resident", user.ID);


                    if (Save_summary.ContainsKey(Save_sum_array[a + 1]) == false)
                    {
                        Save_summary.Add(Save_sum_array[a + 1], Save_sum_array[a]);
                    }
                    else
                        Save_summary[Save_sum_array[a + 1]] += "," + Save_sum_array[a];
                }
            }
            Dining_Attendance_functions.save_Button(home.Id, whichmeal, DateTime.Parse(datesel), Save_summary, user.ID, DateTime.Now);

                return 1;
        }

        [HttpGet]
        public StringBuilder Getting_Dining(int whichmeal, string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            StringBuilder returnstring=Dining_Attendance_functions.getting_LIST(whichmeal, DateTime.Parse(datesel),home.Id);
            return returnstring;

        }

        [HttpGet]
        public ActionResult View_List(int whichmeal, string datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            StringBuilder returnstring = Dining_Attendance_functions.getting_LIST(whichmeal, DateTime.Parse(datesel),home.Id);
            string[] viewlist = returnstring.ToString().Split(';');

            List<dynamic> l_Json = new List<dynamic>();

            for (var c = 0; c < viewlist.Length; c++)
            {
                var cplus = c + 1;
                for (var b = 0; b < viewlist[c].Split(',').Length; b++)
                {
                    if (viewlist[c].Split(',')[b] != "")
                    {
                        dynamic l_J = new System.Dynamic.ExpandoObject();
                        var resident = ResidentsDAL.GetResidentById(int.Parse(viewlist[c].Split(',')[b]));

                        l_J.Name = resident.FirstName + " " + resident.LastName;
                        l_J.residentid = viewlist[c].Split(',')[b];
                        l_J.gender = resident.Gendar;
                        if (c == 0)
                            l_J.action = "Taken";
                        else if (c == 1)
                            l_J.action = "Refused";
                        else if (c == 2)
                            l_J.action = "Hospital";
                        else if (c == 3)
                            l_J.action = "Waiver";
                        else if (c == 4)
                            l_J.action = "Away";
                        else if (c == 5)
                            l_J.action = "Tray Complimentary";

                        l_Json.Add(l_J);

                        //returnstring.Replace(viewlist[c].Split(',')[b], resident.FirstName + resident.LastName);
                    }
                }
            }

            //return returnstring;
            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public string disable_hospital(DateTime datesel)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            List<int> returnleaving = Dining_Attendance_functions.disable_hospital_list(datesel);
            string returnstring= string.Join(",", returnleaving.ToArray());
            return returnstring;
        }

        [HttpGet]
        public int click_progress_note(int residentid, string datesel, string note)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            int returnint = Dining_Attendance_functions.add_progress_note(residentid,DateTime.Now, note,user.ID,DateTime.Now);

            return returnint;
        }

        #endregion

        #region Activity Attendance

        public ActionResult ActivityCalendar(string datesel,string tab)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            Dining_Attendance_simple LIST_VIEW_RESIDENT = new Dining_Attendance_simple();

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Today);
                Collection<ActivityEventModel> l_Events = HomeDAL.GetActivityEvents_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events = l_Events;


                Collection<ActivityEventModel_Calendar2> l_Events_2 = HomeDAL.GetActivityEvents_Calendar2_mike(DateTime.Today.Date, home.Id);
                Collection<ActivityEventModel_Calendar3> l_Events_3 = HomeDAL.GetActivityEvents_Calendar3_mike(DateTime.Today.Date, home.Id);
                Collection<ActivityEventModel_Calendar4> l_Events_4 = HomeDAL.GetActivityEvents_Calendar4_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events2 = l_Events_2;
                ViewBag.Events3 = l_Events_3;
                ViewBag.Events4 = l_Events_4;
            }
            else
            {
                TempData["datechoose"] = datesel;
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Parse(datesel));
                Collection<ActivityEventModel> l_Events = HomeDAL.GetActivityEvents_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events = l_Events;


                Collection<ActivityEventModel_Calendar2> l_Events_2 = HomeDAL.GetActivityEvents_Calendar2_mike(DateTime.Parse(datesel).Date, home.Id);
                Collection<ActivityEventModel_Calendar3> l_Events_3 = HomeDAL.GetActivityEvents_Calendar3_mike(DateTime.Parse(datesel).Date, home.Id);
                Collection<ActivityEventModel_Calendar4> l_Events_4 = HomeDAL.GetActivityEvents_Calendar4_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events2 = l_Events_2;
                ViewBag.Events3 = l_Events_3;
                ViewBag.Events4 = l_Events_4;
            }
            TempData["LIST_VIEW_RESIDENT"] = LIST_VIEW_RESIDENT;
            TempData.Keep("LIST_VIEW_RESIDENT");
            ViewBag.LIST_VIEW_RESIDENT = LIST_VIEW_RESIDENT;

            TempData["tab"] = tab;

            return View(LIST_VIEW_RESIDENT);

        }

        public ActionResult ActivityCalendar3(string datesel, string tab)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];

            ViewBag.Message = TempData["Message"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            Dining_Attendance_simple LIST_VIEW_RESIDENT = new Dining_Attendance_simple();

            if (datesel == "" || datesel == null)
            {
                TempData["datechoose"] = DateTime.Now.ToString("MMMM dd, yyyy");
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Today);
                Collection<ActivityEventModel> l_Events = HomeDAL.GetActivityEvents_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events = l_Events;


                Collection<ActivityEventModel_Calendar2> l_Events_2 = HomeDAL.GetActivityEvents_Calendar2_mike(DateTime.Today.Date, home.Id);
                Collection<ActivityEventModel_Calendar3> l_Events_3 = HomeDAL.GetActivityEvents_Calendar3_mike(DateTime.Today.Date, home.Id);
                Collection<ActivityEventModel_Calendar4> l_Events_4 = HomeDAL.GetActivityEvents_Calendar4_mike(DateTime.Today.Date, home.Id);
                ViewBag.Events2 = l_Events_2;
                ViewBag.Events3 = l_Events_3;
                ViewBag.Events4 = l_Events_4;
            }
            else
            {
                TempData["datechoose"] = datesel;
                LIST_VIEW_RESIDENT = HomeDAL.get_list_resident(home.Id, DateTime.Parse(datesel));
                Collection<ActivityEventModel> l_Events = HomeDAL.GetActivityEvents_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events = l_Events;


                Collection<ActivityEventModel_Calendar2> l_Events_2 = HomeDAL.GetActivityEvents_Calendar2_mike(DateTime.Parse(datesel).Date, home.Id);
                Collection<ActivityEventModel_Calendar3> l_Events_3 = HomeDAL.GetActivityEvents_Calendar3_mike(DateTime.Parse(datesel).Date, home.Id);
                Collection<ActivityEventModel_Calendar4> l_Events_4 = HomeDAL.GetActivityEvents_Calendar4_mike(DateTime.Parse(datesel).Date, home.Id);
                ViewBag.Events2 = l_Events_2;
                ViewBag.Events3 = l_Events_3;
                ViewBag.Events4 = l_Events_4;
            }
            TempData["LIST_VIEW_RESIDENT"] = LIST_VIEW_RESIDENT;
            TempData.Keep("LIST_VIEW_RESIDENT");
            ViewBag.LIST_VIEW_RESIDENT = LIST_VIEW_RESIDENT;

            string[] returnStr = HomeDAL.get_Activity_Calendar_Name(home.Id);
            ViewBag.CalendarName = returnStr;

            TempData["tab"] = tab;

            ViewBag.ActivityCategory = MasterDAL.GetAllActivityCategory();

            ViewBag.AllHome = HomeDAL.GetHomeCollections();

            return View(LIST_VIEW_RESIDENT);

        }

        [HttpPost]
        public void Change_Calendar_Name(string number, string newName)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            HomeDAL.Change_Calendar_Name(home.Id, int.Parse(number), newName,user.ID);

        }

        [HttpPost]
        public int saveButton_Activity(string arr, int whichAEID, string datesel,string eventName, string englishname, string tab)
        {
            int returnint=0;
            string arr_2 = arr.Replace("Active", "option1").Replace("Passive", "option2").Replace("Declined", "option3").Replace("Away", "option4");
            arr = arr.Replace("option1", "Active").Replace("option2", "Passive").Replace("option3", "Declined").Replace("option4", "Away");
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<string> Save_sum_array = new List<string>();
            List<string> Save_sum_array_original = new List<string>();
            Dictionary<string, string> Save_summary = new Dictionary<string, string>();
            if (arr_2.Length != 0)
            {
                Save_sum_array = arr_2.Substring(0, arr_2.Length - 1).Split(',').ToList();
                Save_sum_array_original = arr.Substring(0, arr.Length - 1).Split(',').ToList();
                for (int a = 0; a < Save_sum_array.Count(); a = a + 2)
                {
                    Activity_Attendance_functions.add_progress_note(int.Parse(Save_sum_array_original[a]), DateTime.Now, "Resident " + Save_sum_array_original[a + 1] + " participating in " + englishname, user.ID, DateTime.Now);

                    HomeDAL.AddPersonalCalendar(home.Id, int.Parse(Save_sum_array_original[a]), "<b>" + Save_sum_array_original[a + 1] + "</b> participation in <b>"+ englishname + "</br>", user.ID);

                    if (Save_summary.ContainsKey(Save_sum_array[a + 1]) == false)
                    {
                        Save_summary.Add(Save_sum_array[a + 1], Save_sum_array[a]);
                    }
                    else
                        Save_summary[Save_sum_array[a + 1]] += "," + Save_sum_array[a];
                }
            }
            if (tab == "a" || tab == "")
            {
                returnint = Activity_Attendance_functions.save_Button(home.Id, whichAEID, DateTime.Parse(datesel).Date, Save_summary, user.ID, DateTime.Now);
            }
            else if (tab == "b")
            {
                returnint = Activity_Attendance_functions.save_Button2(home.Id, whichAEID, DateTime.Parse(datesel).Date, Save_summary, user.ID, DateTime.Now);
            }
            else if (tab == "c")
            {
                returnint = Activity_Attendance_functions.save_Button3(home.Id, whichAEID, DateTime.Parse(datesel).Date, Save_summary, user.ID, DateTime.Now);
            }
            else if (tab == "d")
            {
                returnint = Activity_Attendance_functions.save_Button4(home.Id, whichAEID, DateTime.Parse(datesel).Date, Save_summary, user.ID, DateTime.Now);
            }
            else
            {
                returnint = 0;
            }
            return returnint;
        }

        [HttpGet]
        public StringBuilder Getting_Activity(int whichAEID, string datesel, string tab)
        {
            StringBuilder returnstring= new StringBuilder();
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            if (tab == "a" || tab=="")
            {
                returnstring = Activity_Attendance_functions.getting_LIST(whichAEID, DateTime.Parse(datesel), home.Id);
            }
            else if (tab == "b")
            {
                returnstring = Activity_Attendance_functions.getting_LIST2(whichAEID, DateTime.Parse(datesel), home.Id);
            }
            else if(tab == "c")
            {
                returnstring = Activity_Attendance_functions.getting_LIST3(whichAEID, DateTime.Parse(datesel), home.Id);
            }
            else if(tab == "d")
            {
                returnstring = Activity_Attendance_functions.getting_LIST4(whichAEID, DateTime.Parse(datesel), home.Id);
            }
            return returnstring;

        }

        [HttpGet]
        public ActionResult View_List_Activity(int whichAEID, string datesel, string tab)
        {
            StringBuilder returnstring = new StringBuilder();
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");

            if (tab == "a" || tab == "")
            {
                returnstring = Activity_Attendance_functions.getting_LIST(whichAEID, DateTime.Parse(datesel), home.Id);
            }
            else if (tab == "b")
            {
                returnstring = Activity_Attendance_functions.getting_LIST2(whichAEID, DateTime.Parse(datesel), home.Id);
            }
            else if (tab == "c")
            {
                returnstring = Activity_Attendance_functions.getting_LIST3(whichAEID, DateTime.Parse(datesel), home.Id);
            }
            else if (tab == "d")
            {
                returnstring = Activity_Attendance_functions.getting_LIST4(whichAEID, DateTime.Parse(datesel), home.Id);
            }

            string[] viewlist = returnstring.ToString().Split(';');
            List<dynamic> l_Json = new List<dynamic>();
            for (var c = 0; c < viewlist.Length; c++)
            {
                var cplus = c + 1;
                for (var b = 0; b < viewlist[c].Split(',').Length; b++)
                {
                    if (viewlist[c].Split(',')[b] != "")
                    {
                        dynamic l_J = new System.Dynamic.ExpandoObject();
                        var resident = ResidentsDAL.GetResidentById(int.Parse(viewlist[c].Split(',')[b]));

                        l_J.Name = resident.FirstName + " " + resident.LastName;
                        l_J.residentid = viewlist[c].Split(',')[b];
                        l_J.gender = resident.Gendar;
                        if (c == 0)
                            l_J.action = "Active";
                        else if (c == 1)
                            l_J.action = "Passive";
                        else if (c == 2)
                            l_J.action = "Declined";
                        else if (c == 3)
                            l_J.action = "Away";

                        l_Json.Add(l_J);
                    }
                }
            }
            return Json(l_Json, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public int click_progress_note_activity(int residentid, string datesel, string note)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            int returnint = Activity_Attendance_functions.add_progress_note(residentid, DateTime.Now, note, user.ID, DateTime.Now);

            return returnint;
        }

        #endregion

        #region To Do List

        [HttpGet]
        public ActionResult HO_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_hospital_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult HO_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_hospital_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DU_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_DU_list(home.Id, user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public int DU_Acknowledge(string id)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            bool result = to_do_list_function.DU_Acknowledge(id,user.ID.ToString());
            if (result == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            
        }


        [HttpGet]
        public ActionResult IAA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IAA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult IDA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IDA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IFRA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IFRA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IRCA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_IRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IRCA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_IRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PN_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_PN_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AN_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_AN_list(home.Id,user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AN_VIEW_CLICK(int PNid, int residentid)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.AN_VIEW(PNid, residentid, user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RAA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RAA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RAA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RDA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RDA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RDA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RFRA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RFRA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RFRA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RRCA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RRCA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_nextmonth_RRCA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RI_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RI_list(home.Id, user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void RI_Acknowledge_CLICK(int pnid,int residentid,string action)
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            to_do_list_function.get_RI_Acknowledge(user.ID,pnid,residentid,action);
        }

        [HttpGet]
        public ActionResult RB_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RB_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RP_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_RP_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NR_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_NR_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SA_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_SA_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SA_nextmonth_CLICK()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_SA_nextmonth_list(home.Id);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Progress_Note_Reminder()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            TempData.Keep("User");
            TempData.Keep("Home");
            List<dynamic> l_Json = to_do_list_function.get_Progress_Note_Reminder_list(home.Id,user.ID);
            return Json(l_Json, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region resident calendars

        public ActionResult SuggestedActivityCalendar()
        {
            return View();
        }

        public ActionResult SuggestedActivityCalendar_mike()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            TempData["datechoose"] = DateTime.Now.ToString("MMMM yyyy");
            Collection<ActivityEventModel> l_Events = HomeDAL.GetActivityEvents_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
            ViewBag.Events = l_Events;

            Collection<ActivityEventModel_Calendar2> l_Events_2 = HomeDAL.GetActivityEvents_Calendar2_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
            Collection<ActivityEventModel_Calendar3> l_Events_3 = HomeDAL.GetActivityEvents_Calendar3_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
            Collection<ActivityEventModel_Calendar4> l_Events_4 = HomeDAL.GetActivityEvents_Calendar4_mike_SAC(DateTime.Today.Date, home.Id, resident.ID);
            ViewBag.Events2 = l_Events_2;
            ViewBag.Events3 = l_Events_3;
            ViewBag.Events4 = l_Events_4;

            string[] returnStr = HomeDAL.get_Activity_Calendar_Name(home.Id);
            ViewBag.CalendarName = returnStr;


            return View();

        }

        public ActionResult BirthdayCalendar()
        {
            return View();
        }

        public ActionResult BirthdayCalendar_mike()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            TempData["datechoose"] = DateTime.Now.ToString("MMMM yyyy");
            return View();

        }

        public ActionResult PersonalCalendar()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;
            TempData["datechoose"] = DateTime.Now.ToString("MMMM yyyy");
            return View();

        }

        public JsonResult GetPersonalCalendar()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");
            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            var l_ActivityEvents = HomeDAL.GetPersonalCalendar(home.Id,resident.ID);

            List<Dictionary<string, string>> l_Events = new List<Dictionary<string, string>>();

            foreach (var l_Data in l_ActivityEvents)
            {
                var ggstart = new DateTime(l_Data.ProgramStartDate.Year, l_Data.ProgramStartDate.Month, l_Data.ProgramStartDate.Day);
                var ggend = new DateTime(l_Data.ProgramEndDate.Year, l_Data.ProgramEndDate.Month, l_Data.ProgramEndDate.Day);
                var columns = new Dictionary<string, string>
                {
                    { "id", l_Data.ProgramId.ToString()},
                    { "title", l_Data.ProgramName},
                    { "startDate", ggstart.ToString("yyyy-MM-dd")},
                    { "endDate", ggend.ToString("yyyy-MM-dd")},
                    { "startTime", DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endTime", DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},
                    { "startT", ggstart.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramStartTime).ToString("HH:mm")},
                    { "endT", ggend.ToString("yyyy-MM-dd")+"T"+DateTime.Parse(l_Data.ProgramEndTime).ToString("HH:mm")},

                };

                l_Events.Add(columns);
            }

            return Json(l_Events, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Resident_Archive

        public ActionResult Search_Archive()
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            ViewBag.Home = home;
            return View();
        }

        [HttpPost]
        public int saveButton_Archive()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            DateTime returndate = DateTime.Parse(Request.Form["returndate"]);
            string suiteno = (Request.Form["suiteno"]).ToString();
            int occu = int.Parse(Request.Form["occu"]);
            string reason = Request.Form["reason"].ToString();
            string notes = Request.Form["notes"].ToString();

            int status;
            if (reason == "Personal Leave") status = 12;
            else if (reason == "Medical Leave") status = 13;
            else if (reason == "Reason Unknown") status = 14;
            else status = 0;

            notes = "Reason: " + reason + "; Notes: " + notes;

            if (update_Suite_Handler_Table.check_date_validation(returndate) == false && user.UserType != 1)
            {
                return 2;
            }
            else
            {
                int returnint1 = 0;
                int returnint2 = 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/Content/assets/Images/Home/" + home.Id + "/Resident_Image/") + resident.ID.ToString() + ".png");
                    returnint2 = HomeDAL.Save_Image(home.Id, resident.ID);
                    returnint1 = HomeDAL.Save_Archive(user.ID, home.Id, resident.ID, suiteno, occu, returndate, notes,status);

                }
                if (returnint1 == 1 && returnint2 == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }


        }


        [HttpPost]
        public int EDIT_PHOTO()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            int returnint2 = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                file.SaveAs(Server.MapPath("~/Content/assets/Images/Home/" + home.Id + "/Resident_Image/") + resident.ID.ToString() + ".png");
                returnint2 = HomeDAL.Save_Image(home.Id, resident.ID);

            }
            if (returnint2 == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            


        }

        [HttpPost]
        public int ChangeMainPhoto()
        {
            var home = (HomeModel)TempData["Home"];
            var user = (UserModel)TempData["User"];
            var resident = (ResidentModel)TempData["Resident"];
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                if (fileSize > 0)
                {
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("/Content/assets/img/login_home.jpg"));
                }
            }

            return 1;




        }


        [HttpPost]
        public ActionResult EditProfileTable1(ResidentModel postdata)
        {
            foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(postdata) == null) { prop.SetValue(postdata, ""); }
                }
            }

            ResidentsDAL.UpdateResidentGeneralInfo1(postdata);


            return Json(new { msg = "Successfully added "});
        }

        [HttpPost]
        public ActionResult EditProfileTable2(ResidentModel postdata)
        {
            foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(postdata) == null) { prop.SetValue(postdata, ""); }
                }
            }

            ResidentModel currentModel = ResidentsDAL.GetResidentById(postdata.ID);
            postdata.HomePhone1 = currentModel.HomePhone1;
            postdata.CellPhone1 = currentModel.CellPhone1;
            postdata.BusinessPhone1 = currentModel.BusinessPhone1;
            postdata.HomePhoneType1 = currentModel.HomePhoneType1;
            postdata.CellPhoneType1 = currentModel.CellPhoneType1;
            postdata.BusinessPhoneType1 = currentModel.BusinessPhoneType1;
            postdata.HomePhone2 = currentModel.HomePhone2;
            postdata.CellPhone2 = currentModel.CellPhone2;
            postdata.BusinessPhone2 = currentModel.BusinessPhone2;
            postdata.HomePhoneType2 = currentModel.HomePhoneType2;
            postdata.CellPhoneType2 = currentModel.CellPhoneType2;
            postdata.BusinessPhoneType2 = currentModel.BusinessPhoneType2;
            postdata.HomePhone3 = currentModel.HomePhone3;
            postdata.CellPhone3 = currentModel.CellPhone3;
            postdata.BusinessPhone3 = currentModel.BusinessPhone3;
            postdata.HomePhoneType3 = currentModel.HomePhoneType3;
            postdata.CellPhoneType3 = currentModel.CellPhoneType3;
            postdata.BusinessPhoneType3 = currentModel.BusinessPhoneType3;
            if (postdata.HomePhoneType1 == 1) postdata.HomePhone1 = postdata.First_phone1;
            else if (postdata.CellPhoneType1 == 1) postdata.CellPhone1 = postdata.First_phone1;
            else if (postdata.BusinessPhoneType1 == 1) postdata.BusinessPhone1 = postdata.First_phone1;
            if (postdata.HomePhoneType2 == 1) postdata.HomePhone2 = postdata.First_phone2;
            else if (postdata.CellPhoneType2 == 1) postdata.CellPhone2 = postdata.First_phone2;
            else if (postdata.BusinessPhoneType2 == 1) postdata.BusinessPhone2 = postdata.First_phone2;
            if (postdata.HomePhoneType3 == 1) postdata.HomePhone3 = postdata.First_phone3;
            else if (postdata.CellPhoneType3 == 1) postdata.CellPhone3 = postdata.First_phone3;
            else if (postdata.BusinessPhoneType3 == 1) postdata.BusinessPhone3 = postdata.First_phone3;

            ResidentsDAL.UpdateResidentGeneralInfo2(postdata);

            return Json(new { msg = "Successfully added " });
        }

        [HttpPost]
        public ActionResult EditProfileTable3(ResidentModel postdata)
        {
            foreach (PropertyInfo prop in typeof(ResidentModel).GetProperties())
            {
                if (prop.PropertyType.Name == "String" || prop.PropertyType.Name == "string")
                {
                    if (prop.GetValue(postdata) == null) { prop.SetValue(postdata, ""); }
                }
            }
            ResidentsDAL.UpdateResidentGeneralInfo3(postdata);

            return Json(new { msg = "Successfully added " });
        }




        //[HttpPost]
        //public JsonResult saveButton_Image()
        //{
        //    var home = (HomeModel)TempData["Home"];
        //    var user = (UserModel)TempData["User"];
        //    var resident = (ResidentModel)TempData["Resident"];
        //    TempData.Keep("User");
        //    TempData.Keep("Home");
        //    TempData.Keep("Resident");
        //    for (int i = 0; i < Request.Files.Count; i++)
        //    {
        //        HttpPostedFileBase file = Request.Files[i];                                                        
        //        int fileSize = file.ContentLength;
        //        string fileName = file.FileName;
        //        string mimeType = file.ContentType;
        //        System.IO.Stream fileContent = file.InputStream;
        //        file.SaveAs(Server.MapPath("~/Content/assets/Images/Home/" + home.Id + "/Resident_Image/") + resident.ID.ToString()+".png");
        //    }
        //    return Json("Uploaded " + Request.Files.Count + " files");
        //}





        #endregion



    }
}

    public class EmergencyDetailspdfData
    {
        public string fd_suite_no { get; set; }
        public string fd_first_name { get; set; }
        public string fd_last_name { get; set; }
        public string fd_gender { get; set; }
        public string fd_phone { get; set; }
        public string fd_contact_1 { get; set; }
        public string fd_home_phone_1 { get; set; }
        public string sReason { get; set; }
        public int fd_risk_order { get; set; }

        public string MobiltySelectedValue { get; set; }
    }

    public class pdfHeaderFooter : PdfPageEventHelper
    {
        int i = 1;
        private string homename;

    public pdfHeaderFooter(string name)
    {
        homename = name;
    }

    public override void OnStartPage(PdfWriter writer, Document document)
        {

            base.OnOpenDocument(writer, document);
            iTextSharp.text.Font headerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
            PdfPTable tabHeader = new PdfPTable(new float[] { 1F, 1F });
            tabHeader.SpacingAfter = 1F;
            PdfPCell cell;
            tabHeader.TotalWidth = 783;

            PdfPCell cell1 = new PdfPCell(new Phrase("QOLA Date printed: " + DateTime.Now.ToShortDateString()+" "+ DateTime.Now.ToShortTimeString(), headerFont));
            cell1.Border = 0;
            cell1.HorizontalAlignment = Element.ALIGN_LEFT;

            tabHeader.AddCell(cell1);

            cell = new PdfPCell(new Phrase(homename, headerFont)); //removed homename
            cell.Border = 0;

            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabHeader.AddCell(cell);
            tabHeader.WriteSelectedRows(0, -1, 29, (document.PageSize.Height - 10), writer.DirectContent);
        }
    public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            base.OnEndPage(writer, document);
            iTextSharp.text.Font footerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
            PdfPTable tabFot = new PdfPTable(new float[] { 9f, 12f, 8f });

            tabFot.TotalWidth = 785;
            tabFot.WidthPercentage = 100f;

            PdfPCell cell1 = new PdfPCell(new Phrase("EmergencyResidentDetails", footerFont));
            cell1.Border = 0;

            cell1.HorizontalAlignment = Element.ALIGN_LEFT;

            tabFot.AddCell(cell1);

            PdfPTable colorTable = new PdfPTable(8);
            colorTable.TotalWidth = 430f;
            colorTable.SetWidths(new float[] { .03f, .25f, .03f, .25f, .03f, .25f, .03f, .25f });


            PdfPCell redCell = new PdfPCell();
            redCell.BackgroundColor = BaseColor.RED;
            colorTable.AddCell(redCell);


            PdfPCell red = new PdfPCell(new Phrase("HighRisk", footerFont));
            red.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(red);

            PdfPCell yellowCell = new PdfPCell();
            yellowCell.BackgroundColor = BaseColor.YELLOW;
            colorTable.AddCell(yellowCell);

            PdfPCell yellow = new PdfPCell(new Phrase("ModerateRisk", footerFont));
            yellow.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(yellow);


            PdfPCell GreenCell = new PdfPCell();
            GreenCell.BackgroundColor = BaseColor.GREEN;
            colorTable.AddCell(GreenCell);

            PdfPCell Green = new PdfPCell(new Phrase("LowRisk", footerFont));
            Green.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(Green);

            PdfPCell noRiskCell = new PdfPCell();
            noRiskCell.BackgroundColor = BaseColor.WHITE;
            colorTable.AddCell(noRiskCell);


            PdfPCell noRisk = new PdfPCell(new Phrase("NoRisk", footerFont));
            noRisk.HorizontalAlignment = Element.ALIGN_CENTER;
            colorTable.AddCell(noRisk);


            PdfPCell colorCell = new PdfPCell(colorTable);
            colorCell.Border = 0;

            colorCell.HorizontalAlignment = Element.ALIGN_LEFT;
            colorCell.PaddingLeft = 30;
            tabFot.AddCell(colorCell);

            PdfPCell cell = new PdfPCell(new Phrase("Page" + " - " + i++, footerFont));
            cell.Border = 0;

            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 29, document.Bottom - 3, writer.DirectContent);
        }

}

public class pdfHeaderFooter2 : PdfPageEventHelper
{
    int i = 1;
    private string homename;

    public pdfHeaderFooter2(string name)
    {
        homename = name;
    }

    public override void OnStartPage(PdfWriter writer, Document document)
    {

        base.OnOpenDocument(writer, document);
        iTextSharp.text.Font headerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
        PdfPTable tabHeader = new PdfPTable(new float[] { 1F, 1F });
        tabHeader.SpacingAfter = 1F;
        PdfPCell cell;
        tabHeader.TotalWidth = 783;

        PdfPCell cell1 = new PdfPCell(new Phrase("QOLA Date printed: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), headerFont));
        cell1.Border = 0;
        cell1.HorizontalAlignment = Element.ALIGN_LEFT;

        tabHeader.AddCell(cell1);

        cell = new PdfPCell(new Phrase(homename, headerFont)); //removed homename
        cell.Border = 0;

        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        tabHeader.AddCell(cell);
        tabHeader.WriteSelectedRows(0, -1, 29, (document.PageSize.Height - 10), writer.DirectContent);
    }
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        base.OnEndPage(writer, document);
        base.OnEndPage(writer, document);
        iTextSharp.text.Font footerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
        PdfPTable tabFot = new PdfPTable(new float[] { 9f, 8f });

        tabFot.TotalWidth = 785;
        tabFot.WidthPercentage = 100f;

        PdfPCell cell1 = new PdfPCell(new Phrase("Nursing Note List", footerFont));
        cell1.Border = 0;

        cell1.HorizontalAlignment = Element.ALIGN_LEFT;

        tabFot.AddCell(cell1);


        PdfPCell cell = new PdfPCell(new Phrase("Page" + " - " + i++, footerFont));
        cell.Border = 0;

        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        tabFot.AddCell(cell);
        tabFot.WriteSelectedRows(0, -1, 29, document.Bottom - 3, writer.DirectContent);
    }

}

public class pdfHeaderFooterNormal : PdfPageEventHelper
{
    int i = 1;
    private string homename;
    private string reportName;

    public pdfHeaderFooterNormal(string name,string report)
    {
        homename = name;
        reportName = report;
    }
    public override void OnStartPage(PdfWriter writer, Document document)
    {

        base.OnOpenDocument(writer, document);
        iTextSharp.text.Font headerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
        PdfPTable tabHeader = new PdfPTable(new float[] { 1F, 1F });
        tabHeader.SpacingAfter = 1F;

        tabHeader.TotalWidth = PageSize.A4.Width-60;

        PdfPCell cell1 = new PdfPCell(new Phrase("QOLA Date printed: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), headerFont));
        cell1.Border = 0;
        cell1.HorizontalAlignment = Element.ALIGN_LEFT;
        tabHeader.AddCell(cell1);

        PdfPCell cell = new PdfPCell(new Phrase(homename, headerFont)); 
        cell.Border = 0;
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        tabHeader.AddCell(cell);

        tabHeader.WriteSelectedRows(0, -1, 29, (document.PageSize.Height - 10), writer.DirectContent);
    }
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        base.OnEndPage(writer, document);
        base.OnEndPage(writer, document);
        iTextSharp.text.Font footerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10f);
        PdfPTable tabFot = new PdfPTable(new float[] { 9f, 8f });

        tabFot.TotalWidth = PageSize.A4.Width - 60;
        tabFot.WidthPercentage = 100f;

        PdfPCell cell1 = new PdfPCell(new Phrase(reportName, footerFont));
        cell1.Border = 0;

        cell1.HorizontalAlignment = Element.ALIGN_LEFT;

        tabFot.AddCell(cell1);


        PdfPCell cell = new PdfPCell(new Phrase("Page" + " - " + i++, footerFont));
        cell.Border = 0;

        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        tabFot.AddCell(cell);
        tabFot.WriteSelectedRows(0, -1, 29, document.Bottom - 3, writer.DirectContent);
    }

}



