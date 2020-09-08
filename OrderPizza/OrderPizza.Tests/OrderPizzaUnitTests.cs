using System.Linq;
using FluentAssertions;
using OrderPizza.Domain.Models;
using Xunit;

namespace OrderPizza.Tests
{
    public class OrderPizzaUnitTests
    {
        [Fact]
        public void Valor_pedido_valido_com_uma_pizza_e_um_sabor()
        {
            //Arrange
            var order = new Order();
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            order.CalculateOrder();

            //Assert
            order.TotalValue.Should().Be(flavor.Value);
            order.Pizzas.FirstOrDefault()?.Value.Should().Be(flavor.Value);
        }

        [Fact]
        public void Valor_pedido_valido_com_uma_pizza_e_dois_sabores()
        {
            //Arrange
            var order = new Order();
            var pizza = new Pizza();
            var flavor1 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var flavor2 = new Flavor { Name = "Pepperoni", Value = 55 };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            pizza.PizzaFlavors.Add(pizzaFlavor1);
            pizza.PizzaFlavors.Add(pizzaFlavor2);
            order.Pizzas.Add(pizza);

            //Act
            order.CalculateOrder();

            //Assert
            order.TotalValue.Should().Be(48.75M);
            order.Pizzas.FirstOrDefault()?.Value.Should().Be(48.75M);
        }

        [Fact]
        public void Valor_pedido_valido_com_duas_pizzas_e_um_sabor_cada()
        {
            //Arrange
            var order = new Order();
            var pizza1 = new Pizza();
            var flavor1 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            pizza1.PizzaFlavors.Add(pizzaFlavor1);
            order.Pizzas.Add(pizza1);

            var pizza2 = new Pizza();
            var flavor2 = new Flavor { Name = "Veggie", Value = 59.99M };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            pizza2.PizzaFlavors.Add(pizzaFlavor2);
            order.Pizzas.Add(pizza2);

            //Act
            order.CalculateOrder();

            //Assert
            order.TotalValue.Should().Be(102.49M);
            order.Pizzas.FirstOrDefault()?.Value.Should().Be(flavor1.Value);
            order.Pizzas.LastOrDefault()?.Value.Should().Be(flavor2.Value);
        }


        [Fact]
        public void Valor_pedido_valido_com_duas_pizzas_e_uma_de_um_sabor_e_outra_dois_sabores()
        {
            //Arrange
            var order = new Order();
            var pizza1 = new Pizza();
            var flavor1 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            pizza1.PizzaFlavors.Add(pizzaFlavor1);
            order.Pizzas.Add(pizza1);

            var pizza2 = new Pizza();
            var flavor2 = new Flavor { Name = "Veggie", Value = 59.99M };
            var flavor3 = new Flavor { Name = "Pepperoni ", Value = 55M };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            var pizzaFlavor3 = new PizzaFlavor { Flavor = flavor3 };
            pizza2.PizzaFlavors.Add(pizzaFlavor2);
            pizza2.PizzaFlavors.Add(pizzaFlavor3);
            order.Pizzas.Add(pizza2);

            //Act
            order.CalculateOrder();

            //Assert
            order.TotalValue.Should().Be(99.99M);
            order.Pizzas.FirstOrDefault()?.Value.Should().Be(42.5M);
            order.Pizzas.LastOrDefault()?.Value.Should().Be(57.49M);
        }

