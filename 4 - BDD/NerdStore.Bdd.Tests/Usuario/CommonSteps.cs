using TechTalk.SpecFlow;

namespace NerdStore.Bdd.Tests.Usuario
{
    [Binding]
    public class CommonSteps
    {
        [Given(@"Que o visitante está acessando o site da loja")]
        public void DadoQueOVisitanteEstaAcessandoOSiteDaLoja()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Ele será redirecionado para a vitrine")]
        public void EntaoEleSeraRedirecionadoParaAVitrine()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Uma saudação com seu e-mail será exibida no menu superior")]
        public void EntaoUmaSaudacaoComSeuE_MailSeraExibidaNoMenuSuperior()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
