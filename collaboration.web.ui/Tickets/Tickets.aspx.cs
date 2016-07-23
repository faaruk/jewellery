using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Collaboration.Business.Components;
using Collaboration.Business.Entities;
using Collaboration.Web.UI.Utilities;
using System.Configuration;
using System.IO;
using AjaxControlToolkit;

namespace Collaboration.Web.UI.Tickets
{
    public partial class TicketsNew : BasePage
    {
        private static string message = string.Empty;

        Collaboration.Business.Components.TicketManager _ticketManager = new Business.Components.TicketManager();
        static int _userID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        static int _ticketThreadID = 0;
        public int TicketThreadID { set { _ticketThreadID = value; } get { return _ticketThreadID; } }
        public int FilterID { set { } get { return Convert.ToInt32(Request.QueryString[Common.REQUEST_FILTERID]); } }

        static String AttachmentLocation = string.Empty;
        static String ContentType = string.Empty;
        static bool IsValidFile = true;
        static bool IsFileUploaded = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            int countUnreadTickets = _ticketManager.GetCountUnreadTickets(UserID);
            ViewTickets.btnHandler += new UserControl.UC_TicketsNew.OnButtonClick(ViewTickets_btnHandler);
            if (!IsPostBack)
            {
                BindGrid();
                BindUsers();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == Convert.ToString(Common.ActionType.ViewDetails))
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                int ticketThreadID = Convert.ToInt32(commandArgs[0]);
                //int OrderID = Convert.ToInt32(commandArgs[1]);
                ShowViewDetailsDialog(ticketThreadID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (gvTable.Rows.Count > 0)
            {
                //This will add the <thead> and <tbody> elements
                gvTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:EditableTable.init();", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find dropdown control & get values
                HiddenField hdHasUnreadTickets = (HiddenField)e.Row.FindControl("hdHasUnreadMessages");
                if (Convert.ToBoolean(hdHasUnreadTickets.Value) == true)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                    e.Row.Font.Bold = true;
                }

                //if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CreatedBy")) == UserID)
                //{
                //    LinkButton lnlCloseTicket = (LinkButton)e.Row.FindControl("LinkButton");
                //    lnlCloseTicket.Visible = true;
                //}

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowViewDetailsDialog(int ticketThreadID)
        {
            //pnl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
            mpViewDetails.Show();
            ViewTickets.TicketThreadID = TicketThreadID = ticketThreadID;
            ViewTickets.FillInfo();
            updViewDetails.Update();

        }

        /// <summary>uyq
        /// 
        /// </summary>
        private void BindGrid()
        {
            IEnumerable<TicketThreads_Result> ticketThreads_Result = _ticketManager.GetTicketThreads(UserID);
            Session[Collaboration.Web.UI.Common.SESSION_TICKETTHREADS] = ticketThreads_Result;
            if (FilterID == Convert.ToInt32(Common.FilterType.AssignedTo))
                gvTable.DataSource = ticketThreads_Result.Where(x => x.AssignedToUserID == UserID);
            else if (FilterID == Convert.ToInt32(Common.FilterType.UnRead))
                gvTable.DataSource = ticketThreads_Result.Where(x => (x.HasUnReadTickets) == true);
            else
                gvTable.DataSource = ticketThreads_Result;
            gvTable.DataBind();

            if (Session["RequestedTicketThredID"] != null)
            {
                ShowViewDetailsDialog(Convert.ToInt32(Session["RequestedTicketThredID"]));

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        void ViewTickets_btnHandler(string strValue)
        {
            mpViewDetails.Show();
            updViewDetails.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        protected void btnCancelDetails_Click(object sender, EventArgs e)
        {
            Session["RequestedTicketThredID"] = null;
            bool markAsRead = new TicketManager().MarkTicketsAsRead(TicketThreadID, UserID);
            if (markAsRead)
            {
                BindGrid();
                updGrid.Update();
                Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADTICKETS] = new TicketManager().GetCountUnreadTickets(UserID);
                Session[Collaboration.Web.UI.Common.SESSION_UNREADTICKETS] = null;
                Session[Collaboration.Web.UI.Common.SESSION_TICKETSASSIGNED] = null;
                //(this.Master as DasbhoardMaster).UpdateTickets();
            }
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitTicket_Click(object sender, EventArgs e)
        {
            //uploadFirstAlternative();
            TicketThread ticketThread = new TicketThread();
            ticketThread.AssignedTo = Convert.ToInt32(ddlUsers.SelectedValue);
            ticketThread.ClosingRemarks = "";
            ticketThread.CreateDate = DateTime.UtcNow;
            ticketThread.CreatedBy = UserID;
            ticketThread.IsActive = true;
            ticketThread.LastModifiedBy = UserID;
            ticketThread.ModifyDate = DateTime.UtcNow;
            ticketThread.Status = 1;
            ticketThread.Subject = txtTicketSubject.Text;
            ticketThread.TicketThreadID = 0;

            TicketsAttachment ticketsAttachment = new TicketsAttachment();

            Ticket ticket = new Ticket();
            //ticket.TicketText = txtDescription.Text;
            //ticket.TicketText = HttpUtility.HtmlEncode(txtDescription.Value.Replace("\n", "<br />"));
            ticket.TicketText = HttpUtility.HtmlEncode(txtDescription.Value.Replace("\n", "<br />"));

            ticket.IsActive = true;
            if (IsFileUploaded)
            {
                ticket.HasAttachment = true;
                ticketsAttachment.ContentType = ContentType;
                ticketsAttachment.LocationURL = AttachmentLocation;
                IsFileUploaded = false;
            }

            ticketThread.Tickets.Add(ticket);

            int submitTicket_Result = new TicketManager().CreateCase(ticketThread, ticketsAttachment);
            if (submitTicket_Result > 0)
            {
                int submitTicketTO_Result = new TicketManager().TicketToInsert(submitTicket_Result, Convert.ToInt32(ddlUsers.SelectedValue));

                TicketsAttachment ticketsAttachmentInsert = new TicketsAttachment();
                ticketsAttachmentInsert.TicketID = submitTicket_Result;
                ticketsAttachmentInsert.LocationURL = AttachmentLocation;
                ticketsAttachmentInsert.ContentType = ContentType;
                ticketsAttachmentInsert.IsActive = true;

                TicketManager ticketManager = new TicketManager();
                ticketManager.GetAttachmentInsert(ticketsAttachmentInsert);
                AttachmentLocation = string.Empty;
                ContentType = string.Empty;
                ViewTickets.AttachmentLocationU = AttachmentLocation;
                ViewTickets.ContentTypeU = ContentType;
                updViewDetails.Update();
                MessageUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Added);
                ltMessage.Text = Resource.Info_Added;
                BindGrid();
                updGrid.Update();
            }
            else
            {
                MessageUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), "Failed to create ticket. Please try again.");
                ltMessage.Text = "Failed to create ticket. Please try again.";
            }
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
        //                string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_ATTACHMENTURL] + Convert.ToString(TicketThreadID);

        //                bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

        //                if (!exists)
        //                    System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

        //                AttachmentLocation = attachmentURL + @"\" + string.Concat(Path.GetFileNameWithoutExtension(flFile.PostedFile.FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff"),
        //    Path.GetExtension(flFile.PostedFile.FileName));
        //                ContentType = flFile.ContentType;
        //                flFile.SaveAs(Server.MapPath(AttachmentLocation));
        //                IsFileUploaded = true;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}


        protected void File_Upload1(object sender, AjaxFileUploadEventArgs e)
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


                IsValidFile = (contentLength < Convert.ToInt32(Resource.ValidImageFileSize)) ? true : false;

                if (!IsValidFile)
                    MessageUtility.ShowMessage(dvTicket, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                        Resource.Err_InvalidImage);
                else
                {


                    string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_ATTACHMENTURL] + Convert.ToString(TicketThreadID);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

                    AttachmentLocation = attachmentURL + @"\" + fileName;
                    ContentType = e.ContentType;
                    specimenFileUpload1.SaveAs(Server.MapPath(AttachmentLocation));
                    IsFileUploaded = true;
                    ViewTickets.AttachmentLocationU = AttachmentLocation;
                    ViewTickets.ContentTypeU = ContentType;
                    //ViewTickets.FillInfo();
                    updViewDetails.Update();

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            MassUpdate();
        }
        /// <summary>
        /// 
        /// </summary>
        public void MassUpdate()
        {
            string completeString = CreateListOfItem();

            if (completeString.Length == 0)
            {
                MessageUtility.ShowMessage(divConfirmation, ltConfirmation, Convert.ToInt16(Common.MessageTypes.Warning), Resource.UI_WarningMessageClass);
                ltConfirmation.Text = Resource.Info_Updated;
            }
            else
            {
                Collaboration.Business.Components.TicketManager _ticketManager = new Business.Components.TicketManager();
                if (_ticketManager.TicketThreadMassUpdate(completeString, (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID))
                {
                    ltConfirmation.Text = Resource.Info_Updated;
                    MessageUtility.ShowMessage(divConfirmation, ltConfirmation, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Updated);
                }


                BindGrid();
                //btnUpdate.CssClass = Resource.UI_BtnSuccess;
                //btnUpdate.Enabled = false;
                updGrid.Update();
            }
        }
        private string CreateListOfItem()
        {
            StringBuilder strServiceList = new StringBuilder();
            foreach (GridViewRow gr in gvTable.Rows)
            {

                CheckBox _chkRow = gr.FindControl("chkRow") as CheckBox;
                if (_chkRow.Checked == true)
                {
                    TextBox _txtRefNo = gr.FindControl("txtKeyField") as TextBox;
                    strServiceList.Append("" + _txtRefNo.Text + "");
                    strServiceList.Append("û");
                    strServiceList.Append("£");
                }
            }
            string strText = strServiceList.ToString();
            //if (strText.Length > 0)
            //{
            //    strText = strText.Substring(0, strText.Length - 1);
            //}
            return strText;

        }
    }
}