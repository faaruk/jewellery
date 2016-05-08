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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Collaboration.Web.UI.Common.SESSION_USER] != null)
                lblUserName.Text = (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserName;
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

            lblTicketsAssigned.Text = lblTicketsAssigned1.Text = Convert.ToString(ticketsAssignedTo.Count);
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

        protected void btnOpenMessage_Click(object sender, EventArgs e)
        {
            Session["RequestedMessageThredID"] = hdnMessageThredID.Value;
            string redirectUrl = ConfigurationManager.AppSettings["RootPath"].ToString() + "/Messages/Messages.aspx?FilterID=" + Convert.ToInt32(hdnFilterID.Value);
            Response.Redirect(redirectUrl);
                
        }
    }
}


