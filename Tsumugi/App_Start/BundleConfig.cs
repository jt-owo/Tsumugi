using System.Web.Optimization;

namespace Tsumugi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/global.css",
                      "~/Content/css/layout.css",
                      "~/Content/css/wallet.css",
                      "~/Content/css/utility.css",
                      "~/Content/css/loginModal.css",
                      "~/Content/css/dashboard.css"));
        }
    }
}
