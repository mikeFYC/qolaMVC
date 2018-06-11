using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class CarePlanInfectiousDiseasesModel
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int CarePlanId { get; set; }
        public string MRSA { get; set; }
        public string MRSADiagnosedDate { get; set; }
        public string MRSAResolvedDate { get; set; }
        public string VRE { get; set; }
        public string VREDiagnosedDate { get; set; }
        public string VREResolvedDate { get; set; }
        public string CDiff { get; set; }
        public string CDiffDiagnosedDate { get; set; }
        public string CDiffResolvedDate { get; set; }
        public string Other { get; set; }
        public string OtherDiagnosedDate { get; set; }
        public string OtherResolvedDate { get; set; }
    }
}