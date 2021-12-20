using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioSeguros.Models.MySql
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Cliente
    {
        [Key]
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
    }
}