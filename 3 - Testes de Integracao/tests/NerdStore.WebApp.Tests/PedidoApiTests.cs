using NerdStore.WebApp.Mvc;
using NerdStore.WebApp.Mvc.Models;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [TestCaseOrderer(PriorityOrderer.ordererTypeName, PriorityOrderer.ordererAssemblyName)]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PedidoApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "IntegrationTests - Adicionar Item Em Novo Pedido"), Priority(1)]
        [Trait("Categoria", "Teste de Integração - Pedido Api")]
        public async Task AdicionarItem_PedidoNovo_DeveAtualizarValorTotal()
        {
            // Arrange
            var item = new ItemViewModel
            {
                Id = new Guid("7c7d75bf-21cf-44cf-bc9d-27acf3ba69ad"),
                Quantidade = 2
            };

            await _testsFixture.RealizarLoginApi();

            _testsFixture.Client.AtribuirToken(_testsFixture.UserToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/carrinho", item);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "IntegrationTests - Remvoer Item Em Pedido Existente"), Priority(2)]
        [Trait("Categoria", "Teste de Integração - Pedido Api")]
        public async Task RemoverItem_PedidoExistente_DeveRetornarComSucesso()
        {
            // Arrange
            var produtoId = new Guid("7c7d75bf-21cf-44cf-bc9d-27acf3ba69ad");

            await _testsFixture.RealizarLoginApi();

            _testsFixture.Client.AtribuirToken(_testsFixture.UserToken);

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"api/carrinho/{produtoId}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
