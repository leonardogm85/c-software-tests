using AngleSharp.Html.Parser;
using NerdStore.WebApp.Mvc;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class PedidoWebTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _testsFixture;

        public PedidoWebTests(IntegrationTestsFixture<StartupWebTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "IntegrationTests - Adicionar Item Em Novo Pedido")]
        [Trait("Categoria", "Teste de Integração - Pedido Web")]
        public async Task AdicionarItem_PedidoNovo_DeveAtualizarValorTotal()
        {
            // Arrange
            var produtoId = new Guid("7c7d75bf-21cf-44cf-bc9d-27acf3ba69ad");

            var initialResponse = await _testsFixture.Client.GetAsync($"/produto-detalhe/{produtoId}");

            initialResponse.EnsureSuccessStatusCode();

            var formData = new Dictionary<string, string>
            {
                { "Id", produtoId.ToString() },
                { "Quantidade", 2.ToString() }
            };

            await _testsFixture.RealizarLoginWeb();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/meu-carrinho")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();

            var document = (await new HtmlParser()
                .ParseDocumentAsync(responseString))
                .All;

            var inputQuantidade = document
                ?.FirstOrDefault(element => element.Id == "Quantidade")
                ?.GetAttribute("value");
            var inputUnitario = document
                ?.FirstOrDefault(element => element.Id == "ValorUnitario")
                ?.TextContent;
            var inputTotal = document
                ?.FirstOrDefault(element => element.Id == "ValorTotal")
                ?.TextContent;

            var regex = new Regex(@"^.{3}(([1-9]\d{0,2}(?:\.\d{3})*|0),\d{2})$");

            var valueQuantidade = Convert.ToDecimal(inputQuantidade);
            var valueUnitario = Convert.ToDecimal(regex.Replace(inputUnitario, "$1"));
            var valueTotal = Convert.ToDecimal(regex.Replace(inputTotal, "$1"));

            Assert.Equal(valueTotal, valueQuantidade * valueUnitario);
        }
    }
}
