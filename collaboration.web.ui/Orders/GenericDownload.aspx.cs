using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace Collaboration.Web.UI.Orders
{
    public partial class GenericDownload : System.Web.UI.Page
    {
        public const int DEFAULT_BUFFER_SIZE = 4096;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath = Convert.ToString(Session[Common.SESSION_DOWNLOADFILENAME]);

            try
            {
                if (File.Exists(Server.MapPath(filePath)))
                {
                    FileStream stream = File.Open(Server.MapPath(filePath), FileMode.Open);
                    writeAttachment(stream, filePath);
                }
                Session[Common.SESSION_DOWNLOADFILENAME] = null;
                Session[Common.SESSION_DOWNLOADCONTENTTYPE] = null;
            }
            catch (FileNotFoundException)
            {
              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Invalid FileName');", true);

                //Context.Response.Write("<html><body><h1>File Not Found</h1></body></html>");
                //Context.Response.End();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        private void writeAttachment(Stream stream, string filePath)
        {
            string filename = Path.GetFileName(filePath);
            string extension = Path.GetExtension(filePath);
            Context.Response.ContentType = Convert.ToString(Session[Common.SESSION_DOWNLOADCONTENTTYPE]);
            Context.Response.AddHeader("Content-disposition", "attachment; filename=" + filename);
            byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

            try
            {
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Context.Response.OutputStream.Write(buffer, 0, bytesRead);
                }
            }
            finally
            {
                stream.Close();
            }
            Context.Response.End();
        }
    }
}
