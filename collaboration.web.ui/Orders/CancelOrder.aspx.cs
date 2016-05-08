using System;


namespace Collaboration.Web.UI.Orders
{
    public partial class CancelOrders : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Collaboration.Business.Components.AdminManager();
            gvTable.FilterExpression = "OrderStatusID < 9";
        }
    }
}