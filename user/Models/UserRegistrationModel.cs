using Newtonsoft.Json;

namespace user.Models
{
    public class UserRegistrationModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
