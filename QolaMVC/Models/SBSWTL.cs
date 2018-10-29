using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class SBSWTL
    {
        public int ID { get; set; }
        public int residentID { get; set; }
        public List<SBSWTL_row> SBSWTL_List { get; set; }
        public DateTime start_time { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_on { get; set; }
    }
}