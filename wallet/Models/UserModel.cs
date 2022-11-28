using Newtonsoft.Json;

namespace wallet.Models
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
