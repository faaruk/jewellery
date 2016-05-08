<%@ WebHandler Language="C#" Class="hn_SimpeFileUploaderNewOrder" %>
using System.IO;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using Collaboration.Web.UI.Utilities;
using System.Data;
using System.Configuration;
using AjaxControlToolkit;


public class hn_SimpeFileUploaderNewOrder : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{
    private static int specimenFilesUploaded = 0;
    private static string message = string.Empty;
    private static bool _isValidFile = true;
    public bool IsValidFile { set { _isValidFile = value; } get { return _isValidFile; } }
    private static List<OrdersCAD> orderCADs = new List<OrdersCAD>();
    private static OrderDetails_Result _orderDetails;
    static String AttachmentLocation = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        try
        {

        string str_image = "";
        foreach (string s in context.Request.Files)
        {
            HttpPostedFile hpf = context.Request.Files[s];
            if (hpf.ContentLength > 0)
            {
                //hpf.SaveAs(Server.MapPath("~/UploadedExcel/") + System.IO.Path.GetFileName(hpf.FileName));
                string fileName = string.Concat(Path.GetFileNameWithoutExtension(hpf.FileName),
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Path.GetExtension(hpf.FileName)
                    );
                int contentLength = hpf.ContentLength;

                //List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

                //IsValidFile =
                //    validFileExtensions.Any(
                //        x =>
                //            x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 &&
                //            contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                if (!IsValidFile)
                {
                 //   MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                       // Resource.Err_InvalidImage);
                    }
                else
                {
                    string tempURL = ConfigurationManager.AppSettings[Collaboration.Web.UI.Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\Temp";

                    bool exists = Directory.Exists(HttpContext.Current.Server.MapPath(tempURL));

                    if (!exists)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(tempURL));

                    hpf.SaveAs(HttpContext.Current.Server.MapPath(tempURL + @"\" + fileName));
                    Specimen specimen = new Specimen();
                    specimen.ImageLocationURL = fileName;
                    if (HttpContext.Current.Session[Collaboration.Web.UI.Common.SESSION_SPECIMENIMAGES] == null)
                        HttpContext.Current.Session[Collaboration.Web.UI.Common.SESSION_SPECIMENIMAGES] = new List<Specimen>();

                    (HttpContext.Current.Session[Collaboration.Web.UI.Common.SESSION_SPECIMENIMAGES] as List<Specimen>).Add(specimen);

                    specimenFilesUploaded += 1;
                    //SetUploadControl();

                }
            }
        }
        context.Response.Write(str_image);
        }
        catch (Exception ex)
        {
            context.Response.Write(ex);
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



}