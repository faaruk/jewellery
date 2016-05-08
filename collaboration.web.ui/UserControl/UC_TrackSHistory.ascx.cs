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
using Collaboration.Web.UI.Utilities;
using System.Configuration;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;

namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_TrackSHistory : System.Web.UI.UserControl
    {

        private static string message = string.Empty;

        static int _userID = 0;
        static int _sampleID = 0;
        static string _fileName = string.Empty;
        static int _sampleTrackID = 0;
        static string _sampleSerialNumber = string.Empty;
        static String AttachmentLocation = string.Empty;
        static String ContentType = string.Empty;
        static bool IsValidFile = true;
        // Delegate declaration

        public delegate void OnButtonClick(string strValue);

        // Event declaration

        public string fileName = "";

        public event OnButtonClick btnHandler;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int SampleID { set { _sampleID = value; } get { return _sampleID; } }
        public int SampleTrackID { set { _sampleTrackID = value; } get { return _sampleTrackID; } }
        public string SampleSerialNumber { set { _sampleSerialNumber = value; } get { return _sampleSerialNumber; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// 
        /// </summary>
        public void FillInfo()
        {
            ResetValues();
            BindGrid();
        }
        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            IEnumerable<SamplesTrackingHistory_Result> samplesTrackingHistory_Result = new SampleHistoryManager().GetHistory(SampleID, 0, UserID);
            Session[Collaboration.Web.UI.Common.SESSION_MESSAGES] = samplesTrackingHistory_Result;

            if (samplesTrackingHistory_Result.Count() == 0)
            {
                NoMessagePlaceHolder.Visible = true;
            }
            gvTable.DataSource = samplesTrackingHistory_Result;
            gvTable.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetValues()
        {
            //txtMessage.Value = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {

        }



    }
}