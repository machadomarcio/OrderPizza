using System;
using System.Collections.Generic;
using System.Text;

namespace OrderPizza.Domain.Models
{
    class Pizza
    {
        public Pizza()
        {
            Flavors = new List<Flavor>();
        }
        public int Id { get; set; }

        public List<Flavor> Flavors { get; set; }

        public decimal Value { get; set; }
    }
}
