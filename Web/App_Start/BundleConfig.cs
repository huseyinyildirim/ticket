using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Clear();
            bundles.FileSetOrderList.Clear();

            // +++ GLOBAL SCRIPT BUNDLES +++
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.1.0.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-1.10.4.min.js", "~/Scripts/jquery-ui-i18n.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js", "~/Scripts/jquery.unobtrusive-ajax.min.js"));
            // +++ GLOBAL SCRIPT BUNDLES +++


            // +++ LOGIN SCRIPT BUNDLES +++
            bundles.Add(new ScriptBundle("~/bundles/LoginScript").Include("~/Content/JS/Global.js", "~/Content/JS/Login.js"));
            // --- LOGIN SCRIPT BUNDLES ---

            // +++ LOGIN STYLE BUNDLES +++
            bundles.Add(new StyleBundle("~/bundles/LoginStyle").Include("~/Content/CSS/Standart.css", "~/Content/CSS/Icon.css", "~/Content/CSS/Login.css"));
            // --- LOGIN STYLE BUNDLES ---

            // +++ SCRIPT BUNDLES +++
            bundles.Add(new ScriptBundle("~/bundles/Script").Include("~/Content/JS/Choosen.js", "~/Content/JS/Textarea.js", "~/Content/JS/Numeric.js", "~/Content/JS/Mask.js", "~/Content/JS/PerfectScroll.js", "~/Content/JS/Scroll.js", "~/Content/JS/Tipsy.js", "~/Content/JS/Global.js", "~/Content/JS/Script.js"));
            // --- SCRIPT BUNDLES ---

            // +++ STYLE BUNDLES +++
            bundles.Add(new StyleBundle("~/bundles/Style").Include("~/Content/CSS/Standart.css", "~/Content/CSS/Icon.css", "~/Content/themes/base/jquery.ui.core.css", "~/Content/themes/base/jquery.ui.theme.css", "~/Content/themes/base/jquery.ui.datepicker.css", "~/Content/CSS/Style.css"));
            // --- STYLE BUNDLES ---

            // +++ FORM STYLE BUNDLES +++
            bundles.Add(new StyleBundle("~/bundles/FormStyle").Include("~/Content/CSS/Form.css"));
            // --- FORM STYLE BUNDLES ---

        }
    }
}
