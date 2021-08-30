using Features.Tests.Fixtures;
using Xunit;

namespace Features.Tests.DadosHumanos
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteBogusTests
    {
        private readonly ClienteBogusFixture _clienteFixture;

        public ClienteBogusTests(ClienteBogusFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;
        }

        [Fact(DisplayName = "Cliente - Deve Estar Válido")]
        [Trait("Categoria", "Dados Humanos - ClienteBogusTests")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Empty(cliente.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Cliente - Deve Estar Inválido")]
        [Trait("Categoria", "Dados Humanos - ClienteBogusTests")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }
    }
}
