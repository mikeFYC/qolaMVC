using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using QolaMVC.DAL;
using QolaMVC.Models;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using QolaMVC.Helpers;

namespace QolaMVC.Helpers
{
    public class ProgressNotesHelper
    {
        public static void RegisterSession(ResidentModel p_Model)
        {
            if ((HttpContext.Current.Session["resident"] == null))
            {
                HttpContext.Current.Session.Add("resident", p_Model);
                HttpContext.Current.Session.Add("ResidentId", p_Model.ID);
            }
        }
        public class PDFFooter : PdfPageEventHelper
        {
            int i = 1;
            ResidentModel resident = null;
            iTextSharp.text.Font fontBoldText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontBoldHeadText = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font fontCellHeader = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfContentByte cb;
            PdfTemplate template;
            BaseFont bf = null;
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb = writer.DirectContent;
                    template = cb.CreateTemplate(50, 50);
                }
                catch (DocumentException de)
                {
                    string str = de.Message;
                }
                catch (System.IO.IOException ioe)
                {
                    string str = ioe.Message;
                }
            }
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                
                base.OnOpenDocument(writer, document);
                iTextSharp.text.Font headerFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9f);
                PdfPTable tabHeader = new PdfPTable(new float[] { 2F, 1F });
                tabHeader.SpacingAfter = 10F;
                PdfPCell cell;
                tabHeader.TotalWidth = 565F;
                if (document.PageNumber == 1)
                {
                    resident = new ResidentModel();
                    if (HttpContext.Current.Session["ResidentStatus"] != null && HttpContext.Current.Session["ResidentStatus"].ToString() == "I")
                    {
                        resident = ResidentsDAL.GetInActiveResidentById(Convert.ToInt32(HttpContext.Current.Session["ResidentId"]));
                    }
                    else
                    {
                        resident = ResidentsDAL.GetResidentById(Convert.ToInt32(HttpContext.Current.Session["ResidentId"]));
                    }
                }

                PdfPCell hdCell1 = new PdfPCell(new Phrase("ResidentName" + ": " + resident.LastName + ", " + resident.FirstName + "", fontBoldText));
                hdCell1.Border = 0;
                hdCell1.PaddingLeft = 30;
                tabHeader.AddCell(hdCell1);
                PdfPCell hdCell2 = new PdfPCell(new Phrase("HomeName".ToString(), headerFont));
                hdCell2.Border = 0;
                hdCell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabHeader.AddCell(hdCell2);

                PdfPCell hdCell3 = new PdfPCell(new Phrase("Suite" + ": " + resident.SuiteNo, fontBoldText));
                hdCell3.Border = 0;
                hdCell3.PaddingLeft = 30;
                tabHeader.AddCell(hdCell3);
                PdfPCell hdCell4 = new PdfPCell(new Phrase(""));
                hdCell4.Border = 0;
                hdCell4.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabHeader.AddCell(hdCell4);

                PdfPCell HeadCell = new PdfPCell(new Phrase("Multidisciplinary Progress Notes", fontBoldHeadText));
                HeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                HeadCell.PaddingBottom = 5f;
                HeadCell.PaddingLeft = 30;
                HeadCell.Border = 0;
                HeadCell.Colspan = 2;
                tabHeader.AddCell(HeadCell);

                PdfPTable tblProgress = new PdfPTable(7);
                tblProgress.WidthPercentage = 100f;

                float[] wthProgress = new float[] { 0.51f, 1.2f, 0.8f, 1f, 3f, 1.5f, 1.2f };
                tblProgress.SetWidths(wthProgress);
                PdfPCell snoHeadCell = new PdfPCell(new Phrase("SNo", fontCellHeader));

                snoHeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                snoHeadCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                snoHeadCell.FixedHeight = 25f;
                tblProgress.AddCell(snoHeadCell);
                PdfPCell dateHeadCell = new PdfPCell(new Phrase("Date", fontCellHeader));
                dateHeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                dateHeadCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                dateHeadCell.FixedHeight = 20f;
                tblProgress.AddCell(dateHeadCell);
                PdfPCell timeHeadCell = new PdfPCell(new Phrase("Time", fontCellHeader));
                timeHeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                timeHeadCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                timeHeadCell.FixedHeight = 25f;
                tblProgress.AddCell(timeHeadCell);

                PdfPCell categoryHeadCell = new PdfPCell(new Phrase("Category", fontCellHeader));
                categoryHeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                categoryHeadCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                categoryHeadCell.FixedHeight = 25f;
                tblProgress.AddCell(categoryHeadCell);

                PdfPCell titleHeadCell = new PdfPCell(new Phrase("ProgressNotes", fontCellHeader));
                titleHeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                titleHeadCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                titleHeadCell.FixedHeight = 25f;
                tblProgress.AddCell(titleHeadCell);

                PdfPCell userHeadCell = new PdfPCell(new Phrase("User", fontCellHeader));
                userHeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                userHeadCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                userHeadCell.FixedHeight = 25f;
                tblProgress.AddCell(userHeadCell);
                PdfPCell signatureHeadCell = new PdfPCell(new Phrase("Signature", fontCellHeader));
                signatureHeadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                signatureHeadCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                signatureHeadCell.FixedHeight = 25f;
                tblProgress.AddCell(signatureHeadCell);



                PdfPCell Head = new PdfPCell(new PdfPTable(tblProgress));
                Head.PaddingBottom = 5f;
                Head.Border = 0;
                Head.PaddingLeft = 30;
                Head.Colspan = 2;
                tabHeader.AddCell(Head);

                tabHeader.WriteSelectedRows(0, -1, 0, (document.PageSize.Height - 5), writer.DirectContent);
            }
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                int pageN = writer.PageNumber;
                String text = "Page " + pageN + " of  ";
                iTextSharp.text.Rectangle pageSize = document.PageSize;
                cb.BeginText();
                cb.SetFontAndSize(bf, 6);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, text, pageSize.GetRight(40), pageSize.GetBottom(10), 0);
                cb.EndText();
                cb.AddTemplate(template, pageSize.GetRight(40), pageSize.GetBottom(10));
            }
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                template.BeginText();
                template.SetFontAndSize(bf, 6);
                template.SetTextMatrix(0, 0);
                template.ShowText("" + (writer.PageNumber - 1));
                template.EndText();
            }
        }
        public static string GetCategory(int icategory)
        {
            string[] _CateGories = { "", "Others", "MedicalUpdate", "SocialUpdate", "DietaryUpdate", "GeneralUpdate", "ResidentFall", "ResidentBruised" };
            return _CateGories[icategory];
        }

    }
}