using payment.Interfaces.Operations;

namespace payment.Operations
{
    public class WalletApiOperation : IWalletApiOperation
    {
        public IRequestOperation RequestOperation { get; set; }

        public IConfiguration Configuration { get; set; }

        public WalletApiOperation(IRequestOperation requestOperation, IConfiguration configuration)
        {
            RequestOperation = requestOperation;
            Configuration = configuration;
        }

        public async Task<bool> IsWalletExist(string walletNumber)
        {
            var url = Configuration.GetSection("Service:Wallet:Method:IsExist").Value;

            var data = new Dictionary<string, string>();

            data.Add("number", walletNumber);

            return await RequestOperation.Get(url, data);
        }
    }
}
