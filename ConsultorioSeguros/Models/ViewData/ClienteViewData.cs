using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ConsultorioSeguros.Models.ViewData
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ClienteViewData
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
    }
}