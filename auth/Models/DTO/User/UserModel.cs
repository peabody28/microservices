using Newtonsoft.Json;

namespace auth.Models.DTO.User
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("roleCode")]
        public string RoleCode { get; set; }
    }
}
