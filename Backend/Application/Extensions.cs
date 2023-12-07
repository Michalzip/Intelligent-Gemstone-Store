using System;
using System.Reflection;
using MediatR;

namespace IntelligentStore.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            );
            return services;
        }
    }
}
