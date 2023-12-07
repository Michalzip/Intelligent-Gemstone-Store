using System;
using IntelligentStore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using IntelligentStore.Domain;
using System.Reflection;
using IntelligentStore.Application;
using IntelligentStore.Infrastructure.Policies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;

namespace IntelligentStore.ServiceInjector.Api
{
    internal static class Injector
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddMiddlewares();
            services.AddApplication();
            services.AddDomain();
            services.AddInfrastructure();
            services.AddShared();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });

            return services;
        }

        public static IApplicationBuilder UseService(this IApplicationBuilder app)
        {
            app.UseMiddlewares();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseShared();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            return app;
        }
    }
}
