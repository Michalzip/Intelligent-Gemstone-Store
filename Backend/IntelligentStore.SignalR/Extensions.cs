using System;
using IntelligentStore.SignalR.Hubs;
using IntelligentStore.SignalR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentStore.SignalR
{
    public static class Extensions
    {
        public static IServiceCollection AddWebSocketMessage(this IServiceCollection services)
        {
            services
                .AddSignalR()
                .AddHubOptions<WebSocketHub>(options =>
                {
                    options.EnableDetailedErrors = true;
                })
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddScoped<IWebSocketService, WebSocketService>();

            return services;
        }

        public static void UseWebSocketMessage(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<WebSocketHub>("/WebSocketMessageHub");
            });
        }
    }
}
