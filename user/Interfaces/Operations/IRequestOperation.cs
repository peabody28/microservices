using System.Text.Json.Nodes;

namespace user.Interfaces.Operations
{
    public interface IRequestOperation
    {
        Task<T> Post<T>(string url, JsonObject data);

        Task<T> Get<T>(string url, IDictionary<string, string> data);
    }
}
