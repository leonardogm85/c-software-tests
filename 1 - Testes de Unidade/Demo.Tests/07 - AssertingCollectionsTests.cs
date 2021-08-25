using Xunit;

namespace Demo.Tests
{
    public class AssertingCollectionsTests
    {
        [Fact(DisplayName = "Funcionário - Deve Possuir Habilidades")]
        [Trait("Categoria", "Testes Basicos - AssertingCollectionsTests")]
        public void FuncionarioFactory_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.All(funcionario.Habilidades, habilidade => Assert.False(string.IsNullOrEmpty(habilidade)));
        }

        [Fact(DisplayName = "Funcionário - Junior Deve Possuir Habilidades Basicas")]
        [Trait("Categoria", "Testes Basicos - AssertingCollectionsTests")]
        public void FuncionarioFactory_Habilidades_JuniorDevePossuirHabilidadeBasicas()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            // Assert
            Assert.Contains("OOP", funcionario.Habilidades);
        }

        [Fact(DisplayName = "Funcionário - Junior Não Deve Possuir Habilidades Avançadas")]
        [Trait("Categoria", "Testes Basicos - AssertingCollectionsTests")]
        public void FuncionarioFactory_Habilidades_JuniorNaoDevePossuirHabilidadeAvancada()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            // Assert
            Assert.DoesNotContain("Microservices", funcionario.Habilidades);
        }

        [Fact(DisplayName = "Funcionário - Sênior Deve Possuir Todas As Habilidades")]
        [Trait("Categoria", "Testes Basicos - AssertingCollectionsTests")]
        public void FuncionarioFactory_Habilidades_SeniorDevePossuirTodasHabilidades()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 15000);

            var habilidades = new[]
            {
                "Lógica de Programação",
                "OOP",
                "Testes",
                "Microservices"
            };

            // Assert
            Assert.Equal(habilidades, funcionario.Habilidades);
        }
    }
}
