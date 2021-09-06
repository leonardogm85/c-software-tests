using Features.Clientes;
using Features.Tests.DadosHumanos;
using MediatR;
using Moq;
using System.Threading;
using Xunit;

namespace Features.Tests.Mock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteBogusFixture _clienteFixture;

        public ClienteServiceTests(ClienteBogusFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;
        }

        [Fact(DisplayName = "ClienteService - Adicionar Cliente Com Sucesso")]
        [Trait("Categoria", "Mock - ClienteServiceTests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteValido();

            var clienteRepository = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepository.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Once);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "ClienteService - Adicionar Cliente Com Falha")]
        [Trait("Categoria", "Mock - ClienteServiceTests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteInvalido();

            var clienteRepository = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepository.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Never);
            mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "ClienteService - Obter Clientes Ativos")]
        [Trait("Categoria", "Mock - ClienteServiceTests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var clienteRepository = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            clienteRepository.Setup(r => r.ObterTodos())
                .Returns(_clienteFixture.GerarClientesVariados());

            var clienteService = new ClienteService(clienteRepository.Object, mediator.Object);

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            clienteRepository.Verify(r => r.ObterTodos(), Times.Once);
            Assert.NotEmpty(clientes);
            Assert.DoesNotContain(clientes, c => !c.Ativo);
        }
    }
}
