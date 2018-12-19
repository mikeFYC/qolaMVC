using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public int homeid { get; set; }
        public string code { get; set; }
        public string venue { get; set; }
    }
}