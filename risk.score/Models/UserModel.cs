using Newtonsoft.Json;

namespace risk.score.Models
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
