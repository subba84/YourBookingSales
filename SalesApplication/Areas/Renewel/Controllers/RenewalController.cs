using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.Renewel.Controllers
{
    public class RenewalController : Controller
    {
        // GET: Master/Renewal
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
            string currentEmployyee = CurrentContext.User.UserId.ToString();
            List<SalesApplication.SalesEntry> salesList = new List<SalesApplication.SalesEntry>();
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    if(CurrentContext.User.RoleId== RoleIds.Admin.ToString())
                    {
                        var allSales = from s in db.SalesEntries where s.StatusId == SaleStatus.Approve && s.IsActive == true && s.IsDeleted == false && s.ActivatedUpto != null orderby s.Id descending select s;
                        salesList = allSales != null ? allSales.ToList() : null;
                        model.salesList = salesList;

                    }
                    else
                    {
                        var allSales = from s in db.SalesEntries where s.StatusId == SaleStatus.Approve && s.IsActive == true && s.IsDeleted == false && s.ActivatedUpto != null && s.CreatedBy== currentEmployyee orderby s.Id descending select s;
                        salesList = allSales != null ? allSales.ToList() : null;
                        model.salesList = salesList;
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

        public ActionResult ViewSaleRenewalDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;
                    var auditDetails = from s in db.SalesAudits where s.SaleEntrId == id orderby id descending select s;

                    saleData.SaleAudit = auditDetails != null ? auditDetails.ToList() : null;
                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/SalesEntry/_ViewSales.cshtml", saleData);
                    return Json(new { partialview = partialview }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult UpdateInitiator(int? SalesId, int initiatorId)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    SalesApplication.SalesEntry oldModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                    var sales = from s in db.SalesEntries where s.Id == SalesId select s;
                    oldModel = sales != null ? sales.FirstOrDefault() : null;
                    tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(oldModel);
                    finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);

                    finalModel.CreatedBy = initiatorId.ToString();
                    finalModel.CreatedByName = DataCache.Employees.Find(x=>x.Id==initiatorId).EmployeeName;
                    db.SalesEntries.AddOrUpdate(finalModel);
                    db.SaveChanges();
                     
                     
                    return Json(new { message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ViewPaymentHistory(int? SalesId)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var allPaymentHistory = from s in db.PaymentHistories where s.SalesEntryId == SalesId orderby s.Id descending  select s;
                    List<SalesApplication.PaymentHistory> paymentHistory = allPaymentHistory != null ? allPaymentHistory.ToList() : null;
                     
                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/Renewel/Views/Renewal/_ViewPaymentHistory.cshtml", paymentHistory);
                    return Json(new { partialview = partialview }, JsonRequestBehavior.AllowGet);
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}