using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ConsultorioSeguros.Models.API
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Reply
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object AdditionalData { get; set; }
    }
}