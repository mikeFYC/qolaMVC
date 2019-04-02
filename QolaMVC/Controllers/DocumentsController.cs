using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace QolaMVC.Controllers
{
    public class DocumentsController : Controller
    {
        // GET: Assessments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Document(int? DocumentId)
        {
            HomeModel home = (HomeModel)TempData["Home"];
            UserModel user = (UserModel)TempData["User"];
            ResidentModel resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            ViewBag.AllDocumentType = MasterDAL.GetAllDocumentType();

            Documents docModel = new Documents();
            if (DocumentId > 0 && DocumentId != null)
            {
                docModel = DocumentsDAL.GetDocumentById((int)DocumentId);
            }
            else
            {
                docModel = new Documents
                {
                    DocumentType = new DocumentTypeModel()
                };
            }

            return View(docModel);
        }

        [HttpPost]
        public ActionResult Document(Documents p_Model)
        {
            HomeModel home = (HomeModel)TempData["Home"];
            UserModel user = (UserModel)TempData["User"];
            ResidentModel resident = (ResidentModel)TempData["Resident"];
            ViewBag.User = user;
            ViewBag.Resident = resident;
            ViewBag.Home = home;
            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            try
            {
                ViewBag.AllDocumentType = MasterDAL.GetAllDocumentType();
                Documents docModel = new Documents
                {
                    Id = p_Model.Id,
                    Title = p_Model.Title,
                    DocumentType = new DocumentTypeModel()
                };
                docModel.DocumentType = MasterDAL.GetDocumentTypeById(p_Model.DocumentType.Id);

                p_Model.CreatedByUserId = user.ID;

                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 0) //Validate file
                {
                    int fileSize = file.ContentLength;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    string extension = Path.GetExtension(file.FileName);

                    int validateFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["ResidentDocumentFileSize"]);
                    string[] validateFileExtension = Convert.ToString(ConfigurationManager.AppSettings["ResidentDocumentFileExtensions"]).Split(',');

                    //if (extension != ".xls" && extension != ".xlsx" && extension != ".dox" && extension != ".docx" && extension != ".pdf")
                    if (Array.IndexOf(validateFileExtension, extension) == -1)
                    {
                        TempData["notice"] = "Invalid file type";
                        return RedirectToAction("Document");
                    }

                    if ((fileSize / (1024 * 1024)) > validateFileSize)
                    {
                        TempData["notice"] = "File size should be less than 10 mb";
                        return RedirectToAction("Document");
                    }
                }

                string fileExtension = Path.GetExtension(file.FileName);
                string folderFileName = DateTime.Now.ToString("yyyyMMddTHHmmssffffff") + fileExtension;

                if (p_Model.Id == 0) //Save document
                {
                    string fileName = file.FileName;
                    string filePath = "~/Upload/Documents/" + Convert.ToString(resident.ID) + "/" + Convert.ToString(resident.FirstName) + " " + Convert.ToString(resident.LastName) + "/" + Convert.ToString(docModel.DocumentType.Type) + "/";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));
                    }

                    file.SaveAs(Server.MapPath(filePath) + folderFileName + "");
                    p_Model.ResidentId = resident.ID;
                    p_Model.FileName = folderFileName;
                    //p_Model.FilePath = filePath + "" + folderFileName;
                    p_Model.FilePath = string.Empty;
                    int id = DocumentsDAL.AddResidentDocument(p_Model);
                    TempData["notice"] = "Add Successfully";
                }
                else if (p_Model.Id > 0) //Update document
                {
                    Documents EditDocument = DocumentsDAL.GetDocumentById(p_Model.Id);

                    string fileName = string.Empty;
                    string filePath = string.Empty;
                    if (file.ContentLength > 0)
                    {
                        fileName = folderFileName;
                    }
                    else
                    {
                        fileName = EditDocument.FileName;
                    }

                    filePath = "~/Upload/Documents/" + Convert.ToString(resident.ID) + "/" + Convert.ToString(resident.FirstName) + " " + Convert.ToString(resident.LastName) + "/" + Convert.ToString(docModel.DocumentType.Type) + "/";
                    bool exists = System.IO.Directory.Exists(Server.MapPath(filePath));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(filePath));
                    }

                    //Remove file from folder
                    string documentFile = Server.MapPath(filePath + EditDocument.FileName);
                    if (System.IO.File.Exists(documentFile) && file.ContentLength > 0)
                    {
                        System.IO.File.Delete(documentFile);
                    }

                    if (file.ContentLength > 0)
                    {
                        file.SaveAs(Server.MapPath(filePath) + folderFileName + "");
                    }
                
                    p_Model.ResidentId = resident.ID;
                    p_Model.FileName = fileName;
                    //p_Model.FilePath = filePath + "" + folderFileName;
                    p_Model.FilePath = string.Empty;
                    int id = DocumentsDAL.UpdateResidentDocument(p_Model);
                    TempData["notice"] = "Edit Successfully";
                }

                return RedirectToAction("ResidentMenu", "Home", new { @p_ResidentId = resident.ID });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult searchDocuments(int residentId, int? documentTypeId)
        {
            Collection<Documents> p_DocumentsList = new Collection<Documents>();
            p_DocumentsList = DocumentsDAL.GetDocumentListByResident(residentId, documentTypeId);

            return Json(p_DocumentsList);
        }

        [HttpPost]
        public ActionResult DeleteDocument(int DocumentId, string DocumentTypeName)
        {
            HomeModel home = (HomeModel)TempData["Home"];
            UserModel user = (UserModel)TempData["User"];
            ResidentModel resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            Documents l_Document = DocumentsDAL.GetDocumentById(DocumentId);
            string result = DocumentsDAL.DeleteDocument(DocumentId);

            if (result == "Success")
            {
                string filePath = "~/Upload/Documents/" + Convert.ToString(resident.ID) + "/" + Convert.ToString(resident.FirstName) + " " + Convert.ToString(resident.LastName) + "/" + Convert.ToString(DocumentTypeName) + "/" + l_Document.FileName;
                string documentFile = Server.MapPath(filePath);
                if (System.IO.File.Exists(documentFile))
                {
                    System.IO.File.Delete(documentFile);
                }
            }

            return Json(result);
        }

        public ActionResult DownloadFile(string DocumentTypeName, string fileName)
        {
            HomeModel home = (HomeModel)TempData["Home"];
            UserModel user = (UserModel)TempData["User"];
            ResidentModel resident = (ResidentModel)TempData["Resident"];

            ViewBag.User = user;
            ViewBag.Home = home;
            ViewBag.Resident = resident;

            TempData.Keep("User");
            TempData.Keep("Home");
            TempData.Keep("Resident");

            string filePath = "~/Upload/Documents/" + Convert.ToString(resident.ID) + "/" + Convert.ToString(resident.FirstName) + " " + Convert.ToString(resident.LastName) + "/" + Convert.ToString(DocumentTypeName) + "/" + fileName;
            //string fileName = System.IO.Path.GetFileName(filePath);
            string path = Server.MapPath(filePath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}