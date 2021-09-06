using Features.Clientes;
using Features.Tests.DadosHumanos;
using MediatR;
using Moq;
using Moq.AutoMock;
using System.Threading;
using Xunit;

namespace Features.Tests.AutoMock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockerTests
    {
        private readonly ClienteBogusFixture _clienteFixture;

        public ClienteServiceAutoMockerTests(ClienteBogusFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;
        }

        [Fact(DisplayName = "ClienteService - Adicionar Cliente Com Sucesso")]
        [Trait("Categoria", "AutoMock - ClienteServiceAutoMockerTests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteValido();

            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "ClienteService - Adicionar Cliente Com Falha")]
        [Trait("Categoria", "AutoMock - ClienteServiceAutoMockerTests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteInvalido();

            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "ClienteService - Obter Clientes Ativos")]
        [Trait("Categoria", "AutoMock - ClienteServiceAutoMockerTests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>();

            mocker.GetMock<IClienteRepository>().Setup(r => r.ObterTodos())
                .Returns(_clienteFixture.GerarClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.NotEmpty(clientes);
            Assert.DoesNotContain(clientes, c => !c.Ativo);
        }
    }
}
