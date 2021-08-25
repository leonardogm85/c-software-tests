using System;
using Xunit;

namespace Demo.Tests
{
    public class AssertingExceptionsTests
    {
        [Fact(DisplayName = "Calculadora - Deve Retornar Erro De Divisão Por Zero")]
        [Trait("Categoria", "Testes Basicos - AssertingExceptionsTests")]
        public void Calculadora_Dividir_DeveRetornarErroDivisaoPorZero()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(10, 0));
        }

        [Fact(DisplayName = "Funcionário - Deve Retornar Erro Salarial Inferior Ao Permitido")]
        [Trait("Categoria", "Testes Basicos - AssertingExceptionsTests")]
        public void Calculadora_Dividir_DeveRetornarErroSalarioInferiorPermitido()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<Exception>(() => FuncionarioFactory.Criar("Eduardo", 250));

            Assert.Equal("Salario inferior ao permitido", exception.Message);
        }
    }
}
