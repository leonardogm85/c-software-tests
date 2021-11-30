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
        private readonly CadastroDeUsuarioTela _cadastroDeUsuarioTela;

        public Usuario_LoginSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cadastroDeUsuarioTela = new CadastroDeUsuarioTela(_testsFixture.BrowserHelper);
        }

        [When(@"Ele clicar em login")]
        public void QuandoEleClicarEmLogin()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Preencher os dados do formulário de login")]
        public void QuandoPreencherOsDadosDoFormularioDeLogin(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Clicar no botão login")]
        public void QuandoClicarNoBotaoLogin()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
