using Newtonsoft.Json;

namespace user.Models
{
    public class UserInfoModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("roleCode")]
        public string RoleCode { get; set; }
    }
}
