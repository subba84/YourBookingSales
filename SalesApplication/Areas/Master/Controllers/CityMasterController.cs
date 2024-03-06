using SalesApplication;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.Master.Controllers
{
    public class CityMasterController : Controller
    {
        // GET: Master/CityMaster
        [HttpGet]
        public ActionResult List()
        {
            CityMaster model = new CityMaster();
            Preparelist(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult List(CityMaster model)
        {
            Preparelist(model);
            return View(model);
        }
        public void Preparelist(CityMaster model)
        {
            List<CityMaster> cityList = new List<CityMaster>();
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var allcities = from s in db.CityMasters orderby s.Id descending select s;
                    cityList = allcities != null ? allcities.ToList() : null;
                    model.CityList = cityList; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Edit(CityMaster model)
        {
            using (SalesContainer db = new SalesContainer())
            {
                var checkCity = from s in db.CityMasters where s.CityName.Trim() == model.CityName.Trim() && s.Id != model.Id select s;
                CityMaster city = checkCity != null ? checkCity.FirstOrDefault() : null;
                if (city != null)
                {
                    TempData["Notification"] = model.CityName+ " City Name already exist";
                    List(model);
                }
                else
                {
                    if (model.Id > 0)
                    {

                        CityMaster oldModel = new CityMaster();
                        CityMaster tempModel = new CityMaster();
                        CityMaster finalModel = new CityMaster();
                        var states = from s in db.CityMasters where s.Id == model.Id select s;
                        oldModel = states != null ? states.FirstOrDefault() : null;
                        tempModel = TypeMapper.Map<CityMaster, CityMaster>(oldModel);
                        finalModel = TypeMapper.Map<CityMaster, CityMaster>(tempModel);
                        finalModel.IsActive = true;
                        finalModel.IsDeleted = false;
                        finalModel.StateName = model.StateName;
                        finalModel.StateId = model.StateId;
                        finalModel.CityName = model.CityName;
                        finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        finalModel.ChangedByName = CurrentContext.User.UserName;
                        finalModel.ChangedOn = DateTime.Now;
                        db.CityMasters.AddOrUpdate(finalModel);
                        db.SaveChanges();
                        TempData["Notification"] =  "City Updated Successfully";
                    }
                    else
                    {
                        model.IsActive = true;
                        model.IsDeleted = false;
                        model.CreatedBy = CurrentContext.User.UserId.ToString();
                        model.CreatedByName = CurrentContext.User.UserName;
                        model.CreatedOn = DateTime.Now;
                        db.CityMasters.AddOrUpdate(model);
                        db.SaveChanges();

                        TempData["Notification"] =model.CityName+ " City Saved Successfully";
                    }
                }
                DataCache.RefreshCityMasters();

            }
            return RedirectToRoute(new { action = "List", controller = "CityMaster", area = "Master" });
        }

        //EditStateDetailsById
        public ActionResult GetCityDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var cityDetails = from s in db.CityMasters where s.Id == id select s;
                    CityMaster cityData = cityDetails != null ? cityDetails.FirstOrDefault() : null;
                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/Master/Views/CityMaster/_EditCity.cshtml", cityData);
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