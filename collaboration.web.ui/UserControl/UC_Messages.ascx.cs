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
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_Messages : System.Web.UI.UserControl
    {

        private static string message = string.Empty;
       
        static int _userID = 0;
        static int _messageThreadID = 0;
        static string _fileName = string.Empty;
        static int _orderID = 0;
        static String AttachmentLocation = string.Empty;
        static String ContentType = string.Empty;
        static bool IsValidFile = true;
        // Delegate declaration

        public delegate void OnButtonClick(string strValue);

        // Event declaration

        public string fileName = "";

        public event OnButtonClick btnHandler;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int MessageThreadID { set { _messageThreadID = value; } get { return _messageThreadID; } }
        public int OrderID { set { _orderID = value; } get { return _orderID; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblMessageText = (Label)e.Item.FindControl("lblMessageText");
                Label lblLocationURL = (Label)e.Item.FindControl("lblLocationURL");

                var divMask = (System.Web.UI.Control)e.Item.FindControl("divAttachment");
                var divMaskAlt = (System.Web.UI.Control)e.Item.FindControl("divAttachmentAlt");

                //Div Attachment
                if (divMask != null)
                {
                    divMask.Visible = false;
                    if (lblLocationURL.Text != string.Empty)
                    {
                        divMask.Visible = true;

                        var arr = lblLocationURL.Text.Trim().Split('\\');

                        fileName = arr[arr.Length - 1].Replace(" ","");

                        LinkButton lblAttachedFile = (LinkButton)e.Item.FindControl("lblAttachedFile");
                        Label lblSize = (Label)e.Item.FindControl("lblSize");
                        long fileSize = 0;

                        if (File.Exists(Server.MapPath(lblLocationURL.Text)))
                        {
                            FileInfo f = new FileInfo(Server.MapPath(lblLocationURL.Text));
                            fileSize = f.Length / 1000;
                        }
                       
                       
                        lblAttachedFile.Text = fileName;
                        lblSize.Text =   " (Size: " + fileSize + " KB )";
                    }
                }

                //Div Attachment Alt
                if (divMaskAlt != null)
                {
                    divMaskAlt.Visible = false;
                    if (lblLocationURL.Text != string.Empty)
                    {
                        divMaskAlt.Visible = true;

                        var arr = lblLocationURL.Text.Trim().Split('\\');

                        fileName = arr[arr.Length - 1].Replace(" ", "");

                        var lblAttachedFile = (LinkButton)e.Item.FindControl("lblAttachedFile");
                        var lblSize = (Label)e.Item.FindControl("lblSize");

                        long fileSize = 0;

                        if (File.Exists(Server.MapPath(lblLocationURL.Text)))
                        {
                            var f = new FileInfo(Server.MapPath(lblLocationURL.Text));
                            fileSize = f.Length / 1000;
                        }


                        lblAttachedFile.Text = fileName;
                        lblSize.Text = string.Format(" (Size: {0} KB )",fileSize) ;
                    }
                }
                
                
                lblMessageText.Text = HttpUtility.HtmlDecode(lblMessageText.Text);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Download_Click(object sender, EventArgs e)
        {
            LinkButton btndetails = sender as LinkButton;
            ListViewDataItem gvrow = (ListViewDataItem)btndetails.NamingContainer;
            Label lblLocationURL = (gvrow.FindControl("lblLocationURL") as Label);
            Label lblContentType = (gvrow.FindControl("lblContentType") as Label);

            var arr = lblLocationURL.Text.Trim().Split('\\');

            string filename = arr[arr.Length - 1].ToString(); ;
            Response.ContentType = lblContentType.Text;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename.Replace(" ", ""));
            string aaa = Server.MapPath(lblLocationURL.Text);

            if (!File.Exists(Server.MapPath(lblLocationURL.Text)))
            {
                ScriptManager.RegisterStartupScript(updGrid, updGrid.GetType(), "alertmessage", "javascript:alert('Invalid FileName');", true);
                Session[Common.SESSION_DOWNLOADFILENAME] = null;
                Session[Common.SESSION_DOWNLOADCONTENTTYPE] = null;
            }
            else
            {
                // ScriptManager sc = new ScriptManager();               
                Session[Common.SESSION_DOWNLOADFILENAME] = lblLocationURL.Text;
                Session[Common.SESSION_DOWNLOADCONTENTTYPE] = lblContentType.Text;

                Response.TransmitFile(Server.MapPath(lblLocationURL.Text));
                Response.End();
            }

       

            //ImageButton btndetails = sender as ImageButton;
            //ListViewDataItem gvrow = (ListViewDataItem)btndetails.NamingContainer;
            //Label lblContentType = (gvrow.FindControl("lblContentType") as Label);

            //string filePath = (sender as ImageButton).CommandArgument;
            //if (!File.Exists(Server.MapPath(filePath)))
            //{
            //    ScriptManager.RegisterStartupScript(updGrid, updGrid.GetType(), "alertmessage", "javascript:alert('Invalid FileName');", true);
            //    Session[Common.SESSION_DOWNLOADFILENAME] = null;
            //    Session[Common.SESSION_DOWNLOADCONTENTTYPE] = null;
            //}
            //else
            //{
            //    // ScriptManager sc = new ScriptManager();               
            //    Session[Common.SESSION_DOWNLOADFILENAME] = filePath;
            //    Session[Common.SESSION_DOWNLOADCONTENTTYPE] = lblContentType.Text;
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillInfo()
        {
            ResetValues();
            BindGrid();
            BindUsers();
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            IEnumerable<Messages_Result> messageThreads_Result = new MessageManager().GetMessages(MessageThreadID, 0, UserID);
            Session[Collaboration.Web.UI.Common.SESSION_MESSAGES] = messageThreads_Result;

            if (messageThreads_Result.Count() == 0)
            {
                NoMessagePlaceHolder.Visible = true;                
            }
            gvTable.DataSource = messageThreads_Result;
            gvTable.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindUsers()
        {
            IEnumerable<OrderParticipants_Result> orderParticipants_Result = new OrderManager().GetOrderParticipants(OrderID).Where(u => u.UserID != UserID);
            ddlUsers.DataSource = orderParticipants_Result;
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("--Select Assignee--", "0"));
        }
        /// <summary>
        /// 
        /// </summary>
        private void ResetValues()
        {
            txtMessage.Value = string.Empty;
            MessageUtility.ClearMessages(dvMessage, ltMessage);
            AttachmentLocation = string.Empty;
            ContentType = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            Update();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            MessageThread messageThread = new MessageThread();
            messageThread.IsActive = Convert.ToBoolean(Common.IsActive.Yes);
            messageThread.AssignedTo = Convert.ToInt32(ddlUsers.SelectedValue);
            messageThread.Status = Convert.ToInt32(Common.CaseStatus.Open);
            messageThread.OrderID = OrderID;
            messageThread.MessageThreadID = MessageThreadID;
            string newPassword = string.Empty;

            Message message = new Message();
            message.IsActive = Convert.ToBoolean(Common.IsActive.Yes);
            message.Subject = string.Empty;
            message.MessageText = HttpUtility.HtmlEncode(txtMessage.Value.Replace("\n", "<br />"));
            message.SentFrom = UserID;

            message.MessageThreadID = MessageThreadID;
            message.HasAttachment = Convert.ToBoolean(Common.IsActive.No);

            MessagesAttachment messagesAttachment = new MessagesAttachment();
            if (AttachmentLocation != string.Empty)
            {
                messagesAttachment.LocationURL = AttachmentLocation;
                messagesAttachment.ContentType = ContentType;
                message.HasAttachment = Convert.ToBoolean(Common.IsActive.Yes);
                AttachmentLocation = string.Empty;
                ContentType = string.Empty;
            }
            message.MessagesAttachments.Add(messagesAttachment);

            messageThread.Messages.Add(message);

            MessageManager messageManager = new MessageManager();
            string result = string.Empty;
            if (IsValidFile)
            {
                try
                {
                    if (messageManager.InsertMessage(messageThread))
                    {
                        // MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Added);
                        result = Resource.Info_Added;
                        BindGrid();
                        txtMessage.Value = string.Empty;
                        //updGrid.Update();
                    }
                }
                catch (BLLException exception)
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
                    ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
                    result = exception.ErrorMessage;

                }
                catch (Exception exception)
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                    ExceptionUtility.LogException(exception, "Sender: " + Request.RawUrl);
                    result = Resource.Err_General;
                }
            }
            else
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
            IsValidFile = true;
            if (btnHandler != null)
                btnHandler(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Attachment_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        //{
        //    try
        //    {
        //        if (flFile.PostedFile != null)
        //        {
        //            //string fileName = imgProfie.PostedFile.FileName;
        //            int contentLength = flFile.PostedFile.ContentLength;

        //            // List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

        //            IsValidFile = (contentLength < Convert.ToInt32(Resource.ValidImageFileSize)) ? true : false;

        //            if (!IsValidFile)
        //                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
        //            else
        //            {
        //                string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_ATTACHMENTURL] + Convert.ToString(OrderID);

        //                bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

        //                if (!exists)
        //                    System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

        //                AttachmentLocation = attachmentURL + @"\" + string.Concat(Path.GetFileNameWithoutExtension(flFile.PostedFile.FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff"),
        //    Path.GetExtension(flFile.PostedFile.FileName));
        //                ContentType = flFile.ContentType;
        //                flFile.SaveAs(Server.MapPath(AttachmentLocation));
                        
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}


        protected void File_Upload(object sender, AjaxFileUploadEventArgs e)
        {
            try
            {

                            
                message = message + "<b>" + e.FileName + "</b> (" + e.ContentType
                    + ") - <i>Upload1ed</i> <i class=\"icon-ok\"></i>";
                string fileName = string.Concat(Path.GetFileNameWithoutExtension(e.FileName),
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Path.GetExtension(e.FileName)
                    );
                int contentLength = e.FileSize;

                //List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

                //IsValidFile =
                //    validFileExtensions.Any(
                //        x =>
                //            x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 &&
                //            contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                IsValidFile = (contentLength < Convert.ToInt32(Resource.ValidImageFileSize)) ? true : false;

                if (!IsValidFile)
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                        Resource.Err_InvalidImage);
                else
                {
                   
                    string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_ATTACHMENTURL] + Convert.ToString(OrderID);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

                    AttachmentLocation = attachmentURL + @"\" + fileName;
                    ContentType = e.ContentType;
                    specimenFileUpload.SaveAs(Server.MapPath(AttachmentLocation));
                 
                }
            }
            catch (Exception)
            {
            }
        }

      
       
    }
}
