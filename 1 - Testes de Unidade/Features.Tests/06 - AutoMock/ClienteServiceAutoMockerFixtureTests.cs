using Features.Clientes;
using MediatR;
using Moq;
using System.Threading;
using Xunit;

namespace Features.Tests.AutoMock
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceFluentAssertionsTests
    {
        private readonly ClienteAutoMockerFixture _clienteFixture;
        private readonly ClienteService _clienteService;

        public ClienteServiceFluentAssertionsTests(ClienteAutoMockerFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;
            _clienteService = _clienteFixture.ObterClienteService();
        }

        [Fact(DisplayName = "ClienteService - Adicionar Cliente Com Sucesso")]
        [Trait("Categoria", "AutoMock - ClienteServiceAutoMockerFixtureTests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "ClienteService - Adicionar Cliente Com Falha")]
        [Trait("Categoria", "AutoMock - ClienteServiceAutoMockerFixtureTests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteInvalido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "ClienteService - Obter Clientes Ativos")]
        [Trait("Categoria", "AutoMock - ClienteServiceAutoMockerFixtureTests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Setup(r => r.ObterTodos())
                .Returns(_clienteFixture.GerarClientesVariados());

            // Act
            var clientes = _clienteService.ObterTodosAtivos();

            // Assert
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.NotEmpty(clientes);
            Assert.DoesNotContain(clientes, c => !c.Ativo);
        }
    }
}
