using ConsultorioSeguros.Models.API;
using ConsultorioSeguros.Models.ViewData;
using ConsultorioSeguros.Services;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


namespace ConsultorioSeguros.Controllers
{
    [RoutePrefix("api/Cliente")]
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class ClienteController : ApiController
    {
        private readonly ClienteService _clienteService = new ClienteService();

        [Route("list"), HttpPost]
        public IHttpActionResult List()
        {
            Reply reply = _clienteService.GetClientesData(HttpContext.Current.Request);

            return Json(reply);
        }

        [Route("add"), HttpPost]
        public IHttpActionResult Add(ClienteViewData cliente)
        {
            Reply reply = _clienteService.AddCliente(cliente);

            return Json(reply);
        }

        [Route("update"), HttpPut]
        public IHttpActionResult Update(ClienteViewData cliente)
        {
            Reply reply = _clienteService.UpdateCliente(cliente);

            return Json(reply);
        }

        [Route("delete"), HttpDelete]
        public IHttpActionResult Delete(ClienteViewData cliente)
        {
            Reply reply = _clienteService.DeleteCliente(cliente);

            return Json(reply);
        }

        [Route("upload"), HttpPost]
        public IHttpActionResult Upload()
        {
            Reply reply = _clienteService.UploadCliente(HttpContext.Current.Request);

            return Json(reply);
        }
    }
}