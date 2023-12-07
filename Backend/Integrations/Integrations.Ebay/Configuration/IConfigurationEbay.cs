using System;

namespace Integrations.Ebay.Configuration
{
    public interface IConfigurationEbay
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }
}
