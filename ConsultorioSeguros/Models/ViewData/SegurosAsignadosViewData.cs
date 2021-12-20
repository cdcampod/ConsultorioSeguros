using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace ConsultorioSeguros.Models.ViewData
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SegurosAsignadosViewData
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
        public List<SeguroViewData> Seguros { get; set; }
    }
}