using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication 
{
    public partial class Invoice :Paged
    {
        public List<Invoice> InvoiceList { get; set; }
        public List<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}