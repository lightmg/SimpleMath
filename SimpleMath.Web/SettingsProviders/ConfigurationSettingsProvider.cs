using System;
using Microsoft.Extensions.Configuration;

namespace SimpleMath.Web.SettingsProviders
{
    public class ConfigurationSettingsProvider : IMaxRequestLimitProvider
    {
        private readonly IConfiguration configuration;

        public ConfigurationSettingsProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int GetMaxRequestsLimit() => GetConfigurationEntry<int>("ParallelRequestsLimit");

        private TValue GetConfigurationEntry<TValue>(string entryName)
        {
            if (string.IsNullOrWhiteSpace(entryName))
                throw new ArgumentException($"{nameof(entryName)} is null or whitespace ", nameof(entryName));

            var strValue = configuration[entryName] ??
                           throw new InvalidOperationException(
                               "Configuration does not contain definition for " + entryName);

            return (TValue) Convert.ChangeType(strValue, typeof(TValue));
        }
    }
}