﻿using System;

namespace Shared.Services
{
    public class AppInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppInitializer> _logger;

        public AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var initializers = scope.ServiceProvider.GetServices<IInitializer>();

            foreach (var initializer in initializers)
            {
                try
                {
                    await initializer.InitAsync();
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
