using Talent.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Talent.WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeModulePermissionAttribute : ActionFilterAttribute
    {
        string pageName;

        public AuthorizeModulePermissionAttribute(string pageName)
        {
            this.pageName = pageName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (!SiteUtil.CheckMenuPermission(pageName))
            //{
            //    filterContext.Result = new RedirectToRouteResult(new
            //               RouteValueDictionary(new { controller = "Home", action = "NoAccess", area = "" }));
            //}
        }

    }
}