using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Collaboration.Web.UI;
namespace Collaboration.Web.UI.Utilities
{
    public static class MessageUtility
    {
        public static void ShowMessage(HtmlGenericControl Div, Literal ltControl, int MessageType, string Message)
        {
            if (MessageType == Convert.ToInt16(Common.MessageTypes.Success))
            {
                Div.Style.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
                Div.Attributes[Common.UIAttributes.ATTRIBUTE_CLASS] = Resource.UI_SuccessMessageClass;
                ltControl.Text = Message;
                
            }
            else if (MessageType == Convert.ToInt16(Common.MessageTypes.Error))
            {
                Div.Style.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
                Div.Attributes[Common.UIAttributes.ATTRIBUTE_CLASS] = Resource.UI_FaillureMessageClass;
                ltControl.Text = Message;
            }
            else if (MessageType == Convert.ToInt16(Common.MessageTypes.Warning))
            {
                Div.Style.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
                Div.Attributes[Common.UIAttributes.ATTRIBUTE_CLASS] = Resource.UI_WarningMessageClass;
                ltControl.Text = Message;
            }
            else {
                Div.Style.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_BLOCK);
                Div.Attributes[Common.UIAttributes.ATTRIBUTE_CLASS] = Resource.UI_WarningMessageClass;
                ltControl.Text = Message;
            }
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Div"></param>
        /// <param name="ltControl"></param>
        /// <param name="MessageType"></param>
        /// <param name="Message"></param>
        public static void ClearMessages(HtmlGenericControl Div, Literal ltControl)
        {
            Div.Style.Add(Common.UIAttributes.ATTRIBUTE_DISPLAY, Common.UIAttributes.DISPLAY_NONE);
            ltControl.Text = string.Empty;
        }
    }
}