using Xunit;

namespace Demo.Tests
{
    public class AssertStringsTests
    {
        [Fact(DisplayName = "StringsTools - Deve Unir Os Nomes")]
        [Trait("Categoria", "Testes Basicos - AssertStringsTests")]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Equal("Eduardo Pires", nomeCompleto);
        }

        [Fact(DisplayName = "StringsTools - Deve Unir Os Nomes Ignorando O Case")]
        [Trait("Categoria", "Testes Basicos - AssertStringsTests")]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Equal("EDUARDO PIRES", nomeCompleto, ignoreCase: true);
        }

        [Fact(DisplayName = "StringsTools - Deve Unir Os Nomes E Conter Trecho")]
        [Trait("Categoria", "Testes Basicos - AssertStringsTests")]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Contains("ardo", nomeCompleto);
        }

        [Fact(DisplayName = "StringsTools - Deve Unir Os Nomes E Começar Com")]
        [Trait("Categoria", "Testes Basicos - AssertStringsTests")]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.StartsWith("Edu", nomeCompleto);
        }

        [Fact(DisplayName = "StringsTools - Deve Unir Os Nomes E Terminar Com")]
        [Trait("Categoria", "Testes Basicos - AssertStringsTests")]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.EndsWith("res", nomeCompleto);
        }

        [Fact(DisplayName = "StringsTools - Deve Unir Os Nomes E Ser Valido Com Expressão Regular")]
        [Trait("Categoria", "Testes Basicos - AssertStringsTests")]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }
    }
}
