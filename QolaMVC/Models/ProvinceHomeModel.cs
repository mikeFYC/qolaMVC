using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ProvinceHomeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string IconImage { get; set; }
        public int AlertCount { get; set; }
        public int Occupied { get; set; }
        public int TotalSuite { get; set; }
        public int QolaResidentCount { get; set; }

        public ProvinceHomeModel(int p_Id, 
                                string p_Name, 
                                int p_ProvinceId, 
                                string p_ProvinceName, 
                                string p_IconImage, 
                                int p_AlertCount,
                                int p_Occupied,
                                int p_TotalSuite,
                                int p_QolaResidentCount)
        {
            Id = p_Id;
            ProvinceId = p_ProvinceId;
            ProvinceName = p_ProvinceName;
            IconImage = p_IconImage;
            AlertCount = p_AlertCount;
            Occupied = p_Occupied;
            TotalSuite = p_TotalSuite;
            QolaResidentCount = p_QolaResidentCount;
        }
    }
}