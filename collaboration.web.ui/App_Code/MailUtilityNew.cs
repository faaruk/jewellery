using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Configuration;

using System.Xml;
using System.IO;

namespace Collaboration.Web.UI.Utilities
{
    public static class MailUtilityNew
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
        public static bool SendPasswordResetMail(string toList, string ccList, string mailType, string userName, string password, out string errorMessage)
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
        public static bool SendUserRegistrationMail(string toList, string ccList, string mailType, string userName, string password, out string errorMessage)
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
            //string mailBody = string.Format(GetBodyByMailType(mailType), customerName, orderID.ToString().EncryptMAC(Resource.EncryptPassphrase), CADID.ToString().EncryptMAC(Resource.EncryptPassphrase), ConfigurationManager.AppSettings["RootPath"] + "/img/logo_river.png", ConfigurationManager.AppSettings["RootPath"].ToString());
            string mailBody = string.Format(GetBodyByMailType(mailType), customerName, ExtensionsNew.Encrypt(orderID.ToString(), Resource.EncryptPassphrase), ExtensionsNew.Encrypt(CADID.ToString(), Resource.EncryptPassphrase), ConfigurationManager.AppSettings["RootPath"] + "/img/logo_river.png", ConfigurationManager.AppSettings["RootPath"].ToString());
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
                ExceptionUtility.LogException(exception, string.Empty);
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

    public static class ExtensionsNew
    {
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

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
        public static string Encrypt_Old(string strToEncrypt, string strKey)
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
        public static string Decrypt_Old(string strEncrypted, string strKey)
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
        public static string Encrypt1(string toEncrypt, string strKey)
        {
            bool useHashing;
            useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "RiverMount123876";// (string)settingsReader.GetValue("RiverMount123876", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt1(string cipherString, string strKey)
        {
            bool useHashing;
            useHashing = true;
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "RiverMount123876";// (string)settingsReader.GetValue("RiverMount123876", typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string Encrypt2(this string text, string strKey)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt2(this string text, string strKey)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }

        public static string Encrypt3(string TextToBeEncrypted, string strKey)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = "CSC";
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric encryptor object. 
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            //Defines a stream that links data streams to cryptographic transformations
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            //Writes the final state and clears the buffer
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }

        public static string Decrypt4(string TextToBeDecrypted, string strKey)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            string Password = "CSC";
            string DecryptedData;

            try
            {
                byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                //Making of the key for decryption
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                //Creates a symmetric Rijndael decryptor object.
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();

                //Converting to string
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = TextToBeDecrypted;
            }
            return DecryptedData;
        }

        public static string Encrypt(string clearText, string strKey)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText, string strKey)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}