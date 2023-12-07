using System;

namespace Shared.Http
{
    public interface IHttpRequests
    {
        public void AddBearerTokenHeader(string token);
        public Task<string> ApiFetchCallAsync(string url, Dictionary<string, string>? headers);
        public Task<HttpResponseMessage> ApiPostCallAsync(
            string url,
            HttpContent content,
            Dictionary<string, string> headers
        );
    }
}
