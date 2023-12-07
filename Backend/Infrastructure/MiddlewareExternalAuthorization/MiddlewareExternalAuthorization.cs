using IntelligentStore.Application.External.Auth;

namespace IntelligentStore.Infrastructure
{
    public class MiddlewareExternalAuthorization : IMiddleware
    {
        private readonly IEnumerable<IExternalServiceAuthorization> _externalServiceSAuthorizations;

        public MiddlewareExternalAuthorization(
            IEnumerable<IExternalServiceAuthorization> externalServiceSAuthorizations
        )
        {
            _externalServiceSAuthorizations = externalServiceSAuthorizations;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            foreach (var service in _externalServiceSAuthorizations)
            {
                if (service.DoesJwtTokenExist() == false)
                    await service.Auth();
            }

            await next(context);
        }
    }
}
