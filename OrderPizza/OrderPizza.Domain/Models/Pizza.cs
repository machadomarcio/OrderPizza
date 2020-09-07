using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    public class Pizza
    {
        public Pizza()
        {
            PizzaFlavors = new List<PizzaFlavor>();
        }
        public int Id { get; set; }

        public List<PizzaFlavor> PizzaFlavors { get; set; }

        public int Orderid { get; set; }

        public Order Order { get; set; }

        public decimal Value { get; set; }
    }
}
