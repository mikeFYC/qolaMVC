using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NursingNoteReport
    {
        public List<NursingNotesModel> NursingNoteList;
    }


    public class NursingNotesModel
    {
        public string userName { get; set; }
        public string userNameType { get; set; }
        public string suiteNo { get; set; }
        public string FullName { get; set; }
        public int Category { get; set; }
        public string CategoryFull { get; set; }
        public string Note { get; set; }
        public DateTime DateEntered { get; set; }

        


    }
}