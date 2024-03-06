using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.SalesEntry.Controllers
{
    public class ViewerController : Controller
    {
        // GET: SalesEntry/Viewer
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
                using (SalesContainer db = new SalesContainer())
                {
                    var sales = from s in db.SalesEntries where s.IsActive == true select s;
                    model.salesList = sales != null ? sales.ToList() : null;
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
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var sales = from s in db.RenewalHistoryViews select s;
                    model.RenewalHistoryList = sales != null ? sales.ToList() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}