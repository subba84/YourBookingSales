using project.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.SalesEntry.Controllers
{
    public class ApproveController : Controller
    {
        // GET: SalesEntry/Approve
        [HttpGet]
        public ActionResult List()
        {
            SalesApplication.SalesEntry model = new SalesApplication.SalesEntry();
            Preparelist(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult List(SalesApplication.SalesEntry model)
        {
            Preparelist(model);
            return View(model);
        }
        public void Preparelist(SalesApplication.SalesEntry model)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var sales = from s in db.SalesEntries where s.StatusId == SaleStatus.Submit && s.IsActive == true orderby s.Id descending select s;
                    model.salesList = sales != null ? sales.ToList() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetSaleDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;
                    var auditDetails = from s in db.SalesAudits where s.SaleEntrId == id && s.IsActive == true select s;

                    saleData.SaleAudit = auditDetails != null ? auditDetails.ToList() : null;
                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/Approve/_ApproveReject.cshtml", saleData);
                    return Json(new { partialview = partialview }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public ActionResult ApproveRejectSale(int id, int status, string comments)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;

                    tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(saleData);
                    finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);

                    if (status == SaleStatus.Approve)
                    {
                        finalModel.StatusId = SaleStatus.Approve;
                        finalModel.StatusName = SaleStatusName.Approve;
                        finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        finalModel.ChangedByName = CurrentContext.User.UserName;
                        finalModel.ChangedOn = DateTime.Now;
                        Random rnd = new Random();
                        int myRandomNo = rnd.Next(10000000, 99999999);
                        finalModel.ActivationKey = myRandomNo.ToString();
                        finalModel.ActivatedUpto = DateTime.Now.AddDays(370);
                        db.SalesEntries.AddOrUpdate(finalModel);
                        db.SaveChanges();

                        SalesAudit audit = new SalesAudit();
                        audit.SaleEntrId = saleData.Id;
                        audit.IsActive = true;
                        audit.IsDeleted = false;
                        audit.StatusId = SaleStatus.Approve;
                        audit.StatusName = SaleStatusName.Approve;
                        audit.EmployeeNumber = saleData.ChangedBy;
                        audit.EmployeeName = saleData.ChangedByName;
                        audit.CreatedBy = saleData.ChangedBy;
                        audit.CreatedByName = saleData.ChangedByName;
                        audit.CreatedOn = DateTime.Now;
                        audit.Comments = comments;
                        db.SalesAudits.AddOrUpdate(audit);
                        db.SaveChanges();

                        PaymentHistory paymentHistory = new PaymentHistory();
                        paymentHistory.SalesEntryId = finalModel.Id;
                        paymentHistory.IsActive = true;
                        paymentHistory.IsDeleted = false;
                        paymentHistory.CreatedBy = saleData.ChangedBy;
                        paymentHistory.CreatedByName = saleData.ChangedByName;
                        paymentHistory.CreatedOn = DateTime.Now;
                        paymentHistory.ChangedBy = saleData.ChangedBy;
                        paymentHistory.ChangedByName = saleData.ChangedByName;
                        paymentHistory.ChangedOn = DateTime.Now;
                        paymentHistory.PaidType = finalModel.PaidType;
                        paymentHistory.OtherType = finalModel.OtherType;
                        //paymentHistory.ServiceTypeId = finalModel.ServiceTypeId;
                        paymentHistory.ServiceTypeName = "New Sales";
                        paymentHistory.ServiceAmount = finalModel.TotalAmount;
                        paymentHistory.ActivatedUpto = finalModel.ActivatedUpto;
                        paymentHistory.TransactionId = finalModel.TransactionId;
                        db.PaymentHistories.AddOrUpdate(paymentHistory);
                        db.SaveChanges();

                        Invoice invoice = new Invoice();
                        throw new Exception("log");
                        invoice.SalesEntryId = finalModel.Id;
                        invoice.CompanyGSTId = finalModel.CompanyGSTId;
                        invoice.StateName = finalModel.StateName;
                        invoice.TotalAmount = finalModel.TotalAmount;
                        invoice.TotalGSTAmount = finalModel.TotalGSTAmount;
                        invoice.PanNumber = finalModel.PanNumber;
                        invoice.InvoiceType = finalModel.InvoiceType;
                        invoice.ActivationUpto = finalModel.ActivatedUpto;
                        invoice.OwnGSTNumber = "36AAIFD1660R1ZZ";
                        invoice.CreatedBy = CurrentContext.User.UserName;
                        invoice.CreatedOn = DateTime.Now;
                        invoice.IsActive = true;
                        db.Invoices.AddOrUpdate(invoice);
                        db.SaveChanges();

                        InvoiceLineItem invoiceLineItem = new InvoiceLineItem();
                        invoiceLineItem.InvoiceId = invoice.Id;
                        invoiceLineItem.ServiceType = "Offline Ticket Booking Service";
                        invoiceLineItem.SalesAmount = finalModel.ServiceAmount;
                        invoiceLineItem.GSTAmount = finalModel.SalesGST;
                        invoiceLineItem.CreatedOn = DateTime.Now;
                        invoiceLineItem.CreatedBy = CurrentContext.User.UserName;
                        invoiceLineItem.IsActive = true;
                        invoiceLineItem.IsDeleted = false;
                        db.InvoiceLineItems.AddOrUpdate(invoiceLineItem);
                        db.SaveChanges();

                        if (finalModel.SMSPack == "Yes")
                        {
                            InvoiceLineItem invoiceLineItem1 = new InvoiceLineItem();
                            invoiceLineItem1.InvoiceId = invoice.Id;
                            invoiceLineItem1.ServiceType = "SMS Service";
                            invoiceLineItem1.SalesAmount = finalModel.ServiceAmount;
                            invoiceLineItem1.GSTAmount = finalModel.SalesGST;
                            invoiceLineItem1.CreatedOn = DateTime.Now;
                            invoiceLineItem1.CreatedBy = CurrentContext.User.UserName;
                            invoiceLineItem1.IsActive = true;
                            invoiceLineItem1.IsDeleted = false;
                            db.InvoiceLineItems.AddOrUpdate(invoiceLineItem1);
                            db.SaveChanges();
                        }
                        finalModel.InvoiceNumber = invoice.Id;
                        string result = GenarateInvoice(finalModel);



                    }
                    else
                    {
                        finalModel.StatusId = SaleStatus.Reject;
                        finalModel.StatusName = SaleStatusName.Reject;
                        finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        finalModel.ChangedByName = CurrentContext.User.UserName;
                        finalModel.ChangedOn = DateTime.Now;
                        db.SalesEntries.AddOrUpdate(finalModel);
                        db.SaveChanges();

                        SalesAudit audit = new SalesAudit();
                        audit.SaleEntrId = saleData.Id;
                        audit.IsActive = true;
                        audit.IsDeleted = false;
                        audit.StatusId = SaleStatus.Reject;
                        audit.StatusName = SaleStatusName.Reject;
                        audit.EmployeeNumber = saleData.ChangedBy;
                        audit.EmployeeName = saleData.ChangedByName;
                        audit.CreatedBy = saleData.ChangedBy;
                        audit.CreatedByName = saleData.ChangedByName;
                        audit.CreatedOn = DateTime.Now;
                        audit.Comments = comments;
                        db.SalesAudits.AddOrUpdate(audit);
                        db.SaveChanges();
                    }
                }
                if (status == SaleStatus.Approve)
                {
                    TempData["Notification"] = "Sales Item Approved Successfully";
                }
                else if (status == SaleStatus.Reject)
                {
                    TempData["Notification"] = "Sales Item Rejected Successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //  return RedirectToAction("List", "Approve");
            return RedirectToRoute(new { action = "List", controller = "Approve", area = "SalesEntry" });
        }


        public string GenarateInvoice(SalesApplication.SalesEntry saleData)
        {
            string result = string.Empty;
            try
            {
                FileConversion fileConversion = new FileConversion();
                MailHelper mailHelper = new MailHelper();
                string partialview = "";
                string filePath = "";
                partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/Approve/_InvoiceHtml.cshtml", saleData);
                filePath = fileConversion.SaveReport("PDF", partialview, "Invoice");
                 mailHelper.SendEmail3("support@databricks.online", "rrk553@gmail.com", "Please find the Invoice ", "Invoice", filePath);
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Failure";
            }

            return result;

        }

        [HttpGet]
        public ActionResult ApproverRenewalList()
        {
            SalesApplication.SalesEntry model = new SalesApplication.SalesEntry();
            PrepareRenewalList(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult ApproverRenewalList(SalesApplication.SalesEntry model)
        {
            PrepareRenewalList(model);
            return View(model);
        }

        public void PrepareRenewalList(SalesApplication.SalesEntry model)
        {
            string currentEmployyee = CurrentContext.User.UserId.ToString();
            List<SalesApplication.SalesEntry> salesList = new List<SalesApplication.SalesEntry>();
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var allSales = from s in db.SalesEntries where s.RenewelStatusId == SaleStatus.SubmitRenewal && s.IsActive == true orderby s.Id descending select s;
                    salesList = allSales != null ? allSales.ToList() : null;
                    model.salesList = salesList;
                    if (model != null && model.salesList != null && model.salesList.Count() > 0)
                    {
                        foreach (var item in model.salesList)
                        {
                            var renewalHistory = from s in db.RenewalHistories
                                                 where s.SalesEntryId == item.Id
                                                 orderby s.Id descending
                                                 select s;
                            item.renewalHistoryDetails = renewalHistory != null ? renewalHistory.FirstOrDefault() : null;
                        }
                    }


                    if (salesList != null && salesList.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(model.Search))
                        {
                            salesList = salesList.Where(s => s.MobileNumber.Contains(model.Search)).ToList();
                            model.salesList = salesList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult EditRenewalDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;
                    var PaymentHistoryDetails = from s in db.PaymentHistories where s.SalesEntryId == id orderby id descending select s;
                    saleData.paymentHistory = PaymentHistoryDetails != null ? PaymentHistoryDetails.FirstOrDefault() : null;

                    var renewalHistoryDetails = from s in db.RenewalHistories where s.SalesEntryId == id orderby id descending select s;
                    saleData.renewalHistory = renewalHistoryDetails != null ? renewalHistoryDetails.ToList() : null;
                    saleData.renewalHistoryDetails = renewalHistoryDetails != null ? renewalHistoryDetails.FirstOrDefault() : null;

                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/Approve/_ApproveRejectRenewal.cshtml", saleData);
                    return Json(new { partialview = partialview }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ApproveRejectRenewal(int id, int status, string comments)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    RenewalHistory renewalHistory = new RenewalHistory();
                    SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;

                    tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(saleData);
                    finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);

                    var renewalHistoryDetails = from s in db.RenewalHistories
                                                where s.SalesEntryId == id
                                                orderby id descending
                                                select s;
                    renewalHistory = renewalHistoryDetails != null ? renewalHistoryDetails.FirstOrDefault() : null;


                    if (status == SaleStatus.ApproveRenewal)
                    {
                        finalModel.StatusId = SaleStatus.Approve;
                        finalModel.StatusName = SaleStatusName.Approve;
                        finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        finalModel.ChangedByName = CurrentContext.User.UserName;
                        finalModel.ChangedOn = DateTime.Now;

                        finalModel.ActivatedUpto = DateTime.Now.AddDays(370);
                        if ((DateTime)(finalModel.ActivatedUpto) < DateTime.Now)
                        {
                            finalModel.ActivatedUpto = DateTime.Now;
                        }
                        finalModel.ActivatedUpto = ((DateTime)(finalModel.ActivatedUpto)).AddDays(370);
                        //finalModel.PaidType = finalModel.PaidType;
                        //finalModel.ServiceAmount = finalModel.ServiceAmount;
                        //finalModel.PaidType = renewalHistory.PaidType;
                        //finalModel.ServiceType = renewalHistory.ServiceType;
                        //finalModel.TransactionId = finalModel.TransactionId;
                        finalModel.IsUnderRenewel = false;
                        finalModel.RenewelStatusId = SaleStatus.ApproveRenewal;
                        finalModel.RenewelStatusName = SaleStatusName.RenewalApprove;
                         
                        // if()
                        finalModel.TransactionId = renewalHistory.TransactionId;
                        db.SalesEntries.AddOrUpdate(finalModel);
                        db.SaveChanges();

                        SalesAudit audit = new SalesAudit();
                        audit.SaleEntrId = saleData.Id;
                        audit.IsActive = true;
                        audit.IsDeleted = false;
                        audit.StatusId = SaleStatus.ApproveRenewal;
                        audit.StatusName = SaleStatusName.RenewalApprove;
                        audit.EmployeeNumber = saleData.ChangedBy;
                        audit.EmployeeName = saleData.ChangedByName;
                        audit.CreatedBy = saleData.ChangedBy;
                        audit.CreatedByName = saleData.ChangedByName;
                        audit.CreatedOn = DateTime.Now;
                        audit.Comments = comments;
                        db.SalesAudits.AddOrUpdate(audit);
                        db.SaveChanges();

                        PaymentHistory paymentHistory = new PaymentHistory();
                        paymentHistory.SalesEntryId = finalModel.Id;
                        paymentHistory.IsActive = true;
                        paymentHistory.IsDeleted = false;
                        paymentHistory.CreatedBy = saleData.ChangedBy;
                        paymentHistory.CreatedByName = saleData.ChangedByName;
                        paymentHistory.CreatedOn = DateTime.Now;
                        paymentHistory.ChangedBy = saleData.ChangedBy;
                        paymentHistory.ChangedByName = saleData.ChangedByName;
                        paymentHistory.ChangedOn = DateTime.Now;
                        paymentHistory.PaidType = finalModel.PaidType;
                        paymentHistory.ServiceTypeName = "Renewal";
                        paymentHistory.OtherType = finalModel.OtherType;
                        paymentHistory.ServiceTypeId = finalModel.ServiceTypeId;
                        paymentHistory.ServiceTypeName = finalModel.ServiceType;
                        paymentHistory.ServiceAmount = finalModel.ServiceAmount;
                        paymentHistory.ActivatedUpto = finalModel.ActivatedUpto;
                        paymentHistory.TransactionId = finalModel.TransactionId;
                        db.PaymentHistories.AddOrUpdate(paymentHistory);
                        db.SaveChanges();

                        RenewalHistory RenewalAudit = new RenewalHistory();
                        RenewalAudit.SalesEntryId = finalModel.Id;
                        RenewalAudit.IsActive = true;
                        RenewalAudit.IsDeleted = false;
                        RenewalAudit.ServiceType = finalModel.ServiceType;
                        RenewalAudit.ServiceAmount = finalModel.ServiceAmount;
                        RenewalAudit.StatusId = SaleStatus.ApproveRenewal;
                        RenewalAudit.StatusName = SaleStatusName.RenewalApprove;
                        RenewalAudit.TransactionId = finalModel.TransactionId;
                        RenewalAudit.PaidType = finalModel.PaidType;
                        RenewalAudit.OtherType = finalModel.OtherType;
                        RenewalAudit.CreatedBy = CurrentContext.User.UserId.ToString();
                        RenewalAudit.CreatedByName = CurrentContext.User.UserName;
                        RenewalAudit.CreatedOn = DateTime.Now;
                        RenewalAudit.ChangedBy = CurrentContext.User.UserId.ToString();
                        RenewalAudit.ChangedByName = CurrentContext.User.UserName;
                        RenewalAudit.ChangedOn = DateTime.Now;
                        db.RenewalHistories.AddOrUpdate(RenewalAudit);
                        db.SaveChanges();


                        Invoice invoice = new Invoice();
                        invoice.SalesEntryId = finalModel.Id;
                        invoice.CompanyGSTId = finalModel.CompanyGSTId;
                        invoice.StateName = finalModel.StateName;
                        invoice.TotalAmount = (Convert.ToInt32(finalModel.ServiceAmount) + Convert.ToInt32(finalModel.SalesGST)).ToString();
                        invoice.TotalGSTAmount = finalModel.SalesGST;
                        invoice.PanNumber = finalModel.PanNumber;
                        invoice.InvoiceType = finalModel.InvoiceType;
                        invoice.ActivationUpto = finalModel.ActivatedUpto;
                        invoice.OwnGSTNumber = "36AAIFD1660R1ZZ";
                        invoice.CreatedBy = CurrentContext.User.UserName;
                        invoice.CreatedOn = DateTime.Now;
                        invoice.IsActive = true;
                        db.Invoices.AddOrUpdate(invoice);
                        db.SaveChanges();

                        InvoiceLineItem invoiceLineItem1 = new InvoiceLineItem();
                        invoiceLineItem1.InvoiceId = invoice.Id;
                        invoiceLineItem1.ServiceType = "Offline Ticket Booking Service Renewal";
                        invoiceLineItem1.SalesAmount = finalModel.ServiceAmount;
                        invoiceLineItem1.GSTAmount = finalModel.SalesGST;
                        invoiceLineItem1.CreatedOn = DateTime.Now;
                        invoiceLineItem1.CreatedBy = CurrentContext.User.UserName;
                        invoiceLineItem1.IsActive = true;
                        invoiceLineItem1.IsDeleted = false;
                        db.InvoiceLineItems.AddOrUpdate(invoiceLineItem1);
                        db.SaveChanges();

                        finalModel.Invoice = new Invoice();
                        finalModel.Invoice = invoice;
                        finalModel.Invoice.InvoiceLineItems = new List<InvoiceLineItem>();
                        invoice.InvoiceLineItems.Add(invoiceLineItem1);


                        string invoiceresult = GenarateRenewalInvoice(finalModel);

                    }
                    else
                    {
                        finalModel.RenewelStatusId = SaleStatus.RejectRenewal;
                        finalModel.RenewelStatusName = SaleStatusName.RenewalReject;
                        finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        finalModel.ChangedByName = CurrentContext.User.UserName;
                        finalModel.ChangedOn = DateTime.Now;
                        db.SalesEntries.AddOrUpdate(finalModel);
                        db.SaveChanges();

                        SalesAudit audit = new SalesAudit();
                        audit.SaleEntrId = saleData.Id;
                        audit.IsActive = true;
                        audit.IsDeleted = false;
                        audit.StatusId = SaleStatus.RejectRenewal;
                        audit.StatusName = SaleStatusName.RenewalReject;
                        audit.EmployeeNumber = saleData.ChangedBy;
                        audit.EmployeeName = saleData.ChangedByName;
                        audit.CreatedBy = saleData.ChangedBy;
                        audit.CreatedByName = saleData.ChangedByName;
                        audit.CreatedOn = DateTime.Now;
                        audit.Comments = comments;
                        db.SalesAudits.AddOrUpdate(audit);
                        db.SaveChanges();

                        RenewalHistory RenewalAudit = new RenewalHistory();
                        RenewalAudit.SalesEntryId = finalModel.Id;
                        RenewalAudit.IsActive = true;
                        RenewalAudit.IsDeleted = false;
                        RenewalAudit.ServiceType = finalModel.ServiceType;
                        RenewalAudit.ServiceAmount = finalModel.ServiceAmount;
                        RenewalAudit.StatusId = SaleStatus.RejectRenewal;
                        RenewalAudit.StatusName = SaleStatusName.RenewalReject;
                        RenewalAudit.TransactionId = finalModel.TransactionId;
                        RenewalAudit.PaidType = finalModel.PaidType;
                        RenewalAudit.OtherType = finalModel.OtherType;
                        RenewalAudit.CreatedBy = CurrentContext.User.UserId.ToString();
                        RenewalAudit.CreatedByName = CurrentContext.User.UserName;
                        RenewalAudit.CreatedOn = DateTime.Now;
                        RenewalAudit.ChangedBy = CurrentContext.User.UserId.ToString();
                        RenewalAudit.ChangedByName = CurrentContext.User.UserName;
                        RenewalAudit.ChangedOn = DateTime.Now;
                        db.RenewalHistories.AddOrUpdate(RenewalAudit);
                        db.SaveChanges();
                    }
                }
                if (status == SaleStatus.ApproveRenewal)
                {
                    TempData["Notification"] = "Renewal Item Approved Successfully";
                }
                else if (status == SaleStatus.RejectRenewal)
                {
                    TempData["Notification"] = "Renewal Item Rejected Successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //  return RedirectToAction("List", "Approve");
            return RedirectToRoute(new { action = "ApproverRenewalList", controller = "Approve", area = "SalesEntry" });
        }


        #region SMS Topup
        public ActionResult SmsTopup()
        {
            return View();
        }

        public ActionResult ApproveSMSTopUp(string mobile, string smsamount,string whatsappamount)
        {
            string result = string.Empty;
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                    var salesentries = from s in db.SalesEntries where s.MobileNumber == mobile && s.IsActive == true select s;
                    SalesApplication.SalesEntry salesEntry = salesentries != null ? salesentries.FirstOrDefault() : null;
                    if (salesEntry != null)
                    {
                        tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(salesEntry);
                        finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);
                        double smsAmount = ((Convert.ToDouble(smsamount) * 100) / (118));
                        double gst = ((smsAmount * 18) / 100);

                        //WhatsApp Amount Calculation
                        double whatsAppAmount = ((Convert.ToDouble(whatsappamount) * 100) / (118));
                        double whatsAppgst = ((whatsAppAmount * 18) / 100);
                        int whatsappcount = (int)Math.Round((Convert.ToInt32(whatsAppAmount) / 1.25), 0);
                        if (!string.IsNullOrEmpty(finalModel.WhatsAppAmount))
                        {
                            whatsAppAmount += Convert.ToDouble(finalModel.WhatsAppAmount);
                            finalModel.WhatsAppCount = (finalModel.WhatsAppCount == null ? 0 : finalModel.WhatsAppCount) + whatsappcount;
                        }

                        finalModel.WhatsAppAmount = Convert.ToString(Math.Round(whatsAppAmount,2));
                        finalModel.WhatsAppGST = Convert.ToString(Math.Round(whatsAppgst,2));

                        int count = Convert.ToInt32(smsAmount) * 2;
                        if(finalModel.SMSCount!=null && finalModel.SMSCount>0)
                        {
                            finalModel.SMSCount = finalModel.SMSCount + count;
                            finalModel.CurrentSMSAmount = Convert.ToString(Convert.ToDouble(finalModel.CurrentSMSAmount) + smsAmount);
                        }
                        else
                        {
                            finalModel.SMSCount = count;
                            finalModel.CurrentSMSAmount = Convert.ToString(smsAmount);
                        }
                       ////finalModel.SmsCount = Convert.ToInt32(smsAmount) * 2;
                        db.SalesEntries.AddOrUpdate(finalModel);
                        db.SaveChanges();

                        Invoice invoice = new Invoice();
                        invoice.SalesEntryId = finalModel.Id;
                        invoice.CompanyGSTId = finalModel.CompanyGSTId;
                        invoice.StateName = finalModel.StateName;
                        invoice.TotalAmount = smsamount;
                        invoice.TotalGSTAmount = gst.ToString();
                        invoice.PanNumber = finalModel.PanNumber;
                        invoice.InvoiceType = finalModel.InvoiceType;
                        invoice.ActivationUpto = finalModel.ActivatedUpto;
                        invoice.OwnGSTNumber = "36AAIFD1660R1ZZ";
                        invoice.CreatedBy = CurrentContext.User.UserName;
                        invoice.CreatedOn = DateTime.Now;
                        invoice.IsActive = true; 
                        db.Invoices.AddOrUpdate(invoice);
                        db.SaveChanges();

                        InvoiceLineItem invoiceLineItem1 = new InvoiceLineItem();
                        invoiceLineItem1.InvoiceId = invoice.Id;
                        invoiceLineItem1.ServiceType = "SMS Service";
                        invoiceLineItem1.SalesAmount = smsAmount.ToString();
                        invoiceLineItem1.GSTAmount = gst.ToString();
                        invoiceLineItem1.CreatedOn = DateTime.Now;
                        invoiceLineItem1.CreatedBy = CurrentContext.User.UserName;
                        invoiceLineItem1.IsActive = true;
                        invoiceLineItem1.IsDeleted = false;
                        db.InvoiceLineItems.AddOrUpdate(invoiceLineItem1);
                        db.SaveChanges();

                        finalModel.Invoice = new Invoice();
                        finalModel.Invoice = invoice;
                        finalModel.Invoice.InvoiceLineItems = new List<InvoiceLineItem>();
                        invoice.InvoiceLineItems.Add( invoiceLineItem1); 
                        
                        string invoiceresult = GenarateSMSInvoice(finalModel);

                        result = "SMS Pack Renewed Successfully";
                    }
                    else
                    {
                        result = "Please Enter Registered Mobile Number";
                    } 
                }
            }
            catch (Exception ex)
            {
                result = "Somthing Went wrong";
            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        } 
        public string GenarateSMSInvoice(SalesApplication.SalesEntry saleData)
        {
            string result = string.Empty;
            try
            {
                FileConversion fileConversion = new FileConversion();
                MailHelper mailHelper = new MailHelper();
                string partialview = "";
                string filePath = "";
                partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/Approve/_SMSInvoiceHtml.cshtml", saleData);
                filePath = fileConversion.SaveReport("PDF", partialview, "Invoice");
                 mailHelper.SendEmail3("support@databricks.online", "rrk553@gmail.com", "Please find the Invoice ", "Invoice", filePath);
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Failure";
            }

            return result;

        }

        #endregion

        #region Application Renewal
        public ActionResult AppRenewal()
        {
            return View();
        }

        public ActionResult ApproveAppRenewal(string mobile, string amount)
        {
            string result = string.Empty;
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                    var salesentries = from s in db.SalesEntries where s.MobileNumber == mobile && s.IsActive == true select s;
                    SalesApplication.SalesEntry salesEntry = salesentries != null ? salesentries.FirstOrDefault() : null;
                    if (salesEntry != null)
                    {
                        tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(salesEntry);
                        finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);

                        double salesAmount = ((Convert.ToDouble(amount) * 100) / (118));
                        double gst = ((salesAmount * 18) / 100);
                        finalModel.ServiceAmount = salesAmount.ToString();
                        finalModel.SalesGST = gst.ToString();
                        finalModel.ActivatedUpto = ((DateTime)(finalModel.ActivatedUpto)).AddDays(370);

                        db.SalesEntries.AddOrUpdate(finalModel);
                        db.SaveChanges();

                        RenewalHistory RenewalAudit = new RenewalHistory();
                        RenewalAudit.SalesEntryId = finalModel.Id;
                        RenewalAudit.IsActive = true;
                        RenewalAudit.IsDeleted = false;
                        RenewalAudit.ServiceType = finalModel.ServiceType;
                        RenewalAudit.ServiceAmount = finalModel.ServiceAmount;
                        RenewalAudit.StatusId = SaleStatus.ApproveRenewal;
                        RenewalAudit.StatusName = SaleStatusName.RenewalApprove;
                        RenewalAudit.TransactionId = finalModel.TransactionId;
                        RenewalAudit.PaidType = finalModel.PaidType;
                        RenewalAudit.OtherType = finalModel.OtherType;
                        RenewalAudit.CreatedBy = CurrentContext.User.UserId.ToString();
                        RenewalAudit.CreatedByName = CurrentContext.User.UserName;
                        RenewalAudit.CreatedOn = DateTime.Now;
                        RenewalAudit.ChangedBy = CurrentContext.User.UserId.ToString();
                        RenewalAudit.ChangedByName = CurrentContext.User.UserName;
                        RenewalAudit.ChangedOn = DateTime.Now;
                        db.RenewalHistories.AddOrUpdate(RenewalAudit);
                        db.SaveChanges();

                        Invoice invoice = new Invoice();
                        invoice.SalesEntryId = finalModel.Id;
                        invoice.CompanyGSTId = finalModel.CompanyGSTId;
                        invoice.StateName = finalModel.StateName;
                        invoice.TotalAmount = amount;
                        invoice.TotalGSTAmount = gst.ToString();
                        invoice.PanNumber = finalModel.PanNumber;
                        invoice.InvoiceType = finalModel.InvoiceType;
                        invoice.ActivationUpto = finalModel.ActivatedUpto;
                        invoice.OwnGSTNumber = "36AAIFD1660R1ZZ";
                        invoice.CreatedBy = CurrentContext.User.UserName;
                        invoice.CreatedOn = DateTime.Now;
                        invoice.IsActive = true;
                        db.Invoices.AddOrUpdate(invoice);
                        db.SaveChanges();

                        InvoiceLineItem invoiceLineItem1 = new InvoiceLineItem();
                        invoiceLineItem1.InvoiceId = invoice.Id;
                        invoiceLineItem1.ServiceType = "Offline Ticket Booking Service Renewal";
                        invoiceLineItem1.SalesAmount = salesAmount.ToString();
                        invoiceLineItem1.GSTAmount = gst.ToString();
                        invoiceLineItem1.CreatedOn = DateTime.Now;
                        invoiceLineItem1.CreatedBy = CurrentContext.User.UserName;
                        invoiceLineItem1.IsActive = true;
                        invoiceLineItem1.IsDeleted = false;
                        db.InvoiceLineItems.AddOrUpdate(invoiceLineItem1);
                        db.SaveChanges();

                        finalModel.Invoice = new Invoice();
                        finalModel.Invoice = invoice;
                        finalModel.Invoice.InvoiceLineItems = new List<InvoiceLineItem>();
                        invoice.InvoiceLineItems.Add(invoiceLineItem1);
                     
                        
                        string invoiceresult = GenarateRenewalInvoice(finalModel);

                        result = "Offline Ticket Booking Renewed Successfully";
                    }
                    else
                    {
                        result = "Please Enter Registered Mobile Number";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Somthing Went wrong";
            }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }


        public string GenarateRenewalInvoice(SalesApplication.SalesEntry saleData)
        {
            // SalesApplication.SalesEntry saleData = new SalesApplication.SalesEntry();
            string result = string.Empty;
            try
            {
                FileConversion fileConversion = new FileConversion();
                MailHelper mailHelper = new MailHelper();
                string partialview = "";
                string filePath = "";
                partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/Approve/_RenewalInvoiceHtml.cshtml", saleData);
                filePath = fileConversion.SaveReport("PDF", partialview, "Invoice");
                 mailHelper.SendEmail3("support@databricks.online", "rrk553@gmail.com", "Please find the Invoice ", "Invoice", filePath);
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Failure";
            }

            return result;

        }




        #endregion





        [HttpGet]
        public ActionResult Invoice()
        {
            SalesApplication.SalesEntry saleData = new SalesApplication.SalesEntry();
            FileConversion fileConversion = new FileConversion();
            MailHelper mailHelper = new MailHelper();
            string partialview = "";
            string filePath = "";
            partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/Approve/_InvoiceHtml.cshtml", saleData);
            filePath = fileConversion.SaveReport("PDF", partialview, "Invoice");
            mailHelper.SendEmail("support@databricks.online", "rrk553@gmail.com", "Please find the Invoice ", filePath);
            return View();

        }

        public ActionResult TestMail()
        {
            MailHelper mailHelper = new MailHelper();
            mailHelper.SendEmail2("support@databricks.online", "rrk553@gmail.com", "Test Mail ", "Test Mail ");
            return null;
        }

        public ActionResult SendSMS()
        {
            SMS sMS = new SMS();
            string result = sMS.sendMessage("9505880303", "Your ticket for Hyderabad  to Rajuhmundry on 15-05-2022 is confirmed with PNR: 72345, Fare: Rs 900 , Seat No: A2, Boarding Point: hyderabad, Dep Time: 12:00, Boarding Address: SR Nagar bus stop, operator Contact No : 9505880303, Ticket Issued By Fast tack tours and travels, Service Provider DBT");
            //string[] words = result.Split(',');
            //string smsresult = null;
            //if (words[0].Contains("success"))
            //{
            //     smsresult = "success";
            //}
            //else
            //{
            //    smsresult = "Failure";
            //}
            return null;

        }

        public ActionResult SendMailMessage(Contact contact)
        {
            MailHelper mailHelper = new MailHelper();
            mailHelper.SendEmail2("support@databricks.online", contact.EmailId, contact.Message, "Support for "+contact.Name);
            return RedirectToRoute(new { action = "List", controller = "Approve", area = "SalesEntry" });
        }
    }
}

//Your ticket for 100 on 1000 is confirmed with PNR: 7685, Fare: Rs 500 , Seat No: 2, Boarding Point: hyd, Dep Time: 12:00, Boarding Address: Hyd, operator Contact No : 9505880303, Ticket Issued By RK, Service Provider DBT	
//Sh