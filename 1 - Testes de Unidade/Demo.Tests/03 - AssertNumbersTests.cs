using Xunit;

namespace Demo.Tests
{
    public class AssertNumbersTests
    {
        [Fact(DisplayName = "Calculadora - Somar 1 + 2 Deve Ser Igual a 3")]
        [Trait("Categoria", "Testes Basicos - AssertNumbersTests")]
        public void Calculadora_Somar_DeveSerIgual()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(1, 2);

            // Assert
            Assert.Equal(3, resultado);
        }

        [Fact(DisplayName = "Calculadora - Somar 1,13123123123 + 2,2312313123 Deve Ser Diferente a 3,3")]
        [Trait("Categoria", "Testes Basicos - AssertNumbersTests")]
        public void Calculadora_Somar_NaoDeveSerIgual()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(1.13123123123, 2.2312313123);

            // Assert
            Assert.NotEqual(3.3, resultado, 1);
        }
    }
}
