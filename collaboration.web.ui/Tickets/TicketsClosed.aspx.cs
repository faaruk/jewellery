using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Components;
using Collaboration.Business.Entities;
using Collaboration.Web.UI.Utilities;
using System.Configuration;
using System.IO;
using AjaxControlToolkit;

namespace Collaboration.Web.UI.Tickets
{
    public partial class TicketsClosed : BasePage
    {
        private static string message = string.Empty;

        Collaboration.Business.Components.TicketManager _ticketManager = new Business.Components.TicketManager();
        static int _userID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        static int _roleID = 0;
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }
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
            IEnumerable<TicketThreads_Result> ticketThreads_Result;
            if (RoleID == Convert.ToInt32(Resource.DB_AdminRoleID))
            {
                ticketThreads_Result = _ticketManager.GetTicketThreadsClosed(0);
            }
            else
            {
                ticketThreads_Result = _ticketManager.GetTicketThreadsClosed(UserID);
            }
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



    }
}