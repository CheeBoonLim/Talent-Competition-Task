using Talent.WebApp.Filters;
using System.Web;
using System.Web.Mvc;

namespace Talent.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new SkinHandleErrorAttribute());
            filters.Add(new LogUsage());
            filters.Add(new NoCacheAttribute());
            filters.Add(new ApplyUserCultureToThread());
        }
    }
}
