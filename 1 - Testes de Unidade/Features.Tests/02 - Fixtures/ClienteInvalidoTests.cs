using Xunit;

namespace Features.Tests.Fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteInvalidoTests
    {
        private readonly ClienteAutoMockerFixture _clienteFixture;

        public ClienteInvalidoTests(ClienteAutoMockerFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;
        }

        [Fact(DisplayName = "Cliente - Deve Estar Inválido")]
        [Trait("Categoria", "Fixture - ClienteInvalidoTests")]
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
