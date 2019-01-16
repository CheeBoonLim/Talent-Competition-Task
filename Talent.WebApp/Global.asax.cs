using Talent.Diagnostics;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Talent.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Adding for log4net
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //Fires upon attempting to authenticate the use
            if (!(HttpContext.Current.User == null))
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity.GetType() == typeof(FormsIdentity))
                    {
                        var id = (FormsIdentity)HttpContext.Current.User.Identity;
                        var formAuthTicket = id.Ticket;

                        var roles = formAuthTicket.UserData.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                    }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Logging.Error(exception.Message, exception);
        }
    }
}
