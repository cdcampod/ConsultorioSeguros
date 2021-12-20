using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioSeguros.Models.MySql
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Seguro
    {
        [Key]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Prima { get; set; }
        public decimal Suma_asegurada { get; set; }
    }
}