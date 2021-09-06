using Xunit;

namespace Features.Tests.AutoMock
{
    [CollectionDefinition(nameof(ClienteAutoMockerCollection))]
    public class ClienteAutoMockerCollection : ICollectionFixture<ClienteAutoMockerFixture>
    {
    }
}
