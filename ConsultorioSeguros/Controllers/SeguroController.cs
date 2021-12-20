using ConsultorioSeguros.Models.API;
using ConsultorioSeguros.Models.ViewData;
using ConsultorioSeguros.Services;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


namespace ConsultorioSeguros.Controllers
{
    [RoutePrefix("api/Seguro")]
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class SeguroController : ApiController
    {
        private readonly SeguroService _seguroService = new SeguroService();

        [Route("list"), HttpPost]
        public IHttpActionResult List()
        {
            Reply reply = _seguroService.GetSegurosData(HttpContext.Current.Request);

            return Json(reply);
        }

        [Route("add"), HttpPost]
        public IHttpActionResult Add(SeguroViewData seguro)
        {
            Reply reply = _seguroService.AddSeguro(seguro);

            return Json(reply);
        }

        [Route("update"), HttpPut]
        public IHttpActionResult Update(SeguroViewData seguro)
        {
            Reply reply = _seguroService.UpdateSeguro(seguro);

            return Json(reply);
        }

        [Route("delete"), HttpDelete]
        public IHttpActionResult Delete(SeguroViewData seguro)
        {
            Reply reply = _seguroService.DeleteSeguro(seguro);

            return Json(reply);
        }
    }
}