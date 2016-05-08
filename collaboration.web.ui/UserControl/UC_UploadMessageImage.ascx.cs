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
using System.Configuration;
using AjaxControlToolkit;

namespace Collaboration.Web.UI.UserControl
{
    public partial class UC_UploadMessageImage : System.Web.UI.UserControl
    {
        private static int _userID = 0;
        private static int _roleID = 0;
        public int UserID { set { _userID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).UserID; } }
        public int RoleID { set { _roleID = value; } get { return (Session[Collaboration.Web.UI.Common.SESSION_USER] as User).RoleID; } }
        private static bool _isValidFile = true;
        public bool IsValidFile { set { _isValidFile = value; } get { return _isValidFile; } }
        private static int specimenFilesUploaded = 0;
        private static string message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        
    }
}