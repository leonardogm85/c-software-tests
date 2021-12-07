using NerdStore.Bdd.Tests.Config;
using NerdStore.Bdd.Tests.Usuario;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.Bdd.Tests.Pedido
{
    [Binding]
    [Collection(nameof(AutomacaoWebTestsFixtureCollection))]
    public class Pedido_AdicionarItemAoCarrinhoSteps
    {
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly PedidoTela _pedidoTela;
        private readonly LoginDeUsuarioTela _loginDeUsuarioTela;

        private string _urlProduto;

        public Pedido_AdicionarItemAoCarrinhoSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _pedidoTela = new PedidoTela(_testsFixture.BrowserHelper);
            _loginDeUsuarioTela = new LoginDeUsuarioTela(_testsFixture.BrowserHelper);
        }

        [Given(@"O usuário esteja logado")]
        public void DadoOUsuarioEstejaLogado()
        {
            // Arrange
            _testsFixture.Usuario = new UsuarioModel
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };

            // Act
            var logado = _loginDeUsuarioTela.Login(_testsFixture.Usuario);

            // Assert
            Assert.True(logado);
        }

        [Given(@"Que um produto esteja na vitrine")]
        public void DadoQueUmProdutoEstejaNaVitrine()
        {
            // Arrange
            _pedidoTela.AcessarVitrineDeProdutos();

            // Act
            _pedidoTela.ObterDetalhesDoProduto();
            _urlProduto = _pedidoTela.ObterUrl();

            // Assert
            Assert.True(_pedidoTela.ValidarProdutoDisponivel());
        }

        [Given(@"Esteja disponível no estoque")]
        public void DadoEstejaDisponivelNoEstoque()
        {
            // Assert
            Assert.True(_pedidoTela.ObterQuantidadeNoEstoque() > 0);
        }

        [Given(@"Não tenha nenhum produto adicionado ao carrinho")]
        public void DadoNaoTenhaNenhumProdutoAdicionadoAoCarrinho()
        {
            // Act
            _pedidoTela.NavegarParaCarrinhoDeCompras();
            _pedidoTela.ZeraCarrinhoDeCompras();

            // Assert
            Assert.Equal(0, _pedidoTela.ObterValorTotalCarrinho());

            _pedidoTela.NavegarParaUrl(_urlProduto);
        }

        [Given(@"O mesmo produto já tenha sido adicionado ao carrinho anteriormente")]
        public void DadoOMesmoProdutoJaTenhaSidoAdicionadoAoCarrinhoAnteriormente()
        {
            // Act
            _pedidoTela.NavegarParaCarrinhoDeCompras();
            _pedidoTela.ZeraCarrinhoDeCompras();
            _pedidoTela.AcessarVitrineDeProdutos();
            _pedidoTela.ObterDetalhesDoProduto();
            _pedidoTela.ClicarEmComprarAgora();

            // Assert
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompras());

            _pedidoTela.VoltarNavegacao();
        }

        [When(@"O usuário adicionar uma unidade ao carrinho")]
        public void QuandoOUsuarioAdicionarUmaUnidadeAoCarrinho()
        {
            // Act
            _pedidoTela.ClicarEmComprarAgora();
        }

        [When(@"O usuário adicionar um item acima da quantidade máxima permitida")]
        public void QuandoOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            // Arrange
            _pedidoTela.ClicarAdicionarQuantidadeItens(Vendas.Domain.Pedido.MaxUnidadesItem + 1);

            // Act
            _pedidoTela.ClicarEmComprarAgora();
        }

        [When(@"O usuário adicionar a quantidade máxima permitida ao carrinho")]
        public void QuandoOUsuarioAdicionarAQuantidadeMaximaPermitidaAoCarrinho()
        {
            // Arrange
            _pedidoTela.ClicarAdicionarQuantidadeItens(Vendas.Domain.Pedido.MaxUnidadesItem);

            // Act
            _pedidoTela.ClicarEmComprarAgora();
        }

        [Then(@"O usuário será redirecionado ao resumo da compra")]
        public void EntaoOUsuarioSeraRedirecionadoAoResumoDaCompra()
        {
            // Assert
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompras());
        }

        [Then(@"O valor total do pedido será exatamente o valor do item adicionado")]
        public void EntaoOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionado()
        {
            // Arrange
            var valorUnitorio = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var valorTotal = _pedidoTela.ObterValorTotalCarrinho();

            // Assert
            Assert.Equal(valorUnitorio, valorTotal);
        }

        [Then(@"Reberá uma mensagem de erro mencionando que foi ultrapassada a quantidade limite")]
        public void EntaoReberaUmaMensagemDeErroMencionandoQueFoiUltrapassadaAQuantidadeLimite()
        {
            // Arrange
            var mensagem = _pedidoTela.ObterMensagemDeErroProduto();

            // Assert
            Assert.Equal($"A quantidade máxima de um item é {Vendas.Domain.Pedido.MaxUnidadesItem}.", mensagem);
        }

        [Then(@"A quantidade de itens daquele produto terá sido acrescentada em uma unidade a mais")]
        public void EntaoAQuantidadeDeItensDaqueleProdutoTeraSidoAcrescentadaEmUmaUnidadeAMais()
        {
            // Assert
            Assert.True(_pedidoTela.ObterQuantidadeDeItensPrimeiroProdutoCarrinho() == 2);
        }

        [Then(@"O valor total do pedido será a multiplicação da quantidade de itens pelo valor unitário")]
        public void EntaoOValorTotalDoPedidoSeraAMultiplicacaoDaQuantidadeDeItensPeloValorUnitario()
        {
            // Arrange
            var valorUnitorio = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var valorTotal = _pedidoTela.ObterValorTotalCarrinho();
            var quantidade = _pedidoTela.ObterQuantidadeDeItensPrimeiroProdutoCarrinho();

            // Assert
            Assert.Equal(valorUnitorio * quantidade, valorTotal);
        }
    }
}
