using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using System.IO;
using Collaboration.Web.UI.Utilities;

namespace Collaboration.Web.UI.Account
{
    public partial class EditProfile : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillProfileInfo();
                EditProfile1.ActionType = Convert.ToString(Common.ActionType.EditSelf);
                EditProfile1.FillInfo();
            }
            EditProfile1.btnHandler += new UserControl.UC_EditProfileN.OnButtonClick(Edit_btnHandler);
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillProfileInfo()
        {
            lblUserName.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserName;
            lblEmailInitial.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).EMail;
            //Image1.ImageUrl = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).ImageLocationURL;    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeletePicture_Click(object sender, EventArgs e)
        {
            DeleteProfilePicture();
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteProfilePicture()
        {
            User userInfo = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User);
            userInfo.ImageLocationURL = null;
            userInfo.ModifyDate = DateTime.Now;


            AccountManager accountManager = new AccountManager();
            if (accountManager.ModifyUserInfo(userInfo))
                Session[Collaboration.Web.UI.Common.SESSION_USER] = userInfo;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        void Edit_btnHandler(string strValue)
        {
            FillProfileInfo();
        }
    }
}