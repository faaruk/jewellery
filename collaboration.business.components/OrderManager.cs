using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Data;
using Collaboration.Business.Entities;
using System.Data;
using Collaboration.Business.Components.Utilities;
using Collaboration.Data;

namespace Collaboration.Business.Components
{
    public class OrderManager
    {
        #region Orders
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderParticipants_Result> GetOrderParticipants(int OrderID)
        {
            try
            {
                return new OrderContext().GetOrderParticipants(OrderID);
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
        public bool CreateOrder(Order order, ref int orderID)
        {            
            try
            {                
                return new OrderContext().CreateOrder(order, ref orderID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public String GetAndSerialNumber()
        //{
        //    try
        //    {
        //        return new OrderContext().GetAndSerialNumber();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw MessageUtility.GetErrorMessage(ex);
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Orders_Result> GetOrders(int assignedTo, int status, int userID)
        {
            try
            {
                return new OrderContext().GetOrders(assignedTo, status, userID);
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
        public List<Orders_Result> GetCancelledOrders(int assignedTo, int status, int userID)
        {
            try
            {
                return new OrderContext().GetCancelledOrders(assignedTo, status, userID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignedTo"></param>
        /// <param name="status"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public OrderDetails_Result GetOrderDetails(int orderID, int userID)
        {
            try
            {
                return new OrderContext().GetOrderDetails(orderID, userID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="CADID"></param>
        /// <returns></returns>
        public List<OrdersCAD> GetOrderCADs(int orderID, int CADID)
        {
            try
            {
                return new OrderContext().GetOrderCADs(orderID, CADID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="CADID"></param>
        /// <returns></returns>
        public List<OrdersCAD> GetOrderCADsTop2(int orderID, int CADID)
        {
            try
            {
                return new OrderContext().GetOrderCADsTop2(orderID, CADID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public List<OrderStatu> GetOrderStatuses(int statusID)
        {
            try
            {
                return new OrderContext().GetOrderStatuses(statusID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool ModifyOrder(Order order)
        {
            try
            {
                return new OrderContext().ModifyOrder(order);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool ModifyOrderDetails(Order order)
        {
            try
            {
                return new OrderContext().ModifyOrderDetails(order);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool ChangeOrderStatus(Order order)
        {
            try
            {
                return new OrderContext().ChangeOrderStatus(order);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public DashboardData_Result GetDashboardData(int userID, int deadlineInDays, int assignedToID)
        {
            try
            {
                return new OrderContext().GetDashboardData(userID, deadlineInDays, assignedToID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public List<Sample> GetSamples(int orderID, int sampleID)
        {
            try
            {
                return new OrderContext().GetSamples(orderID, sampleID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool UpdateSamples(List<Sample> sampleList)
        {
            try
            {
                return new OrderContext().UpdateSamples(sampleList);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public List<Specimen> GetSpecimens(int specimenID, int orderID, int messageID)
        {
            try
            {
                return new OrderContext().GetSpecimens(specimenID, orderID, messageID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        public void DeleteOrder(int? OrderID)
        {
            try
            {
                using (var db = ConnectionFactory.GetCollaborationLinqDataContext())
                {
                    db.Order_Delete(OrderID);
                }
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        #endregion Orders
    }
}
