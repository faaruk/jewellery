using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Collaboration.Business.Entities;
using Collaboration.Web.UI.Utilities;
using System.Web.Security;
using Collaboration.Business.Components;
namespace Collaboration.Web.UI
{
    public partial class CommonMaster : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        } 
    }
}