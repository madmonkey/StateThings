using System.Web;
using System.Web.Optimization;

namespace StateInterface
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.form.js",
                        "~/Scripts/jquery.maskedinput-{version}.js",
                        "~/Scripts/jquery.chained.js"));

            bundles.Add(new ScriptBundle("~/bundles/stateinterface").Include(
                "~/Scripts/stateinterface.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design").Include(
                "~/Scripts/stateinterface.design.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_certify").Include(
                "~/Scripts/stateinterface.certify.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_certify_update").Include(
                "~/Scripts/stateinterface.certify.update.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_certify_updateform").Include(
                "~/Scripts/stateinterface.certify.updateform.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_certify_statistics").Include(
                "~/Scripts/stateinterface.certify.statistics.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_certify_openissues").Include(
                "~/Scripts/stateinterface.certify.openissues.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_home").Include(
                "~/Scripts/stateinterface.design.home.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_field").Include(
                "~/Scripts/stateinterface.design.field.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_field_details").Include(
                "~/Scripts/stateinterface.design.field.details.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_form").Include(
                "~/Scripts/stateinterface.design.form.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_form_details").Include(
                "~/Scripts/stateinterface.design.form.details.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_connect_home").Include(
                "~/Scripts/stateinterface.connect.home.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_layout").Include(
                "~/Scripts/stateinterface.design.layout.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_snippet").Include(
                "~/Scripts/stateinterface.design.snippet.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_snippetfield").Include(
                "~/Scripts/stateinterface.design.snippetfield.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_list").Include(
                "~/Scripts/stateinterface.design.list.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_design_list_details").Include(
                "~/Scripts/stateinterface.design.list.details.js"));
            bundles.Add(new ScriptBundle("~/bundles/stateinterface_connect_specifications").Include(
                "~/Scripts/stateinterface.connect.specifications.js"));

            bundles.Add(new ScriptBundle("~/bootstrap").Include(
                "~/Scripts/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.mapping-latest.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                "~/content/bootstrap.css",
                "~/Content/non-responsive.css"));

            bundles.Add(new StyleBundle("~/bundles/layout/css").Include(
                "~/Content/jquery.gridster.css",
                "~/Content/layout.designer.css",
                "~/Content/layout.css"));

            bundles.Add(new StyleBundle("~/bundles/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}