using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using System.IO;
using Collaboration.Web.UI.Utilities;
using System.Configuration;
namespace Collaboration.Web.UI.Admin
{
    public partial class ImportCustomers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void flUploadCustomer_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
            {
                if (flUploadCustomer.PostedFile != null)
                {
                    string fileName = string.Concat(Path.GetFileNameWithoutExtension(flUploadCustomer.PostedFile.FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                               Path.GetExtension(flUploadCustomer.PostedFile.FileName));
                    int contentLength = flUploadCustomer.PostedFile.ContentLength;

                    List<String> validFileExtensions = new List<String>(Resource.ValidCustomerImportExtensions.Split(','));

                    bool IsValidFile = validFileExtensions.Any(x => x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 && contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                    if (!IsValidFile)
                    {
                        fileName = string.Empty;
                        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
                    }
                    else
                    {
                        string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_CUSTOMERIMPORTURL];

                        bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

                        string attachmentLocation = attachmentURL + @"\" + fileName;
                        flUploadCustomer.SaveAs(Server.MapPath(attachmentLocation));

                        int a = new AdminManager().ImportCustomers(Server.MapPath(attachmentLocation));
                    }

                }
            }
            catch (Exception)
            { }
        }     
    }
}