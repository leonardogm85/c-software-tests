using Xunit;

namespace Features.Tests.Skip
{
    public class SkipTests
    {
        [Fact(DisplayName = "Cliente - Novo Cliente 2.0", Skip = "Nova Versão 2.0 Quebrando")]
        [Trait("Categoria", "Skip - SkipTests")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(false);
        }
    }
}
