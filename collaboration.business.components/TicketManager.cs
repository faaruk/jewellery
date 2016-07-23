using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Data;
using Collaboration.Business.Entities;
using Collaboration.Business.Components.Utilities;

namespace Collaboration.Business.Components
{
    public class TicketManager
    {
        #region Tickets
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetAttachmentInsert(TicketsAttachment ticketsAttachment)
        {
            try
            {
                return new TicketContext().GetAttachmentInsert(ticketsAttachment);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TicketParticipants_Result> GetTicketParticipants()
        {
            try
            {
                return new TicketContext().GetTicketParticipants();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool GetTicketThreadUpdate(int ticketThreadID, int assignedTo, int assignedFrom)
        {
            try
            {
                return new TicketContext().GetTicketThreadUpdate(ticketThreadID, assignedTo, assignedFrom);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<TicketThreads_Result> GetTicketThreads(int userID)
        {
            try
            {
                return new TicketContext().GetTicketThreads(userID);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<TicketThreads_Result> GetTicketThreadsClosed(int userID)
        {
            try
            {
                return new TicketContext().GetTicketThreadsClosed(userID);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }           
        }
        
        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetTickets(int ticketThreadID)
        {
            try
            {
                return new TicketContext().GetTickets(ticketThreadID);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public int CreateCase(TicketThread ticketThread, TicketsAttachment ticketsAttachment)
        {
            try
            {
                return new TicketContext().CreateCase(ticketThread, ticketsAttachment);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }
         /// <summary>
        /// 
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        public int TicketToInsert(int TicketID, int AssignedTo)
        {
            try
            {
                return new TicketContext().TicketToInsert(TicketID, AssignedTo);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }

        
        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public Tuple<bool, int> InsertTicket(TicketThread ticketThread)
        {
            try
            {
                Tuple<bool, int> ret = new TicketContext().InsertTicket(ticketThread);
                return ret;
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetCountUnreadTickets(int userID)
        {
            try
            {
                return new TicketContext().GetUnreadTickets(userID);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool MarkTicketsAsRead(int ticketThreadID, int userID)
        {
            try
            {
                return new TicketContext().MarkTicketsAsRead(ticketThreadID, userID);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetTicketAssignedTo(int userID)
        {
            try
            {
                return new TicketContext().GetTicketAssignedTo(userID);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetUnReadTickets(int userID, bool hasRead)
        {
            try
            {
                return new TicketContext().GetUnReadTickets(userID, hasRead);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Tickets_Result> GetTicketsByUserID(int userID, bool hasRead)
        {
            try
            {
                return new TicketContext().GetUnReadTickets(userID, hasRead);
            }
            catch (Exception ex)
            {
                throw TicketUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool TicketThreadMassUpdate(string TicketThreadIDs, int Updatedby)
        {
            try
            {
                return new TicketContext().TicketThreadMassUpdate(TicketThreadIDs, Updatedby);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        #endregion 

    }
}
