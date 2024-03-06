using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace SalesApplication.PagedClasses
{
    public class PagedSalesEntry:Paged
    {
        public IPagedList<SalesEntry> PagedSalesEntryList { get; set; }
        public PagedSalesEntry()
        {
            PagedSalesEntryList = new List<SalesEntry>().ToPagedList(1, 10);
        }
    }
}