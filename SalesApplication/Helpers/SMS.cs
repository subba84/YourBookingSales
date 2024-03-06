using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SalesApplication 
{
    public class SMS
    {
        public string sendMessage(string phoneNo, string message)
        {
            string url = "http://api.bulksmsgateway.in/sendmessage.php";
            string result = "";
            message = HttpUtility.UrlPathEncode(message);
            String strPost = "?user=" + HttpUtility.UrlPathEncode("subbarao.vallarapu@gmail.com") + "&password=" + HttpUtility.UrlPathEncode("8099743666") + "&sender=" + HttpUtility.UrlPathEncode("TIKDBT") + "&mobile=" + HttpUtility.UrlPathEncode(phoneNo) + "&type=" + HttpUtility.UrlPathEncode("3") + "&message=" + message + "&template_id=" + HttpUtility.UrlPathEncode("1507165167067359930");
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url + strPost);
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader sr.Close();
            }
            return result;
        }
    }
}