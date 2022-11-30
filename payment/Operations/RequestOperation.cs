using Microsoft.AspNetCore.WebUtilities;
using payment.Interfaces.Operations;
using payment.Interfaces.Common;
using System.Net;

namespace payment.Operations
{
    public class RequestOperation : IRequestOperation
    {
        private HttpClient HttpClient;

        public ICurrentRequest CurrentRequest { get; set; }

        public RequestOperation(HttpClient _httpClient, ICurrentRequest currentRequest)
        {
            HttpClient = _httpClient;
            CurrentRequest = currentRequest;
        }

        /// <summary>
        /// return boolean (is response status 200 (OK))
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Get(string url, IDictionary<string, string> data, bool isAuthenticationNeed = true)
        {
            try
            {
                var uri = new Uri(QueryHelpers.AddQueryString(url, data));

                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                if(isAuthenticationNeed)
                    request.Headers.Add("Authorization", CurrentRequest.Context.Request.Headers.Authorization);

                using var response = await HttpClient.SendAsync(request);

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