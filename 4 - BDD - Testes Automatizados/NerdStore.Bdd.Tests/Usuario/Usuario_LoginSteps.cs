using NerdStore.Bdd.Tests.Config;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.Bdd.Tests.Usuario
{
    [Binding]
    [Collection(nameof(AutomacaoWebTestsFixtureCollection))]
    public class Usuario_LoginSteps
    {
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly LoginDeUsuarioTela _loginDeUsuarioTela;

        public Usuario_LoginSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _loginDeUsuarioTela = new LoginDeUsuarioTela(_testsFixture.BrowserHelper);
        }

        [When(@"Ele clicar em login")]
        public void QuandoEleClicarEmLogin()
        {
            // Act
            _loginDeUsuarioTela.ClicarNoLinkLogin();

            // Assert
            Assert.Equal(_testsFixture.Configuration.LoginUrl, _loginDeUsuarioTela.ObterUrl());
        }

        [When(@"Preencher os dados do formulário de login")]
        public void QuandoPreencherOsDadosDoFormularioDeLogin(Table table)
        {
            // Arrange
            _testsFixture.Usuario = new UsuarioModel
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };

            // Act
            _loginDeUsuarioTela.PreencherFormularioLogin(_testsFixture.Usuario);

            // Assert
            Assert.True(_loginDeUsuarioTela.ValidarPreenchimentoFormularioLogin(_testsFixture.Usuario));
        }

        [When(@"Clicar no botão login")]
        public void QuandoClicarNoBotaoLogin()
        {
            // Act
            _loginDeUsuarioTela.ClicarNoBotaoLogin();
        }
    }
}
