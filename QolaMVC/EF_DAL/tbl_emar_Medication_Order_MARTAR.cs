//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QolaMVC.EF_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_emar_Medication_Order_MARTAR
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public string RxNumber { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<int> ResidentId { get; set; }
        public string Physicain { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> OrderType { get; set; }
        public Nullable<int> DrugId { get; set; }
        public Nullable<int> TotalDispenedAmount { get; set; }
        public string Frequency { get; set; }
        public Nullable<decimal> DoseAmount { get; set; }
        public string DoseUnit { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> DurationType { get; set; }
        public string RouteOfAdministration { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public Nullable<bool> AddtoMar { get; set; }
        public string Directions { get; set; }
        public Nullable<int> PharmacySource { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> FrequencyStatus { get; set; }
        public string Note { get; set; }
        public Nullable<bool> AddSendtoReceiveOrders { get; set; }
        public Nullable<int> IsDisplayOnDashboard { get; set; }
        public string PhysicianName { get; set; }
        public Nullable<int> SigForm { get; set; }
        public string SigCode { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> SendToKrollSuccess { get; set; }
        public Nullable<int> ActualQuantity { get; set; }
    }
}
