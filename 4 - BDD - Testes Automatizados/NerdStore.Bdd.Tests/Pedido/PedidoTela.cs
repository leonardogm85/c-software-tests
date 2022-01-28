using NerdStore.Bdd.Tests.Config;
using System;
using System.Text.RegularExpressions;

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
            Helper.ClicarPorXpath($"/html/body/div/main/div/div/div[{posicao}]/span/a");
        }

        public bool ValidarProdutoDisponivel()
        {
            return Helper.ValidarConteudoUrl(Helper.Configuration.ProdutoUrl);
        }

        public int ObterQuantidadeNoEstoque()
        {
            var elemento = Helper.ObterElementoPorXpath("/html/body/div/main/div/div/div[2]/p[1]");
            var quantidade = Regex.Replace(elemento.Text, @"^Apenas (\d+) em estoque!$", "$1");

            if (char.IsNumber(quantidade, 0))
            {
                return Convert.ToInt32(quantidade);
            }

            return 0;
        }

        public void ClicarEmComprarAgora()
        {
            Helper.ClicarPorXpath("/html/body/div/main/div/div/div[2]/form/div[2]/button");
        }

        public bool ValidarSeEstaNoCarrinhoDeCompras()
        {
            return Helper.ValidarConteudoUrl(Helper.Configuration.CarrinhoUrl);
        }

        public decimal ObterValorUnitarioProdutoCarrinho()
        {
            var regex = new Regex(@"^.{3}(([1-9]\d{0,2}(?:\.\d{3})*|0),\d{2})$");

            var textoElemento = Helper.ObterTextoElementoPorId("ValorUnitario");
            var valueElemento = Convert.ToDecimal(regex.Replace(textoElemento, "$1"));

            return valueElemento;
        }

        public decimal ObterValorTotalCarrinho()
        {
            var regex = new Regex(@"^.{3}(([1-9]\d{0,2}(?:\.\d{3})*|0),\d{2})$");

            var textoElemento = Helper.ObterTextoElementoPorId("ValorTotalCarrinho");
            var valueElemento = Convert.ToDecimal(regex.Replace(textoElemento, "$1"));

            return valueElemento;
        }

        public void ClicarAdicionarQuantidadeItens(int quantidade)
        {
            var botaoAdicionar = Helper.ObterElementoPorClasseCss("btn-plus");

            if (botaoAdicionar == null)
            {
                return;
            }

            for (var i = 0; i < quantidade; i++)
            {
                botaoAdicionar.Click();
            }
        }

        public string ObterMensagemDeErroProduto()
        {
            return Helper.ObterElementoPorXpath("/html/body/div/main/div/div[1]/p").Text;
        }

        public void NavegarParaCarrinhoDeCompras()
        {
            Helper.ObterElementoPorXpath("/html/body/header/nav/div/div/ul[2]/li[3]/a").Click();
        }

        public int ObterQuantidadeDeItensPrimeiroProdutoCarrinho()
        {
            return Convert.ToInt32(Helper.ObterElementoPorId("Quantidade").GetAttribute("value"));
        }

        public void VoltarNavegacao(int vezes = 1)
        {
            Helper.VoltarNavegacao(vezes);
        }

        public void ZeraCarrinhoDeCompras()
        {
            while (ObterValorTotalCarrinho() > 0)
            {
                Helper.ClicarPorXpath("/html/body/div/main/div/div/div/table/tbody/tr[1]/td[5]/form/button");
            }
        }
    }
}
