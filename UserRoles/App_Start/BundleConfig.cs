using System.Web;
using System.Web.Optimization;

namespace UserRoles
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new Bundle("~/bundles/scripts")
                .Include("~/Scripts/jquery.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/script.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/styles.css"
                      ));
        }
    }
}
