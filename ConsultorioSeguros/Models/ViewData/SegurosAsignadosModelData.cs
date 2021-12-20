using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ConsultorioSeguros.Models.ViewData
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SegurosAsignadosModelData
    {
        public string Codigo_seguro { get; set; }
        public string Cedula { get; set; }
    }
}