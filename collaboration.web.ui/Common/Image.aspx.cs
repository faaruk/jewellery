using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using System.IO;
using Collaboration.Business.Components;
namespace Collaboration.Web.UI
{
    public partial class Image : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindImage();
        }
        /// <summary>
        /// 
        /// </summary>
        public void BindImage()
        {
            Byte[] image = null;
            if (Request.QueryString[Common.EntityAttributes.CADID] != null)
            {
                int CADID = Convert.ToInt32(Request.QueryString[Common.EntityAttributes.CADID]);
                OrdersCAD entity = new OrderManager().GetOrderCADs(0, CADID).SingleOrDefault();
               // image = entity.CAD;
                Response.BinaryWrite(image);
            }
            else if (Request.QueryString[Common.EntityAttributes.SPECIMENID] != null)
            {
                int SpecimenID = Convert.ToInt32(Request.QueryString[Common.EntityAttributes.SPECIMENID]);
                Specimen entity = new OrderManager().GetSpecimens(SpecimenID, 0, 0).SingleOrDefault();
               // image = entity.SpecimenImage;
                Response.BinaryWrite(image);
            }
            else if (Request.QueryString[Common.ImageRequestPages.PAGE] == Common.ImageRequestPages.DYNAMICORDER && Session[Common.SESSION_CADIMAGE] != null)
            {
                image = Session[Common.SESSION_CADIMAGE] as Byte[];
                Response.BinaryWrite(image);               
            }
            else if (Request.QueryString[Common.ImageRequestPages.PAGE] == Common.ImageRequestPages.VIEWORDER && Session[Common.SESSION_SPECIMENIMAGE] != null)
            {
                image = Session[Common.SESSION_SPECIMENIMAGE] as Byte[];
                Response.BinaryWrite(image);               
            }
            else if (Request.QueryString[Common.ImageRequestPages.PAGE] == Common.ImageRequestPages.EDITORDER && Session[Common.SESSION_SPECIMENIMAGE] != null)
            {
                image = Session[Common.SESSION_SPECIMENIMAGE] as Byte[];
                Response.BinaryWrite(image);              
            }
            else if (Request.QueryString[Common.ImageRequestPages.PAGE] == Common.ImageRequestPages.COMPARECAD && Session[Common.SESSION_COMAPARECADIMAGE] != null)
            {
                image = Session[Common.SESSION_COMAPARECADIMAGE] as Byte[];
                Response.BinaryWrite(image);              
            }
            if (image != null)
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "image/JPEG";
                Response.AddHeader("content-disposition", "attachment;filename=test");
                Response.Flush();
                Response.End();
            }
        }        
    }
}
