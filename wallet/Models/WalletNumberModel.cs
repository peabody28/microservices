using Newtonsoft.Json;

namespace wallet.Models
{
    public class WalletNumberModel
    {
        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
