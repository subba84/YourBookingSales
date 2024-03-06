using System.Web.Mvc;

namespace SalesApplication.Areas.Reports
{
    public class ReportsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(null, "SalesReportList", new { action = "List", controller = "Reports", id = UrlParameter.Optional });

        }
    }
}