using System;

using Shared.Storage;

using Shared.Http;

namespace Shared.Middlewares.MiddlewareJwtAuthentication
{
    public class JwtAuthenticationMiddleware : IMiddleware
    {
        private readonly IRequestStorage _requestStorage;
        private readonly IHttpRequests _httpRequest;

        public JwtAuthenticationMiddleware(
            IRequestStorage requestStorage,
            IHttpRequests httpRequest
        )
        {
            _requestStorage = requestStorage;
            _httpRequest = httpRequest;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
         

            var token = _requestStorage.GetCookie(CookieKeys.APPLICATION_JWT_KEY);

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
                _httpRequest.AddBearerTokenHeader(token);
            }
           

            await next(context);
        }
    }
}
