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
    
    public partial class Jobwork
    {
        public int Id { get; set; }
        public string JobType { get; set; }
        public string JobDetails { get; set; }
        public Nullable<bool> IsRunning { get; set; }
        public Nullable<bool> IsSuccess { get; set; }
        public string FailedMessage { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
    }
}
