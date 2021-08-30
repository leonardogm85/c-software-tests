using Xunit;

namespace Features.Tests.Fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteValidoTests
    {
        private readonly ClienteFixture _clienteFixture;

        public ClienteValidoTests(ClienteFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;
        }

        [Fact(DisplayName = "Cliente - Deve Estar Válido")]
        [Trait("Categoria", "Fixture - ClienteValidoTests")]
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
    }
}
