using NerdStore.Bdd.Tests.Config;

namespace NerdStore.Bdd.Tests.Pedido
{
    public class PedidoTela : PageObjectModel
    {
        public PedidoTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void AcessarVitrineDeProdutos()
        {
            Helper.IrParaUrl(Helper.Configuration.VitrineUrl);
        }

        public void ObterDetalhesDoProduto(int posicao = 1)
        {
            Helper.ClicarPorXpath($"html/body/div/main/div/div/div[{posicao}]/span/a");
        }

        public bool ValidarProdutoDisponivel()
        {
            return Helper.ValidarConteudoUrl(Helper.Configuration.ProdutoUrl);
        }
    }
}
