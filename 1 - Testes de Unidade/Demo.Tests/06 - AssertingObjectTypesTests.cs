using Xunit;

namespace Demo.Tests
{
    public class AssertingObjectTypesTests
    {
        [Fact(DisplayName = "FuncionarioFactory - Criar Deve Retornar Tipo Valido De Funcionário")]
        [Trait("Categoria", "Testes Basicos - AssertingObjectTypesTests")]
        public void FuncionarioFactory_Criar_DeveRetornarTipoFuncionario()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.IsType<Funcionario>(funcionario);
        }

        [Fact(DisplayName = "FuncionarioFactory - Criar Deve Retornar Tipo Derivado De Pessoa")]
        [Trait("Categoria", "Testes Basicos - AssertingObjectTypesTests")]
        public void FuncionarioFactory_Criar_DeveRetornarTipoDerivadoPessoa()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.IsAssignableFrom<Pessoa>(funcionario);
        }
    }
}
