using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Domain;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "AdicionarItemPedidoCommand - Adicionar Item Command Válido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Application.Tests.AdicionarItemPedidoCommandTests")]
        public void AdicionarItemPedidoCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Produto 1",
                2,
                100);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "AdicionarItemPedidoCommand - Adicionar Item Command Inválido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Application.Tests.AdicionarItemPedidoCommandTests")]
        public void AdicionarItemPedidoCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.Empty,
                Guid.Empty,
                string.Empty,
                0,
                0);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoCommandValidation.IdClienteErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoCommandValidation.IdProdutoErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoCommandValidation.NomeErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoCommandValidation.QuantidadeMinimaErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoCommandValidation.ValorErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "AdicionarItemPedidoCommand - Adicionar Item Command Quantidade Acima do Permitido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Application.Tests.AdicionarItemPedidoCommandTests")]
        public void AdicionarItemPedidoCommand_QuantidadeUnidadesSuperiorAoPermitido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Produto 1",
                Pedido.MaxUnidadesItem + 1,
                100);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoCommandValidation.QuantidadeMaximaErroMsg, pedidoCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
