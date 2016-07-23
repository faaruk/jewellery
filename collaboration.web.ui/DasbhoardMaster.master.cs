﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Collaboration.Business.Entities;
using Collaboration.Web.UI.Utilities;
using System.Web.Security;
using Collaboration.Business.Components;
namespace Collaboration.Web.UI
{
    public partial class DasbhoardMaster : BaseMasterPage
    {
        static int _userID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int countUnReadMessages { set { Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES] = value; } get { return (Convert.ToInt32(Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES])); } }
        public int countUnReadTickets { set { Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADTICKETS] = value; } get { return (Convert.ToInt32(Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADTICKETS])); } }
        public List<Orders_Result> ordersAssigned { set { Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] as List<Orders_Result>); } }
        public List<Messages_Result> messagesAssigned { set { Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] as List<Messages_Result>); } }
        public List<Messages_Result> unReadMessages { set { Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] as List<Messages_Result>); } }
        public List<Tickets_Result> unReadTickets { set { Session[Collaboration.Web.UI.Common.SESSION_UNREADTICKETS] = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_UNREADTICKETS] as List<Tickets_Result>); } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitHead();
            UpdateTickets();
            UpdateOrderdAssigned();
            SetActivePage();
            Page.Header.DataBind();
            //if (!IsPostBack)
            //    ClearSession();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearSession()
        {
            Session.Remove(Common.SESSION_USERSLIST);
            Session.Remove(Common.SESSION_MODELTYPELIST);
            Session.Remove(Common.SESSION_METALLIST);
            Session.Remove(Common.SESSION_PROCESSTYPELIST);
            Session.Remove(Common.SESSION_FINGERSIZELIST);
            Session.Remove(Common.SESSION_PRIORITIESLIST);
            Session.Remove(Common.SESSION_RINGTYPESLIST);
            Session.Remove(Common.SESSION_MESSAGETHREADS);
            Session.Remove(Common.SESSION_DOWNLOADFILENAME);
            Session.Remove(Common.SESSION_DOWNLOADCONTENTTYPE);
            Session.Remove(Common.SESSION_ORDERS);
            Session.Remove(Common.SESSION_SPECIMENIMAGE);
            Session.Remove(Common.SESSION_CADIMAGE);
            Session.Remove(Common.SESSION_CADS);
            Session.Remove(Common.SESSION_COMAPARECADIMAGE);
            Session.Remove(Common.SESSION_SAMPLES);
            Session.Remove(Common.SESSION_SPECIMENIMAGES);
        }
        /// <summary>
        ///     
        /// </summary>
        private void SetActivePage()
        {
            string rawURL = Request.RawUrl;
            rawURL = rawURL.Substring(rawURL.LastIndexOf('/') + 1);
            if (rawURL.ToLower() == Common.Pages.DEFAULT.ToLower())
                PageUtility.SetClass(dvDefault);
            else if (rawURL.ToLower() == Common.Pages.CREATEORDER.ToLower())
                PageUtility.SetClass(dvOrders, liCreateOrder);
            else if (rawURL.ToLower() == Common.Pages.VIEWORDER.ToLower() || rawURL.ToLower().Contains(Common.Pages.VIEWORDERDETAILS.ToLower()))
                PageUtility.SetClass(dvOrders, liViewOrder);
            else if (rawURL.ToLower() == Common.Pages.MESSAGETHREADS.ToLower())
                PageUtility.SetClass(dvMessg);
            if ((Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID == Convert.ToInt32(Common.Roles.Admin))
            {
                if (rawURL.ToLower() == Common.Pages.PROCESSTYPES.ToLower())
                    PageUtility.SetClass(dvMgmt, liProcess);
                else if (rawURL.ToLower() == Common.Pages.FINGERMEASUREMENTS.ToLower())
                    PageUtility.SetClass(dvMgmt, liFingerSize);
                else if (rawURL.ToLower() == Common.Pages.METALS.ToLower())
                    PageUtility.SetClass(dvMgmt, liMetal);
                else if (rawURL.ToLower() == Common.Pages.MODELTYPES.ToLower())
                    PageUtility.SetClass(dvMgmt, liModel);
                else if (rawURL.ToLower() == Common.Pages.USERMANAGEMENT.ToLower())
                    PageUtility.SetClass(dvMgmt, liUser);
                else if (rawURL.ToLower() == Common.Pages.PRIORITIES.ToLower())
                    PageUtility.SetClass(dvMgmt, liPriority);
                else if (rawURL.ToLower() == Common.Pages.RINGTYPES.ToLower())
                    PageUtility.SetClass(dvMgmt, liRingType);
                else if (rawURL.ToLower() == Common.Pages.CUSTOMER.ToLower())
                    PageUtility.SetClass(dvMgmt, liCustomer);
                else if (rawURL.ToLower() == Common.Pages.SAMPLESTATUS.ToLower())
                    PageUtility.SetClass(dvMgmt, liSampleStatus);
                else if (rawURL.ToLower() == Common.Pages.CANCELORDER.ToLower())
                    PageUtility.SetClass(dvOrders, liCancelOrder);
                //else if (rawURL.ToLower() == Common.Pages.TICKETTHREADS.ToLower())
                //    PageUtility.SetClass(dvOrders, liTicket);
                else if (rawURL.ToLower() == "Tickets.aspx".ToLower())
                    PageUtility.SetClass(dvTicket, liTickets);
                else if (rawURL.ToLower().Contains("TicketsClosed.aspx".ToLower()))
                    PageUtility.SetClass(dvTicket, liTicketsClosed);
                else if (rawURL.ToLower().Contains("TrackSamples.aspx".ToLower()))
                    PageUtility.SetClass(dvSamples, li1);
                else if (rawURL.ToLower().Contains("ReturnedSamples.aspx".ToLower()))
                    PageUtility.SetClass(dvSamples, li2);
            }
            else
            {
                dvMgmt.Visible = false;
                CancelOrderPlaceHolder.Visible = false;
            }
            if ((Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID == Convert.ToInt32(Common.Roles.Factory))
            {
                liCreateOrder.Visible = false;
            }
            else { liCreateOrder.Visible = true; }

        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateTickets()
        {
            //if (Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES] == null)
            //    UnReadMessages = new MessageManager().GetCountUnreadMessages(UserID);

            //Tickets
            //if (Session[Collaboration.Web.UI.Common.SESSION_UNREADTICKETS] == null)
            //{
                unReadTickets = new TicketManager().GetUnReadTickets(UserID, false);
                countUnReadTickets = unReadTickets.Count();
            //}


            //Messages
            if (Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] == null)
            {
                unReadMessages = new MessageManager().GetUnReadMessages(UserID, false);
                countUnReadMessages = unReadMessages.Count();
            }
            if (Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] == null)
            {
                messagesAssigned = new MessageManager().GetMessageAssignedTo(UserID);
            }

            var Tickets = new TicketManager().GetTicketAssignedTo(UserID);
            //countUnReadTickets = Tickets.Count();
            Head1.UpdateTickets(Tickets, unReadTickets);
            Head1.UpdateMessages(messagesAssigned, unReadMessages);

            if (countUnReadMessages > 0)
                spCountUnread.Visible = true;
            else
                spCountUnread.Visible = false;
            lblUnreadMessages.Text = Convert.ToString(countUnReadMessages);

            //Tickets
            if (countUnReadTickets > 0)
                spCountUnreadTikets.Visible = true;
            else
                spCountUnreadTikets.Visible = false;
            lblUnreadTickets.Text = Convert.ToString(countUnReadTickets);

            updTicket.Update();
            pnlUpd.Update();
            pnlHeader.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateOrderdAssigned()
        {
            if (Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] == null)
                ordersAssigned = new OrderManager().GetOrders(UserID, 0, UserID);
            Head1.UpdateOrders(ordersAssigned);
        }

       void InitHead()
        {
            HeadLiteral.Text = string.Format(@"
            <link rel=""stylesheet"" href=""{0}Styles/bootstrap.min.css"" />
            <link rel=""stylesheet"" href=""{0}Styles/bootstrap-reset.css"" />            
            <link rel=""stylesheet"" href=""{0}Styles/style.css"" />
            <link rel=""stylesheet"" href=""{0}Styles/style-responsive.css"" />
            <link rel=""stylesheet"" href=""{0}assets/font-awesome/css/font-awesome.min.css"" />
            <link rel=""stylesheet"" href=""{0}assets/data-tables/DT_bootstrap.css"" />    
            <link rel=""stylesheet"" href=""{0}Styles/bootstrap.css"" />
            <link rel=""stylesheet"" href=""{0}assets/fancybox/jquery.fancybox.css"" />",
            ResolveUrl("~/"));
        }
    }
}
