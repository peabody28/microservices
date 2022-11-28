using Newtonsoft.Json;

namespace payment.Models
{
    public class PaymentResponseModel
    {
        [JsonProperty("paymentId")]
        public Guid PaymentId { get; set; }
    }
}
