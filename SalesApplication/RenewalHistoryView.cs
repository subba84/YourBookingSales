//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class RenewalHistoryView
    {
        public int Id { get; set; }
        public Nullable<int> SalesEntryId { get; set; }
        public string TransactionId { get; set; }
        public string ServiceAmount { get; set; }
        public string PaidType { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public Nullable<System.DateTime> ActivatedUpto { get; set; }
    }
}