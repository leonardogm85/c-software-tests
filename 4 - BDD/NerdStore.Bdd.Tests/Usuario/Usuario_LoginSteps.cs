using TechTalk.SpecFlow;

namespace NerdStore.Bdd.Tests.Usuario
{
    [Binding]
    public class Usuario_LoginSteps
    {
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
