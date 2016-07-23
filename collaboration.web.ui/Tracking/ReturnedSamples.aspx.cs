using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Collaboration.Business.Entities;
using System.Data;
using Collaboration.Business.Components;
using Collaboration.Web.UI.Utilities;

namespace Collaboration.Web.UI.Tracking
{
    public partial class ReturnedSamples : BasePage
    {
        Collaboration.Business.Components.AdminManager _adminManager = new Business.Components.AdminManager();
        //  static List<SamplesTracking> _lstSampleTrack = null;
        static SamplesTracking _samplesTrack = null;
        static SampleTracking_Result _sampleTrackInfo = null;
        static int _sampleTrackID = 0;
        public static string _actionType = string.Empty;
        int _roleID = 0;
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            ViewMessages.btnHandler += new UserControl.UC_TrackSHistory.OnButtonClick(ViewMessages_btnHandler);
            if (!IsPostBack)
                BindGrid();
        }

        /// <summary>
        /// Bind Sample Status Tracking  data to grid
        /// </summary>
        private void BindGrid()
        {
            List<SampleTracking_Result> samplesTrackInfo = _adminManager.SamplesTracking_Result().Where(st => st.SampleStatusID == (Convert.ToInt32(Resource.DB_SampleStatus_ReceivedAtUK))).ToList();
            Session[Collaboration.Web.UI.Common.SESSION_SAMPLESTRACKLIST] = samplesTrackInfo;

            gvTable.DataSource = samplesTrackInfo.ToArray();
            gvTable.DataBind();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_PreRender(object sender, EventArgs e)
        {
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (gvTable.Rows.Count > 0)
            {
                //This will add the <thead> and <tbody> elements
                gvTable.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:EditableTable.init();", true);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        protected void gvTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == Convert.ToString(Common.ActionType.Edit))
            //{
            //    _sampleTrackID = Convert.ToInt32(e.CommandArgument);
            //    _actionType = Convert.ToString(Common.ActionType.Edit);
            //    ShowAddDialog();
            //}
            if (e.CommandName == Convert.ToString(Common.ActionType.ViewDetails))
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                int SampleID = Convert.ToInt32(commandArgs[0]);
                int SampleTrackID = Convert.ToInt32(commandArgs[1]);
                string SampleSerialNumber = commandArgs[2].ToString();

                ShowViewDetailsDialog(SampleID, SampleTrackID, SampleSerialNumber);
            }
        }
        public void ShowViewDetailsDialog(int SampleID, int SampleTrackID, string SampleSerialNumber)
        {
            //pnl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
            mpViewDetails.Show();
            ViewMessages.SampleID = SampleID;
            ViewMessages.SampleTrackID = SampleTrackID;
            ViewMessages.SampleSerialNumber = SampleSerialNumber;
            Label1.Text = SampleSerialNumber;
            ViewMessages.FillInfo();
            updViewDetails.Update();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_Sorting(object sender, GridViewSortEventArgs e)
        {

        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void gvTable_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvTable.PageIndex = e.NewPageIndex;
            //BindGrid();
        }

        /// <summary>
        /// Bind Sample Status data to grid
        /// </summary>
        private void BindGrid(List<SampleTracking_Result> filteredSampleTrack)
        {
            gvTable.DataSource = filteredSampleTrack;
            gvTable.DataBind();


        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ddlList"></param>
        ///// <param name="dataSource"></param>
        //public void FillDropDown(DropDownList ddlList, Array dataSource, string optionalstr = Common.DROPDOWN_SELECT_TEXT, bool bindDefaultValue = true)
        //{
        //    ddlList.DataSource = dataSource;
        //    ddlList.DataBind();
        //    if (bindDefaultValue)
        //        ddlList.Items.Insert(0, new ListItem(optionalstr, Common.DROPDOWN_SELECT_VALUE));
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public void ShowAddDialog()
        //{
        //    pnl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
        //    mpAdd.Show();
        //    FillInfo();
        //    updAdd.Update();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    Update();
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        //public void Update()
        //{
        //    //_lstSampleTrack = _adminManager.SamplesTracking_Select();

        //    _samplesTrack = _adminManager.SamplesTracking_Select(_sampleTrackID);

        //    _samplesTrack.ModifyDate = DateTime.Now;

        //    _samplesTrack.SampleStatusID = Convert.ToInt32(ddlSampleStatusName.SelectedValue);


        //    AccountManager accountManager = new AccountManager();
        //    try
        //    {
        //        if (_adminManager.SamplesTracking_Update(_samplesTrack))
        //        {
        //            lblMessage.Text = Resource.Info_Updated;
        //            MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Updated);
        //        }


        //        BindGrid();
        //        btnUpdate.CssClass = Resource.UI_BtnSuccess;
        //        btnUpdate.Enabled = false;
        //        updGrid.Update();
        //    }
        //    catch (BLLException exception)
        //    {
        //        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
        //        ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
        //        btnUpdate.CssClass = Resource.UI_BtnDanger;
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
        //        ExceptionUtility.LogException(exception, Request.RawUrl);
        //        btnUpdate.CssClass = Resource.UI_BtnDanger;
        //    }
        //    finally
        //    {
        //        Session[Collaboration.Web.UI.Common.SESSION_SAMPLESTRACKLIST] = _adminManager.SamplesTracking_Result();
        //        mpAckDelete.Show();
        //        updAdd.Update();

        //    }

        //}







        ///// <summary>
        ///// 
        ///// </summary>
        //private void FillInfo()
        //{
        //    ResetValues();
        //    if (_actionType == Convert.ToString(Common.ActionType.Edit))
        //    {
        //        _sampleTrackInfo = (Session[Collaboration.Web.UI.Common.SESSION_SAMPLESTRACKLIST] as List<SampleTracking_Result>).Where(x => x.SampleTrackID == _sampleTrackID).First();

        //        List<SampleStatu> _lstSampleStatus = _adminManager.GetSampleStatus().ToList();


        //        lblSampleSerialNumber.Text = _sampleTrackInfo.SampleSerialNumber;

        //        if (_sampleTrackInfo.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_Pending))
        //            FillDropDown(ddlSampleStatusName, _lstSampleStatus.Where(x => x.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_SentToChina)).ToArray(), "", false);
        //        else if (_sampleTrackInfo.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_SentToChina))
        //            FillDropDown(ddlSampleStatusName, _lstSampleStatus.Where(x => x.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_ReceivedAtChina)).ToArray(), "", false);
        //        else if (_sampleTrackInfo.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_ReceivedAtChina))
        //            FillDropDown(ddlSampleStatusName, _lstSampleStatus.Where(x => x.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_ReturnedToUK)).ToArray(), "", false);
        //        else if (_sampleTrackInfo.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_ReturnedToUK))
        //            FillDropDown(ddlSampleStatusName, _lstSampleStatus.Where(x => x.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_ReceivedAtUK)).ToArray(), "", false);
        //        else if (_sampleTrackInfo.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_ReceivedAtUK))
        //            FillDropDown(ddlSampleStatusName, _lstSampleStatus.Where(x => x.SampleStatusID == Convert.ToInt32(Resource.DB_SampleStatus_SentToChina)).ToArray(), "", false);


        //        ddlSampleStatusName.SelectedIndex = 0;
        //    }

        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //private void ResetValues()
        //{
        //    lblSampleSerialNumber.Text = string.Empty;
        //    //  ddlSampleStatusName.SelectedIndex = 0;
        //    ddlSampleStatusName.Items.Clear();
        //    MessageUtility.ClearMessages(dvMessage, ltMessage);
        //    btnUpdate.CssClass = Resource.UI_BtnPrimary;
        //    btnUpdate.Enabled = true;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        ///// <param name="function"></param>
        //private void CheckForOptimisticConcurrencyException(ObjectDataSourceStatusEventArgs e, string function)
        //{
        //    if (e.Exception.InnerException is OptimisticConcurrencyException)
        //    {
        //        var concurrencyExceptionValidator = new CustomValidator();
        //        concurrencyExceptionValidator.IsValid = false;
        //        concurrencyExceptionValidator.ErrorMessage =
        //            "The record you attempted to edit or delete was modified by another " +
        //            "Sample Status after you got the original value. The edit or delete operation was canceled " +
        //            "and the other Sample Status values have been displayed so you can " +
        //            "determine whether you still want to edit or delete this record.";
        //        Page.Validators.Add(concurrencyExceptionValidator);
        //        e.ExceptionHandled = true;
        //    }
        //}

        //protected void gvTable_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //Find dropdown control & get values
        //        HiddenField hdnSampleStatusID = (HiddenField)e.Row.FindControl("hdnSampleStatusID");
        //        LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");
        //        if (RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
        //        {
        //            if (Convert.ToInt32(hdnSampleStatusID.Value) == Convert.ToInt32(Resource.DB_SampleStatus_Pending) ||
        //                 Convert.ToInt32(hdnSampleStatusID.Value) == Convert.ToInt32(Resource.DB_SampleStatus_ReturnedToUK) ||
        //                Convert.ToInt32(hdnSampleStatusID.Value) == Convert.ToInt32(Resource.DB_SampleStatus_ReceivedAtUK))
        //            {
        //                lnkUpdate.Visible = false;
        //            }
        //            else
        //            {
        //                lnkUpdate.Visible = true;
        //            }

        //        }
        //        else if (RoleID == Convert.ToInt32(Resource.DB_TMRoleID))
        //        {
        //            if (Convert.ToInt32(hdnSampleStatusID.Value) == Convert.ToInt32(Resource.DB_SampleStatus_SentToChina) ||
        //                Convert.ToInt32(hdnSampleStatusID.Value) == Convert.ToInt32(Resource.DB_SampleStatus_ReceivedAtChina)
        //                )
        //            {
        //                lnkUpdate.Visible = false;
        //            }
        //            else
        //            {
        //                lnkUpdate.Visible = true;
        //            }

        //        }
        //    }
        //}       
        protected void btnCancelDetails_Click(object sender, EventArgs e)
        {
            //Session["RequestedMessageThredID"] = null;
            //bool markAsRead = new MessageManager().MarkMessagesAsRead(MessageThreadID, UserID);
            //if (markAsRead)
            //{
            //    BindGrid();
            //    updGrid.Update();
            //    Session[Collaboration.Web.UI.Common.SESSION_COUNTUNREADMESSAGES] = new MessageManager().GetCountUnreadMessages(UserID);
            //    Session[Collaboration.Web.UI.Common.SESSION_UNREADMESSAGES] = null;
            //    Session[Collaboration.Web.UI.Common.SESSION_MESSAGESASSIGNED] = null;
            //    (this.Master as DasbhoardMaster).UpdateTickets();


            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        void ViewMessages_btnHandler(string strValue)
        {
            mpViewDetails.Show();
            updViewDetails.Update();
        }
    }
}