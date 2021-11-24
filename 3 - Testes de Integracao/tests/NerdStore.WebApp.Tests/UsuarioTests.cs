using Bogus;
using NerdStore.WebApp.Mvc;
using NerdStore.WebApp.Tests.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _testsFixture;

        public UsuarioTests(IntegrationTestsFixture<StartupWebTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "IntegrationTests - Realizar Cadastros Com Sucesso")]
        [Trait("Categoria", "Teste de Integração - Usuário")]
        public async Task Usuario_RealizaCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            var urlRequest = "/Identity/Account/Register";

            var initialResponse = await _testsFixture.Client.GetAsync(urlRequest);

            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryTokenValue = _testsFixture
                .GetAntiForgeryTokenValue(await initialResponse.Content.ReadAsStringAsync());

            _testsFixture.GenerateUser();

            var formData = new Dictionary<string, string>
            {
                { _testsFixture.AntiForgeryTokenName, antiForgeryTokenValue },
                { "Input.Email", _testsFixture.UserEmail },
                { "Input.Password", _testsFixture.UserPassword },
                { "Input.ConfirmPassword", _testsFixture.UserPassword }
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, urlRequest)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();

            Assert.Contains($"Register confirmation", responseString);
        }
    }
}
