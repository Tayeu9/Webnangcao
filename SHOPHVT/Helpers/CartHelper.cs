using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOPHVT.Helpers
{
    public static class CartHelper
    {
        private static string CartItemCount = "CartItemCount";

        public static string GetTotalCartItem(this HtmlHelper html)
        {
            if (html.ViewContext.HttpContext.Session[CartItemCount] == null)
            {
                return "0";
            }
            return html.ViewContext.HttpContext.Session[CartItemCount].ToString();
        }
    }
}