using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using System.IO;
using Collaboration.Web.UI.Account;
using Collaboration.Web.UI.Utilities;
using System.Configuration;


namespace Collaboration.Web.UI.Orders
{
    public partial class ViewOrderDetails : BasePage
    {
        private static int _userID = 0;
        private static int _roleID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }
        static OrderDetails_Result order;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var Action = Request.Form["ac"];
            if (!string.IsNullOrEmpty(Action))
            {
                RemoveImage();
            }

            //if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            //{
            //    // partial (asynchronous) postback occured
            //    // insert Ajax custom logic here
            //}
            if (!IsPostBack)
                SetValues();
        }

        private void RemoveImage()
        {
            try
            {
                Response.Clear();
                var fileName = Request.Form["fn"];

                if (Session[Common.SESSION_SPECIMENIMAGES] != null)
                {
                    //Session[Common.SESSION_SPECIMENIMAGES] =
                    //    (Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>).Where(i => i.ImageFile != fileName)
                    //        .ToList();
                    Session[Common.SESSION_SPECIMENIMAGES] =
                          (Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>).Where(i => i.ImageLocationURL != fileName)
                          .ToList();
                }
            }
            catch
                (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        private void GetOrderDetails(int orderID)
        {
            order = new OrderManager().GetOrderDetails(orderID, UserID);
            //int OrderStatusID = order.OrderStatusID;
            //if (OrderStatusID == 9)
            //{
            //    btnSave.Visible = false;
            //}
            //else
            //{
            //    btnSave.Visible = true;
            //}
            if (order == null)
                Response.Redirect("~/AccessDenied.html");
        }
        protected void btnCancelDetails_Click(object sender, EventArgs e)
        {
            int MessageThreadID = new MessageManager().MessageOrder(order.OrderID);
            bool markAsRead = new MessageManager().MarkMessagesAsRead(MessageThreadID, UserID);
            if (markAsRead)
            {
                Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES] = new MessageManager().GetCountUnreadMessages(UserID);
                Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] = null;
                Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowPopup_Click(object sender, EventArgs e)
        {
            ShowViewDetailsDialog(order.OrderID);
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowViewDetailsDialog(int OrderID)
        {
            //pnl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
            int MessageThreadID = new MessageManager().MessageOrder(order.OrderID);
            mpViewDetails.Show();
            ViewMessages.MessageThreadID = MessageThreadID;
            ViewMessages.OrderID = OrderID;
            ViewMessages.FillInfo();
            updViewDetails.Update();

        }
        /// <summary>
        /// 
        /// </summary>
        public void SetValues()
        {
            try
            {
                int orderID = Convert.ToInt32(Request.QueryString[Common.EntityAttributes.ORDERID]);
                GetOrderDetails(orderID);
                if (RoleID == Convert.ToInt32(Resource.DB_AdminRoleID))
                {
                    EditOrder.Visible = true;
                    EditOrder.FillInfo(false);
                    EditOrder.SetValues(order);
                    ViewOrderDetail.Visible = false;
                    divHeading.InnerHtml = "Update Order";
                }
                else
                {
                    EditOrder.Visible = false;
                    ViewOrderDetail.Visible = true;
                    ViewOrderDetail.SetValues(order);
                    divHeading.InnerHtml = "Order Details";
                }

                SetDynamicSection(order);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetDynamicSection(OrderDetails_Result order)
        {
            bool isTMAssigned = order.TMUserID == null ? false : true;
            btnSave.Visible = true;

            if ((order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Initiated) && !isTMAssigned && RoleID != Convert.ToInt32(Resource.DB_AdminRoleID)) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Initiated) && isTMAssigned && RoleID == Convert.ToInt32(Resource.DB_TMRoleID)) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADInProgress) && RoleID == Convert.ToInt32(Resource.DB_TMRoleID)) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) && RoleID == Convert.ToInt32(Resource.DB_TMRoleID)) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingTMReview) && RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID)) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingCustomerConfirmation) && (RoleID != Convert.ToInt32(Resource.DB_AdminRoleID) && RoleID != Convert.ToInt32(Resource.DB_TMRoleID))) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADConfirmed) && RoleID == Convert.ToInt32(Resource.DB_TMRoleID)) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PrototypingBegins) && RoleID == Convert.ToInt32(Resource.DB_TMRoleID)) ||
               (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Shipped) && RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID)) ||
                order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Shipped) && RoleID == Convert.ToInt32(Resource.DB_TMRoleID) && (Convert.ToBoolean(!order.IsSampleProvided)) ||
                (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Shipped) && RoleID == Convert.ToInt32(Resource.DB_AdminRoleID)))

                btnSave.Visible = false;

            DynamicOrderDetails.SetDynamicSection(order, UserID, RoleID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //DynamicOrderDetails.uploadCad();
            if (RoleID != Convert.ToInt32(Resource.DB_AdminRoleID) && !DynamicOrderDetails.IsValidFile)
                //if (RoleID != Convert.ToInt32(Resource.DB_AdminRoleID))
                if (order.OrderStatusID != 1)
                {
                    if (RoleID != Convert.ToInt32(Resource.DB_AdminRoleID))
                    {
                        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.ErrUploadCAD);
                        UpdatePanel1.Update();
                        btnSave.CssClass = Resource.UI_BtnDanger;
                        return;
                    }
                }
            if (!EditOrder.IsValidFile)
            {
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
                UpdatePanel1.Update();
                btnSave.CssClass = Resource.UI_BtnDanger;
                return;
            }
            Order modifiedOrder = new Order();
            Order newOrder = DynamicOrderDetails.SaveInfo(order, UserID, RoleID);
            if (Session["AttachmentLocation"] != null)
            {
                Session["AttachmentLocation"] = null;
            }

            if (RoleID == Convert.ToInt32(Resource.DB_AdminRoleID))
            {
                modifiedOrder = EditOrder.PlaceCADOrder();
                //  modifiedOrder.UpdateSampleInfo = newOrder.IsSampleProvided;
                // modifiedOrder.Sample = new Sample();
                if ((order.IsSampleProvided == null || !Convert.ToBoolean(order.IsSampleProvided)) && Convert.ToBoolean(modifiedOrder.IsSampleProvided))
                {
                    modifiedOrder.SampleSerialNumber = modifiedOrder.SampleSerialNumber;
                    modifiedOrder.UpdateSampleInfo = true;
                }
                else
                {
                    //if (order.SampleID.HasValue)
                    //    modifiedOrder.Sample.SampleID = Convert.ToInt32(order.SampleID);
                    //modifiedOrder.Sample.IsReturned = newOrder.Sample.IsReturned;
                    //modifiedOrder.Sample.ReturnedDate = newOrder.Sample.ReturnedDate;
                    //modifiedOrder.Sample.IsConfirmed = newOrder.Sample.IsConfirmed;
                    //modifiedOrder.Sample.ConfirmedBy = newOrder.Sample.ConfirmedBy;
                }
                modifiedOrder.OrderID = newOrder.OrderID;
                modifiedOrder.OrderStatusID = order.OrderStatusID;
            }

            try
            {
                if (RoleID != Convert.ToInt32(Resource.DB_AdminRoleID))
                {
                    if (new OrderManager().ModifyOrderDetails(newOrder))
                    {
                        string errorMessage = Resource.Err_General;
                        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_CADUpdated);
                        UpdatePanel1.Update();
                        btnSave.CssClass = Resource.UI_BtnSuccess;
                        btnSave.Visible = false;
                        GetOrderDetails(newOrder.OrderID);
                        SetDynamicSection(order);
                        if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingCustomerConfirmation))
                        {
                            OrdersCAD orderCAD = newOrder.OrdersCADs.SingleOrDefault(x => x.IsApproved == null);
                            if (!MailUtilityNew.SendCADConfirmationMail(order.CustomerEmail, order.TMEmail, Convert.ToString(Common.MailType.CADConfirmation), order.CustomerName, order.OrderID, orderCAD.CADID, out errorMessage))
                            {
                                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                                UpdatePanel1.Update();
                            }
                            //IEnumerable<OrderParticipants_Result> users = new OrderManager().GetOrderParticipants(order.OrderID);
                            //string toList = users.Select(s => s.EMail).Aggregate((current, next) => current + ", " + next);
                            //if (!MailUtility.SendCADResposeMail(order.TMEmail, "", Convert.ToString(Common.MailType.CADResponse), order.TMUserName, order.SerialNumber, "Customer Review", out errorMessage))
                            //    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                        }
                        var CADCount = new OrderManager().GetOrderCADs(order.OrderID, 0).Count(x => x.IsApproved == false);

                        if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) && CADCount >= 3)
                        {
                            var AdminEmails = string.Join(",", new AccountManager().GetUsers().Where(x => x.RoleID == 1 && !string.IsNullOrEmpty(x.EMail)).Select(s => s.EMail));
                            // var AdminEmails = "tvaladze@gmail.com, mike@63bits.com, david@63bits.com";
                            if (!MailUtility.SendCADThreeRequestsIsueMail(AdminEmails, "", Convert.ToString(Common.MailType.CADThreeRequestsIsue), order.SerialNumber, out errorMessage))
                            {
                                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                                UpdatePanel1.Update();
                            }
                        }


                        Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] = null;
                        if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingCustomerConfirmation))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CallResetScrollPosition", "SaveMessage();", true);
                            //var script ="Sys.WebForms.PageRequestManager.getInstance()._scrollPosition = null; " +
                              //  "window.scrollTo(0, 0);";
                            //ScriptManager.RegisterStartupScript(this, GetType(), "key", script, true);
                        }
                        //(this.Master as DasbhoardMaster).UpdateOrderdAssigned();
                    }
                }
                else if (new OrderManager().ModifyOrder(modifiedOrder))
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_CADUpdated);
                    UpdatePanel1.Update();
                    btnSave.CssClass = Resource.UI_BtnSuccess;
                    btnSave.Visible = false;
                    GetOrderDetails(newOrder.OrderID);
                    MoveFiles(order.OrderID.ToString(), Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>);
                    EditOrder.SetValues(order);
                    SetDynamicSection(order);
                    Session[Collaboration.Web.UI.Common.SESSION_ORDERASSIGNED] = null;
                    //(this.Master as DasbhoardMaster).UpdateOrderdAssigned();
                }
                else
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                    UpdatePanel1.Update();
                    btnSave.CssClass = Resource.UI_BtnDanger;
                }
            }
            catch (BLLException exception)
            {
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
                UpdatePanel1.Update();
                ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
                btnSave.CssClass = Resource.UI_BtnDanger;
            }
            catch (Exception exception)
            {
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                UpdatePanel1.Update();
                ExceptionUtility.LogException(exception, "Sender: " + Request.RawUrl);
                btnSave.CssClass = Resource.UI_BtnDanger;
            }
            if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADInProgress))
            {
                Response.Redirect(Request.RawUrl);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlaceNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Orders/CreateOrder.aspx");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            EditOrder.ResetValues();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <param name="lst"></param>
        private void MoveFiles(string orderID, List<Specimen> lst)
        {
            if (lst != null && lst.Count() > 0)
            {
                foreach (Specimen item in lst)
                {
                    string sourceFile = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\Temp\" + item.ImageLocationURL;

                    if (System.IO.File.Exists(Server.MapPath(sourceFile)))
                    {
                        string destinationDir = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\" + orderID;

                        if (!System.IO.Directory.Exists(Server.MapPath(destinationDir)))
                            System.IO.Directory.CreateDirectory(Server.MapPath(destinationDir));

                        System.IO.File.Move(Server.MapPath(sourceFile), Server.MapPath(destinationDir + @"\" + item.ImageLocationURL));
                    }
                }
            }
            Session[Common.SESSION_SPECIMENIMAGES] = null;
        }
    }
}