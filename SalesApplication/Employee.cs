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
    
    public partial class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string CreateByName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string DeleteBy { get; set; }
        public string DeleteByName { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    }
}
