using System;
using System.Web.Mvc;

namespace Talent.WebApp.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }

        private const string IsAuthorized = "isAuthorized";

        public string RedirectUrl = "~/Account/AccessDenied";

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);

            if (httpContext.Items[IsAuthorized] == null)
                httpContext.Items.Add(IsAuthorized, isAuthorized);
            else
                httpContext.Items[IsAuthorized] = isAuthorized;

            return isAuthorized;
        }

        // if they don't have the correct role for the request, send them to the access-denied page
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAuthorized = filterContext.HttpContext.Items[IsAuthorized] != null && Convert.ToBoolean(filterContext.HttpContext.Items[IsAuthorized]);

            if (!isAuthorized && filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect(RedirectUrl);
            }
        }
    }
}
