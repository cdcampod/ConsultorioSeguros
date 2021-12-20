using ConsultorioSeguros.Models.API;
using ConsultorioSeguros.Models.ViewData;
using ConsultorioSeguros.Services;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


namespace ConsultorioSeguros.Controllers
{
    [RoutePrefix("api/SeguroCliente")]
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class SeguroClienteController : ApiController
    {
        private readonly SeguroClienteService _seguroClienteService = new SeguroClienteService();

        [Route("seguros"), HttpGet]
        public IHttpActionResult SegurosList(string search)
        {
            Reply reply = _seguroClienteService.GetSeguros(search);

            return Json(reply);
        }

        [Route("list/{cedula}"), HttpPost]
        public IHttpActionResult ListSegurosAsignados(string cedula)
        {
            Reply reply = _seguroClienteService.GetSegurosAsignados(HttpContext.Current.Request, cedula);

            return Json(reply);
        }

        [Route("clientesAsegurados/{codigo}"), HttpPost]
        public IHttpActionResult ListClientesAsegurados(string codigo)
        {
            Reply reply = _seguroClienteService.GetClientesAseguradosData(HttpContext.Current.Request, codigo);

            return Json(reply);
        }

        [Route("add"), HttpPost]
        public IHttpActionResult Add(SegurosAsignadosModelData seguro_cliente)
        {
            Reply reply = _seguroClienteService.AddSeguroCliente(seguro_cliente);

            return Json(reply);
        }

        [Route("delete"), HttpDelete]
        public IHttpActionResult Delete(SegurosAsignadosModelData seguro_cliente)
        {
            Reply reply = _seguroClienteService.DeleteSeguroCliente(seguro_cliente);

            return Json(reply);
        }
    }
}