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
using System.Data;
using System.Linq.Expressions;
using AjaxControlToolkit;

namespace Collaboration.Web.UI.Admin
{
    public partial class FingerMeasurements : BasePage
    {
        Collaboration.Business.Components.AdminManager _adminManager = new Business.Components.AdminManager();
        static FingerSize _fingerSize = null;
        static int _fingerSizeID = 0;
        public static string _actionType = string.Empty;        


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!BasePage.IsUserAuthorized((Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID))
            //    Response.Redirect("~/AccessDenied.html");
            //else 
                if (!IsPostBack)
                BindGrid();    
        }       
         /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_Sorting(object sender, GridViewSortEventArgs e)
        {
            //IEnumerable<FingerSize> FingerSizesInfo = _adminManager.GetFingerSizes();
            //FingerSizesInfo = FingerSizesInfo.OrderBy(e.SortExpression, e.SortDirection);

            //gvTable.DataSource = FingerSizesInfo.ToArray();
            //gvTable.DataBind();
        }
        /// <summary>
        /// Bind FingerSize data to grid
        /// </summary>
        private void BindGrid(List<FingerSize> filteredFingerSizes)
        {
            gvTable.DataSource = filteredFingerSizes;
            gvTable.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_PreRender(object sender, EventArgs e)
        {
            //if (_actionType == Convert.ToString(Common.ActionType.Edit))
            //    return;
            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (gvTable.Rows.Count > 0)
            {                
                //This will add the <thead> and <tbody> elements
                gvTable.HeaderRow.TableSection = TableRowSection.TableHeader;               
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:EditableTable.init();", true);            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == Convert.ToString(Common.ActionType.Edit))
            {
                _fingerSizeID = Convert.ToInt32(e.CommandArgument);
                _actionType = Convert.ToString(Common.ActionType.Edit);
                ShowAddDialog();                
            }
        }       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
        }
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnYes_Click(object sender, EventArgs e)
        {
            int fingerSizeID = Convert.ToInt32(ViewState[Common.EntityAttributes.FINGERSIZEID]);
            Delete(fingerSizeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btndetails = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

            ViewState[Common.EntityAttributes.FINGERSIZEID] = btndetails.CommandArgument;
            mpConfirmDelete.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            _actionType = Convert.ToString(Common.ActionType.Add);
            // EditProfile.btnHandler += new FingerSizeControl.EditProfile.OnButtonClick(WebFingerSizeControl1_btnHandler);
            ShowAddDialog();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowAddDialog()
        {
            pnl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
            mpAdd.Show();
            FillInfo();
            updAdd.Update();
        }
        /// <summary>
        /// Bind FingerSize data to grid
        /// </summary>
        private void BindGrid()
        {
            IEnumerable<FingerSize> fingerSizesInfo = _adminManager.GetFingerSizes();
            Session[Collaboration.Web.UI.Common.SESSION_FINGERSIZELIST] = fingerSizesInfo;

            gvTable.DataSource = fingerSizesInfo.ToArray();
            gvTable.DataBind();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        public void Delete(int fingerSizeID)
        {
            try
            {
                _adminManager.DeleteFingerSize(fingerSizeID);
                lblMessage.Text = Resource.Info_Deleted;
                BindGrid();
            }
            catch (BLLException exception)
            {
                // MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
                ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
                lblMessage.Text = exception.ErrorMessage;
            }
            catch (Exception exception)
            {
                // MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                ExceptionUtility.LogException(exception, Request.RawUrl);
                lblMessage.Text = Resource.Err_General;
            }
            finally
            {                
                mpAckDelete.Show();
                updGrid.Update();
                updAdd.Update();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillInfo()
        {
            ResetValues();
            if (_actionType == Convert.ToString(Common.ActionType.Edit))
                _fingerSize = (Session[Collaboration.Web.UI.Common.SESSION_FINGERSIZELIST] as List<FingerSize>).Where(x => x.FingerSizeID == _fingerSizeID).First();
            else if (_actionType == Convert.ToString(Common.ActionType.Add))           
                _fingerSize = new FingerSize();


            txtFingerSize.Text = _fingerSize.Size;
            txtDescription.Text = _fingerSize.Description;           
        }
        /// <summary>
        /// 
        /// </summary>
        private void ResetValues()
        {
            txtFingerSize.Text = txtDescription.Text = string.Empty;
            MessageUtility.ClearMessages(dvMessage, ltMessage);
            btnUpdate.CssClass = Resource.UI_BtnPrimary;
            btnUpdate.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            //_fingerSizeInfo = Session[Collaboration.Web.UI.Common.SESSION_fingerSize] as FingerSize;
            if (IsFingerSizeUniq())
            {
                if (_actionType == Convert.ToString(Common.ActionType.Add))
                {
                    _fingerSize = new FingerSize();
                    _fingerSize.CreateDate = DateTime.Now;
                    _fingerSize.IsActive = true;
                }
                else
                    _fingerSize.ModifyDate = DateTime.Now;


                _fingerSize.Size = txtFingerSize.Text;
                _fingerSize.Description = txtDescription.Text;

                string ImageName = string.Empty;

                AccountManager accountManager = new AccountManager();
                try
                {
                    if (_actionType == Convert.ToString(Common.ActionType.Add))
                    {
                        if (_adminManager.CreateFingerSize(_fingerSize))
                        {
                            MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Added);
                            lblMessage.Text = Resource.Info_Added;
                        }
                    }
                    else
                    {
                        if (_adminManager.ModifyFingerSize(_fingerSize))
                        {
                            lblMessage.Text = Resource.Info_Updated;
                            MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Updated);
                        }
                    }
                    BindGrid();
                    btnUpdate.CssClass = Resource.UI_BtnSuccess;
                    btnUpdate.Enabled = false;
                    updGrid.Update();
                }
                catch (BLLException exception)
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), exception.ErrorMessage);
                    ExceptionUtility.LogException(exception, Common.INFO_PROCEDURE + exception.ProcedureName);
                    btnUpdate.CssClass = Resource.UI_BtnDanger;
                }
                catch (Exception exception)
                {
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                    ExceptionUtility.LogException(exception, Request.RawUrl);
                    btnUpdate.CssClass = Resource.UI_BtnDanger;
                }
                finally
                {
                    Session[Collaboration.Web.UI.Common.SESSION_FINGERSIZELIST] = _adminManager.GetFingerSizes();
                    mpAckDelete.Show();
                    updAdd.Update();
                    //mpAdd.Show();
                }
            }
            else
            {
                UniqFingerSizeValidator.IsValid = false;
                mpAdd.Show();
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="function"></param>
        private void CheckForOptimisticConcurrencyException(ObjectDataSourceStatusEventArgs e, string function)
        {
            if (e.Exception.InnerException is OptimisticConcurrencyException)
            {
                var concurrencyExceptionValidator = new CustomValidator();
                concurrencyExceptionValidator.IsValid = false;
                concurrencyExceptionValidator.ErrorMessage =
                    "The record you attempted to edit or delete was modified by another " +
                    "FingerSize after you got the original value. The edit or delete operation was canceled " +
                    "and the other FingerSize's values have been displayed so you can " +
                    "determine whether you still want to edit or delete this record.";
                Page.Validators.Add(concurrencyExceptionValidator);
                e.ExceptionHandled = true;
            }
        }

        bool IsFingerSizeUniq()
        {
            var SizeList = (Session[Collaboration.Web.UI.Common.SESSION_FINGERSIZELIST] as List<FingerSize>);
            if(_fingerSizeID>0)
            {
                return SizeList.Where(s=>s.FingerSizeID!=_fingerSizeID && s.Size == txtFingerSize.Text).Count() == 0;
            }
            else
            {
                return SizeList.Where(s=>s.Size == txtFingerSize.Text).Count() == 0;
            }
        }

        protected void txtFingerSize_TextChanged(object sender, EventArgs e)
        {
            UniqFingerSizeValidator.IsValid = IsFingerSizeUniq();
            mpAdd.Show();
        }
    }
}