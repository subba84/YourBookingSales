using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication.Models
{
    public class AccessModel
    {
        public List<BookingModule> Modules { get; set; }
        public List<UserModuleAccess> UserModuleAccessList { get; set; }
        public List<CompanyModuleAccessDto> CompanyModuleAccessList { get; set; }
    }
}