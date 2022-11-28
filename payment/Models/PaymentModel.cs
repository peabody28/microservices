using Newtonsoft.Json;

namespace payment.Models
{
    public class PaymentModel
    {
        [JsonProperty("walletNumber")]
        public string WalletNumber { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Credit
        /// Debit
        /// </summary>
        [JsonProperty("balanceOperationTypeCode")]
        public string BalanceOperationTypeCode { get; set; }
    }
}
