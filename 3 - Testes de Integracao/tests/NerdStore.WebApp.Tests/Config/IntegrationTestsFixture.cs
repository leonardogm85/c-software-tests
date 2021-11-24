using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace NerdStore.WebApp.Tests.Config
{
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public string AntiForgeryTokenName = "__RequestVerificationToken";

        public string UserEmail;
        public string UserPassword;

        public readonly LojaAppFactory<TStartup> Factory;
        public readonly HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
            };

            Factory = new LojaAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public string GetAntiForgeryTokenValue(string content)
        {
            var requestVerificationTokenMatch = Regex.Match(
                content,
                $@"\<input name=""{AntiForgeryTokenName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
            {
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
            }

            throw new ArgumentException($"O campo {AntiForgeryTokenName} não foi encontrado no HTML.", nameof(content));
        }

        public void GenerateUser()
        {
            var faker = new Faker();
            UserEmail = faker.Internet.Email().ToLower();
            UserPassword = faker.Internet.Password(8, false, string.Empty, "@1Ab_");
        }

        public void Dispose()
        {
            Factory.Dispose();
            Client.Dispose();
        }
    }
}
