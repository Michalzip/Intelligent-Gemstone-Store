using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;

namespace Integrations.Ebay.Configuration
{
    internal sealed class ConfigurationEbay : IConfigurationEbay
    {
        private const string SectionName = "Ebay";

        private readonly IConfigurationSection _configuration;

        public ConfigurationEbay(IConfiguration configuration)
        {
            _configuration = configuration.GetSection(SectionName);
        }

        public string ClientId => Guard.Against.NullOrEmpty(_configuration["ClientId"]);
        public string ClientSecret => Guard.Against.NullOrEmpty(_configuration["ClientSecret"]);
    }
}
