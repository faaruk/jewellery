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
    public partial class Priorities : BasePage
    {
        Collaboration.Business.Components.AdminManager _adminManager = new Business.Components.AdminManager();
        static Priority _Priority = null;
        static int _PriorityID = 0;
        public static string _actionType = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
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
            
        }
        /// <summary>
        /// Bind Priority data to grid
        /// </summary>
        private void BindGrid(List<Priority> filteredPrioritys)
        {
            gvTable.DataSource = filteredPrioritys;
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
                _PriorityID = Convert.ToInt32(e.CommandArgument);
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
            int PriorityID = Convert.ToInt32(ViewState[Common.EntityAttributes.PRIORITYID]);
            Delete(PriorityID);
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

            ViewState[Common.EntityAttributes.PRIORITYID] = btndetails.CommandArgument;
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
            // EditProfile.btnHandler += new PriorityControl.EditProfile.OnButtonClick(WebPriorityControl1_btnHandler);
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
        /// Bind Priority data to grid
        /// </summary>
        private void BindGrid()
        {
            IEnumerable<Priority> PrioritysInfo = _adminManager.GetPriorities();
            Session[Collaboration.Web.UI.Common.SESSION_PRIORITIESLIST] = PrioritysInfo;

            gvTable.DataSource = PrioritysInfo.ToArray();
            gvTable.DataBind();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        public void Delete(int PriorityID)
        {
            try
            {
                _adminManager.DeletePriority(PriorityID);
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
                _Priority = (Session[Collaboration.Web.UI.Common.SESSION_PRIORITIESLIST] as List<Priority>).Where(x => x.PriorityID == _PriorityID).First();
            else if (_actionType == Convert.ToString(Common.ActionType.Add))           
                _Priority = new Priority();


            txtPriority.Text = _Priority.Name;
            txtFrequency.Text = Convert.ToString(_Priority.Frequency);
            txtDescription.Text = _Priority.Description;           
        }
        /// <summary>
        /// 
        /// </summary>
        private void ResetValues()
        {
            txtPriority.Text = txtDescription.Text = txtFrequency.Text = string.Empty;
            MessageUtility.ClearMessages(dvMessage, ltMessage);
            btnUpdate.CssClass = Resource.UI_BtnPrimary;
            btnUpdate.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            if (IsPriorityUniq())
            {
                //_PriorityInfo = Session[Collaboration.Web.UI.Common.SESSION_Priority] as Priority;
                if (_actionType == Convert.ToString(Common.ActionType.Add))
                {
                    _Priority = new Priority();
                    _Priority.CreateDate = DateTime.Now;
                    _Priority.IsActive = true;
                }
                else
                    _Priority.ModifyDate = DateTime.Now;


                _Priority.Name = txtPriority.Text;
                _Priority.Frequency = Convert.ToInt32(txtFrequency.Text);
                _Priority.Description = txtDescription.Text;

                string ImageName = string.Empty;

                AccountManager accountManager = new AccountManager();
                try
                {
                    if (_actionType == Convert.ToString(Common.ActionType.Add))
                    {
                        if (_adminManager.CreatePriority(_Priority))
                        {
                            MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Added);
                            lblMessage.Text = Resource.Info_Added;
                        }
                    }
                    else
                    {
                        if (_adminManager.ModifyPriority(_Priority))
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
                    Session[Collaboration.Web.UI.Common.SESSION_PRIORITIESLIST] = _adminManager.GetPriorities();
                    mpAckDelete.Show();
                    updAdd.Update();
                    //mpAdd.Show();
                }
            }
            else
            {
                PriorityUniqValidator.IsValid = false;
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
                    "Priority after you got the original value. The edit or delete operation was canceled " +
                    "and the other Priority's values have been displayed so you can " +
                    "determine whether you still want to edit or delete this record.";
                Page.Validators.Add(concurrencyExceptionValidator);
                e.ExceptionHandled = true;
            }
        }

        protected void txtPriority_TextChanged(object sender, EventArgs e)
        {
            PriorityUniqValidator.IsValid = IsPriorityUniq();
            mpAdd.Show();
        }

        bool IsPriorityUniq()
        {
            var PriorityList =(Session[Collaboration.Web.UI.Common.SESSION_PRIORITIESLIST] as List<Priority>);
            if (_PriorityID > 0)
            {
                return PriorityList.Where(x => x.PriorityID != _PriorityID && x.Name == txtPriority.Text).Count() == 0;
            }
            else
            {
                return PriorityList.Where(x => x.Name == txtPriority.Text).Count() == 0;
            }
        }
    }
}