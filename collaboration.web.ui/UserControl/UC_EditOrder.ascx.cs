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
using AjaxControlToolkit;
using System.Xml;
using System.Configuration;

using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Colors;
using CodeCarvings.Piczard.Web;
using System.Data;
using System.Web.Caching;
using DevExpress.XtraPrinting.Native;

namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_EditOrder : System.Web.UI.UserControl
    {
        private static int _userID = 0;
        private static int _roleID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }
        private static byte[] Image = null;
        private static bool _isValidFile = true;
        public bool IsValidFile { set { _isValidFile = value; } get { return _isValidFile; } }
        private static int specimenFilesUploaded = 0;
        private static string message = string.Empty;
        public static string EditedImageUrl = string.Empty;
        public static int OrderID = 0;
        public static bool IsNewImage = false;
        public static DataTable dtTempImages = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlCustomers.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        public void FillInfo(bool getNewSerialNumber = true)
        {

            if (!specimenFileUpload.IsInFileUploadPostBack)
            {
                FillDropDown(ddlModelTypes, new AdminManager().GetModelTypes().ToArray());
                FillDropDown(ddlPriority, new AdminManager().GetPriorities().ToArray());
                FillDropDown(ddlFingerSize, new AdminManager().GetFingerSizes().ToArray());
                ddlFingerSize.Items.Insert(ddlFingerSize.Items.Count, new ListItem(Common.DROPDOWN_OTHERS_TEXT, Common.DROPDOWN_OTHERS_VALUE));
                FillDropDown(ddlMetals, new AdminManager().GetMetals().ToArray());
                ddlMetals.Items.Insert(ddlMetals.Items.Count, new ListItem(Common.DROPDOWN_OTHERS_TEXT, Common.DROPDOWN_OTHERS_VALUE));
                FillDropDown(ddlProcessTypes, new AdminManager().GetProcessTypes().ToArray());
                FillDropDown(ddlRingType, new AdminManager().GetRingTypes().ToArray());
                List<Customer> customer = new AdminManager().GetCustomers();

                //FillDropDown(ddlCustomers, customer.ToArray(),"Choose Customer");
                FillCustomerDropDown(ddlCustomers, customer, "Choose Customer");

                FillDropDown(ddlCustomerEmail, customer.ToArray(), "Customer E-mail");
                FillDropDown(ddlModelSubType, GetModelSubType());
                FillDropDown(ddlLength, GetLength());
                FillDropDown(ddlQuantity, GetQuantity(), "", false);
                ddlQuantity.Items.Insert(ddlQuantity.Items.Count, new ListItem(Common.DROPDOWN_OTHERS_TEXT, Common.DROPDOWN_OTHERS_VALUE));
                //FillDropDown(ddlNoOfSamples, GetQuantity(Common.EnumValue(Common.Quantity.Three)), "", false);
                FillDropDown(ddlNoOfSamples, GetQuantityModified("10"), "", false); //Need to change this common class as soon as Project file will get eidted
                //ddlNoOfSamples.Items.Insert(ddlNoOfSamples.Items.Count, new ListItem(Common.DROPDOWN_OTHERS_TEXT, Common.DROPDOWN_OTHERS_VALUE));

                //if (getNewSerialNumber)
                //    txtSerialNumber.Text = new OrderManager().GetAndSerialNumber();
                //txtSampleSerialNumber.Text = txtSerialNumber.Text.Replace(Resource.DB_SerialNumberPrefix, Resource.DB_SampleSerialNumberPrefix);

                List<User> users = new AccountManager().GetUsers();
                FillDropDown(ddlTemaMembers, users.Where(x => x.RoleID == Convert.ToInt32(Resource.DB_TMRoleID)).ToArray());

                //FillDropDown(ddlAssignee, users.ToArray());
                //FillDropDown(ddlAssignee, users.Where(x => x.UserID != UserID && x.RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID)).ToArray());  // Bind users except logged in user
                if (RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                {
                    FillDropDown(ddlAssignee, users.Where(x => x.UserID == UserID).ToArray(), "", false);  // Bind users except logged in user
                }
                else
                {
                    FillDropDown(ddlAssignee, users.Where(x => x.UserID != UserID && x.RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID)).ToArray());  // Bind users except logged in user}
                }
                if (RoleID == Convert.ToInt32(Resource.DB_TMRoleID) || RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
                {
                    dvTeamMember.Visible = false;
                    ddlTemaMembers.Visible = false;
                    TeamMembersRequired.Enabled = false;
                }
                specimenFileUpload.MaximumNumberOfFiles = Convert.ToInt32(Resource.MaxSpecimenUpload);

                //picSpecimen.Visible = false;
                //ddlModelTypes.SelectedIndex = 1;
                //ddlCustomers.SelectedIndex = 1;
                //ddlMetals.SelectedIndex = 1;
                //ddlProcessTypes.SelectedIndex = 1;
                //ddlPriority.SelectedIndex = 1;
                //ddlTemaMembers.SelectedIndex = 1;
                //ddlAssignee.SelectedIndex = 1;
                //txtModelNumber.Text = "CX";
                //ddlRingType.SelectedIndex = 3;
                //ddlFingerSize.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Array GetModelSubType()
        {
            List<ListItem> lst = new List<ListItem>();
            foreach (Common.SubType r in Enum.GetValues(typeof(Common.SubType)))
            {
                ListItem item = new ListItem(r.EnumValue(), Convert.ToInt32(r).ToString());
                lst.Add(item);
            }
            return lst.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        private Array GetLength()
        {
            List<ListItem> lst = new List<ListItem>();
            foreach (Common.Length r in Enum.GetValues(typeof(Common.Length)))
            {
                ListItem item = new ListItem(Enum.GetName(typeof(Common.Length), r), r.ToString());
                lst.Add(item);
            }
            return lst.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        private Array GetQuantity(string NoOfCount = "")
        {
            List<ListItem> lst = new List<ListItem>();
            foreach (Common.Quantity r in Enum.GetValues(typeof(Common.Quantity)))
            {
                ListItem item = new ListItem(r.EnumText(), r.EnumValue());
                lst.Add(item);
                if (item.Value == NoOfCount)
                    break;
            }
            return lst.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        private Array GetQuantityModified(string NoOfCount = "")
        {
            List<ListItem> lst = new List<ListItem>();
            for (int i = 1; i <= 10; i++)
            {
                ListItem item = new ListItem((i).ToString(), (i).ToString());
                lst.Add(item);
                if (item.Value == NoOfCount)
                    break;
            }

            return lst.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        // public void SetValues(OrderDetails_Result orderDetails)
        public Dictionary<string, string> SetValues(OrderDetails_Result orderDetails)
        {
            // int NoOfSample = 0;
            Dictionary<string, string> dicSample = new Dictionary<string, string>();

            if (!specimenFileUpload.IsInFileUploadPostBack)
            {
                specimenFilesUploaded = 0;
                divSerialNumber.Visible = true;
                ddlModelTypes.SelectedValue = Convert.ToString(orderDetails.ModelTypeID);
                ddlAssignee.SelectedValue = Convert.ToString(orderDetails.AssignedTo);

                ddlCustomers.SelectedValue = Convert.ToString(orderDetails.CustomerID);
                //Customer objCustomer = (new AdminManager().GetCustomers().Where(x => x.CustomerID == orderDetails.CustomerID).FirstOrDefault());
                //txtCustomer.Text = objCustomer.CustomerName+":"+objCustomer.CustomerCode;

                ddlCustomerEmail.SelectedValue = Convert.ToString(orderDetails.CustomerID);
                ddlCustomerEmail.SelectedValue = Convert.ToString(orderDetails.CustomerID);

                txtCustomerEmail.Value = ddlCustomerEmail.SelectedItem.Text;
                //  txtCustomerEmail.Value = ddlCustomerEmail.SelectedValue;


                lblSerialNumber.Text = orderDetails.SerialNumber;
                lblSampleSerialNumber.Text = orderDetails.SampleSerialNumber;
                ddlFingerSize.SelectedValue = Convert.ToString(orderDetails.FingerSizeID);
                if (orderDetails.MetalID == null)
                {
                    ddlMetals.SelectedValue = Convert.ToString(Common.DROPDOWN_OTHERS_VALUE);
                    txtMetalOther.Text = orderDetails.MetalOther;
                }
                else
                    ddlMetals.SelectedValue = Convert.ToString(orderDetails.MetalID);
                if (orderDetails.Quantity == null)
                {
                    ddlQuantity.SelectedValue = Convert.ToString(Common.DROPDOWN_OTHERS_VALUE);
                    txtQuantityOther.Text = orderDetails.QuantityOther;
                }
                else
                    ddlQuantity.SelectedValue = Convert.ToString(orderDetails.Quantity);


                ddlQuantity.SelectedValue = Convert.ToString(orderDetails.Quantity);
                ddlPriority.SelectedValue = Convert.ToString(orderDetails.PriorityID);
                ddlProcessTypes.SelectedValue = Convert.ToString(orderDetails.ProcessTypeID);
                ddlRingType.SelectedValue = Convert.ToString(orderDetails.RingTypeID);
                ddlModelSubType.SelectedValue = Convert.ToString(orderDetails.ModelSubTypeID);
                ddlLength.SelectedValue = Convert.ToString(orderDetails.LengthMeasurement);
                txtLength.Text = Convert.ToString(orderDetails.Length);

                rdExactCopiedYes.Checked = (orderDetails.MakeExactCopies == null || Convert.ToBoolean(!orderDetails.MakeExactCopies)) ? false : true;
                rdExactCopiedNo.Checked = (orderDetails.MakeExactCopies == null || Convert.ToBoolean(orderDetails.MakeExactCopies)) ? false : true;

                if (ddlModelTypes.SelectedValue == Resource.DB_ChainID)
                    txtAdditionalInfoGeneral.Value = orderDetails.AdditionalInfo;
                else if (ddlModelTypes.SelectedValue == Resource.DB_JacketsID)
                {
                    txtAdditionalInfoGeneral.Value = orderDetails.AdditionalInfo;
                    txtHeadSizeGeneral.Text = orderDetails.HeadSize;
                    txtMatchJacketModel.Text = orderDetails.ModelToMatch;
                    rdExistingModelGeneralYes.Checked = (orderDetails.IsExistingModel == null || Convert.ToBoolean(!orderDetails.IsExistingModel)) ? false : true;
                    rdExistingModelGeneralNo.Checked = !rdExistingModelGeneralYes.Checked;
                }
                else if (ddlModelTypes.SelectedValue == Resource.DB_PedantID || ddlModelTypes.SelectedValue == Resource.DB_EarringsID || ddlModelTypes.SelectedValue == Resource.DB_BraceletID
                   || ddlModelTypes.SelectedValue == Resource.DB_NecklaceID)
                {
                    if (ddlModelSubType.SelectedValue == Resource.DB_SubTypeMountID)
                    {
                        txtAdditionalInfoGeneral.Value = orderDetails.AdditionalInfo;
                        txtHeadSizeGeneral.Text = orderDetails.HeadSize;
                        rdExistingModelGeneralYes.Checked = (orderDetails.IsExistingModel == null || Convert.ToBoolean(!orderDetails.IsExistingModel)) ? false : true;
                        rdExistingModelGeneralNo.Checked = !rdExistingModelGeneralYes.Checked;
                    }
                }
                else if (ddlModelTypes.SelectedValue == Resource.DB_RingID)
                {
                    if (orderDetails.FingerSizeID == null)
                    {
                        ddlFingerSize.SelectedValue = Convert.ToString(Common.DROPDOWN_OTHERS_VALUE);
                        txtFingerSizeOther.Text = orderDetails.FingerSizeOther;
                    }
                    else
                        ddlFingerSize.SelectedValue = Convert.ToString(orderDetails.FingerSizeID);
                    if (ddlRingType.SelectedValue == Resource.DB_RingMountID)
                    {
                        rdPFYes.Checked = (orderDetails.IsPF != null && !Convert.ToBoolean(!orderDetails.IsPF));
                        rdPFNo.Checked = !rdPFYes.Checked;
                        txtHeadSizeModel.Text = orderDetails.HeadSize;
                        rdExistingModelYes.Checked = (orderDetails.IsExistingModel != null && !Convert.ToBoolean(!orderDetails.IsExistingModel));
                        rdExistingModelNo.Checked = !rdExistingModelYes.Checked;
                        txtAdditionalInfoModel.Value = orderDetails.AdditionalInfo;
                    }
                    else if (ddlRingType.SelectedValue == Resource.DB_IsMatchingETID)
                    {
                        txtHeadSizeET.Text = orderDetails.HeadSize;
                        txtModelNumberToMatch.Text = orderDetails.ModelToMatch;

                        rdStraight.Checked = (orderDetails.CurveType == Common.CurveType.Straight.ToString()) ? true : false;
                        rdCurve.Checked = (orderDetails.CurveType == Common.CurveType.Curved.ToString()) ? true : false;
                        rdTailor.Checked = (orderDetails.CurveType == Common.CurveType.Tailored.ToString()) ? true : false;

                        rdFollowMountShape.Checked = (orderDetails.TailoredType == Common.TailoredType.FollowsMountShape.ToString()) ? true : false;
                        rdCurveToFit.Checked = (orderDetails.TailoredType == Common.TailoredType.CurveToFit.ToString()) ? true : false;

                        chkFinishAtSomePoint.Checked = (orderDetails.IsFinishAtSomePoint != null && !Convert.ToBoolean(!orderDetails.IsFinishAtSomePoint));
                        txtAdditionalInfoET.Value = orderDetails.AdditionalInfo;
                    }
                }
                ddlTemaMembers.SelectedValue = Convert.ToString(orderDetails.TMUserID);
                txtModelNumber.Text = orderDetails.ModelNumber;
                hdnShippingDate.Text = string.Format("{0:dd MMM yyyy}", orderDetails.ExpectedShippingDate);

                if (orderDetails.IsCADRequested != null)
                {
                    rdCadRequestedYes.Checked = Convert.ToBoolean(orderDetails.IsCADRequested);
                    rdCadRequestedNo.Checked = !Convert.ToBoolean(orderDetails.IsCADRequested);
                }
                if (orderDetails.IsSampleProvided != null)
                {
                    divSampleSerialNumber.Visible = false;
                    rdSampleProvidedYes.Checked = Convert.ToBoolean(orderDetails.IsSampleProvided);
                    rdSampleProvidedYes.Enabled = false;
                    rdSampleProvidedNo.Checked = !rdSampleProvidedYes.Checked;
                    rdSampleProvidedNo.Enabled = false;
                    rdExactCopiedSampleYes.Checked = Convert.ToBoolean(orderDetails.MakeExactCopiesSample);
                    rdExactCopiedSampleNo.Checked = !rdExactCopiedSampleYes.Checked;
                    if (rdSampleProvidedYes.Checked)
                    {
                        lblSampleSerialNumber.Text = orderDetails.SampleSerialNumber;
                        if (ddlNoOfSamples.Items.FindByValue(Convert.ToString(orderDetails.NoOfSamples)) == null)
                        {
                            ddlNoOfSamples.SelectedValue = Convert.ToString(Common.DROPDOWN_OTHERS_VALUE);
                            txtSampleOther.Text = Convert.ToString(orderDetails.NoOfSamples);
                            // NoOfSample = Convert.ToInt16(orderDetails.NoOfSamples);
                        }
                        else
                        {
                            ddlNoOfSamples.SelectedValue = Convert.ToString(orderDetails.NoOfSamples);
                            // NoOfSample = Convert.ToInt16(orderDetails.NoOfSamples);
                        }
                        dicSample.Add("NoOfSample", orderDetails.NoOfSamples.ToString());
                        dicSample.Add("SampleNumbers", orderDetails.SampleSerialNumber);
                        divSampleSerialNumber.Visible = true;
                    }
                }
                if (orderDetails.IsStoneProvided != null)
                {
                    rdStoneProvidedYes.Checked = Convert.ToBoolean(orderDetails.IsStoneProvided);
                    rdStoneProvidedNo.Checked = !rdSampleProvidedYes.Checked;
                    if (rdSampleProvidedYes.Checked)
                    {
                        txtStoneDescription.Text = orderDetails.StoneDescription;
                        txtSettingInstructions.Text = orderDetails.SettingInstructions;
                    }
                }
                List<Specimen> lst = new OrderManager().GetSpecimens(0, orderDetails.OrderID, 0);
                if (lst != null && lst.Count() > 0)
                {
                    OrderID = orderDetails.OrderID;
                    dlImages.Visible = true;
                    BindSpecimenImages(lst);
                }
                specimenFilesUploaded += lst.Count;
                specimenFileUpload.MaximumNumberOfFiles = Convert.ToInt32(Resource.MaxSpecimenUpload) - specimenFilesUploaded;
                SetUploadControl();

                txtRemarks.Value = orderDetails.Remarks;
                fileUploadarea.Visible = false;
                //BindTempImages();
            }


            //// Code to call javascript function from current user control
            //Control Caller = this; //user control
            //string MyScript = "OnCustomerSelected();";
            //ScriptManager.RegisterStartupScript(Caller, Caller.GetType(), "Script Name", MyScript, true);

            // return NoOfSample;
            return dicSample;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewImageInstructions_Click(object sender, EventArgs e)
        {
            //mpSpecimenImages.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        public void BindSpecimenImages(List<Specimen> lst)
        {
            dlImages.Visible = true;
            dlImages.DataSource = lst;
            dlImages.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlImages_RowDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
             e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var imgSpecimen = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSpecimen");
                var imgSpecimenLink = (HyperLink)e.Item.FindControl("imgSpecimenLink");
                var DowmloadImgLink = (Literal)e.Item.FindControl("DowmloadImgLink");

                var orderID = DataBinder.Eval(e.Item.DataItem, "OrderID");
                var ImageLocationURL = DataBinder.Eval(e.Item.DataItem, "ImageLocationURL");

                var PathtoImage = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\" +
                                  orderID + @"\";
                var ImgLink = PathtoImage + ImageLocationURL;

                imgSpecimenLink.NavigateUrl =
                    imgSpecimen.ImageUrl = ImgLink;

                DowmloadImgLink.Text = string.Format("<a href='{0}' download='{1}' title='{1}' class='block'>Download Image</a>", ResolveUrl(ImgLink), ImageLocationURL);


                // LinkButton lnkEditSpecimenImage = (LinkButton)e.Item.FindControl("lnkEditSpecimenImage");
                // lnkEditSpecimenImage.CommandArgument = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\" + orderID + @"\" + ImageLocationURL;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlList"></param>
        /// <param name="dataSource"></param>
        public void FillDropDown(DropDownList ddlList, Array dataSource, string optionalstr = Common.DROPDOWN_SELECT_TEXT, bool addDefault = true)
        {

            ddlList.DataSource = dataSource;
            ddlList.DataBind();

            if (addDefault)
                ddlList.Items.Insert(0, new ListItem(optionalstr, Common.DROPDOWN_SELECT_VALUE));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddlList"></param>
        /// <param name="dataSource"></param>
        public void FillCustomerDropDown(DropDownList ddlList, List<Customer> dataSource, string optionalstr = Common.DROPDOWN_SELECT_TEXT, bool addDefault = true)
        {
            var dataSourceNew = dataSource.Select(c => new { CustomerID = c.CustomerID, CustomerName_Code = c.CustomerName.ToString() + " : " + c.CustomerCode });
            ddlList.DataSource = dataSourceNew;
            ddlList.DataValueField = "CustomerID";
            ddlList.DataTextField = "CustomerName_Code";
            ddlList.DataBind();

            if (addDefault)
                ddlList.Items.Insert(0, new ListItem(optionalstr, Common.DROPDOWN_SELECT_VALUE));
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetValues()
        {
            ddlModelTypes.SelectedIndex = 0;
            ddlPriority.SelectedIndex = 0;
            ddlFingerSize.SelectedIndex = 0;
            ddlMetals.SelectedIndex = 0;
            ddlProcessTypes.SelectedIndex = 0;
            ddlRingType.SelectedIndex = 0;
            ddlCustomers.SelectedIndex = 0;
            hdnShippingDate.Text = "";

            Session[Common.SESSION_SPECIMENIMAGES] = null;

            specimenFileUpload = new AjaxFileUpload();

            lblFiles.InnerText = "";

            //  txtCustomer.Text = "";
            ddlCustomerEmail.SelectedIndex = 0;
            ddlTemaMembers.SelectedIndex = 0;
            ddlAssignee.SelectedIndex = 0;
            ddlQuantity.SelectedIndex = 0;
            ddlLength.SelectedIndex = 0;
            ddlModelSubType.SelectedIndex = 0;
            ddlNoOfSamples.SelectedIndex = 0;
            if (RoleID == Convert.ToInt32(Resource.DB_TMRoleID) || RoleID == Convert.ToInt32(Resource.DB_FactoryRoleID))
            {
                ddlTemaMembers.Visible = false;
                TeamMembersRequired.Enabled = false;
            }
            txtLength.Text = txtMetalOther.Text = txtAdditionalInfoModel.Value = txtAdditionalInfoET.Value = txtAdditionalInfoGeneral.Value =
                 txtHeadSizeGeneral.Text = txtHeadSizeModel.Text = txtHeadSizeET.Text = txtModelNumber.Text = txtMatchJacketModel.Text = txtModelNumberToMatch.Text =
                 txtRemarks.Value = txtStoneDescription.Text = txtSettingInstructions.Text = txtQuantityOther.Text = txtFingerSizeOther.Text = string.Empty;
            MessageUtility.ClearMessages(dvMessage, ltMessage);
        }
        /// <summary>
        /// 
        /// </summary>
        public Order PlaceCADOrder()
        {
            Order order = new Order();
            // string[] customerInfo = txtCustomer.Text.Split(':');
            // Customer objCustomer = new AdminManager().GetCustomers().Where(x => x.CustomerName == customerInfo[0] && x.CustomerCode == customerInfo[1]).First();
            // order.CustomerID = objCustomer.CustomerID;

            order.CustomerID = Convert.ToInt32(ddlCustomers.SelectedValue);

            order.ExpectedShippingDate = Convert.ToDateTime(hdnShippingDate.Text);

            order.ModelTypeID = Convert.ToInt32(ddlModelTypes.SelectedValue);
            order.ModelNumber = txtModelNumber.Text;
            order.ProcessTypeID = Convert.ToInt32(ddlProcessTypes.SelectedValue);
            order.PriorityID = Convert.ToInt16(ddlPriority.SelectedValue);

            if (ddlMetals.SelectedValue == Common.DROPDOWN_OTHERS_VALUE)
                order.MetalOther = txtMetalOther.Text;
            else
                order.MetalID = Convert.ToInt32(ddlMetals.SelectedValue);

            if (ddlQuantity.SelectedValue == Common.DROPDOWN_OTHERS_VALUE)
                order.QuantityOther = txtQuantityOther.Text;
            else
                order.Quantity = ddlQuantity.SelectedValue;

            if (rdExactCopiedYes.Checked)
                order.MakeExactCopies = true;
            else
                order.MakeExactCopies = false;

            if (ddlModelTypes.SelectedValue == Resource.DB_RingID)
            {
                if (ddlFingerSize.SelectedValue == Common.DROPDOWN_OTHERS_VALUE)
                    order.FingerSizeOther = txtFingerSizeOther.Text;
                else
                    order.FingerSizeID = Convert.ToInt32(ddlFingerSize.SelectedValue);

                order.RingTypeID = Convert.ToInt16(ddlRingType.SelectedValue);
                if (ddlRingType.SelectedValue == Resource.DB_RingMountID)
                {
                    order.IsExistingModel = rdExistingModelYes.Checked;
                    order.IsPF = rdPFYes.Checked;
                    order.HeadSize = txtHeadSizeModel.Text;
                    order.AdditionalInfo = txtAdditionalInfoModel.Value;
                }
                if (ddlRingType.SelectedValue == Resource.DB_IsMatchingETID)
                {
                    order.HeadSize = txtHeadSizeET.Text;
                    order.ModelToMatch = txtModelNumberToMatch.Text;
                    if (rdStraight.Checked)
                        order.CurveType = Common.CurveType.Straight.ToString();
                    else if (rdCurve.Checked)
                        order.CurveType = Common.CurveType.Curved.ToString();
                    else if (rdTailor.Checked)
                    {
                        order.CurveType = Common.CurveType.Tailored.ToString();
                        if (rdFollowMountShape.Checked)
                            order.TailoredType = Common.TailoredType.FollowsMountShape.ToString();
                        else if (rdCurveToFit.Checked)
                            order.TailoredType = Common.TailoredType.CurveToFit.ToString();
                    }
                    order.IsFinishAtSomePoint = chkFinishAtSomePoint.Checked;
                    order.AdditionalInfo = txtAdditionalInfoET.Value;
                }
            }
            else if (ddlModelTypes.SelectedValue == Resource.DB_ChainID)
            {
                order.AdditionalInfo = txtAdditionalInfoGeneral.Value;
                order.Length = Convert.ToDecimal(txtLength.Text);
                order.LengthMeasurement = ddlLength.SelectedItem.Text;
            }
            else if (ddlModelTypes.SelectedValue == Resource.DB_JacketsID)
            {
                order.AdditionalInfo = txtAdditionalInfoGeneral.Value;
                order.HeadSize = txtHeadSizeGeneral.Text;
                order.ModelToMatch = txtMatchJacketModel.Text;
                order.IsExistingModel = rdExistingModelGeneralYes.Checked;
            }
            else if (ddlModelTypes.SelectedValue == Resource.DB_PedantID || ddlModelTypes.SelectedValue == Resource.DB_EarringsID || ddlModelTypes.SelectedValue == Resource.DB_BraceletID
               || ddlModelTypes.SelectedValue == Resource.DB_NecklaceID)
            {
                order.ModelSubTypeID = Convert.ToInt32(ddlModelSubType.SelectedValue);
                if (ddlModelSubType.SelectedValue == Resource.DB_SubTypeMountID)
                {
                    order.AdditionalInfo = txtAdditionalInfoGeneral.Value;
                    order.HeadSize = txtHeadSizeGeneral.Text;
                    order.IsExistingModel = rdExistingModelGeneralYes.Checked;
                }
                if (ddlModelTypes.SelectedValue == Resource.DB_BraceletID || ddlModelTypes.SelectedValue == Resource.DB_NecklaceID)
                {
                    order.Length = Convert.ToDecimal(txtLength.Text);
                    order.LengthMeasurement = ddlLength.SelectedItem.Text;
                }
            }
            order.IsCADRequested = rdCadRequestedYes.Checked;

            if (rdSampleProvidedYes.Checked)
            {
                order.IsSampleProvided = true;
                order.MakeExactCopiesSample = rdExactCopiedSampleYes.Checked;
                if (ddlNoOfSamples.SelectedValue == Common.DROPDOWN_OTHERS_VALUE)
                    order.NoOfSamples = Convert.ToByte(txtSampleOther.Text);
                else
                    order.NoOfSamples = Convert.ToByte(ddlNoOfSamples.SelectedValue);
            }
            else
                order.IsSampleProvided = false;

            if (rdStoneProvidedYes.Checked)
            {
                order.IsStoneProvided = rdStoneProvidedYes.Checked;
                order.StoneDescription = txtStoneDescription.Text;
                order.SettingInstructions = txtSettingInstructions.Text;
            }

            if (RoleID == Convert.ToInt32(Resource.DB_TMRoleID))
                order.TMUserID = UserID;
            else if (RoleID == Convert.ToInt32(Resource.DB_AdminRoleID))
            {
                if (ddlTemaMembers.SelectedIndex != 0)
                    order.TMUserID = Convert.ToInt32(ddlTemaMembers.SelectedValue);
            }

            order.AssignedTo = Convert.ToInt32(ddlAssignee.SelectedValue);
            order.UserID = UserID;
            order.OrderStatusID = Convert.ToInt32(Resource.DB_Status_Initiated);
            //uploadFirstAlternative();
            List<Specimen> lst = Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>;
            if (lst != null)
            {
                XmlDocument xml = new XmlDocument();
                XmlElement root = xml.CreateElement("Specimen");
                xml.AppendChild(root);

                for (int i = 0; i <= lst.Count - 1; i++)
                {
                    XmlElement child = xml.CreateElement("SpecimenImage");
                    child.SetAttribute("LocationURL", lst[i].ImageLocationURL);
                    root.AppendChild(child);
                }

                order.SpecimenData = xml.OuterXml;
            }
            order.Remarks = txtRemarks.Value;
            message = string.Empty;
            lblFiles.InnerHtml = message;
            // Session[Common.SESSION_SPECIMENIMAGES] = null;
            return order;
        }
        /////// <summary>
        /////// 
        /////// </summary>
        /////// <param name="sender"></param>
        /////// <param name="e"></param>
        //protected void ImgProfie_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        //{
        //    try
        //    {
        //        if (imgSpecimen.PostedFile != null)
        //        {
        //            string fileName = imgSpecimen.PostedFile.FileName;
        //            int contentLength = imgSpecimen.PostedFile.ContentLength;

        //            List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

        //            IsValidFile = validFileExtensions.Any(x => x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 && contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

        //            if (!IsValidFile)
        //                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
        //            else
        //            {
        //                Image = new byte[imgSpecimen.PostedFile.ContentLength];
        //                HttpPostedFile UploadedImage = imgSpecimen.PostedFile;
        //                UploadedImage.InputStream.Read(Image, 0, (int)imgSpecimen.PostedFile.ContentLength);
        //                //Session[Common.SESSION_CADIMAGE] = Image;
        //                //picSpecimen.Visible = true;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void File_Upload(object sender, AjaxFileUploadEventArgs e)
        {

            try
            {

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
                    string tempURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\Temp";

                    bool exists = Directory.Exists(Server.MapPath(tempURL));

                    if (!exists)
                        Directory.CreateDirectory(Server.MapPath(tempURL));

                    specimenFileUpload.SaveAs(Server.MapPath(tempURL + @"\" + fileName));

                    //EditedImageUrl = tempURL + @"\" + fileName;
                    //ppt.LoadImageFromFileSystem(EditedImageUrl, new FixedCropConstraint(200, 200));
                    //ppt.OpenPopup(600, 600);

                    Specimen specimen = new Specimen();
                    specimen.ImageLocationURL = fileName;
                    //specimen.ImageFile = e.FileName;
                    if (Session[Common.SESSION_SPECIMENIMAGES] == null)
                        Session[Common.SESSION_SPECIMENIMAGES] = new List<Specimen>();

                    (Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>).Add(specimen);

                    specimenFilesUploaded += 1;
                    SetUploadControl();

                    //BindTempImages();

                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Function for getting temp folder uploaded imagees and bind to dlTempImages
        /// </summary>
        //private void BindTempImages()
        //{
        //    if (Session[Common.SESSION_SPECIMENIMAGES] != null)
        //    {
        //        dtTempImages.Clear();

        //        if (dtTempImages.Columns.Count == 0)
        //        {
        //            dtTempImages.Columns.Add("ImageLocationURL", typeof(string));
        //        }

        //        List<Specimen> lstTempSpecimen = new List<Specimen>();
        //        lstTempSpecimen = (List<Specimen>)Session[Common.SESSION_SPECIMENIMAGES];

        //        foreach (Specimen item in lstTempSpecimen)
        //        {
        //            DataRow dr=dtTempImages.NewRow();
        //            dr["ImageLocationURL"]=item.ImageLocationURL;
        //            dtTempImages.Rows.Add(dr);
        //        }

        //        if (dtTempImages.Rows.Count > 0)
        //        {
        //            dlTempImages.Visible = true;
        //            dlTempImages.DataSource = dtTempImages;
        //            dlTempImages.DataBind();

        //        }

        //    }




        protected void Button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    HttpFileCollection hfc = Request.Files;
            //    for (int i = 0; i < hfc.Count; i++)
            //    {
            //        HttpPostedFile hpf = hfc[i];
            //        if (hpf.ContentLength > 0)
            //        {
            //            hpf.SaveAs(Server.MapPath("~/UploadedExcel/") + System.IO.Path.GetFileName(hpf.FileName));
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }

        public void uploadFirstAlternative()
        {

            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    //hpf.SaveAs(Server.MapPath("~/UploadedExcel/") + System.IO.Path.GetFileName(hpf.FileName));
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
                        string tempURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\Temp";

                        bool exists = Directory.Exists(Server.MapPath(tempURL));

                        if (!exists)
                            Directory.CreateDirectory(Server.MapPath(tempURL));

                        hpf.SaveAs(Server.MapPath(tempURL + @"\" + fileName));
                        Specimen specimen = new Specimen();
                        specimen.ImageLocationURL = fileName;
                        if (Session[Common.SESSION_SPECIMENIMAGES] == null)
                            Session[Common.SESSION_SPECIMENIMAGES] = new List<Specimen>();

                        (Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>).Add(specimen);

                        specimenFilesUploaded += 1;
                        //SetUploadControl();

                    }
                }
            }
        }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            HttpFileCollection fileCollection = Request.Files;
            if (fileCollection.Count <= specimenFilesUploaded)
            {
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile uploadfile = fileCollection[i];
                    message = message + uploadfile.FileName + " uploaded successfully" + "<br />";
                    try
                    {
                        string fileName = string.Concat(Path.GetFileNameWithoutExtension(uploadfile.FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                               Path.GetExtension(uploadfile.FileName));//Path.GetFileName(uploadfile.FileName);
                        int contentLength = uploadfile.ContentLength;

                        List<String> validFileExtensions = new List<String>(Resource.ValidProfileImageExtensions.Split(','));

                        IsValidFile = validFileExtensions.Any(x => x.ToLower().IndexOf(Path.GetExtension(fileName).ToLower()) > 0 && contentLength < Convert.ToInt32(Resource.ValidImageFileSize));

                        if (!IsValidFile)
                            MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_InvalidImage);
                        else
                        {
                            string tempURL = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\Temp";

                            bool exists = System.IO.Directory.Exists(Server.MapPath(tempURL));

                            if (!exists)
                                System.IO.Directory.CreateDirectory(Server.MapPath(tempURL));

                            uploadfile.SaveAs(Server.MapPath(tempURL + @"\" + fileName));

                            //string filename = e.FileName;
                            Specimen specimen = new Specimen();
                            specimen.ImageLocationURL = fileName;
                            if (Session[Common.SESSION_SPECIMENIMAGES] == null)
                                Session[Common.SESSION_SPECIMENIMAGES] = new List<Specimen>();
                            (Session[Common.SESSION_SPECIMENIMAGES] as List<Specimen>).Add(specimen);
                            specimenFilesUploaded = specimenFilesUploaded + 1;
                            SetUploadControl();
                        }
                    }

                    catch (Exception)
                    { }

                }
            }
            else
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.MaxSpecimenUpload);
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetUploadControl()
        {
            if (specimenFilesUploaded < Convert.ToInt32(Resource.MaxSpecimenUpload))
                hd.Value = "0";
            else
                hd.Value = "1";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCAD_Click(object sender, EventArgs e)
        {
            SetUploadControl();
            lblFiles.InnerHtml = message;
            lblFiles.Visible = true;
            //updatePanelAttachments.Update();
        }

        //protected void btnTest_Click(object sender, EventArgs e)
        //{
        //    ppt.LoadImageFromFileSystem("~/Dk20150305112626975.png", new FixedCropConstraint(100, 100));
        //    ppt.OpenPopup(500, 500);
        //}

        //protected void PopupPictureTrimmer_PopupClose(object sender, PictureTrimmerPopupCloseEventArgs e)
        //{
        //    if (e.SaveChanges)
        //    {
        //        ppt.SaveProcessedImageToFileSystem(EditedImageUrl);
        //        if (!IsNewImage)
        //        {
        //            List<Specimen> lst = new OrderManager().GetSpecimens(0, OrderID, 0);
        //            if (lst != null && lst.Count() > 0)
        //            {
        //                dlImages.Visible = true;
        //                BindSpecimenImages(lst);
        //            }
        //        }

        //    }

        //}

        //protected void dlImages_ItemCommand(object source, DataListCommandEventArgs e)
        //{
        //    if (e.CommandName == "EditImage")
        //    {
        //        EditedImageUrl = e.CommandArgument.ToString();
        //        ppt.LoadImageFromFileSystem(e.CommandArgument.ToString(), new FixedCropConstraint(200, 200));
        //        ppt.OpenPopup(600, 600);
        //    }
        //}

        //protected void dlTempImages_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item ||
        //    e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        System.Web.UI.WebControls.Image imgSpecimen = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSpecimen");

        //        var ImageLocationURL = DataBinder.Eval(e.Item.DataItem, "ImageLocationURL");
        //        imgSpecimen.ImageUrl = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\" + "Temp" + @"\" + ImageLocationURL;
        //        LinkButton lnkEditSpecimenImage = (LinkButton)e.Item.FindControl("lnkEditSpecimenImage");
        //        lnkEditSpecimenImage.CommandArgument = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\" + "Temp" + @"\" + ImageLocationURL;

        //    }

        //}

        //protected void dlTempImages_ItemCommand(object source, DataListCommandEventArgs e)
        //{
        //    if (e.CommandName == "EditImage")
        //    {
        //        EditedImageUrl = e.CommandArgument.ToString();
        //        ppt.LoadImageFromFileSystem(e.CommandArgument.ToString(), new FixedCropConstraint(200, 200));
        //        ppt.OpenPopup(600, 600);
        //    }

        //}
    }
}