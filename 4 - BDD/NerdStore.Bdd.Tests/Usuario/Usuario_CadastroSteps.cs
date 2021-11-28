using TechTalk.SpecFlow;

namespace NerdStore.Bdd.Tests.Usuario
{
    [Binding]
    public class Usuario_CadastroSteps
    {
        [When(@"Ele clicar em registrar")]
        public void QuandoEleClicarEmRegistrar()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Preencher os dados do formulário")]
        public void QuandoPreencherOsDadosDoFormulario(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Clicar no botão registrar")]
        public void QuandoClicarNoBotaoRegistrar()
        {
            ScenarioContext.Current.Pending();
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
