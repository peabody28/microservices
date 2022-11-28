using Newtonsoft.Json;

namespace user.Models.DTO.Wallet
{
    public class WalletModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
