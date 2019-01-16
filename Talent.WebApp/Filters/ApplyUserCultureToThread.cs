using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Talent.WebApp.Filters
{   
    public class ApplyUserCultureToThread : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var defaultCulture = "en-NZ";

            //if (HttpContext.Current.Session != null)
            //{
            //    if (HttpContext.Current.Request.IsAuthenticated && HttpContext.Current.Session["CurrentUser"] != null)
            //    {
            //        // TODO when culture is required.
            //    }
            //}

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(defaultCulture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }
    }
}