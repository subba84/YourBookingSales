using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication.Models
{
    public class InvoiceModel
    {
        public Invoice InvoiceDetails { get;set;}
        public List<InvoiceLineItem> InvoiceLineItems { get;set;}
    }
}