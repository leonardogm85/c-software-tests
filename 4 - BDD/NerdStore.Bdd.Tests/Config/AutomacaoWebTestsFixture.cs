using Bogus;
using NerdStore.Bdd.Tests.Usuario;

namespace NerdStore.Bdd.Tests.Config
{
    public class AutomacaoWebTestsFixture
    {
        public readonly ConfigurationHelper Configuration;

        public SeleniumHelper BrowserHelper;
        public UsuarioModel Usuario;

        public AutomacaoWebTestsFixture()
        {
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, false);
            Usuario = new UsuarioModel();
        }

        public void GerarDadosUsuario()
        {
            var faker = new Faker();
            Usuario.Email = faker.Internet.Email().ToLower();
            Usuario.Senha = faker.Internet.Password(8, false, string.Empty, "@1Ab_");
        }
    }
}
