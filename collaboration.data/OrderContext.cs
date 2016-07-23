using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Business.Entities;
using System.Data;
using System.Data.Objects;
namespace Collaboration.Data
{
    public class OrderContext
    {
        #region Orders
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<OrderParticipants_Result> GetOrderParticipants(int OrderID)
        {
            using (var context = new CollaborationDBContext())
            {
                var modelType = context.GetOrderparticipants(OrderID);
                return modelType.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CreateOrder(Order order, ref int orderID)
        {           
            //Specimen specimen = order.Specimens.SingleOrDefault();
            bool result =false;
            int orId = 0;
            using (var context = new CollaborationDBContext())
            {
                 //var iReturnValue = context.CreateOrder(order.CustomerID, order.ModelTypeID, order.ModelSubTypeID, order.ModelNumber, order.ProcessTypeID,
                 //   order.MetalID, order.MetalOther, order.FingerSizeID,order.FingerSizeOther, order.Quantity,order.QuantityOther, order.Length, order.LengthMeasurement, order.PriorityID,order.MakeExactCopies, order.RingTypeID, order.IsExistingModel, order.ModelToMatch, order.CurveType, order.TailoredType,
                 //   order.IsFinishAtSomePoint, order.AdditionalInfo, order.IsPF, order.HeadSize, order.IsCADRequested,order.IsSampleProvided, order.NoOfSamples, order.MakeExactCopiesSample,order.IsStoneProvided,order.StoneDescription,order.SettingInstructions,
                 //   order.Remarks, order.AssignedTo, order.TMUserID, order.OrderStatusID, order.UserID, order.SpecimenData).SingleOrDefault();
                //var iReturnValue = context.CreateOrderNew(order.CustomerID, order.ExpectedShippingDate, order.ModelTypeID, order.ModelSubTypeID, order.ModelNumber,order.SerialNumber, order.ProcessTypeID,

                var iReturnValue = context.CreateOrderNew(order.CustomerID, order.ExpectedShippingDate, order.ModelTypeID, order.ModelSubTypeID, order.ModelNumber, order.ProcessTypeID,
                    order.MetalID, order.MetalOther, order.FingerSizeID, order.FingerSizeOther, order.Quantity, order.QuantityOther, order.Length, order.LengthMeasurement, order.PriorityID, order.MakeExactCopies, order.RingTypeID, order.IsExistingModel, order.ModelToMatch, order.CurveType, order.TailoredType,
                    order.IsFinishAtSomePoint, order.AdditionalInfo, order.IsPF, order.HeadSize, order.IsCADRequested, order.IsSampleProvided, order.NoOfSamples, order.MakeExactCopiesSample, order.IsStoneProvided, order.StoneDescription, order.SettingInstructions,
                    order.Remarks, order.AssignedTo, order.TMUserID, order.OrderStatusID, order.UserID, order.SpecimenData).SingleOrDefault();

                 try
                 {
                     orId = Convert.ToInt32(iReturnValue);
                 }
                 catch (Exception ex)
                 {
                     
                 }

                 if (iReturnValue.HasValue)
                 {
                     result = (Convert.ToInt32(orId) != 0);
                     orderID = Convert.ToInt32(iReturnValue.Value);
                 }
                return result;
            }
        }
        /////// <summary>
        /////// 
        /////// </summary>
        /////// <returns></returns>
        ////public String GetAndSerialNumber()
        ////{           
        ////    using (var context = new CollaborationDBContext())
        ////    {
        ////        var orderSerialNumber = context.GetAndSetSerialNumber().SingleOrDefault();
        ////        return orderSerialNumber;
        ////    }
        ////}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignedTo"></param>
        /// <param name="status"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Orders_Result> GetOrders(int assignedTo,int status,int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var orders = context.GetOrders(assignedTo, status, userID);
                var lst = orders.ToList<Orders_Result>();
                return lst;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignedTo"></param>
        /// <param name="status"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Orders_Result> GetCancelledOrders(int assignedTo, int status, int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var orders = context.GetCancelledOrders(assignedTo, status, userID);
                var lst = orders.ToList<Orders_Result>();
                return lst;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderDetails_Result GetOrderDetails(int orderID,int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var orders = context.GetOrderDetails(orderID,userID).FirstOrDefault();
                return orders;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="CADID"></param>
        /// <returns></returns>
        public List<OrdersCAD> GetOrderCADs(int orderID,int CADID)
        {
            using (var context = new CollaborationDBContext())
            {
                var list = context.GetOrderCADs(orderID, CADID);
                return list.ToList();
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
            using (var context = new CollaborationDBContext())
            {
                var list = context.OrderCADTop2_Select(orderID, CADID);
                return list.ToList();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public List<OrderStatu> GetOrderStatuses(int statusID)
        {
            using (var context = new CollaborationDBContext())
            {
                var list = context.GetStatuses(statusID);
                return list.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool ModifyOrder(Order order)
        {
            Specimen specimen = order.Specimens.SingleOrDefault();
            using (var context = ConnectionFactory.GetCollaborationLinqDataContext())
            {
                var result = context.Order_Update(order.OrderID, order.CustomerID, order.ExpectedShippingDate, order.ModelTypeID,order.ModelSubTypeID, order.ModelNumber, order.ProcessTypeID,
                    order.MetalID, order.MetalOther, order.FingerSizeID,order.FingerSizeOther, order.Quantity,order.QuantityOther, order.Length, order.LengthMeasurement, order.PriorityID,order.MakeExactCopies, order.RingTypeID,order.IsExistingModel, order.ModelToMatch, order.CurveType, order.TailoredType,
                    order.IsFinishAtSomePoint, order.AdditionalInfo, order.IsPF, order.HeadSize, order.IsCADRequested,
                    order.IsSampleProvided, order.NoOfSamples, order.MakeExactCopiesSample, order.IsStoneProvided, order.StoneDescription, order.SettingInstructions, order.Remarks, order.TMUserID, order.OrderStatusID, order.AssignedTo);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool ModifyOrderDetails(Order order)
        {
            OrdersCAD orderCAD = order.OrdersCADs.SingleOrDefault();
            using (var context = new CollaborationDBContext())
            {
                var result = context.ModifyOrderDetails(order.OrderID, order.UpdateCADInfo, order.UpdateSampleInfo, orderCAD.CADID, orderCAD.CADLocationURL, orderCAD.UploadedBy
                , orderCAD.IsApproved, orderCAD.Remarks, orderCAD.ChangeInstructions, order.AssignedTo, order.OrderStatusID);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool ChangeOrderStatus(Order order)
        {
            OrdersCAD orderCAD = order.OrdersCADs.SingleOrDefault();
            using (var context = new CollaborationDBContext())
            {
                var result = context.UpdateOrderStatus(order.OrderID, order.OrderStatusID, orderCAD.ChangeInstructions, order.UpdateCADInfo, orderCAD.CADID, orderCAD.IsApproved, orderCAD.IsUpdatedByCustomer, orderCAD.ChangeInstructionsCustomer);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public DashboardData_Result GetDashboardData(int userID, int deadlineInDays, int assignedToID)
        {            
            using (var context = new CollaborationDBContext())
            {
                var result = context.GetDashboardData(userID, deadlineInDays,assignedToID).SingleOrDefault();
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public List<Sample> GetSamples(int orderID, int sampleID)
        {
            using (var context = new CollaborationDBContext())
            {
                var result = context.GetSamples(orderID, sampleID);
                return result.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public bool UpdateSamples(List<Sample> sampleList)
        {
            using (var context = new CollaborationDBContext())
            {
                foreach (Sample entity in sampleList)
                {
                    var result = context.UpdateSampleInfo(entity.SampleID, entity.SampleSerialNumber, entity.IsActive, entity.IsReturned, entity.IsConfirmed, entity.ConfirmedBy,entity.ReturnedDate);
                }
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public List<Specimen> GetSpecimens(int specimenID,int orderID, int messageID)
        {
            using (var context = new CollaborationDBContext())
            {
                var result = context.GetSpecimens(specimenID,orderID, messageID);
                return result.ToList();
            }
        }
        #endregion Orders
    }
}
