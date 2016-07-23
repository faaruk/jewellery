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
using System.Data;
using System.Configuration;
using AjaxControlToolkit;

namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_UploadCad : System.Web.UI.UserControl
    {
        private static int _userID = 0;
        private static int _roleID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }
        private static bool _isValidFile = true;
        public bool IsValidFile { set { _isValidFile = value; } get { return _isValidFile; } }
        private static int specimenFilesUploaded = 0;
        private static string message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    uploadFirstAlternative();
            //}
        }
        //public void uploadFirstAlternative()
        //{

        //    HttpFileCollection hfc = Request.Files;
        //    for (int i = 0; i < hfc.Count; i++)
        //    {
        //        HttpPostedFile hpf = hfc[i];
        //        if (hpf.ContentLength > 0)
        //        {
        //            //hpf.SaveAs(Server.MapPath("~/UploadedExcel/") + System.IO.Path.GetFileName(hpf.FileName));
        //            string fileName = string.Concat(Path.GetFileNameWithoutExtension(hpf.FileName),
        //                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
        //                Path.GetExtension(hpf.FileName)
        //                );
        //            int contentLength = hpf.ContentLength;

        //            List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

        //            IsValidFile =
        //                validFileExtensions.Any(
        //                    x =>
        //                        x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 &&
        //                        contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

        //            if (!IsValidFile)
        //            {
        //                //MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
        //                //    Resource.Err_InvalidImage);
        //            }
        //            else
        //            {
        //                string tempURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\Temp";

        //                bool exists = Directory.Exists(Server.MapPath(tempURL));

        //                if (!exists)
        //                    Directory.CreateDirectory(Server.MapPath(tempURL));

        //                hpf.SaveAs(Server.MapPath(tempURL + @"\" + fileName));
        //                Specimen specimen = new Specimen();
        //                specimen.ImageLocationURL = fileName;
        //                if (Session[Common.SESSION_SPECIMENIMAGES] == null)
        //                    Session[Common.SESSION_SPECIMENIMAGES] = new List<Specimen>();

        //                (Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>).Add(specimen);

        //                specimenFilesUploaded += 1;
        //                //SetUploadControl();

        //            }
        //        }
        //    }
        //}
    }
}