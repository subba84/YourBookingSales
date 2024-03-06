using SalesApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.SalesEntry.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult InvoiceList()
        {
            return View();
        }

        public ActionResult Edit()
        {
            try
            {
                InvoiceModel model = new InvoiceModel();
                model.InvoiceLineItems = new List<InvoiceLineItem>();
                model.InvoiceLineItems.Add(new InvoiceLineItem());
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetServiceType()
        {
            try
            {
                using (SalesContainer container = new SalesContainer())
                {
                    var masterData = container.MasterDatas.Where(x => x.Category == "SERVICE TYPE").ToList();
                    if (masterData != null && masterData.Count() > 0)
                    {
                        return Json(masterData, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult Edit(InvoiceModel model)
        {
            try
            {
                using (SalesContainer context = new SalesContainer())
                { 
                    SalesApplication.SalesEntry company = context.SalesEntries.First(x=>x.Id == model.InvoiceDetails.CompanyId);
                    Invoice invoice = new Invoice();
                    invoice.SalesEntryId = model.InvoiceDetails.CompanyId;
                    invoice.CompanyGSTId = company.CompanyGSTId;
                    invoice.StateName = company.StateName;
                    invoice.TotalAmount = Convert.ToString(model.InvoiceLineItems.Sum(x=> Convert.ToDouble(x.SalesAmount)));
                    invoice.TotalGSTAmount = Convert.ToString(model.InvoiceLineItems.Sum(x => Convert.ToDouble(x.GSTAmount)));
                    invoice.PanNumber = company.PanNumber;
                    invoice.InvoiceType = company.InvoiceType;
                    invoice.ActivationUpto = company.ActivatedUpto;
                    invoice.OwnGSTNumber = "36AAIFD1660R1ZZ";
                    invoice.CreatedBy = CurrentContext.User.UserName;
                    invoice.CreatedOn = DateTime.Now;
                    invoice.IsActive = true;
                    model.InvoiceDetails = invoice;
                    context.Invoices.AddOrUpdate(model.InvoiceDetails);
                    context.SaveChanges();

                    if(model.InvoiceLineItems!=null && model.InvoiceLineItems.Count() > 0)
                    {
                        foreach(var item in model.InvoiceLineItems)
                        {
                            item.InvoiceId = Convert.ToInt32(model.InvoiceDetails.Id);
                            item.CreatedOn = DateTime.Now;
                            item.CompanyId = invoice.CompanyId;
                            item.CompanyName = invoice.CompanyName;
                            context.InvoiceLineItems.AddOrUpdate(item);
                            context.SaveChanges();
                        }
                    }

                    TempData["Message"] = "Invoice - " + model.InvoiceDetails.Id + " have added successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("List", new {controller = "Finance" });
        }

        public JsonResult GetCompanies()
        {
            try
            {
                using (SalesContainer container = new SalesContainer())
                {
                    var masterData = container.SalesEntries.Where(x => x.StatusName == "Approve").ToList();
                    if (masterData != null && masterData.Count() > 0)
                    {
                        return Json(masterData, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult DeleteInvoiceLineItem(int id)
        {
            try
            {
                using (SalesContainer container = new SalesContainer())
                {
                    var invoiceLineItem = container.InvoiceLineItems.First(x => x.Id == id);
                    var masterData = container.InvoiceLineItems.Remove(invoiceLineItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json("");
        }



    }
}