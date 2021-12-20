using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ConsultorioSeguros.Models.API
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ReplyDatatable : Reply
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}