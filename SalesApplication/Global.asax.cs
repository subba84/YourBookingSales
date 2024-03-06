using SalesApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace SalesApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DataCache.Load();
            scheduler.Start();
        }
        protected void Application_Error(object sender ,EventArgs e)
        {
            Exception exception = Server.GetLastError();            
            if (exception == null) return;
            if (!exception.Message.Contains("HTTP headers"))
            {
                LogWriter.LogWrite(exception.Message + " -- " + exception.StackTrace);
                HttpContext.Current.Response.Redirect("~/Home/Index");
            }
        }
    }
}
