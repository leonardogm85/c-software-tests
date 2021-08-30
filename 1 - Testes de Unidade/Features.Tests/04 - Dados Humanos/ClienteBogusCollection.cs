using Xunit;

namespace Features.Tests.Fixtures
{
    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteBogusFixture>
    {
    }
}
