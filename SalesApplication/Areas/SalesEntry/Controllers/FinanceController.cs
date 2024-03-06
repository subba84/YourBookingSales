using project.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.SalesEntry.Controllers
{
    public class FinanceController : Controller
    {
        // GET: SalesEntry/Finance
        [HttpGet]
        public ActionResult List()
        {
            Invoice model = new Invoice();
            Preparelist(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult List(Invoice model)
        {
             
            Preparelist(model);
            return View(model);
        }
        public void Preparelist(Invoice model)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    string createdBy = CurrentContext.User.UserId.ToString();
                    var invoices = from s in db.Invoices where s.CreatedOn!=null orderby s.Id descending select s;
                    model.InvoiceList = invoices != null ? invoices.OrderByDescending(x => x.Id).ToList() : null;
                    if (!string.IsNullOrEmpty(model.FromDate) && !string.IsNullOrEmpty(model.ToDate))
                    {
                        //DateTime? toDate =(DateTime?) (Convert.ToDateTime(model.ToDate));
                        DateTime fromDate = DateTime.ParseExact(model.FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        DateTime toDate = DateTime.ParseExact(model.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                   
                        //model.InvoiceList = model.InvoiceList.Where(s => s.CreatedOn >= fromDate && s.CreatedOn <= toDate).ToList();
                        //model.InvoiceList = model.InvoiceList.Where(s => DbFunctions.TruncateTime(s.CreatedOn) >= (fromDate) && DbFunctions.TruncateTime(s.CreatedOn) <= (toDate)).ToList();

                        model.InvoiceList = model.InvoiceList.Where(x => DateTime.Compare(x.CreatedOn.Value.Date, fromDate) >= 0 && DateTime.Compare( toDate, x.CreatedOn.Value.Date) >= 0).ToList();
                        
                    }
                    if (model.InvoiceList != null && model.InvoiceList.Count() > 0)
                    {
                        List<int?> companyIds = model.InvoiceList.Select(x => x.SalesEntryId).Distinct().ToList();
                        if (companyIds.Any())
                        {
                            var companies = from c in db.SalesEntries
                                            where companyIds.Contains(c.Id)
                                            select c;
                            if (companies != null && companies.Count() > 0)
                            {
                                foreach (var invoice in model.InvoiceList)
                                {
                                    var company = companies.Where(c => c.Id == invoice.SalesEntryId);
                                    if(company!=null && company.Count() > 0)
                                    {
                                        invoice.CompanyName = company.First().Name;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpPost]
        public ActionResult DownLoad(int? id)
        {
            try
            {
                SalesApplication.SalesEntry saleData = new SalesApplication.SalesEntry();
                saleData.Invoice = new Invoice();
                Invoice invoice = new Invoice();
                saleData.Invoice.InvoiceLineItems = new List<InvoiceLineItem>();
                using (SalesContainer db = new SalesContainer())
                {
                    var allInvoices = from s in db.Invoices where s.Id == id select s;

                    invoice = allInvoices != null ? allInvoices.FirstOrDefault() : null;
                    if (invoice != null && invoice.Id > 0)
                    {
                        var salesdata = from s in db.SalesEntries where s.Id == invoice.SalesEntryId && s.IsActive == true select s;
                        saleData = salesdata != null ? salesdata.FirstOrDefault() : null;
                        saleData.Invoice = invoice;
                        var allInvoiceslistItems = from s in db.InvoiceLineItems where s.InvoiceId == saleData.Invoice.Id select s;
                        saleData.Invoice.InvoiceLineItems = allInvoiceslistItems != null ? allInvoiceslistItems.ToList() : null;
                    }
                }
                //Build the File Path.
                string path = GenaratInvoices(saleData);
                //string path = Path.Combine(DataCache.MapPath + "/Attachments/", "Invoice_ b34e254b-59f0-43d3-89b6-e743c23e678f.pdf");

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/octet-stream", "Invoice.pdf");
            }
            catch(Exception ex)
            {
                using (SalesContainer db = new SalesContainer())
                {
                    Error error = new Error();
                    error.Error1 = ex.StackTrace;
                    error.ErrorMessage = ex.Message;
                    error.CreatedOn = DateTime.Now;
                    db.Errors.AddOrUpdate(error);
                    db.SaveChanges();
                }
                return null;
            }
            

            //Send the File to Download.
           
        }

        public string GenaratInvoices(SalesApplication.SalesEntry saleData)
        {
            string result = string.Empty;
                string filePath = "";
            try
            {
                FileConversion fileConversion = new FileConversion();
                MailHelper mailHelper = new MailHelper();
                string partialview = "";
                partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/Finance/_GenarateInvoice.cshtml", saleData);
                filePath = fileConversion.SaveReport("PDF", partialview, "Invoice");
            return filePath;
            }
            catch (Exception ex)
            {
                result = "Failure";
                using (SalesContainer db = new SalesContainer())
                {
                    Error error = new Error();
                    error.Error1 = ex.StackTrace;
                    error.ErrorMessage = ex.Message;
                    error.CreatedOn = DateTime.Now;
                    db.Errors.AddOrUpdate(error);
                    db.SaveChanges();
                }
            }

            return filePath;
        }

    }
}