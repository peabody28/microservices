using Newtonsoft.Json;

namespace auth.Models
{
    public class ErrorModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
