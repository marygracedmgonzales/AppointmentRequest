using System.Web;
using System.Web.Optimization;

namespace ConsultationAppointmentRequestCalendar
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datepicker.css",
                      "~/Content/Icons/font-awesome-4.7.0/css/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/hims_appointment_scripts").Include(
                        "~/Scripts/HIMS_Scripts/hims_appointment.js",
                        "~/Scripts/moment.js"));

            bundles.Add(new StyleBundle("~/Content/hims_appointment_styles").Include(
                        "~/Content/HIMS_Styles/hims_appointment.css"));

            bundles.Add(new StyleBundle("~/Content/error_styles").Include(
                        "~/Content/HIMS_Stylyes/error-page.css"));
        }
    }
}
