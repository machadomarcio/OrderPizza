using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    public class Flavor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public List<PizzaFlavor> PizzaFlavors { get; set; } 
    }
}
