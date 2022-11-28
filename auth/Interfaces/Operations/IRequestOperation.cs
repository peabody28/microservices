using System.Text.Json.Nodes;

namespace auth.Interfaces.Operations
{
    public interface IRequestOperation
    {
        Task<T> Post<T>(string url, JsonObject data);
    }
}
