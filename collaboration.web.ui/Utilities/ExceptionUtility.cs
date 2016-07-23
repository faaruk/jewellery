using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Collaboration.Logging;
using Collaboration.Business.Components;
namespace Collaboration.Web.UI.Utilities
{
    // Create our own utility for exceptions
    public sealed class ExceptionUtility
    {
       
        // All methods are static, so this can be private
        private ExceptionUtility()
        { }

        // Log an Exception
        public static void LogException(Exception exception, string additionalInfo)
        {
             ILogger logger = new Logger();
            // Include logic for logging exceptions
            // Get the absolute path to the log file
            //string logFile = "App_Data/ErrorLog.txt";
            //logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for append and write the log

            
            StringBuilder sb = new StringBuilder();
            //sb.Append("********** {0} **********", DateTime.Now);
            sb.Append("*****Unhandled error occured in application*****");
            sb.AppendLine();
            //Exception exception = Server.GetLastError().GetBaseException();
          
            if (exception.InnerException != null)
            {
                sb.AppendLine("Inner Exception Type: ");
                sb.Append(exception.InnerException.GetType().ToString());
                sb.AppendLine("Inner Exception: ");
                sb.Append(exception.InnerException.Message);
                sb.AppendLine("Inner Source: ");
                sb.Append(exception.InnerException.Source);
                if (exception.InnerException.StackTrace != null)
                {
                    sb.AppendLine("Inner Stack Trace: ");
                    sb.Append(exception.InnerException.StackTrace);
                }
            }
            sb.AppendLine("Exception Type: ");
            sb.Append(exception.GetType().ToString());
            sb.AppendLine("Exception: " + exception.Message);
            

            if (exception.StackTrace != null)
            {
                sb.AppendLine("Stack Trace: ");
                sb.Append(exception.StackTrace);               
            }
            if (additionalInfo != string.Empty)
            {
                sb.AppendLine("Additonal Info: ");
                sb.Append(additionalInfo);
            }
            logger.LogError(sb.ToString());
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        private static void WriteError(StringBuilder errorMessage)
        {
            ILogger logger = new Logger();
            logger.LogError(errorMessage.ToString());
        }
    }
}