using Xunit;

namespace Features.Tests.Traits
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteFixture>
    {
    }
}
