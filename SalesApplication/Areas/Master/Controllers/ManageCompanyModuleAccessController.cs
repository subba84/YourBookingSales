using SalesApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.Master.Controllers
{
    public class ManageCompanyModuleAccessController : Controller
    {
        // GET: Master/ManageCompanyModuleAccess
        public ActionResult CompanyModuleAccessList()
        {
            try
            {
                var companies = new List<Company>();
                using (SalesContainer db = new SalesContainer())
                {
                    companies = db.SalesEntries.Where(x=>x.StatusId == SaleStatus.Approve).Select(x => new Company
                    {
                        Id = x.Id,
                        CompanyName = x.Name
                    }).DistinctBy(x=>x.Id).ToList();
                }
                return View(companies);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CompanyAccessList(int companyId)
        {
            try
            {
                AccessModel model = new AccessModel();
                Company company = new Company();
                using (SalesContainer db = new SalesContainer())
                {
                    var companies = db.SalesEntries.Where(x => x.Id == companyId);
                    if(companies!=null && companies.Count() > 0)
                    {
                        company.Id = companies.First().Id;
                        company.CompanyName = companies.First().Name;
                    }


                    var accessList = db.GetCompanyModules(companyId).Select(x => new CompanyModuleAccess
                    {
                        Id = x.Id,
                        CompanyId = (int)x.CompanyId,
                        CompanyName = x.CompanyName,
                        ModuleId = (int)x.ModuleId,
                        ModuleName = x.ModuleName,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        IsHavingAccess = x.IsHavingAccess ?? false
                    }).ToList();
                    //model.CompanyModuleAccessList = TypeMapper.Map<CompanyModuleAccess, CompanyModuleAccessDto>(accessList);
                    model.CompanyModuleAccessList = new List<CompanyModuleAccessDto>();
                    model.Modules = db.GetBookingModules().Select(x => new BookingModule
                    {
                        Id = x.Id,
                        ModuleCategory = x.ModuleCategory,
                        ModuleName = x.ModuleName
                    }).ToList();

                    if (model.Modules != null && model.Modules.Count > 0)
                    {
                        foreach (var item in model.Modules)
                        {
                            CompanyModuleAccessDto access = new CompanyModuleAccessDto();
                            access.ModuleId = item.Id;
                            access.ModuleName = item.ModuleName;
                            access.ModuleCategory = item.ModuleCategory;
                            access.CompanyId = companyId;
                            access.CompanyName = company.CompanyName;
                            var accessDetails = accessList.Where(x => x.CompanyId == access.CompanyId && x.ModuleId == access.ModuleId);
                            if (accessDetails != null && accessDetails.Count() > 0)
                            {
                                access.IsHavingAccess = accessDetails.First().IsHavingAccess;
                                access.Id = accessDetails.First().Id;
                            }
                            else
                            {
                                access.IsHavingAccess = false;
                            }
                            model.CompanyModuleAccessList.Add(access);
                        }
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SaveCompanyAccess(AccessModel model)
        {
            try
            {
                using(SalesContainer db=new SalesContainer())
                {
                    if (model.CompanyModuleAccessList != null && model.CompanyModuleAccessList.Count() > 0)
                    {
                        foreach (CompanyModuleAccess companyModuleAccess in model.CompanyModuleAccessList)
                        {
                            if (companyModuleAccess.Id > 0)
                                db.USP_ManageCompanyModule(companyModuleAccess.Id, companyModuleAccess.CompanyId, companyModuleAccess.CompanyName, companyModuleAccess.ModuleId, companyModuleAccess.ModuleName, companyModuleAccess.FromDate, companyModuleAccess.ToDate, companyModuleAccess.IsHavingAccess, "Update");
                            else
                                db.USP_ManageCompanyModule(companyModuleAccess.Id, companyModuleAccess.CompanyId, companyModuleAccess.CompanyName, companyModuleAccess.ModuleId, companyModuleAccess.ModuleName, companyModuleAccess.FromDate, companyModuleAccess.ToDate, companyModuleAccess.IsHavingAccess, "Add");
                        }
                    }

                    var companyId = model.CompanyModuleAccessList[0].CompanyId;
                    var userDetails = db.Users.Where(x => x.CompanyId == companyId && x.RoleId == RoleIds.CompanyAdmin);
                    if(userDetails!=null && userDetails.Count() > 0)
                    {
                        User user = userDetails.First();
                        List<UserModuleAccess> userownedmodules = new List<UserModuleAccess>();
                        var userModuleAccessList = db.UserModuleAccesses.Where(x => x.UserId == user.Id);
                        if (userModuleAccessList != null && userModuleAccessList.Count() > 0)
                        {
                            userModuleAccessList = userModuleAccessList.Where(x=>x.IsHavingAccess == true);
                            if (userModuleAccessList != null && userModuleAccessList.Count() > 0)
                            {
                                userownedmodules = userModuleAccessList.ToList();
                            }
                        }

                        foreach (var module in model.CompanyModuleAccessList.ToList())
                        {
                            if (module.IsHavingAccess == true)
                            {
                                if(!userownedmodules.Select(x=>x.ModuleId).Contains(module.ModuleId))
                                {
                                    UserModuleAccess userModuleAccess = new UserModuleAccess();
                                    userModuleAccess.UserId = user.Id;
                                    userModuleAccess.UserName = user.Name;
                                    userModuleAccess.CompanyId = module.CompanyId;
                                    userModuleAccess.ModuleId = module.ModuleId;
                                    userModuleAccess.IsHavingAccess = module.IsHavingAccess;
                                    db.USP_ManageUserModuleAccess(userModuleAccess.Id, userModuleAccess.UserId, userModuleAccess.UserName, userModuleAccess.ModuleId, userModuleAccess.IsHavingAccess, userModuleAccess.CompanyId, userModuleAccess.BranchId, "Add");
                                }
                            }
                            else
                            {
                                if (userownedmodules.Select(x => x.ModuleId).Contains(module.ModuleId))
                                {
                                    UserModuleAccess userModuleAccess = userownedmodules.First(x => x.ModuleId == module.ModuleId);
                                    db.USP_ManageUserModuleAccess(userModuleAccess.Id, 0, string.Empty, 0, false, 0, 0, "Delete");
                                }
                            }
                        }

                        //var companyownedmodules = model.CompanyModuleAccessList.Where(x => x.IsHavingAccess == true);
                        //if (companyownedmodules != null && companyownedmodules.Count() > 0)
                        //{
                            
                        //}





                        ////Delete User Module Access
                        //if (userModuleAccessList!=null && userModuleAccessList.Count() > 0)
                        //{
                            





                        //    foreach (var userModuleAccess in userModuleAccessList.ToList())
                        //    {
                        //        db.USP_ManageUserModuleAccess(userModuleAccess.Id, 0, string.Empty, 0, false, 0, 0, "Delete");
                        //    }
                        //}

                        //// Save User Module Access
                        //if (model.CompanyModuleAccessList != null && model.CompanyModuleAccessList.Count() > 0)
                        //{
                        //    foreach (CompanyModuleAccess companyModuleAccess in model.CompanyModuleAccessList)
                        //    {
                        //        UserModuleAccess userModuleAccess = new UserModuleAccess();
                        //        userModuleAccess.UserId = user.Id;
                        //        userModuleAccess.UserName = user.Name;
                        //        userModuleAccess.CompanyId = companyModuleAccess.CompanyId;
                        //        userModuleAccess.ModuleId = companyModuleAccess.ModuleId;
                        //        userModuleAccess.IsHavingAccess = companyModuleAccess.IsHavingAccess;
                        //        db.USP_ManageUserModuleAccess(userModuleAccess.Id, userModuleAccess.UserId, userModuleAccess.UserName, userModuleAccess.ModuleId, userModuleAccess.IsHavingAccess, userModuleAccess.CompanyId, userModuleAccess.BranchId, "Add");
                        //    }
                        //}
                    }



                }
                TempData["Notification"] = "Company Module Access Saved Successfully";
                return RedirectToAction("CompanyModuleAccessList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}