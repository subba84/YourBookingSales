using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication 
{
    public class LoginModel
    {
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string ActivationKey { get; set; }
    }
}