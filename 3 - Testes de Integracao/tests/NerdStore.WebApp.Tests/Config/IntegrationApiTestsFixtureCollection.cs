﻿using NerdStore.WebApp.Mvc;
using Xunit;

namespace NerdStore.WebApp.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>>
    {
    }
}
