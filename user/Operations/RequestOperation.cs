using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using user.Interfaces.Operations;

namespace user.Operations
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
            catch (Exception e)
            {
                return default(T);
            }
        }

        public async Task<T> Get<T>(string url, IDictionary<string, string> data)
        {
            try
            {
                var uri = new Uri(QueryHelpers.AddQueryString(url, data));

                using var response = await HttpClient.GetAsync(uri);

                if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                    return default(T);

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch(Exception e)
            {
                return default(T);
            }
        }
    }
}