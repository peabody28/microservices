using Newtonsoft.Json;

namespace user.Models
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
