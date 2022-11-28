using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using payment.Interfaces.Operations;

namespace payment.Operations
{
    public class RequestOperation : IRequestOperation
    {
        private HttpClient HttpClient;

        public RequestOperation(HttpClient _httpClient)
        {
            HttpClient = _httpClient;
        }

        /// <summary>
        /// return boolean (is response status 200 (OK))
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Get(string url, IDictionary<string, string> data)
        {
            try
            {
                var uri = new Uri(QueryHelpers.AddQueryString(url, data));

                using var response = await HttpClient.GetAsync(uri);

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}