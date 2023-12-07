using IntelligentStore.Application.External;
using Integrations.Ebay.Configuration;
using IntelligentStore.Integrations.Ebay.Services;
using System.Runtime.CompilerServices;
using IntelligentStore.Application.External.Auth;
using IntelligentStore.Integrations.Ebay.Authorization;

[assembly: InternalsVisibleTo("IntelligentStore.Infrastructure")]

namespace IntelligentStore.Integrations.Ebay
{
    internal static class Extensions
    {
        public static IServiceCollection AddEbay(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationEbay, ConfigurationEbay>();
            services.AddScoped<IExternalService, EbayService>();
            services.AddScoped<IExternalServiceAuthorization, EbayAuthorization>();

            return services;
        }
    }
}
