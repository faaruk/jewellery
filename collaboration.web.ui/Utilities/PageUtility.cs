using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
namespace Collaboration.Web.UI.Utilities
{
    public class PageUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public static void SetClass(HtmlAnchor htmlAnchor, HtmlGenericControl lnkControl)
        {
            htmlAnchor.Attributes.Add(Common.UIAttributes.ATTRIBUTE_CLASS, Resource.UI_Active);
            lnkControl.Attributes.Add(Common.UIAttributes.ATTRIBUTE_CLASS, Resource.UI_Active);
        }
        /// <summary>
        /// 
        /// </summary>
        public static void SetClass(HtmlAnchor htmlAnchor)
        {
            htmlAnchor.Attributes.Add(Common.UIAttributes.ATTRIBUTE_CLASS, Resource.UI_Active);
        }
    }
}