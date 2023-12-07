using System;
using IntelligentStore.Domain.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Tests.Services;

namespace Tests.Fixtures
{
    public class AdminServiceFixture : IDisposable
    {
        public IAdminRepository adminRepository;

        public AdminServiceFixture()
        {
            var services = new ServiceCollection();

            services.AddScoped<IAdminRepository, AdminTestDataRepository>();
        }

        public void Dispose()
        {
            // clean up the setup code, if required
        }
    }
}
