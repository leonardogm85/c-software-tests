using NerdStore.Bdd.Tests.Config;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.Bdd.Tests.Usuario
{
    [Binding]
    [Collection(nameof(AutomacaoWebTestsFixtureCollection))]
    public class CommonSteps
    {
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly CadastroDeUsuarioTela _cadastroDeUsuarioTela;

        public CommonSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cadastroDeUsuarioTela = new CadastroDeUsuarioTela(_testsFixture.BrowserHelper);
        }

        [Given(@"Que o visitante está acessando o site da loja")]
        public void DadoQueOVisitanteEstaAcessandoOSiteDaLoja()
        {
            // Act
            _cadastroDeUsuarioTela.AcessarSiteLoja();

            // Assert
            Assert.Equal(_testsFixture.Configuration.VitrineUrl, _cadastroDeUsuarioTela.ObterUrl());
        }

        [Then(@"Ele será redirecionado para a vitrine")]
        public void EntaoEleSeraRedirecionadoParaAVitrine()
        {
            // Assert
            Assert.Equal(_testsFixture.Configuration.VitrineUrl, _cadastroDeUsuarioTela.ObterUrl());
        }

        [Then(@"Uma saudação com seu e-mail será exibida no menu superior")]
        public void EntaoUmaSaudacaoComSeuE_MailSeraExibidaNoMenuSuperior()
        {
            // Assert
            Assert.True(_cadastroDeUsuarioTela.ValidarSaudacaoUsuarioLogado(_testsFixture.Usuario));
        }
    }
}
