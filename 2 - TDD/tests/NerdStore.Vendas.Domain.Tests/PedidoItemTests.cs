using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoItemTests
    {
        [Fact(DisplayName = "PedidoItem - Novo item pedido com unidades abaixo do permitido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Domain.Tests.PedidoItemTests")]
        public void NovoItemPedido_UnidadesItemAbaixoDoPermitido_DeveRetornarException()
        {
            // Arrange, Act & Assert
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Produto Teste", Pedido.MinUnidadesItem - 1, 100));
        }
    }
}
