using System.Reflection;
using MediatR;
using IntelligentStore.SignalR;

namespace IntelligentStore.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
            );
            services.AddWebSocketMessage();
            return services;
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
        {
            app.UseWebSocketMessage();

            return app;
        }
    }
}
