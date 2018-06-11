using iTextSharp.text;
using iTextSharp.text.pdf;
using QolaMVC.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public ActionResult Index()
        {
            return View();
        }

        public void SpecialDietReport(int p_HomeId)
        {
            DataTable dt = new DataTable();
            Document document = new Document();
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, mStream);
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);

            PdfPTable table = new PdfPTable(7);
            PdfPRow row = null;
            float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f };

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            int iCol = 0;
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            PdfPCell l_Name = new PdfPCell(new Phrase("Name", font5));
            l_Name.Border = 0;
            l_Name.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Name.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_Suite = new PdfPCell(new Phrase("Suite", font5));
            l_Suite.Border = 0;
            l_Suite.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Suite.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_ResidentName = new PdfPCell(new Phrase("Resident Name", font5));
            l_ResidentName.Border = 0;
            l_ResidentName.HorizontalAlignment = Element.ALIGN_LEFT;
            l_ResidentName.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_AssessedDate = new PdfPCell(new Phrase("Assessed Date", font5));
            l_AssessedDate.Border = 0;
            l_AssessedDate.HorizontalAlignment = Element.ALIGN_LEFT;
            l_AssessedDate.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_Likes = new PdfPCell(new Phrase("Suite", font5));
            l_Likes.Border = 0;
            l_Likes.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Likes.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_DisLikes = new PdfPCell(new Phrase("Suite", font5));
            l_DisLikes.Border = 0;
            l_DisLikes.HorizontalAlignment = Element.ALIGN_LEFT;
            l_DisLikes.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_Notes = new PdfPCell(new Phrase("Suite", font5));
            l_Notes.Border = 0;
            l_Notes.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Notes.BackgroundColor = BaseColor.GRAY;

            table.AddCell(l_Name);
            table.AddCell(l_Suite);
            table.AddCell(l_ResidentName);
            table.AddCell(l_AssessedDate);
            table.AddCell(l_Likes);
            table.AddCell(l_DisLikes);
            table.AddCell(l_Notes);

            var l_SpecialDietReport = HomeDAL.GetSpecialDietReport(p_HomeId);

            foreach (var r in l_SpecialDietReport)
            {
                if (l_SpecialDietReport.Count > 0)
                {
                    table.AddCell(new Phrase(r.Diet, font5));
                    table.AddCell(new Phrase(r.Suite.SuiteNo, font5));
                    table.AddCell(new Phrase(r.Resident.ShortName, font5));
                    table.AddCell(new Phrase(r.DateEntered.ToString(), font5));
                    table.AddCell(new Phrase(r.Likes, font5));
                    table.AddCell(new Phrase(r.DisLikes, font5));
                    table.AddCell(new Phrase("", font5));
                }
            }
            document.Add(table);
            document.Close();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=Special_Diet_Report.pdf");
            Response.Clear();
            Response.BinaryWrite(mStream.ToArray());
        }

        public void LikesReport(int p_HomeId)
        {
            DataTable dt = new DataTable();
            Document document = new Document();
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, mStream);
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);

            PdfPTable table = new PdfPTable(4);
            PdfPRow row = null;
            float[] widths = new float[] { 4f, 4f, 4f, 4f };

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            int iCol = 0;
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            PdfPCell l_Suite = new PdfPCell(new Phrase("Suite", font5));
            l_Suite.Border = 0;
            l_Suite.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Suite.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_ResidentName = new PdfPCell(new Phrase("Resident Name", font5));
            l_ResidentName.Border = 0;
            l_ResidentName.HorizontalAlignment = Element.ALIGN_LEFT;
            l_ResidentName.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_AssessedDate = new PdfPCell(new Phrase("Assessed Date", font5));
            l_AssessedDate.Border = 0;
            l_AssessedDate.HorizontalAlignment = Element.ALIGN_LEFT;
            l_AssessedDate.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_Notes = new PdfPCell(new Phrase("Suite", font5));
            l_Notes.Border = 0;
            l_Notes.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Notes.BackgroundColor = BaseColor.GRAY;

            table.AddCell(l_Suite);
            table.AddCell(l_ResidentName);
            table.AddCell(l_AssessedDate);
            table.AddCell(l_Notes);

            var l_SpecialDietReport = HomeDAL.GetLikesReport(p_HomeId);

            foreach (var r in l_SpecialDietReport)
            {
                if (l_SpecialDietReport.Count > 0)
                {
                    table.AddCell(new Phrase(r.Suite.SuiteNo, font5));
                    table.AddCell(new Phrase(r.Resident.ShortName, font5));
                    table.AddCell(new Phrase(r.DateEntered.ToString(), font5));
                    table.AddCell(new Phrase(r.Likes, font5));
                }
            }
            document.Add(table);
            document.Close();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=Likes_Report.pdf");
            Response.Clear();
            Response.BinaryWrite(mStream.ToArray());
        }

        public void DisLikesReport(int p_HomeId)
        {
            DataTable dt = new DataTable();
            Document document = new Document();
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, mStream);
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);

            PdfPTable table = new PdfPTable(4);
            PdfPRow row = null;
            float[] widths = new float[] { 4f, 4f, 4f, 4f };

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            int iCol = 0;
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            PdfPCell l_Suite = new PdfPCell(new Phrase("Suite", font5));
            l_Suite.Border = 0;
            l_Suite.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Suite.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_ResidentName= new PdfPCell(new Phrase("Resident Name", font5));
            l_ResidentName.Border = 0;
            l_ResidentName.HorizontalAlignment = Element.ALIGN_LEFT;
            l_ResidentName.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_AssessedDate = new PdfPCell(new Phrase("Assessed Date", font5));
            l_AssessedDate.Border = 0;
            l_AssessedDate.HorizontalAlignment = Element.ALIGN_LEFT;
            l_AssessedDate.BackgroundColor = BaseColor.GRAY;

            PdfPCell l_Notes = new PdfPCell(new Phrase("Suite", font5));
            l_Notes.Border = 0;
            l_Notes.HorizontalAlignment = Element.ALIGN_LEFT;
            l_Notes.BackgroundColor = BaseColor.GRAY;

            table.AddCell(l_Suite);
            table.AddCell(l_ResidentName);
            table.AddCell(l_AssessedDate);
            table.AddCell(l_Notes);

            var l_SpecialDietReport = HomeDAL.GetDisLikesReport(p_HomeId);

            foreach (var r in l_SpecialDietReport)
            {
                if (l_SpecialDietReport.Count > 0)
                {
                    table.AddCell(new Phrase(r.Suite.SuiteNo, font5));
                    table.AddCell(new Phrase(r.Resident.ShortName, font5));
                    table.AddCell(new Phrase(r.DateEntered.ToString(), font5));
                    table.AddCell(new Phrase(r.Likes, font5));
                }
            }
            document.Add(table);
            document.Close();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=DisLikes_Report.pdf");
            Response.Clear();
            Response.BinaryWrite(mStream.ToArray());
        }
    }
}