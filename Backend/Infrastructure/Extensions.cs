using IntelligentStore.Infrastructure.External;
using IntelligentStore.Application.External;
using IntelligentStore.Domain.IRepositories;
using IntelligentStore.Infrastructure.Repositories;
using IntelligentStore.Integrations.Ebay;
using Microsoft.AspNetCore.Authorization;
using IntelligentStore.Infrastructure.Policies;
using Shared;
using IntelligentStore.Infrastructure.EF;
using IntelligentStore.Infrastructure.Sql;
using IntelligentStore.Domain;
using Microsoft.AspNetCore.Identity;
using IntelligentStore.Domain.IInfrastructureServiceInterface;
using IntelligentStore.Infrastructure.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IntelligentStore.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.DbConfigure();
            services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();
            services.AddScoped<IExternalServiceFactory, ExternalServiceFactory>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationHandler, RequirementAdminHandler>();
            services.AddInitializer<AdminInitializer>();
            services.AddEbay();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.Requirements.Add(new AdminPolicy()));
            });
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("ETB4GNHYY2ETB4GNHYY2")
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
