using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace SalesApplication 
{
    public class PagedEmployees:Paged
    {
        public IPagedList<Employee> PagedemployeeList { get; set; }
        public PagedEmployees()
        {
            PagedemployeeList = new List<Employee>().ToPagedList(1, 10);
        }
    }
}