using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.Master.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Master/Employee
        [HttpGet]
        public ActionResult List()
        {
           Employee model = new Employee();
            Preparelist(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult List(Employee model)
        {
            Preparelist(model);
            return View(model);
        }
        public void Preparelist(Employee model)
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var employees = from s in db.Employees  orderby s.Id descending  select s;
                    employeeList = employees != null ? employees.ToList() : null;
                    model.EmployeeList = employeeList;
               
                if(employeeList!=null && employeeList.Count()>0)
                {
                    if(!string.IsNullOrEmpty( model.Search))
                    {
                        employeeList= employeeList.Where(s=>s.EmailId.Contains(model.Search)).ToList();
                        model.EmployeeList = employeeList;
                    }
                    if (model.EmployeeList!=null && model.EmployeeList.Count()>0)
                    {
                        foreach(var item in model.EmployeeList)
                        {
                                var employeerole = from s in db.EmployeeInRoles where s.EmployeeId == item.Id select s;
                                item.EmployeeRole = employeerole != null ? employeerole.FirstOrDefault() : null;
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

        public ActionResult Edit(Employee model)
        {
            using (SalesContainer db = new SalesContainer())
            {
                EmployeeInRole tempEmplyeeInRoleModel = new EmployeeInRole();
                var checkEmployee = from s in db.Employees where s.EmailId.Trim() == model.EmailId.Trim() && s.Id!=model.Id select s;
                Employee emp = checkEmployee != null ? checkEmployee.FirstOrDefault():null;
                if(emp!=null)
                {
                    TempData["Notification"] = "Employee MailId already exist";
                    return RedirectToRoute(new { action = "List", controller = "Employee", area = "Master" });
                }
                else
                {
                    if (model.Id > 0)
                    {
                      var employeeRole = from s in db.EmployeeInRoles where s.EmployeeId == model.Id select s;
                        EmployeeInRole empRole = employeeRole != null ? employeeRole.FirstOrDefault() : new EmployeeInRole();

                        EmployeeInRole oldEmplyeeInRoleModel = new EmployeeInRole();

                        oldEmplyeeInRoleModel = TypeMapper.Map<EmployeeInRole, EmployeeInRole>(empRole);
                        tempEmplyeeInRoleModel = TypeMapper.Map<EmployeeInRole, EmployeeInRole>(oldEmplyeeInRoleModel);
                        Employee oldModel = new Employee();
                        Employee tempModel = new Employee();
                        Employee finalModel = new Employee();
                        var employee = from s in db.Employees where s.Id == model.Id select s;
                        oldModel = employee != null ? employee.FirstOrDefault() : null;
                        tempModel = TypeMapper.Map<Employee, Employee>(oldModel);
                        finalModel = TypeMapper.Map<Employee, Employee>(tempModel);
                        finalModel.IsActive = true;
                        finalModel.IsDeleted = false;
                        finalModel.MobileNumber = model.MobileNumber;
                        finalModel.EmployeeName = model.EmployeeName;
                        finalModel.EmailId = model.EmailId;
                        finalModel.Password = model.Password;
                        finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        finalModel.ChangedByName = CurrentContext.User.UserName;
                        finalModel.ChangedOn = DateTime.Now; 
                        db.Employees.AddOrUpdate(finalModel);
                        db.SaveChanges();

                        tempEmplyeeInRoleModel.EmployeeName = model.EmployeeName;
                        tempEmplyeeInRoleModel.RoleId = model.EmployeeRole.RoleId;
                        tempEmplyeeInRoleModel.RoleName = model.EmployeeRole.RoleName;
                        tempEmplyeeInRoleModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        tempEmplyeeInRoleModel.ChangedByName = CurrentContext.User.UserName;
                        tempEmplyeeInRoleModel.ChangedOn = DateTime.Now;
                        db.EmployeeInRoles.AddOrUpdate(tempEmplyeeInRoleModel);
                        db.SaveChanges();

                        TempData["Notification"] = "Employee Updated Successfully";
                    }
                    else
                    {
                        model.IsActive = true;
                        model.IsDeleted = false;
                        model.CreatedBy = CurrentContext.User.UserId.ToString();
                        model.CreateByName = CurrentContext.User.UserName;
                        model.CreatedOn = DateTime.Now;
                        db.Employees.AddOrUpdate(model);
                        db.SaveChanges();

                        tempEmplyeeInRoleModel.EmployeeName = model.EmployeeName;
                        tempEmplyeeInRoleModel.EmployeeId = model.Id;
                        tempEmplyeeInRoleModel.RoleId = model.EmployeeRole.RoleId;
                        tempEmplyeeInRoleModel.RoleName = model.EmployeeRole.RoleName;
                        tempEmplyeeInRoleModel.CreatedBy = CurrentContext.User.UserId.ToString();
                        tempEmplyeeInRoleModel.CreatedByName = CurrentContext.User.UserName;
                        tempEmplyeeInRoleModel.ChangedOn = DateTime.Now;
                        tempEmplyeeInRoleModel.IsActive = true;
                        tempEmplyeeInRoleModel.IsDeleted = false;
                        db.EmployeeInRoles.AddOrUpdate(tempEmplyeeInRoleModel);
                        db.SaveChanges();

                        TempData["Notification"] = "Employee Saved Successfully";
                    }
                }
                DataCache.RefreshEmployees();
                DataCache.RefreshEmployeeInRoless();
            }
            return RedirectToRoute(new { action = "List", controller = "Employee", area = "Master" });
        }

        public ActionResult GetEmployeeDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var employeeeDetails = from s in db.Employees where s.Id == id select s;
                    Employee empData = employeeeDetails != null ? employeeeDetails.FirstOrDefault() : null;

                    var employeeRole = from s in db.EmployeeInRoles where s.EmployeeId ==id select s;
                    empData.EmployeeRole = employeeRole != null ? employeeRole.FirstOrDefault() : null;

                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/Master/Views/Employee/_EditEmployee.cshtml", empData);
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