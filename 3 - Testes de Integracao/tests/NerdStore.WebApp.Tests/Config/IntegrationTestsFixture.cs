using AngleSharp.Html.Parser;
using Bogus;
using NerdStore.WebApp.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Tests.Config
{
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public string AntiForgeryTokenName = "__RequestVerificationToken";

        public string UserEmail;
        public string UserPassword;
        public string UserToken;

        public readonly LojaAppFactory<TStartup> Factory;
        public readonly HttpClient Client;

        public IntegrationTestsFixture()
        {
            Factory = new LojaAppFactory<TStartup>();
            Client = Factory.CreateClient();
        }

        public void GenerateUser()
        {
            var faker = new Faker();
            UserEmail = faker.Internet.Email().ToLower();
            UserPassword = faker.Internet.Password(8, 16);
        }

        public async Task<string> GetAntiForgeryTokenValue(string content)
        {
            var document = (await new HtmlParser()
                .ParseDocumentAsync(content))
                .All;

            var antiForgeryTokenValue = document
                ?.FirstOrDefault(element => element.GetAttribute("name") == AntiForgeryTokenName)
                ?.GetAttribute("value");

            if (string.IsNullOrEmpty(antiForgeryTokenValue))
            {
                throw new ArgumentException($"O campo {AntiForgeryTokenName} não foi encontrado no HTML.", nameof(content));
            }

            return antiForgeryTokenValue;
        }

        public async Task RealizarLoginWeb()
        {
            var urlRequest = "/Identity/Account/Login";

            var initialResponse = await Client.GetAsync(urlRequest);

            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryTokenValue = await GetAntiForgeryTokenValue(await initialResponse.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                { AntiForgeryTokenName, antiForgeryTokenValue },
                { "Input.Email", "teste@teste.com" },
                { "Input.Password", "Teste@123" }
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, urlRequest)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            await Client.SendAsync(postRequest);
        }

        public async Task RealizarLoginApi()
        {
            var login = new LoginViewModel
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };

            var response = await Client.PostAsJsonAsync("api/login", login);

            response.EnsureSuccessStatusCode();

            UserToken = await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            Factory.Dispose();
            Client.Dispose();
        }
    }
}
