using IntelligentStore.Infrastructure;
using Shared;
using IntelligentStore.Domain;
using IntelligentStore.Application;
using IntelligentStore.SignalR;

namespace IntelligentStore.ServiceInjector.Api
{
    internal static class Injector
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMiddlewares();
            services.AddWebSocketMessage();
            services.AddDomain();
            services.AddInfrastructure();
            services.AddShared();
            services.AddApplication();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });

            services.AddCors(c =>
            {
                c.AddPolicy(
                    "AllowOrigin",
                    options =>
                    {
                        options
                            .WithOrigins("http://localhost:3000", "http://localhost:3000/")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    }
                );
            });
            return services;
        }

        public static IApplicationBuilder UseService(this IApplicationBuilder app)
        {
            app.UseCors("AllowOrigin");
            app.UseMiddlewares();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseShared();
            app.UseApplication();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            return app;
        }
    }
}
