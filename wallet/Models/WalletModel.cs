using Newtonsoft.Json;

namespace wallet.Models
{
    public class WalletModel
    {
        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
