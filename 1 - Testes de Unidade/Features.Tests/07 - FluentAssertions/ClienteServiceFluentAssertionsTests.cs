using Features.Clientes;
using Features.Tests.AutoMock;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using System.Threading;
using Xunit;

namespace Features.Tests.FluentAssertions
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
        [Trait("Categoria", "FluentAssertions - ClienteServiceFluentAssertionsTests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            cliente.EhValido().Should().BeTrue();
            cliente.ValidationResult.Errors.Should().BeEmpty();
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "ClienteService - Adicionar Cliente Com Falha")]
        [Trait("Categoria", "FluentAssertions - ClienteServiceFluentAssertionsTests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteInvalido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            cliente.EhValido().Should().BeFalse();
            cliente.ValidationResult.Errors.Should().NotBeEmpty();
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "ClienteService - Obter Clientes Ativos")]
        [Trait("Categoria", "FluentAssertions - ClienteServiceFluentAssertionsTests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Setup(r => r.ObterTodos())
                .Returns(_clienteFixture.GerarClientesVariados());

            // Act
            var clientes = _clienteService.ObterTodosAtivos();

            // Assert
            _clienteFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);

            clientes
                .Should()
                .NotBeEmpty()
                .And
                .OnlyHaveUniqueItems()
                .And
                .NotContain(c => !c.Ativo);

            _clienteService
                .ExecutionTimeOf(c => c.ObterTodosAtivos())
                .Should()
                .BeLessOrEqualTo(50.Milliseconds());
        }
    }
}
