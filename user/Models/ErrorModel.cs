using Newtonsoft.Json;

namespace user.Models
{
    public class ErrorModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
