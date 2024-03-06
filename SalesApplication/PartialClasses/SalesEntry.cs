using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication
{
    public partial class SalesEntry : Paged
    {
        public List<SalesEntry> salesList { get; set; }
        public List<RenewalHistoryView> RenewalHistoryList { get; set; }
        public List<SalesAudit> SaleAudit { get; set; }
        public PaymentHistory paymentHistory { get; set; }
        public List<PaymentHistory> allPaymentHistory { get; set; }
        public List<RenewalHistory> renewalHistory { get; set; }
        public RenewalHistory renewalHistoryDetails { get; set; }
        public Nullable<System.DateTime> SystemDate { get; set; }
        public string ActivationMessage { get; set; }
        public int? InvoiceNumber { get; set; }
        //public int? SmsCount { get; set; }
        public string ApplicationAmount { get; set; }

        public Invoice Invoice { get; set; }
        public List<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}