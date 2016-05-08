using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using System.Web.Security;
using Collaboration.Business.Components;
using System.IO;
using Collaboration.Web.UI.Utilities;
using System.Configuration;
namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_Header : System.Web.UI.UserControl
    {
        static int _userID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int countUnReadMessages { set { Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES] = value; } get { return (Convert.ToInt32(Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES])); } }
        public int countUnReadTickets { set { Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADTICKETS] = value; } get { return (Convert.ToInt32(Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADTICKETS])); } }
        public List<Orders_Result> ordersAssigned { set { Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] as List<Orders_Result>); } }
        public List<Messages_Result> messagesAssigned { set { Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] as List<Messages_Result>); } }
        public List<Messages_Result> unReadMessages { set { Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] as List<Messages_Result>); } }
        public List<Tickets_Result> unReadTickets { set { Session[Collaboration.Web.UI.Common.SESSION_UNREADTICKETS] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_UNREADTICKETS] as List<Tickets_Result>); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Collaboration.Web.UI.Common.SESSION_USER] != null)
            {
                lblUserName.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserName;
                //((DasbhoardMaster)this.Master).UpdateTickets();
                //((DasbhoardMaster)this.Master).UpdateOrderdAssigned();
                //((DasbhoardMaster)this.Page).UpdateTickets();
                //((DasbhoardMaster)this.Page).UpdateOrderdAssigned();
                UpdateTickets();
                //((DasbhoardMaster)this.Page.Master).UpdateTickets();
                //((DasbhoardMaster)this.Page.Master).UpdateOrderdAssigned();
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Session.Clear();
            FormsAuthentication.SignOut();
            // FormsAuthentication.RedirectToLoginPage();
            Roles.DeleteCookie();
            Response.Redirect("~/Account/Login.aspx");
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateMessages(List<Messages_Result> messagesAssignedTo, List<Messages_Result> messagesUnRead)
        {

            lblMessagesAssigned.Text = lblMessagesAssigned1.Text = Convert.ToString(messagesAssignedTo.Count);
            lstMessagesAssigned.DataSource = messagesAssignedTo.Take(10);
            lstMessagesAssigned.DataBind();

            lblUnreadMessages.Text = lblUnreadMessages1.Text = Convert.ToString(messagesUnRead.Count);
            lstUnReadMessages.DataSource = messagesUnRead.Take(10);
            lstUnReadMessages.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateTickets(List<Tickets_Result> ticketsAssignedTo, List<Tickets_Result> ticketsUnRead)
        {

            lblTicketsAssigned.Text = lblTicketsAssigned1.Text = Convert.ToString(ticketsUnRead.Count);
            lstTicketsAssigned.DataSource = ticketsAssignedTo.Take(10);
            lstTicketsAssigned.DataBind();

            //lblUnreadTickets.Text = lblUnreadTickets1.Text = Convert.ToString(ticketsUnRead.Count);
            //lstUnReadTickets.DataSource = ticketsUnRead.Take(10);
            //lstUnReadTickets.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateOrders(List<Orders_Result> ordersAssignedTo)
        {
            lblOrdersAssigned.Text = lblOrdersAssigned1.Text = Convert.ToString(ordersAssignedTo.Count);
            lstOrdersAssigned.DataSource = ordersAssignedTo.Take(10);
            lstOrdersAssigned.DataBind();
        }

        public void UpdateTickets()
        {
            //if (Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES] == null)
            //    UnReadMessages = new MessageManager().GetCountUnreadMessages(UserID);

            //Tickets
            //if (Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] == null)
            //{
                unReadTickets = new TicketManager().GetUnReadTickets(UserID, false);
                countUnReadTickets = unReadTickets.Count();
            //}


            //Messages
            //if (Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] == null)
            //{
                unReadMessages = new MessageManager().GetUnReadMessages(UserID, false);
                countUnReadMessages = unReadMessages.Count();
            //}
            //if (Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] == null)
            //{
                messagesAssigned = new MessageManager().GetMessageAssignedTo(UserID);
            //}
            //if (Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] == null)
                ordersAssigned = new OrderManager().GetOrders(UserID, 0, UserID);

                
            var Tickets = new TicketManager().GetTicketAssignedTo(UserID);
            UpdateTickets(Tickets, unReadTickets);
            UpdateMessages(messagesAssigned, unReadMessages);
            UpdateOrders(ordersAssigned);
            //if (countUnReadMessages > 0)
            //    spCountUnread.Visible = true;
            //else
            //    spCountUnread.Visible = false;
            lblUnreadMessages.Text = Convert.ToString(countUnReadMessages);

            ////Tickets
            //if (countUnReadTickets > 0)
            //    spCountUnreadTikets.Visible = true;
            //else
            //    spCountUnreadTikets.Visible = false;
            //lblUnreadTickets.Text = Convert.ToString(countUnReadTickets);
        }

        protected void btnOpenMessage_Click(object sender, EventArgs e)
        {
            Session["RequestedMessageThredID"] = hdnMessageThredID.Value;
            string redirectUrl = ConfigurationManager.AppSettings["RootPath"].ToString() + "/Messages/Messages.aspx?FilterID=" + Convert.ToInt32(hdnFilterID.Value);
            Response.Redirect(redirectUrl);

        }
    }
}