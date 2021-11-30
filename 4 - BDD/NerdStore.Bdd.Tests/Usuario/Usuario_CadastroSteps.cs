using NerdStore.Bdd.Tests.Config;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.Bdd.Tests.Usuario
{
    [Binding]
    [Collection(nameof(AutomacaoWebTestsFixtureCollection))]
    public class Usuario_CadastroSteps
    {
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly CadastroDeUsuarioTela _cadastroDeUsuarioTela;

        public Usuario_CadastroSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cadastroDeUsuarioTela = new CadastroDeUsuarioTela(_testsFixture.BrowserHelper);
        }

        [When(@"Ele clicar em registrar")]
        public void QuandoEleClicarEmRegistrar()
        {
            // Act
            _cadastroDeUsuarioTela.ClicarNoLinkRegistrar();

            // Assert
            Assert.Equal(_testsFixture.Configuration.RegisterUrl, _cadastroDeUsuarioTela.ObterUrl());
        }

        [When(@"Preencher os dados do formulário")]
        public void QuandoPreencherOsDadosDoFormulario(Table table)
        {
            // Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;

            // Act
            _cadastroDeUsuarioTela.PreencherFormularioRegistro(usuario);

            // Assert
            Assert.True(_cadastroDeUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [When(@"Clicar no botão registrar")]
        public void QuandoClicarNoBotaoRegistrar()
        {
            // Act
            _cadastroDeUsuarioTela.ClicarNoBotaoRegistrar();
        }

        [When(@"Preencher os dados do formulário com uma senha sem maiusculas")]
        public void QuandoPreencherOsDadosDoFormularioComUmaSenhaSemMaiusculas(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Preencher os dados do formulário com uma senha sem caractere especial")]
        public void QuandoPreencherOsDadosDoFormularioComUmaSenhaSemCaractereEspecial(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter uma letra maiuscula")]
        public void EntaoEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmaLetraMaiuscula()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter um caractere especial")]
        public void EntaoEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmCaractereEspecial()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
