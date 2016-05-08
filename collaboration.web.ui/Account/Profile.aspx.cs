using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
namespace Collaboration.Web.UI.Account
{
    public partial class Profile : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Collaboration.Web.UI.Common.SESSION_USER] == null)
                Response.Redirect(Server.MapPath("~/Account/Login.aspx"));
            else
                if (!IsPostBack)
                    FillProfileInfo();
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillProfileInfo()
        {
            lblUserName.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserName;
            lblFirstName.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).FirstName;
            lblLastName.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).LastName;
            lblMobile.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).Mobile;
            lblEmail.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).EMail;
            lblEmailInitial.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).EMail;      
           
        }
    }
}
