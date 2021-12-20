using System.Web.Optimization;

namespace ConsultorioSeguros
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/umd/popper.js",
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/datatable/datatables.min.css",
                      "~/Content/datatable/DataTables-1.11.3/css/dataTables.bootstrap4.min.css",
                      "~/Content/datatable/Responsive-2.2.9/css/responsive.bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                      "~/Scripts/datatable/datatables.min.js",
                      "~/Scripts/datatable/DataTables-1.11.3/js/dataTables.bootstrap4.min.js",
                      "~/Scripts/datatable/plugins/fnPagingInfo.js",
                      "~/Scripts/datatable/Responsive-2.2.9/js/responsive.bootstrap4.min.js"));
        }
    }
}
