using System.Text.Json.Nodes;
using user.Interfaces.Operations;
using user.Models.DTO.Wallet;

namespace user.Operations
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

        public async Task<WalletModel> Create(string username)
        {
            var url = Configuration.GetSection("Service:Wallet:Method:Create").Value;

            var data = new JsonObject { { "name", username } };

            var response = await RequestOperation.Post<WalletModel>(url, data);

            return response;
        }
    }
}
