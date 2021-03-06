﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace OrderPizza.Domain.Models
{
    public class Order
    {
        public Order()
        {
            Pizzas = new List<Pizza>();
        }

        public int? Id { get; set; }

        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual List<Pizza> Pizzas { get; set; }

        public decimal TotalValue { get; set; }

        public DateTime OrderDate { get; set; }

        public StatusPizza Status { get; set; }

        public void CalculateOrder()
        {
            foreach (var pizza in Pizzas)
            {
                pizza.Value = pizza.PizzaFlavors.Sum(x => x.Flavor.Value) / pizza.PizzaFlavors.Count;

                pizza.Value = decimal.Round(pizza.Value, 2, MidpointRounding.ToZero);
            }

            TotalValue = Pizzas.Sum(x => x.Value);

            TotalValue = decimal.Round(TotalValue, 2, MidpointRounding.ToZero);
        }

        public ValidationResult Validate()
        {
            var validator = new OrderValidator();
            return validator.Validate(this);
        }
    }

    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            When(x => x.CustomerId == null, () =>
            {
                RuleFor(x => x.Customer).NotNull().WithMessage("Deve ser informar os dados do solicitante.");
            });

            When(x => x.CustomerId == null && x.Customer != null, () =>
            {
                RuleFor(x => x.Customer.Address).NotNull().WithMessage("Deve ser informado o endereço de entrega.");
            });

            When(x => x.CustomerId == null && x.Customer?.Address != null, () =>
            {
                RuleFor(x => x.Customer.Address).NotNull().SetValidator(new AddressValidator());
            });

            RuleFor(x => x.Pizzas).NotNull().WithMessage("Deve ser informado pelo menos uma(1) pizza.");
            RuleFor(x => x.Pizzas).Must(x => x.Any()).WithMessage("Deve ser informado pelo menos uma(1) pizza.");
            RuleFor(x => x.Pizzas).Must(x => x.Count < 11)
                .WithMessage("O número máximo de pizzas por pedido é dez(10).");

            RuleForEach(x => x.Pizzas).ChildRules(pizza =>
            {
                pizza.RuleFor(x => x.PizzaFlavors).NotNull()
                    .WithMessage("Deve ser informado pelo menos um(1) sabor para a pizza.");
                pizza.RuleFor(x => x.PizzaFlavors).Must(x => x.Any())
                    .WithMessage("Deve ser informado pelo menos um(1) sabor para a pizza.");
                pizza.RuleFor(x => x.PizzaFlavors).Must(x => x.Count < 3)
                    .WithMessage("Cada pizza pode ter no máximo dois(2) sabores.");
            });

        }
    }

    public enum StatusPizza
    {
        [Description("Em Andamento")]
        InProgress = 1,

        [Description("Em Preparação")]
        InPreparation = 2,

        [Description("Pronto")]
        Done = 3,

        [Description("Saiu para Entrega")]
        OutForDelivery = 4,

        [Description("Entregue")]
        Delivered = 5,

        [Description("Cancelado")]
        Canceled = 6
    }
}
