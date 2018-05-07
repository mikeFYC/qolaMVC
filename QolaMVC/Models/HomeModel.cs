using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static QolaMVC.Constants.EnumerationTypes;

namespace QolaMVC.Models
{
   /*
    * Chime Created
    * 4-23-2018
    */
    public class HomeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ProvinceModel Province { get; set; }
        public string ProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int NumberOfFloors { get; set; }
        public int NumberOfSuites { get; set; }
        public int OccupiedSuites { get; set; }
        public int TotalSuites { get; set; }
        public string IconImage { get; set; }
        public AvailabilityStatus Status { get; set; }
        public UserModel ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string DineTimeIds { get; set; } //comma separated value for the meal time ides. refers to tbl_dine_time
        public string Phone { get; set; }
        public Guid GUID { get; set; }
        public string PassTimeIds { get; set; }

        public HomeModel( 
                int p_Id,
                string p_Name,
                string p_Code,
                string p_Address,
                string p_City,
                ProvinceModel p_Province,
                string p_ProvinceName,
                string p_PostalCode,
                string p_Country,
                int p_NumberOfFloors,
                int p_NumberOfSuites,
                string p_IconImage,
                AvailabilityStatus p_Status,
                UserModel p_ModifiedBy,
                DateTime p_ModifiedOn,
                string p_DineTimeIds,
                string p_Phone,
                Guid p_GUID,
                string p_PassTimeIds
                )
        {
            Id = p_Id;
            Name = p_Name;
            Code = p_Code;
            Address = p_Address;
            City = p_City;
            Province = p_Province;
            ProvinceName = p_ProvinceName;
            PostalCode = p_PostalCode;
            Country = p_Country;
            NumberOfFloors = p_NumberOfFloors;
            NumberOfSuites = p_NumberOfSuites;
            IconImage = p_IconImage;
            Status = p_Status;
            ModifiedBy = p_ModifiedBy;
            ModifiedOn = p_ModifiedOn;
            DineTimeIds = p_DineTimeIds;
            Phone = p_Phone;
            GUID = p_GUID;
            PassTimeIds = p_PassTimeIds;
        }

        public HomeModel()
        {

        }
    }
}