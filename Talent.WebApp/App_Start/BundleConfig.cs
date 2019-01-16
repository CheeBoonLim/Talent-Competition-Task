using System.Web;
using System.Web.Optimization;

namespace Talent.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/notification").Include(
                "~/Scripts/Utils/NotificationFx/js/classie.js",
                "~/Scripts/Utils/NotificationFx/js/modernizr.custom.js",
                "~/Scripts/Utils/NotificationFx/js/notificationFx.js",
                "~/Scripts/Utils/NotificationFx/js/snap.svg-min.js"
            ));
            bundles.Add(new StyleBundle("~/notification/css").Include(
                "~/Scripts/Utils/NotificationFx/css/normalize.css",
                "~/Scripts/Utils/NotificationFx/css/ns-default.css",
                "~/Scripts/Utils/NotificationFx/css/ns-style-attached.css",
                "~/Scripts/Utils/NotificationFx/css/ns-style-bar.css",
                "~/Scripts/Utils/NotificationFx/css/ns-style-growl.css",
                "~/Scripts/Utils/NotificationFx/css/ns-style-other.css"
            ));
        }
    }
}
