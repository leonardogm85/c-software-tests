using Xunit;

namespace NerdStore.Bdd.Tests.Config
{
    [CollectionDefinition(nameof(AutomacaoWebTestsFixtureCollection))]
    public class AutomacaoWebTestsFixtureCollection : ICollectionFixture<AutomacaoWebTestsFixture>
    {
    }
}
