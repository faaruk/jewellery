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
using System.Configuration;



using System.Linq;
namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_CompareCADForCustomer : System.Web.UI.UserControl
    {
        private static int _userID = 0;
        private static int _roleID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>
        public void SetValues(OrderDetails_Result orderDetails, int CADID, bool hasResponseSent)
        {
            if (hasResponseSent == true)
            {
                divInfo.Visible = false;
            }
            else { divInfo.Visible = true; }

            List<OrdersCAD> lstOrderCADs = new OrderManager().GetOrderCADsTop2(orderDetails.OrderID, CADID);
            if (lstOrderCADs != null && lstOrderCADs.Count > 0)
            {
                gvTable.DataSource = lstOrderCADs.Take(1).OrderByDescending(x => x.CADID);
                gvTable.DataBind();
              
            }


            // Bind order details

            lblModelType.Text = orderDetails.ModelCode;
            lblAssigneeUserName.Text = orderDetails.AssignedToUserName;
            lblCustomerName.Text = orderDetails.CustomerName;
            lblCustomerEMail.Text = orderDetails.CustomerEmail;
            lblExpectedShippingDate.Text = Convert.ToDateTime(orderDetails.ExpectedShippingDate).ToLongDateString();
            //lblSerialNumber.Text = orderDetails.SerialNumber;
            lblModelNumber.Text = orderDetails.ModelNumber;
            lblQuantity.Text = orderDetails.Quantity == "1P" ? Common.EnumText(Common.Quantity.OnePieceOnly) : orderDetails.Quantity;
            lblMetal.Text = orderDetails.MetalName;

            if (orderDetails.MetalOther != null)
            {
                lblMetalOtherText.Visible = true;
                lblMetalOther.Visible = true;
                lblMetalOther.Text = ": " + orderDetails.MetalOther;
                divMetalother.Visible = true;
            }
            else
            {
                lblMetalOtherText.Visible = false;
                lblMetalOther.Visible = false;
                divMetalother.Visible = false;
            }

            lblPriority.Text = orderDetails.Name;
            lblProcessType.Text = orderDetails.ProcessType;

            if (orderDetails.QuantityOther != null)
            {
                lblQuantityOtherText.Visible = true;
                lblQuantityOther.Visible = true;
                lblQuantityOther.Text = ": " + orderDetails.QuantityOther;
                divQuantityOther.Visible = true;

            }
            else
            {
                lblQuantityOtherText.Visible = false;
                lblQuantityOther.Visible = false;
                divQuantityOther.Visible = false;
            }


            lblMakeExactCopies.Text = orderDetails.MakeExactCopies != null && (Convert.ToBoolean(orderDetails.MakeExactCopies)) ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);

            if (orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_ChainID))
            {
                divGeneral.Visible = divLength.Visible = true;
                divModelSubType.Visible = false;
                divModelSubType.Visible = divHeadsize.Visible = divMatchJacketModel.Visible = false;
                lblAdditionalInfoGeneral.Text = orderDetails.AdditionalInfo;
                lblLength.Text = orderDetails.Length + " " + orderDetails.LengthMeasurement;
            }
            else if (orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_JacketsID))
            {
                divGeneral.Visible = true;
                divModelSubType.Visible = false;
                lblAdditionalInfoGeneral.Text = orderDetails.AdditionalInfo;
                lblHeadSizeGeneral.Text = orderDetails.HeadSize;
                lblMatchJacketModel.Text = orderDetails.ModelToMatch;
                lblIsExistingGeneral.Text = orderDetails.IsExistingModel != null && Convert.ToBoolean(orderDetails.IsExistingModel) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
            }
            else if (orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_PedantID) || orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_EarringsID)
                || orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_BraceletID) || orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_NecklaceID))
            {
                lblModelSubTypeText.Text = Common.GetModelTypeName(orderDetails.ModelTypeID);
                lblModelSubTypeValue.Text = Common.GetSubType(orderDetails.ModelSubTypeID);
                divModelSubType.Visible = divGeneral.Visible = true;
                lblAdditionalInfoGeneral.Text = orderDetails.AdditionalInfo;
                lblHeadSizeGeneral.Text = orderDetails.HeadSize;

                lblIsExistingGeneral.Text = orderDetails.IsExistingModel != null && Convert.ToBoolean(orderDetails.IsExistingModel) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);

                if (orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_BraceletID) || orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_NecklaceID))
                {
                    lblLength.Text = orderDetails.Length + " " + orderDetails.LengthMeasurement;
                    divLength.Visible = true;
                }
            }
            else if (orderDetails.ModelTypeID == Convert.ToInt32(Resource.DB_RingID))
            {
                lblFingerSize.Text = orderDetails.Size;
                divFingerSize.Visible = true;
                lblFingerSizeOther.Text = orderDetails.FingerSizeOther;
                if (orderDetails.Size == null)
                {
                    divFingerSizeLabel.Visible = false;
                }
                if (orderDetails.FingerSizeOther == null)
                {
                    divFingerSizeOtherLabel.Visible = false;
                }

                lblRingType.Text = orderDetails.RingType;
                divRingType.Visible = true;
                if (orderDetails.RingTypeID == Convert.ToInt32(Resource.DB_RingMountID))
                {
                    divRingMountType.Visible = true;
                    lblIsPF.Text = orderDetails.IsPF != null && Convert.ToBoolean(orderDetails.IsPF) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
                    lblHeadSizeModel.Text = orderDetails.HeadSize;
                    lblIsExisting.Text = orderDetails.IsExistingModel != null && Convert.ToBoolean(orderDetails.IsExistingModel) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
                    lblAdditionalInfoMount.Text = orderDetails.AdditionalInfo;
                }
                else if (orderDetails.RingTypeID == Convert.ToInt32(Resource.DB_IsMatchingETID))
                {
                    divMatchingETType.Visible = true;
                    lblHeadSizET.Text = orderDetails.HeadSize;
                    lblMatchModelNumber.Text = orderDetails.ModelToMatch;
                    if (orderDetails.CurveType == Common.CurveType.Straight.ToString())
                        lblCurveType.Text = Common.CurveType.Straight.ToString();
                    else if (orderDetails.CurveType == Common.CurveType.Curved.ToString())
                        lblCurveType.Text = Common.CurveType.Curved.ToString();
                    else if (orderDetails.CurveType == Common.CurveType.Tailored.ToString())
                    {
                        lblCurveType.Text = Common.CurveType.Tailored.ToString();
                        if (orderDetails.TailoredType == Common.TailoredType.FollowsMountShape.ToString())
                            lblTailoredType.Text = Common.TailoredType.FollowsMountShape.EnumText();
                        else if (orderDetails.TailoredType == Common.TailoredType.CurveToFit.ToString())
                            lblTailoredType.Text = Common.TailoredType.CurveToFit.EnumText();
                    }
                    lblFinishAtSomePoint.Text = orderDetails.IsFinishAtSomePoint != null && Convert.ToBoolean(orderDetails.IsFinishAtSomePoint) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
                    lblAdditionalInfoET.Text = orderDetails.AdditionalInfo;
                }
            }
            List<Specimen> lst = new OrderManager().GetSpecimens(0, orderDetails.OrderID, 0);
            if (lst != null && lst.Count() > 0)
            {
                dlImages.Visible = true;
                BindSpecimenImages(lst);
            }

            lblCADRequested.Text = orderDetails.IsCADRequested != null && Convert.ToBoolean(orderDetails.IsCADRequested) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
            lblSampleProvided.Text = orderDetails.IsSampleProvided != null && Convert.ToBoolean(orderDetails.IsSampleProvided) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
            if (orderDetails.IsSampleProvided != null && Convert.ToBoolean(orderDetails.IsSampleProvided))
            {
                lblNoOfSamples.Text = Convert.ToString(orderDetails.NoOfSamples);
                lblSampleSerialNumber.Text = orderDetails.SampleSerialNumber;
                lblMakeExistingModelSample.Text = orderDetails.MakeExactCopiesSample != null && Convert.ToBoolean(orderDetails.MakeExactCopiesSample) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
                divSampleSerialNumber.Visible = true;
            }
            lblStoneProvided.Text = orderDetails.IsStoneProvided != null && Convert.ToBoolean(orderDetails.IsStoneProvided) == true ? Convert.ToString(Common.Response.Yes) : Convert.ToString(Common.Response.No);
            if (orderDetails.IsStoneProvided != null && Convert.ToBoolean(orderDetails.IsStoneProvided))
            {
                lblStoneDescription.Text = orderDetails.StoneDescription;
                lblSettingInstructions.Text = orderDetails.SettingInstructions;
                divStoneProvided.Visible = true;
            }

            lblTMUserName.Text = orderDetails.TMUserName;
            lblAssigneeUserName.Text = orderDetails.AssignedToUserName;
            lblRemarks.Text = orderDetails.Remarks;
        }


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
                System.Web.UI.WebControls.Image imgSpecimen = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSpecimen");
                var orderID = DataBinder.Eval(e.Item.DataItem, "OrderID");
                var ImageLocationURL = DataBinder.Eval(e.Item.DataItem, "ImageLocationURL");
                imgSpecimen.ImageUrl = ConfigurationManager.AppSettings[Common.APPSETTINGS_SPECIMENIMAGESURL] + @"\" + orderID + @"\" + ImageLocationURL;

            }
        }
    }
}