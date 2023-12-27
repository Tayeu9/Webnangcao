using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOPHVT.Helpers
{
    public static class NavBarHelper
    {
        public static string IsActive(this HtmlHelper html, string action = null, string controller = null)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            //System.Diagnostics.Debug.WriteLine($"Input active class: {controller} | {action}");
            //System.Diagnostics.Debug.WriteLine($"From active class: {currentController} | {currentAction}");

            if (string.IsNullOrEmpty(controller))
            {
                controller = currentController;
            }
            if (string.IsNullOrEmpty(action))
            {
                action = currentAction;
            }

            if (controller == currentController && action == currentAction)
            {
                return "active";
            }
            return string.Empty;
        }
    }
}