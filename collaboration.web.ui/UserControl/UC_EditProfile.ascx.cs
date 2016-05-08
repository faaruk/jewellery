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
using System.Configuration;
using AjaxControlToolkit;
namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_EditProfile : System.Web.UI.UserControl
    {
        User _userInfo = null;
        static int _userID = 0;
        public static string _actionType = string.Empty;
        static string fileName = null;
        static bool IsValidFile = true;
        public static string filePath = null;
        public ModalPopupExtender ParentPopup = null;
        // Delegate declaration

        public delegate void OnButtonClick(string strValue);

        // Event declaration

        public event OnButtonClick btnHandler;

        public string ActionType
        {
            set
            {
                _actionType = value;
                if (_actionType == Convert.ToString(Common.ActionType.Edit) || _actionType == Convert.ToString(Common.ActionType.EditSelf))
                {
                    ddlUserTypes.Visible = false;
                    txtUserType.Visible = true;
                    txtUserType.ReadOnly = true;
                    txtUserName.ReadOnly = true;
                    if (_actionType == Convert.ToString(Common.ActionType.EditSelf))
                        btnCancel.Visible = false;
                }
                else if (_actionType == Convert.ToString(Common.ActionType.Add))
                {
                    txtUserType.Visible = false;
                    txtUserType.ReadOnly = true;
                    txtUserName.ReadOnly = false;
                    ddlUserTypes.Visible = true;
                }
            }
            get
            {
                return _actionType;
            }
        }
        public int UserID { set { _userID = value; } get { return _userID; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Collaboration.Web.UI.Common.SESSION_USER] == null)
                Response.Redirect(Server.MapPath("~/Account/Login.aspx"));
            // Page.Form.Attributes.Add("enctype", "multipart/form-data");

            UniqUsernameValidator.Text = Resource.Err_UniqUsername;
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillInfo()
        {
            FillRoles();
            ResetValues();
            if (ActionType == Convert.ToString(Common.ActionType.Edit))
            {
                _userInfo = (Session[Collaboration.Web.UI.Common.SESSION_USERSLIST] as List<User>).Where(x => x.UserID == UserID).First();
                ddlUserTypes.SelectedValue = Convert.ToString(_userInfo.RoleID);
                txtUserType.Text = ddlUserTypes.SelectedItem.Text;
            }
            else if (ActionType == Convert.ToString(Common.ActionType.EditSelf))
            {
                _userInfo = Session[Collaboration.Web.UI.Common.SESSION_USER] as User;
                ddlUserTypes.SelectedValue = Convert.ToString(_userInfo.RoleID);
                ddlUserTypes.Visible = false;
                txtUserType.Text = ddlUserTypes.SelectedItem.Text;
            }
            else if (ActionType == Convert.ToString(Common.ActionType.Add))
                _userInfo = new User();

            txtUserName.Text = _userInfo.UserName;
            txtFirstName.Text = _userInfo.FirstName;
            txtLastName.Text = _userInfo.LastName;
            txtMobile.Text = _userInfo.Mobile;
            txtEmail.Text = _userInfo.EMail;
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillRoles()
        {
            ddlUserTypes.DataSource = new AccountManager().GetRoles();
            ddlUserTypes.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ResetValues()
        {
            txtUserName.Text = txtFirstName.Text = txtLastName.Text = txtMobile.Text = txtEmail.Text = txtUserType.Text = string.Empty;
            MessageUtility.ClearMessages(dvMessage, ltMessage);
            btnUpdate.CssClass = Resource.UI_BtnPrimary;
            btnUpdate.Enabled = true;
            fileName = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeletePicture_Click(object sender, EventArgs e)
        {
            DeletePicture();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            if (IsUsernameUniq())
            {
                string newPassword = string.Empty;
                var UserList = (Session[Collaboration.Web.UI.Common.SESSION_USERSLIST] as List<User>);

                if (ActionType == Convert.ToString(Common.ActionType.Add))
                {
                    _userInfo = new User();
                    _userInfo.CreateDate = DateTime.Now;
                    _userInfo.DefaultPasswordChanged = false;
                    _userInfo.IsActive = true;
                    _userInfo.RoleID = Convert.ToInt16(ddlUserTypes.SelectedValue);
                    _userInfo.StringPassword = newPassword = System.Web.Security.Membership.GeneratePassword(8, 2);
                    _userInfo.UserName = txtUserName.Text;
                }
                else if (ActionType == Convert.ToString(Common.ActionType.EditSelf))
                    _userInfo = Session[Collaboration.Web.UI.Common.SESSION_USER] as User;
                else
                    _userInfo = UserList.Where(x => x.UserID == UserID).First();



                _userInfo.ImageLocationURL = filePath;
                _userInfo.FirstName = txtFirstName.Text;
                _userInfo.LastName = txtLastName.Text;
                _userInfo.Mobile = txtMobile.Text;
                _userInfo.EMail = txtEmail.Text;

                filePath = string.Empty;


                string ImageName = string.Empty;

                AccountManager accountManager = new AccountManager();
                String message = string.Empty;
                if (IsValidFile)
                {
                    try
                    {
                        if (ActionType == Convert.ToString(Common.ActionType.Add))
                        {
                            if (accountManager.CreateUser(_userInfo))
                            {
                                string errorMessage = Resource.Err_General;
                                Session[Collaboration.Web.UI.Common.SESSION_USERSLIST] = accountManager.GetUsers();
                                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Added);
                                message = Resource.Info_Added;
                                if (!Utilities.MailUtility.SendUserRegistrationMail(_userInfo.EMail, string.Empty, Convert.ToString(Common.MailType.UserRegistration), _userInfo.UserName, newPassword, out errorMessage))
                                    message = errorMessage;
                            }
                        }
                        else
                        {
                            if (accountManager.ModifyUserInfo(_userInfo))
                            {
                                Session[Collaboration.Web.UI.Common.SESSION_USERSLIST] = _userInfo;
                                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Updated);
                                message = Resource.Info_Updated;
                            }
                        }
                        if (ActionType != Convert.ToString(Common.ActionType.EditSelf))
                            btnUpdate.Enabled = false;
                        btnUpdate.CssClass = Resource.UI_BtnSuccess;
                    }
                    catch (BLLException exception)
                    {
                        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
                        ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
                        message = exception.ErrorMessage;
                        btnUpdate.CssClass = Resource.UI_BtnDanger;
                    }
                    catch (Exception exception)
                    {
                        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                        ExceptionUtility.LogException(exception, "Sender: " + Request.RawUrl);
                        message = Resource.Err_General;
                        btnUpdate.CssClass = Resource.UI_BtnDanger;
                    }
                }
                else
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
                IsValidFile = true;
                if (btnHandler != null)
                    btnHandler(message);
            }
            else
            {
                UniqUsernameValidator.IsValid = false;
                ParentPopup.Show();
            }
        }

        bool IsUsernameUniq()
        {
            _userInfo = (Session[Collaboration.Web.UI.Common.SESSION_USERSLIST] as List<User>).Where(x => x.UserID == UserID).First();
            var UserList = (Session[Collaboration.Web.UI.Common.SESSION_USERSLIST] as List<User>);
            if (ActionType == Convert.ToString(Common.ActionType.Add))
            {
                return UserList.Count(u => u.UserName == txtUserName.Text) == 0;
            }
            else
            {
                return UserList.Count(u => u.UserID != _userInfo.UserID && u.UserName == txtUserName.Text) == 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeletePicture()
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgProfie_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            try
            {
                if (imgProfie.PostedFile != null)
                {
                    fileName = imgProfie.PostedFile.FileName;
                    int contentLength = imgProfie.PostedFile.ContentLength;

                    List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

                    IsValidFile = validFileExtensions.Any(x => x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 && contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                    if (!IsValidFile)
                    {
                        fileName = string.Empty;
                        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
                    }
                    else
                    {
                        string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_PROFILEIMAGESURL] + txtUserName.Text;

                        bool exists = Directory.Exists(Server.MapPath(attachmentURL));
                        if (!exists)
                            Directory.CreateDirectory(Server.MapPath(attachmentURL));

                        // string filename = System.IO.Path.GetFileName(imgProfie.FileName);
                        filePath = attachmentURL + @"\" + fileName;

                        imgProfie.SaveAs(Server.MapPath(filePath));
                    }
                }
            }
            catch (Exception)
            { }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (filePath != string.Empty || filePath != "")
            {
                string filepath = Server.MapPath(filePath);
                if (File.Exists(filepath))
                {
                    FileInfo myfileinf = new FileInfo(filepath);
                    myfileinf.Delete();
                }
            }
        }

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            UniqUsernameValidator.IsValid = IsUsernameUniq();
            ParentPopup.Show();
        }
    }
}
