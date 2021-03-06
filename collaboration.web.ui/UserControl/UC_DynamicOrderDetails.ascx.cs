﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using System.IO;
using Collaboration.Web.UI.Utilities;
using System.Data;
using System.Configuration;
using AjaxControlToolkit;

namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_DynamicOrderDetails : System.Web.UI.UserControl
    {
        private static int specimenFilesUploaded = 0;
        private static string message = string.Empty;
        private static bool _isValidFile = true;
        public bool IsValidFile { set { _isValidFile = value; } get { return _isValidFile; } }
        public static byte[] Image = null;
        public static bool loadImage = true;
        private static List<OrdersCAD> orderCADs = new List<OrdersCAD>();
        private static OrderDetails_Result _orderDetails;
        private static int _userID = 0;
        private static int _roleID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }
        static String AttachmentLocation = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session[Common.SESSION_CADS] = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void HideControls()
        {
            divAssignee.Visible = divStatus.Visible = divUploadedCADs.Visible = divUploadCAD.Visible = divNote.Visible = divCustomerEmail.Visible =
                divCRInstructions.Visible = divSample.Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCAD_Click(object sender, EventArgs e)
        {
            imgCAD1.Style.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
            imgCAD1.ImageUrl = AttachmentLocation;
            updDynamicDetails.Update();
        }
        //protected void btnCancelCADCompare_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(Request.RawUrl);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewSamples_Click(object sender, EventArgs e)
        {
            BindSamples();
            if (_orderDetails.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ProductionInProgress))
            {
                if ((Session[Common.SESSION_SAMPLES] as List<Sample>).Where(x => x.IsReturned == null || x.IsReturned == false).Count() == 0)
                    btnSave.Visible = false;
            }
            if (_orderDetails.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Shipped))
            {
                if ((Session[Common.SESSION_SAMPLES] as List<Sample>).Where(x => x.IsConfirmed == null || x.IsConfirmed == false).Count() == 0)
                    btnSave.Visible = false;
            }
            MessageUtility.ClearMessages(dvMessageSample, ltMessageSample);
            mpSamples.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        public void BindSamples()
        {
            Session[Common.SESSION_SAMPLES] = new OrderManager().GetSamples(_orderDetails.OrderID, 0);
            gvTableSamples.DataSource = Session[Common.SESSION_SAMPLES] as List<Sample>;
            gvTableSamples.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderCADs"></param>
        private void BindUploadedCAD(int orderID)
        {
            orderCADs = new OrderManager().GetOrderCADs(orderID, 0);
            Session[Common.SESSION_CADS] = orderCADs;
            if (orderCADs != null && orderCADs.Count > 0)
            {
                
                divUploadedCADs.Visible = true;
                gvTable.DataSource = orderCADs;
                gvTable.DataBind();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderCADs"></param>
        private void BindUploadedCAD()
        {
            List<OrdersCAD> orderCADs = Session[Common.SESSION_CADS] as List<OrdersCAD>;
            if (orderCADs != null && orderCADs.Count > 0)
            {
                divUploadedCADs.Visible = true;
                gvTable.DataSource = orderCADs;
                gvTable.DataBind();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void SetDynamicSection(OrderDetails_Result order, int userID, int roleID)
        {
            txtRole.Value = RoleID.ToString();
            _orderDetails = order;
            HideControls();
            lblStatus.Text = "[ " + order.Status + " ]";
            if (roleID == Convert.ToInt32(Resource.DB_AdminRoleID))
                divAssignee.Visible = false;

            bool isTMAssigned = false;
            isTMAssigned = order.TMUserID != null;
            List<OrderStatu> orderStatus = new OrderManager().GetOrderStatuses(0);
            IEnumerable<OrderParticipants_Result> users = new OrderManager().GetOrderParticipants(order.OrderID);


            if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Initiated) && !isTMAssigned)
            {

            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Initiated) && isTMAssigned || order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest))
            {
                if (roleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                    divStatus.Visible = true;
                FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_CADInProgress)).ToArray(), "", false);
                BindUploadedCAD(order.OrderID);
            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADInProgress))
            {
                //IsValidFile = false; Oct 23, 2015
                if (roleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                {
                    divStatus.Visible = true;
                    divAssignee.Visible = true;
                    divUploadCAD.Visible = true;
                }
                FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_PendingTMReview)).ToArray(), "", false);
                FillDropDown(ddlAssignTo, users.Where(x => x.RoleID == Convert.ToInt32(Resource.DB_TMRoleID)).ToArray(), "", false);
                BindUploadedCAD(order.OrderID);
            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingTMReview))
            {
                if (UserID == order.TMUserID)
                {
                    if (!Convert.ToBoolean(order.IsCADRequested))
                    {
                        
                        divNote.Visible = true;
                        FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) || x.StatusID == Convert.ToInt32(Resource.DB_Status_CADConfirmed)).ToArray());
                    }
                    else
                    {
                        OrdersCAD CAD = new OrderManager().GetOrderCADs(order.OrderID, 0).Take(1).OrderByDescending(x => x.CADID).SingleOrDefault();
                        if (CAD.IsUpdatedByCustomer != true)
                        {
                            FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) || x.StatusID == Convert.ToInt32(Resource.DB_Status_PendingCustomerConfirmation)).ToArray());
                        }
                        else {
                            FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) || x.StatusID == Convert.ToInt32(Resource.DB_Status_CADConfirmed)).ToArray());
                        }

                        
                        if (String.IsNullOrEmpty(order.CustomerEmail))
                        {
                            divCustomerEmail.Visible = true;
                            ddlStatus.Items.FindByValue(Resource.DB_Status_PendingCustomerConfirmation).Attributes.Add("disabled", "true");
                        }
                    }
                    divCRInstructions.Visible = true;
                    // divCompareCAD.Visible = true;
                    divStatus.Visible = true;
                    divAssignee.Visible = true;
                    FillDropDown(ddlAssignTo, users.Where(x => x.RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID)).ToArray());
                }
                BindUploadedCAD(order.OrderID);
            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest))
            {

            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingCustomerConfirmation))
            {
                //BindUploadedCAD(order.OrderID);
                if (UserID == order.TMUserID)
                {
                    if (!Convert.ToBoolean(order.IsCADRequested))
                    {

                        divNote.Visible = true;
                        FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) || x.StatusID == Convert.ToInt32(Resource.DB_Status_CADConfirmed)).ToArray());
                    }
                    else
                    {
                        OrdersCAD CAD = new OrderManager().GetOrderCADs(order.OrderID, 0).Take(1).OrderByDescending(x => x.CADID).SingleOrDefault();
                        FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) || x.StatusID == Convert.ToInt32(Resource.DB_Status_CADConfirmed)).ToArray());
                        if (String.IsNullOrEmpty(order.CustomerEmail))
                        {
                            divCustomerEmail.Visible = true;
                            ddlStatus.Items.FindByValue(Resource.DB_Status_PendingCustomerConfirmation).Attributes.Add("disabled", "true");
                        }
                    }
                    divCRInstructions.Visible = true;
                    // divCompareCAD.Visible = true;
                    divStatus.Visible = true;
                    divAssignee.Visible = true;

                    //ddlAssignTo.ClearSelection();
                    ddlAssignTo.Items.Clear();
                    //ddlAssignTo.Items.Insert(0, new ListItem(Common.DROPDOWN_SELECT_TEXT, Common.DROPDOWN_SELECT_VALUE));
                    //ddlAssignTo.SelectedIndex = -1;
                    //ddlAssignTo.AppendDataBoundItems = false;
                    //updDynamicDetails.Update();
                    //ddlAssignTo.DataSource = null;
                    //ddlAssignTo.DataBind();
                    //ddlAssignTo.SelectedValue = null;
                    ddlAssignTo.Items.Insert(0, new ListItem(Common.DROPDOWN_SELECT_TEXT, Common.DROPDOWN_SELECT_VALUE));
                    OrderParticipants_Result users2 = users.Where(x => x.RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID)).SingleOrDefault();
                    ddlAssignTo.Items.Insert(1, new ListItem(users2.UserName, users2.UserID.ToString()));
                    
                    //FillDropDown(ddlAssignTo, users.Where(x => x.RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID)).ToArray());
                }
                BindUploadedCAD(order.OrderID);
            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADConfirmed))
            {
                BindUploadedCAD(order.OrderID);
                if (roleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                    divStatus.Visible = true;
                FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_PrototypingBegins)).ToArray(), "", false);
            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PrototypingBegins))
            {
                BindUploadedCAD(order.OrderID);
                if (roleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                    divStatus.Visible = true;
                FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_ProductionInProgress)).ToArray(), "", false);
            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ProductionInProgress))
            {
                BindUploadedCAD(order.OrderID);
                if (roleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                {
                    divStatus.Visible = true;
                    FillDropDown(ddlStatus, orderStatus.Where(x => x.StatusID == Convert.ToInt32(Resource.DB_Status_Shipped)).ToArray(), "", false);
                    //if (Convert.ToBoolean(order.IsSampleProvided))
                    //    divSample.Visible = true;  //Commented it out on Oct 23, 2015 as Shashank asked

                    divAssignee.Visible = true;
                    FillDropDown(ddlAssignTo, users.Where(x => x.RoleID == Convert.ToInt32(Resource.DB_TMRoleID)).ToArray(), "", false);
                }
            }
            else if (order.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Shipped))
            {
                BindUploadedCAD(order.OrderID);
                //if (Convert.ToBoolean(order.IsSampleProvided)) 
                //    divSample.Visible = true; //Commented it out on Oct 23, 2015 as Shashank asked
            }
            updDynamicDetails.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlList"></param>
        /// <param name="dataSource"></param>
        public void FillDropDown(DropDownList ddlList, Array dataSource, string optionalstr = Common.DROPDOWN_SELECT_TEXT, bool bindDefaultValue = true)
        {
            ddlList.DataSource = dataSource;
            ddlList.DataBind();
            if (bindDefaultValue)
                ddlList.Items.Insert(0, new ListItem(optionalstr, Common.DROPDOWN_SELECT_VALUE));
        }
        /// <summary>
        /// 
        /// </summary>
        public Order SaveInfo(OrderDetails_Result orderResult, int userID, int roleID)
        {
            Order order = new Order();
            OrdersCAD orderCAD = new OrdersCAD();
            // order.Sample = new Sample();
            //if (orderResult.SampleID.HasValue)
            //    order.Sample.SampleID = Convert.ToInt32(orderResult.SampleID);
            order.UpdateSampleInfo = false;
            if (orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADInProgress) || orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingTMReview) || orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingCustomerConfirmation))
            {
                order.UpdateCADInfo = true;
                if (roleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                {
                    orderCAD = new OrdersCAD();
                    //orderCAD.CADLocationURL = AttachmentLocation; //Oct 23, 2015
                    orderCAD.CADLocationURL = Session["AttachmentLocation"].ToString();
                    orderCAD.UploadedBy = userID;
                }
                else if (roleID == Convert.ToInt32(Resource.DB_TMRoleID))
                {
                    //List<OrdersCAD> orderCADs = Session[Common.SESSION_CADS]as List<OrdersCAD>;
                    orderCAD = orderCADs.Where(x => x.IsApproved == null).SingleOrDefault();
                    if (ddlStatus.SelectedValue == Resource.DB_Status_ChangeRequest)
                    {
                        orderCAD.IsApproved = false;
                        orderCAD.ChangeInstructions = txtCRInstructions.Value;
                    }
                    else if (ddlStatus.SelectedValue == Resource.DB_Status_CADConfirmed)
                    {
                        orderCAD.IsApproved = true;
                    }
                }
            }
            else if (orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ProductionInProgress))
            {
                //if (Convert.ToBoolean(orderResult.IsSampleProvided))
                //{
                //    order.UpdateSampleInfo = true;     
                //    order.s
                //    //order.Sample.SampleID = Convert.ToInt32(orderResult.SampleID);
                //    //order.Sample.IsReturned = chkSampleReturned.Checked;
                //    //order.Sample.ReturnedDate = DateTime.Now;
                //}
            }
            else if (orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Shipped))
            {
                //if (Convert.ToBoolean(orderResult.IsSampleProvided))
                //{
                //    order.UpdateSampleInfo = true;                    
                //    //order.Sample.SampleID = Convert.ToInt32(orderResult.SampleID);
                //    //order.Sample.IsConfirmed = chkConfirmSampleReturned.Checked;
                //    //order.Sample.ConfirmedBy = userID;
                //}
            }
            order.OrderID = orderResult.OrderID;
            order.OrdersCADs.Add(orderCAD);

            if (ddlStatus.SelectedValue != string.Empty && ddlStatus.SelectedValue == Resource.DB_Status_PendingCustomerConfirmation)
                ddlAssignTo.SelectedValue = string.Empty;

            if (orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADInProgress) || orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingTMReview) ||
                orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ProductionInProgress))
                if (ddlAssignTo.SelectedValue != string.Empty)
                    order.AssignedTo = Convert.ToInt32(ddlAssignTo.SelectedValue);

            if ((orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_Initiated) && Convert.ToBoolean(orderResult.TMUserID != null)) ||
                orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADInProgress) || orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingTMReview) || orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PendingCustomerConfirmation) ||
                orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ChangeRequest) || orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_CADConfirmed) ||
                orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_PrototypingBegins) || orderResult.OrderStatusID == Convert.ToInt32(Resource.DB_Status_ProductionInProgress))
                if (ddlStatus.SelectedValue != string.Empty)
                    order.OrderStatusID = Convert.ToInt32(ddlStatus.SelectedValue);
            return order;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drview = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find dropdown control & get values
                //  Label lblUserID = (Label)e.Row.FindControl("lblUserID");
                LinkButton lnkUserID = (LinkButton)e.Row.FindControl("lnkViewPicture");
                Label lblIsApproved = (Label)e.Row.FindControl("lblIsApproved");
                //var IsApproved = DataBinder.Eval(e.Row.DataItem, "IsApproved");
                if (lblIsApproved.Text == string.Empty)
                    lblIsApproved.Text = "Pending";
                else
                    lblIsApproved.Text = lblIsApproved.Text == "True" ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);

                Label _lblIsCustomerInstruction = (Label)e.Row.FindControl("lblIsCustomerInstruction");
                Label _lblChangeInstructionsCustomer = (Label)e.Row.FindControl("lblChangeInstructionsCustomer");
                if (_lblChangeInstructionsCustomer.Text == "")
                {
                    if (lblIsApproved.Text == "Yes")
                    {
                        _lblChangeInstructionsCustomer.Text = "[ Approved: Yes ] " + _lblChangeInstructionsCustomer.Text;
                    }
                }
                else
                {
                    
                        _lblChangeInstructionsCustomer.Text = "[ Approved: No ] " + _lblChangeInstructionsCustomer.Text;
                    
                }

                if (_lblIsCustomerInstruction.Text == string.Empty)
                    _lblIsCustomerInstruction.Text = "No";
                else
                    _lblIsCustomerInstruction.Text = _lblIsCustomerInstruction.Text == "True" ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
                
                if (lblIsApproved.Text == Convert.ToString(Common.Response.Yes))
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#d6e9c6");
                    e.Row.ForeColor = System.Drawing.Color.FromName("#161816");
                }
                else if (lblIsApproved.Text == Convert.ToString(Common.Response.No))
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#E54625");
                    e.Row.ForeColor = System.Drawing.Color.FromName("#FCF8E3");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTableSample_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drview = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkReturn = (CheckBox)e.Row.FindControl("chkReturn");
                CheckBox chkConfirm = (CheckBox)e.Row.FindControl("chkConfirm");
                var IsApproved = DataBinder.Eval(e.Row.DataItem, "IsReturned");
                var IsConfirmed = DataBinder.Eval(e.Row.DataItem, "IsConfirmed");
                if (Convert.ToBoolean(IsApproved))
                {
                    chkReturn.Checked = true;
                    chkReturn.Enabled = false;
                }
                if (Convert.ToBoolean(IsConfirmed))
                {
                    chkConfirm.Checked = true;
                    chkConfirm.Enabled = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
                if (RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                    e.Row.Cells[2].Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == Convert.ToString(Common.ActionType.Compare))
            {
                int CADID = Convert.ToInt32(e.CommandArgument);
                CompareCADN.SetValues(_orderDetails, CADID);
                //Session[Common.SESSION_COMAPARECADIMAGE] = orderCADs.Where(x => x.CADID == CADID).SingleOrDefault().CAD;
                mpCompareCAD.Show();
                gvTableSamples.DataBind();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<Sample> lst = Session[Common.SESSION_SAMPLES] as List<Sample>;
            foreach (GridViewRow row in gvTableSamples.Rows)
            {
                int sampleID = Convert.ToInt32(((Label)row.FindControl("lblSampleID")).Text);
                Sample sample = lst.Where(x => x.SampleID == sampleID).SingleOrDefault();
                CheckBox chkReturn = (CheckBox)row.FindControl("chkReturn");
                CheckBox chkConfirm = (CheckBox)row.FindControl("chkConfirm");
                sample.IsReturned = chkReturn.Checked;
                if (chkConfirm.Checked)
                {
                    sample.IsReturned = chkConfirm.Checked;
                    sample.ConfirmedBy = UserID;
                    sample.IsConfirmed = chkConfirm.Checked;
                }
            }
            try
            {
                if (new OrderManager().UpdateSamples(lst))
                {
                    gvTableSamples.DataSource = Session[Common.SESSION_SAMPLES] as List<Sample>;
                    gvTableSamples.DataBind();
                    if ((Session[Common.SESSION_SAMPLES] as List<Sample>).Where(x => x.IsConfirmed == null || x.IsConfirmed == false).Count() == 0)
                        btnSave.Visible = false;
                    string errorMessage = Resource.Err_General;
                    MessageUtility.ShowMessage(dvMessageSample, ltMessageSample, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Updated);
                    mpSamples.Show();
                }
            }
            catch (BLLException exception)
            {
                MessageUtility.ShowMessage(dvMessageSample, ltMessageSample, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
                ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
                btnSave.CssClass = Resource.UI_BtnDanger;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void ImgProfie_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        //{
        //    try
        //    {
        //        if (loadImage)
        //        {
        //            Session[Common.SESSION_CADIMAGE] = null;
        //            if (imgCAD.PostedFile != null)
        //            {
        //                string fileName = string.Concat(Path.GetFileNameWithoutExtension(imgCAD.PostedFile.FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff"),
        //    Path.GetExtension(imgCAD.PostedFile.FileName));
        //                int contentLength = imgCAD.PostedFile.ContentLength;

        //                List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

        //                IsValidFile = validFileExtensions.Any(x => x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 && contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

        //                if (!IsValidFile)
        //                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
        //                else
        //                {
        //                    string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_CADIMAGESURL] + @"\" + Convert.ToString(_orderDetails.OrderID);

        //                    bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

        //                    if (!exists)
        //                        System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

        //                    AttachmentLocation = attachmentURL + @"\" + fileName;
        //                    imgCAD.SaveAs(Server.MapPath(AttachmentLocation));


        //                    OrdersCAD orderCAD = new OrdersCAD();
        //                    orderCAD.CADLocationURL = AttachmentLocation;
        //                    orderCAD.IsApproved = false;
        //                    List<OrdersCAD> lst = new List<OrdersCAD>();
        //                    if (Session[Common.SESSION_CADS] != null)
        //                        lst = Session[Common.SESSION_CADS] as List<OrdersCAD>;
        //                    lst.Add(orderCAD);
        //                    divUploadedCADs.Visible = true;
        //                }
        //            }
        //        }
        //        loadImage = loadImage == true ? false : true;
        //    }
        //    catch (Exception)
        //    { }
        //}


        protected void File_Upload(object sender, AjaxFileUploadEventArgs e)
        {
            try
            {
                Session[Common.SESSION_CADIMAGE] = null;
                message = message + "<b>" + e.FileName + "</b> (" + e.ContentType
                    + ") - <i>Upload1ed</i> <i class=\"icon-ok\"></i>";
                string fileName = string.Concat(Path.GetFileNameWithoutExtension(e.FileName),
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Path.GetExtension(e.FileName)
                    );
                int contentLength = e.FileSize;

                List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

                IsValidFile =
                    validFileExtensions.Any(
                        x =>
                            x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 &&
                            contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                if (!IsValidFile)
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                        Resource.Err_InvalidImage);
                else
                {
                    string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_CADIMAGESURL] + @"\" + Convert.ToString(_orderDetails.OrderID);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

                    AttachmentLocation = attachmentURL + @"\" + fileName;
                    specimenFileUpload.SaveAs(Server.MapPath(AttachmentLocation));


                    OrdersCAD orderCAD = new OrdersCAD();
                    orderCAD.CADLocationURL = AttachmentLocation;
                    orderCAD.IsApproved = false;
                    List<OrdersCAD> lst = new List<OrdersCAD>();
                    if (Session[Common.SESSION_CADS] != null)
                        lst = Session[Common.SESSION_CADS] as List<OrdersCAD>;
                    lst.Add(orderCAD);
                    divUploadedCADs.Visible = true;

                }
            }
            catch (Exception)
            {
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            uploadCad();
        }
        public void uploadCad()
        {
            HttpPostedFile file = Request.Files["testUpload"];

            
            
            //HttpFileCollection hfc = Request.Files;
            HttpFileCollection hfc = Request.Files;
            
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];

                Session[Common.SESSION_CADIMAGE] = null;
                message = message + "<b>" + hpf.FileName + "</b> (" + hpf.ContentType
                    + ") - <i>Upload1ed</i> <i class=\"icon-ok\"></i>";
                string fileName = string.Concat(Path.GetFileNameWithoutExtension(hpf.FileName),
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Path.GetExtension(hpf.FileName)
                    );
                int contentLength = hpf.ContentLength;

                List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

                IsValidFile =
                    validFileExtensions.Any(
                        x =>
                            x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 &&
                            contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                if (!IsValidFile)
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error),
                        Resource.Err_InvalidImage);
                else
                {
                    string attachmentURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_CADIMAGESURL] + @"\" + Convert.ToString(_orderDetails.OrderID);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(attachmentURL));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(attachmentURL));

                    AttachmentLocation = attachmentURL + @"\" + fileName;
                    specimenFileUpload.SaveAs(Server.MapPath(AttachmentLocation));


                    OrdersCAD orderCAD = new OrdersCAD();
                    orderCAD.CADLocationURL = AttachmentLocation;
                    orderCAD.IsApproved = false;
                    List<OrdersCAD> lst = new List<OrdersCAD>();
                    if (Session[Common.SESSION_CADS] != null)
                        lst = Session[Common.SESSION_CADS] as List<OrdersCAD>;
                    lst.Add(orderCAD);
                    divUploadedCADs.Visible = true;

                }
            }
            
        }
    }
}