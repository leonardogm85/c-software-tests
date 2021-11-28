using Microsoft.Extensions.Configuration;

namespace NerdStore.Bdd.Tests.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelper()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string WebDrivers => _configuration.GetSection("WebDrivers").Value;
    }
}
