using Cassette.Configuration;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Talent.WebApp
{
    /// <summary>
    /// Configures the Cassette asset modules for the web application.
    /// </summary>
    public class CassetteConfiguration : ICassetteConfiguration
    {
        public void Configure(BundleCollection bundles, CassetteSettings settings)
        {
            // TODO: Configure your bundles here...
            // Please read http://getcassette.net/documentation/configuration

            // This default configuration treats each file as a separate 'bundle'.
            // In production the content will be minified, but the files are not combined.
            // So you probably want to tweak these defaults!

            bundles.Add<ScriptBundle>("Scripts/jquery-3.2.1.js");
            bundles.Add<ScriptBundle>("Scripts/semantic.js");
            bundles.Add<ScriptBundle>("Scripts/handlebars-5.0.js");
            bundles.Add<ScriptBundle>("Scripts/datatables.js");
            //bundles.Add<ScriptBundle>("ReactScripts", new[] { "Scripts/react/react.js" ,"Scripts/react/react-dom.js" });
            bundles.Add<ScriptBundle>("Scripts/Skin.js");
            bundles.Add<ScriptBundle>("Scripts/Treant.js");
            bundles.Add<ScriptBundle>("Scripts/raphael.js");
            bundles.Add<ScriptBundle>("Scripts/Utils/Talent-lib.js");
            bundles.AddPerSubDirectory<ScriptBundle>("Scripts/Utils/Plugins/NotificationFx/js");
            bundles.AddPerSubDirectory<StylesheetBundle>("Scripts/Utils/Plugins/NotificationFx/css");
            // To combine files, try something like this instead:
            //   bundles.Add<StylesheetBundle>("Content");
            // In production mode, all of ~/Content will be combined into a single bundle.

            // If you want a bundle per folder, try this:
            //   bundles.AddPerSubDirectory<ScriptBundle>("Scripts");
            // Each immediate sub-directory of ~/Scripts will be combined into its own bundle.
            // This is useful when there are lots of scripts for different areas of the website.

            // *** TOP TIP: Delete all ".min.js" files now ***
            // Cassette minifies scripts for you. So those files are never used.
        }
    }
}