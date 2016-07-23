using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using Collaboration.Web.UI.Utilities;
namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_ChangePassword : System.Web.UI.UserControl
    {   
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session[Collaboration.Web.UI.Common.SESSION_USER] as User) != null)
            {
                User userInfo = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User);
                if (userInfo.DefaultPasswordChanged == false)
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_ForcePasswordChanged);
                    btnCancel.Visible = false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            UpdatePassword();
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdatePassword()
        {
            bool _isRedirect = false;
            AccountManager accountManager = new AccountManager();
            User userInfo = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User);
            if (userInfo.DefaultPasswordChanged == false)
                            _isRedirect =true;

            if (accountManager.ChangePassword(userInfo.UserName, txtNewPassword.Text))
            {
                (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).DefaultPasswordChanged = true;
                if (_isRedirect)
                    Response.Redirect(ResolveUrl("~/default.aspx"), true);
                else
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_PasswordChanged);
                    btnUpdatePassword.CssClass = Resource.UI_BtnSuccess;
                }
            }
            else
            {
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                btnUpdatePassword.CssClass = Resource.UI_BtnDanger;
            }
            //accountManager.updat((Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID, txtNewPassword.Text);

        }       
    }
}