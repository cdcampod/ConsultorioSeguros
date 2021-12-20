using System.Web.Mvc;

namespace ConsultorioSeguros.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Clientes()
        {
            return View();
        }

        public ActionResult SegurosAsignados(string cedula)
        {
            ViewBag.cedula = cedula;
            return View();
        }

        public ActionResult ClientesAsegurados(string codigo)
        {
            ViewBag.codigo = codigo;
            return View();
        }
    }
}