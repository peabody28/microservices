using System.Text.Json.Nodes;
using System.Text;
using System.Net;
using auth.Interfaces.Operations;

namespace auth.Operations
{
    public class RequestOperation : IRequestOperation
    {
        private HttpClient HttpClient;

        public RequestOperation(HttpClient _httpClient)
        {
            HttpClient = _httpClient;
        }

        public async Task<T> Post<T>(string url, JsonObject data)
        {
            try
            {
                var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                using var response = await HttpClient.PostAsync(url, content);

                if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                    return default(T);

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
