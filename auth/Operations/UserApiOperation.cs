using auth.Interfaces.Operations;
using auth.Models.DTO.User;
using System.Text.Json.Nodes;

namespace auth.Operations
{
    public class UserApiOperation : IUserApiOperation
    {
        public IRequestOperation RequestOperation { get; set; }

        public IConfiguration Configuration { get; set; }

        public UserApiOperation(IRequestOperation requestOperation, IConfiguration configuration)
        {
            RequestOperation = requestOperation;
            Configuration = configuration;
        }

        public async Task<UserModel> GetUser(string username, string password)
        {
            var url = Configuration.GetSection("Service:User:Method:Info").Value;

            var data = new JsonObject { { "name", username }, { "password", password } };

            var response = await RequestOperation.Post<UserModel>(url, data);

            return response;
        }
    }
}
