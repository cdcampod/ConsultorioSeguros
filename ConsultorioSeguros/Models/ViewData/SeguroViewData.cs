using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ConsultorioSeguros.Models.ViewData
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SeguroViewData
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Prima { get; set; }
        public decimal Suma_asegurada { get; set; }
    }
}