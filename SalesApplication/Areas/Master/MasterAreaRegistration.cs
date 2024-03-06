using System.Web.Mvc;

namespace SalesApplication.Areas.Master
{
    public class MasterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Master";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(null, "EmployeesList", new { action = "List", controller = "Employee", id = UrlParameter.Optional });
            context.MapRoute(null, "GetEmployeeDetailsById", new { action = "GetEmployeeDetailsById", controller = "Employee", id = UrlParameter.Optional });
            context.MapRoute(null, "Edit", new { action = "Edit", controller = "Employee", id = UrlParameter.Optional });

            context.MapRoute(null, "StateList", new { action = "List", controller = "StateMaster", id = UrlParameter.Optional });
            context.MapRoute(null, "Edit/State", new { action = "Edit", controller = "StateMaster", id = UrlParameter.Optional });
            context.MapRoute(null, "GetStateDetailsById", new { action = "GetStateDetailsById", controller = "StateMaster", id = UrlParameter.Optional });

            context.MapRoute(null, "CityList", new { action = "List", controller = "CityMaster", id = UrlParameter.Optional });
            context.MapRoute(null, "Edit/City", new { action = "Edit", controller = "CityMaster", id = UrlParameter.Optional });
            context.MapRoute(null, "GetCityDetailsById", new { action = "GetCityDetailsById", controller = "CityMaster", id = UrlParameter.Optional });

            context.MapRoute(null, "CompanyModuleAccess", new { action = "CompanyModuleAccessList", controller = "ManageCompanyModuleAccess", id = UrlParameter.Optional });
            context.MapRoute(null, "CompanyAccessList", new { action = "CompanyAccessList", controller = "ManageCompanyModuleAccess", id = UrlParameter.Optional });
            context.MapRoute(null, "SaveCompanyAccess", new { action = "SaveCompanyAccess", controller = "ManageCompanyModuleAccess", id = UrlParameter.Optional });
        }
    }
}