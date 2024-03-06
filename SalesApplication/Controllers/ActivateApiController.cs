using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace SalesApplication.Controllers
{
    public class ActivateApiController : ApiController
    {
        // [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    SalesEntry entry = new SalesEntry();
                    var allSales = from s in db.SalesEntries select s;
                    entry = allSales != null ? allSales.FirstOrDefault() : null;
                    return Request.CreateResponse(HttpStatusCode.OK, entry);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        [Route("api/ActivateApi/activateApplication")]
        public HttpResponseMessage ActivateApplication(SalesEntry model)
        {

            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    if(model!=null)
                    {
                        SalesEntry entry = new SalesEntry();
                        SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                        SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                        var allSales = from s in db.SalesEntries where s.ActivationKey == model.ActivationKey && s.MobileNumber == model.MobileNumber select s;
                        entry = allSales != null ? allSales.FirstOrDefault() : null;
                        if(entry!=null)
                        {
                            tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(entry);
                            finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel);
                            if(ConvertToDateTime(DateTime.Now.Date.ToString()) == ConvertToDateTime((model.SystemDate.Value.Date).ToString()))
                            {
                                if (finalModel.SeriesNumber != null && finalModel.SeriesNumber != model.SeriesNumber)
                                {
                                    return Request.CreateResponse(HttpStatusCode.NotFound);
                                }
                                else
                                {
                                    if(finalModel.SMSCount!=null && finalModel.SMSCount > 0)
                                    {
                                        finalModel.SMSCount = finalModel.SMSCount;
                                    }
                                    else
                                    {
                                        finalModel.SMSCount = 0;
                                    }
                                    finalModel.ApplicationAmount = (Convert.ToInt32(finalModel.ServiceAmount) + Convert.ToInt32(finalModel.SalesGST)).ToString();
                                    finalModel.Password = model.Password;
                                    finalModel.SeriesNumber = model.SeriesNumber;
                                    db.SalesEntries.AddOrUpdate(finalModel);
                                    db.SaveChanges();
                                    entry = allSales != null ? allSales.FirstOrDefault() : null;
                                    return Request.CreateResponse(HttpStatusCode.OK, finalModel);
                                }
                            }
                            else
                            {
                                finalModel.ActivationMessage = "System Date Changed...";
                                return Request.CreateResponse(HttpStatusCode.OK, finalModel);
                            }

                            
                        }
                        
                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        private string ConvertToDateTime(string strDateTime)
        {
            DateTime dtFinaldate; string sDateTime;
            try { dtFinaldate = Convert.ToDateTime(strDateTime); }
            catch (Exception e)
            {
                string[] sDate = strDateTime.Split('/');
                sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                dtFinaldate = Convert.ToDateTime(sDateTime);
            }
            return dtFinaldate.ToString("dd-MM-yyyy");
        }


        [HttpPost]
        [Route("api/ActivateApi/sendSMS")]
        public HttpResponseMessage sendSMS(TicketInfo model)
        { 
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    if (model != null)
                    {
                        SMSHistory tempModel = TypeMapper.Map<SalesApplication.TicketInfo, SalesApplication.SMSHistory>(model);
                        SMSHistory finalModel = TypeMapper.Map<SalesApplication.SMSHistory, SalesApplication.SMSHistory>(tempModel); 
                        SMS sMS = new SMS();
                        string smsresult = null;
                        string result = sMS.sendMessage(finalModel.MobileNumber, "Your ticket for "+ finalModel.JourneyFrom + " to "+ finalModel.JourneyTo+ " on "+ ConvertToDateTime(finalModel.DepartureOn.ToString())+ "is confirmed with PNR: "+ finalModel.TicketNumber + ", Fare: Rs "+ model.TicketFare + " , Seat No: "+ finalModel.SeatNumbers + ", Boarding Point: "+model.BoardingPoint + ", Dep Time: "+ model.DepartureTime + ", Boarding Address: "+model.BoardingPoint+", operator Contact No : "+model.OperatorContactNumber + ", Ticket Issued By "+ model.OperatorName + ", Service Provider DBT");
                        string[] words = null;
                        if (result!=null)
                        {
                            words= result.Split(',');
                            if (words[0].Contains("success"))
                            {
                                smsresult = "success";
                            }
                        } 
                        else
                        {
                            smsresult = "Failure";
                        } 
                        if(smsresult== "success")
                        {
                            var salesEntrys = from s in db.SalesEntries where s.MobileNumber == model.SalesEntryMobileNumber && s.IsActive == true select s;
                            SalesEntry oldentry = salesEntrys != null ? salesEntrys.FirstOrDefault() : null;
                            SalesEntry tempEntry = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(oldentry);
                            SalesEntry finalEntry = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempEntry);
                            if(finalEntry.SMSCount!=null && finalEntry.SMSCount>0)
                            {
                                finalEntry.SMSCount = finalEntry.SMSCount - 1;
                                db.SalesEntries.AddOrUpdate(finalEntry);
                                db.SaveChanges();
                            } 
                            finalModel.CreatedBy = "0000";
                            finalModel.CreatedByName = "Admin";
                            finalModel.ChangedBy = "0000";
                            finalModel.ChangedByName = "Admin";
                            finalModel.CreatedOn = DateTime.Now;
                            finalModel.ChangedOn = DateTime.Now;
                            finalModel.IsSMSSent = true;
                            finalModel.IsActive = true;
                            finalModel.IsDeleted = false;
                            db.SMSHistories.AddOrUpdate(finalModel);
                            db.SaveChanges();
                           return Request.CreateResponse(HttpStatusCode.OK, finalModel);
                        } 
                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("api/ActivateApi/sendTicketEmail")]
        public HttpResponseMessage sendTicketEmail(TicketInfo model)
        {
            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    if (model != null)
                    {
                        var salesEntrys = from s in db.SalesEntries where s.MobileNumber == model.SalesEntryMobileNumber && s.IsActive == true select s;
                        string operatorEMailId = salesEntrys != null ? salesEntrys.FirstOrDefault().EmailId : null;

                        SMSHistory tempModel = TypeMapper.Map<SalesApplication.TicketInfo, SalesApplication.SMSHistory>(model);
                        SMSHistory finalModel = TypeMapper.Map<SalesApplication.SMSHistory, SalesApplication.SMSHistory>(tempModel);
                        SMS sMS = new SMS();
                        //string smsresult = null;
                        string mailids = model.EmailId != null ? model.EmailId : "";
                        if (!string.IsNullOrEmpty(model.EmailId))
                        {
                            mailids = model.EmailId;
                            if(!string.IsNullOrEmpty(model.EmailId))
                            {
                                mailids += "," + operatorEMailId;
                            }
                        }
                        else if(!string.IsNullOrEmpty(model.EmailId))
                        {
                            mailids =  operatorEMailId;
                        }
                        // mailids += model.EmailId != null ? model.EmailId: "";
                        if (mailids != null && mailids.Trim() != "")
                        {

                            //string htmlString = @"<html><body><p>Ticket Details</p><p>From :"+ finalModel.JourneyFrom +"</p> <p>To "+ " + finalModel.JourneyTo + " + "</p> </body></html>";
                            StringBuilder html = new StringBuilder();
                             html.Append("<html><body><table border='1' style='border - collapse:collapse'>");
                            html.Append("<tr><td colspan='2'>Agent</td><td colspan='2'><b>"+model.OperatorName+"</b></td>");
                            html.Append("  <td colspan='2'>Agent Number</td><td colspan='2'><b>"+model.OperatorContactNumber+"</b></td></tr>");
                            html.Append("<tr><td colspan='2'>Ticket/PNR Number</td><td colspan='6'><b>"+model.TicketNumber+"</b></td></tr>");
                            html.Append("<tr><td colspan='2'>Bus Number</td><td colspan='2'><b>"+model.BusNumber+"</b></td>");
                            html.Append("<td colspan='2'>Pickup Point</td><td colspan='2'><b>"+model.BoardingPoint+"</b></td></tr>");
                            html.Append("<tr><td colspan='2'>Date of Journey</td><td colspan='2'><b>"+ConvertToDateTime(model.DepartureOn.ToString()) + "</b></td>");
                            html.Append("<td colspan='2'>Departure Time</td><td colspan='2'><b>"+model.DepartureTime+"</b></td></tr>");
                            html.Append("<tr><td colspan='8'><b>Ticket Fare Details</b></td></tr>");
                            html.Append("<tr><td>Ticket Fare</td><td><b>"+model.TicketFare+"</b></td>");
                            html.Append("<td>GST</td><td><b>"+model.GST+"</b></td>");
                            html.Append("<td>Service Charge</td><td><b>"+model.ServiceCharge+"</b></td>");
                            html.Append("<td>Total Fare</td><td><b>"+model.TotalFare+"</b></td></tr>");
                            html.Append("<tr><td colspan='8'></td></tr>");
                            html.Append("<tr><td colspan='2'>Passenger Name(S)</td><td colspan='2'>Age</td><td colspan='2'>Gender</td><td colspan='2'>Seat Number(S)</td></tr>");
                            html.Append("<tr><td colspan='2'><b>"+model.Name+"</b></td>");
                            html.Append("<td colspan='2'><b>"+model.Age+"</b></td>");
                            html.Append("<td colspan='2'><b>" + model.Gender + "</b></td>");
                            html.Append("<td colspan='2'><b>"+model.SeatNumbers+"</b></td></tr></table></body></html>");

                            MailHelper mailHelper = new MailHelper();
                            //mailHelper.SendEmail2("support @databricks.online", mailids, "Your ticket for " + finalModel.JourneyFrom + " to " + finalModel.JourneyTo + " on " + ConvertToDateTime(finalModel.DepartureOn.ToString()) + "is confirmed with PNR: " + finalModel.TicketNumber + ", Fare: Rs " + model.TicketFare + " , Seat No: " + finalModel.SeatNumbers + ", Boarding Point: " + model.BoardingPoint + ", Dep Time: " + model.DepartureTime + ", Boarding Address: " + model.BoardingPoint + ", operator Contact No : " + model.OperatorContactNumber + ", Ticket Issued By " + model.OperatorName , "Ticket Information ");
                            mailHelper.SendEmail2("support @databricks.online", mailids, html.ToString(), "Ticket Information ");
                        } 
                        
                            return Request.CreateResponse(HttpStatusCode.OK); 
                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        #region SMS Renewal
        [HttpPost]
        [Route("api/ActivateApi/RenewSMS")]
        public HttpResponseMessage RenewSMS(SalesEntry model)
        {

            try
            {
                using (SalesContainer db = new SalesContainer())
                {
                    if (model != null)
                    {
                        SalesEntry entry = new SalesEntry();
                        SalesApplication.SalesEntry tempModel = new SalesApplication.SalesEntry();
                        SalesApplication.SalesEntry finalModel = new SalesApplication.SalesEntry();
                        var allSales = from s in db.SalesEntries where s.MobileNumber == model.MobileNumber &&s.IsActive==true select s;
                        entry = allSales != null ? allSales.FirstOrDefault() : null;
                        if (entry != null)
                        {
                            tempModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(entry);
                            finalModel = TypeMapper.Map<SalesApplication.SalesEntry, SalesApplication.SalesEntry>(tempModel); 
                                
                                    if (finalModel.SMSCount != null && Convert.ToInt32(finalModel.SMSCount) > 0)
                                    {
                                        finalModel.SMSCount = finalModel.SMSCount;
                                    }
                                    else
                                    {
                                        finalModel.SMSCount = 0;
                                    }   
                                    return Request.CreateResponse(HttpStatusCode.OK, finalModel); 

                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

    }
}
