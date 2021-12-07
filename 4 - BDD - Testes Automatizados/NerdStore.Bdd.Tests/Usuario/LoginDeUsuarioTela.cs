using NerdStore.Bdd.Tests.Config;

namespace NerdStore.Bdd.Tests.Usuario
{
    public class LoginDeUsuarioTela : BaseUsuarioTela
    {
        public LoginDeUsuarioTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void ClicarNoLinkLogin()
        {
            Helper.ClicarLinkPorTexto("Login");
        }

        public void PreencherFormularioLogin(UsuarioModel usuario)
        {
            Helper.PreencherTextBoxPorId("Input_Email", usuario.Email);
            Helper.PreencherTextBoxPorId("Input_Password", usuario.Senha);
        }

        public bool ValidarPreenchimentoFormularioLogin(UsuarioModel usuario)
        {
            if (Helper.ObterValorTextBoxPorId("Input_Email") != usuario.Email)
            {
                return false;
            }

            if (Helper.ObterValorTextBoxPorId("Input_Password") != usuario.Senha)
            {
                return false;
            }

            return true;
        }

        public void ClicarNoBotaoLogin()
        {
            Helper.ClicarBotaoPorId("login-submit");
        }

        public bool Login(UsuarioModel usuario)
        {
            AcessarSiteLoja();
            ClicarNoLinkLogin();
            PreencherFormularioLogin(usuario);

            if (!ValidarPreenchimentoFormularioLogin(usuario))
            {
                return false;
            }

            ClicarNoBotaoLogin();

            if (!ValidarSaudacaoUsuarioLogado(usuario))
            {
                return false;
            }

            return true;
        }
    }
}
