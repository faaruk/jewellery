using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using System.IO;
namespace Collaboration.Web.UI.Account
{
    public partial class ProfileImage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindImage();
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindImage()
        {
            byte[] profileImage;
            if (Request.QueryString[Collaboration.Web.UI.Common.EntityAttributes.USERID] != null)
            {
                int userID = Convert.ToInt32( Request.QueryString[Collaboration.Web.UI.Common.EntityAttributes.USERID] );
                User userInfo = (Session[Common.SESSION_USERSLIST] as List<User>).First(x => x.UserID == userID) ;

               // if (userInfo.ImageLocationURL != null && userInfo.ImageLocationURL != string.Empty)  
                if (!string.IsNullOrEmpty(userInfo.ImageLocationURL))  
                {
                    profileImage = ImageToBinary(Server.MapPath(userInfo.ImageLocationURL));
                    Response.BinaryWrite(profileImage);
                }                  
                else
                {
                    profileImage = ImageToBinary(Server.MapPath("~/img/ProfileImage.jpg"));
                    Response.BinaryWrite(profileImage);
                }
            }
            else if ((Session[Common.SESSION_USER] as User).ImageLocationURL != null && (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).ImageLocationURL != string.Empty)            
                Response.BinaryWrite(ImageToBinary(Server.MapPath((Session[Collaboration.Web.UI.Common.SESSION_USER] as User).ImageLocationURL)));           
            else
            {
                profileImage = ImageToBinary(Server.MapPath("~/img/ProfileImage.jpg"));
                Response.BinaryWrite(profileImage);
            }
          
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "image/JPEG";
            Response.AddHeader("content-disposition", "attachment;filename="
            + (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserName);
            
            Response.Flush();
            Response.End();

        }
        /// <summary>
        /// s
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private byte[] ImageToBinary(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                imagePath = Server.MapPath("~/img/ProfileImage.jpg");
            
            var fS = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            return b;
        }
    }
}
