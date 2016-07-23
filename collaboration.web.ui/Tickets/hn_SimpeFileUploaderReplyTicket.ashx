<%@ WebHandler Language="C#" Class="hn_SimpeFileUploader" %>
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


public class hn_SimpeFileUploader : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
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

        //string dirFullPath = HttpContext.Current.Server.MapPath("~/Images/WallImages/imagepath/");
        //string[] files;
        //int numFiles;
        //files = System.IO.Directory.GetFiles(dirFullPath);
        //numFiles = files.Length;
        //numFiles = numFiles + 1;

        //string str_image = "";

        //foreach (string s in context.Request.Files)
        //{
        //    HttpPostedFile file = context.Request.Files[s];
        //    string fileName = file.FileName;
        //    string fileExtension = file.ContentType;

        //    if (!string.IsNullOrEmpty(fileName))
        //    {
        //        fileExtension = Path.GetExtension(fileName);
        //        str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
        //        string pathToSave = HttpContext.Current.Server.MapPath("~/Images/WallImages/imagepath/") + str_image;
        //        file.SaveAs(pathToSave);
        //    }
        //}
        //context.Response.Write(str_image);

        try
        {
            string str_image = "";
            foreach (string s in context.Request.Files)
            {
 
                if (HttpContext.Current.Session["Session_CADImage"] != null)
                {
                    HttpContext.Current.Session["Session_CADImage"] = null;
                }
                HttpPostedFile file = context.Request.Files[s];
                message = message + "<b>" + file.FileName + "</b> (" + file.ContentType
                    + ") - <i>Upload1ed</i> <i class=\"icon-ok\"></i>";

                string fileName = string.Concat(Path.GetFileNameWithoutExtension(file.FileName),
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Path.GetExtension(file.FileName)
                    );
                int contentLength = file.ContentLength;



                //List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

                //IsValidFile =
                //    validFileExtensions.Any(
                //        x =>
                //            x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 &&
                //            contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                if (!IsValidFile)
                {
                    //MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                    //    Resource.Err_InvalidImage);
                }
                else
                {
                    //string attachmentURL = ConfigurationManager.AppSettings["CADImagesURL"] + @"\" + Convert.ToString(_orderDetails.OrderID);
                    string OrderID = context.Request.QueryString["OrderID"];
                    string attachmentURL = ConfigurationManager.AppSettings["CADImagesURL"] + @"\" + OrderID;

                    bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(attachmentURL));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(attachmentURL));

                    AttachmentLocation = attachmentURL + @"\" + fileName;
                    file.SaveAs(HttpContext.Current.Server.MapPath(AttachmentLocation));

                    OrdersCAD orderCAD = new OrdersCAD();
                    orderCAD.CADLocationURL = AttachmentLocation;
                    orderCAD.IsApproved = false;
                    HttpContext.Current.Session["AttachmentLocation"] = AttachmentLocation;
                    List<OrdersCAD> lst = new List<OrdersCAD>();
                    if (HttpContext.Current.Session["Session_CADImage"] != null)
                        lst = HttpContext.Current.Session["Session_CADImage"] as List<OrdersCAD>;
                    lst.Add(orderCAD);

                    string fileExtension = file.ContentType;
                    fileExtension = Path.GetExtension(fileName);
                    str_image = fileName.ToString() + fileExtension;

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