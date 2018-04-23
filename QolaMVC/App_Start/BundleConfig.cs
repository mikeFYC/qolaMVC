using System.Web;
using System.Web.Optimization;

namespace QolaMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/assets/plugins/pace/pace-theme-flash.css",
                      "~/Content/assets/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/plugins/font-awesome/css/font-awesome.css",
                      "~/Content/assets/plugins/jquery-scrollbar/jquery.scrollbar.css",
                      "~/Content/assets/plugins/select2/css/select2.min.css",
                      "~/Content/assets/plugins/switchery/css/switchery.min.css",
                      "~/Content/pages/css/pages-icons.css",
                      "~/Content/assets/css/pages.css",
                      "~/Content/assets/css/datatables.css",
                      "~/Content/assets/css/style.css"));
        }
    }
}
