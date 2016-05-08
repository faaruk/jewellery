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
    public partial class UC_TicketsNew : System.Web.UI.UserControl
    {
        private static string message = string.Empty;

        static int _userID = 0;
        static int _ticketThreadID = 0;
        static int _orderID = 0;
        static String AttachmentLocation = string.Empty;
        static String _attachmentLocationU = string.Empty;
        static String ContentType = string.Empty;
        static String _contentType = string.Empty;
        static bool IsValidFile = true;
        // Delegate declaration

        public delegate void OnButtonClick(string strValue);

        // Event declaration

        public string fileName = "";

        public event OnButtonClick btnHandler;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int TicketThreadID { set { _ticketThreadID = value; } get { return _ticketThreadID; } }
        public int OrderID { set { _orderID = value; } get { return _orderID; } }
        public string AttachmentLocationU { set { _attachmentLocationU = value; } get { return _attachmentLocationU; } }
        public string ContentTypeU { set { _contentType = value; } get { return _contentType; } }

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
                Label lblTicketText = (Label)e.Item.FindControl("lblTicketText");
                Label lblLocationURL = (Label)e.Item.FindControl("lblLocationURL");

                System.Web.UI.Control divMask = (System.Web.UI.Control)e.Item.FindControl("divAttachment");
                System.Web.UI.Control divMaskAlt = (System.Web.UI.Control)e.Item.FindControl("divAttachmentAlt");

                //Div Attachment
                if (divMask != null)
                {
                    divMask.Visible = false;
                    if (lblLocationURL.Text != string.Empty)
                    {
                        divMask.Visible = true;

                        var arr = lblLocationURL.Text.Trim().Split('\\');

                        fileName = arr[arr.Length - 1].ToString().Replace(" ", "");

                        LinkButton lblAttachedFile = (LinkButton)e.Item.FindControl("lblAttachedFile");
                        Label lblSize = (Label)e.Item.FindControl("lblSize");
                        FileInfo f = new FileInfo(Server.MapPath(lblLocationURL.Text));
                        long fileSize = f.Length / 1000;


                        lblAttachedFile.Text = fileName;
                        lblSize.Text = " (Size: " + fileSize.ToString() + " KB )";
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

                        fileName = arr[arr.Length - 1].ToString().Replace(" ", "");

                        LinkButton lblAttachedFile = (LinkButton)e.Item.FindControl("lblAttachedFile");
                        Label lblSize = (Label)e.Item.FindControl("lblSize");
                        FileInfo f = new FileInfo(Server.MapPath(lblLocationURL.Text));
                        long fileSize = f.Length / 1000;


                        lblAttachedFile.Text = fileName;
                        lblSize.Text = " (Size: " + fileSize.ToString() + " KB )";
                    }
                }

                lblTicketText.Text = HttpUtility.HtmlDecode(lblTicketText.Text);
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
            IEnumerable<Tickets_Result> ticketThreads_Result = new TicketManager().GetTickets(TicketThreadID);
            Session[Collaboration.Web.UI.Common.SESSION_TICKETS] = ticketThreads_Result;

            gvTable.DataSource = ticketThreads_Result;
            gvTable.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindUsers()
        {
            IEnumerable<TicketParticipants_Result> ticketParticipants_Result = new TicketManager().GetTicketParticipants();
            var user = from u in ticketParticipants_Result
                       where u.UserID != UserID
                       select u;
            ddlUsers.DataSource = user;
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("--Select Assignee--", "0"));
        }
        /// <summary>
        /// 
        /// </summary>
        private void ResetValues()
        {
            txtMessage.Value = string.Empty;
            TicketUtility.ClearTickets(dvTicket, ltMessage);
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
            uploadFirstAlternative();
            TicketThread ticketThread = new TicketThread();
            ticketThread.IsActive = Convert.ToBoolean(Common.IsActive.Yes);
            ticketThread.AssignedTo = Convert.ToInt32(ddlUsers.SelectedValue);
            ticketThread.Status = Convert.ToInt32(Common.CaseStatus.Open);
            ticketThread.TicketThreadID = TicketThreadID;
            string newPassword = string.Empty;

            Ticket ticket = new Ticket();
            ticket.IsActive = Convert.ToBoolean(Common.IsActive.Yes);
            ticket.TicketText = HttpUtility.HtmlEncode(txtMessage.Value.Replace("\n", "<br />"));
            ticket.AssignedFrom = UserID;

            ticket.TicketThreadID = TicketThreadID;
            ticket.HasAttachment = Convert.ToBoolean(Common.IsActive.No);

            TicketsAttachment ticketsAttachment = new TicketsAttachment();
            if (AttachmentLocation == string.Empty)
            {
                AttachmentLocation = AttachmentLocationU;
            }
            if (ContentType == string.Empty)
            {
                ContentType = ContentTypeU;
            }
            if (AttachmentLocation != string.Empty)
            {
                ticketsAttachment.LocationURL = AttachmentLocation;
                ticketsAttachment.ContentType = ContentType;
                ticket.HasAttachment = Convert.ToBoolean(Common.IsActive.Yes);
                AttachmentLocation = string.Empty;
                ContentType = string.Empty;
                AttachmentLocationU = string.Empty;
                ContentTypeU = string.Empty;
            }
            ticket.TicketsAttachments.Add(ticketsAttachment);

            ticketThread.Tickets.Add(ticket);

            TicketManager ticketManager = new TicketManager();
            string result = string.Empty;
            if (IsValidFile)
            {
                try
                {
                    Tuple<bool, int> insertResult = ticketManager.InsertTicket(ticketThread);
                    if (insertResult.Item1 == true)
                    {
                        int submitTicketTO_Result = new TicketManager().TicketToInsert(insertResult.Item2, Convert.ToInt32(ddlUsers.SelectedValue));
                        //Insert attachment details
                        if (ticket.HasAttachment == true)
                        {
                            TicketsAttachment ticketsAttachmentInsert = ticket.TicketsAttachments.SingleOrDefault();
                            ticketsAttachmentInsert.TicketID = insertResult.Item2;
                            ticketManager.GetAttachmentInsert(ticketsAttachmentInsert);
                        }
                        //Update Ticket Thread
                        var ticketThreadUpdate = ticketManager.GetTicketThreadUpdate(TicketThreadID, Convert.ToInt32(ddlUsers.SelectedValue), UserID);

                        //TicketUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Added);
                        result = Resource.Info_Added;
                        BindGrid();
                        ddlUsers.SelectedIndex = 0;
                        txtMessage.Value = string.Empty;
                        updGrid.Update();

                        TicketUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), "Thank you for your response. The ticket has been assigned to the selected user.");
                        
                        
                        //Response.Redirect(Request.RawUrl);
                    }
                }
                catch (BLLException exception)
                {
                    TicketUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
                    ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
                    result = exception.ErrorMessage;

                }
                catch (Exception exception)
                {
                    TicketUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                    ExceptionUtility.LogException(exception, "Sender: " + Request.RawUrl);
                    result = Resource.Err_General;
                }
            }
            else
                TicketUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
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
        //                TicketUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
        //            else
        //            {
        //                string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_TICKETIMAGESURL] + Convert.ToString(OrderID);

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


        protected void File_Uploadx(object sender, AjaxFileUploadEventArgs e)
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
                    MessageUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                        Resource.Err_InvalidImage);
                else
                {


                    string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_TICKETIMAGESURL];

                    bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

                    AttachmentLocation = attachmentURL + @"\" + fileName;
                    ContentType = e.ContentType;
                    specimenFileUpload2.SaveAs(Server.MapPath(AttachmentLocation));

                }
            }
            catch (Exception)
            {
            }
        }
        public void uploadFirstAlternative()
        {

            try
            {

                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        message = message + "<b>" + hpf.FileName + "</b> (" + hpf.ContentType
                            + ") - <i>Upload1ed</i> <i class=\"icon-ok\"></i>";
                        string fileName = string.Concat(Path.GetFileNameWithoutExtension(hpf.FileName),
                            DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                            Path.GetExtension(hpf.FileName)
                            );
                        int contentLength = hpf.ContentLength;


                        IsValidFile = (contentLength < Convert.ToInt32(Resource.ValidImageFileSize)) ? true : false;

                        if (!IsValidFile)
                            MessageUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                                Resource.Err_InvalidImage);
                        else
                        {


                            string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_TICKETIMAGESURL];

                            bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

                            if (!exists)
                                System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

                            AttachmentLocation = attachmentURL + @"\" + fileName;
                            ContentType = hpf.ContentType;
                            hpf.SaveAs(Server.MapPath(attachmentURL + @"\" + fileName));

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}