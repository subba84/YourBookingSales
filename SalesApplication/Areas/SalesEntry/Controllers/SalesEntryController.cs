using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace SalesApplication.Areas.SalesEntry.Controllers
{
    public class SalesEntryController : Controller
    {
        // GET: SalesEntry/SalesEntry
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
                using (SalesContainer db=new SalesContainer())
                {
                    string createdBy = CurrentContext.User.UserId.ToString();
                    var sales = from s in db.SalesEntries where s.CreatedBy== createdBy && s.IsActive==true orderby s.Id descending select s;
                    model.salesList= sales != null ? sales.OrderByDescending(x=>x.Id).ToList() : null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult SaveSalesDetails(SalesApplication.SalesEntry model)
        {
            using (SalesContainer db = new SalesContainer())
            {
                if(model.Id>0)
                {
                    SalesApplication.SalesEntry oldModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                    var sales = from s in db.SalesEntries where s.Id==model.Id select s;
                    oldModel = sales != null ? sales.FirstOrDefault() : null;
                    tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(oldModel);
                    finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);
                    model.IsActive = true;
                    model.IsDeleted = false;
                    model.Id = finalModel.Id;
                    model.StatusId = SaleStatus.Submit;
                    model.StatusName = SaleStatusName.Submit;
                    model.ChangedBy = CurrentContext.User.UserId.ToString();
                    model.ChangedByName = CurrentContext.User.UserName;
                    model.ChangedOn = DateTime.Now;
                    model.CreatedBy = finalModel.CreatedBy;
                    model.CreatedByName = finalModel.CreatedByName;
                    model.CreatedOn = finalModel.CreatedOn;
                    model.Password = finalModel.Password;
                    if(model.SMSPack=="Yes" && model.SMSAmount!=null)
                    {
                        int smscount = Convert.ToInt32(model.SMSAmount) * 2;
                        model.SMSCount = smscount;
                    }
                    else
                    {
                        model.SMSCount = 0;
                    }
                    db.SalesEntries.AddOrUpdate(model);
                    db.SaveChanges();

                    SalesAudit audit = new SalesAudit();
                    audit.SaleEntrId = model.Id;
                    audit.IsActive = true;
                    audit.IsDeleted = false;
                    audit.StatusId = SaleStatus.Submit;
                    audit.StatusName = SaleStatusName.Submit;
                    audit.EmployeeNumber = model.CreatedBy;
                    audit.EmployeeName = model.ChangedByName;
                    audit.CreatedBy = model.ChangedBy;
                    audit.CreatedByName = model.ChangedByName;
                    audit.CreatedOn = DateTime.Now;
                   
                  db.SalesAudits.AddOrUpdate(audit);
                    db.SaveChanges();
                }
                else
                {
                    model.IsActive = true;
                    model.IsDeleted = false;
                    model.StatusId = SaleStatus.Submit;
                    model.StatusName = SaleStatusName.Submit;
                    model.CreatedBy = CurrentContext.User.UserId.ToString();
                    model.CreatedByName = CurrentContext.User.UserName;
                    model.CreatedOn = DateTime.Now;
                    model.Password = model.MobileNumber;
                    if (model.SMSPack == "Yes" && model.SMSAmount != null)
                    {
                        int smscount = Convert.ToInt32(model.SMSAmount) * 2;
                        model.SMSCount = smscount;
                    }
                    else
                    {
                        model.SMSCount = 0;
                    }

                    db.SalesEntries.AddOrUpdate(model);
                    db.SaveChanges();

                    SalesAudit audit = new SalesAudit();
                    audit.SaleEntrId = model.Id;
                    audit.IsActive = true;
                    audit.IsDeleted = false;
                    audit.StatusId = SaleStatus.Submit;
                    audit.StatusName = SaleStatusName.Submit;
                    audit.EmployeeNumber = model.CreatedBy;
                    audit.EmployeeName = model.CreatedByName;
                    audit.CreatedBy = model.CreatedBy;
                    audit.CreatedByName = model.CreatedByName;
                    audit.CreatedOn = DateTime.Now;
                    db.SalesAudits.AddOrUpdate(audit);
                    db.SaveChanges();
                }

                // Save Root User in User table
                if (!string.IsNullOrEmpty(model.MobileNumber))
                {
                    // Check if already user exists with the same mobile number
                    var userDetails = db.Users.Where(x => x.MobileNumber == model.MobileNumber);
                    if(userDetails!=null && userDetails.Count() > 0)
                    {
                        // User Already Existed with same mobile number
                        User user = userDetails.First();
                        user.CompanyId = model.Id;
                        user.CompanyName = model.Name;
                        user.MobileNumber = model.MobileNumber;
                        user.Name = model.Name + " Admin";
                        user.Password = "Admin@123";
                        user.RoleName = "Company Admin";
                        user.RoleId = RoleIds.CompanyAdmin;
                        user.EmailId = model.EmailId;
                        user.CreatedBy = 0;
                        user.CreatedOn = DateTime.Now;
                        user.IsActive = true;
                        db.Users.AddOrUpdate(user);
                        db.SaveChanges();
                    }
                    else
                    {
                        User user = new User();
                        user.CompanyId = model.Id;
                        user.CompanyName = model.Name;
                        user.MobileNumber = model.MobileNumber;
                        user.Name = model.Name + " Admin";
                        user.Password = "Admin@123";
                        user.RoleName = "Company Admin";
                        user.RoleId = RoleIds.CompanyAdmin;
                        user.EmailId = model.EmailId;
                        user.CreatedBy = 0;
                        user.CreatedOn = DateTime.Now;
                        user.IsActive = true;
                        db.Users.Add(user);
                        db.SaveChanges();
                        // Add User Role Map
                        UserRoleMap userRoleMap = new UserRoleMap();
                        userRoleMap.UserId = user.Id;
                        userRoleMap.UserName = user.Name;
                        userRoleMap.RoleId = user.RoleId;
                        userRoleMap.RoleName = user.RoleName;
                        userRoleMap.CompanyId = user.CompanyId;
                        userRoleMap.BranchId = 0;
                        db.UserRoleMaps.Add(userRoleMap);
                        db.SaveChanges();
                    }
                }
                if (model.StatusId == SaleStatus.Submit)
                {
                    TempData["Notification"] = "Sales Item Submitted Successfully";
                }
            }
            return RedirectToRoute(new { action = "List", controller = "SalesEntry", area = "SalesEntry" });
        }

        public ActionResult ViewSaleDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;
                    var auditDetails = from s in db.SalesAudits where s.SaleEntrId == id select s;

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


        public ActionResult EditSaleDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;
                    var auditDetails = from s in db.SalesAudits where s.SaleEntrId == id select s; 
                    saleData.SaleAudit = auditDetails != null ? auditDetails.ToList() : null;
                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/SalesEntry/_EditSaleDetails.cshtml", saleData);
                    return Json(new { partialview = partialview }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult BindCity(int? stateId)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var allCities = from s in DataCache.CityMasters where s.StateId == stateId select s;
                    List<CityMaster> cities = allCities != null ? allCities.ToList() : null;
                      
                    return Json(new { cities = cities }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult RenewalList()
        {
            SalesApplication.SalesEntry model = new SalesApplication.SalesEntry();
            PrepareRenewalList(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult RenewalList(SalesApplication.SalesEntry model)
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
                        var allSales = from s in db.SalesEntries where s.StatusId == SaleStatus.Approve && s.IsActive == true && s.CreatedBy == currentEmployyee && s.ActivatedUpto != null orderby s.Id descending select s;
                        salesList = allSales != null ? allSales.ToList() : null;
                        model.salesList = salesList;

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


        public ActionResult EditRenewalSaleDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;
                    var PaymentHistoryDetails = from s in db.PaymentHistories where s.SalesEntryId == id orderby  id descending select s;
                    saleData.paymentHistory = PaymentHistoryDetails != null ? PaymentHistoryDetails.FirstOrDefault() : null;

                    var renewalHistoryDetails = from s in db.RenewalHistories where s.SalesEntryId == id orderby id descending select s;
                    saleData.renewalHistory  = renewalHistoryDetails != null ? renewalHistoryDetails.ToList() : null;
                    saleData.renewalHistoryDetails  = renewalHistoryDetails != null ? renewalHistoryDetails.FirstOrDefault() : null;

                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/SalesEntry/_EditRenewal.cshtml", saleData);
                    return Json(new { partialview = partialview }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult SaveSalesRenewalDetails(SalesApplication.SalesEntry model)
        {
            using (SalesContainer db = new SalesContainer())
            {
                if (model.Id > 0)
                {
                    SalesApplication.SalesEntry oldModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                    SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                    var sales = from s in db.SalesEntries where s.Id == model.Id select s;
                    oldModel = sales != null ? sales.FirstOrDefault() : null;
                    tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(oldModel);
                    finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);
                    
                    finalModel.RenewelStatusId = SaleStatus.SubmitRenewal;
                    finalModel.RenewelStatusName = SaleStatusName.RenewalSubmit;
                    finalModel.IsUnderRenewel = true;
                    finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                    finalModel.ChangedByName = CurrentContext.User.UserName;
                    finalModel.ChangedOn = DateTime.Now;
                    finalModel.ServiceAmount = model.ServiceAmount;
                    finalModel.SalesGST = model.SalesGST;

                    db.SalesEntries.AddOrUpdate(finalModel);
                    db.SaveChanges();

                    RenewalHistory audit = new RenewalHistory();
                    audit.SalesEntryId = model.Id;
                    audit.IsActive = true;
                    audit.IsDeleted = false;
                    audit.ServiceType = model.ServiceType;
                    audit.ServiceAmount = model.ServiceAmount;
                    audit.StatusId = SaleStatus.SubmitRenewal;
                    audit.StatusName = SaleStatusName.RenewalSubmit;
                    audit.TransactionId = model.TransactionId;
                    audit.PaidType = model.PaidType;
                    audit.OtherType = model.OtherType; 
                    audit.CreatedBy = CurrentContext.User.UserId.ToString();
                    audit.CreatedByName = CurrentContext.User.UserName;
                    audit.CreatedOn = DateTime.Now;
                    audit.ChangedBy = CurrentContext.User.UserId.ToString();
                    audit.ChangedByName = CurrentContext.User.UserName;
                    audit.ChangedOn = DateTime.Now;
                    db.RenewalHistories.AddOrUpdate(audit);
                    db.SaveChanges();
                }
                else
                {
                    
                    model.RenewelStatusId = SaleStatus.SubmitRenewal;
                    model.RenewelStatusName = SaleStatusName.RenewalSubmit;
                    model.ChangedBy = CurrentContext.User.UserId.ToString();
                    model.ChangedByName = CurrentContext.User.UserName;
                    model.ChangedOn = DateTime.Now;
                    db.SalesEntries.AddOrUpdate(model);
                    db.SaveChanges();

                    RenewalHistory audit = new RenewalHistory();
                    audit.SalesEntryId = model.Id;
                    audit.IsActive = true;
                    audit.IsDeleted = false;
                    audit.StatusId = SaleStatus.SubmitRenewal;
                    audit.StatusName = SaleStatusName.RenewalSubmit;
                    audit.TransactionId = model.TransactionId;
                    audit.PaidType = model.PaidType;
                    audit.OtherType = model.OtherType;
                    audit.CreatedBy = CurrentContext.User.UserId.ToString();
                    audit.CreatedByName = CurrentContext.User.UserName;
                    audit.CreatedOn = DateTime.Now;
                    audit.ChangedBy = CurrentContext.User.UserId.ToString();
                    audit.ChangedByName = CurrentContext.User.UserName;
                    audit.ChangedOn = DateTime.Now;
                    audit.ServiceType = model.ServiceType;
                    audit.ServiceAmount = model.ServiceAmount;
                    db.RenewalHistories.AddOrUpdate(audit);
                    db.SaveChanges();
                }
                if (model.RenewelStatusId == SaleStatus.SubmitRenewal)
                {
                    TempData["Notification"] = "Renewal of Sales Item Submitted Successfully";
                }
            }
            return RedirectToRoute(new { action = "RenewalList", controller = "SalesEntry", area = "SalesEntry" });
        }

        public ActionResult ViewSaleRenewalDetailsById(int id)
        
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var saleDetails = from s in db.SalesEntries where s.Id == id select s;
                    SalesApplication.SalesEntry saleData = saleDetails != null ? saleDetails.FirstOrDefault() : null;
                    var PaymentHistoryDetails = from s in db.PaymentHistories where s.SalesEntryId == id orderby s.Id descending select s;
                    saleData.paymentHistory = PaymentHistoryDetails != null ? PaymentHistoryDetails.FirstOrDefault() : null;

                    var renewalHistoryDetails = from s in db.RenewalHistories where s.SalesEntryId == id orderby s.Id descending select s;
                    saleData.renewalHistory = renewalHistoryDetails != null ? renewalHistoryDetails.ToList() : null;
                    saleData.renewalHistoryDetails = renewalHistoryDetails != null ? renewalHistoryDetails.FirstOrDefault() : null;
                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/SalesEntry/Views/SalesEntry/_ViewRenewal.cshtml", saleData);
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