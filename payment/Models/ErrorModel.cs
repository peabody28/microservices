using Newtonsoft.Json;

namespace payment.Models
{
    public class ErrorModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}