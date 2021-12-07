using NerdStore.Bdd.Tests.Config;

namespace NerdStore.Bdd.Tests.Usuario
{
    public abstract class BaseUsuarioTela : PageObjectModel
    {
        public BaseUsuarioTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void AcessarSiteLoja()
        {
            Helper.IrParaUrl(Helper.Configuration.VitrineUrl);
        }

        public bool ValidarSaudacaoUsuarioLogado(UsuarioModel usuario)
        {
            return Helper.ObterTextoElementoPorId("SaudacaoUsuario").Contains(usuario.Email);
        }

        public bool ValidarMensagemDeErroFormulario(string mensagem)
        {
            return Helper.ObterElementoPorXpath("//div[@class='text-danger validation-summary-errors']/ul/li").Text.Equals(mensagem);
        }
    }
}
