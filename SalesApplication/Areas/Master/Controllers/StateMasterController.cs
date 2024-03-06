using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesApplication.Areas.Master.Controllers
{
    public class StateMasterController : Controller
    {
        // GET: Master/State
        [HttpGet]
        public ActionResult List()
        {
            StateMaster model = new StateMaster();
            Preparelist(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult List(StateMaster model)
        {
            Preparelist(model);
            return View(model);
        }
        public void Preparelist(StateMaster model)
        {
            List<StateMaster> stateList = new List<StateMaster>();
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var allStates = from s in db.StateMasters orderby s.Id descending select s;
                    stateList = allStates != null ? allStates.ToList() : null;
                    model.StatesList = stateList;

                    if (stateList != null && stateList.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(model.Search))
                        {
                            stateList = stateList.Where(s => s.StateName.Contains(model.Search)).ToList();
                            model.StatesList = stateList;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Edit(StateMaster model)
        {
            using (SalesContainer db = new SalesContainer())
            {
                var checkstate = from s in db.StateMasters where s.StateName.Trim() == model.StateName.Trim() && s.Id != model.Id select s;
                StateMaster state = checkstate != null ? checkstate.FirstOrDefault() : null;
                if (state != null)
                {
                    TempData["Notification"] = model.StateName+ " State Name already exist";
                    return RedirectToRoute(new { action = "List", controller = "StateMaster", area = "Master" });
                }
                else
                {
                    if (model.Id > 0)
                    {
          
                        StateMaster oldModel = new StateMaster();
                        StateMaster tempModel = new StateMaster();
                        StateMaster finalModel = new StateMaster();
                        var states = from s in db.StateMasters where s.Id == model.Id select s;
                        oldModel = states != null ? states.FirstOrDefault() : null;
                        tempModel = TypeMapper.Map<StateMaster, StateMaster>(oldModel);
                        finalModel = TypeMapper.Map<StateMaster, StateMaster>(tempModel);
                        finalModel.IsActive = true;
                        finalModel.IsDeleted = false;
                        finalModel.StateName = model.StateName;
                        finalModel.ChangedBy = CurrentContext.User.UserId.ToString();
                        finalModel.ChangedByName = CurrentContext.User.UserName;
                        finalModel.ChangedOn = DateTime.Now;
                        db.StateMasters.AddOrUpdate(finalModel);
                        db.SaveChanges(); 
                        TempData["Notification"] = "State Updated Successfully";
                    }
                    else
                    {
                        model.IsActive = true;
                        model.IsDeleted = false;
                        model.CreatedBy = CurrentContext.User.UserId.ToString();
                        model.CreatedByName = CurrentContext.User.UserName;
                        model.CreatedOn = DateTime.Now;
                        db.StateMasters.AddOrUpdate(model);
                        db.SaveChanges();
 
                        TempData["Notification"] = "State Saved Successfully";
                    }
                }
                DataCache.RefreshStateMasters();
            }
            return RedirectToRoute(new { action = "List", controller = "StateMaster", area = "Master" });
        }

        //EditStateDetailsById
        public ActionResult GetStateDetailsById(int id)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    var stateDetails = from s in db.StateMasters where s.Id == id select s;
                    StateMaster empData = stateDetails != null ? stateDetails.FirstOrDefault() : null;
                    string partialview = "";
                    partialview = RenderLibrary.RenderPartialView(this, "~/Areas/Master/Views/StateMaster/_EditState.cshtml", empData);
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