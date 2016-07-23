using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Collaboration.Business.Entities;
using Collaboration.Business.Components;
using Collaboration.Web.UI.Utilities;
using System.Collections.Specialized;



using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Collaboration.Web.UI.Orders
{
    public partial class ConfirmCAD : System.Web.UI.Page
    {
        static OrderDetails_Result orderDetails;
        public int OrderID;
        public int CADID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //int.TryParse(Request.QueryString[Common.EntityAttributes.ORDERID].DecryptMAC(Resource.EncryptPassphrase), out OrderID);
                //int.TryParse(Request.QueryString[Common.EntityAttributes.CADID].DecryptMAC(Resource.EncryptPassphrase), out CADID);

                //int.TryParse(Request.QueryString[Common.EntityAttributes.ORDERID], out OrderID);

                int.TryParse(ExtensionsNew.Decrypt(Request.QueryString[Common.EntityAttributes.ORDERID], Resource.EncryptPassphrase), out OrderID);
                int.TryParse(ExtensionsNew.Decrypt(Request.QueryString[Common.EntityAttributes.CADID], Resource.EncryptPassphrase), out CADID);
                if (OrderID == 0 || CADID == 0)
                {
                    Response.Redirect("~/AccessDenied.html");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/AccessDenied.html");
            }


            if (!IsPostBack)
                GetOrderDetails();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //OrdersCAD CAD = new OrderManager().GetOrderCADs(OrderID, CADID).SingleOrDefault();
            //string path;
            //path=CAD.CADLocationURL.Replace("\\","/");
            //Response.Clear();
            //Response.ContentType = "image/JPG";
            //Response.AddHeader("Content-Disposition", "attachment; filename=downloadName.jpg");
            //Response.WriteFile(Server.MapPath(path));
            //Response.End();
            string sFile = Image1.ImageUrl;
            if (string.IsNullOrEmpty(sFile))
            {
                return;
            }
            FileInfo fi = new FileInfo(Server.MapPath(sFile)); // error popup here
            if (!fi.Exists)
            {
                return;
            }
            if (!string.IsNullOrEmpty(sFile))
            {
                // check if the file is an image
                NameValueCollection imageExtensions = new NameValueCollection();
                imageExtensions.Add(".jpg", "image/jpeg");
                imageExtensions.Add(".gif", "image/gif");
                imageExtensions.Add(".png", "image/png");
                if (imageExtensions.AllKeys.Contains(fi.Extension))
                {
                    Response.ContentType = imageExtensions.Get(fi.Extension);
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                    Response.TransmitFile(fi.FullName);
                    //Response.WriteFile(fi.FullName);
                    Response.End();
                }

                Response.Redirect(sFile);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        private void GetOrderDetails()
        {

            OrdersCAD CAD = new OrderManager().GetOrderCADs(OrderID, CADID).SingleOrDefault();
            Image1.ImageUrl = CAD.CADLocationURL.ToString();
            
            orderDetails = new OrderManager().GetOrderDetails(OrderID, 0);
            lblSerialNumber.Text = orderDetails.SerialNumber;
            if (orderDetails == null)
                Response.Redirect("~/AccessDenied.html");
            else if (CAD.IsApproved == null)
            {

                if (CAD.ChangeInstructionsCustomer != null)
                {
                    CompareCAD.SetValues(orderDetails, CADID, true);
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.InfoResponseRegistered);
                    btnApproveCAD.Visible = false;
                    btnChangeRequest.Visible = false;
                }
                else { CompareCAD.SetValues(orderDetails, CADID, false); }
            }
                
        else if (CAD.IsApproved == false)
            {
                CompareCAD.SetValues(orderDetails, CADID, true);
                //MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.InfoChangeRequestbyTeam);
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), "This CAD has already been reviewed, please contact River Mounts for further details.");
                btnApproveCAD.Visible = false;
                btnChangeRequest.Visible = false;
            }
            else
            {
                CompareCAD.SetValues(orderDetails, CADID, true);
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.InfoResponseRegistered);
                btnApproveCAD.Visible = false;
                btnChangeRequest.Visible = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            OrdersCAD orderCAD = new OrdersCAD();
            orderCAD.CADID = CADID;
            orderCAD.ChangeInstructions = "";
            orderCAD.IsApproved = true;
            order.OrderID = orderDetails.OrderID;
            order.OrderStatusID = Convert.ToInt32(Resource.DB_Status_CADConfirmed);
            order.UpdateCADInfo = true;
            order.OrdersCADs.Add(orderCAD);
            if (new OrderManager().ChangeOrderStatus(order))
            {
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_ResponseSent);
                string errorMessage;
                IEnumerable<OrderParticipants_Result> users = new OrderManager().GetOrderParticipants(order.OrderID);
                string toList = users.Select(s => s.EMail).Aggregate((current, next) => current + ", " + next);
                if (!MailUtility.SendCADResposeMail(orderDetails.TMEmail, "", Convert.ToString(Common.MailType.CADResponse), orderDetails.TMUserName, orderDetails.SerialNumber, "Yes", out errorMessage))
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                btnApproveCAD.Visible = false;
                btnChangeRequest.Visible = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangeRequest_Click(object sender, EventArgs e)
        {
            mpAddCR.Show();
            updAdd.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            mpAddCR.Show();
            Order order = new Order();
            OrdersCAD orderCAD = new OrdersCAD();
            orderCAD.CADID = CADID;
            //orderCAD.ChangeInstructions = txtChangeInstructions.Value;
            orderCAD.ChangeInstructionsCustomer = txtChangeInstructions.Value;
            orderCAD.IsApproved = null;
            orderCAD.IsUpdatedByCustomer = true;
            order.OrderID = orderDetails.OrderID;
            order.OrderStatusID = Convert.ToInt32(Resource.DB_Status_PendingTMReview);
            order.UpdateCADInfo = true;
            order.OrdersCADs.Add(orderCAD);
            if (new OrderManager().ChangeOrderStatus(order))
            {
                MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Success), Resource.Info_ResponseSent);
                btnApproveCAD.Visible = false;
                btnChangeRequest.Visible = false;
                string errorMessage;
                string strresponse = txtChangeInstructions.Value;
                strresponse = "<b>No.<b> " + "<br /> <br />" + strresponse;
                IEnumerable<OrderParticipants_Result> users = new OrderManager().GetOrderParticipants(order.OrderID);
                string toList = users.Select(s => s.EMail).Aggregate((current, next) => current + ", " + next);
                if (!MailUtility.SendCADResposeMail(orderDetails.TMEmail, "", Convert.ToString(Common.MailType.CADResponse), orderDetails.TMUserName, orderDetails.SerialNumber, strresponse, out errorMessage))
                    MessageUtility.ShowMessage(dvMessage, ltMessage, Convert.ToInt16(Common.MessageTypes.Error), Resource.Err_General);
                mpAddCR.Hide();
                updAdd.Update();
            }
        }
    }
}