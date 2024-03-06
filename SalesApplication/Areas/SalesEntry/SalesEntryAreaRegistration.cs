using System.Web.Mvc;

namespace SalesApplication.Areas.SalesEntry
{
    public class SalesEntryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SalesEntry";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(null, "SalesList", new { action = "List", controller = "SalesEntry", id = UrlParameter.Optional });
            context.MapRoute(null, "SaveSalesDetails", new { action = "SaveSalesDetails", controller= "SalesEntry", id = UrlParameter.Optional });
            context.MapRoute(null, "ViewSaleDetailsById", new { action = "ViewSaleDetailsById", controller= "SalesEntry", id = UrlParameter.Optional });
            context.MapRoute(null, "EditSaleDetailsById", new { action = "EditSaleDetailsById", controller = "SalesEntry", id = UrlParameter.Optional });
            context.MapRoute(null, "BindCity", new { action = "BindCity", controller = "SalesEntry", id = UrlParameter.Optional });

            context.MapRoute(null, "SalesEntry/RenewalList", new { action = "RenewalList", controller = "SalesEntry", id = UrlParameter.Optional });
            context.MapRoute(null, "EditRenewalSaleDetailsById", new { action = "EditRenewalSaleDetailsById", controller = "SalesEntry", id = UrlParameter.Optional });
            context.MapRoute(null, "ViewSaleRenewalDetailsById", new { action = "ViewSaleRenewalDetailsById", controller = "SalesEntry", id = UrlParameter.Optional });
            context.MapRoute(null, "SalesEntry/SaveSalesRenewalDetails", new { action = "SaveSalesRenewalDetails", controller = "SalesEntry", id = UrlParameter.Optional });


            context.MapRoute(null, "ApproveList", new { action = "List", controller = "Approve", id = UrlParameter.Optional });
            context.MapRoute(null, "ApproveRejectDetails", new { action = "ApproveRejectSale", controller= "Approve", id = UrlParameter.Optional });
            context.MapRoute(null, "GetSaleDetailsById", new { action = "GetSaleDetailsById", controller= "Approve", id = UrlParameter.Optional });

            context.MapRoute(null, "ApproverRenewalList", new { action = "ApproverRenewalList", controller = "Approve", id = UrlParameter.Optional });
            context.MapRoute(null, "EditRenewalDetailsById", new { action = "EditRenewalDetailsById", controller = "Approve", id = UrlParameter.Optional });

            context.MapRoute(null, "ViewerList", new { action = "List", controller = "Viewer", id = UrlParameter.Optional });
            context.MapRoute(null, "RenewalList", new { action = "RenewalList", controller = "Viewer", id = UrlParameter.Optional });

            context.MapRoute(null, "Approve/Invoice", new { action = "Invoice", controller = "Approve", id = UrlParameter.Optional });
            context.MapRoute(null, "Approve/GenarateInvoice", new { action = "GenarateInvoice", controller = "Approve", id = UrlParameter.Optional });
            
            context.MapRoute(null, "Approve/SmsTopup", new { action = "SmsTopup", controller = "Approve", id = UrlParameter.Optional });
            context.MapRoute(null, "Approve/ApproveSMSTopUp", new { action = "ApproveSMSTopUp", controller = "Approve", id = UrlParameter.Optional });

            context.MapRoute(null, "Approve/AppRenewal", new { action = "AppRenewal", controller = "Approve", id = UrlParameter.Optional });
            context.MapRoute(null, "Approve/ApproveAppRenewal", new { action = "ApproveAppRenewal", controller = "Approve", id = UrlParameter.Optional });

            context.MapRoute(null, "Finance/List", new { action = "List", controller = "Finance", id = UrlParameter.Optional });
            context.MapRoute(null, "Finance/DownLoad", new { action = "DownLoad", controller = "Finance", id = UrlParameter.Optional });

            context.MapRoute(null, "SendMailMessage", new { action = "SendMailMessage", controller = "Approve", id = UrlParameter.Optional });


            context.MapRoute(null, "manageinvoice", new { action = "Edit", controller = "Invoice", id = UrlParameter.Optional });
            context.MapRoute(null, "getservicetypes", new { action = "GetServiceType", controller = "Invoice", id = UrlParameter.Optional });
            context.MapRoute(null, "getcompanies", new { action = "GetCompanies", controller = "Invoice", id = UrlParameter.Optional });
            context.MapRoute(null, "deleteinvoicelineitem", new { action = "DeleteInvoiceLineItem", controller = "Invoice", id = UrlParameter.Optional });

        }
    }
}