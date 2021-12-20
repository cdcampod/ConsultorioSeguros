using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace ConsultorioSeguros.Models.ViewData
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ClienteAseguradoViewData
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Prima { get; set; }
        public decimal Suma_asegurada { get; set; }
        public List<ClienteViewData> Clientes { get; set; }
    }
}