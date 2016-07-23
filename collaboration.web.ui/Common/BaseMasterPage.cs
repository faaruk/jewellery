using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Collaboration.Business.Entities;
using System.Web.Security;

namespace Collaboration.Web.UI
{
    public class BaseMasterPage : System.Web.UI.MasterPage
    {
        public BaseMasterPage()
        {
            base.Init += new EventHandler(BasePage_Init);        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BasePage_Init(object sender, EventArgs e)
        {
            if (!IsUserAuthenticated())
                Logout();
            else if (!IsUserAuthorized())
                Response.Redirect("~/AccessDenied.html");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsUserAuthenticated()
        {           
            if (!Request.IsAuthenticated || Session[Collaboration.Web.UI.Common.SESSION_USER]==null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool IsUserAuthorized()
        {
            bool _isUserAuthorized = true;
            if (Request.IsAuthenticated && Session[Collaboration.Web.UI.Common.SESSION_USER] != null)
            {
                int roleID = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID;
                string rawURL = Request.RawUrl;
                rawURL = rawURL.Substring(rawURL.LastIndexOf('/') + 1);

                if (roleID != Convert.ToInt32(Common.Roles.Admin))
                {
                    if (rawURL.ToLower() == Common.Pages.PROCESSTYPES.ToLower() ||
                        rawURL.ToLower() == Common.Pages.FINGERMEASUREMENTS.ToLower() ||
                        rawURL.ToLower() == Common.Pages.METALS.ToLower() ||
                        rawURL.ToLower() == Common.Pages.MODELTYPES.ToLower() ||
                        rawURL.ToLower() == Common.Pages.USERMANAGEMENT.ToLower() ||
                        rawURL.ToLower() == Common.Pages.PRIORITIES.ToLower() ||
                        rawURL.ToLower() == Common.Pages.RINGTYPES.ToLower() ||
                        rawURL.ToLower() == Common.Pages.CANCELORDER.ToLower()
                        )
                        _isUserAuthorized = false;
                }                
            }
            return _isUserAuthorized;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static bool IsUserAuthenticated(object sessionState)
        {            
            if (sessionState == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Logout()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Session.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            Response.Redirect("../Account/Login.aspx");
        }
    }
}