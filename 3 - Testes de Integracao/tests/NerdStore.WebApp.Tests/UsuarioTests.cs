using NerdStore.WebApp.Mvc;
using NerdStore.WebApp.Tests.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [TestCaseOrderer(PriorityOrderer.ordererTypeName, PriorityOrderer.ordererAssemblyName)]
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _testsFixture;

        public UsuarioTests(IntegrationTestsFixture<StartupWebTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Priority(2)]
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

            Assert.Contains($"Hello {_testsFixture.UserEmail}!", responseString);
        }

        [Priority(1)]
        [Fact(DisplayName = "IntegrationTests - Realizar Cadastros Com Senha Fraca")]
        [Trait("Categoria", "Teste de Integração - Usuário")]
        public async Task Usuario_RealizaCadastroComSenhaFraca_DeveRetornarMensagemDeErro()
        {
            // Arrange
            var urlRequest = "/Identity/Account/Register";

            var initialResponse = await _testsFixture.Client.GetAsync(urlRequest);

            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryTokenValue = _testsFixture
                .GetAntiForgeryTokenValue(await initialResponse.Content.ReadAsStringAsync());

            _testsFixture.GenerateUser();

            var weakPassword = "123456";

            var formData = new Dictionary<string, string>
            {
                { _testsFixture.AntiForgeryTokenName, antiForgeryTokenValue },
                { "Input.Email", _testsFixture.UserEmail },
                { "Input.Password", weakPassword },
                { "Input.ConfirmPassword", weakPassword }
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

            Assert.Contains("Passwords must have at least one non alphanumeric character.", responseString);
            Assert.Contains("Passwords must have at least one lowercase (&#x27;a&#x27;-&#x27;z&#x27;).", responseString);
            Assert.Contains("Passwords must have at least one uppercase (&#x27;A&#x27;-&#x27;Z&#x27;).", responseString);
        }

        [Priority(3)]
        [Fact(DisplayName = "IntegrationTests - Realizar Login Com Sucesso")]
        [Trait("Categoria", "Teste de Integração - Usuário")]
        public async Task Usuario_RealizaLogin_DeveExecutarComSucesso()
        {
            // Arrange
            var urlRequest = "/Identity/Account/Login";

            var initialResponse = await _testsFixture.Client.GetAsync(urlRequest);

            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryTokenValue = _testsFixture
                .GetAntiForgeryTokenValue(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                { _testsFixture.AntiForgeryTokenName, antiForgeryTokenValue },
                { "Input.Email", _testsFixture.UserEmail },
                { "Input.Password", _testsFixture.UserPassword }
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

            Assert.Contains($"Hello {_testsFixture.UserEmail}!", responseString);
        }
    }
}
