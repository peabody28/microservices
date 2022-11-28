using user.Interfaces.Operations;
using user.Models.DTO.RiskScore;

namespace user.Operations
{
    public class RiskScoreApiOperation : IRiskScoreApiOperation
    {
        public IRequestOperation RequestOperation { get; set; }

        public IConfiguration Configuration { get; set; }

        public RiskScoreApiOperation(IRequestOperation requestOperation, IConfiguration configuration)
        {
            RequestOperation = requestOperation;
            Configuration = configuration;
        }

        public async Task<bool> Status(string username)
        {
            var url = Configuration.GetSection("Service:RiskScore:Method:Status").Value;

            var data = new Dictionary<string, string>();
            data.Add("name", username);

            var response = await RequestOperation.Get<RiskScoreStatusModel>(url, data);

            return response?.Status ?? true;
        }
    }
}