        [Fact]
        public void Valor_pedido_valido_com_duas_pizzas_dois_sabores_cada()
        {
            //Arrange
            var order = new Order();
            var pizza1 = new Pizza();
            var flavor1 = new Flavor { Name = "3 Queijos", Value = 50M };
            var flavor2 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            pizza1.PizzaFlavors.Add(pizzaFlavor1);
            pizza1.PizzaFlavors.Add(pizzaFlavor2);
            order.Pizzas.Add(pizza1);

            var pizza2 = new Pizza();
            var flavor3 = new Flavor { Name = "Veggie", Value = 59.99M };
            var flavor4 = new Flavor { Name = "Pepperoni ", Value = 55M };
            var pizzaFlavor3 = new PizzaFlavor { Flavor = flavor3 };
            var pizzaFlavor4 = new PizzaFlavor { Flavor = flavor4 };
            pizza2.PizzaFlavors.Add(pizzaFlavor3);
            pizza2.PizzaFlavors.Add(pizzaFlavor4);
            order.Pizzas.Add(pizza2);

            //Act
            order.CalculateOrder();

            //Assert
            order.TotalValue.Should().Be(103.74M);
            order.Pizzas.FirstOrDefault()?.Value.Should().Be(46.25M);
            order.Pizzas.LastOrDefault()?.Value.Should().Be(57.49M);
        }

