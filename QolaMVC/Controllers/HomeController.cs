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

namespace QolaMVC.Controllers
{
    public class HomeController : Controller
    {
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
                Collection<HomeModel> l_Homes = HomeDAL.GetHomeFill(user.ID, 1);// Convert.ToInt32(l_UserId));
                UserModel l_User = UserDAL.GetUserById(user.ID);// Convert.ToInt32(l_UserId));
                TempData["User"] = l_User;

                ViewBag.User = l_User;
                TempData.Keep("User");
                return View(l_Homes.Where(m => m.ProvinceName == "Alberta"));
            }
            else
            {
                return RedirectToAction("Index", "/Login");
            }
        }

        public ActionResult Menu(int p_HomeId)
        {
            var user = (UserModel)TempData["User"];
            TempData["Home"] = HomeDAL.GetHomeById(p_HomeId);
            TempData.Keep("User");
            TempData.Keep("Home");
            ViewBag.User = user;
            HomeModel l_Home = HomeDAL.GetHomeById(p_HomeId);
            return View(l_Home);
        }

        public ActionResult ResidentMenu(int p_ResidentId)
        {
            var user = (UserModel)TempData["User"];
            var home = (HomeModel)TempData["Home"];
            var resident = ResidentsDAL.GetResidentById(p_ResidentId);
            var progressNotes = ProgressNotesDAL.GetProgressNotesCollections(resident.ID, DateTime.Now, DateTime.Now, "A");

            ViewBag.Message = TempData["Message"];

            TempData["Resident"] = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;
            
            ViewBag.ProgressNotes = progressNotes;
            ProgressNotesHelper.RegisterSession(resident);
            return View(resident);
        }
        public ActionResult ManageResidents(int p_HomeId)
        {
            var l_Residents = ResidentsDAL.GetResidentCollections(p_HomeId);
            return View(l_Residents);
        }

        public ActionResult AddNewResident()
        {
            return View();
        }

        public ActionResult ActivityCalendar()
        {
            List<ActivityEventModel> data = new List<ActivityEventModel> {
             new ActivityEventModel {
                    ProgramName = "Turtle Walk",
                    Comments = "Night out with turtles",
                    ProgramStartTime = new DateTime(2016, 6, 2, 3, 0, 0),
                    ProgramEndTime = new DateTime(2016, 6, 2, 4, 0, 0),
                    IsAllDay = true
             },
             new ActivityEventModel {
                    ProgramName = "Winter Sleepers",
                    Comments = "Long sleep during winter season",
                    ProgramStartTime = new DateTime(2016, 6, 3, 1, 0, 0),
                    ProgramEndTime = new DateTime(2016, 6, 3, 2, 0, 0)
             },
             new ActivityEventModel {
                    ProgramName = "Estivation",
                    Comments = "Sleeping in hot season",
                    ProgramStartTime = new DateTime(2016, 6, 4, 3, 0, 0),
                    ProgramEndTime = new DateTime(2016, 6, 4, 4, 0, 0)
             }
            };
            return View(data);
        }


        [HttpPost]
        public void btnPdf_Click()
        {
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

    }
}