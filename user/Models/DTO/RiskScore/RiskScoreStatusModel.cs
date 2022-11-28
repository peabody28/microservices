using Newtonsoft.Json;

namespace user.Models.DTO.RiskScore
{
    public class RiskScoreStatusModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
