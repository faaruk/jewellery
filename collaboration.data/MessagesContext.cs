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
    public class MessageContext
    {
        #region Messages
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<MessageThreads_Result> GetMessageThreads(int userID, int orderID)
        {            
            using (var context = new CollaborationDBContext())
            {
                var messageThreads = context.GetMessageThreads(userID, orderID);
                return messageThreads.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Messages_Result> GetMessages(int messageThreadID, int orderID, int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var messages = context.GetMessages(messageThreadID);
                return messages.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool CreateCase(MessageThread messageThread)
        {
            using (var context = new CollaborationDBContext())
            {
                Message message = messageThread.Messages.SingleOrDefault();
                MessageTo messageTo = message.MessageToes.SingleOrDefault();
                MessagesAttachment messagesAttachment = message.MessagesAttachments.SingleOrDefault();

                var messages = context.InsertMessage(message.MessageThreadID, message.SentFrom, messageThread.AssignedTo, messageThread.OrderID, message.Subject, message.MessageText, message.IsActive, messageThread.Status, messagesAttachment.LocationURL, messagesAttachment.ContentType, message.HasAttachment);
                return true;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool InsertMessage(MessageThread messageThread)
        {
            using (var context = new CollaborationDBContext())
            {
                Message message = messageThread.Messages.SingleOrDefault();
               // MessageTo messageTo = message.MessageToes.SingleOrDefault();
                MessagesAttachment messagesAttachment = message.MessagesAttachments.SingleOrDefault();

                var messages = context.InsertMessage(messageThread.MessageThreadID, message.SentFrom, messageThread.AssignedTo, messageThread.OrderID, message.Subject, message.MessageText, message.IsActive, messageThread.Status, messagesAttachment.LocationURL, messagesAttachment.ContentType, message.HasAttachment);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetUnreadMessages(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                int countUnreadMessages = context.ExecuteStoreQuery<int>("SELECT [dbo].[ufnCountUnreadMessages](@UserID)", new SqlParameter { ParameterName = "UserID", Value = userID }).FirstOrDefault();
                return countUnreadMessages;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAssignedMessages(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                int countUnreadMessages = context.ExecuteStoreQuery<int>("SELECT [dbo].[ufnCountAssignedMessages](@UserID)", new SqlParameter { ParameterName = "UserID", Value = userID }).FirstOrDefault();
                return countUnreadMessages;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool MarkMessagesAsRead(int messageThreadID,int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var isRead = context.MarkMessageAsRead(messageThreadID, userID,true);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Messages_Result> GetMessageAssignedTo(int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var messages = context.GetMessagesByAssignment(userID);
                return messages.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<Messages_Result> GetUnReadMessages(int userID, bool hasRead)
        {
            using (var context = new CollaborationDBContext())
            {
                var messages = context.GetUnReadMessages(userID, hasRead);
                return messages.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public int MessageOrder(int orderID)
        {
            using (var context = new CollaborationDBContext())
            {
                var modelType = (context.MessageOrder_Select(orderID));
                var aidd = (dynamic)modelType;
                var aid = (int)modelType.ToList().SingleOrDefault();
                return aid;
            }
        }
        #endregion Messages
    }
}
