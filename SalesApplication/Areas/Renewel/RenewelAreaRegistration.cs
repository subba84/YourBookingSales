using System.Web.Mvc;

namespace SalesApplication.Areas.Renewel
{
    public class RenewelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Renewel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(null, "Renewal/RenewelList", new { action = "List", controller = "Renewal", id = UrlParameter.Optional });
            context.MapRoute(null, "Renewal/UpdateInitiator", new { action = "UpdateInitiator", controller = "Renewal", id = UrlParameter.Optional });
            context.MapRoute(null, "Renewal/ViewPaymentHistory", new { action = "ViewPaymentHistory", controller = "Renewal", id = UrlParameter.Optional });
        }
    }
}