using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talent.Diagnostics;

namespace Talent.WebApp.Filters
{
    public class SkinHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            try
            {
                var verb = filterContext.HttpContext.Request.HttpMethod;
                var controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"];
                var action = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
                var childAction = filterContext.Controller.ControllerContext.IsChildAction;
                var isAjax = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
                var error = filterContext.Exception;

                bool isImpersonation = false;
                var impersonationSession = HttpContext.Current.Session["ImpersonatedBy"];
                if (impersonationSession != null && (int)impersonationSession != 0)
                {
                    isImpersonation = true;
                }

                var message = string.Format("ERR: {0}.{1}.{2} {3} {4} {5}",
                    verb, controller, action, childAction ? "(ChildAction)" : string.Empty, isAjax ? "(ajax)" : string.Empty,
                    isImpersonation ? ":Impersonating" : string.Empty
                    );


                Logging.Error(message, error);
            }
            catch (Exception x)
            {
                Logging.Error(x.Message, x);
            }
        }
    }
}