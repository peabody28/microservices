using Newtonsoft.Json;

namespace risk.score.Models
{
    public class UserRiskScoreStatusModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
