using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioSeguros.Models.SQL
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Seguro_cliente
    {
        [Key]
        public int Id { get; set; }
        public string Codigo_seguro { get; set; }
        public string Cedula { get; set; }
    }
}