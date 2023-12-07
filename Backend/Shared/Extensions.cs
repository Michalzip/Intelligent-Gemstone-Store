using Shared.Http;
using Shared.Services;
using Shared.Storage;
using Shared.Middlewares.MiddlewareErrorHandle;

namespace Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddScoped<ITokenExternalService, TokenExternalService>();
            services.AddScoped<IHttpRequests, HttpRequests>();
            services.AddScoped<IRequestStorage, RequestStorage>();
            services.AddHostedService<AppInitializer>();

            return services;
        }

        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseSession();
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }

        public static IServiceCollection AddInitializer<T>(this IServiceCollection services)
            where T : class, IInitializer => services.AddTransient<IInitializer, T>();

        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var middlewareTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract && typeof(IMiddleware).IsAssignableFrom(type));

            foreach (var middlewareType in middlewareTypes)
            {
                services.AddScoped(middlewareType);
            }

            return services;
        }

        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var middlewareTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract && typeof(IMiddleware).IsAssignableFrom(type));

            foreach (var middlewareType in middlewareTypes)
            {
                app.UseMiddleware(middlewareType);
            }

            return app;
        }
    }
}
