using Features.Tests.DadosHumanos;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Features.Tests.FluentAssertions
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteFluentAssertionsTests
    {
        private readonly ClienteBogusFixture _clienteFixture;
        private readonly ITestOutputHelper _outputHelper;

        public ClienteFluentAssertionsTests(ClienteBogusFixture clienteFixture, ITestOutputHelper outputHelper)
        {
            _clienteFixture = clienteFixture;
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "Cliente - Deve Estar Válido")]
        [Trait("Categoria", "FluentAssertions - ClienteFluentAssertionsTests")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            result.Should().BeTrue(because: "O cliente deve ser válido");
            cliente.ValidationResult.Errors.Should().BeEmpty(because: "Não deve ter erros de validação");
        }

        [Fact(DisplayName = "Cliente - Deve Estar Inválido")]
        [Trait("Categoria", "FluentAssertions - ClienteFluentAssertionsTests")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert
            result.Should().BeFalse(because: "O cliente deve ser inválido");
            cliente.ValidationResult.Errors.Should().NotBeEmpty(because: "Deve ter erros de validação");

            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");
        }
    }
}
