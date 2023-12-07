using IntelligentStore.Domain.Services.AdminService;

namespace IntelligentStore.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();

            return services;
        }
    }
}
