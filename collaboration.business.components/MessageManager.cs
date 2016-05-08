using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Data;
using Collaboration.Business.Entities;
using Collaboration.Business.Components.Utilities;
namespace Collaboration.Business.Components
{
    public class MessageManager
    {
        #region Messages
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<MessageThreads_Result> GetMessageThreads(int userID, int orderID)
        {
            try
            {
                return new MessageContext().GetMessageThreads(userID, orderID);
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
        public List<Messages_Result> GetMessages(int messageThreadID, int orderID, int userID)
        {
            try
            {
                return new MessageContext().GetMessages(messageThreadID, orderID, userID);
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
        public int MessageOrder(int orderID)
        {
            try
            {
                return new MessageContext().MessageOrder(orderID);
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
        public bool CreateCase(MessageThread messageThread)
        {
            try
            {
                return new MessageContext().CreateCase(messageThread);
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
        public bool InsertMessage(MessageThread messageThread)
        {
            try
            {
                return new MessageContext().InsertMessage(messageThread);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetCountUnreadMessages(int userID)
        {
            try
            {
                return new MessageContext().GetUnreadMessages(userID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool MarkMessagesAsRead(int messageThreadID, int userID)
        {
            try
            {
                return new MessageContext().MarkMessagesAsRead(messageThreadID, userID);
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
        public List<Messages_Result> GetMessageAssignedTo(int userID)
        {
            try
            {
                return new MessageContext().GetMessageAssignedTo(userID);
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
        public List<Messages_Result> GetUnReadMessages(int userID, bool hasRead)
        {
            try
            {
                return new MessageContext().GetUnReadMessages(userID,hasRead);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        #endregion Messages
    }
}
