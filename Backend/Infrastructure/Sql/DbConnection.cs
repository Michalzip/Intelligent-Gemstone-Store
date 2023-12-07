using System;
using IntelligentStore.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace IntelligentStore.Infrastructure.Sql
{
    public static class DbConnection
    {
        private const string DbConnectionString =
            "Server=localhost,1433;Initial Catalog=ShopDatabase;User Id=sa;Password=Password.1;Encrypt=false;";

        public static IServiceCollection DbConfigure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(DbConnectionString);
            });

            return services;
        }
    }
}