        [Fact]
        public void Pedido_valido_com_uma_pizza_e_um_sabor()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeTrue();
            resultValidation.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Pedido_valido_com_uma_pizza_e_dois_sabores()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };
            var pizza = new Pizza();
            var flavor1 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var flavor2 = new Flavor { Name = "Pepperoni", Value = 55 };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            pizza.PizzaFlavors.Add(pizzaFlavor1);
            pizza.PizzaFlavors.Add(pizzaFlavor2);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeTrue();
            resultValidation.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Pedido_valido_com_duas_pizzas_e_um_sabor_cada()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };
            var pizza1 = new Pizza();
            var flavor1 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            pizza1.PizzaFlavors.Add(pizzaFlavor1);
            order.Pizzas.Add(pizza1);

            var pizza2 = new Pizza();
            var flavor2 = new Flavor { Name = "Veggie", Value = 59.99M };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            pizza2.PizzaFlavors.Add(pizzaFlavor2);
            order.Pizzas.Add(pizza2);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeTrue();
            resultValidation.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Pedido_valido_com_duas_pizzas_e_dois_sabores_cada()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };
            var pizza1 = new Pizza();
            var flavor1 = new Flavor { Name = "3 Queijos", Value = 50M };
            var flavor2 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            pizza1.PizzaFlavors.Add(pizzaFlavor1);
            pizza1.PizzaFlavors.Add(pizzaFlavor2);
            order.Pizzas.Add(pizza1);

            var pizza2 = new Pizza();
            var flavor3 = new Flavor { Name = "Veggie", Value = 59.99M };
            var flavor4 = new Flavor { Name = "Pepperoni ", Value = 55M };
            var pizzaFlavor3 = new PizzaFlavor { Flavor = flavor3 };
            var pizzaFlavor4 = new PizzaFlavor { Flavor = flavor4 };
            pizza2.PizzaFlavors.Add(pizzaFlavor3);
            pizza2.PizzaFlavors.Add(pizzaFlavor4);
            order.Pizzas.Add(pizza2);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeTrue();
            resultValidation.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Pedido_valido_com_dez_pizzas()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };

            for (var i = 0; i < 10; i++)
            {
                var pizza = new Pizza();
                var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
                var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
                pizza.PizzaFlavors.Add(pizzaFlavor);
                order.Pizzas.Add(pizza);
            }

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeTrue();
            resultValidation.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Pedido_invalido_com_nenhuma_pizza()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informado pelo menos uma(1) pizza.");
        }

        [Fact]
        public void Pedido_invalido_com_onze_pizzas()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };

            for (var i = 0; i < 11; i++)
            {
                var pizza = new Pizza();
                var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
                var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
                pizza.PizzaFlavors.Add(pizzaFlavor);
                order.Pizzas.Add(pizza);
            }

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("O número máximo de pizzas por pedido é dez(10).");
        }

        [Fact]
        public void Pedido_valido_com_uma_pizza_sem_sabor()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };
            var pizza = new Pizza();
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informado pelo menos um(1) sabor para a pizza.");
        }

        [Fact]
        public void Pedido_invalido_com_uma_pizzas_e_tres_sabores()
        {
            //Arrange
            var order = new Order { CustomerId = 1 };
            var pizza = new Pizza();
            var flavor1 = new Flavor { Name = "Calabresa", Value = 42.5M };
            var flavor2 = new Flavor { Name = "3 Queijos", Value = 50M };
            var flavor3 = new Flavor { Name = "Veggie", Value = 59.99M };
            var pizzaFlavor1 = new PizzaFlavor { Flavor = flavor1 };
            var pizzaFlavor2 = new PizzaFlavor { Flavor = flavor2 };
            var pizzaFlavor3 = new PizzaFlavor { Flavor = flavor3 };
            pizza.PizzaFlavors.Add(pizzaFlavor1);
            pizza.PizzaFlavors.Add(pizzaFlavor2);
            pizza.PizzaFlavors.Add(pizzaFlavor3);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Cada pizza pode ter no máximo dois(2) sabores.");
        }

        [Fact]
        public void Pedido_invalido_sem_cadastro_e_sem_informacoes_do_solicitante()
        {
            //Arrange
            var order = new Order();
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informar os dados do solicitante.");
        }

        [Fact]
        public void Pedido_invalido_sem_cadastro_e_sem_endereco_solicitante()
        {
            //Arrange
            var order = new Order { Customer = new Customer() };
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informado o endereço de entrega.");
        }

        [Fact]
        public void Pedido_invalido_sem_cadastro_e_sem_informacoes_do_endereco_solicitante()
        {
            //Arrange
            var order = new Order { Customer = new Customer { Address = new Address() } };
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(4);
        }

        [Fact]
        public void Pedido_invalido_sem_cadastro_e_sem_informacoes_de_rua_do_endereco_solicitante()
        {
            //Arrange
            var order = new Order
            {
                Customer = new Customer
                {
                    Address = new Address
                    {
                        Number = 56,
                        Complement = "Apto. 301",
                        Neighborhood = "Triâgulo",
                        City = "Lages",
                        State = "SC",
                    }
                }
            };
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informado a rua de entrega.");
        }

        [Fact]
        public void Pedido_invalido_sem_cadastro_e_sem_informacoes_de_numero_do_endereco_solicitante()
        {
            //Arrange
            var order = new Order
            {
                Customer = new Customer
                {
                    Address = new Address
                    {
                        Street = "Thiago Vieira de Castro",
                        Complement = "Apto. 301",
                        Neighborhood = "Triâgulo",
                        City = "Lages",
                        State = "SC",
                    }
                }
            };
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informado o número de entrega.");
        }

        [Fact]
        public void Pedido_invalido_sem_cadastro_e_sem_informacoes_de_bairro_do_endereco_solicitante()
        {
            //Arrange
            var order = new Order
            {
                Customer = new Customer
                {
                    Address = new Address
                    {
                        Street = "Thiago Vieira de Castro",
                        Number = 56,
                        Complement = "Apto. 301",
                        City = "Lages",
                        State = "SC",
                    }
                }
            };
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informado o bairro de entrega.");
        }

        [Fact]
        public void Pedido_invalido_sem_cadastro_e_sem_informacoes_de_cidade_do_endereco_solicitante()
        {
            //Arrange
            var order = new Order
            {
                Customer = new Customer
                {
                    Address = new Address
                    {
                        Street = "Thiago Vieira de Castro",
                        Number = 56,
                        Complement = "Apto. 301",
                        Neighborhood = "Triâgulo",
                        State = "SC",
                    }
                }
            };
            var pizza = new Pizza();
            var flavor = new Flavor { Name = "Calabresa", Value = 42.5M };
            var pizzaFlavor = new PizzaFlavor { Flavor = flavor };
            pizza.PizzaFlavors.Add(pizzaFlavor);
            order.Pizzas.Add(pizza);

            //Act
            var resultValidation = order.Validate();

            //Assert
            resultValidation.IsValid.Should().BeFalse();
            resultValidation.Errors.Count.Should().Be(1);
            resultValidation.Errors.FirstOrDefault().ErrorMessage.Should().Be("Deve ser informado a cidade de entrega.");
        }
    }
}
