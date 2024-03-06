using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SalesApplication.Helpers
{
    public class LogWriter
    {
        //private static string logsPath = "C:\\Working Projects\\Safepot\\Safepot.Web.Api\\Logs\\";
        private static string logsPath = "C:\\WebSite\\sales.yourbooking.in\\23-02-2024\\Logs\\";
        public static void LogWrite(string logMessage)
        {
            try
            {
                using (StreamWriter w = File.AppendText(logsPath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + " -- " + logMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}