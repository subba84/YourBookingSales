using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication 
{
    public partial class tblUser
    {
        public long LoginId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string Company { get; set; }
        public string Username { get; set; }
        public string Mobilenumber { get; set; }
        public string Password { get; set; }
        public Nullable<int> LoginCount { get; set; }
        public string Email { get; set; }
        public Nullable<int> SMSCount { get; set; }
    }
}