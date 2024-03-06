using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.Reports.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports/Reports

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
                List<SalesApplication.SalesEntry> saleList = new List<SalesApplication.SalesEntry>();
                List<SalesApplication.SalesEntry> finalList = new List<SalesApplication.SalesEntry>();
                using (SalesContainer db = new SalesContainer())
                {
                    var sales = from s in db.SalesEntries where s.IsActive == true && s.StatusId== SaleStatus.Approve orderby s.Id descending select s;
                    saleList = sales != null ? sales.ToList() : null;
                    model.salesList = sales != null ? sales.ToList() : null;
                }
                if (saleList != null && saleList.Count() > 0)
                {
                    if(!string.IsNullOrEmpty(model.FromDate) && !string.IsNullOrEmpty(model.ToDate))
                    {
                        //DateTime? fromDate = (DateTime?)(Convert.ToDateTime(model.FromDate));
                        //DateTime? toDate =(DateTime?) (Convert.ToDateTime(model.ToDate));
                        DateTime? fromDate = DateTime.ParseExact(model.FromDate, "MM/dd/yyyy",CultureInfo.InvariantCulture);
                        DateTime? toDate = DateTime.ParseExact(model.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


                        model.salesList = saleList.Where(s => s.CreatedOn >= fromDate && s.CreatedOn <= toDate).ToList();
                    }
                    //if (!string.IsNullOrEmpty(model.FromDate))
                    //{
                    //    if (model.Search == "7")
                    //    {
                         
                    //        foreach (var info in saleList)
                    //        {
                    //            SalesApplication.SalesEntry data = new SalesApplication.SalesEntry();
                    //            DateTime date = (DateTime)(info.CreatedOn);

                    //            if (date >= DateTime.Now.AddDays(-7))
                    //            {
                    //                finalList.Add(info);
                    //            }
                    //        }
                    //    }
                    //   else if (model.Search == "30")
                    //    {

                    //        foreach (var info in saleList)
                    //        {
                    //            SalesApplication.SalesEntry data = new SalesApplication.SalesEntry();
                    //            DateTime date = (DateTime)(info.CreatedOn);

                    //            if (date >= DateTime.Now.AddDays(-30))
                    //            {
                    //                finalList.Add(info);
                    //            }
                    //        }
                    //    }
                    //    else if (model.Search == "90")
                    //    {

                    //        foreach (var info in saleList)
                    //        {
                    //            SalesApplication.SalesEntry data = new SalesApplication.SalesEntry();
                    //            DateTime date = (DateTime)(info.CreatedOn);

                    //            if (date >= DateTime.Now.AddDays(-90))
                    //            {
                    //                finalList.Add(info);
                    //            }
                    //        }
                    //    }
                    //    model.salesList = finalList;
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}