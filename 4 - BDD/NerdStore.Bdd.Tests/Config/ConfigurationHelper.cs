using Microsoft.Extensions.Configuration;
using System.IO;

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
        public string DomainUrl => _configuration.GetSection("DomainUrl").Value;
        public string RegisterUrl => $"{DomainUrl}{_configuration.GetSection("RegisterUrl").Value}";
        public string VitrineUrl => $"{DomainUrl}{_configuration.GetSection("VitrineUrl").Value}";
        public string LoginUrl => $"{DomainUrl}{_configuration.GetSection("LoginUrl").Value}";
        public string ProdutoUrl => $"{DomainUrl}{_configuration.GetSection("ProdutoUrl").Value}";
        public string CarrinhoUrl => $"{DomainUrl}{_configuration.GetSection("CarrinhoUrl").Value}";
        public string FolderPath => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        public string FolderPicture => $"{FolderPath}{_configuration.GetSection("FolderPicture").Value}";
    }
}
