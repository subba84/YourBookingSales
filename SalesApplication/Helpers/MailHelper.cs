using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SalesApplication
{
    public class MailHelper
    {

        public void SendEmail (string fromEmail, string toEmail, string emailBody, string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(toEmail))
                {
                    // System.Net.Mail.SmtpClient mailServer = new System.Net.Mail.SmtpClient(tblemail.Host, int.Parse(tblemail.Port.ToString()));
                    // System.Net.Mail.SmtpClient mailServer = new System.Net.Mail.SmtpClient("10.20.2.25", int.Parse("5221"));

                    //using (SalesContainer db = new SalesContainer())
                    //{
                    //    Error error = new Error();
                    //    error.Error1 = "MailMethod called";
                    //    error.ErrorMessage = "MailMethod called";
                    //    error.CreatedOn = DateTime.Now;
                    //    db.Errors.AddOrUpdate(error);
                    //    db.SaveChanges();
                    //}
                    var smtp = new SmtpClient
                    {
                       //Host = "http://relay-hosting.secureserver.net",
                      Host= ConfigurationManager.AppSettings["EmailHost"],
                        Port = 25,
                        EnableSsl = false,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("support@databricks.online", "MyTelangana#63")
                    };
                    //System.Net.Mail.SmtpClient mailServer = new System.Net.Mail.SmtpClient("http://relay.hosting.secureserver.net", int.Parse("25"));
                    


                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(fromEmail, toEmail);
                    if(path!=null)
                    {
                        msg.Attachments.Add(new System.Net.Mail.Attachment(path));
                    }
                    msg.Subject = "Invoice";
                    msg.Body = emailBody;
                    msg.IsBodyHtml = true;
                    if (ConfigurationManager.AppSettings["IsEmailEnabled"] == "Y")
                    {
                        smtp.Send(msg);
                    }

                }
            }
            catch (Exception ex)
            {
                using (SalesContainer db = new SalesContainer())
                {
                    Error error = new Error();
                    error.Error1 = ex.StackTrace;
                    error.ErrorMessage = ex.Message;
                    error.CreatedOn = DateTime.Now;
                    db.Errors.AddOrUpdate(error);
                    db.SaveChanges();
                }
                    throw ex;
            }
            using (SalesContainer db = new SalesContainer())
            {
                Error error = new Error();
                error.Error1 = "MailMethod Ended";
                error.ErrorMessage = "MailMethod called";
                error.CreatedOn = DateTime.Now;
                db.Errors.AddOrUpdate(error);
                db.SaveChanges();
            }
        }



        public void SendEmail1(string fromEmail, string toEmail, string emailBody, string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(toEmail))
                { 
                    var smtp = new SmtpClient
                    {
                        Host = "http://relay-hosting.secureserver.net",
                        Port = 25,
                        EnableSsl = false,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("support@databricks.online", "MyTelangana#63")
                    }; 

                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(fromEmail, toEmail);
                    if (path != null)
                    {
                        msg.Attachments.Add(new System.Net.Mail.Attachment(path));
                    }
                    msg.Subject = "Invoice";
                    msg.Body = emailBody;
                    msg.IsBodyHtml = true;
                    if (ConfigurationManager.AppSettings["IsEmailEnabled"] == "Y")
                    {
                        smtp.Send(msg);
                    } 
                }
            }
            catch (Exception ex)
            {
                using (SalesContainer db = new SalesContainer())
                {
                    Error error = new Error();
                    error.Error1 = ex.StackTrace;
                    error.ErrorMessage = ex.Message;
                    error.CreatedOn = DateTime.Now;
                    db.Errors.AddOrUpdate(error);
                    db.SaveChanges();
                }
                throw ex;
            } 
        }

        // With Attachment
        public void SendEmail3(string fromEmail, string toEmail, string emailBody, string Subject, string path)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(fromEmail);
                message.From = fromAddress;
                message.To.Add(toEmail);
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = emailBody;
                if (path != null)
                {
                    message.Attachments.Add(new System.Net.Mail.Attachment(path));
                }
                smtpClient.Host = "relay-hosting.secureserver.net";   //-- Donot change.
                smtpClient.Port = 25; //--- Donot change
                smtpClient.EnableSsl = false;//--- Donot change
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("support@databricks.online", "MyTelangana#63");

                smtpClient.Send(message);

                // lblConfirmationMessage.Text = "Your email successfully sent.";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                using (SalesContainer db = new SalesContainer())
                {
                    Error error = new Error();
                    error.Error1 = ex.StackTrace;
                    error.ErrorMessage = ex.Message;
                    error.CreatedOn = DateTime.Now;
                    db.Errors.AddOrUpdate(error);
                    db.SaveChanges();
                }
            }
        }


        // Without Attachment
        public void SendEmail2(string fromEmail, string toEmail, string emailBody,string subject)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(fromEmail);
                message.From = fromAddress;
                message.To.Add(toEmail);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = emailBody;
                smtpClient.Host = "relay-hosting.secureserver.net";   //-- Donot change.
                smtpClient.Port = 25; //--- Donot change
                smtpClient.EnableSsl = false;//--- Donot change
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("support@databricks.online", "MyTelangana#63");

                smtpClient.Send(message);

               // lblConfirmationMessage.Text = "Your email successfully sent.";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                using (SalesContainer db = new SalesContainer())
                {
                    Error error = new Error();
                    error.Error1 = ex.StackTrace;
                    error.ErrorMessage = ex.Message;
                    error.CreatedOn = DateTime.Now;
                    db.Errors.AddOrUpdate(error);
                    db.SaveChanges();
                }
            }
        }

    }
}































//public void SendingAlertingEmail(string emailType, string toEmails, string emailBody)
//{
//    try
//    {
//        if (!string.IsNullOrEmpty(toEmails))
//        {
//            tblEmailConfiguration tblemail = GetOutboundEmailConfiguration();
//            System.Net.Mail.SmtpClient mailServer = new System.Net.Mail.SmtpClient(tblemail.Host, int.Parse(tblemail.Port.ToString()));
//            string fromEmail = tblemail.EmailId;
//            string[] toEmailAddresses = toEmails.Split(';');
//            if (toEmailAddresses != null && toEmailAddresses.Length > 0)
//            {
//                for (int i = 0; i < toEmailAddresses.Length; i++)
//                {
//                    string toEmail = toEmailAddresses[i];
//                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(fromEmail, toEmail);
//                    msg.Subject = GetSubjectforAerting(emailType);
//                    msg.Body = emailBody;
//                    msg.IsBodyHtml = true;
//                    if (ConfigurationManager.AppSettings["IsEmailEnabled"] == "Y")
//                    {
//                        mailServer.Send(msg);
//                    }
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}
