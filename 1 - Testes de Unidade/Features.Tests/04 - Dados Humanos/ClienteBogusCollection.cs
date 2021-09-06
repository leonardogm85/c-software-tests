using Xunit;

namespace Features.Tests.DadosHumanos
{
    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteBogusFixture>
    {
    }
}
