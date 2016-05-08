using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Business.Entities;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;

namespace Collaboration.Data
{
    public class TicketContext
    {
        #region Tickets
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public int GetAttachmentInsert(TicketsAttachment ticketsAttachment)
        {
            using (var context = new CollaborationDBContext())
            {
                var modelType = (context.TicketAttachment_InsertNew(ticketsAttachment.TicketID, ticketsAttachment.LocationURL, ticketsAttachment.ContentType, true));
                var aidd = (dynamic)modelType;                
                var aid = (int)modelType.ToList().SingleOrDefault();
                return aid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        public int TicketToInsert(int TicketID, int AssignedTo)
        {
            using (var context = new CollaborationDBContext())
            {
                var modelType = (context.TicketTo_Insert(TicketID, AssignedTo, true));
                var aidd = (dynamic)modelType;                
                var aid = (int)modelType.ToList().SingleOrDefault();
                return aid;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<TicketParticipants_Result> GetTicketParticipants()
        {
            using (var context = new CollaborationDBContext())
            {
                var modelType = context.TicketParticipants_Select();
                return modelType.ToList <TicketParticipants_Result>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public bool GetTicketThreadUpdate(int ticketThreadID, int assignedTo, int assignedFrom)
        {
            using (var context = new CollaborationDBContext())
            {
                var modelType = context.TicketThread_UpdateAssignNew(ticketThreadID, assignedTo, assignedFrom);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<TicketThreads_Result> GetTicketThreads(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var ticketThreads = context.TicketThread_Select(userID);
                var toList = ticketThreads.ToList<TicketThreads_Result>();
                return toList;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<TicketThreads_Result> GetTicketThreadsClosed(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var ticketThreads = context.TicketThreadClosed_Select(userID);
                var toList = ticketThreads.ToList<TicketThreads_Result>();
                return toList;
            }
        }
        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetTickets(int ticketThreadID)
        {
            using (var context = new CollaborationDBContext())
            {
                var tickets = context.Ticket_Select(ticketThreadID);
                return tickets.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public int CreateCase(TicketThread ticketThread, TicketsAttachment ticketsAttachment)
        {
            using (var context = new CollaborationDBContext())
            {
                Ticket ticket = ticketThread.Tickets.SingleOrDefault();
                //TicketTo ticketTo = ticket.TicketToes.SingleOrDefault();
                //TicketsAttachment ticketsAttachment = ticket.TicketsAttachments.SingleOrDefault();

                var tickets = context.Create_CaseForTicketNew(ticketThread.CreatedBy, ticketThread.AssignedTo, ticketThread.Subject, ticket.TicketText, ticket.IsActive, ticketThread.Status, ticketsAttachment.LocationURL, ticketsAttachment.ContentType, ticket.HasAttachment,0);
                var aidd = (dynamic)tickets;
                var aid = (int)tickets.ToList().SingleOrDefault();
                return aid;
            }
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public Tuple<bool, int> InsertTicket(TicketThread ticketThread)
        {
            Tuple<bool, int> ret = new Tuple<bool, int>(false, -1);

            try
            {
                using (var context = new CollaborationDBContext())
                {
                    Ticket ticket = ticketThread.Tickets.SingleOrDefault();
                    // TicketTo ticketTo = ticket.TicketToes.SingleOrDefault();
                    TicketsAttachment ticketsAttachment = ticket.TicketsAttachments.SingleOrDefault();

                    var tickets = (context.Ticket_InsertNew(ticketThread.TicketThreadID, ticket.TicketText, ticket.AssignedFrom, Convert.ToBoolean(ticket.IsActive), Convert.ToInt16(ticket.HasAttachment)));
                    var tid = (int)tickets.SingleOrDefault();
                    ret = new Tuple<bool, int>(true, tid);
                }
            }
            catch (Exception)
            {
                
            }
            return ret; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetUnreadTickets(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                int countUnreadTickets = context.ExecuteStoreQuery<int>("SELECT [dbo].[ufnCountUnreadTickets](@UserID)", new SqlParameter { ParameterName = "UserID", Value = userID }).FirstOrDefault();
                return countUnreadTickets;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAssignedTickets(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                int countUnreadTickets = context.ExecuteStoreQuery<int>("SELECT [dbo].[ufnCountAssignedTickets](@UserID)", new SqlParameter { ParameterName = "UserID", Value = userID }).FirstOrDefault();
                return countUnreadTickets;
            }
        }
        /// <summary>
        /// Done
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool MarkTicketsAsRead(int ticketThreadID, int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var isRead = context.TicketTo_MarkRead(ticketThreadID, userID, true);
                return true;
            }
        }
        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetTicketAssignedTo(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var tickets = context.Ticket_SelectByAssignedTo(userID);
                return tickets.ToList();
            }
        }
        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetUnReadTickets(int userID, bool hasRead)
        {
            using (var context = new CollaborationDBContext())
            {
                var tickets = context.Ticket_SelectByReadStatus(userID, hasRead);
                return tickets.ToList();
            }
        }
        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetTicketsByUserID(int userID, bool hasRead)
        {
            using (var context = new CollaborationDBContext())
            {
                var tickets = context.Ticket_SelectByUserID(userID, hasRead);
                return tickets.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TicketThreadMassUpdate"></param>
        /// <returns></returns>
        public bool TicketThreadMassUpdate(string TicketThreadIDs, int Updatedby)
        {
            using (var context = new CollaborationDBContext())
            {

                var iReturnValue = context.TicketThread_MassUpdate(TicketThreadIDs, Updatedby);
                return true;
            }
        }
        #endregion
    }
}
