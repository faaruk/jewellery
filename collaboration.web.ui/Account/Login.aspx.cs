using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Web.UI.Utilities;
using log4net;

namespace Collaboration.Web.UI.Account
{
    public partial class Login : System.Web.UI.Page
    {
        int _UserID = 0;
        Collaboration.Business.Components.AccountManager _accountManager = new Business.Components.AccountManager();      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Page.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Account/Login.aspx");
                }
                if (Request.Cookies[Common.COOKIE_USER] != null && Request.Cookies[Common.COOKIE_PASSWORD] != null)
                {
                    txtUserName.Text = Request.Cookies[Common.COOKIE_USER].Value;
                    txtPassword.Attributes["value"] = Request.Cookies[Common.COOKIE_PASSWORD].Value;
                    chkRememberMe.Checked = true;
                }   
            }
                
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            ValidateUser();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void btnResetPassword_Click(object sender, EventArgs args)
        {
            ResetPassword();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void btnCancel_Click(object sender, EventArgs args)
        {
            ResetValues();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ValidateUser()
        {
            if (!_accountManager.ValidateUser(txtUserName.Text, txtPassword.Text, out _UserID))
                MessageUtility.ShowMessage(dvLoginError, ltLogin, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidCredentials);
            else
            {
                FormsAuthentication.SetAuthCookie(txtUserName.Text, true);
                string returnUrl = Request.QueryString[Common.REQUEST_RETURNURL];
                Business.Entities.User userInfo = _accountManager.GetUserInfo(_UserID);
                Session[Collaboration.Web.UI.Common.SESSION_USER] = userInfo;

                if (chkRememberMe.Checked)
                {
                    Response.Cookies[Common.COOKIE_USER].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies[Common.COOKIE_PASSWORD].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies[Common.COOKIE_USER].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies[Common.COOKIE_PASSWORD].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies[Common.COOKIE_USER].Value = txtUserName.Text.Trim();
                Response.Cookies[Common.COOKIE_PASSWORD].Value = txtPassword.Text.Trim();

                if (userInfo.DefaultPasswordChanged == false)
                {
                    Session[Collaboration.Web.UI.Common.SESSION_USER] = _accountManager.GetUserInfo(_UserID);
                    Response.Redirect("~/Account/ChangePassword.aspx", true);
                }
                else
                {
                    Session[Collaboration.Web.UI.Common.SESSION_USER] = _accountManager.GetUserInfo(_UserID);
                    //Response.Redirect(FormsAuthentication.GetRedirectUrl(userInfo.UserName, chkRememberMe.Checked));
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        returnUrl = "~/default.aspx";
                        Response.Redirect(returnUrl, true);
                    }
                    else
                    {
                        Session[Collaboration.Web.UI.Common.SESSION_USER] = _accountManager.GetUserInfo(_UserID);
                        Response.Redirect(returnUrl, true);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ResetPassword()
        {
            string errorMessage = Resource.Err_General;

            string newPassword = System.Web.Security.Membership.GeneratePassword(8, 0);
            var accountManager = new Business.Components.AccountManager();
            string EmailAddress = string.Empty;

            if (!accountManager.ResetPassword(txtResetUserName.Text.ToString(), newPassword, out EmailAddress))
                MessageUtility.ShowMessage(dvResetPasswordInfo, ltInfoResetPassword, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidUserName);
            else
                if (Utilities.MailUtility.SendPasswordResetMail(EmailAddress, string.Empty, Convert.ToString(Common.MailType.ResetPassword), txtResetUserName.Text.ToString(), newPassword, out errorMessage))
                {
                    MessageUtility.ShowMessage(dvResetPasswordInfo, ltInfoResetPassword, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_PasswordReeset);
                    UsernameTextBoxPlaceHolder.Visible = false;
                    btnResetPassword.Visible = false;
                    btnCancel.Text = "Close";
                }
                else
                    MessageUtility.ShowMessage(dvResetPasswordInfo, ltInfoResetPassword, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
            mpForgotPassword.Show();
            UpdatePanel1.Update(); 
        }
        /// <summary>
        /// 
        /// </summary>
        public void ResetValues()
        {
            txtResetUserName.Text = string.Empty;
            MessageUtility.ClearMessages(dvResetPasswordInfo, ltInfoResetPassword);
            UpdatePanel1.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            pnl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
            mpForgotPassword.Show();
            txtResetUserName.Text = string.Empty;
            MessageUtility.ClearMessages(dvResetPasswordInfo, ltInfoResetPassword);
           // UpdatePanel1.Update();
        }      
    }
}
