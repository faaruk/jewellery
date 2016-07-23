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
    public partial class ModelTypes : BasePage
    {
        Collaboration.Business.Components.AdminManager _adminManager = new Business.Components.AdminManager();
        static ModelType _modelType = null;
        static int _modelTypeID = 0;
        public static string _actionType = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!BasePage.IsUserAuthorized((Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID))
            //    Response.Redirect("~/AccessDenied.html");
            //else
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTable_Sorting(object sender, GridViewSortEventArgs e)
        {
            //IEnumerable<ModelType> ModelTypesInfo = _adminManager.GetModelTypes();
            //ModelTypesInfo = ModelTypesInfo.OrderBy(e.SortExpression, e.SortDirection);

            //gvTable.DataSource = ModelTypesInfo.ToArray();
            //gvTable.DataBind();
        }
        /// <summary>
        /// Bind ModelType data to grid
        /// </summary>
        private void BindGrid(List<ModelType> filteredModelTypes)
        {
            gvTable.DataSource = filteredModelTypes;
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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:EditableTable.init();", true);            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdModelTypes_RowDataBound(object sender, GridViewRowEventArgs e)
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
                _modelTypeID = Convert.ToInt32(e.CommandArgument);
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
            int modelTypeID = Convert.ToInt32(ViewState[Common.EntityAttributes.MODELTYPEID]);
            Delete(modelTypeID);
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

            ViewState[Common.EntityAttributes.MODELTYPEID] = btndetails.CommandArgument;
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
            // EditProfile.btnHandler += new ModelTypeControl.EditProfile.OnButtonClick(WebModelTypeControl1_btnHandler);
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
        /// Bind ModelType data to grid
        /// </summary>
        private void BindGrid()
        {
            IEnumerable<ModelType> ModelTypesInfo = _adminManager.GetModelTypes();
            Session[Collaboration.Web.UI.Common.SESSION_MODELTYPELIST] = ModelTypesInfo;

            gvTable.DataSource = ModelTypesInfo.ToArray();
            gvTable.DataBind();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        public void Delete(int userID)
        {
            try
            {
                _adminManager.DeleteModelType(userID);
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
                _modelType = (Session[Collaboration.Web.UI.Common.SESSION_MODELTYPELIST] as List<ModelType>).Where(x => x.ModelTypeID == _modelTypeID).First();
            else if (_actionType == Convert.ToString(Common.ActionType.Add))
                _modelType = new ModelType();


            txtModelCode.Text = _modelType.ModelCode;
            txtDescription.Text = _modelType.Description;
            txtSortOrder.Text = Convert.ToString(_modelType.SortOrder);
        }
        /// <summary>
        /// 
        /// </summary>
        private void ResetValues()
        {
            txtModelCode.Text = txtDescription.Text = string.Empty;
            MessageUtility.ClearMessages(dvMessage, ltMessage);
            btnUpdate.CssClass = Resource.UI_BtnPrimary;
            btnUpdate.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            //_ModelTypeInfo = Session[Collaboration.Web.UI.Common.SESSION_ModelType] as ModelType;
            if (IsUniqModelType())
            {
                if (_actionType == Convert.ToString(Common.ActionType.Add))
                {
                    _modelType = new ModelType();
                    _modelType.CreateDate = DateTime.Now;
                    _modelType.IsActive = true;
                }
                else
                    _modelType.ModifyDate = DateTime.Now;


                _modelType.ModelCode = txtModelCode.Text;
                _modelType.Description = txtDescription.Text;
                _modelType.SortOrder = Convert.ToByte(txtSortOrder.Text);

                string ImageName = string.Empty;

                AccountManager accountManager = new AccountManager();
                try
                {
                    if (_actionType == Convert.ToString(Common.ActionType.Add))
                    {
                        if (_adminManager.CreateModelType(_modelType))
                        {
                            MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_Added);
                            lblMessage.Text = Resource.Info_Added;
                        }
                    }
                    else
                    {
                        if (_adminManager.ModifyModelType(_modelType))
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
                    Session[Collaboration.Web.UI.Common.SESSION_MODELTYPELIST] = _adminManager.GetModelTypes();
                    mpAckDelete.Show();
                    updAdd.Update();
                    //mpAdd.Show();
                }
            }
            else
            {
                UniqModelValidator.IsValid = false;
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
                    "ModelType after you got the original value. The edit or delete operation was canceled " +
                    "and the other ModelType's values have been displayed so you can " +
                    "determine whether you still want to edit or delete this record.";
                Page.Validators.Add(concurrencyExceptionValidator);
                e.ExceptionHandled = true;
            }
        }

        protected void txtModelCode_TextChanged(object sender, EventArgs e)
        {
            UniqModelValidator.IsValid = IsUniqModelType();
            mpAdd.Show();
        }

        bool IsUniqModelType()
        {
            if (_actionType == Convert.ToString(Common.ActionType.Add))
            {
                return (Session[Collaboration.Web.UI.Common.SESSION_MODELTYPELIST] as IEnumerable<ModelType>).Where(m => m.ModelCode == txtModelCode.Text).Count() == 0;
            }
            else
            {
                return (Session[Collaboration.Web.UI.Common.SESSION_MODELTYPELIST] as IEnumerable<ModelType>).Where(m => m.ModelTypeID != _modelTypeID && m.ModelCode == txtModelCode.Text).Count() == 0;
            }
        }
    }
}