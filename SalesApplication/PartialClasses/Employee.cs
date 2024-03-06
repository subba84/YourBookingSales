using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication 
{
    public partial class Employee
    {
        public List<Employee> EmployeeList { get; set; }
        public EmployeeInRole EmployeeRole{ get; set; }

        public string Search { get; set; }
    }
}