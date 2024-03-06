using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication.Models
{
    public class CompanyModuleAccessDto : CompanyModuleAccess
    {
        public string ModuleCategory { get; set; }
    }
}