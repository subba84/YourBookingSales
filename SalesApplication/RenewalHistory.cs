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
    
    public partial class RenewalHistory
    {
        public int Id { get; set; }
        public Nullable<int> SalesEntryId { get; set; }
        public string TransactionId { get; set; }
        public string PaidType { get; set; }
        public Nullable<int> ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        public string ServiceAmount { get; set; }
        public string OtherType { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string StatusName { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedByName { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    }
}
