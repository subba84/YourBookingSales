using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication.PagedClasses
{
    public class PagedInvoice:Paged
    {
        public IPagedList<Invoice> PagedInvoiceList { get; set; }
        public PagedInvoice()
        {
            PagedInvoiceList = new List<Invoice>().ToPagedList(1, 10);
        }
    }
}