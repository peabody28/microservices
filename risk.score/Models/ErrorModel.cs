using Newtonsoft.Json;

namespace risk.score.Models
{
    public class ErrorModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
