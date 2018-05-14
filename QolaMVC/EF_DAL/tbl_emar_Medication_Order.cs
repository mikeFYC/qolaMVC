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
    
    public partial class tbl_emar_Medication_Order
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public string RxNumber { get; set; }
        public string OriginalRxNumber { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<int> ResidentId { get; set; }
        public Nullable<int> Physicain { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> EffectedDate { get; set; }
        public Nullable<int> OrderType { get; set; }
        public Nullable<int> DrugId { get; set; }
        public string Directions { get; set; }
        public Nullable<int> OrderSent { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> KrollHandledOnDate { get; set; }
        public Nullable<int> KrollHandledStatus { get; set; }
        public Nullable<System.DateTime> eMarHandledOnDate { get; set; }
        public Nullable<int> eMarHandledStatus { get; set; }
        public Nullable<int> eMarHandledBy { get; set; }
        public string KrollRxNumber { get; set; }
        public Nullable<bool> SendToKrollSuccess { get; set; }
        public Nullable<bool> IsDisplayReceiveOrder { get; set; }
        public string PhysicianName { get; set; }
    }
}
