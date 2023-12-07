using System.Net.Http.Headers;

namespace Shared.Http
{
    public sealed class HttpRequests : IHttpRequests
    {
        private readonly HttpClient _httpClient;

        public HttpRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void AddHeaders(Dictionary<string, string>? headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                        header.Key,
                        header.Value
                    );
                }
            }
        }

        public void AddBearerTokenHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }

        public async Task<string> ApiFetchCallAsync(string url, Dictionary<string, string>? headers)
        {
            try
            {
                AddHeaders(headers);
                var response = await _httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<HttpResponseMessage> ApiPostCallAsync(
            string url,
            HttpContent content,
            Dictionary<string, string> headers
        )
        {
            AddHeaders(headers);

            return await _httpClient.PostAsync(url, content);
        }
    }
}
