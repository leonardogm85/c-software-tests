using NerdStore.Core.DomainObjects;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Pedido - Adicionar Novo Item De Pedido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Pedido - Adicionar Item De Pedido Existente")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produtoId = Guid.NewGuid();

            var pedidoItem1 = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", 1, 100);

            pedido.AdicionarItem(pedidoItem1);

            // Act
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Single(pedido.PedidoItems);
            Assert.Contains(pedido.PedidoItems, item => item.ProdutoId == produtoId && item.Quantidade == 3);
        }

        [Fact(DisplayName = "Pedido - Adicionar Quantidade do Item De Pedido Acima Do Permitido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AdicionarItemPedido_UnidadesItemAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
        }

        [Fact(DisplayName = "Pedido - Adicionar Quantidade do Item De Pedido Existente Acima Do Permitido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AdicionarItemPedido_ItemExistenteSomaUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produtoId = Guid.NewGuid();

            var pedidoItem1 = new PedidoItem(produtoId, "Produto Teste", 1, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM, 100);

            pedido.AdicionarItem(pedidoItem1);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));
        }

        [Fact(DisplayName = "Pedido - Atualizar Item De Pedido Inexistente")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItem));
        }

        [Fact(DisplayName = "Pedido - Atualizar Item De Pedido Valido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produtoId = Guid.NewGuid();

            var itemAdicionado = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            var itemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);

            pedido.AdicionarItem(itemAdicionado);

            // Act
            pedido.AtualizarItem(itemAtualizado);

            // Assert
            Assert.Contains(pedido.PedidoItems, item => item.ProdutoId == produtoId && item.Quantidade == 5);
        }

        [Fact(DisplayName = "Pedido - Atualizar Item De Pedido Valido Com Valor Total Calculado")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produtoId = Guid.NewGuid();

            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto 1", 2, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto 2", 3, 50);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var itemAtualizado2 = new PedidoItem(produtoId, "Produto 2", 5, 50);

            var valorTotal = pedidoItem1.Quantidade * pedidoItem1.ValorUnitario
                + itemAtualizado2.Quantidade * itemAtualizado2.ValorUnitario;

            // Act
            pedido.AtualizarItem(itemAtualizado2);

            // Assert
            Assert.Equal(valorTotal, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Pedido - Atualizar Item De Pedido Com Quantidade Acima Do Permitido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void AtualizarItemPedido_ItemUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produtoId = Guid.NewGuid();

            var itemAdicionado = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            var itemAtualizado = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            pedido.AdicionarItem(itemAdicionado);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(itemAtualizado));
        }

        [Fact(DisplayName = "Pedido - Remover Item De Pedido Inexistente")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void RemoverItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var itemPedido = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(itemPedido));
        }

        [Fact(DisplayName = "Pedido - Remover Item De Pedido Valido Com Valor Total Calculado")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoTests")]
        public void RemoverItemPedido_ItemExistente_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produtoId = Guid.NewGuid();

            var pedidoItem1 = new PedidoItem(Guid.NewGuid(), "Produto 1", 2, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto 2", 3, 50);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var valorTotal = pedidoItem2.Quantidade * pedidoItem2.ValorUnitario;

            // Act
            pedido.RemoverItem(pedidoItem1);

            // Assert
            Assert.Equal(valorTotal, pedido.ValorTotal);
        }
    }
}
