using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class Documents
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public DocumentTypeModel DocumentType { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public int? ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
    }
}