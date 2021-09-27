using MediatR;
using Moq;
using Moq.AutoMock;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class PedidoCommandHandlerTests
    {
        [Fact(DisplayName = "PedidoCommandHandler - Adicionar Item Novo Pedido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Application.Tests.PedidoCommandHandlerTests")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Produto 1",
                2,
                100);

            var mocker = new AutoMocker();

            var pedidoHandler = mocker.CreateInstance<PedidoCommandHandler>();

            mocker.GetMock<IPedidoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var result = await pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);

            mocker.GetMock<IPedidoRepository>().Verify(r => r.Adicionar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

            // mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "PedidoCommandHandler - Adicionar Novo Item Pedido Ao Rascunho")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Application.Tests.PedidoCommandHandlerTests")]
        public async Task AdicionarItem_NovoItemAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange
            var clienteId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clienteId);

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto 1", 2, 100);

            pedido.AdicionarItem(pedidoItem);

            var pedidoCommand = new AdicionarItemPedidoCommand(
                clienteId,
                Guid.NewGuid(),
                "Produto 2",
                5,
                50);

            var mocker = new AutoMocker();

            var pedidoHandler = mocker.CreateInstance<PedidoCommandHandler>();

            mocker.GetMock<IPedidoRepository>()
                .Setup(r => r.ObterPedidoRascunhoPorClienteId(clienteId))
                .Returns(Task.FromResult(pedido));

            mocker.GetMock<IPedidoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var result = await pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);

            mocker.GetMock<IPedidoRepository>().Verify(r => r.AdicionarItem(It.IsAny<PedidoItem>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "PedidoCommandHandler - Adicionar Item Existente Ao Pedido Rascunho")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Application.Tests.PedidoCommandHandlerTests")]
        public async Task AdicionarItem_ItemExistenteAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var produtoId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clienteId);

            var pedidoItem = new PedidoItem(produtoId, "Produto 1", 2, 100);

            pedido.AdicionarItem(pedidoItem);

            var pedidoCommand = new AdicionarItemPedidoCommand(
                clienteId,
                produtoId,
                "Produto 1",
                5,
                100);

            var mocker = new AutoMocker();

            var pedidoHandler = mocker.CreateInstance<PedidoCommandHandler>();

            mocker.GetMock<IPedidoRepository>()
                .Setup(r => r.ObterPedidoRascunhoPorClienteId(clienteId))
                .Returns(Task.FromResult(pedido));

            mocker.GetMock<IPedidoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var result = await pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);

            mocker.GetMock<IPedidoRepository>().Verify(r => r.AtualizarItem(It.IsAny<PedidoItem>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "PedidoCommandHandler - Adicionar Item Command Inválido")]
        [Trait("Categoria", "TDD - NerdStore.Vendas.Application.Tests.PedidoCommandHandlerTests")]
        public async Task AdicionarItem_CommandInvalido_DeveRetornarFalsoELancarEventosDeNotificacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(
                Guid.Empty,
                Guid.Empty,
                string.Empty,
                0,
                0);

            var mocker = new AutoMocker();

            var pedidoHandler = mocker.CreateInstance<PedidoCommandHandler>();

            //mocker.GetMock<IPedidoRepository>()
            //    .Setup(r => r.UnitOfWork.Commit())
            //    .Returns(Task.FromResult(false));

            // Act
            var result = await pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.False(result);

            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(5));

            //mocker.GetMock<IPedidoRepository>().Verify(r => r.Adicionar(It.IsAny<Pedido>()), Times.Once);
            //mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}
