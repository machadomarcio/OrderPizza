using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    class Order
    {
        public Order()
        {
            Pizzas = new List<Pizza>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public decimal TotalValue { get; set; }
    }
}
