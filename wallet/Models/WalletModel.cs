using Newtonsoft.Json;

namespace wallet.Models
{
    public class WalletModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }


        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
