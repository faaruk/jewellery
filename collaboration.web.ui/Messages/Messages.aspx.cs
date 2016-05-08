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
namespace Collaboration.Web.UI.Messages
{
    public partial class MessagesN : BasePage
    {
        Collaboration.Business.Components.MessageManager _messageManager = new Business.Components.MessageManager();
        static int _userID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        static int _messageThreadID = 0;
        public int MessageThreadID { set { _messageThreadID = value; } get { return _messageThreadID; } }
        public int FilterID { set { } get { return Convert.ToInt32(Request.QueryString[Common.REQUEST_FILTERID]); } }
        //public int RequestMessageThreadID { set { } get { return Convert.ToInt32(Request.QueryString[Common.REQUEST_MessageThreadID]); } }


        protected void Page_Load(object sender, EventArgs e)
        {
            int countUnreadMessages = _messageManager.GetCountUnreadMessages(UserID);
            ViewMessages.btnHandler += new UserControl.UC_Messages.OnButtonClick(ViewMessages_btnHandler);
            if (!IsPostBack)
            {
                BindGrid();
                //((DasbhoardMaster)this.Master).UpdateTickets();
                //((DasbhoardMaster)this.Master).UpdateOrderdAssigned();
                
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
                int messageThreadID = Convert.ToInt32(commandArgs[0]);
                int OrderID = Convert.ToInt32(commandArgs[1]);
                ShowViewDetailsDialog(messageThreadID, OrderID);
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
                HiddenField hdHasUnreadMessages = (HiddenField)e.Row.FindControl("hdHasUnreadMessages");
                if (Convert.ToBoolean(hdHasUnreadMessages.Value) == true)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                    e.Row.Font.Bold = true;
                }
                Label lblNoOfMessages = (Label)e.Row.FindControl("lblNoOfMessages");
                Label lblModifyDate = (Label)e.Row.FindControl("lblModifyDate");
                Label lblUserName = (Label)e.Row.FindControl("lblUserName");
                
                if (lblNoOfMessages.Text == "0")
                {
                    lblModifyDate.Text = "";
                    lblUserName.Text = "";
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowViewDetailsDialog(int messageThreadID, int OrderID)
        {
            //pnl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
            mpViewDetails.Show();
            ViewMessages.MessageThreadID = MessageThreadID = messageThreadID;
            ViewMessages.OrderID = OrderID;
            ViewMessages.FillInfo();
            updViewDetails.Update();

        }

        /// <summary>uyq
        /// 
        /// </summary>
        private void BindGrid()
        {
            IEnumerable<MessageThreads_Result> messageThreads_Result = _messageManager.GetMessageThreads(UserID, 0);
            Session[Collaboration.Web.UI.Common.SESSION_MESSAGETHREADS] = messageThreads_Result;
            if (FilterID == Convert.ToInt32(Common.FilterType.AssignedTo))
                gvTable.DataSource = messageThreads_Result.Where(x => x.AssignedToUserID == UserID);
            else if (FilterID == Convert.ToInt32(Common.FilterType.UnRead))
                gvTable.DataSource = messageThreads_Result.Where(x => x.HasUnReadMessages == true);
            else
                gvTable.DataSource = messageThreads_Result;
            gvTable.DataBind();

            if (Session["RequestedMessageThredID"] != null)
            {
                ShowViewDetailsDialog(Convert.ToInt32(Session["RequestedMessageThredID"]), 0);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        void ViewMessages_btnHandler(string strValue)
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
            Session["RequestedMessageThredID"] = null;
            bool markAsRead = new MessageManager().MarkMessagesAsRead(MessageThreadID, UserID);
            if (markAsRead)
            {
                BindGrid();
                updGrid.Update();
                Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES] = new MessageManager().GetCountUnreadMessages(UserID);
                Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] = null;
                Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] = null;
                //(this.Master as DasbhoardMaster).UpdateTickets();
                //((DasbhoardMaster)this.Master).UpdateTickets();


            }
        }
    }
}