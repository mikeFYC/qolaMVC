using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class DocumentTypeModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
    }

    public class EditDocumentTypeModel
    {
        public int ResidentId { get; set; }
        public string ResidentName { get; set; }
        public string OldDocumentTypeName { get; set; }
    }
}