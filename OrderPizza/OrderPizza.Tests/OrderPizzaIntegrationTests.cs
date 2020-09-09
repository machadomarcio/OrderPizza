using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using OrderPizza.API;
using OrderPizza.Domain.Models;
using Xunit;

namespace OrderPizza.Tests
{
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class OrderPizzaIntegrationTests
    {
        private readonly IntegrationTestFixture<StartupApiTests> _integrationTestFixture;
        public OrderPizzaIntegrationTests(IntegrationTestFixture<StartupApiTests> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Fact]
        public async Task Busca_todos_os_clientes_via_api()
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/Customer");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Busca_todos_os_pedidos_via_api()
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/OrderPizza");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Busca_pedido_inexistente_por_id_via_api()
        {
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/OrderPizza/5");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            requisicao.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            resposta.Should().Be("Pedido não encontrado!");
            Assert.False(requisicao.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Inserindo_um_cliente_incompleto_via_api()
        {
            var customer = new Customer();
            var jsonContent = JsonConvert.SerializeObject(customer);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requisicao = await _integrationTestFixture.Client.PostAsync($"/api/Customer", content);
            var resposta = await requisicao.Content.ReadAsStringAsync();

            requisicao.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            resposta.Should().Be("Não foi possível cadastrar o cliente. Erro: Deve ser informado o nome do cliente., Deve ser informado o CPF do cliente., Deve ser informado o telefone do cliente., Deve ser informado o endereço de entrega.");
            Assert.False(requisicao.IsSuccessStatusCode);

        }

        [Fact]
        public async Task Inserindo_um_cliente_valido_via_api()
        {
            var customer = new Customer
            {
                Cpf = "029.336.559-85",
                Name = "Marcio Machado",
                NickName = "Marcio",
                Phone = "(49)98802-2184",
                Address = new Address
                {
                    Street = "Thiago Vieira de Castro",
                    Number = 56,
                    Neighborhood = "Triângulo",
                    City = "Lages",
                    State = "SC"
                }
            };
            var jsonContent = JsonConvert.SerializeObject(customer);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requisicao = await _integrationTestFixture.Client.PostAsync($"/api/Customer", content);
            var resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);

        }

        [Fact]
        public async Task Inserindo_um_sabor_via_api()
        {
            var flavor = new Flavor
            {
                Name = "3 Queijos",
                Value = 50M
            };

            var jsonContent = JsonConvert.SerializeObject(flavor);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requisicao = await _integrationTestFixture.Client.PostAsync($"/api/Flavor", content);
            var resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);
        }


        [Fact]
        public async Task Inserindo_um_pedido_via_api()
        {
            var flavor1 = new Flavor
            {
                Name = "3 Queijos",
                Value = 50M
            };

            var jsonContent = JsonConvert.SerializeObject(flavor1);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requisicao = await _integrationTestFixture.Client.PostAsync($"/api/Flavor", content);
            var resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);

            var flavor2 = new Flavor
            {
                Name = "Veggie",
                Value = 59.99M
            };

            jsonContent = JsonConvert.SerializeObject(flavor2);
            content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            requisicao = await _integrationTestFixture.Client.PostAsync($"/api/Flavor", content);
            resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);

            var customer = new Customer
            {
                Cpf = "029.336.559-85",
                Name = "Marcio Machado",
                NickName = "Marcio",
                Phone = "(49)98802-2184",
                Address = new Address
                {
                    Street = "Thiago Vieira de Castro",
                    Number = 56,
                    Neighborhood = "Triângulo",
                    City = "Lages",
                    State = "SC"
                }
            };

            jsonContent = JsonConvert.SerializeObject(customer);
            content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            requisicao = await _integrationTestFixture.Client.PostAsync($"/api/Customer", content);
            resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);

            var order = new Order
            {
                CustomerId = 1,
                Status = StatusPizza.InProgress,
                OrderDate = DateTime.Now,
            };
            var pizza = new Pizza();

            var pizzaFlavor1 = new PizzaFlavor
            {
                FlavorId = 1,
                Flavor = flavor1
            };
            pizza.PizzaFlavors.Add(pizzaFlavor1);

            var pizzaFlavor2 = new PizzaFlavor
            {
                FlavorId = 2,
                Flavor = flavor2
            };
            pizza.PizzaFlavors.Add(pizzaFlavor2);

            order.Pizzas.Add(pizza);

            jsonContent = JsonConvert.SerializeObject(order);
            content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            requisicao = await _integrationTestFixture.Client.PostAsync($"/api/OrderPizza", content);
            resposta = await requisicao.Content.ReadAsStringAsync();

            Assert.True(requisicao.IsSuccessStatusCode);
        }

    }
}
