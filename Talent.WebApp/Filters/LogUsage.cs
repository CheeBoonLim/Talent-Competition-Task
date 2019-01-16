using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talent.Diagnostics;
using Talent.WebApp.Models;

namespace Talent.WebApp.Filters
{
    public class LogUsage : ActionFilterAttribute
    {
        public LogUsage()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(IgnoreLogAttribute), false).Any())
                {
                    return;
                }

                var verb = filterContext.HttpContext.Request.HttpMethod;
                var controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"];
                var action = filterContext.ActionDescriptor.ActionName;
                var childAction = filterContext.Controller.ControllerContext.IsChildAction;
                var isAjax = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();

                if (controller.ToString().IndexOf("polling", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    return;
                }

                var message = string.Format("REQ: {0}.{1}.{2} {3} {4}", verb, controller, action, childAction ? "(ChildAction)" : string.Empty, isAjax ? "(ajax)" : string.Empty);

                Logging.DebugFormat(message);
            }
            catch (Exception x)
            {
                Logging.Error(x.Message, x);
            }
        }

        public class IgnoreLogAttribute : FilterAttribute, IActionFilter
        {
            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
            }

            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
            }
        }
    }
}