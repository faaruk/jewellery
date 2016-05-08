 <%@ WebHandler Language="C#" Class="FileUploadHandler" %>
 
using System;
using System.Web;
 
public class FileUploadHandler : IHttpHandler {
    string FileName = string.Empty;
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                FileName = file.FileName;
              string fname = context.Server.MapPath("~/uploads/" + file.FileName);
                file.SaveAs(fname);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(FileName + " Uploaded Successfully!");
        }
 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
 
}