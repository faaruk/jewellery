using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using System.IO;
using Collaboration.Web.UI.Utilities;
using System.ComponentModel;
using DevExpress.Web;
namespace Collaboration.Web.UI.Orders
{
    public partial class ViewOrder : System.Web.UI.Page
    {
        static int _userID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int OrderStatusID { set { } get { return Convert.ToInt32(Request.QueryString[Common.EntityAttributes.ORDERSTATUSID]); } }
        public int OrderFilterID { set { } get { return Convert.ToInt32(Request.QueryString[Common.REQUEST_FILTERID]); } }

        //Get Role Id of logged in user
        public int RoleID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }

        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!IsPostBack)
            BindGrid();

            OrdersDataSource.SelectParameters["UserID"].DefaultValue = _userID.ToString();
            (gvTable.Columns["ExpectedShippingDate"] as GridViewDataDateColumn).PropertiesEdit.DisplayFormatString = Resource.FormatDate;
        }


        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            try
            {
                IEnumerable<Orders_Result> orders_Result = new OrderManager().GetOrders(0, 0, UserID);

                Session[Collaboration.Web.UI.Common.SESSION_ORDERS] = orders_Result;

                if (Request.QueryString[Common.EntityAttributes.ORDERSTATUSID] != null)
                {
                    // Check if logged in user is Admin than display all the new orders
                    if (RoleID == Convert.ToInt32(Resource.DB_AdminRoleID))
                    {
                        gvTable.DataSource = orders_Result.Where(x => x.OrderStatusID == OrderStatusID);
                    }
                    else if (RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                    {
                        gvTable.DataSource = orders_Result.Where(x => x.OrderStatusID == OrderStatusID && (x.CreatedBy == UserID || x.ParticipantsUserID== UserID || x.AssignedTo == UserID));
                    }
                    else // else only display assined to order of logged in user
                    {
                        gvTable.DataSource = orders_Result.Where(x => x.OrderStatusID == OrderStatusID && (x.CreatedBy == UserID || x.ParticipantsUserID == UserID || x.AssignedTo == UserID));
                    }

                }
                else if (Request.QueryString[Common.REQUEST_FILTERID] != null)
                {
                    Double deadlineInDays = Convert.ToDouble(Resource.Val_ApproachingDeadline);
                    IEnumerable<Orders_Result> orders_ResultDup;
                    IEnumerable<Orders_Result> orders_ResultDup2;
                    
                    
                    if (OrderFilterID == Convert.ToInt32(Common.FilterType.Delayed))
                        gvTable.DataSource =
                            orders_Result.Where(
                                x =>
                                    Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) < DateTime.Today &&
                                    x.OrderStatusID < Convert.ToInt32(Resource.DB_Status_Delayed) && x.ParticipantsUserID == UserID);
                    else if (OrderFilterID == Convert.ToInt32(Common.FilterType.SampleNotReturned))
                        gvTable.DataSource =
                            orders_Result.Where(x => x.IsSampleProvided == true && x.CountSampleNotReturned > 0 && x.ParticipantsUserID == UserID);
                    else if (OrderFilterID == Convert.ToInt32(Common.FilterType.ApproachingDeadline))
                    {
                        orders_ResultDup = orders_Result.Where(
                                x =>
                                    Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) < DateTime.Today &&
                                    x.OrderStatusID < Convert.ToInt32(Resource.DB_Status_Delayed) && x.ParticipantsUserID == UserID);
                        orders_ResultDup2 = orders_Result.Where(
                                x =>
                                    Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) <
                                    DateTime.Today.AddDays(deadlineInDays) &&
                                    x.OrderStatusID < Convert.ToInt32(Resource.DB_Status_Delayed) && x.ParticipantsUserID == UserID);
                        orders_ResultDup = orders_ResultDup2.Where(m => !orders_ResultDup.Contains(m)).ToList();
                        gvTable.DataSource = orders_ResultDup.ToList();
                    }
                    else if (OrderFilterID == Convert.ToInt32(Common.FilterType.TMNotAssigned))
                        gvTable.DataSource = orders_Result.Where(x => x.TMUserID == null);
                    else if (OrderFilterID == Convert.ToInt32(Common.FilterType.AssignedTo))
                        gvTable.DataSource = orders_Result.Where(x => x.AssignedTo == UserID);
                    else if (OrderFilterID == Convert.ToInt32(Common.FilterType.ApproachingDeadlineCustomize))
                    {
                        if (Request.QueryString[Common.REQUEST_PRIORITY] != null &&
                            Request.QueryString[Common.REQUEST_DAYS] != null)
                        {
                            //gvTable.DataSource =
                            //    orders_Result.Where(
                            //        x =>
                            //            Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) <
                            //            DateTime.Today.AddDays(Convert.ToInt32(Request.QueryString[Common.REQUEST_DAYS]))
                            //            && x.OrderStatusID != Convert.ToInt32(Resource.DB_Status_Shipped)
                            //            && x.PriorityID == Convert.ToInt32(Request.QueryString[Common.REQUEST_PRIORITY]));

                            orders_ResultDup = orders_Result.Where(
                               x =>
                                   Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) < DateTime.Today &&
                                   x.OrderStatusID < Convert.ToInt32(Resource.DB_Status_Delayed) && x.ParticipantsUserID == UserID);
                            orders_ResultDup2 = orders_Result.Where(
                                    x =>
                                        Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) <
                                        DateTime.Today.AddDays(Convert.ToInt32(Request.QueryString[Common.REQUEST_DAYS])) &&
                                        x.OrderStatusID < Convert.ToInt32(Resource.DB_Status_Delayed) && x.ParticipantsUserID == UserID);
                            orders_ResultDup = orders_ResultDup2.Where(m => !orders_ResultDup.Contains(m)).ToList();
                            gvTable.DataSource = orders_ResultDup.Where(x => x.PriorityID == Convert.ToInt32(Request.QueryString[Common.REQUEST_PRIORITY])).ToList();
                        }
                    }

                    else if (OrderFilterID == Convert.ToInt32(Common.FilterType.DelayedShipping))
                        gvTable.DataSource =
                            orders_Result.Where(
                                x =>
                                    //Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) < DateTime.Today &&
                                    Convert.ToDateTime(x.ExpectedShippingDate) < DateTime.Today &&
                                    x.OrderStatusID != Convert.ToInt32(Resource.DB_Status_Shipped) && x.ParticipantsUserID == UserID);
                    else if (OrderFilterID == Convert.ToInt32(Common.FilterType.ApproachingShippingDeadline))
                    {
                        orders_ResultDup = orders_Result.Where(
                                x =>
                                    Convert.ToDateTime(x.ExpectedShippingDate) < DateTime.Today &&
                                    x.OrderStatusID != Convert.ToInt32(Resource.DB_Status_Shipped) && x.ParticipantsUserID == UserID);
                        orders_ResultDup2 = orders_Result.Where(
                                x =>
                                    //Convert.ToDateTime(x.CreateDate).AddDays(Convert.ToDouble(x.Frequency)) <
                                    Convert.ToDateTime(x.ExpectedShippingDate) <
                                    DateTime.Today.AddDays(deadlineInDays) &&
                                    x.OrderStatusID != Convert.ToInt32(Resource.DB_Status_Shipped) && x.ParticipantsUserID == UserID);
                        orders_ResultDup = orders_ResultDup2.Where(m => !orders_ResultDup.Contains(m)).ToList();
                        gvTable.DataSource = orders_ResultDup.ToList();
                    }
                }
                else
                {
                    gvTable.DataSource = orders_Result;//.Where(x => x.OrderStatusID != Convert.ToInt32(Resource.DB_Status_Shipped));
                }


                gvTable.DataBind();
            }
            catch (Exception ex)
            { }
        }

    }
}