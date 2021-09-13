using Features.Core;
using FluentAssertions;
using Xunit;

namespace Features.Tests.CodeCoverage
{
    public class CpfValidationTests
    {
        [Theory(DisplayName = "CpfValidation - Todos Os CPFs Devem ser validos")]
        [Trait("Categoria", "Code Coverage - CpfValidationTests")]
        [InlineData("15647413544")]
        [InlineData("93852162289")]
        [InlineData("43030324257")]
        [InlineData("76952136754")]
        [InlineData("13830803800")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerValidos(string cpf)
        {
            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.EhValido(cpf).Should().BeTrue();
        }

        [Theory(DisplayName = "CpfValidation - Todos Os CPFs Devem ser invalidos")]
        [Trait("Categoria", "Code Coverage - CpfValidationTests")]
        [InlineData("25647413544000")]
        [InlineData("25647413544")]
        [InlineData("83852162289")]
        [InlineData("53030324257")]
        [InlineData("66952136754")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerInvalidos(string cpf)
        {
            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.EhValido(cpf).Should().BeFalse();
        }
    }
}
