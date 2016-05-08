using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace Collaboration.Web.UI.Utilities
{
    public static class MailUtility
    {       
        //Satrt Send Email Function
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="ccList"></param>
        /// <param name="mailType"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool SendPasswordResetMail(string toList, string ccList,string mailType,string userName,string password,out string errorMessage)
        {
            string mailBody = string.Format(GetBodyByMailType(mailType), userName, userName, password, ConfigurationManager.AppSettings["RootPath"] + "/img/logo_river.png", ConfigurationManager.AppSettings["RootPath"].ToString());
            string subject = GetSubjectByMailType(mailType);
            return SendMail(toList, ccList, mailBody, subject, out errorMessage);
        }
        //Satrt Send Email Function
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="ccList"></param>
        /// <param name="mailType"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool SendUserRegistrationMail(string toList, string ccList, string mailType, string userName, string password,out string errorMessage)
        {
            string mailBody = string.Format(GetBodyByMailType(mailType), userName, userName, password, ConfigurationManager.AppSettings["RootPath"] + "/img/logo_river.png", ConfigurationManager.AppSettings["RootPath"]);
            string subject = GetSubjectByMailType(mailType);
            return SendMail(toList, ccList, mailBody, subject, out errorMessage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="ccList"></param>
        /// <param name="mailType"></param>
        /// <param name="customerName"></param>
        /// <param name="orderID"></param>
        /// <param name="CADID"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool SendCADConfirmationMail(string toList, string ccList, string mailType, string customerName, int orderID, int CADID, out string errorMessage)
        {
            string mailBody = string.Format(GetBodyByMailType(mailType), customerName, orderID.ToString().EncryptMAC(Resource.EncryptPassphrase), CADID.ToString().EncryptMAC(Resource.EncryptPassphrase), ConfigurationManager.AppSettings["RootPath"] + "/img/logo_river.png", ConfigurationManager.AppSettings["RootPath"].ToString());
            string subject = GetSubjectByMailType(mailType);
            return SendMail(toList, ccList, mailBody, subject, out errorMessage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="ccList"></param>
        /// <param name="mailType"></param>
        /// <param name="userName"></param>
        /// <param name="serialNumber"></param>
        /// <param name="response"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool SendCADResposeMail(string toList, string ccList, string mailType, string userName, string serialNumber, string response, out string errorMessage)
        {
            string mailBody = string.Format(GetBodyByMailType(mailType), userName, serialNumber, response, ConfigurationManager.AppSettings["RootPath"] + "/img/logo_river.png", ConfigurationManager.AppSettings["RootPath"].ToString());
            string subject = GetSubjectByMailType(mailType);
            return SendMail(toList, ccList, mailBody, subject, out errorMessage);
        }

        public static bool SendCADThreeRequestsIsueMail(string toList, string ccList, string mailType, string SerialNumber, out string errorMessage)
        {
            string mailBody = string.Format(GetBodyByMailType(mailType), ConfigurationManager.AppSettings["RootPath"] + "/img/logo_river.png", SerialNumber);
            string subject = GetSubjectByMailType(mailType);
            return SendMail(toList, ccList, mailBody, subject, out errorMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="ccList"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static bool SendMail(string toList, string ccList, string body, string subject, out string errorMessage)
        {
            errorMessage = Resource.Err_General;
            try
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = ConfigurationManager.AppSettings[Common.APPSETTINGS_SMTPSERVER],
                    //Port = Convert.ToInt32(ConfigurationManager.AppSettings[Common.APPSETTINGS_SMTPPORT]),
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings[Common.APPSETTINGS_ENABLESSL]),
                    Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings[Common.APPSETTINGS_SMTPUSER], ConfigurationManager.AppSettings[Common.APPSETTINGS_SMTPPASSWORD])
                };

                MailMessage message = new MailMessage();

                MailAddress fromAddress = new MailAddress((ConfigurationManager.AppSettings[Common.APPSETTINGS_FROMEMAILID]));
                message.From = fromAddress;
                message.To.Add(toList);
                if (!string.IsNullOrEmpty(ccList))
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                
                smtp.Send(message);
                return true;
            }
            catch (SmtpException exception)
            {
                SmtpStatusCode code = ((SmtpException)(exception)).StatusCode;
                ExceptionUtility.LogException(exception, Common.INFO_SMTPCODE + code);
                errorMessage = exception.Message.ToString();
                return false;
            }
            catch (Exception exception)
            {               
                ExceptionUtility.LogException(exception,string.Empty);
                return false;                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailType"></param>
        /// <returns></returns>
        private static string GetSubjectByMailType(string mailType)
        {
            if (mailType == Common.MailType.ResetPassword.ToString())
                return Resource.SubjectResetPassword;
            else if (mailType == Common.MailType.UserRegistration.ToString())
                return Resource.SubjectUserRegistration;
            else if (mailType == Common.MailType.CADConfirmation.ToString())
                return Resource.SubjectCADConfirmation;
            else if (mailType == Common.MailType.CADResponse.ToString())
                return Resource.SubjectCADResponse;
            else if (mailType == Common.MailType.CADThreeRequestsIsue.ToString())
                return Resource.CADThreeRequestsIsue;
            else
                return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailType"></param>
        /// <returns></returns>
        private static string GetBodyByMailType(string mailType)
        {
            if (mailType == Common.MailType.ResetPassword.ToString())
                return System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/MailTemplates/PasswordReset.txt"));
            else if (mailType == Common.MailType.UserRegistration.ToString())
                return System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/MailTemplates/UserRegistration.txt"));
            else if (mailType == Common.MailType.CADConfirmation.ToString())
                return System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/MailTemplates/CADConfirmation.txt"));
            else if (mailType == Common.MailType.CADResponse.ToString())
                return System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/MailTemplates/CADResponse.txt"));
            else if (mailType == Common.MailType.CADThreeRequestsIsue.ToString())
                return System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/MailTemplates/CADThreeRequestsIsue.txt"));
            else
                return string.Empty;        
        }      
        //End Send Email Function
    }

    public static class Extensions
    {
        public static string DecryptMAC(this string Message, string Passphrase)
        {
            byte[] Results;
            var UTF8 = new UTF8Encoding();

            var HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            var TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            return UTF8.GetString(Results);
        }

        public static string EncryptMAC(this string Message, string Passphrase)
        {
            byte[] Results;
            var UTF8 = new UTF8Encoding();

            var HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            var TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            return Convert.ToBase64String(Results);
        }
        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }
    }
}