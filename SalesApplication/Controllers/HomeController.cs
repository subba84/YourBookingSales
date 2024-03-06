using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SalesApplication.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}

        [HttpPost]
        //[AllowAnonymous]
        public ActionResult ValidateUser(string username, string password)
        {
            //string url = "http://localhost:1251/api/WebAPI/SaveUser"; 
            //tblUser tblUser = new tblUser();
            //tblUser.CompanyId = 1;
            ////string url = FileBasePath.BasePath;
            ////url = url + "api/ActivateApi/sendSMS";
            //client.DefaultRequestHeaders.Clear();
            //// client.DefaultRequestHeaders.Add("ActivationKey", model.ActivationKey);
            //HttpResponseMessage response = client.PostAsJsonAsync(url, tblUser).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = response.Content.ReadAsStringAsync().Result;
            //    if (result != null)
            //    {
            //        //model = JsonConvert.DeserializeObject<TicketInformation>(result);
            //    }               
            //}




            List<EmployeeInRole> userRoles = new List<EmployeeInRole>();
            using (SalesContainer db = new SalesContainer())
            {
                var data = from s in  db.Employees where s.EmailId==username && s.Password==password select s ;
                Employee employee = data != null ? data.FirstOrDefault() : null;
                if (employee!=null && !string.IsNullOrEmpty(employee.EmailId ))
                {
                    var roles = from s in db.EmployeeInRoles where s.EmployeeId == employee.Id select s;
                    userRoles = roles != null ? roles.ToList() : null;
                    FillSessions(employee, userRoles);
                    var currentRole = userRoles.OrderBy(x => x.Priority).FirstOrDefault();

                    Session["UserRoleId"] = currentRole.RoleId;
                    Session["UserRoleName"] = currentRole.RoleName;
                    Session["SMSPrice"] = SMSPrice.SMSCharges;
                    CurrentUser user = new CurrentUser();
                    user.UserName = employee.EmployeeName;
                    user.UserId = employee.Id;
                    user.EmailId = employee.EmailId;
                    user.RoleId = currentRole.RoleId.ToString();
                    user.RoleName = currentRole.RoleName;
                    CurrentContext.SetCurrentContext(user);
                    if (currentRole.RoleId == RoleIds.Admin)
                    {
                        return RedirectToRoute(new { action = "List", controller = "Reports", area = "Reports" });
                    }
                    else if (currentRole.RoleId == RoleIds.Sales)
                    {
                        return RedirectToRoute(new { action = "List", controller = "SalesEntry", area = "SalesEntry" });
                    }
                    else if (currentRole.RoleId == RoleIds.Approver)
                    {
                        return RedirectToRoute(new { action = "List", controller = "Approve", area = "SalesEntry" });
                    }
                    else if (currentRole.RoleId == RoleIds.Observer)
                    {
                        return RedirectToRoute(new { action = "List", controller = "Viewer", area = "SalesEntry" });
                    }
                    else if (currentRole.RoleId == RoleIds.Finance)
                    {
                        return RedirectToRoute(new { action = "List", controller = "Finance", area = "SalesEntry" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Message"] = "Invalid Username/Password";
                    return RedirectToAction("Index", "Home");
                }
            }

        } 

        public void FillSessions(Employee employee, List<EmployeeInRole> roles)
        {
            Session["UserId"] = employee.Id;
            Session["UserName"] = employee.EmployeeName;
            Session["EmailId"] = employee.EmailId;

            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}